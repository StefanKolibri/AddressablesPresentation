using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadingExample : MonoBehaviour
{
    [SerializeField] private string _assetAddress = "Folder1/Cube1";
    private void LoadByAddress()
    {
        GameObject loadedGameObject = null;

        Addressables.LoadAssetAsync<GameObject>(_assetAddress).Completed += handle =>
        {
            loadedGameObject = handle.Result;
            // Do something with loaded GameObject
        };
    }

    [SerializeField] private AssetReference _assetReference;
    private void LoadByAssetReference()
    {
        GameObject loadedGameObject = null;

        Addressables.LoadAssetAsync<GameObject>(_assetReference).Completed += handle =>
        {
            loadedGameObject = handle.Result;
            // Do something with loaded GameObject
        };
    }
}
