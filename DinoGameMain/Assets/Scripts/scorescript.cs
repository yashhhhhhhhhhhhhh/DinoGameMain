using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    public TMP_Text livesText;
    public TMP_Text scoreText;

    private bool gameOver = false;

    // Power-up flags
    private bool isInvincible = false;
    private bool isDoubleScore = false;

    private float scoreInterval = 1f;
    private float scoreTimer = 0f;

    public GameObject player; // Reference to the player object
    public Transform respawnPoint; // Respawn location when player loses a life

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (!gameOver)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= scoreInterval)
            {
                IncreaseScore();
                scoreTimer = 0f;
            }
        }
    }

    // Increase the score over time
    void IncreaseScore()
    {
        score += isDoubleScore ? 2 : 1;
        UpdateUI();
    }

    // Decrease life on collision, with game-over check
    public void DecreaseLife()
    {
        if (isInvincible) return; // Ignore collisions while invincible

        lives--;

        if (lives <= 0)
        {
            gameOver = true;
            RestartGame();
        }
        else
        {
            RespawnPlayer(); // Respawn player when they have lives remaining
        }

        UpdateUI();
    }

    // Add an extra life to the player
    public void AddLife()
    {
        lives++;
        UpdateUI();
    }

    // Activate invincibility for a duration
    public void ActivateInvincibility(float duration)
    {
        StartCoroutine(InvincibilityRoutine(duration));
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    // Activate double score for a duration
    public void ActivateDoubleScore(float duration)
    {
        StartCoroutine(DoubleScoreRoutine(duration));
    }

    IEnumerator DoubleScoreRoutine(float duration)
    {
        isDoubleScore = true;
        yield return new WaitForSeconds(duration);
        isDoubleScore = false;
    }

    // Update the UI to reflect the current lives and score
    void UpdateUI()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    // Restart the game by reloading the scene
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Respawn the player at the designated spawn point
    void RespawnPlayer()
    {
        player.transform.position = respawnPoint.position; // Move player to respawn point

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Reset player velocity to stop movement
        }


    }
}
