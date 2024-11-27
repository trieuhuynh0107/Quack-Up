using Game.Score;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; // Tốc độ rơi của đạn
    public float lifetime = 3f; // Thời gian tồn tại của đạn trước khi tự hủy

    private Rigidbody2D rb;

    void Start()
    {
        // Đặt vận tốc thẳng xuống cho Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * speed; // Rơi thẳng xuống
        }

        // Hủy đạn sau thời gian nhất định
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Lấy GameOverManager
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                // Lấy ScoreManager
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null)
                {
                    int currentScore = scoreManager.GetCurrentScore(); // Lấy điểm hiện tại
                    gameOverManager.ShowGameOverMenu(currentScore); // Hiển thị Game Over
                }
            }
            // Hủy đạn sau khi va chạm với Player
            Destroy(gameObject);
        }
    }

}
