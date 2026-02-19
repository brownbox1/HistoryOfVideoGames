using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private new Camera camera;
    private Vector2 moveInput;

    private bool isGrounded;
    public bool isJumpPressed;
    public LayerMask whatIsGround;

    public float moveSpeed = 8f;
    public float acceleration = 50f;
    public float groundFriction = 40f;
    public float skidFriction = 100f;

    public float sprintSpeedMultiplier = 1.5f;
    public float sprintAccelMultiplier = 1.5f;
    private bool isSprinting;

    public float jumpHeight = 3.5f;
    public float timeToPeak = 0.4f;
    public float fallFasterMultiplier = 1.8f;
    public float groundCheckDistance = 0.7f;

    public float ceilingCheckDistance = 0.6f;
    private bool isTouchingCeiling;

    private float gravity;
    private float initialJumpVelocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        camera = Camera.main;
        
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToPeak, 2);
        initialJumpVelocity = Mathf.Abs(gravity) * timeToPeak;
    }

    void Update()
    {
        // 1. Get the Keyboard
        var keyboard = Keyboard.current;
        if (keyboard == null) return; // Safety check
    
        // sprint button
        isSprinting = keyboard.leftShiftKey.isPressed || keyboard.rightShiftKey.isPressed;

        // 2. Horizontal Movement
        float xInput = 0;
        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed) xInput = -1;
        if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed) xInput = 1;
        
        moveInput = new Vector2(xInput, 0);

        // 3. Jump Logic (Tap vs Hold)
        // wasPressedThisFrame is only true for 1 frame (perfect for the initial jump)
        if ((keyboard.spaceKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame) && isGrounded)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, initialJumpVelocity);
        }

        // isPressed is true as long as you hold it (perfect for our gravity logic)
        isJumpPressed = keyboard.spaceKey.isPressed || keyboard.upArrowKey.isPressed;
    }

    void FixedUpdate()
    {
        CheckGround();
        CheckCeiling();
        ApplyMovement();
        ApplyBetterJumpWeights();
        FlipSprite();
    }

    void ApplyMovement()
    {
        float currentMaxSpeed;
        float currentAccel;

        if (isSprinting)
        {
            currentMaxSpeed = moveSpeed * sprintSpeedMultiplier;
            currentAccel = acceleration * sprintAccelMultiplier;
        }
        else
        {
            currentMaxSpeed = moveSpeed;
            currentAccel = acceleration;
        }

        float targetSpeed = moveInput.x * currentMaxSpeed;
        float speedDif = targetSpeed - rigidbody.linearVelocity.x;

        float accelRate;

        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            bool isSkidding = (moveInput.x > 0 && rigidbody.linearVelocity.x < -0.1f) || (moveInput.x < 0 && rigidbody.linearVelocity.x > 0.1f);

            if (isSkidding)
            {
                accelRate = skidFriction;
            }
            else
            {
                accelRate = currentAccel;
            }
        }
        else
        {
            accelRate = groundFriction;
        }

        rigidbody.AddForce(speedDif * accelRate * Vector2.right);
    }

    void FlipSprite()
    {
        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void ApplyBetterJumpWeights()
    {
        float gravityRatio = Mathf.Abs(gravity) / 9.81f;

        if (rigidbody.linearVelocity.y < 0)
        {
            rigidbody.gravityScale = gravityRatio * fallFasterMultiplier;
        }
        else if (rigidbody.linearVelocity.y > 0 && !isJumpPressed)
        {
            rigidbody.gravityScale = gravityRatio * 5f;
        }
        else
        {
            rigidbody.gravityScale = gravityRatio;
        }
    }

    void CheckCeiling()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, ceilingCheckDistance, whatIsGround);

        if (hit.collider != null)
        {
            if (rigidbody.linearVelocity.y > 0)
            {
                // stop upward momentum immediately
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0);
                MysteryBlock block = hit.collider.GetComponent<MysteryBlock>();
                if (block != null)
                {
                    block.RequestHit();
                }
            }
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // check if mario is falling and above goomba
            if (rigidbody.linearVelocity.y < 0 && transform.position.y > collision.transform.position.y + 0.2f)
            {
                EntityMovement enemy = collision.gameObject.GetComponent<EntityMovement>();

                if (enemy != null)
                {
                    enemy.Squash();

                    // bounce mario up
                    rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, initialJumpVelocity * 0.6f);
                }
            }
            else
            {
                Debug.Log("mario death thing");
            }
        }
    }
}
