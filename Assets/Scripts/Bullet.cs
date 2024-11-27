using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Tốc độ bay của đạn
    public int damage = 1; // Sát thương gây ra
    public float lifeTime = 2f; // Thời gian sống của đạn trước khi tự hủy

    void Start()
    {
        // Tự động hủy đạn sau khi hết thời gian sống
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Di chuyển đạn thẳng lên
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với Enemy
        if (collision.CompareTag("Enemy"))
        {
            // Gây sát thương cho Enemy
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Hủy đạn sau khi va chạm
            Destroy(gameObject);
        }
    }
}
