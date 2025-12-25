using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class BuildScript
{
    private static string[] GetScenes()
    {
        return new string[]
        {
            "Assets/Scenes/WorldMap.unity"
        };
    }

    [MenuItem("Build/Build Android APK %#&a")] // Ctrl+Shift+Alt+A
    public static void BuildAndroid()
    {
        // Build klasörünü oluştur
        string buildPath = "Builds/Android";
        if (!Directory.Exists(buildPath))
        {
            Directory.CreateDirectory(buildPath);
        }

        // Player ayarları
        PlayerSettings.productName = "Empire Wars";
        PlayerSettings.companyName = "EmpireWars";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.empirewars.game");
        PlayerSettings.bundleVersion = "0.1.0";
        PlayerSettings.Android.bundleVersionCode = 1;

        // Android ayarları
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24; // Android 7.0
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel34; // Android 14
        EditorUserBuildSettings.androidBuildType = AndroidBuildType.Release;
        EditorUserBuildSettings.buildAppBundle = false; // APK olarak derle

        // Hedef mimari - ARMv7 + ARM64 (geniş uyumluluk)
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;

        // IL2CPP kullan (ARM64 için gerekli)
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        // Build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetScenes(),
            locationPathName = Path.Combine(buildPath, "EmpireWars.apk"),
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        Debug.Log("Android build başlatılıyor...");

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"Android build başarılı! Boyut: {summary.totalSize / (1024 * 1024)} MB");
            Debug.Log($"APK konumu: {buildPlayerOptions.locationPathName}");
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError("Android build başarısız!");
        }
    }

    // Komut satırından çağrılabilir
    public static void BuildAndroidFromCommandLine()
    {
        BuildAndroid();
    }
}
