using UnityEngine;
using UnityEngine.UI;

public class HomeInterfaceSaves : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private int slot = 2;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    public void Load()
    {
        gameManager.Load(slot);
        // Either start in the vault or the level you were saved in
    }

    public void Delete()
    {
        if (gameManager.storedData[slot].isNew == true) Debug.Log("Nothing to delete");
        else 
        {
            // Needs a confirmation dialog
            gameManager.storedData[slot] = new SavedData();
            Debug.Log("Deleted slot data");
        }
    }
}
