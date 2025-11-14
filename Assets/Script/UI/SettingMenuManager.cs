using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingMenuManager : MonoBehaviour
{
    public TMP_Dropdown ResDropdown;
    public Toggle FullscreenTogle;
    Resolution[] AllResolution;
    bool IsFullScreen;
    int SelectedResolution;
    List<Resolution> SelectedResolutionList = new List<Resolution>();
    void Start()
    {
        IsFullScreen = true;
        AllResolution = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach (Resolution res in AllResolution)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResolutionList.Add(res);
            }

        }

        ResDropdown.AddOptions(resolutionStringList);
    }

    public void ChangeResolution()
    {
        SelectedResolution = ResDropdown.value;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
    }
    
    public void ChangeFullScreen()
    {
        IsFullScreen = FullscreenTogle.isOn;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
    }
}
