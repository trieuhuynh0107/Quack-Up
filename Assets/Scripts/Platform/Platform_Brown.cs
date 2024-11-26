using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Brown : Platform
{
    private bool isFalling = false; // Kiểm tra trạng thái rơi

    void FixedUpdate()
    {
        // Nếu nền tảng đang rơi, di chuyển xuống
        if (isFalling)
        {
            transform.position -= new Vector3(0, 0.15f * Time.fixedDeltaTime * 60f, 0);
        }
    }

    protected override void OnPlayerContact(Rigidbody2D playerRigidbody)
    {
        // Không cho phép Player nhảy
        Deactivate(); // Gọi hành vi rơi ngay lập tức
    }

    public override void Deactivate()
    {
        // Vô hiệu hóa BoxCollider2D nếu tồn tại
        if (TryGetComponent(out BoxCollider2D boxCollider))
        {
            boxCollider.enabled = false;
        }

        // Vô hiệu hóa PlatformEffector2D nếu tồn tại
        if (TryGetComponent(out PlatformEffector2D platformEffector))
        {
            platformEffector.enabled = false;
        }
        // Phát âm thanh khi nền tảng bắt đầu rơi
        if (TryGetComponent(out AudioSource audioSource))
        {
            audioSource.Play();
        }
        // Bắt đầu rơi
        isFalling = true;

        // Xóa nền tảng sau 2 giây
        Destroy(gameObject, 2f);
    }
}
