using UnityEngine;
using UnityEngine.UI;

public class HomeInterfaceSaves : MonoBehaviour
{
    private GameManager gameManager;
    private int slot = 2;

    [SerializeField] private Button backButton;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backButton.onClick.Invoke();
        }
    }

    public void Slot1()
    {
        slot = 1;
    }

    public void Slot2()
    {
        slot = 2;
    }

    public void Slot3()
    {
        slot = 3;
    }

    // Either start in the vault or the level you were saved in
    public void Load()
    {
        Debug.Log("Loading from slot " + slot + ". . .");

        gameManager.LoadGame(slot);
    }

    // Needs a confirmation dialog
    public void Delete()
    {
        Debug.Log("Deleting slot " + slot + ". . .");

        SaveSystem.DeleteData(slot);
    }
}
