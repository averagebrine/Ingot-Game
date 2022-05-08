// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using System;
// using System.IO;
// using System.Runtime.Serialization.Formatters.Binary;

// public class SaveEngine : MonoBehaviour
// {
//     private string savePath => Application.persistentDataPath + "/savefile.sexyingotfile";

//     [ContextMenu("Save")]
//     public static void Save()
//     {
//         var state = LoadFile();
//         CaptureState(state);
//         SaveFile(state);
//     }

//     [ContextMenu("Load")]
//     public static void Load()
//     {
//         var state = LoadFile();
//         RestoreState(state);
//     }

//     private void SaveFile(object state)
//     {
//         using (var stream = File.Open(savePath, FileMode.Create))
//         {
//             var formatter = new BinaryFormatter();
//             formatter.Serialize(stream, state);
//         }
//     }

//     private Dictionary<string, object> LoadFile()
//     {
//         if(!File.Exists(savePath))
//         {
//             return new Dictionary<string, object>();
//         }

//         using (FileStream stream = File.Open(savePath, FileMode.Open))
//         {
//             var formatter = new BinaryFormatter();
//             return (Dictionary<string, object>)formatter.Deserialize(stream);
//         }
//     }

//     private void CaptureState(Dictionary<string, object> state)
//     {
//         foreach(var saveable in FindObjectsOfType<Saveable>())
//         {
//             state[saveable.ID] = saveable.CaptureState();
//         }
//     }

//     private void RestoreState(Dictionary<string, object> state)
//     {
//         foreach (var saveable in FindObjectsOfType<Saveable>())
//         {
//             if (state.TryGetValue(saveable.ID, out object value))
//             {
//                 saveable.RestoreState(value);
//             }
//         }
//     }
// }