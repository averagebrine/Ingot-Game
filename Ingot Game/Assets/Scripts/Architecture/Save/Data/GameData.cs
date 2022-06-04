using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region progression
    public int recentLevel;
    #endregion

    #region collection
    public SerializedDictionary<string, bool> collectedIngots;
    #endregion

    #region character state
    public bool isNew;
    #endregion

    // set the default values here (these will be used when a new game is started)
    public GameData()
    {
        // progression
        recentLevel = 0;

        // collection
        collectedIngots = new SerializedDictionary<string, bool>();

        // character state
        isNew = true;
    }
}
