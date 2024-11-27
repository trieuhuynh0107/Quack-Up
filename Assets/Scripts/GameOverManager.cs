using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro cho UI

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Game Over Panel
    public TextMeshProUGUI highScoreText; // Hiển thị High Score
    public TextMeshProUGUI currentScoreText; // Hiển thị Current Score

    private bool isGameOver = false; // Trạng thái Game Over

    public void ShowGameOverMenu(int currentScore)
    {
        if (isGameOver) return; // Tránh gọi nhiều lần
        isGameOver = true;

        // Hiển thị Game Over Panel
        gameOverPanel.SetActive(true);

        // Cập nhật High Score
        int highScore = Data_Manager.GetHighScore();
        if (currentScore > highScore)
        {
            highScore = currentScore;
            Data_Manager.SetHighScore(highScore); // Lưu điểm cao nhất
        }

        // Hiển thị điểm trong Panel
        highScoreText.text = $"High Score: {highScore}";
        currentScoreText.text = $"Score: {currentScore}";

        // Dừng game
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        Time.timeScale = 1; // Tiếp tục thời gian
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại scene hiện tại
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Tiếp tục thời gian
        SceneManager.LoadScene("Menu"); // Đổi tên "Menu" thành tên scene chính của bạn
    }
}
