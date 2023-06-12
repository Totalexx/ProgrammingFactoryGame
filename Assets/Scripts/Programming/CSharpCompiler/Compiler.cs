using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using UnityEditor;
using UnityEngine;

namespace Programming.CSharpCompiler
{
    public class Compiler
    {
        private static List<MetadataReference> metadataReferences = CompilerSettings.MetadataReferences;
        private static CSharpCompilationOptions compilationOptions = CompilerSettings.CompilationOptions;

        public static void Initialize() { }
        
        public static void CompileAndRun(string code, string typeName, string methodToRun)
        {
            var compiledCode = EmitToArray(Compile(code));
            var assembly = Assembly.Load(compiledCode);
            var programType = assembly.GetType(typeName);
            var mainMethod = programType.GetMethod(methodToRun);
            var robotTask = new Task(() =>  mainMethod.Invoke(null, null));
            robotTask.ContinueWith(t => Debug.Log(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
            robotTask.Start();
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
    }
}