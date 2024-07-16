using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using Unity.Profiling;
using System;


/// <summary>
/// A progress bar for VideoPlayer
/// </summary>
[RequireComponent(typeof(Image), typeof(RectTransform))]
public class VideoPlayerProgress : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private AppStatus appStatus;

    [Space]
    [SerializeField] private Image pauseButton;
    [SerializeField] private Image fullscreenButton;
    [SerializeField] private Image loadingImage;
    [SerializeField] private TMP_Text videoSeekTime;

    [Space]
    [SerializeField] private CanvasGroup playbackUI;
    [SerializeField] private GameObject buttonUI;
    [SerializeField] private GameObject videoName;
    [SerializeField] private GameObject halfScreenTouch;
    /// <summary>
    /// Is seeking through the video enabled?
    /// </summary>
    public bool SeekingEnabled;

    /// <summary>
    /// The VideoPlayer to synchronize with
    /// </summary>
    public VideoPlayer videoPlayer;

    Image m_PlaybackProgress;
    RectTransform m_RectTransform;

    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_PlaybackProgress = GetComponent<Image>();

        if (m_PlaybackProgress.sprite == null)
        {
            var texture = Texture2D.whiteTexture;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 100);
            m_PlaybackProgress.sprite = sprite;
        }
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            m_PlaybackProgress.fillAmount =
                (float)(videoPlayer.length > 0 ? videoPlayer.time / videoPlayer.length : 0);
            loadingImage.gameObject.SetActive(false);
        }
        else
        {
            switch (appStatus.videoStatus)
            {
                case VideoStatus.PLAY:
                    loadingImage.gameObject.SetActive(true);
                    break;
            }
        }
        videoSeekTime.text = TimeSpan.FromSeconds((int)videoPlayer.time) +  " / " + TimeSpan.FromSeconds((int)videoPlayer.length);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Seek(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Seek(Input.mousePosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        videoPlayer.Pause();
        ChangePauseStatus(VideoStatus.PAUSE);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        videoPlayer.Play();
        ChangePauseStatus(VideoStatus.PLAY);
    }

    void Seek(Vector2 cursorPosition)
    {
        if (!SeekingEnabled || !videoPlayer.canSetTime)
            return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_RectTransform, cursorPosition, null, out var localPoint))
            return;

        var rect = m_RectTransform.rect;
        var progress = (localPoint.x - rect.x)  / rect.width;

        videoPlayer.time = videoPlayer.length * progress;
        m_PlaybackProgress.fillAmount = progress;
    }

    public void ChangeToLandscape()
    {
        switch (appStatus.screenStatus)
        {
            case ScreenStatus.PORTRAITE:
                ChangeScreenOrientation(ScreenStatus.LANDSCAPE);
                break;
            case ScreenStatus.LANDSCAPE:
                ChangeScreenOrientation(ScreenStatus.PORTRAITE);
                break;
        }
    }

    public void ChangeScreenOrientation(ScreenStatus _screenOrientation)
    {
        appStatus.screenStatus = _screenOrientation;
        switch (appStatus.screenStatus)
        {
            case ScreenStatus.PORTRAITE:
                fullscreenButton.sprite = appStatus.fullscreenSprite;
                Screen.orientation = ScreenOrientation.Portrait;
                appStatus.screenStatus = ScreenStatus.PORTRAITE;
                break;
            case ScreenStatus.LANDSCAPE:
                fullscreenButton.sprite = appStatus.minimizeSprite;
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                appStatus.screenStatus = ScreenStatus.LANDSCAPE;
                break;
        }
    }

    public void _PauseButton()
    {
        switch (appStatus.videoStatus)
        {
            case VideoStatus.PLAY:
                appStatus.videoStatus = VideoStatus.PAUSE;
                break;
            case VideoStatus.PAUSE:
                appStatus.videoStatus = VideoStatus.PLAY;
                break;
        }

        ChangePauseStatus(appStatus.videoStatus);
    }

    public void ChangePauseStatus(VideoStatus videoStatus)
    {
        appStatus.videoStatus = videoStatus;
        switch (videoStatus)
        {
            case VideoStatus.PLAY:
                pauseButton.sprite = appStatus.pauseSprite;
                videoPlayer.Play();
                break;
            case VideoStatus.PAUSE:
                pauseButton.sprite = appStatus.playSprite;
                videoPlayer.Pause();
                break;
        }
    }

    public void _ScreenTap()
    {
        switch (appStatus.playbackInfo)
        {
            case PlaybackInfo.SHOW:
                ChangePlaybackInfo(PlaybackInfo.HIDE);
                break;
            case PlaybackInfo.HIDE:
                ChangePlaybackInfo(PlaybackInfo.SHOW);
                break;
        }
    }

    private void ChangePlaybackInfo(PlaybackInfo playbackInfo)
    {
        appStatus.playbackInfo = playbackInfo;

        switch (playbackInfo)
        {
            case PlaybackInfo.SHOW:
                videoName.SetActive(true);
                buttonUI.SetActive(true);
                playbackUI.alpha = 1.0f;

                break;
            case PlaybackInfo.HIDE:
                playbackUI.alpha = 0.4f;
                buttonUI.SetActive(false);
                videoName.SetActive(false);

                break;
        }
    }
}
