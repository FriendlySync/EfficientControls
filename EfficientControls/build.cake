//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
readonly var target = Argument("target", "Default");
readonly var configuration = Argument("configuration", "Debug");
readonly bool sign = Argument("sign", true);

//////////////////////////////////////////////////////////////////////
// VARIABLES
//////////////////////////////////////////////////////////////////////
readonly var VER_MAJOR = "1";
readonly var VER_MINOR = "0";
readonly var VER_PATCH = "2";

//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
readonly var SCRIPT_DIR = System.IO.Directory.GetCurrentDirectory();
readonly var solutionFullPath = PathCombine(SCRIPT_DIR, "EfficientControls.sln");
readonly var binDir = PathCombine(SCRIPT_DIR, "bin", configuration);
readonly var objDir = PathCombine(SCRIPT_DIR, "obj", configuration);


//////////////////////////////////////////////////////////////////////
// HELPER METHODS
//////////////////////////////////////////////////////////////////////

string PathCombine(params string[] parts)
{
    return System.IO.Path.Combine(parts);
}

string AsPlatformPath(FilePath file)
{
    string[] parts = file.Segments;
    if (IsRunningOnWindows() && parts.Length > 0 && parts[0].Length == 2 && parts[0][1] == ':') {
        parts[0] = parts[0] + "\\";
    }

    return System.IO.Path.Combine(parts);
}

void WriteFileIfDifferent(string filePath, string expectedContent)
{
    byte[] expectedBytes = Encoding.UTF8.GetBytes(expectedContent);
    byte[] actualBytes = System.IO.File.ReadAllBytes(filePath);
    
    if (!actualBytes.SequenceEqual(expectedBytes)) {
        System.IO.File.WriteAllBytes(filePath, expectedBytes);
    }
}


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(binDir);
    CleanDirectory(objDir);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFullPath);
});

Task("SetVersion")
    .Does(() =>
{
    string filePath = PathCombine(SCRIPT_DIR, "EfficientControls.csproj");
    string oldText = System.IO.File.ReadAllText(filePath);
    string pattern = "<Version>[0-9.]+</Version>";
    string newText = System.Text.RegularExpressions.Regex.Replace(oldText, pattern, $"<Version>{VER_MAJOR}.{VER_MINOR}.{VER_PATCH}</Version>");

    WriteFileIfDifferent(filePath, newText);
});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("SetVersion")
    .Does(() =>
{
      // Use MSBuild
      MSBuild(solutionFullPath, settings => {
        settings.SetConfiguration(configuration);
        settings.UseToolVersion(MSBuildToolVersion.VS2019);
        settings.WithProperty("ContinuousIntegrationBuild", "true");
      });
});

Task("Sign")
    .IsDependentOn("Build")
    .Does(() =>
{
    if (sign) {
        FilePath nugetPath = Context.Tools.Resolve("nuget.exe");
        string origNupkgPath = PathCombine(binDir, $"EfficientControls.{VER_MAJOR}.{VER_MINOR}.{VER_PATCH}.nupkg");
        string outputDirectory = PathCombine(SCRIPT_DIR, "nupkg");
        
        StartProcess(nugetPath, new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append("sign")
            .Append(origNupkgPath)
            .Append("-OutputDirectory")
            .Append(outputDirectory)
            .Append("-CertificateSubjectName")
            .Append("FriendlySync")
            .Append("-Timestamper")
            .Append("http://timestamp.digicert.com")
        });
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Sign");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
