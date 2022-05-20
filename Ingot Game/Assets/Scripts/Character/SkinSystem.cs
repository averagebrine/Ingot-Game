using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject eyes;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SwitchSkin(0);       
        }
    
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchSkin(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchSkin(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchSkin(3);
        }
    }

    public void SwitchSkin(int i)
    {
        characterMaterial.SetTexture("_SkinTex", characters[i].baseTexture);

        if (characters[i].defaultHat != null)
        {
            hat.SetActive(true);
            hat.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[i].defaultHat.hatTexture);
        }
        else
        {
            hat.SetActive(false);
        }

        if (characters[i].defaultEyes != null)
        {
            eyes.SetActive(true);
            eyes.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[i].defaultEyes.eyesTexture);
        }
        else
        {
            eyes.SetActive(false);
        }
    }
}
