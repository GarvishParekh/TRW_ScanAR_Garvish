using System;
using UnityEngine;


public enum ScreenStatus
{
    PORTRAITE,
    LANDSCAPE
}

public enum VideoStatus
{
    PLAY,
    PAUSE
}

public enum PlaybackInfo
{
    SHOW,
    HIDE
}

[Serializable]
[CreateAssetMenu(fileName = "Application Status", menuName = "Scriptable/Application Status")]
public class AppStatus : ScriptableObject
{
    [Space]
    public ScreenStatus screenStatus;
    public VideoStatus videoStatus;
    public PlaybackInfo playbackInfo;

    public Sprite pauseSprite;
    public Sprite playSprite;

    [Space]
    public Sprite fullscreenSprite;
    public Sprite minimizeSprite;
}
