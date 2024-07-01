using UnityEngine;

public class TrackAssigner : MonoBehaviour
{
    TrackManager trackManager;
    [SerializeField] private TrackData trackData;
    private void Start()
    {
        trackManager = TrackManager.instance;
        SetMyTrack();
    }

    public void SetMyTrack()
    {
        trackManager._SetTrackData(trackData);
    }
}
