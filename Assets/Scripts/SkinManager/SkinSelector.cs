using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public GameObject skinSelectionPanel; // Panel chứa giao diện chọn skin
    public Image skinDisplay; // UI hiển thị skin
    public List<Sprite> skinSprites; // Danh sách sprite của các skin
    private int currentSkinIndex = 0; // Chỉ số skin hiện tại

    private void Start()
    {
        skinSelectionPanel.SetActive(false); // Ẩn panel khi bắt đầu
        UpdateSkinDisplay(); // Cập nhật skin hiển thị đầu tiên
    }

    // Mở giao diện chọn skin
    public void OpenSkinSelection()
    {
        skinSelectionPanel.SetActive(true); // Hiển thị panel
        UpdateSkinDisplay();
    }

    // Đóng giao diện chọn skin
    public void CloseSkinSelection()
    {
        skinSelectionPanel.SetActive(false); // Ẩn panel
    }

    // Chuyển sang skin tiếp theo
    public void NextSkin()
    {
        currentSkinIndex = (currentSkinIndex + 1) % skinSprites.Count; // Chuyển vòng qua danh sách
        UpdateSkinDisplay();
    }

    // Quay lại skin trước đó
    public void PreviousSkin()
    {
        currentSkinIndex = (currentSkinIndex - 1 + skinSprites.Count) % skinSprites.Count; // Chuyển vòng ngược
        UpdateSkinDisplay();
    }

    // Cập nhật hình ảnh hiển thị skin
    private void UpdateSkinDisplay()
    {
        skinDisplay.sprite = skinSprites[currentSkinIndex]; // Hiển thị sprite tương ứng
    }

    // Lưu skin hiện tại vào PlayerPrefs
    public void SelectCurrentSkin()
    {
        PlayerPrefs.SetInt("SelectedSkin", currentSkinIndex); // Lưu chỉ số skin
        PlayerPrefs.Save(); // Lưu dữ liệu
        Debug.Log("Skin selected and saved: " + currentSkinIndex);
        CloseSkinSelection(); // Đóng panel sau khi chọn
    }
}
