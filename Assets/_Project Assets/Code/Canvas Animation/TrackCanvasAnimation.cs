using UnityEngine;

public class TrackCanvasAnimation : MonoBehaviour, ICanvasAnimation
{
    [SerializeField] private CanvasGroup trackGroup;
    [SerializeField] private GameObject footerAnimation;
    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.alphaCanvas(trackGroup, 1, 0.35f).setEaseInOutSine();
        LeanTween.moveLocalY(footerAnimation, 1, 0.35f).setEaseInOutSine();
    }

    public void ResetAnimation()
    {
        LeanTween.moveLocalY(footerAnimation, -200f, 0);
        trackGroup.alpha = 0;
    }
}
