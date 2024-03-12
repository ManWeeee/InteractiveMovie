using Eflatun.SceneReference;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[Serializable]
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    Canvas _loadingCanvas;
    [SerializeField]
    Camera _loadingCamera;
    
    private void Start()
    {
        LoadNextScene();
    }

    public async void LoadNextScene()
    {
        EnableLoadingCanvas();
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1, LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            await Task.Delay(200);
        }
        currentScene = SceneManager.GetSceneByBuildIndex(currentScene.buildIndex + 1);
        if (currentScene.IsValid())
        {
            foreach (GameObject root in currentScene.GetRootGameObjects())
            {
                if (root.TryGetComponent<Camera>(out Camera mainCamera) || root.TryGetComponent<VideoPlayer>(out VideoPlayer player) || root.TryGetComponent<ClipLoader>(out ClipLoader loader))
                    continue;
                root.SetActive(false);
            }
            SceneManager.SetActiveScene(currentScene);
            Debug.Log(currentScene.name.ToString());
        }
        await Task.Delay(5000);
        EnableLoadingCanvas(false);
    }
    void EnableLoadingCanvas(bool enable = true)
    {
        _loadingCanvas.gameObject.SetActive(enable);
        _loadingCamera.gameObject.SetActive(enable);
    }
}
