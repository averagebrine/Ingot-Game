using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavedData
{
    public bool isNew = true;
    public string slotDisplay;
    public bool[] levelsUnlocked;
    public bool[] skinsUnlocked;

    public SavedData()
    {
        levelsUnlocked = new bool[2];
        skinsUnlocked = new bool[2];
        slotDisplay = "empty slot";
    }
}
