using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    

    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusiVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
        
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        MyMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        MyMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadMusiVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }
}
