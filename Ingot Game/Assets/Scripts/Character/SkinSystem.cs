using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject eyes;
    [SerializeField] private GameObject cape;
    [SerializeField] private GameObject capeFront;
    [SerializeField] private GameObject necklace;

    private int currentCharacter = 0;

    [HideInInspector] public bool ingotSeen;
    private bool isWide;

    void Awake()
    {
        SwitchSkin(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            currentCharacter --;
            if (currentCharacter < 0) currentCharacter = characters.Length - 1;

            SwitchSkin(currentCharacter);       
        }
    
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentCharacter ++;
            if (currentCharacter >= characters.Length) currentCharacter = 0;

            SwitchSkin(currentCharacter);
        }

        // O_O
        if (ingotSeen)
        {
            if (characters[currentCharacter].defaultEyes == null) return;

            eyes.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[currentCharacter].defaultEyes.wideTexture);
            ingotSeen = false;

            isWide = true;
        }
        else if (isWide)
        {
            eyes.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[currentCharacter].defaultEyes.eyesTexture);

            isWide = false;
        }
    }

    public void SwitchSkin(int i)
    {
        currentCharacter = i;

        characterMaterial.SetTexture("_SkinTex", characters[i].baseTexture);

        // this code is so dumb but I don't think I'm gonna add any more types of cosmetics so it should be fine lol
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

        if (characters[i].defaultCape != null)
        {
            cape.SetActive(true);
            cape.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[i].defaultCape.capeTexture);
        }
        else
        {
            cape.SetActive(false);
        }

        if (characters[i].defaultCape != null)
        {
            capeFront.SetActive(true);
            capeFront.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[i].defaultCape.capeTexture);
        }
        else
        {
            capeFront.SetActive(false);
        }

        if (characters[i].defaultNecklace != null)
        {
            necklace.SetActive(true);
            necklace.GetComponent<SpriteRenderer>().material.SetTexture("_SkinTex", characters[i].defaultNecklace.necklaceTexture);
        }
        else
        {
            necklace.SetActive(false);
        }
    }
}
