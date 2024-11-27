using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health = 1; // Máu của Enemy
    public float moveSpeed = 2f; // Tốc độ di chuyển
    public float moveRange = 2f; // Khoảng cách di chuyển

    protected Vector3 startingPosition;
    protected bool movingRight = true;

    protected virtual void Start()
    {
        startingPosition = transform.position; // Lưu vị trí ban đầu
    }

    protected virtual void Update()
    {
        MoveEnemy();
    }

    protected void MoveEnemy()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= startingPosition.x + moveRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= startingPosition.x - moveRange)
            {
                movingRight = true;
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Hủy Enemy nếu máu <= 0
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Tìm GameOverManager trong Scene
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                // Tính điểm hiện tại (tùy thuộc vào game của bạn)
                int currentScore = 0; // Thay thế bằng hệ thống điểm hiện tại
                gameOverManager.ShowGameOverMenu(currentScore); // Hiển thị giao diện Game Over
            }
        }
    }
}
