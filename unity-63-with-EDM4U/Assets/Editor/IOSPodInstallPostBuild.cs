#if UNITY_EDITOR && UNITY_IOS
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class IOSPodInstallPostBuild
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS)
            return;

        var process = new Process();
        process.StartInfo.FileName = "/bin/bash";

        // Use -lc so it loads your shell env (PATH with Homebrew Ruby + pod)
        process.StartInfo.Arguments =
            "-lc \"cd '" + pathToBuiltProject + "' && pod install --repo-update\"";

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        UnityEngine.Debug.Log("[iOSPodInstallPostBuild] pod install output:\n" + output);

        if (!string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.LogWarning("[iOSPodInstallPostBuild] pod install errors:\n" + error);
        }
    }
}
#endif
