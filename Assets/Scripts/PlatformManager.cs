using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Player & Platform Settings")]
    public GameObject Player; // Nhân vật chính
    public GameObject PlatformGreen;
    public GameObject PlatformBlue;
    public GameObject PlatformWhite;
    public GameObject PlatformBrown;

    [Header("Interactive Object Prefabs")]
    public GameObject Spring;
    public GameObject Trampoline;
    public GameObject Propeller;

    [Header("Platform Generation Settings")]
    public float offset = 1.2f; // Khoảng cách mặc định giữa các nền tảng
    public int initialPlatforms = 10; // Số lượng platform khởi tạo ban đầu

    private Vector3 topLeft; // Góc trên bên trái của màn hình
    private float CurrentY = 0f; // Vị trí Y hiện tại để tạo nền tảng

    void Start()
    {
        // Xác định ranh giới màn hình
        topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        // Khởi tạo các nền tảng ban đầu
        GeneratePlatform(initialPlatforms);
    }

    void Update()
    {
        // Kiểm tra nếu nhân vật đã lên đủ cao để tạo tầng mới
        if (Player != null && Player.transform.position.y > CurrentY - 10f)
        {
            GeneratePlatform(5); // Tạo thêm 5 nền tảng mới
        }
    }

    public void GeneratePlatform(int num)
    {
        for (int i = 0; i < num; i++)
        {
            // Tính toán vị trí X và Y ngẫu nhiên cho platform
            float distX = Random.Range(topLeft.x + offset, -topLeft.x - offset);
            float distY = Random.Range(1f, 4f); // Khoảng cách ngẫu nhiên giữa các nền tảng

            // Tăng CurrentY để đặt nền tảng mới cao hơn
            CurrentY += distY;
            Vector3 platformPos = new Vector3(distX, CurrentY, 0);

            // Tạo nền tảng ngẫu nhiên
            GameObject platform = CreateRandomPlatform(platformPos);

            // Nếu platform được tạo, thêm vật phẩm tương tác ngẫu nhiên
            if (platform != null)
            {
                CreateRandomInteractiveObject(platform, platformPos);
            }
        }
    }

    private GameObject CreateRandomPlatform(Vector3 position)
    {
        int randPlatform = Random.Range(1, 10);

        if (randPlatform == 1) // Tạo nền tảng xanh dương
        {
            return Instantiate(PlatformBlue, position, Quaternion.identity);
        }
        else if (randPlatform == 2) // Tạo nền tảng trắng
        {
            return Instantiate(PlatformWhite, position, Quaternion.identity);
        }
        else if (randPlatform == 3) // Tạo nền tảng nâu
        {
            return Instantiate(PlatformBrown, position, Quaternion.identity);
        }
        else // Tạo nền tảng xanh lá
        {
            return Instantiate(PlatformGreen, position, Quaternion.identity);
        }
    }

    private void CreateRandomInteractiveObject(GameObject platform, Vector3 platformPos)
    {
        int randObject = Random.Range(1, 40);

        if (randObject == 4) // Tạo Spring
        {
            Vector3 springPos = new Vector3(platformPos.x + 0.5f, platformPos.y + 0.27f, 0);
            AttachObjectToPlatform(Spring, platform, springPos);
        }
        else if (randObject == 7) // Tạo Trampoline
        {
            Vector3 trampolinePos = new Vector3(platformPos.x + 0.13f, platformPos.y + 0.25f, 0);
            AttachObjectToPlatform(Trampoline, platform, trampolinePos);
        }
        else if (randObject == 15) // Tạo Propeller
        {
            Vector3 propellerPos = new Vector3(platformPos.x + 0.13f, platformPos.y + 0.75f, 0);
            AttachObjectToPlatform(Propeller, platform, propellerPos);
        }
    }

    private void AttachObjectToPlatform(GameObject objPrefab, GameObject platform, Vector3 position)
    {
        if (objPrefab != null && platform != null)
        {
            GameObject obj = Instantiate(objPrefab, position, Quaternion.identity);
            obj.transform.parent = platform.transform; // Gán object làm con của platform
        }
    }

    public float GetDestroyDistance(Transform playerTransform)
    {
        // Tính khoảng cách phá hủy: Dưới Player 10 đơn vị
        return playerTransform.position.y - 10f;
    }

}
