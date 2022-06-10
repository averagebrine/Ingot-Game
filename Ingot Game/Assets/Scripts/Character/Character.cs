using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string id;
    public string characterName;

    public Texture2D baseTexture;
    public Eyes defaultEyes;
    public Hat defaultHat;
    public Cape defaultCape;
    public Necklace defaultNecklace;
    
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
