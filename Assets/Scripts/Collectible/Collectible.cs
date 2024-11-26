using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    protected bool isCollected = false; // Kiểm tra nếu đối tượng đã được thu thập

    // Khi Player tương tác
    public virtual void OnPlayerInteract(GameObject player)
    {
        if (!isCollected)
        {
            isCollected = true;
            Collect(player);
        }
    }

    // Phương thức thu thập (cần được ghi đè)
    protected abstract void Collect(GameObject player);

    // Hủy đối tượng
    protected void DestroyCollectible()
    {
        Destroy(gameObject);
    }
}
