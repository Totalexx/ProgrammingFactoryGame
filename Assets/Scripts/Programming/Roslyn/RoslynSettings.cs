using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp
{
    [CreateAssetMenu(fileName ="RoslynSettings", menuName = "RoslynCSharp/Create a RoslynCSharp Setting")]
    public class RoslynSettings : ScriptableObject
    {
        public OptimizationLevel optimizationLevel = OptimizationLevel.Release;

        public bool allowUnsafeCode = false;

        public bool allowConcurrentCompile = false;

        public bool generateInMemory = true;

        public List<string> assemblyName = new List<string>() { "UnityEngine.dll"};

        public bool shouldWeCheckSecurity = true;
        
        public bool _
        {
            get
            {
                return !shouldWeCheckSecurity;
            }
        }

        public List<string> prohibitedNamespace = new List<string>() { "System.IO.*", "System.Reflection.*" };

        public List<string> prohibitedType = new List<string>() { "System.AppDomain" };

        public List<string> prohibitedMember = new List<string>() { "UnityEngine.Application.Quit" };
    }
}