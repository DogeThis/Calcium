using System.IO;
using UnityEngine;

public class LoadFromFileExample : MonoBehaviour
{
  public void LoadAssetBundle()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "ubody_dnc0am_c403.bundle"));

        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Assets/Share/Addressables/Unit/Model/uBody/Dnc0AM/c403/Prefabs/uBody_Dnc0AM_c403.prefab");
        // var cool = Instantiate(prefab);
    }
}