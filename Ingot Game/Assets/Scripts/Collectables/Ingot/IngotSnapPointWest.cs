using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngotSnapPointWest : MonoBehaviour
{
    [SerializeField] private Ingot parent;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Ingot") return;

        parent.TriggeredWest(col.gameObject.GetComponent<Ingot>());
    }
}
