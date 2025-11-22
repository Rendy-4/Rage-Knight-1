using Unity.VisualScripting;
using UnityEngine;

public class PlayerExperience : MonoBehaviour, IDataPresistence

{

    public int level = 1;
    public float currentExp = 0;
    public float expToNextLevel = 100; // nilai awal
    public float growthMultiplier = 1.5f; // scaling EXP setiap level
    public static PlayerExperience Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void AddExp(float amount)
    {
        if(expToNextLevel <= 0)
        {
            expToNextLevel = 100;
        }

        currentExp += amount;

        int safety = 0;
        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();

            safety++;
            if (safety > 50)
            {
                Debug.LogError("EXP LOOP BREAK (safety triggered)");
                break;
            }
        }
    }

    void LevelUp()
    {
        level++;
        expToNextLevel *= growthMultiplier;   // contoh scaling 1.5x
        Debug.Log("LEVEL UP! Level sekarang: " + level);
    }
    public void LoadData(GameData data)
    {
        level = data.playerLevel;
        currentExp = data.playerExp;
        expToNextLevel = data.ExpToNextLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.playerLevel = level;
        data.playerExp = currentExp;
        data.ExpToNextLevel = expToNextLevel;
    }
}
