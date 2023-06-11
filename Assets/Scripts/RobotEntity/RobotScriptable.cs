using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Programming.CSharpCompiler;
using UnityEngine;

namespace RobotEntity
{
    public class RobotScriptable : MonoBehaviour
    {
        private static string programPath;

        private void Start()
        {
            programPath = Path
                .Combine(Path.GetDirectoryName(Application.dataPath), "Programming");
        }

        public void StartProgram()
        {
            var programName = "Program";//"Robot" + GetComponent<RobotController>().RobotName;
            Compiler.CompileAndRun(GetScript(programName), programName, "Main");
        }

        private string GetScript(string programName)
        {
            var scriptPath = Path.Combine(programPath, programName + ".cs");
            var script = new StreamReader(scriptPath).ReadToEnd();
            return script.Replace("Programming.Api", "Programming");
        }
        
        static List<string> GetClasses(string nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            List<string> namespacelist = new List<string>();
            List<string> classlist = new List<string>();

            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == nameSpace)
                    namespacelist.Add(type.Name);
            }

            foreach (string classname in namespacelist)
                classlist.Add(classname);

            return classlist;
        }
    }
}