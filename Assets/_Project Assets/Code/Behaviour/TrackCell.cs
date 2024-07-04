using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackCell : MonoBehaviour
{
    SfxManager sfxManager;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videplayerData;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private TMP_Text trackNameText;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private string trackName = string.Empty;
    [SerializeField] private int trackIndex = 0;
    [SerializeField] private string url;

    private void Start()
    {
        sfxManager = SfxManager.instance;
    }

    public void SetInformation(string _trackName, string _url)
    {
        trackName = _trackName;
        trackNameText.text = trackName;
        url = _url;
    }

    public void _PlayVideo()
    {
        ResetXRSettings();

        videplayerData.currentTrackName = trackName;
        sfxManager?.PlayClickSound();
        SceneManager.LoadScene(SceneData.VIDEOPLAYER);

    }

    private void ResetXRSettings()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        xrManagerSettings.InitializeLoaderSync();
    }
}
