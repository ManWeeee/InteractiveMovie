using Eflatun.SceneReference;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

[Serializable]
public partial class SceneLoader : MonoBehaviour
{
    [SerializeField]
    Image _loadingBar;
    [SerializeField]
    float _fillSpeed;
    [SerializeField]
    Canvas _loadingCanvas;
    [SerializeField]
    Camera _loadingCamera;
    [SerializeField]
    SceneGroup[] _sceneGroup;

    float targetProgress;
    int currentGroupIndex;
    bool isLoading;

    public readonly SceneGroupManager sceneGroupManager = new SceneGroupManager();

    private void Awake()
    {
        sceneGroupManager.OnSceneLoaded += sceneName => Debug.Log($"Loaded: {sceneName}");
        sceneGroupManager.OnSceneUnloaded += sceneName => Debug.Log($"Unloaded: {sceneName}");
        sceneGroupManager.OnSceneGroupLoaded += () => Debug.Log($"Scene group Loaded");
    }

    private async void Start()
    {
        currentGroupIndex = 0;
        await LoadSceneGroup(currentGroupIndex);
    }

    private void Update()
    {
        if (!isLoading) return;

        float currentFillAmount = _loadingBar.fillAmount;
        float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

        float dynamicFillSpeed = progressDifference * _fillSpeed;

        _loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
    }

    public async void LoadSceneGroup()
    {
        await LoadSceneGroup(currentGroupIndex);
    }

    public async Task LoadSceneGroup(int index)
    {
        _loadingBar.fillAmount = 0;
        targetProgress = 1f;
        if (index < 0 || index >= _sceneGroup.Length) {
            Debug.LogError($"Index out of scene group : {index}");
            return;
        }
        LoadingProgress progress = new LoadingProgress();
        progress.OnProgressed += target => targetProgress = Mathf.Max(target, targetProgress);
        EnableLoadingCanvas();
        await sceneGroupManager.LoadScenes(_sceneGroup[index], progress);
        currentGroupIndex++;
    }

    public void EnableLoadingCanvas(bool enable = true)
    {
        isLoading = enable;
        _loadingCamera.gameObject.SetActive(enable);
        _loadingCanvas.gameObject.SetActive(enable);
    }

    private void SetSceneVisibility(string sceneName, bool visibility = true)
    {
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);

        foreach (GameObject root in loadedScene.GetRootGameObjects())
        {
            if (root.TryGetComponent<Camera>(out Camera mainCamera) || root.TryGetComponent<VideoPlayer>(out VideoPlayer player))
                continue;
            root.SetActive(visibility);
        }
    }
}
