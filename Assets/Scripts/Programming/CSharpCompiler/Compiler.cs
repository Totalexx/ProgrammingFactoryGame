using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using UnityEditor;
using UnityEngine;

namespace Programming.CSharpCompiler
{
    public class Compiler
    {
        private static List<MetadataReference> metadataReferences = CompilerSettings.MetadataReferences;
        private static CSharpCompilationOptions compilationOptions = CompilerSettings.CompilationOptions;
        
        private static readonly string usingSystemThreading = "using System.Threading;";
        private static readonly string cancelTokenVariable =
            "public static CancellationTokenSource ct = new ();";
        private static readonly string cancelString = "ct.Token.ThrowIfCancellationRequested();";

        private static readonly UsingDirectiveSyntax usingSystemThreadingSyntax =
            ((CompilationUnitSyntax)SyntaxFactory.ParseSyntaxTree(usingSystemThreading).GetRoot()).Usings[0];
        private static readonly FieldDeclarationSyntax[] cancelToken = 
        {
            SyntaxFactory.ParseSyntaxTree(cancelTokenVariable)
                .GetRoot()
                .DescendantNodes()
                .Where(b => b is FieldDeclarationSyntax)
                .Cast<FieldDeclarationSyntax>()
                .First()
        };
        private static readonly ExpressionStatementSyntax[] cancelNode = 
        {
            SyntaxFactory
                .ParseSyntaxTree(cancelString)
                .GetRoot()
                .DescendantNodes()
                .Where(b => b is ExpressionStatementSyntax)
                .Cast<ExpressionStatementSyntax>()
                .First()
        };

        public static void Initialize() { }
        
        public static CancellationTokenSource CompileAndRun(string code, string typeName, string methodToRun)
        {
            var cancellationToken = new CancellationTokenSource();
            var compilation = Compile(code, cancellationToken.Token);
            var compiledCode = EmitToArray(compilation);

            
            var assembly = Assembly.Load(compiledCode);
            var programType = assembly.GetType(typeName);
            var mainMethod = programType.GetMethod(methodToRun);
            
            var robotTask = new Task(() =>  mainMethod.Invoke(null, null), cancellationToken.Token);
            robotTask.ContinueWith(t => Debug.Log(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
            robotTask.Start();

            return null;//cancellationToken;
        }
        
        private static CSharpCompilation Compile(string code, CancellationToken cancellationToken)
        {
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
            var treeWithCancels = AddCheckCancel(syntaxTree);
            var compilation = CSharpCompilation.Create
            (
                GUID.Generate().ToString(),
                new[] { treeWithCancels },
                options: compilationOptions,
                references: metadataReferences
            );
            
            return compilation;
        }
        
        private static byte[] EmitToArray(Compilation compilation)
        {
            using var stream = new MemoryStream();
            
            var emitResult = compilation.Emit(stream);
            if (emitResult.Success) 
                return stream.ToArray();
            
            var firstError = emitResult
                .Diagnostics
                .FirstOrDefault(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
            
            throw new Exception(firstError?.ToString());
        }

        private static SyntaxTree AddCheckCancel(SyntaxTree syntaxTree)
        {
            var root = ((CompilationUnitSyntax)syntaxTree.GetRoot()).AddUsings(usingSystemThreadingSyntax);
            var rootNodes = root
                .DescendantNodes();
        
            var classDeclaration = rootNodes
                .Where(b => b is ClassDeclarationSyntax)
                .Cast<ClassDeclarationSyntax>()
                .First();
            var blocks = rootNodes
                .Where(b => b is BlockSyntax)
                .Cast<BlockSyntax>();
        
            var editor = new SyntaxEditor(root, new AdhocWorkspace());

            editor.InsertMembers(classDeclaration, 0, cancelToken);
        
            foreach (var block in blocks)
            {
                var nodesToInsert = block?.Statements;
                if (nodesToInsert == null) continue;

                var test = editor.GetChangedRoot();
            
                foreach (var statementSyntax in nodesToInsert)
                    editor.InsertBefore(statementSyntax, cancelNode);
            }

            var changed = editor.GetChangedRoot().SyntaxTree;
            return changed;
        }
    }
}