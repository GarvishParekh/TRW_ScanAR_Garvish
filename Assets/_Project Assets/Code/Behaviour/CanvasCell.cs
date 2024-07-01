using UnityEngine;

[RequireComponent (typeof(CanvasGroup))] 
public class CanvasCell : MonoBehaviour
{
    ICanvasAnimation anime;
    CanvasGroup canvasGroup;
    [SerializeField] private CanvasName myCanvasName;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        anime = GetComponent<ICanvasAnimation>();
    }

    public CanvasName GetCanvasName()
    {
        return myCanvasName;
    }

    public void OpenCanvas()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        anime?.PlayAnimation();
    }

    public void CloseCanvas()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
