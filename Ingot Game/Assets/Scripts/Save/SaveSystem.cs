using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(GameManager manager, int slot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile" + slot + ".sexyingotfile";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavedData data = new SavedData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SavedData LoadData(GameManager manager, int slot)
    {
        string path = Application.persistentDataPath + "/savefile" + slot + ".sexyingotfile";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData data = formatter.Deserialize(stream) as SavedData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path + ", creating an empty one!");
            return new SavedData(manager);
        }
    }
}
