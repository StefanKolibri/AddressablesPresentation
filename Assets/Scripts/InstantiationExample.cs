using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InstantiationExample : MonoBehaviour
{
    [SerializeField] private string _assetAddress;
    [SerializeField] private AssetReference _assetReference;
    [SerializeField] private bool _useAssetAddress;

    private readonly List<AsyncOperationHandle> _handles = new List<AsyncOperationHandle>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_useAssetAddress)
            {
                InstantiateWithAddress();
            }
            else
            {
                InstantiateWithAssetReference();
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ClearInstantiatedGameObjects();
        }
    }

    private void InstantiateWithAddress()
    {
        Addressables.InstantiateAsync(_assetAddress, parent: null).Completed += handle =>
        {
            _handles.Add(handle);
            handle.Result.transform.position = new Vector3(0, 5, 0);
        };
    }

    private void InstantiateWithAssetReference()
    {
        _assetReference.InstantiateAsync(new Vector3(0, 5, 0), Quaternion.identity, parent: null).Completed += handle =>
        {
            _handles.Add(handle);
            handle.Result.transform.position = new Vector3(0, 5, 0);
        };
    }

    private void ClearInstantiatedGameObjects()
    {
        _handles.ForEach(handle => Addressables.ReleaseInstance(handle));
        _handles.Clear();
    }
}
