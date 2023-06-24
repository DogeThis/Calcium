#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditor(typeof(LoadFromFileExample))]
public class LoadFromFileExampleEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        LoadFromFileExample myTarget = (LoadFromFileExample)target;
        if (GUILayout.Button("Load prefab from AssetBundle"))
        {
            myTarget.LoadAssetBundle();
        }
        
        if (GUILayout.Button("Crawl Prefab and transfer Spring Bone Information"))
        {
            CrawlBundlePrefab.Crawl(myTarget.loadedPrefab, myTarget.editedMesh);
        }
        if (GUILayout.Button("Unload All (Do this before saving)"))
        {
            myTarget.UnloadAssetBundles();
        }

        if (GUILayout.Button("Build bundle (you may have to hit me twice"))
        {
            // Argument validation
            var assetBundleName = myTarget.nameOfBundleToBuild;
            if (assetBundleName == null)
            {
                Debug.Log("I need an bundle name to continue.");
                return;
            }
            
            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

            var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = myTarget.outputFileName + ".bundle";
            build.assetNames = assetPaths;

            builds.Add(build);
            Debug.Log("assetBundle to build:" + build.assetBundleName);
            
            BuildPipeline.BuildAssetBundles ("Assets/AssetBundles",builds.ToArray(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
    }
}
#endif