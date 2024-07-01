using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimation : MonoBehaviour, ICanvasAnimation
{
    [SerializeField] private GameObject HeaderObject;
    [SerializeField] private GameObject footerObject;
    [SerializeField] private CanvasGroup bookCanvasGroup;

    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.moveLocalY(HeaderObject, -130, 0.5f).setEaseInOutSine().setDelay(0.1f).setDelay(0.5f);
        LeanTween.moveLocalY(footerObject, 130, 0.5f).setEaseInOutSine().setDelay(0.2f).setDelay(0.5f);
        LeanTween.alphaCanvas(bookCanvasGroup, 1, 0.5f).setEaseInOutSine().setDelay(0.24f).setDelay(0.5f);
    }

    public void ResetAnimation()
    {
        LeanTween.moveLocalY(HeaderObject, 300, 0);
        LeanTween.moveLocalY(footerObject, -300, 0);
        bookCanvasGroup.alpha = 0;
    }
}
