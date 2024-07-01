using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class ScanningUiHandler : MonoBehaviour
{
    UiManager uiManager;

    private void Awake()
    {

    }

    void Start()
    {
        uiManager = UiManager.instance;

        uiManager.OpenCanvas(CanvasName.SCANNING);
    }

    public void _BackToMainMenu()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        xrManagerSettings.InitializeLoaderSync();
        SceneManager.LoadScene(0);
    }

    public void _ScanAgain()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        xrManagerSettings.InitializeLoaderSync();
        SceneManager.LoadScene(1);
    }
}
