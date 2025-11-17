using System.Collections.Generic;
using UnityEngine;

public class KeybindManager : MonoBehaviour, IDataPresistence
{
    public static KeybindManager Instance;
    public Dictionary<string, KeyCode> Keybinds = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);  
    }

    private void LoadDefaultKeysIfEmpty()
    {
        if (Keybinds.Count == 0)
        {
            Keybinds["Attack"] = KeyCode.Mouse0;
            Keybinds["Sprint"] = KeyCode.LeftShift;
            Keybinds["Interact"] = KeyCode.F;
            Keybinds["Skill1"] = KeyCode.Alpha1;
            Keybinds["Skill2"] = KeyCode.Alpha2;
            Keybinds["Skill3"] = KeyCode.Alpha3;  
        }
    }

    public void LoadData(GameData data)
    {
        Keybinds.Clear();
        foreach (var kvp in data.keybinds)
        {
            Keybinds[kvp.Key] = (KeyCode)kvp.Value;
        }
        LoadDefaultKeysIfEmpty();
    }

    public void SaveData(ref GameData data)
    {
        data.keybinds.Clear();
        foreach (var kvp in Keybinds)
        {
            data.keybinds[kvp.Key] = (int)kvp.Value;
        }
    }

    public bool SetKey(string action, KeyCode key)
    {
        if (IsKeyAlreadyUsed(key, action))
        {
            return false;
        }

        Keybinds[action] = key;
        return true;
    }

    public KeyCode GetKey(string action)
    {
        if (!Keybinds.ContainsKey(action))
            LoadDefaultKeysIfEmpty();

        return Keybinds[action];
    }
    public bool IsKeyAlreadyUsed(KeyCode key, string currentAction)
{
    foreach (var kvp in Keybinds)
    {
        // Kalau key digunakan oleh action lain → return true
        if (kvp.Key != currentAction && kvp.Value == key)
            return true;
    }

    return false;
}

}
