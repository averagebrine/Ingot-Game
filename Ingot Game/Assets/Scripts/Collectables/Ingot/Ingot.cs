using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;

    public string type;

    [SerializeField] private bool stationary;
    [SerializeField] private bool collidable;
    [SerializeField] private bool animates;

    private Renderer sprite;
    private SkinSystem character;

    [HideInInspector] public bool collected;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
 
    private void Awake()
    {
        sprite = GetComponent<Renderer>();
        character = FindObjectOfType<SkinSystem>();

        if (!stationary && !collidable) collidable = true;

        if (stationary)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (!collidable)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Ingot");
        }
        else 
        {
            gameObject.layer = LayerMask.NameToLayer("Collidable Ingot");
        }
              
        if (!animates)
        {
            GetComponent<Animator>().enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (sprite.isVisible)
        {
            character.ingotSeen = true;
        }
    }

    public void Collect()
    {
        collected = true;
        Destroy(gameObject);
    }

    public void LoadData(GameData data)
    {
        data.collectedIngots.TryGetValue(id, out collected);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.collectedIngots.ContainsKey(id))
        {
            data.collectedIngots.Remove(id);
        }
        data.collectedIngots.Add(id, collected);
    }
}
