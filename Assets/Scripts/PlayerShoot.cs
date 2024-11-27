using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab của đạn
    public Transform bulletSpawnPoint; // Vị trí xuất phát của đạn
    public float shootCooldown = 0.5f; // Thời gian chờ giữa các lần bắn (giây)

    private float nextShootTime = 0f; // Thời gian có thể bắn tiếp theo

    void Update()
    {
        // Kiểm tra nếu nhấn phím Space và đủ thời gian cooldown
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Tạo đạn tại vị trí spawn
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Cập nhật thời gian bắn tiếp theo
        nextShootTime = Time.time + shootCooldown;
    }
}
