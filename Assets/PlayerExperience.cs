using UnityEngine;

public class PlayerExperience : MonoBehaviour, IDataPresistence
{
    public static PlayerExperience Instance;
    [Header("Player Exp Data")]
    public float currentExp = 0;
    public int level = 1;
    public float expToNextLevel = 100;
    public float growthMultiplier = 1.5f;

    void Awake()
    {
        if(Instance == null)
        Instance = this;
        else
        Destroy(gameObject);
    }

    public void AddExp(float amount)
    {
        currentExp += amount;
        while(currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
       level++;
       expToNextLevel *= growthMultiplier;
       Debug.Log("Level UP! Level Sekarang: " + level);
    }


    public void LoadData(GameData data)
    {
        currentExp = data.playerExp;
        level = data.playerLevel;
        expToNextLevel = data.expToNextLevel;

    }

    public void SaveData(ref GameData data)
    {
        data.playerExp = currentExp;
        data.playerLevel = level;
        data.expToNextLevel = expToNextLevel;
    }
}
