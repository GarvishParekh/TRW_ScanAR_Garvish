using UnityEngine;

public enum VideoPlayerType
{
    AUDIO,
    VIDEO
}
[CreateAssetMenu(fileName = "Videoplayer Data", menuName = "Scriptable/Videoplayer Data")]
public class VideoPlayerData : ScriptableObject
{
    public VideoPlayerType videoPlayerType;

    [Header ("<size=15>UI")]
    public string videoDirectLink;
    public string currentBookName;
    public string currentTrackName;

    [Space]
    public string[] bookName;
}