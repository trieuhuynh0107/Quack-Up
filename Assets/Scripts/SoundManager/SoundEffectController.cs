using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Lấy `AudioSource` từ prefab
        audioSource = GetComponent<AudioSource>();

        // Kiểm tra và đồng bộ trạng thái từ `AudioManager`
        if (AudioManager.Instance != null)
        {
            audioSource.mute = !AudioManager.Instance.IsSoundEffectOn();
        }
    }

    public void UpdateSoundState()
    {
        // Cập nhật trạng thái bật/tắt dựa trên `AudioManager`
        if (audioSource != null && AudioManager.Instance != null)
        {
            audioSource.mute = !AudioManager.Instance.IsSoundEffectOn();
        }
    }
}
