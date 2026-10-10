using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoves : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.05f;
    public LayerMask groundLayer;

    public Sprite idleSprite;
    public Sprite jumpSprite;
    public Sprite fallSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public bool isGrounded;

    private float moveInput;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ¸I¦a
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Åª¨ú
        moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput = 1f;

        // Â½Âà
        if (moveInput > 0.01f)
            sr.flipX = false;
        else if (moveInput < -0.01f)
            sr.flipX = true;

        // ¸õÅD
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            jumpPressed = true;

        UpdateSprite();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    void UpdateSprite()
    {
        if (isGrounded)
            sr.sprite = idleSprite;
        else if (rb.linearVelocity.y > 0.1f)
            sr.sprite = jumpSprite;
        else
            sr.sprite = fallSprite;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}