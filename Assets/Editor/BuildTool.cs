using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Editor
{
    public class BuildTool 
    {
        [MenuItem("BuildTool/Build")]
        private static void Build()
        {
            Console.WriteLine($"Start build...........{DateTime.Now:HHmmdd}");
            CustomizedCommandLine();
            if(BuildTool._action.ToLower() == "exportpackage")
            {
                Console.WriteLine($"Package name: {BuildTool._packageName}, Build version: {BuildTool._versionNumber}");
                ExportPackage(BuildTool._packageName, BuildTool._versionNumber);
            }
            //
            // string destinationPath = Path.Combine(_destinationPath, PlayerSettings.productName);
            // destinationPath += GetExtension();
            //
            // BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, destinationPath, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
            // var task = new BuildTask
            //            {
            //                BuildPath = "/Users/jamisliao/Unity/Flow-unity-sdk/",
            //                ProductName = "iOS",
            //                BuildTarget = BuildTarget.iOS,
            //            };
            // var buildScenes = new List<EditorBuildSettingsScene>
            //                   {
            //                       new EditorBuildSettingsScene("Assets/Scenes/EvmScene.unity", true),
            //                   };
            //
            // var locationPathName = $"{task.BuildPath}/{task.ProductName}";
            // var report = BuildPipeline.BuildPlayer(buildScenes.ToArray(), locationPathName, task.BuildTarget, BuildOptions.None);
            // Debug.Log($"[{task.ProductName}] 打包结果: {report.summary.result}\r\n");
        }
 
 
        private static string _destinationPath;
        private static string _packageName;
        private static string _versionNumber;
        private static string _action;
        private static void CustomizedCommandLine()
        {
            Dictionary<string, Action<string>> cmdActions = new Dictionary<string, Action<string>>
                                                            {
                                                                {
                                                                    "-destinationPath", delegate(string argument)
                                                                                        {
                                                                                            _destinationPath = argument;
                                                                                        }
                                                                },
                                                                {
                                                                    "-packageName", delegate(string argument)
                                                                                    {
                                                                                        BuildTool._packageName = argument;
                                                                                    }
                                                                },
                                                                {
                                                                    "-buildVersion", delegate(string argument)
                                                                                {
                                                                                    BuildTool._versionNumber = argument;
                                                                                }
                                                                },
                                                                {
                                                                    "-action", delegate(string argument)
                                                                               {
                                                                                   BuildTool._action = argument;
                                                                               }
                                                                },
                                                            };
 
            Action<string> actionCache;
            var cmdArguments = Environment.GetCommandLineArgs();
 
            for (int count = 0; count < cmdArguments.Length; count++)
            {
                Console.WriteLine($"Argument: {cmdArguments[count]}");
                if (cmdActions.ContainsKey(cmdArguments[count]))
                {
                    actionCache = cmdActions[cmdArguments[count]];
                    actionCache(cmdArguments[count + 1]);
                }
            }
 
            if (string.IsNullOrEmpty(_destinationPath))
            {
                _destinationPath = Path.GetDirectoryName(Application.dataPath);
            }
        }
 
        private static void ExportPackage(string packageName, string buildVersion)
        {
            var directories = new List<string>();
            var packageType = default(object);
            var result = Enum.TryParse(typeof(PackageTypeEnum), packageName, true, out packageType);
            if(result == false)
            {
                throw new ArgumentException("Package type not found.");
            }
        
            switch ((PackageTypeEnum)packageType)
            {
                case PackageTypeEnum.FCL:
                    var fclAssetsPaths = new List<string>
                                         {
                                             "Assets/Plugins/Flow/FCL",
                                             "Assets/Plugins/Flow/Flow.Net.Sdk",
                                             "Assets/Plugins/Flow/Flow.Net.Sdk.Client.Unity",
                                             "Assets/Plugins/Flow/Flow.Net.Sdk.Core",
                                             "Assets/Plugins/Flow/Flow.Net.Sdk.Utility",
                                             "Assets/Plugins/Dll",
                                             "Assets/Plugins/System.ComponentModel.Annotations.dll"
                                         };
                    AssetDatabase.ExportPackage(fclAssetsPaths.ToArray(), $"release/fcl-unity/fcl-unity.{buildVersion}.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.Default);
                    Debug.Log("FCL export successed.");
                    break;
                case PackageTypeEnum.BloctoUnitySDK:
                    directories.Add("Assets/Plugins/Blocto.Sdk/Core");
                    directories.Add("Assets/Plugins/Blocto.Sdk/Flow");
                    directories.Add("Assets/Plugins/Dll");
                    directories.Add($"Assets/Plugins/Android");
                    directories.Add($"Assets/Plugins/iOS/UnityIosPlugin");
                    AssetDatabase.ExportPackage(directories.ToArray(), $"release/blocto-unity-sdk/Blocto-unity-sdk.{buildVersion}.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.Default);
                    Debug.Log("Blocto-unity-SDK export successed.");
                    break;
                case PackageTypeEnum.Core:
                    var coreOutputPath = $"release/{buildVersion}";
                    if(Directory.Exists(coreOutputPath) == false)
                    {
                        Directory.CreateDirectory(coreOutputPath);
                    }
                
                    var coreDirInfo = new DirectoryInfo($"{Application.dataPath}/Plugins/Blocto.Sdk/Core");
                    var coreDirPaths = coreDirInfo.GetDirectories().Select(p => {
                                                                               var tmp = p.FullName.Split("Assets/")[1];
                                                                               return $"Assets/{tmp}";
                                                                           }).ToList(); 
                    directories.AddRange(coreDirPaths);
                    directories.Add("Assets/Plugins/Dll");
                    directories.Add($"Assets/Plugins/Android");
                    directories.Add($"Assets/Plugins/iOS/UnityIosPlugin");
                    AssetDatabase.ExportPackage(directories.ToArray(), $"release/{buildVersion}/Portto.Blocto.Core.{buildVersion}.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.Default);
                    Debug.Log("Portto.Blocto.Core export successed.");
                    break;
                case PackageTypeEnum.Solana:
                    var solanaOutputPath = $"release/{buildVersion}";
                    if(Directory.Exists(solanaOutputPath) == false)
                    {
                        Directory.CreateDirectory(solanaOutputPath);
                    }

                    var solanaDirInfo = new DirectoryInfo($"{Application.dataPath}/Plugins/Blocto.Sdk/Solana");
                    var solanaDirPaths = solanaDirInfo.GetDirectories().Select(p => {
                                                                                   var tmp = p.FullName.Split("Assets/")[1];
                                                                                   return $"Assets/{tmp}";
                                                                               }).ToList(); 
                    directories.AddRange(solanaDirPaths);
                    AssetDatabase.ExportPackage(directories.ToArray(), $"release/{buildVersion}/Portto.Blocto.Solana.{buildVersion}.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.Default);
                    Debug.Log("Protto.Blocto.Solana export successed.");
                    break;
                case PackageTypeEnum.Evm:
                    var evmOutputPath = $"release/{buildVersion}";
                    if(Directory.Exists(evmOutputPath) == false)
                    {
                        Directory.CreateDirectory(evmOutputPath);
                    }

                    var evmDirInfo = new DirectoryInfo($"{Application.dataPath}/Plugins/Blocto.Sdk/Evm");
                    var evmDirPaths = evmDirInfo.GetDirectories().Select(p => {
                                                                             var tmp = p.FullName.Split("Assets/")[1];
                                                                             return $"Assets/{tmp}";
                                                                         }).ToList(); 
                    directories.AddRange(evmDirPaths);
                    AssetDatabase.ExportPackage(directories.ToArray(), $"release/{buildVersion}/Portto.Blocto.Evm.{buildVersion}.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.Default);
                    Debug.Log("Protto.Blocto.Solana export successed."); 
                    break;
            }
        }
 
        private static string GetExtension()
        {
            string extension = "";
 
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                    break;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    extension = ".exe";
                    break;
                case BuildTarget.Android:
                    extension = ".apk";
                    break;
            }
 
            return extension;
        }
    }
}