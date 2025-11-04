using UnityEngine;
using UnityEngine.UI;

public class TabControler : MonoBehaviour
{
    public Button[] tabButtons;
    public GameObject[] pages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void activeTab(int tabIndex)
    {
        for (int i =0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            ColorBlock colorBlock = tabButtons[i].colors;
            colorBlock.normalColor = Color.gray;
            tabButtons[i].colors = colorBlock;
        }

        pages[tabIndex].SetActive(true);
        ColorBlock activeColorBlock = tabButtons[tabIndex].colors;
        activeColorBlock.normalColor = Color.white;
        tabButtons[tabIndex].colors = activeColorBlock;
    }
}
