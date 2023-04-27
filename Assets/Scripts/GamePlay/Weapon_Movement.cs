using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Movement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Camera cam;
    Vector2 mousePos;
    Vector2 dir;
    
    private enum MovementState { Down, Up, Side }
    private MovementState movementState;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        if (dir.x > 0f)
        {
            spriteRenderer.flipX = true;
            movementState = MovementState.Side;
        }
        else if (dir.x < 0f)
        {
            spriteRenderer.flipX = false;
            movementState = MovementState.Side;
        }
       
        animator.SetInteger("State", (int)movementState);
        
    }
}
