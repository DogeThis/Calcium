using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class LoadFromFileExample : MonoBehaviour
{
    // label as "Name of bundle" in the editor
    
    [Tooltip("The name of the bundle you want to load for spring bone information. No .bundle extension, we'll add it for you.")]
    [FormerlySerializedAs("pathToAssetBundle")] public string nameOfBundleToLoad = "ubody_dnc0am_c403";
    
    public GameObject loadedPrefab;

    public GameObject editedMesh;

    public  GameObject createdObject;

    [Tooltip("The name of the bundle you want to build. (this is what you assigned yourself in Unity's editor) (no .bundle extension, we'll add it for you")]
    public string nameOfBundleToBuild;

    [Tooltip("Name of the output bundle (no .bundle extension, we'll add it for you")]
    public string outputFileName = "output_name";
    public void LoadAssetBundle()
    {
        // remove ".bundle" from the name if it happens to be there
        var nameToUse = nameOfBundleToLoad.Replace(".bundle", "") + ".bundle";
        if (nameOfBundleToLoad == null) 
        {
            Debug.Log("no path to asset bundle");
            return;
        }
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, nameToUse));

        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        
        var names = myLoadedAssetBundle.GetAllAssetNames();
        
        
        loadedPrefab = myLoadedAssetBundle.LoadAsset<GameObject>(names[0]);
        createdObject = Instantiate(loadedPrefab);
    }

    public void UnloadAssetBundles()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        if (createdObject != null)
        {
            DestroyImmediate(createdObject);
        }
    }
}