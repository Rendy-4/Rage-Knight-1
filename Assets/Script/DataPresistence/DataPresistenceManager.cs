using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DataPresistenceManager : MonoBehaviour
{
    [Header("Game Config")]
    [SerializeField] private bool initializeDataIfNull = true;
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption = false;
    private GameData gameData;
    private List<IDataPresistence> dataPresistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPresistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Menemukan lebih dari satu DataPresistenceManager di scene!. Menghancurkan yang terbaru.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPresistenceObjects = FindAllDataPresistenceObjects();
        LoadGame();
    }
    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null && initializeDataIfNull)
        {
            Debug.Log("Tidak ada data game yang ditemukan. Memulai permainan baru.");
            NewGame();
        }
        if (this.gameData == null)
        {
            Debug.Log("Tidak ada data game yang ditemukan. Memerlukan Game Baru.");
            return;
        }

        if (gameData.inventoryData == null)
            gameData.inventoryData = new InventorySaveData();

        if (gameData.inventoryData.savedSlots == null)
            gameData.inventoryData.savedSlots = new List<SavedSlotData>();

        foreach (IDataPresistence dataPresistenceObj in dataPresistenceObjects)
        {
            dataPresistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("Tidak ada data game yang ditemukan. Tidak dapat menyimpan permainan.");
            return;
        }
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

    public bool HasGameData()
    {
        return this.gameData != null;
    }
}
