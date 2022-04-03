using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite coconut;
    void Awake()
    {
        if(coconut == null) Application.Quit();
    }
}
