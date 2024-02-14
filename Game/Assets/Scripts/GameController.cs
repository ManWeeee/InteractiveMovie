using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void LoadScene(SceneLoadInfo scene)
    {
        SceneManager.LoadScene(scene.address);
    }
}
