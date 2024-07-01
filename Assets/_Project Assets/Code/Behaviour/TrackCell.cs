using TMPro;
using UnityEngine;

public class TrackCell : MonoBehaviour
{
    [SerializeField] private TMP_Text trackNameText;
    [SerializeField] private string trackName = string.Empty;
    [SerializeField] private int trackIndex = 0;
    [SerializeField] private string url;

    public void SetInformation(string _trackName, string _url)
    {
        trackName = _trackName;
        trackNameText.text = trackName;
        url = _url;
    }
}
