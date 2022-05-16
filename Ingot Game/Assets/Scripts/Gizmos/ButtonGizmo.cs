using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGizmo : MonoBehaviour
{
    [SerializeField] private LayerMask whatCanActivate;
    [SerializeField] private Transform raycastPosition;
    [SerializeField] private float radius = 0.01f;
    private bool active = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DetectActivation();
    }

    private void DetectActivation()
    {
        bool inputDetected = Physics2D.OverlapBox(raycastPosition.position, new Vector2(0.875f, radius), 0, whatCanActivate);

        if (!inputDetected && active) Activate();
        else if (inputDetected && !active) Activate();
    }

    public void Activate()
    {
        active = !active;
        animator.SetBool("Active", active);
    }
}