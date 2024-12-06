using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : Collectible
{
    private bool isFalling = false; // Kiểm tra trạng thái đang rơi
    public float boostForce = 80f; // Lực đẩy lên khi gắn vào Player
    private Rigidbody2D playerRigidbody; // Tham chiếu Rigidbody2D của Player
    private bool isAttached = false; // Kiểm tra nếu Propeller đang gắn vào Player

    private PlatformManager gameManager; // Tham chiếu đến PlatformManager

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // Xử lý rơi nếu Propeller đang trong trạng thái rơi
        if (isFalling)
        {
            HandleFalling();
        }

        // Kiểm tra nếu lực đẩy đã hết và tách Propeller khỏi Player
        if (isAttached && playerRigidbody != null)
        {
            // Nếu vận tốc Player giảm (lực đẩy hết), kích hoạt trạng thái rơi
            if (playerRigidbody.velocity.y <= 0)
            {
                DetachFromPlayer();
            }
        }
    }

    private void HandleFalling()
    {
        // Xoay Propeller và di chuyển xuống
        transform.Rotate(new Vector3(0, 0, -3.5f));
        transform.position -= new Vector3(0, 0.3f * Time.fixedDeltaTime * 60f, 0);

        // Dừng âm thanh nếu đang phát
        if (TryGetComponent(out AudioSource audioSource) && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Kiểm tra khoảng cách phá hủy
        if (gameManager != null && gameManager.Player != null &&
            transform.position.y < gameManager.GetDestroyDistance(gameManager.Player.transform))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi Player va chạm với Propeller
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayerInteract(collision.gameObject);
        }
    }

    protected override void Collect(GameObject player)
    {
        if (!isAttached && player.transform.Find("Propeller") == null)
        {
            isAttached = true; // Đánh dấu Propeller đã được gắn vào Player
            playerRigidbody = player.GetComponent<Rigidbody2D>();

            // Gắn Propeller vào Player
            transform.parent = player.transform;
            transform.name = "Propeller"; // Đặt tên để dễ kiểm tra
            transform.localPosition = new Vector3(0, 4.5f, 0); // Điều chỉnh vị trí cho đúng với Player

            // Vô hiệu hóa collider
            if (TryGetComponent(out BoxCollider2D boxCollider))
            {
                boxCollider.enabled = false;
            }

            // Thêm lực đẩy lên cho Player
            if (playerRigidbody != null)
            {
                Vector2 force = playerRigidbody.velocity;
                force.y = boostForce; // Thêm lực đẩy
                playerRigidbody.velocity = force;

                // Phát âm thanh nếu có
                if (TryGetComponent(out AudioSource audioSource))
                {
                    audioSource.Play();
                }

                // Kích hoạt animation nếu có
                if (TryGetComponent(out Animator animator))
                {
                    animator.SetBool("Active", true);
                }

                // Đưa Propeller ra phía trước
                if (TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.sortingOrder = 12;
                }
            }
        }
    }


    private void DetachFromPlayer()
    {
        isAttached = false; // Đánh dấu Propeller không còn gắn vào Player
        transform.parent = null; // Gỡ khỏi Player

        // Dừng âm thanh nếu đang phát
        if (TryGetComponent(out AudioSource audioSource) && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        SetFalling(); // Kích hoạt trạng thái rơi
    }

    public void SetFalling()
    {
        isFalling = true;

        // Bật lại collider nếu cần
        if (TryGetComponent(out BoxCollider2D boxCollider))
        {
            boxCollider.enabled = true;
        }

        // Dừng âm thanh nếu Propeller đang rơi
        if (TryGetComponent(out AudioSource audioSource) && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
