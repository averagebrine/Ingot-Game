using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainConnect : MonoBehaviour
{
    [SerializeField] private HingeJoint2D joint;

    public void Connect(GameObject character)
    {
        joint.enabled = true;
        joint.connectedBody = character.GetComponent<Rigidbody2D>();

        character.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);

        character.GetComponent<CharacterMovement>().Swing();
    }

    public void Disconnect(GameObject character)
    {
        joint.connectedBody = null;
        joint.enabled = false;

        character.GetComponent<CharacterMovement>().Unswing();
    }
}
