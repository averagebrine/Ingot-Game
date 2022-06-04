using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header ("File Storage Config")]
    [SerializeField] private string filename;
    [SerializeField] private bool useEncrytion;

    [HideInInspector] public FileDataHandler dataHandler;
    private List<IDataPersistance> dataPersistanceObjects;
    private GameData gameData;

    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, filename, useEncrytion);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // if no data is found, initialize a new game
        if(this.gameData == null)
        {
            Debug.Log("No saved data found. Initializing new game.");
            NewGame();
        }

        // push saved data to all scripts that need it
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // pass current data to all scripts so they can update it
        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        // save data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);

    }
}
