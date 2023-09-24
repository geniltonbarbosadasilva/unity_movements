using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 8.0f;
    private float jumpingPower = 16.0f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Flip();
        Jump();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private bool IsGrounded()
    {
        Debug.Log(groundCheck.position);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Walk()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
}
