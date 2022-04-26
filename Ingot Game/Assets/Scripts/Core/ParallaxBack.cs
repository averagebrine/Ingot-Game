using UnityEngine;

public class ParallaxBack : MonoBehaviour
{
    private Transform cam;

    void Awake()
    {
        cam = FindObjectOfType<Camera>().transform;
    }

    void Update()
    {
        Vector2 newPos = new Vector2(cam.transform.position.x, cam.transform.position.y);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
