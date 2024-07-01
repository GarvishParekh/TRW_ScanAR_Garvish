using UnityEngine;
using UnityEngine.SceneManagement;

public class VidePlayUiHandler : MonoBehaviour
{
    SfxManager sfxManager;

    private void Start()
    {
        sfxManager = SfxManager.instance;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        sfxManager.PlayClickSound();
    }
}
