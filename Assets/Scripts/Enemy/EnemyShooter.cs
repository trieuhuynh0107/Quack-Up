using UnityEngine;

public class EnemyShooter : EnemyBase
{
    public GameObject bulletPrefab; // Prefab của đạn
    public Transform bulletSpawnPoint; // Vị trí bắn đạn
    public float shootCooldown = 2f; // Thời gian giữa các lần bắn
    private float nextShootTime; // Thời gian có thể bắn tiếp theo

    protected override void Update()
    {
        base.Update(); // Gọi logic di chuyển từ EnemyBase

        // Kiểm tra cooldown để bắn đạn
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown; // Reset thời gian bắn tiếp theo
        }
    }

    private void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Đảm bảo đạn có vận tốc đúng
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = Vector2.down * 5f; // Rơi thẳng xuống
            }
        }
    }

}
