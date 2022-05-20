using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Drip/Eyes")]
public class Eyes : ScriptableObject
{
    public string eyesName;

    public Texture2D eyesTexture;
    public Texture2D wideTexture;
    public bool collectable;
}
