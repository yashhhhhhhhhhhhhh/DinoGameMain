using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public CanvasGroup gameOverCanvasGroup;
    public float fadeSpeed = 1f;
    private bool isFading = false;

    void Start()
    {
        gameOverCanvasGroup.alpha = 0f;
        gameOverCanvasGroup.gameObject.SetActive(false);
    }

    void Update()
    {
        // Press G to test game over fade-in
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShowGameOver();
        }
    }

    public void ShowGameOver()
    {
        gameOverCanvasGroup.gameObject.SetActive(true);
        if (!isFading)
        {
            StartCoroutine(FadeInGameOver());
        }
    }

    System.Collections.IEnumerator FadeInGameOver()
    {
        isFading = true;
        while (gameOverCanvasGroup.alpha < 1f)
        {
            gameOverCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        gameOverCanvasGroup.alpha = 1f;
        isFading = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
