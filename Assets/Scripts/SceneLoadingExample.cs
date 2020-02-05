using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoadingExample : MonoBehaviour
{
    [SerializeField] private AssetReference _sceneAssetReference;
    private AsyncOperationHandle<SceneInstance> _loadedSceneHandle;
    private bool _unloaded;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Addressables.LoadSceneAsync(_sceneAssetReference, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded) return;

        Debug.Log("Successfully loaded scene.");
        _loadedSceneHandle = obj;
    } 

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.X) || _unloaded) return;

        _unloaded = true;
        UnloadScene();
    }

    private void UnloadScene()
    {
        Addressables.UnloadSceneAsync(_loadedSceneHandle, true).Completed += op => {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Successfully unloaded scene.");
            }
        };
    }
}