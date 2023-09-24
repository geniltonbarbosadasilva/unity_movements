using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private bool isFacingRight = true;
    [Range(0.0f, 100.0f)] public float speed = 10.0f;
    [Range(0.0f, 100.0f)] public float jumpingPower = 15.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Jump
        if (IsGrounded() && Input.GetButtonDown("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    public void FixedUpdate()
    {
        // Walk
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Flip
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
