using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGroupManager
{
    public event Action<string> OnSceneLoaded;
    public event Action<string> OnSceneUnloaded;
    public event Action OnSceneGroupLoaded;

    SceneGroup _activeSceneGroup;

    public async Task LoadScenes(SceneGroup sceneGroup, IProgress<float> progress, bool realoadDupScenes = false)
    {
        _activeSceneGroup = sceneGroup;
        var loadedScenes = new List<string>();

        await UnloadScenes();

        int sceneCount = SceneManager.sceneCount;

        for(var i = 0; i < sceneCount; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i).name);
        }

        var totalScenesToLoad = _activeSceneGroup.Scenes.Count;

        var operationGroup = new AsyncOperationGroup(totalScenesToLoad);

        for (var i = 0;i < totalScenesToLoad;i++)
        {
            var sceneData = sceneGroup.Scenes[i];
            if(realoadDupScenes == false && loadedScenes.Contains(sceneData.Reference.Name))
                continue;

            var operation = SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
            await Task.Delay(10000);
            operationGroup.Operations.Add(operation);
            OnSceneLoaded?.Invoke(sceneData.Name);
        }

        while (!operationGroup.IsDone)
        {
            progress.Report(operationGroup.Progress);
            await Task.Delay(100);
        }

        Scene activeScene = SceneManager.GetSceneByName(sceneGroup.FindSceneNameByType(SceneType.ActiveScene));

        if (activeScene.IsValid())
        {
            SceneManager.SetActiveScene(activeScene);
        }

        OnSceneGroupLoaded?.Invoke();
    }

    public async Task UnloadScenes()
    {
        var unloadSceneNames = new List<string>();
        var activeScene = SceneManager.GetActiveScene().name;

        int sceneCount = SceneManager.sceneCount;

        for(int i = sceneCount - 1; i > 0; i--)
        {
            var sceneAt = SceneManager.GetSceneAt(i);
            if (!sceneAt.isLoaded) continue;

            var sceneName = sceneAt.name;
            if (sceneName == "Bootstrapper" || sceneName.Equals(activeScene)) continue;
            unloadSceneNames.Add(sceneName);
        }

        var operationGroup = new AsyncOperationGroup(unloadSceneNames.Count);

        for(int i = 0; i < unloadSceneNames.Count; i++)
        {
            var operation = SceneManager.UnloadSceneAsync(unloadSceneNames[i]);
            if (operation == null) continue;
            operationGroup.Operations.Add(operation);

            OnSceneUnloaded(unloadSceneNames[i]);
        }

        while (!operationGroup.IsDone)
        {
            await Task.Delay(100);
        }
    }
}
