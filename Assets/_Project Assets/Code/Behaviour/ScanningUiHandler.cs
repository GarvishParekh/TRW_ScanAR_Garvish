using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanningUiHandler : MonoBehaviour
{
    UiManager uiManager;
    SfxManager sfxManager;

    void Start()
    {
        uiManager = UiManager.instance;
        sfxManager = SfxManager.instance;

        uiManager.OpenCanvas(CanvasName.SCANNING);
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
}
