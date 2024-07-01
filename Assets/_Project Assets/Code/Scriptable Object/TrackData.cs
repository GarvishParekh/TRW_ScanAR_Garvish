using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "Track Data", menuName = "Scriptable / Track Data")]
public class TrackData : ScriptableObject
{
    public string bookName;
    public List <TrackInfo> tracks = new List <TrackInfo>();   
}

[System.Serializable]
public class TrackInfo
{
    public string trackName = string.Empty;
    public int trackIndex = 0;
    public string url;
}
