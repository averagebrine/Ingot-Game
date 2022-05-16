using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : MonoBehaviour
{
    public string type;

    [SerializeField] private bool stationary;
    [SerializeField] private bool collidable;
    [SerializeField] private bool animates;

 
    private void Awake()
    {
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

    public void Collect()
    {
        Destroy(gameObject);
    }
}
