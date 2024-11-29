using UnityEngine;
using UnityEngine.UI;

public class SoundToggleUI : MonoBehaviour
{
    public Button musicToggleButton; // Nút bật/tắt nhạc
    public Button soundToggleButton; // Nút bật/tắt âm thanh
    public GameObject settingsPanel; // Panel chứa settings

    public Sprite musicOnSprite; // Hình ảnh khi nhạc bật
    public Sprite musicOffSprite; // Hình ảnh khi nhạc tắt
    public Sprite soundOnSprite; // Hình ảnh khi âm thanh bật
    public Sprite soundOffSprite; // Hình ảnh khi âm thanh tắt

    private void Start()
    {
        settingsPanel.SetActive(false); // Panel bị ẩn khi bắt đầu
        UpdateMusicToggleUI();
        UpdateSoundToggleUI();
    }

    public void ToggleMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic();
            UpdateMusicToggleUI();
        }
    }

    public void ToggleSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleSound();
            UpdateSoundToggleUI();
        }
    }

    private void UpdateMusicToggleUI()
    {
        if (AudioManager.Instance != null)
        {
            bool isMusicOn = AudioManager.Instance.IsMusicOn();
            musicToggleButton.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        }
    }

    private void UpdateSoundToggleUI()
    {
        if (AudioManager.Instance != null)
        {
            bool isSoundOn = AudioManager.Instance.IsSoundOn();
            soundToggleButton.image.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }

    // Mở panel settings
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true); // Hiển thị panel
    }

    // Đóng panel settings
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false); // Ẩn panel
    }
}
