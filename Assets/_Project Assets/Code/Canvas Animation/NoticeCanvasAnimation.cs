using UnityEngine;

public class NoticeCanvasAnimation : MonoBehaviour, ICanvasAnimation
{
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject mainHolder;
    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.alphaCanvas(bgCanvasGroup, 1, 0.2f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.moveLocalY(mainHolder, 0, 0.35f).setEaseInOutSine();
        });
    }

    public void ResetAnimation()
    {
        bgCanvasGroup.alpha = 0f;
        LeanTween.moveLocalY(mainHolder, -2000, 0);
    }
}
