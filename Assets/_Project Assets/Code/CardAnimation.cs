using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class CardAnimation : MonoBehaviour
{
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>(); 
        SpawningAnimation();
    }

    private void SpawningAnimation()
    {
        canvasGroup.alpha = 0f;   
        LeanTween.alphaCanvas(canvasGroup, 1, 0.25f).setEaseInOutSine().setDelay((transform.GetSiblingIndex() * 0.12f) + 0.35f);
    }
}
