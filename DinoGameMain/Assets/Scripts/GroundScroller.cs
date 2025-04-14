using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 startPosition;
    private float repeatWidth;
    private bool isFlipped = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        startPosition = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        // Move ground left at game speed (irrespective of flip)
        transform.position += Vector3.left * gameManager.gameSpeed * Time.deltaTime;

        // Reset position when it moves out of view
        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }

    public void FlipGround()
    {
        isFlipped = !isFlipped;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
}
