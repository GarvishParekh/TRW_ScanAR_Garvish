using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    AudioSource audioSource;
    
    public AudioData audioData;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();  
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(audioData.buttonClickSound);
    }

    public void PlayBookFoundSound()
    {
        audioSource.PlayOneShot(audioData.bookFoundSound);
    }
}
