// NOTE: AR foundation cannot re-scan without reseting XR settings 

using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TrackCell : MonoBehaviour
{
    SfxManager sfxManager;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videplayerData;
    [SerializeField] private BookUiIData bookUiData;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private Image bookCoverPage;
    [SerializeField] private string bookName;
    [SerializeField] private string trackName = string.Empty;
    [SerializeField] private TMP_Text trackNameText;

    CanvasGroup cardCG;


    [Header("<size=15>JSON PAYLOAD")]
    [SerializeField] private int bookId;
    [SerializeField] private int pageID;
    [SerializeField] private int trackID;

    [Space]
    [SerializeField] private string jsonPayload;

    #region json payload demo
    /*
"{\"bookId\": 1, \"pageNumber\": 1,\"trackId\": 1, \"path\": \"TRW_Reader_2/Page_1/Track_1/TRW_Reader_2_Page_1_V.m4v\"}";
*/ 
    #endregion

    [SerializeField] private string postURL;

    private void OnEnable()
    {
        ScanningUiHandler.LoadAudio += ChangeToMusicOnly;
        ScanningUiHandler.LoadVideo += ChangeToVideo;
    }

    private void OnDisable()
    {
        ScanningUiHandler.LoadAudio -= ChangeToMusicOnly;
        ScanningUiHandler.LoadVideo -= ChangeToVideo;
    }

    private void Start()
    {
        sfxManager = SfxManager.instance;

        cardCG = GetComponent<CanvasGroup>();
        cardCG.alpha = 0;
        SpawningAnimation();
    }

    public void SetInformation(string _bookName, int _bookId, int _PageId, int _trackId, string _url, Sprite _coverImage, string _trackName)
    {
        // for card ui
        bookName = _bookName;
        trackName = _trackName.Split('_')[1];   
        trackNameText.text = "Track: " + trackName;
        bookCoverPage.sprite = _coverImage;

        // json payload data 
        bookId = _bookId;
        pageID = _PageId;
        trackID = _trackId;

        #region Preparing json payload

        PayloadClass psClass = new PayloadClass();
        psClass.bookId = bookId;
        psClass.pageNumber = pageID;

        psClass.trackId = trackID;
        psClass.path = _url; 
        
        jsonPayload = JsonUtility.ToJson(psClass);
        #endregion

    }

    public void _PlayVideo()
    {
        ResetXRSettings();

        videplayerData.currentTrackName = trackName;
        sfxManager?.PlayClickSound();

        StartCoroutine(nameof(Upload));
    }

    private void SpawningAnimation()
    {
        LeanTween.alphaCanvas(cardCG, 1, (transform.GetSiblingIndex() + 2) * 0.25f).setEaseInOutSine().setDelay(0.5f);
    }

    // AR foundation cannot re-scan without reseting XR settings 
    private void ResetXRSettings()
    {
        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        xrManagerSettings.InitializeLoaderSync();
    }

    
    IEnumerator Upload()
    {
        using (UnityWebRequest request = UnityWebRequest.Post(postURL, jsonPayload.ToString(), "application/json"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string videoDirectLink = request.downloadHandler.text;
                Debug.Log("Video direct link: " + videoDirectLink);

                // preaparing video player ui
                videplayerData.videoDirectLink = videoDirectLink;
                videplayerData.currentBookName = bookName;
                videplayerData.currentTrackName = trackName;

                SceneManager.LoadScene(SceneData.VIDEOPLAYER);
            }
        }
    }


    [Space]
    [SerializeField] private Image backgroundColorImage;
    [SerializeField] private Image iconIndication;

    [Space]
    [SerializeField] private Color backgroundColorAudio;
    [SerializeField] private Color backgroundColorVideo;

    [Space]
    [SerializeField] private Sprite musicIcon;
    [SerializeField] private Sprite videoIcon;


    private void ChangeToMusicOnly()
    {
        backgroundColorImage.color = backgroundColorAudio;
        iconIndication.sprite = musicIcon;
    }

    private void ChangeToVideo()
    {
        backgroundColorImage.color = backgroundColorVideo;
        iconIndication.sprite = videoIcon;
    }
}

[Serializable]
public class PayloadClass
{
    public int bookId;
    public int pageNumber;
    public int trackId;
    public int trackName;
    public string path;
}