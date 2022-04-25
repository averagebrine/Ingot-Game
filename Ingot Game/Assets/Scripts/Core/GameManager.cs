using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;

    public static GameManager instance;

    #region Save Data
    public SavedData[] storedData;
    public SavedData data;
    public string slotDisplay;
    public bool[] levelsUnlocked;
    public bool[] skinsUnlocked;
    #endregion

    void Awake()
    {
        // most important code in the game
        if(coconut == null) Application.Quit();

        if (storedData.Length < 3)
        {
            // What to do if these slots haven't been created yet?
            // How to check if they have been created?
            Load(1);
            storedData[1] = data;
            Load(2);
            storedData[2] = data;
            Load(3);
            storedData[3] = data;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // should probably use the input manager
        if (Input.GetKeyDown(KeyCode.Escape) && Input.GetKey(KeyCode.LeftControl))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    // The interface should work around the gamemanager's saving system, the gamemanager's system shouldn't need to change for it
    public void Save(int slot)
    {
        Debug.Log("Saving to slot " + slot);
        SaveSystem.SaveData(slot);
    }

    public void Load(int slot)
    {
        Debug.Log("Loading from slot" + slot);
        data = SaveSystem.LoadData(slot);

        data.isNew = false;
        data.slotDisplay = "Slot " + slot + ": at some level idk";
        levelsUnlocked = data.levelsUnlocked;
        skinsUnlocked = data.skinsUnlocked;
    }
}
