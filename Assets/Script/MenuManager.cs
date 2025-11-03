using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public TMP_Text panelName;
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("User Telah Keluar");
    }

    public void setPanelName(string name)
    {
        panelName.text = name;
    }
}
