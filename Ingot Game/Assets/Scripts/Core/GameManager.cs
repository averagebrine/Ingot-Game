using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;

    public static GameManager instance;

    #region Save data
    private int loadedSlot = -1;

    public bool isNew;
    public string slotDisplay;
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // most important code in the game
        if (coconut == null) Application.Quit();

        isNew = true;
        slotDisplay = "null";
    }

    public void LoadGame(int slot)
    {
        loadedSlot = slot;
        SavedData data = SaveSystem.LoadData(this, loadedSlot);

        isNew = data.isNew;
        slotDisplay = data.slotDisplay;

        if(data.isNew)
        {
            data.isNew = false;
            data.slotDisplay = "Slot " + loadedSlot;
            SaveGame();
        }
        else
        {
            SaveGame();
        }

        // Temporary, just load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Successfully loaded slot " + loadedSlot + "!");
    }

    public void SaveGame()
    {
        if(loadedSlot == -1) return;
    
        SaveSystem.SaveData(this, loadedSlot);
        Debug.Log("Successfully saved slot " + loadedSlot + "!");
    }
}
