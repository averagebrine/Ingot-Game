using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    [SerializeField] private Material characterMaterial;

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

    public void SwitchSkin(int Character)
    {
        Debug.Log("Switching to " + characters[Character].name);
        characterMaterial.SetTexture("_SkinTex", characters[Character].texture);
    }
}
