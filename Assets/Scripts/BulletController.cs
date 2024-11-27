using UnityEngine;

public class BulletController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu viên đạn va chạm với quái vật
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Hủy quái vật
            Destroy(gameObject); // Hủy viên đạn
        }
    }
}
