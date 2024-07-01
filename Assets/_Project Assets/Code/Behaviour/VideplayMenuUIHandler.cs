using UnityEngine;
using UnityEngine.SceneManagement;

public class VideplayMenuUIHandler : MonoBehaviour
{
    [SerializeField] private VideoPlayerProgress videoPlayerprogress;
    public void BackButton()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        videoPlayerprogress.ChangeScreenOrientation(ScreenStatus.PORTRAITE);
        Invoke(nameof(BackToMainMenu), 0.3f);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
