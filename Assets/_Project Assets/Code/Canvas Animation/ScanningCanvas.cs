using UnityEngine;

public class ScanningCanvas : MonoBehaviour, ICanvasAnimation
{
    [SerializeField] private GameObject HeaderObject;
    [SerializeField] private GameObject scanner;

    public void PlayAnimation()
    {
        ResetAnimation();

        LeanTween.moveLocalY(HeaderObject, 0, 0.5f).setEaseInOutSine();
        LeanTween.moveY(scanner, -1500, 2f).setEaseInOutSine().setLoopCount(-1);
    }

    public void ResetAnimation()
    {
        LeanTween.moveLocalY(HeaderObject, -300, 0);
        LeanTween.moveLocalY(scanner, 1600, 0);
    }
}
