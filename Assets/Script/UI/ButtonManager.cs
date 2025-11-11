using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    AudioManager audiomanager;
    private Button button;
    void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");

        if(audioObj != null)
        {
            if (audiomanager != null)
            {
                audiomanager.PlaySFX(audiomanager.ButtonClick);
            }
        }
    }
}
