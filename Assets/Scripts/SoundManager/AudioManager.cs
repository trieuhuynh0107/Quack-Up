using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource backgroundMusic; // Nhạc nền

    private bool isMusicOn = true;
    private bool isSoundEffectOn = true;

    void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ AudioManager qua các scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSoundEffects()
    {
        isSoundEffectOn = !isSoundEffectOn;

        // Thông báo cho tất cả các prefab có `SoundEffectController`
        SoundEffectController[] controllers = FindObjectsOfType<SoundEffectController>();
        foreach (var controller in controllers)
        {
            controller.UpdateSoundState();
        }

        Debug.Log($"Sound effects: {(isSoundEffectOn ? "On" : "Off")}");
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isMusicOn;
        }

        Debug.Log($"Background music: {(isMusicOn ? "On" : "Off")}");
    }

    public bool IsSoundEffectOn()
    {
        return isSoundEffectOn;
    }

    public bool IsMusicOn()
    {
        return isMusicOn;
    }
}
