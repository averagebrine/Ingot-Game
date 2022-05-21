using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Drip/Necklace")]
public class Necklace : ScriptableObject
{
    public string necklaceName;

    public Texture2D necklaceTexture;

    public bool collectable;
}