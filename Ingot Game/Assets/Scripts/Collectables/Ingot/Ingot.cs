using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : MonoBehaviour
{
    public bool canSnap = true;

    [SerializeField] private RelativeJoint2D westSnap;
    [SerializeField] private RelativeJoint2D southSnap;
    [HideInInspector] public bool westSnapOccupied;
    [HideInInspector] public bool southSnapOccupied;

    [HideInInspector] public bool isStackParent;
    [HideInInspector] public bool isStacked;
    [HideInInspector] public GameObject stackParent;
    [HideInInspector] public List<GameObject> stackedIngots;


    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (southSnapOccupied && westSnapOccupied) canSnap = false;
    //     if (col.gameObject.tag != "Ingot") return;
    //     if (!canSnap || !col.gameObject.GetComponent<Ingot>().canSnap) return;
    
    //     GameObject ingot = col.gameObject;
    // }

    public void TriggeredWest(Ingot ingot)
    {
        if (westSnapOccupied && southSnapOccupied) canSnap = false;
        if (!canSnap || !ingot.canSnap) return;

        SnapFromWest(ingot);
    }

    public void TriggeredSouth(Ingot ingot)
    {
        if (westSnapOccupied && southSnapOccupied) canSnap = false;
        if (!canSnap || !ingot.canSnap) return;

        SnapFromSouth(ingot);
    }

    public void SnapFromWest(Ingot ingot)
    {
        // if there are already stacked ingots on this one
        if (ingot.isStacked || ingot.isStackParent)
        {
            stackParent = ingot.stackParent;

            stackParent.GetComponent<Ingot>().stackedIngots.Add(gameObject);


            westSnap.enabled = true;
            westSnap.connectedBody = ingot.gameObject.GetComponent<Rigidbody2D>();
            
            isStacked = true;
        }
        else // if there aren't, then start a new stack
        {
            stackParent = gameObject;
            ingot.stackParent = gameObject;

            stackedIngots.Add(gameObject);
            stackedIngots.Add(ingot.gameObject);

            westSnap.enabled = true;
            westSnap.connectedBody = ingot.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    public void SnapFromSouth(Ingot ingot)
    {
        // if there are already stacked ingots on this one
        if (ingot.isStacked || ingot.isStackParent)
        {
            stackParent = ingot.stackParent;

            stackParent.GetComponent<Ingot>().stackedIngots.Add(gameObject);

            southSnap.enabled = true;
            southSnap.connectedBody = ingot.gameObject.GetComponent<Rigidbody2D>();

            isStacked = true;
        }
        else // if there aren't, then start a new stack
        {
            stackParent = gameObject;
            ingot.stackParent = gameObject;

            stackedIngots.Add(gameObject);
            stackedIngots.Add(ingot.gameObject);

            southSnap.enabled = true;
            southSnap.connectedBody = ingot.gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
