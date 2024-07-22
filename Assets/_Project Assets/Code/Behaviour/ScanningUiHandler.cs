using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScanningUiHandler : MonoBehaviour
{
    public static Action LoadAudio;
    public static Action LoadVideo;

    UiManager uiManager;
    SfxManager sfxManager;
    jsonConverter converter;

    [SerializeField] private VideoPlayerData videoPlayerData;

    [Header("<size=15>SCRIPTS")]
    [SerializeField] private ImageTracking imageTracking;

    [Header("<size=15>UI")]
    [SerializeField] private Image audioImage;
    [SerializeField] private TMP_Text audioText;
    [SerializeField] private List<TrackCell> spawnedCell;

    [Space]
    [SerializeField] private Image videoImage;
    [SerializeField] private TMP_Text videoText;

    [Header("<size=15>UI COLOR")]
    [SerializeField] private Color selectedColorImage;
    [SerializeField] private Color unSelectedColorImage;

    [Space]
    [SerializeField] private Color selectedColorText;
    [SerializeField] private Color unSelectedColorText;


    void Start()
    {
        uiManager = UiManager.instance;
        sfxManager = SfxManager.instance;
        converter = jsonConverter.instance;

        if (converter.previousScannedBookName == string.Empty)
            uiManager.OpenCanvas(CanvasName.SCANNING);
        else
        {
            imageTracking.NoNeedToScan();
            imageTracking.GenerateBookData(converter.previousScannedBookName);
        }
        ChangeToAudio();
    }

    public void _BackToMainMenu()
    {
        converter.previousScannedBookName = string.Empty;
        ResetXRSettings();
        SceneManager.LoadScene(SceneData.MAINMENU);

        sfxManager?.PlayClickSound();
    }

    public void _ScanAgain()
    {
        converter.previousScannedBookName = string.Empty;
        ResetXRSettings();
        sfxManager?.PlayClickSound();

        SceneManager.LoadScene(SceneData.SCANNING);
    }

    private void ResetXRSettings()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        xrManagerSettings.InitializeLoaderSync();
    }

    private void ChangeVideoPlayerType(VideoPlayerType _videoPlayerType)
    {
        switch(_videoPlayerType)
        {
            case VideoPlayerType.AUDIO:
                videoPlayerData.videoPlayerType = VideoPlayerType.AUDIO;

                audioImage.color = selectedColorImage;
                audioText.color = selectedColorText;

                videoImage.color = unSelectedColorImage;
                videoText.color = unSelectedColorText;

                LoadAudio?.Invoke();
                break;
            case VideoPlayerType.VIDEO:
                videoPlayerData.videoPlayerType = VideoPlayerType.VIDEO;

                audioImage.color = unSelectedColorImage;
                audioText.color = unSelectedColorText;

                videoImage.color = selectedColorImage;
                videoText.color = selectedColorText;

                LoadVideo?.Invoke();
                break;
        }
    }

    public void ChangeToAudio()
        => ChangeVideoPlayerType(VideoPlayerType.AUDIO);
    public void ChangeToVideo()
        => ChangeVideoPlayerType(VideoPlayerType.VIDEO);
        
}
