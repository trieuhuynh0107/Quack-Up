using UnityEngine;
using UnityEngine.SceneManagement; // Dùng để quản lý scene

public class GameOverManager : MonoBehaviour
{
    public Transform player;
    public float gameOverThreshold = -10f;
    public GameObject gameOverUI; // Tham chiếu đến UI Game Over

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && player.position.y < gameOverThreshold)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");

        Time.timeScale = 0; // Dừng game
        gameOverUI.SetActive(true); // Hiển thị UI Game Over
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Khôi phục thời gian
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại scene hiện tại
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Khôi phục thời gian
        SceneManager.LoadScene("Main menu"); // Thay "MainMenu" bằng tên scene màn hình chính của bạn
    }
}
