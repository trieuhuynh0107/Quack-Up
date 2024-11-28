using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton để quản lý âm thanh toàn cục

    [Header("Audio Sources")]
    public AudioSource musicSource; // Nguồn nhạc nền
    public AudioSource[] soundSources; // Các nguồn âm thanh hiệu ứng

    private bool isSoundOn = true; // Trạng thái âm thanh
    private bool isMusicOn = true; // Trạng thái nhạc nền

    private const string SOUND_PREF_KEY = "SoundOn"; // Key lưu trạng thái âm thanh
    private const string MUSIC_PREF_KEY = "MusicOn"; // Key lưu trạng thái nhạc nền

    void Awake()
    {
        // Singleton Pattern để giữ AudioManager không bị phá hủy giữa các scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Tải trạng thái từ PlayerPrefs
        isSoundOn = PlayerPrefs.GetInt(SOUND_PREF_KEY, 1) == 1; // Mặc định bật nếu chưa có
        isMusicOn = PlayerPrefs.GetInt(MUSIC_PREF_KEY, 1) == 1; // Mặc định bật nếu chưa có

        // Áp dụng trạng thái ban đầu
        ApplySoundState();
        ApplyMusicState();
    }

    // Bật/Tắt âm thanh
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt(SOUND_PREF_KEY, isSoundOn ? 1 : 0); // Lưu trạng thái
        ApplySoundState();
    }

    // Bật/Tắt nhạc nền
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt(MUSIC_PREF_KEY, isMusicOn ? 1 : 0); // Lưu trạng thái
        ApplyMusicState();
    }

    // Áp dụng trạng thái âm thanh
    private void ApplySoundState()
    {
        foreach (var source in soundSources)
        {
            if (source != null)
            {
                source.mute = !isSoundOn; // Mute nếu tắt âm thanh
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
                if (!musicSource.isPlaying) musicSource.Play(); // Phát nhạc nếu chưa bật
            }
            else
            {
                musicSource.Pause(); // Tạm dừng nếu tắt nhạc
            }
        }
    }

    // Kiểm tra trạng thái âm thanh
    public bool IsSoundOn()
    {
        return isSoundOn;
    }

    // Kiểm tra trạng thái nhạc nền
    public bool IsMusicOn()
    {
        return isMusicOn;
    }
}
