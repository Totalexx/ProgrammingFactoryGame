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
using UnityEditor;
using UnityEngine;

namespace Programming.CSharpCompiler
{
    public class Compiler
    {
        private static List<MetadataReference> metadataReferences = CompilerSettings.MetadataReferences;
        private static CSharpCompilationOptions compilationOptions = CompilerSettings.CompilationOptions;

        public static void Initialize() { }
        
        public static CancellationTokenSource CompileAndRun(string code, string typeName, string methodToRun)
        {
            var compilation = Compile(code);
            var compiledCode = EmitToArray(compilation);

            // AddCheckCancel(compilation.SyntaxTrees[0]);
            
            var assembly = Assembly.Load(compiledCode);
            var programType = assembly.GetType(typeName);
            var mainMethod = programType.GetMethod(methodToRun);
            
            var cancellationToken = new CancellationTokenSource();
            var robotTask = new Task(() =>  mainMethod.Invoke(null, null), cancellationToken.Token);
            robotTask.ContinueWith(t => Debug.Log(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
            robotTask.Start();

            return cancellationToken;
        }
        
        private static CSharpCompilation Compile(string code)
        {
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
            var compilation = CSharpCompilation.Create
            (
                GUID.Generate().ToString(),
                new[] { syntaxTree },
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

        private static void AddCheckCancel(SyntaxTree syntaxTree)
        {
            var nodes = syntaxTree.GetRoot().DescendantNodes();
            var es = syntaxTree.GetRoot().DescendantNodes().Where(b => b is ExpressionStatementSyntax);
            var methods = syntaxTree
                .GetRoot()
                .DescendantNodes()
                .Where(b => b is MethodDeclarationSyntax)
                .Cast<MethodDeclarationSyntax>()
                .FirstOrDefault();
            
            var cancelString = "ct.ThrowIfCancellationRequested();";
            var st = SyntaxFactory.ParseSyntaxTree(cancelString).GetRoot();
            var a = methods.Body.InsertNodesBefore(methods.Body.Statements.First().DescendantNodes().First(), new[] { st });
        }

        // private static StatementSyntax Add()
        // {
        //     var cancelString = "ct.ThrowIfCancellationRequested();";
        //     return SyntaxFactory.ParseSyntaxTree(cancelString).GetRoot();
        // }
    }
}