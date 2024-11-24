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
            FlipSprite(); // Xử lý hướng sprite
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
            if (moveX > 0 && !isFacingRight)
            {
                isFacingRight = true;
                spriteRenderer.flipX = false; // Quay mặt về bên phải
            }
            else if (moveX < 0 && isFacingRight)
            {
                isFacingRight = false;
                spriteRenderer.flipX = true; // Quay mặt về bên trái
            }
        }
    }
}
