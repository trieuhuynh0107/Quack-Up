using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Mảng chứa các Prefab Enemy
    public Transform player; // Tham chiếu đến Player
    public float spawnHeightOffset = 20f; // Khoảng cách Y từ player để spawn enemy
    public float minX = -5f, maxX = 5f; // Giới hạn trục X để spawn enemy
    public float minSpawnInterval = 5f; // Khoảng cách tối thiểu giữa các lần spawn (theo Y)
    public float maxSpawnInterval = 10f; // Khoảng cách tối đa giữa các lần spawn (theo Y)

    private float nextSpawnY; // Vị trí Y để spawn tiếp theo
    private List<GameObject> activeEnemies = new List<GameObject>(); // Danh sách các enemy đang tồn tại

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned in EnemySpawner!");
            return;
        }

        // Thiết lập vị trí spawn ban đầu cao hơn player
        nextSpawnY = player.position.y + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Kiểm tra nếu Player vượt qua ngưỡng spawn tiếp theo
        if (player.position.y > nextSpawnY - spawnHeightOffset* 3)
        {
            // Random spawn enemy (chỉ khi random đủ nhỏ)
            if (Random.value < 0.4f) // 40% cơ hội spawn
            {
                SpawnEnemy();
            }
            // Luôn cập nhật nextSpawnY để tránh kiểm tra lại liên tục
            nextSpawnY += Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        // Xóa các quái đã ra khỏi màn hình
        CleanupEnemies();
    }

    private void SpawnEnemy()
    {
        // Chọn vị trí ngẫu nhiên trên trục X
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, nextSpawnY);

        // Chọn một Enemy ngẫu nhiên từ mảng Prefabs
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Thêm enemy mới vào danh sách
        activeEnemies.Add(enemy);

        // Cập nhật vị trí spawn tiếp theo ngẫu nhiên
        nextSpawnY += Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void CleanupEnemies()
    {
        // Xóa các enemy ra khỏi màn hình (dưới vị trí Player)
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = activeEnemies[i];

            if (enemy != null && enemy.transform.position.y < player.position.y - spawnHeightOffset)
            {
                Destroy(enemy); // Xóa enemy khỏi scene
                activeEnemies.RemoveAt(i); // Xóa khỏi danh sách
            }
        }
    }
}
