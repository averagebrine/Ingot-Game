using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region character state
    // public Character skin;
    public SerializedDictionary<string, bool> collectedCharacters;
    public SerializedDictionary<string, bool> collectedDrip;
    #endregion

    #region progression
    public int recentLevel;
    #endregion

    #region collection
    public SerializedDictionary<string, bool> collectedIngots;
    #endregion

    #region scene management

    #endregion

    // set the default values here (these will be used when a new game is started)
    public GameData()
    {
        // character state
        // skin = new Character();

        // progression
        recentLevel = 0;

        // collection
        collectedIngots = new SerializedDictionary<string, bool>();

        // scene management
    }
}
