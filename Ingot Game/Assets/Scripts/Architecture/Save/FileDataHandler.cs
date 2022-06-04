using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFilename = "";
    private bool useEncrytion = false;
    private readonly string encryptionCodeword = "ingot";

    public FileDataHandler(string dataDirPath, string dataFilename, bool useEncrytion)
    {
        this.dataDirPath = dataDirPath;
        this.dataFilename = dataFilename;
        this.useEncrytion = useEncrytion;
    }

    public GameData Load()
    {
        // Path.Combine accounts for different path separators among different operating systems
        string fullPath = Path.Combine(dataDirPath, dataFilename);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // optionally decrypt the data
                if (useEncrytion)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the Json data back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("An error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    
    public void Save(GameData data)
    {
        // Path.Combine accounts for different path separators among different operating systems
        string fullPath = Path.Combine(dataDirPath, dataFilename);

        try
        {
            // create the directory if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into a Json
            string dataToStore = JsonUtility.ToJson(data, true);

            // optionally encrypt the data
            if (useEncrytion)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            // write the serialized data to a file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log("An error occured when trying to sava data to file: " + fullPath + "\n" + e);
        }
    }

    // public void Delete()
    // {
    //     // Path.Combine accounts for different path separators among different operating systems
    //     string fullPath = Path.Combine(dataDirPath, dataFilename);
    //     if(File.Exists(fullPath))
    //     {
    //         try
    //         {
    //             File.Delete(fullPath);
    //         }
    //         catch (Exception e)
    //         {
    //             Debug.Log("An error occured when trying to delete file: " + fullPath + "\n" + e);
    //         }
    //     }
    // }

    // simple XOR encryption implementation
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ encryptionCodeword[i % encryptionCodeword.Length]);
        }
        return modifiedData;
    }
}
