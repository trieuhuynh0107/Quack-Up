using UnityEngine;

public class DestroyByTrigger : MonoBehaviour
{
    public GameObject platformPrefab; // Gán Prefab gốc của Platform

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng là instance của Platform Prefab
        if (collision.gameObject.name.Contains(platformPrefab.name))
        {
            Destroy(collision.gameObject); // Xóa đối tượng
        }
    }
}
