using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private CircleCollider2D circleCollider2D;
    [SerializeField]
    private LayerMask jumpableGround;
    [SerializeField]
    private ParticleSystem jumpEffect;
    [SerializeField]
    private float playerSpeed;
    
    [SerializeField]
    private float dustFormationPeriod;
    private float counter;
    Vector2 dir;
    private bool Jump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        UpdateAnimations();
        Jumping();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_DASH);
            }
        }

    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        Moving();
        //rigidBody.MovePosition(rigidBody.position + dir * playerSpeed * Time.fixedDeltaTime);

    }
    private void Moving()
    {
        if (rigidBody.bodyType != RigidbodyType2D.Static)
        {
            rigidBody.MovePosition(rigidBody.position + dir * playerSpeed * Time.fixedDeltaTime);
        }
    }
    private void Jumping()
    {
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            Jump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || Jump)
            {
                if (AudioManager.HasInstance)
                {
                    AudioManager.Instance.PlaySE(AUDIO.SE_JUMP);
                }
                Jump = !Jump;
                animator.SetBool("Jump", !Jump);
                if (!Jump)
                {
                    jumpEffect.Play();
                }
            }
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
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(circleCollider2D.bounds.center, circleCollider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(dir.x * dashingPower, 0f);
        rigidBody.velocity = new Vector2(dir.y * dashingPower, 0f);
        //trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //trailRenderer.emitting = false;
        rigidBody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
