using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.Escape))
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

        // Temporary, just load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Successfully loaded slot " + loadedSlot + "!");
    }
}
