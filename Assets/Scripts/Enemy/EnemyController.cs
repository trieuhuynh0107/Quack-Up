using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển

    void Update()
    {
        // Di chuyển quái vật xuống dưới
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        // Hủy quái vật nếu vượt khỏi màn hình
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 1f)
        {
            Destroy(gameObject);
        }
    }
}
