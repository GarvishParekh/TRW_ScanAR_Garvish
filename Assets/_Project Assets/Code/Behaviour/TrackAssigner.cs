using UnityEngine;


public enum BookID
{
    Reader1,
    Reader2
}
public class TrackAssigner : MonoBehaviour
{
    [SerializeField] private VideoPlayerData videoplayerData;
    TrackManager trackManager;
    [SerializeField] private TrackData trackData;
    [SerializeField] private BookID bookID;

    private void Start()
    {
        trackManager = TrackManager.instance;
        SetMyTrack();

        videoplayerData.currentBookName = videoplayerData.bookName[(int)bookID];
    }

    public void SetMyTrack()
    {
        trackManager._SetTrackData(trackData);
    }
}
