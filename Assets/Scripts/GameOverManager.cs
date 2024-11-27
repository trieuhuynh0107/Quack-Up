using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Game.Score; // Thêm namespace của ScoreManager

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;

    private bool isGameOver = false;

    public void ShowGameOverMenu(int currentScore)
    {
        if (isGameOver) return;
        isGameOver = true;

        gameOverPanel.SetActive(true);

        int highScore = Data_Manager.GetHighScore();
        if (currentScore > highScore)
        {
            highScore = currentScore;
            Data_Manager.SetHighScore(highScore);
        }

        highScoreText.text = $"High Score: {highScore}";
        currentScoreText.text = $"Score: {currentScore}";

        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    // Game Over khi Player va chạm với Enemy
    public void GameOverByEnemy()
    {
        // Lấy điểm từ ScoreManager
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            int currentScore = scoreManager.GetCurrentScore();
            ShowGameOverMenu(currentScore); // Gọi hàm hiển thị giao diện
        }
        else
        {
            Debug.LogError("ScoreManager not found!");
        }
    }
}
