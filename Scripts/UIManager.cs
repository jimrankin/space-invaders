using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text scoreText, livesText;
    public GameObject gameOverPanel, startPanel;

    void Awake() { instance = this; }

    void Start()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Update()
    {
        if (startPanel != null && startPanel.activeSelf && Input.anyKeyDown)
        {
            startPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void UpdateScore(int s)
    {
        if (scoreText != null)
            scoreText.text = "SCORE: " + s;
    }

    public void UpdateLives(int l)
    {
        if (livesText != null)
            livesText.text = "LIVES: " + l;
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
