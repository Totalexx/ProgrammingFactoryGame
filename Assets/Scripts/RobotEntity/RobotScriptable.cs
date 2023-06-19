using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Programming.CSharpCompiler;
using Programming.ProjectCreator;
using UnityEngine;

namespace RobotEntity
{
    public class RobotScriptable : MonoBehaviour
    {
        private static string programPath;
        private string scriptPath;
        public CancellationTokenSource CancellationToken { get; private set; }

        private void Start()
        {
            var robotName = GetComponent<RobotController>().RobotName;
            var programName = "Robot" + robotName;
            
            programPath = Path
                .Combine(Path.GetDirectoryName(Application.dataPath), "Programming");
            scriptPath = Path.Combine(programPath, programName + ".cs");
            
            CreateProgramFileIfNotExist(scriptPath, programName, robotName);
        }

        public void StartProgram()
        {
            var robotName = GetComponent<RobotController>().RobotName;
            var programName = "Robot" + robotName;
            CancellationToken = Compiler.CompileAndRun(GetScript(programName, robotName), programName, "Start");
        }

        private string GetScript(string programName, string robotName)
        {
            CreateProgramFileIfNotExist(scriptPath, programName, robotName);
            
            var reader = new StreamReader(scriptPath);
            var script = reader.ReadToEnd();
            reader.Close();
            
            return script.Replace("Programming.Api", "Programming");
        }

        private void CreateProgramFileIfNotExist(string scriptPath, string programName, string robotName)
        {
            if (!File.Exists(scriptPath))
                ProjectCreator.CreateFileExample(scriptPath, programName, robotName);
        }
    }
}