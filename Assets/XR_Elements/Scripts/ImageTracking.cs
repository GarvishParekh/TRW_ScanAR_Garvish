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
    [SerializeField] private TMP_Text pageNumberText;

    [Header("<size=15>TRACK DATA")]
    [SerializeField] private string playLoadUrl;
    [SerializeField] private BookData bookData = new BookData();
    [SerializeField] private TrackCell trackCell;
    [SerializeField] private Transform trackCellSpawnPlace;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videoPlayerData;
    [SerializeField] private BookUiIData bookUiIData;

    [Space]
    private ARTrackedImageManager trackedImages;
    [SerializeField] private List<GameObject> ArPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> ArObjects = new List<GameObject>();

    public string bookName;
    public int bookPageNumber;
    public int selectedBookIndex = 0;

    bool found = false;

    private void Awake()
    {
        selectedBookIndex = PlayerPrefs.GetInt(ConstantKeys.SELECTEDBOOK);
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
        {
            Debug.Log ("Image already tracked !");
            return;
        }

        foreach (var trackedImage in args.added)
        {
            string trackedImageName = trackedImage.referenceImage.name;
            Debug.Log ("Book name: " + trackedImageName);
            
            // generating bookname, and bookpage number
            SplitBookName(trackedImageName);

            #region Recognized book name
            
            switch (bookName)
            {
                case "TRW_Starter":
                    if (selectedBookIndex == 1)
                    {
                        bookData = jsonConverter.data.book_data[0];
                        found = true;
                    }
                    else
                        return;
                    break;
                case "TRW_1":
                    if (selectedBookIndex == 2)
                    {
                        bookData = jsonConverter.data.book_data[1];
                        found = true;
                    }
                    else
                        return;
                    break;
                case "TRW_2":
                    if (selectedBookIndex == 3)
                    {
                        bookData = jsonConverter.data.book_data[2];
                        found = true;
                    }
                    else
                        return;
                    break;
                case "TRW_3":
                    if (selectedBookIndex == 4)
                    {
                        bookData = jsonConverter.data.book_data[3];
                        found = true;
                    }
                    else
                        return;
                    break;
                case "TRW_Reader_1":
                    if (selectedBookIndex == 5)
                    {
                        bookData = jsonConverter.data.book_data[4];
                        found = true;
                    }
                    else
                        return;
                    break;
                case "TRW_Reader_2":
                    if (selectedBookIndex == 6)
                    {
                        bookData = jsonConverter.data.book_data[5];
                        found = true;
                    }
                    else
                        return;
                    break;
            }
            
            uiManager.OpenCanvas(CanvasName.TACK_LIST);

            // track list ui 
            bookNameText.text = bookUiIData.trwBooks[bookData.bookId - 1].bookDisplayName;
            #endregion

            #region Recognize page name and generate tracks 

            int trackCount = 0;
            foreach (var pages in bookData.page)
            {
                if (bookPageNumber == pages.pageNumber)
                {
                    // track list ui 
                    string pageId = pages.pageName;
                    pageNumberText.text = pageId.Replace("_", ": ");

                    // for generating pages
                    trackCount = pages.track.Length;
                    for (int i = 0; i < trackCount; i++)
                    {
                        GenerateTrack
                        (
                            #region Card information
                            // for display
                            bookData.bookName,
                            // for payload url
                            bookData.bookId,
                            // to recognize page number
                            pages.pageNumber,
                            // unique ID for json payload
                            pages.track[i].trackId,
                            // lamda URL for json payload
                            pages.track[i].url,
                            // book cover for ui display 
                            bookUiIData.trwBooks[bookData.bookId - 1].bookCoverSprite,
                            // track name for ui display
                            pages.track[i].trackName 
                        #endregion
                        );
                    }
                }
            }
            #endregion
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
        bookPageNumber = int.Parse(secondPart);
    }

    private void GenerateTrack(string bookName,int _bookId, int _pageId, int trackid, string payloadURL, Sprite _coverImage, string _trackName)
    {
        Debug.Log("Generate track");
        TrackCell generatedCell =  Instantiate(trackCell, trackCellSpawnPlace);

        // bookname, pageid, tackid, trackname, payloadurl
        generatedCell.SetInformation(bookName, _bookId, _pageId, trackid, payloadURL, _coverImage, _trackName);
    }
}


