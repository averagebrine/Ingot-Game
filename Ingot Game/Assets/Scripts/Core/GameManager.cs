using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;

    public static GameManager instance;

    #region Save data
    public bool isNew;
    public string slotDisplay;
    #endregion

    void Awake()
    {
        // most important code in the game
        if (coconut == null) Application.Quit();

        isNew = true;
        slotDisplay = "null";

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

    public void LoadGame(int loadedSlot)
    {
        SavedData data = SaveSystem.LoadData(this, loadedSlot);

        isNew = data.isNew;
        slotDisplay = data.slotDisplay;

        if(data.isNew)
        {
            data.isNew = false;
            data.slotDisplay = "Slot " + loadedSlot;
            SaveSystem.SaveData(this, loadedSlot);;
        }
        else
        {
            SaveSystem.LoadData(this, loadedSlot);;
        }
    }
}
