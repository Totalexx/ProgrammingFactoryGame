using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Programming.CSharpCompiler
{
    public static class CompilerSettings
    {
        public static readonly bool AllowUnsafeCode = false;

        public static readonly bool AllowConcurrentCompile = false;

        public static readonly bool GenerateInMemory = true;

        public static readonly bool ShouldWeCheckSecurity = true;
        
        public static readonly OptimizationLevel OptimizationLevel = OptimizationLevel.Release;
        
        public static readonly List<Assembly> Assembly = new()
        {
            typeof(UnityEngine.Debug).Assembly,
            typeof(Robot).Assembly,
            typeof(Enumerable).Assembly,
            typeof(MoveDirection).Assembly,
            typeof(System.Object).Assembly
        };

        public static readonly List<string> ProhibitedNamespace = new ()
        {
            "System.IO.*", "System.Reflection.*"
        };

        public static readonly List<string> ProhibitedType = new()
        {
            "System.AppDomain"
        };

        public static readonly List<string> ProhibitedMember = new()
        {
            "UnityEngine.Application.Quit"
        };

        public static List<MetadataReference> MetadataReferences
        {
            get
            {
                return Assembly
                    .Select(assembly => assembly.Location)
                    .Select(assemblyLocation => MetadataReference.CreateFromFile(assemblyLocation))
                    .Cast<MetadataReference>()
                    .ToList();
            }
        }
        
        public static CSharpCompilationOptions CompilationOptions =>
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithOptimizationLevel(OptimizationLevel)
                .WithAllowUnsafe(AllowUnsafeCode)
                .WithConcurrentBuild(AllowConcurrentCompile);
    }
}