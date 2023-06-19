using System.IO;
using UnityEngine;

public class LoadFromFileExample : MonoBehaviour
{
    public string pathToAssetBundle = "ubody_dnc0am_c403";
    public GameObject loadedPrefab;

    public GameObject editedMesh;

    public  GameObject createdObject;
    public void LoadAssetBundle()
    {
        // remove ".bundle" from the name if it happens to be there
        var nameToUse = pathToAssetBundle.Replace(".bundle", "") + ".bundle";
        if (pathToAssetBundle == null) 
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