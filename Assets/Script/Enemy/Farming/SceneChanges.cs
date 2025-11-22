using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour
{
    public static SceneChanges instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cari spawn point di scene baru
        GameObject spawn = GameObject.FindGameObjectWithTag("FarmingSpawn");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (spawn != null && player != null)
        {
            player.transform.position = spawn.transform.position;
        }
    }
}
