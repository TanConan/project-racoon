using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Namen der zu ladenden Szenen")]
    public string sceneName1;
    public string sceneName2;

    void Start()
    {
        LoadExtraScenes();
    }

    void LoadExtraScenes()
    {
        // Prüfen, ob Szene 1 bereits geladen ist, um Duplikate zu vermeiden
        if (!SceneManager.GetSceneByName(sceneName1).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName1, LoadSceneMode.Additive);
        }

        // Prüfen für Szene 2
        if (!SceneManager.GetSceneByName(sceneName2).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName2, LoadSceneMode.Additive);
        }
    }
}
