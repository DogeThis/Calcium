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
        if (GUILayout.Button("Load AssetBundle"))
        {
            myTarget.LoadAssetBundle();
        }

        if (GUILayout.Button("Debug"))
        {
            Debug.Log("hey there");
        }

        if (GUILayout.Button("Crawl Prefab"))
        {
            CrawlBundlePrefab.Crawl(myTarget.loadedPrefab, myTarget.editedMesh);
        }
    }
}
#endif