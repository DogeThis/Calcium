#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

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
    }
}
#endif