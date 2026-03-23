using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int lives = 3;

    // Audio — assign these in the Inspector (optional, game works without them)
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton — only one GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Get AudioSource if one is attached
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int points)
    {
        score += points;
        // Null-safe: UIManager may not exist yet (Phase 5 before Phase 6)
        if (UIManager.instance != null)
            UIManager.instance.UpdateScore(score);
    }

    public void PlayerHit()
    {
        lives--;
        if (UIManager.instance != null)
            UIManager.instance.UpdateLives(lives);
        if (lives <= 0) GameOver();
    }

    public void GameOver()
    {
        if (UIManager.instance != null)
            UIManager.instance.ShowGameOver();
        PlayGameOver();
        Time.timeScale = 0f; // Pause the game
    }

    public void Victory()
    {
        if (UIManager.instance != null)
            UIManager.instance.ShowVictory();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        // Clear static reference so it doesn't persist across scene reloads
        if (instance == this) instance = null;
    }

    // --- Audio helpers (safe to call even with no AudioSource) ---
    public void PlayShoot()
    {
        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);
    }

    public void PlayExplosion()
    {
        if (audioSource != null && explosionSound != null)
            audioSource.PlayOneShot(explosionSound);
    }

    public void PlayGameOver()
    {
        if (audioSource != null && gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);
    }
}
