using UnityEngine;
using Programming.CSharpCompiler;

public class ScriptRunner : MonoBehaviour
{
    // private static readonly ScriptOptions scriptOptions = ScriptOptions.Default
        // .AddImports("System", "System.Collections.Generic",
        //     "System.Linq", "System.Text", "RobotProgramming")
        // .AddReferences("System", "System.Core", "Microsoft.CSharp")
        // .WithReferences(typeof(RobotCommands).Assembly);

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
            RunScript();
    }

    void RunScript()
    {
        var compiler = new Compiler();
        compiler.RunFile();
        // var stringScript = await new StreamReader("Assets/Resources/TestScript.cs").ReadToEndAsync();
        // var script = CSharpScript.Create("using RobotProgramming; RobotCommands.Debug();", options: scriptOptions);
        // script.Compile();
        // script.RunAsync();
    }
}
