using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackManager : MonoBehaviour
{
    public static TrackManager instance;
    [Header ("<size=15>[SCRIPTABLE]")]
    [SerializeField] private TrackData trackData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private Transform cellSpawnPoint;

    [Header ("<size=15>[UI]")]
    [SerializeField] private TrackCell trackCell;
    [SerializeField] private TMP_Text bookNameText;

    private void Awake()
    {
        instance = this;
    }

    public void GenerateTracks()
    {
        int trackCount = trackData.tracks.Count;

        for (int i = 0; i < trackCount; i++)
        {
            TrackInfo selectedTrack = trackData.tracks[i];  
            TrackCell newTrackCell = Instantiate(trackCell, cellSpawnPoint);
            newTrackCell.SetInformation(selectedTrack.trackName, selectedTrack.url);
        }

        bookNameText.text = trackData.bookName;
    }

    public void _SetTrackData(TrackData _trackData)
    {
        trackData = _trackData;
        GenerateTracks();
    }

    public void _PlayVideo()
    {
        SceneManager.LoadScene(2);
    }
}
