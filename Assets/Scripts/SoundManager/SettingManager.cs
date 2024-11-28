using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button soundButton; // Nút bật/tắt âm thanh
    public Button musicButton; // Nút bật/tắt nhạc nền
    public Sprite soundOnSprite; // Hình ảnh bật âm thanh
    public Sprite soundOffSprite; // Hình ảnh tắt âm thanh
    public Sprite musicOnSprite; // Hình ảnh bật nhạc nền
    public Sprite musicOffSprite; // Hình ảnh tắt nhạc nền

    [Header("Audio Sources")]
    public AudioSource musicSource; // Nhạc nền
    public AudioSource[] soundSources; // Các nguồn âm thanh hiệu ứng

    private bool isSoundOn; // Trạng thái âm thanh
    private bool isMusicOn; // Trạng thái nhạc nền

    private const string SOUND_PREF_KEY = "SoundOn"; // Key lưu trạng thái âm thanh
    private const string MUSIC_PREF_KEY = "MusicOn"; // Key lưu trạng thái nhạc nền

    void Start()
    {
        // Load trạng thái từ PlayerPrefs
        isSoundOn = PlayerPrefs.GetInt(SOUND_PREF_KEY, 1) == 1; // Mặc định bật nếu không có key
        isMusicOn = PlayerPrefs.GetInt(MUSIC_PREF_KEY, 1) == 1; // Mặc định bật nếu không có key

        // Gán sự kiện cho các nút
        soundButton.onClick.AddListener(ToggleSound);
        musicButton.onClick.AddListener(ToggleMusic);

        // Cập nhật trạng thái ban đầu
        UpdateSoundButton();
        UpdateMusicButton();

        // Áp dụng trạng thái âm thanh và nhạc nền
        ApplySoundState();
        ApplyMusicState();
    }

    // Bật/Tắt âm thanh
    private void ToggleSound()
    {
        isSoundOn = !isSoundOn; // Đảo trạng thái âm thanh
        PlayerPrefs.SetInt(SOUND_PREF_KEY, isSoundOn ? 1 : 0); // Lưu trạng thái vào PlayerPrefs
        PlayerPrefs.Save(); // Lưu dữ liệu ngay lập tức
        ApplySoundState(); // Áp dụng thay đổi trạng thái âm thanh
        UpdateSoundButton(); // Cập nhật hình ảnh nút
    }

    // Bật/Tắt nhạc nền
    private void ToggleMusic()
    {
        isMusicOn = !isMusicOn; // Đảo trạng thái nhạc nền
        PlayerPrefs.SetInt(MUSIC_PREF_KEY, isMusicOn ? 1 : 0); // Lưu trạng thái vào PlayerPrefs
        PlayerPrefs.Save(); // Lưu dữ liệu ngay lập tức
        ApplyMusicState(); // Áp dụng thay đổi trạng thái nhạc nền
        UpdateMusicButton(); // Cập nhật hình ảnh nút
    }

    // Cập nhật hình ảnh của nút âm thanh
    private void UpdateSoundButton()
    {
        if (soundButton != null)
        {
            soundButton.image.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }

    // Cập nhật hình ảnh của nút nhạc nền
    private void UpdateMusicButton()
    {
        if (musicButton != null)
        {
            musicButton.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        }
    }

    // Áp dụng trạng thái âm thanh
    private void ApplySoundState()
    {
        foreach (var source in soundSources)
        {
            if (source != null)
            {
                source.mute = !isSoundOn; // Tắt tiếng nếu trạng thái âm thanh tắt
            }
        }
    }

    // Áp dụng trạng thái nhạc nền
    private void ApplyMusicState()
    {
        if (musicSource != null)
        {
            if (isMusicOn)
            {
                if (!musicSource.isPlaying)
                {
                    musicSource.Play(); // Bật nhạc nếu chưa phát
                }
            }
            else
            {
                musicSource.Pause(); // Tạm dừng nếu tắt nhạc
            }
        }
    }
}
