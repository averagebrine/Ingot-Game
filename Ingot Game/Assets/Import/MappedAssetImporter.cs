using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

public class MappedAssetImporter
{
    [MenuItem("Assets/Import/Mapped Asset")]
    public static void ImportMappedAsset()
    {
        // get the selected objects
        UnityEngine.Object[] selectedObjects = Selection.objects;

        // check that we have 2 objects
        if (selectedObjects.Length != 2)
        {
            Debug.Log("Please select 2 textures");
            return;
        }

        // check that the objects are textures
        if (!(selectedObjects[0] is Texture2D) || !(selectedObjects[1] is Texture2D))
        {
            Debug.Log("Please select 2 textures");
            return;
        }

        // check the textures are named with _mapped and _map
        if (!selectedObjects[0].name.Contains("_mapped") && !selectedObjects[1].name.Contains("_map") || !selectedObjects[0].name.Contains("_map") && !selectedObjects[1].name.Contains("_mapped"))
        {
            Debug.Log("Please make sure the textures are named properly");
            return;
        }

        // get the textures
        Texture2D texture = (Texture2D)selectedObjects[0];
        Texture2D map = (Texture2D)selectedObjects[1];

        // get the path to the textures
        string path = AssetDatabase.GetAssetPath(texture);

        // check we got the names correctly
        if(!texture.name.Contains("_mapped"))
        {
            texture = (Texture2D)selectedObjects[1];
            map = (Texture2D)selectedObjects[0];
        }

        #region CreateFinalAsset
        
        int textureWidth = texture.width;
        int textureHeight = texture.height;

        int mapWidth = map.width;
        int mapHeight = map.height;
    
        Texture2D finalTexture = new Texture2D(textureWidth, textureHeight);

        // loop through the texture
        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < textureHeight; y++)
            {
                // get the pixel from the texture
                Color texturePixel = texture.GetPixel(x, y, 0);

                // skip if the pixel is transparent
                if (texturePixel.a == 0)
                {
                    finalTexture.SetPixel(x, y, new Color(0, 0, 0, 0));
                    continue;
                }

                // loop through the map
                for (int mx = 0; mx < mapWidth; mx++)
                {
                    for (int my = 0; my < mapHeight; my++)
                    {
                        // get the pixel from the map
                        Color mapPixel = map.GetPixel(mx, my, 0);

                        // check if the colors are the same
                        if (mapPixel == texturePixel)
                        {
                            // set the pixel in the final texture
                            // unity stores colors in floats from 0 to 1, so we'll need to multiply by 0.003921569f...
                            Color theColor = new Color(mx * 0.003921569f, my * 0.003921569f, 0, 1);
                            finalTexture.SetPixel(x, y, theColor);
                        }
                    }
                }
            }
        }   

        #endregion

        // save the texture in the same path as the originals
        byte[] bytes = finalTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/" + texture.name + "_final.png", bytes);

        // reload the asset database
        AssetDatabase.Refresh();

        // select the new texture
        Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/" + texture.name + "_final.png", typeof(Texture2D));
    }
}