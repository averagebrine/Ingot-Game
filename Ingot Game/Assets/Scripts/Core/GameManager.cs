using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;

    public static GameManager instance;

    #region Save data
    // private int loadedSlot = -1;

    public bool isNew;
    public string slotDisplay;
    #endregion

    void Awake()
    {
        instance = this;

        // most important code in the game
        if (coconut == null) Application.Quit();

        isNew = true;
        slotDisplay = "null";
    }

    private void Start()
    {
        DataPersistanceManager.instance.LoadGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnApplicationQuit()
    {
        DataPersistanceManager.instance.SaveGame();
    }
}
