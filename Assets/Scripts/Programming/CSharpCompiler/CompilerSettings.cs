using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;

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
            typeof(RobotCommands).Assembly,
            typeof(System.Linq.Enumerable).Assembly,
            typeof(MoveDirection).Assembly
        };

        public static readonly List<string> Namespaces = new()
        {
            "System",
            "System.Linq",
            "System.Collections.Generic",
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
    }
}