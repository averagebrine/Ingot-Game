using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private bool playMusic = true;

    public static MusicPlayer instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(playMusic) StartCoroutine(Music());
    }

    IEnumerator Music()
    {
        FindObjectOfType<AudioManager>().PlayFromPool("Temp Music Pool");

        yield return new WaitForSeconds(Random.Range(16, 480));

        StartCoroutine(Music());
    }
}
