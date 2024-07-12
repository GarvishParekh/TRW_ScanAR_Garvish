using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class TrackCell : MonoBehaviour
{
    SfxManager sfxManager;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videplayerData;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private TMP_Text trackNameText;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private string bookName;
    [SerializeField] private int bookId;
    [SerializeField] private int pageID;
    [SerializeField] private int trackID;
    [SerializeField] private string trackName = string.Empty;

    [SerializeField] private string jsonPayload;
    
    /*
    "{\"bookId\": 1, \"pageId\": 1,\"trackId\": 1, \"path\": \"TRW_Reader_2/Page_1/Track_1/TRW_Reader_2_Page_1_V.m4v\"}";
    */

    [SerializeField] private string postURL;

    private void Start()
    {
        sfxManager = SfxManager.instance;
    }

    public void SetInformation(string _bookName, int _bookId, int _PageId, int _trackId, string _url)
    {
        bookName = _bookName;
        bookId = _bookId;
        pageID = _PageId;
        trackID = _trackId;
        trackNameText.text = trackName;
        postURL = _url;

        PayloadClass psClass = new PayloadClass();
        psClass.bookId = bookId;
        psClass.pageId = pageID;
        psClass.trackId = trackID;
        psClass.url = postURL;

        jsonPayload = JsonUtility.ToJson(psClass);
    }
    public void _PlayVideo()
    {
        ResetXRSettings();

        videplayerData.currentTrackName = trackName;
        sfxManager?.PlayClickSound();
        
    }

    private void ResetXRSettings()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        xrManagerSettings.InitializeLoaderSync();
    }

    
    IEnumerator Upload()
    {
        using (UnityWebRequest request = UnityWebRequest.Post(postURL, jsonPayload, "application/json"))
        {
            //request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string videoDirectLink = request.downloadHandler.text;
                Debug.Log("Video direct link: " + videoDirectLink);
                videplayerData.videoDirectLink = videoDirectLink;   
                SceneManager.LoadScene(SceneData.VIDEOPLAYER);
            }
        }
    }
}

[Serializable]
public class PayloadClass
{
    public int bookId;
    public int pageId;
    public int trackId;
    public string url;
}