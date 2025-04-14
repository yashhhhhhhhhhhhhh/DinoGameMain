using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    private float flipTime = 6f;
    private bool isFlipped = false;
    private ScreenShake screenShake;

    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        Invoke("StartFlipSequence", flipTime - 3f); // Shake starts 3 seconds before flip
    }

    void Update()
    {
        gameSpeed += speedIncreaseRate * Time.deltaTime;
    }

    void StartFlipSequence()
    {
        screenShake.TriggerShake(2f, 0.3f); // Shake for 2 seconds
        Invoke("FlipGame", 2.5f); // Flip after shake
    }

    [System.Obsolete]
    void FlipGame()
    {
        isFlipped = !isFlipped;

        // Flip gravity
        Physics2D.gravity = new Vector2(0, -Physics2D.gravity.y);

        // Rotate camera
        Camera.main.transform.Rotate(0, 0, 180);

        // Flip all objects tagged as "Flippable"
        GameObject[] flippableObjects = GameObject.FindGameObjectsWithTag("Flippable");
        foreach (GameObject obj in flippableObjects)
        {
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, -obj.transform.localScale.y, obj.transform.localScale.z);
        }

        // Flip ground movement
        GroundScroller[] groundScrollers = FindObjectsOfType<GroundScroller>();
        foreach (GroundScroller ground in groundScrollers)
        {
            ground.FlipGround();
        }

        // Flip Player Jump Logic
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.FlipPlayer();
        }

        // Re-trigger the next flip after another 60 seconds
        Invoke("StartFlipSequence", flipTime);
    }
    private bool isGameOver = false;

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Stops the entire game
    }

}
