using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private float playerSpeed;
    Vector2 dir;
    

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal") * playerSpeed;
        dir.y = Input.GetAxisRaw("Vertical") * playerSpeed;
        UpdateAnimations();
    }
    void FixedUpdate()
    {
        Moving();
    }
    private void Moving()
    {
        if (rigidBody.bodyType != RigidbodyType2D.Static)
        {
            rigidBody.MovePosition(rigidBody.position + dir * playerSpeed * Time.fixedDeltaTime);
        }
    }
    private void UpdateAnimations()
    {
        if (dir.x != 0)
        {
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
        }
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        animator.SetFloat("Speed", dir.sqrMagnitude);
    }
}
