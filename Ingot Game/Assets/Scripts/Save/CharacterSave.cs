// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using System;

// public class CharacterSave : MonoBehaviour, ISaveable
// {

//     [SerializeField] private string displayName = "brine";
//     [SerializeField] private float health = 100f;
//     [SerializeField] private int level = 1;
//     [SerializeField] private int skin = 1;

//     public object CaptureState()
//     {
//         return new SaveData
//         {
//             displayName = displayName,
//             health = health,
//             level = level,
//             skin = skin
//         };
//     }

//     public void RestoreState(object state)
//     {
//         var saveData = (SaveData)state;

//         displayName = saveData.displayName;
//         health = saveData.health;
//         level = saveData.level;
//         skin = saveData.skin;
//     }

//     [Serializable]
//     private struct SaveData
//     {
//         public string displayName;
//         public float health;
//         public int level;
//         public int skin;
//     }
// }
