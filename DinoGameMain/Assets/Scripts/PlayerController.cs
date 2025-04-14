using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    public float jumpForce = 10f;
    private bool isFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Jump in the correct direction based on gravity
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            float jumpDirection = isFlipped ? -1f : 1f;  // Adjust jump direction
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * jumpDirection);
            animator.SetTrigger("Jump");
        }
    }

    private bool IsGrounded()
    {
        return rb.linearVelocity.y == 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }


    public void FlipPlayer()
    {
        isFlipped = !isFlipped;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        rb.gravityScale = isFlipped ? -1f : 1f;
    }
}
