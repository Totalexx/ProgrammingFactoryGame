using Programming.CSharpCompiler;
using UnityEngine;

namespace Programming
{
    public class ScriptRunner : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                new Compiler().RunWithCompile();
            }
        }
    }
}