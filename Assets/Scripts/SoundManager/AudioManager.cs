using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource; // Nhạc nền
    public List<GameObject> soundParents; // Danh sách các GameObject chứa sound effects (ví dụ: con vịt, đồ vật)

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    private void Awake()
    {
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

        // Lấy trạng thái từ PlayerPrefs
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;

        UpdateAudioStates();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;

        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;

        foreach (var parent in soundParents)
        {
            if (parent != null)
            {
                AudioSource[] sources = parent.GetComponentsInChildren<AudioSource>();
                foreach (var source in sources)
                {
                    source.mute = !isSoundOn;
                }
            }
        }

        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateAudioStates()
    {
        // Áp dụng trạng thái nhạc
        musicSource.mute = !isMusicOn;

        // Áp dụng trạng thái sound effects cho tất cả các GameObject con
        foreach (var parent in soundParents)
        {
            if (parent != null)
            {
                AudioSource[] sources = parent.GetComponentsInChildren<AudioSource>();
                foreach (var source in sources)
                {
                    source.mute = !isSoundOn;
                }
            }
        }
    }

    public void PlaySound(string soundName)
    {
        if (!isSoundOn) return;

        foreach (var parent in soundParents)
        {
            if (parent != null)
            {
                AudioSource[] sources = parent.GetComponentsInChildren<AudioSource>();
                foreach (var source in sources)
                {
                    if (source.name == soundName)
                    {
                        source.Play();
                        return;
                    }
                }
            }
        }

        Debug.LogWarning($"Sound '{soundName}' not found in soundParents.");
    }

    public bool IsMusicOn()
    {
        return isMusicOn;
    }

    public bool IsSoundOn()
    {
        return isSoundOn;
    }
}
