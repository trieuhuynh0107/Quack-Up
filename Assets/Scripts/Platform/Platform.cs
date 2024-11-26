using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float JumpForce = 600f; // Lực nhảy mặc định

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với Player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rigid != null && rigid.velocity.y <= 0)
            {
                // Gọi hành vi khi Player chạm vào (được ghi đè trong lớp con)
                OnPlayerContact(rigid);
            }
        }
    }

    // Hành vi mặc định khi Player chạm vào (có thể ghi đè)
    protected virtual void OnPlayerContact(Rigidbody2D playerRigidbody)
    {
        // Thêm lực nhảy
        playerRigidbody.AddForce(Vector3.up * JumpForce);

        // Phát âm thanh nếu có AudioSource
        if (TryGetComponent(out AudioSource audioSource))
        {
            audioSource.Play();
        }

        // Kích hoạt animation nếu có Animator
        if (TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Activate");
        }
    }

    // Phương thức vô hiệu hóa (có thể ghi đè)
    public virtual void Deactivate()
    {
        // Logic mặc định: không làm gì
    }
}
