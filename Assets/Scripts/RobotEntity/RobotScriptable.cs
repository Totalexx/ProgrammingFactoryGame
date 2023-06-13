using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Programming.CSharpCompiler;
using UnityEngine;

namespace RobotEntity
{
    public class RobotScriptable : MonoBehaviour
    {
        private static string programPath;
        public CancellationTokenSource CancellationToken { get; private set; }

        private void Start()
        {
            programPath = Path
                .Combine(Path.GetDirectoryName(Application.dataPath), "Programming");
        }

        public void StartProgram()
        {
            var programName = "Program";//"Robot" + GetComponent<RobotController>().RobotName;
            CancellationToken = Compiler.CompileAndRun(GetScript(programName), programName, "Main");
        }

        private string GetScript(string programName)
        {
            var scriptPath = Path.Combine(programPath, programName + ".cs");
            var reader = new StreamReader(scriptPath);
            var script = reader.ReadToEnd();
            reader.Close();
            return script.Replace("Programming.Api", "Programming");
        }
        
        static List<string> GetClasses(string nameSpace)
        {
            var asm = Assembly.GetExecutingAssembly();
            return (from type in asm.GetTypes() where type.Namespace == nameSpace select type.Name).ToList();
        }
    }
}