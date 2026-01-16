#if UNITY_EDITOR && UNITY_IOS
using System.Diagnostics;
using System.IO;
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

        // Unity build path is the Builds folder itself
        var buildsDir   = pathToBuiltProject;
        var podfilePath = Path.Combine(buildsDir, "Podfile");

        if (!File.Exists(podfilePath))
        {
            UnityEngine.Debug.Log("[iOSPodInstallPostBuild] No Podfile found in " + buildsDir + ", skipping pod install.");
            return;
        }

        var process = new Process();
        process.StartInfo.FileName = "/bin/bash";

        const string rubyBin = "/opt/homebrew/opt/ruby/bin";
        const string gemBin  = "/opt/homebrew/lib/ruby/gems/4.0.0/bin"; // adjust if Gem.bindir differs

        var cmd =
            "export LANG=en_US.UTF-8; " +
            "export LC_ALL=en_US.UTF-8; " +
            "export PATH=\"" + rubyBin + ":" + gemBin + ":$PATH\"; " +
            "cd '" + buildsDir + "' && pod install --repo-update";

        process.StartInfo.Arguments = "-lc \"" + cmd + "\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string error  = process.StandardError.ReadToEnd();

        process.WaitForExit();

        UnityEngine.Debug.Log("[iOSPodInstallPostBuild] pod install output:\n" + output);

        if (!string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.LogWarning("[iOSPodInstallPostBuild] pod install errors:\n" + error);
        }
    }
}
#endif
