using UnityEngine;

public class WarningCanvasAnimation : MonoBehaviour, ICanvasAnimation
{
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject mainHolder;
    public void PlayAnimation()
    {
        ResetAnimation();
        LeanTween.moveLocalY(mainHolder, 0, 0.35f).setEaseInOutSine();
        LeanTween.alphaCanvas(bgCanvasGroup, 1, 0.2f).setEaseInOutSine().setDelay(0.24f).setOnComplete(() =>
        {
            //LeanTween.scale(mainHolder, Vector3.one, 0.25f).setEaseInOutSine();
        });
    }

    public void ResetAnimation()
    {
        bgCanvasGroup.alpha = 0;
        LeanTween.moveLocalY(mainHolder, -2000, 0);
        //mainHolder.transform.localScale = Vector3.zero;
    }
}
