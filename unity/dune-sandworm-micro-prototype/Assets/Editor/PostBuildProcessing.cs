using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Editor
{
    public static class PostBuildProcessing
    {
        [PostProcessBuild]
        public static void BuildPostProcess(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.WebGL)
                WebGLBuildPostProcess(target, pathToBuiltProject);
        }

        private static void WebGLBuildPostProcess(BuildTarget target, string pathToBuiltProject)
        {
            var buildFolderPath = Path.Combine(pathToBuiltProject, "Build");
            var info = new DirectoryInfo(buildFolderPath);
            var files = info.GetFiles("*.js");
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var filePath = file.FullName;
                var text = File.ReadAllText(filePath);
                text = text.Replace("UnityLoader.SystemInfo.mobile", "false");

                Debug.Log("Removing mobile warning from " + filePath);
                File.WriteAllText(filePath, text);
            }
        }
    }
}