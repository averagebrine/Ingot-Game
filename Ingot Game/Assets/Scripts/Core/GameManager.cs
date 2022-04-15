using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;

    void Awake()
    {
        // most important code in the game
        if(coconut == null) Application.Quit();
    }

    void Update()
    {
        // should probably use the input manager
        if (Input.GetKeyDown(KeyCode.Escape) && Input.GetKey(KeyCode.LeftControl))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
