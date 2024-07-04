using UnityEngine;

using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    UiManager uiManager;
    SfxManager sfxManager;

    [Header("<size=15>SCRIPTABLE")]
    [SerializeField] private VideoPlayerData videoPlayerData;

    [Space]
    private ARTrackedImageManager trackedImages;
    [SerializeField] private List<GameObject> ArPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> ArObjects = new List<GameObject>();

    bool found = false;

    private void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        sfxManager = SfxManager.instance;
    }

    private void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTackedImageChanges;
    }

    private void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTackedImageChanges;
    }

    private void OnTackedImageChanges(ARTrackedImagesChangedEventArgs args)
    {
        if (found)
        {
            return;
        }

        foreach (var tackedImage in args.added)
        {
            foreach (var arPrefabs in ArPrefabs)
            {
                if (tackedImage.referenceImage.name == arPrefabs.name)
                {
                    videoPlayerData.currentBookName = arPrefabs.name;   
                    var newPrefab = Instantiate(arPrefabs, tackedImage.transform);
                    ArObjects.Add(newPrefab);
                    found = true;

                    uiManager.OpenCanvas(CanvasName.TACK_LIST);
                    sfxManager?.PlayBookFoundSound();
                }
            }
        }

        foreach (var tackedImage in args.added)
        {
            foreach (var arObjects in ArObjects)
            {
                string tempName = arObjects.name + "(Clone)";
                if (tempName == trackedImages.name)
                {
                    gameObject.SetActive(tackedImage.trackingState == TrackingState.Tracking);
                }
            }
        }
    }
}


