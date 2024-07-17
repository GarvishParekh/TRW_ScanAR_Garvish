using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScanningUiHandler : MonoBehaviour
{
    UiManager uiManager;
    SfxManager sfxManager;

    [SerializeField] private VideoPlayerData videoPlayerData;

    [Header("<size=15>UI")]
    [SerializeField] private Image audioImage;
    [SerializeField] private TMP_Text audioText;

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

        uiManager.OpenCanvas(CanvasName.SCANNING);
        ChangeToAudio();
    }

    public void _BackToMainMenu()
    {
        ResetXRSettings();
        SceneManager.LoadScene(SceneData.MAINMENU);

        sfxManager?.PlayClickSound();
    }

    public void _ScanAgain()
    {

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
                break;
            case VideoPlayerType.VIDEO:
                videoPlayerData.videoPlayerType = VideoPlayerType.VIDEO;

                audioImage.color = unSelectedColorImage;
                audioText.color = unSelectedColorText;

                videoImage.color = selectedColorImage;
                videoText.color = selectedColorText;
                break;
        }
    }

    public void ChangeToAudio()
        => ChangeVideoPlayerType(VideoPlayerType.AUDIO);
    public void ChangeToVideo()
        => ChangeVideoPlayerType(VideoPlayerType.VIDEO);
        
}
