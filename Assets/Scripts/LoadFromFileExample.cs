using System.IO;
using UnityEngine;

public class LoadFromFileExample : MonoBehaviour
{
    public string pathToAssetBundle = "ubody_dnc0am_c403.bundle";
    public GameObject loadedPrefab;

    public GameObject editedMesh;
    public void LoadAssetBundle()
    {
        if (pathToAssetBundle == null) 
        {
            Debug.Log("no path to asset bundle");
            return;
        }
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, pathToAssetBundle));

        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        loadedPrefab = myLoadedAssetBundle.LoadAsset<GameObject>("Assets/Share/Addressables/Unit/Model/uBody/Dnc0AM/c403/Prefabs/uBody_Dnc0AM_c403.prefab");
        var cool = Instantiate(loadedPrefab);
    }
}