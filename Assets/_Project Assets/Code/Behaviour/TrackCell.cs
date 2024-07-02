using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void _PlayVideo()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        SceneManager.LoadScene(2);
    }
}
