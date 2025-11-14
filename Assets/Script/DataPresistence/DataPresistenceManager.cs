using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPresistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPresistence> dataPresistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPresistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Menemukan lebih dari satu DataPresistenceManager di scene!");
        }
        instance = this;
        
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPresistenceObjects = FindAllDataPresistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.LogWarning("Tidak ada data game yang ditemukan. Memulai game baru...");
            NewGame();
        }

        foreach (IDataPresistence dataPresistenceObj in dataPresistenceObjects)
        {
            dataPresistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach (IDataPresistence dataPresistenceObj in dataPresistenceObjects)
        {
            dataPresistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPresistence> FindAllDataPresistenceObjects()
    {
        MonoBehaviour[] objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        IEnumerable<IDataPresistence> dataPresistenceObjects = objects.OfType<IDataPresistence>();

        return new List<IDataPresistence>(dataPresistenceObjects);
    }
}
