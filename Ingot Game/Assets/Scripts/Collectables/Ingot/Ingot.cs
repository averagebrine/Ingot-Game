using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : MonoBehaviour
{
    public string type;

    [SerializeField] private bool stationary;
    [SerializeField] private bool collidable;
    [SerializeField] private bool animates;

    private Renderer sprite;
    private SkinSystem character;

 
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
        Destroy(gameObject);
    }
}
