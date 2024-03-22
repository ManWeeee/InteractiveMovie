using UnityEngine;
using UnityEngine.SceneManagement;
public class Bootstrapper :MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        Debug.Log("Init Bootstrap method");
        SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
    }
}
