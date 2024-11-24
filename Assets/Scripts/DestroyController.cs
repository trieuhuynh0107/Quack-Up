using UnityEngine;

public class DestroyerController : MonoBehaviour
{
    public Transform player; // Nhân vật
    public float distanceBelowPlayer = 10f; // Khoảng cách dưới nhân vật

    void Update()
    {
        if (player != null)
        {
            // Destroyer di chuyển theo nhân vật
            transform.position = new Vector3(
                player.position.x,
                player.position.y - distanceBelowPlayer,
                transform.position.z
            );
        }
    }
}
