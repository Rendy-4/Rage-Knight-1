using Unity.VisualScripting;
using UnityEngine;

public class PlayerCurency : MonoBehaviour, IDataPresistence
{
    public static PlayerCurency Instance;
    public int Coins = 0;
    void Awake()
    {
        Instance = this;  
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        Debug.Log("Menambah " + amount + " Coins. Total Coins: " + Coins);
    }

    public bool SpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            Debug.Log("Mengurangi " + amount + " Coins. Total Coins: " + Coins);
            return true;
        }
        
            Debug.Log("Coins tidak cukup!");
            return false;       
    }


    public void LoadData(GameData data)
    {
       data.PlayerCoins = Coins;
    }

    public void SaveData(ref GameData data)
    {
        Coins = data.PlayerCoins;
    }
}
