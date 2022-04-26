using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavedData
{
    public bool isNew;
    public string slotDisplay;

    public SavedData(GameManager manager)
    {
        isNew = manager.isNew;
        slotDisplay = manager.slotDisplay;
    }
}
