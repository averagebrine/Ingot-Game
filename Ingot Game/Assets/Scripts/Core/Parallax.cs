using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera cam;
    private Transform character;

    private Vector2 startPos;
    float startZ;

    private Vector2 travel => (Vector2)cam.transform.position - startPos;
    private float parralaxFactor => Mathf.Abs(distanceFromCharacter) / clippingPlane;
    private float distanceFromCharacter => transform.position.z - character.position.z;
    private float clippingPlane => (cam.transform.position.z + (distanceFromCharacter > 0 ? cam.farClipPlane : cam.nearClipPlane));

    void Awake()
    {
        cam = FindObjectOfType<Camera>();
        character = FindObjectOfType<CharacterMovement>().transform;

        startPos = transform.position;
        startZ = transform.position.z;
    }

    void Update()
    {
        Vector2 newPos = startPos + travel * parralaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
