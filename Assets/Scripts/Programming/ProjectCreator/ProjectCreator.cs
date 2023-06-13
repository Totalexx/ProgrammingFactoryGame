using System.IO;
using Programming.CSharpCompiler;
using Programming.MainThread;
using UnityEditor;
using UnityEngine;

namespace Programming.ProjectCreator
{
    public class ProjectCreator : MonoBehaviour
    {
        private const string projectName = "Programming";
        
        public void Start()
        {
            CreateProjectFolderIfNeed();
            Compiler.Initialize();
            MainContextHolder.Initialize();
        }

        private void CreateProjectFolderIfNeed()
        {
            var playerProjectLocation = Path.Combine(Path.GetDirectoryName(Application.dataPath), projectName);
            
            if (Directory.Exists(playerProjectLocation))
                return;
            
            var initialProjectLocation = Path.Combine(Application.streamingAssetsPath, projectName);
            
            CopyFilesRecursively(initialProjectLocation, playerProjectLocation);
        }
        
        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));

            foreach (var newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}