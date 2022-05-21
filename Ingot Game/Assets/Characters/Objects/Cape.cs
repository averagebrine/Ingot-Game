using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Drip/Cape")]
public class Cape : ScriptableObject
{
    public string capeName;

    public Texture2D capeTexture;

    public bool collectable;
}