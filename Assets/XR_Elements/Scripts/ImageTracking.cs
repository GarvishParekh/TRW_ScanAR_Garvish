using TMPro;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ImageTracking : MonoBehaviour
{
    UiManager uiManager;
    SfxManager sfxManager;
    jsonConverter jsonConverter;

    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private TMP_Text trackedNameText;
    [SerializeField] private TMP_Text firstLastText;
    [SerializeField] private TMP_Text bookNameText;

    [Header("<size=15>TRACK DATA")]
    [SerializeField] private string pageName;
    [SerializeField] private string playLoadUrl;
    [SerializeField] private BookData bookData = new BookData();
    [SerializeField] private TrackCell trackCell;
    [SerializeField] private Transform trackCellSpawnPlace;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videoPlayerData;

    [Space]
    private ARTrackedImageManager trackedImages;
    [SerializeField] private List<GameObject> ArPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> ArObjects = new List<GameObject>();

    public string bookName;
    public int bookPageNumber;

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
        uiManager.OpenCanvas(CanvasName.TACK_LIST);
        foreach (var trackedImage in args.added)
        {
            string trackedImageName = trackedImage.referenceImage.name;
            Debug.Log ("Book name: " + trackedImageName);
            
            // generating bookname, and bookpage number
            SplitBookName(trackedImageName);

            int trackCount = 0;
            
            switch (bookName)
            {
                case "TRW_Reader_1":
                    bookData = jsonConverter.data.book_data[0];
                    pageName = "Page " + bookPageNumber;
                    break;
                case "TRW_Reader_2":
                    bookData = jsonConverter.data.book_data[1];
                    pageName = "Page " + bookPageNumber;
                    break;
                default:
                    bookData = jsonConverter.data.book_data[0];
                    pageName = "Page " + bookPageNumber;
                    break;
            }
            foreach (var pages in bookData.page)
            {
                if (pageName == pages.pageName)
                {
                    trackCount = pages.track.Length;
                    for (int i = 0; i < trackCount; i++)
                    {
                        GenerateTrack
                            (
                                bookData.bookName,
                                bookData.bookId,
                                pages.pageId,
                                pages.track[i].trackId,
                                pages.track[i].url
                            );
                    }
                }
            }
        }
    }

    private void SplitBookName(string trackedImageName)
    {
        // split book name and book number from tracked image name
        string[] parts = trackedImageName.Split("_Page_");
        string firstPart = parts[0];    
        string secondPart = parts[1];

        // get book name and page number to find track data from json
        bookName = firstPart;
        bookNameText.text = bookName.Replace("_", " ");
        bookPageNumber = int.Parse(secondPart);
    }

    private void GenerateTrack(string bookName,int _bookId, int _pageId, int trackid, string payloadURL)
    {
        Debug.Log("Generate track");
        TrackCell generatedCell =  Instantiate(trackCell, trackCellSpawnPlace);

        // bookname, pageid, tackid, trackname, payloadurl
        generatedCell.SetInformation(bookName, _bookId, _pageId, trackid, payloadURL);
    }
}


