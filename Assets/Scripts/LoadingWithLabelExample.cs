using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingWithLabelExample : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private string _address;

    public void SwitchToHighDef()
    {
        LoadTexture(_address, "HQ");
    }

    public void SwitchToLowDef()
    {
        LoadTexture(_address, "SQ");
    }

    private void LoadTexture(string key, string label)
    {
        Addressables.LoadAssetsAsync<Texture2D>(new List<object> { key, label }, null, Addressables.MergeMode.Intersection).Completed
            += TextureLoaded;
    }

    private void TextureLoaded(AsyncOperationHandle<IList<Texture2D>> obj)
    {
        _material.mainTexture = obj.Result[0];
    }
}