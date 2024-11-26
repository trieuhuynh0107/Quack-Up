using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_White : Platform
{
    private bool hasBounced = false; // Đã nhảy một lần chưa?

    protected override void OnPlayerContact(Rigidbody2D playerRigidbody)
    {
        if (!hasBounced)
        {
            // Gọi logic nhảy mặc định từ lớp cha
            base.OnPlayerContact(playerRigidbody);

            // Đánh dấu đã nhảy và vô hiệu hóa nền tảng
            hasBounced = true;
            Deactivate();
        }
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

        // Xóa nền tảng sau 0.5 giây
        Destroy(gameObject, 0.5f);
    }
}
