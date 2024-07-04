using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public static BackgroundMusic instance;

    private void OnEnable()
        => SceneManager.activeSceneChanged += OnChangeScene;

    

    private void OnDisable()
        => SceneManager.activeSceneChanged += OnChangeScene;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayBackgroundMusic(bool check)
    {
        if (audioSource == null)
            return;

        if (check)
            audioSource.volume = 0.06f;
        else
            audioSource.volume = 0;
    }

    private void OnChangeScene(Scene next, Scene current)
    {
        if (current.name == "PlayVideo")  
            PlayBackgroundMusic(false);
        else
            PlayBackgroundMusic(true);
    }

}

