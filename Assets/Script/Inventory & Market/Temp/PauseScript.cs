using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
}
