using UnityEngine;
using UnityEngine.SceneManagement;

public class ScanningUiHandler : MonoBehaviour
{
    UiManager uiManager;

    void Start()
    {
        uiManager = UiManager.instance;

        uiManager.OpenCanvas(CanvasName.SCANNING);
    }

    public void _BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void _ScanAgain()
    {
        SceneManager.LoadScene(1);
    }
}
