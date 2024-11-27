using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của quái vật
    public float spawnInterval = 3f; // Thời gian giữa các lần sinh
    public float minX = -2.5f; // Tọa độ X tối thiểu
    public float maxX = 2.5f;  // Tọa độ X tối đa
    public float spawnY = 6f; // Tọa độ Y nơi quái vật xuất hiện

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // Reset thời gian
        }
    }

    void SpawnEnemy()
    {
        // Tọa độ X ngẫu nhiên
        float randomX = Random.Range(minX, maxX);

        // Vị trí sinh quái vật
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Tạo quái vật
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
