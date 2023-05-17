using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using UnityEditor;
using UnityEngine;

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
            var scriptPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.dataPath), "Programming"), "Program.cs");
            var script = new StreamReader(scriptPath).ReadToEnd();
            script = script.Replace("Programming.Api", "Programming");
            
            var cscript = CSharpScript
                .RunAsync(script, scriptOptions)
                .GetAwaiter()
                .GetResult();
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

        public void RunWithCompile()
        {
            var scriptPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.dataPath), "Programming"), "Program.cs");
            var script = new StreamReader(scriptPath).ReadToEnd();
            script = script.Replace("Programming.Api", "Programming");
            var compiledCode = EmitToArray(Compile(script));
            var assembly = Assembly.Load(compiledCode);
            var programType = assembly.GetType("Program");
            var mainMethod = programType.GetMethod("Main");
            mainMethod.Invoke(null, null);
        }
        
        private CSharpCompilation Compile(string code)
        {
            SyntaxTree syntaxTree = SyntaxFactory.ParseSyntaxTree(code, null, "");

            CSharpCompilation compilation = CSharpCompilation.Create
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
            using (var stream = new MemoryStream())
            {
                // emit result into a stream
                var emitResult = compilation.Emit(stream);

                if (!emitResult.Success)
                {
                    // if not successful, throw an exception
                    Diagnostic firstError =
                        emitResult
                            .Diagnostics
                            .FirstOrDefault
                            (
                                diagnostic =>
                                    diagnostic.Severity == DiagnosticSeverity.Error
                            );

                    throw new Exception(firstError?.GetMessage());
                }

                // get the byte array from a stream
                return stream.ToArray();
            }
        }

    }
    
}