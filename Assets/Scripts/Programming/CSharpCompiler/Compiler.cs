using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using UnityEditor;

namespace Programming.CSharpCompiler
{
    public class Compiler
    {
        private readonly List<MetadataReference> metadataReferences = new();
        private CSharpCompilationOptions compilationOptions;
        private ScriptOptions scriptOptions;
        
        public Compiler()
        {
            InitializeReferences();
            InitializeOptions();
        }

        public void RunFile()
        {
            var script = "using Programming; RobotCommands.MoveTo(MoveDirection.UP);";
            var cscript = CSharpScript.EvaluateAsync(script, scriptOptions).GetAwaiter().GetResult();
        }
        
        private void InitializeReferences()
        {
            foreach (var assembly in CompilerSettings.Assembly)
            {
                var assemblyLocation = assembly.Location;
                var reference = MetadataReference.CreateFromFile(assemblyLocation);
                metadataReferences.Add(reference);
            }
        }

        private void InitializeOptions()
        {
            compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithOptimizationLevel(CompilerSettings.OptimizationLevel)
                .WithAllowUnsafe(CompilerSettings.AllowUnsafeCode)
                .WithConcurrentBuild(CompilerSettings.AllowConcurrentCompile);

            scriptOptions = ScriptOptions
                .Default
                .WithOptimizationLevel(CompilerSettings.OptimizationLevel)
                .WithAllowUnsafe(CompilerSettings.AllowUnsafeCode)
                .WithReferences(metadataReferences)
                .WithImports(CompilerSettings.Namespaces);
        }
        
        private void CompileFile(string script)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(script);
            var syntaxNodes = syntaxTree.GetRoot().DescendantNodes();
            var compilationUnit = syntaxTree.GetCompilationUnitRoot();
            
            var compilation = CSharpCompilation.Create(
                GUID.Generate().ToString(), 
                new[] { syntaxTree }, 
                metadataReferences, 
                compilationOptions);
            var model = compilation.GetSemanticModel(syntaxTree);
        }
    }
}