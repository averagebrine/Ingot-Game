using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;
    void Awake()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        if(coconut == null) Application.Quit();
    }
    void Update()
    {
        // copilot wrote all of this for me
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
