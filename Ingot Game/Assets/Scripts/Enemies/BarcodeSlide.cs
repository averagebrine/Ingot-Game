using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcodeSlide : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Texture2D[] lookupTextures;
    private int current;

    void Awake()
    {
        current = 0;
        material.SetTexture("_SkinTex", lookupTextures[current]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            StartCoroutine(SlideLeft(8, 0.05f));
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            StartCoroutine(SlideRight(4, 0.05f));
        }
    }

    IEnumerator SlideLeft(int steps, float delay)
    {
        while (steps > 0)
        {
            steps--;
            current--;

            if (current < 0) current = lookupTextures.Length - 1;

            material.SetTexture("_SkinTex", lookupTextures[current]);

            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SlideRight(int steps, float delay)
    {
        while (steps > 0)
        {
            steps--;
            current++;

            if (current >= lookupTextures.Length) current = 0;

            material.SetTexture("_SkinTex", lookupTextures[current]);

            yield return new WaitForSeconds(delay);
        }
    }
}
