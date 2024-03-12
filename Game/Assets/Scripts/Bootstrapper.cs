using UnityEngine;
using UnityEngine.SceneManagement;
public class Bootstrapper :MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        SceneManager.LoadSceneAsync("Bootstrapper", LoadSceneMode.Single);
    }
}
