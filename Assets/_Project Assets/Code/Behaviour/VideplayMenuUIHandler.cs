using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Video;

public class VideplayMenuUIHandler : MonoBehaviour
{
    [Header ("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videoPlayerData;

    [Header("<size=15>COMPOENENTS")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<GameObject> videoElements = new List<GameObject>();
    [SerializeField] private List<GameObject> audioElements = new List<GameObject>();

    [Header ("<size=15>USER INTERFACE")]
    [SerializeField] private TMP_Text audioToggleText;
    [SerializeField] private string trackName;
    [SerializeField] private TMP_Text currentVideoNameVideo;
    [SerializeField] private TMP_Text currentVideoNameOnlyImage;

    [SerializeField] private VideoPlayerProgress videoPlayerprogress;

    [SerializeField] private bool showVideo = true;

    private void Awake()
    {
        videoPlayer.url = videoPlayerData.videoDirectLink;
        videoPlayer.Play();
    }

    private void Start()
    {
        SetVideoName();
        ShowVideo(showVideo);
    }

    public void BackButton()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        videoPlayerprogress.ChangeScreenOrientation(ScreenStatus.PORTRAITE);
        Invoke(nameof(BackToScanningScene), 0.3f);
    }

    private void BackToScanningScene()
    {
        SceneManager.LoadScene(SceneData.SCANNING);
    }

    private void SetVideoName()
    {
        trackName = $"{videoPlayerData.currentBookName} - {videoPlayerData.currentTrackName}";

        currentVideoNameVideo.text = trackName;
        currentVideoNameOnlyImage.text = trackName;
    }


    private void ShowVideo(bool check)
    {
        foreach (GameObject ve in videoElements)
        {
            ve.SetActive(check);
        }
        foreach (GameObject ae in audioElements)
        {
            ae.SetActive(!check);
        }
     
        if (check)
            audioToggleText.text = "AUDIO";
        else
            audioToggleText.text = "VIDEO";
    }

    public void _AudioVideoToggle()
    {
        if (showVideo)
            showVideo = false;  
        else
            showVideo = true;  
        
        ShowVideo(showVideo);
    }
}
