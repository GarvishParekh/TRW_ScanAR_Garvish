using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CanvasName
{
    MAIN_MENU,
    WARNING,
    NOTICE,
    UPDATE_AVAILABLE
}

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField] private List<CanvasCell> canvasCells = new List<CanvasCell>();

    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OpenCanvas(CanvasName _canvasName)
    {
        foreach (CanvasCell canvasCell in canvasCells)
        {
            if (_canvasName == canvasCell.GetCanvasName())
            {
                canvasCell.OpenCanvas();
            }
            else
            {
                canvasCell.CloseCanvas();
            }
        }
    }

    public void OpenPopup(CanvasName _canvasName)
    {
        foreach (CanvasCell canvasCell in canvasCells)
        {
            if (_canvasName == canvasCell.GetCanvasName())
            {
                canvasCell.OpenCanvas();
            }
        }
    }

    public void ClosePopup(CanvasName _canvasName)
    {
        foreach (CanvasCell canvasCell in canvasCells)
        {
            if (_canvasName == canvasCell.GetCanvasName())
            {
                canvasCell.CloseCanvas();
            }
        }
    }
}
