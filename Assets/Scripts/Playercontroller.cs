using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 10f; // Tốc độ di chuyển
        public GameObject bulletPrefab; // Prefab của viên đạn
        public Transform bulletSpawnPoint; // Điểm xuất phát của viên đạn
        public float bulletSpeed = 20f; // Tốc độ của viên đạn

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private float moveX; // Lưu giá trị Input.Horizontal
        private bool isFacingRight = true; // Hướng mặc định của nhân vật

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            // Nhận Input từ bàn phím
            moveX = Input.GetAxisRaw("Horizontal"); // Sử dụng Input thô để tránh độ trễ

            // Gọi FlipSprite khi có sự thay đổi hướng di chuyển
            if ((moveX > 0 && !isFacingRight) || (moveX < 0 && isFacingRight))
            {
                FlipSprite();
            }

            // Nhận Input bắn đạn
            if (Input.GetMouseButtonDown(0)) // Nhấn chuột trái
            {
                ShootBullet();
            }
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        // Xử lý di chuyển trong FixedUpdate
        private void MovePlayer()
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y); // Di chuyển theo trục X
        }

        // Xử lý hướng nhân vật
        private void FlipSprite()
        {
            isFacingRight = !isFacingRight; // Đảo trạng thái hướng mặt
            spriteRenderer.flipX = !spriteRenderer.flipX; // Đảo chiều sprite
        }

        // Xử lý bắn đạn
        private void ShootBullet()
        {
            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                // Tạo viên đạn tại vị trí bulletSpawnPoint
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

                // Thêm vận tốc cho viên đạn
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                if (bulletRb != null)
                {
                    bulletRb.velocity = new Vector2(0, bulletSpeed); // Đạn bắn lên theo trục Y
                }

                // Hủy viên đạn sau 2 giây để tránh quá tải
                Destroy(bullet, 2f);
            }
            else
            {
                Debug.LogWarning("Bullet prefab or spawn point is not assigned!");
            }
        }
    }
}
