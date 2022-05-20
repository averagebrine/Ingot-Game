using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Drip/Hat")]
public class Hat : ScriptableObject
{
    public string hatName;

    public Texture2D hatTexture;
    public bool collectable;
}
