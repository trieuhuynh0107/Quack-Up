using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 10f; // Tốc độ di chuyển
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
    }
}
