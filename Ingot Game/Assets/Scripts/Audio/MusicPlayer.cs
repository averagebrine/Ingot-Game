using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private bool playMusic = true;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(playMusic) StartCoroutine(Music());
    }

    IEnumerator Music()
    {
        FindObjectOfType<AudioManager>().PlayFromPool("Temp Music Pool");

        yield return new WaitForSeconds(Random.Range(16, 480));

        StartCoroutine(Music());
    }
}
