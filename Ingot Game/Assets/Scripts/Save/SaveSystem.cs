using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(int slot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile" + slot + ".sexyingotfile";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavedData data = new SavedData();

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Successfully saved at: " + path + "!");
    }

    public static SavedData LoadData(int slot)
    {
        string path = Application.persistentDataPath + "/savefile" + slot + ".sexyingotfile";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData data = formatter.Deserialize(stream) as SavedData;
            stream.Close();

            Debug.Log("Successfully loaded data from " + path + "!");
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
