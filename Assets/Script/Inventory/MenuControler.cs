using UnityEditor;
using UnityEngine;

public class MenuControler : MonoBehaviour
{
    public GameObject MenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPanel.SetActive(!MenuPanel.activeSelf);
            if (MenuPanel.activeSelf)
            {
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                Time.timeScale = 1f; // Resume the game
            }
        }
    }
}
