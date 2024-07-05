using TMPro;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    UiManager uiManager;
    SfxManager sfxManager;
    jsonConverter jsonConverter;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private TMP_Text trackedNameText;
    [SerializeField] private TMP_Text firstLastText;

    [Header("<size=15>TRACK DATA")]
    [SerializeField] private TracksData[] trackData;
    [SerializeField] private TrackCell trackCell;
    [SerializeField] private Transform trackCellSpawnPlace;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videoPlayerData;

    [Space]
    private ARTrackedImageManager trackedImages;
    [SerializeField] private List<GameObject> ArPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> ArObjects = new List<GameObject>();

    public string bookName;
    public float bookPageNumber;

    bool found = false;

    private void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    private void Start()
        => GetSingletonScripts();

    private void GetSingletonScripts()
    {
        uiManager = UiManager.instance;
        sfxManager = SfxManager.instance;
        jsonConverter = jsonConverter.instance;
    }

    private void OnEnable()
        => trackedImages.trackedImagesChanged += OnTackedImageChanges;

    private void OnDisable()
        => trackedImages.trackedImagesChanged -= OnTackedImageChanges;

    private void OnTackedImageChanges(ARTrackedImagesChangedEventArgs args)
    {
        if (found)
            return;

        foreach (var trackedImage in args.added)
        {
            string trackedImageName = trackedImage.referenceImage.name;
            Debug.Log ("Book name: " + trackedImageName);
            
            SplitBookName(trackedImageName);
            if (jsonConverter != null)
            {
                trackData = jsonConverter.FetchTracks(bookName, bookPageNumber);
                found = true;

                for (int i = 0; i < trackData.Length; i++)
                {
                    string name = "Track " + trackData[i].trackNumber.ToString();
                    GenerateTrack(trackData[i].url, name);
                }
            }
            else if (jsonConverter == null)
            {
                Debug.Log("json converter not found");
            }
        }
    }

    private void SplitBookName(string trackedImageName)
    {
        // split book name and book number from tracked image name
        string[] parts = trackedImageName.Split("_Page_");
        string firstPart = parts[0];    
        string secondPart = parts[1];


        // ui update for testing
        trackedNameText.text = trackedImageName;
        firstLastText.text = " Part one: " + firstPart + " \n Part two: " + secondPart;

        // get book name and page number to find track data from json
        bookName = firstPart;
        bookPageNumber = float.Parse(secondPart);
    }

    private void GenerateTrack(string trackUrl, string trackName)
    {
        TrackCell generatedCell =  Instantiate(trackCell, trackCellSpawnPlace);
        generatedCell.SetInformation(trackName, trackUrl);
    }
}


