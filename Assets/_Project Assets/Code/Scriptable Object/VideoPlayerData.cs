using UnityEngine;

[CreateAssetMenu(fileName = "Videoplayer Data", menuName = "Scriptable/Videoplayer Data")]
public class VideoPlayerData : ScriptableObject
{
    public string videoDirectLink;
    public string currentBookName;
    public string currentTrackName;

    public string[] bookName;
}