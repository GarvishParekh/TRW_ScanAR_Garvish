using UnityEngine;


public enum BookID
{
    Trw_Yellow,
    Trw_Red,
    Trw_Blue,
    Trw_Green,
    Trw_Reader_1,
    Trw_Reader_2
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
        //Yellow_1.3
    }
}
