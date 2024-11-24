using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject Player; // Nhân vật chính
    public GameObject platformPrefab; // Prefab nền tảng
    public GameObject specialPlatformPrefab; // Prefab nền đặc biệt
    public int slotsPerLayer = 6; // Số khu vực trong một tầng (số ô dọc theo trục X)
    public float minLayerHeight = 3.0f; // Khoảng cách nhỏ nhất giữa các tầng
    public float maxLayerHeight = 8.0f; // Khoảng cách lớn nhất giữa các tầng
    public int minPlatformsPerLayer = 2; // Số lượng nền ít nhất trong một tầng
    public int maxPlatformsPerLayer = 5; // Số lượng nền nhiều nhất trong một tầng

    public float minX = -5.5f; // Giới hạn trái
    public float maxX = 5.5f; // Giới hạn phải
    public float minDistanceBetweenPlatforms = 1.5f; // Khoảng cách tối thiểu giữa các nền

    private float nextLayerY = 4.0f; // Vị trí Y cho tầng tiếp theo
   

    void Start()
    {
        // Spawn trước 3 tầng khi bắt đầu game
        SpawnInitialLayers(3);

    }

    void Update()
    {
       
        // Kiểm tra nếu nhân vật đã lên đủ cao để tạo tầng mới
        if (Player != null && Player.transform.position.y > nextLayerY - minLayerHeight*5)
        {
            SpawnLayer();
        }
    }
    private void SpawnInitialLayers(int layersToSpawn)
    {
        for (int i = 0; i < layersToSpawn; i++)
        {
            SpawnLayer();
        }
    }

    private void SpawnLayer()
    {
        // Random số lượng Lifebuoy cho tầng này
        int platformsThisLayer = Random.Range(minPlatformsPerLayer, maxPlatformsPerLayer);

        // Chia dải X thành các slot
        float slotWidth = (maxX - minX) / slotsPerLayer; // Chiều rộng mỗi slot
        List<int> usedSlots = new List<int>(); // Danh sách các slot đã dùng

        for (int i = 0; i < platformsThisLayer; i++)
        {
            int randomSlot;

            // Random slot chưa được sử dụng
            do
            {
                randomSlot = Random.Range(0, slotsPerLayer);
            } while (usedSlots.Contains(randomSlot) && usedSlots.Count < slotsPerLayer);

            // Đánh dấu slot này đã được dùng
            usedSlots.Add(randomSlot);

            // Random vị trí X trong slot
            float randomX = minX + randomSlot * slotWidth + Random.Range(-slotWidth / 3, slotWidth / 3);

            // Random lệch nhẹ theo trục Y
            float offsetY = Random.Range(-2.0f, 1.0f);
            Vector2 spawnPosition = new Vector2(randomX, nextLayerY + offsetY);

            // Random loại nền tảng
            if (Random.value < 0.2f) // 20% cơ hội tạo nền đặc biệt
            {
                Instantiate(specialPlatformPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            }
        }

        // Random khoảng cách đến tầng tiếp theo
        nextLayerY += Random.Range(minLayerHeight, maxLayerHeight);
    }
}
