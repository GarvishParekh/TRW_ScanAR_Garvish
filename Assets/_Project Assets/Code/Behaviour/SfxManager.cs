using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SfxManager : MonoBehaviour
{
    AudioSource audioSource;
    public static SfxManager instance;
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
}
