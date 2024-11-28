using System.Collections.Generic;
using UnityEngine;

public class DuckSkinManager : MonoBehaviour
{
    public List<Sprite> duckSkins; // Danh sách các Sprite cho các skin
    private SpriteRenderer spriteRenderer; // SpriteRenderer của Duck

    private void Start()
    {
        // Lấy SpriteRenderer từ Duck
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on Duck!");
            return;
        }

        // Tải và áp dụng skin đã lưu
        int savedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0); // Mặc định là skin 0
        ChangeSkin(savedSkinIndex);
    }

    public void ChangeSkin(int index)
    {
        // Kiểm tra index hợp lệ
        if (index >= 0 && index < duckSkins.Count)
        {
            spriteRenderer.sprite = duckSkins[index]; // Thay đổi sprite
            Debug.Log("Skin changed to index: " + index);
        }
        else
        {
            Debug.LogWarning("Invalid skin index: " + index);
        }
    }
}
