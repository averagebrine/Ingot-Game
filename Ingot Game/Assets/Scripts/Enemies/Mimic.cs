using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    [SerializeField] private BoxCollider2D range;
    [SerializeField] private bool hidden = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hidden = true;
            range.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && hidden)
        {
            SurpriseMF();
        }
    }

    private void SurpriseMF()
    {
        hidden = false;
        range.enabled = false;
    }
}
