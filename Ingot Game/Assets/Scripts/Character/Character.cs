using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;

    public Texture2D baseTexture;
    public Eyes defaultEyes;
    public Hat defaultHat;
    public Cape defaultCape;
    public Necklace defaultNecklace;
}
