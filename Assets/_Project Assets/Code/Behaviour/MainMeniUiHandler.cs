using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMeniUiHandler : MonoBehaviour
{
    UiManager uiManager;
    SfxManager sfxManager;

    [Header ("<size=15> [SCRIPTABLE]")]
    [SerializeField] private UrlCollection urlCollection;
    [SerializeField] private ApplicationSettings applicationSettings;

    [Header ("<size=15> [UI]")]
    [SerializeField] private Button okayButton;

    private void Start()
    {
        uiManager = UiManager.instance;
        sfxManager = SfxManager.instance;

        uiManager.OpenCanvas(CanvasName.MAIN_MENU);

        CheckApplicationVersion();
    }

    public void OpenWarningCanvas()
    {
        uiManager.OpenPopup(CanvasName.WARNING);
        sfxManager.PlayClickSound();
    }

    public void _AgreeButton()
    {
        uiManager.ClosePopup(CanvasName.WARNING);
        sfxManager.PlayClickSound();
        Invoke(nameof(ChangeToScanningScene), 0.2f);
    }

    public void ChangeToScanningScene()
    {
        SceneManager.LoadScene(SceneData.SCANNING);
    }

    public void AssignAboutUs()
    {
        sfxManager.PlayClickSound();
        uiManager.OpenPopup(CanvasName.NOTICE);
        okayButton.onClick.RemoveAllListeners();
        okayButton.onClick.AddListener(AboutUs);
    }

    public void AssignPrivacy()
    {
        sfxManager.PlayClickSound();
        uiManager.OpenPopup(CanvasName.NOTICE);
        okayButton.onClick.RemoveAllListeners();
        okayButton.onClick.AddListener(PrivacyPolicyButton);
    }

    private void AboutUs()
    {
        Application.OpenURL(urlCollection.aboutUsURL);
        uiManager.ClosePopup(CanvasName.NOTICE);
        sfxManager.PlayClickSound();
    }

    private void PrivacyPolicyButton()
    {
        Application.OpenURL(urlCollection.privacyURL);
        uiManager.ClosePopup(CanvasName.NOTICE);
        sfxManager.PlayClickSound();
    }

    public void NoticeBackButton()
    {
        uiManager.ClosePopup(CanvasName.NOTICE);
        sfxManager.PlayClickSound();
    }

    public void UpdateNowButton()
    {
        uiManager.ClosePopup(CanvasName.UPDATE_AVAILABLE);
        Application.OpenURL(urlCollection.updateURL);
        sfxManager.PlayClickSound();
    }

    public void CheckApplicationVersion()
    {
        if (applicationSettings.appCurrentVersion != applicationSettings.appLiveVersion)
        {
            uiManager.OpenPopup(CanvasName.UPDATE_AVAILABLE);
        }
    }
}
