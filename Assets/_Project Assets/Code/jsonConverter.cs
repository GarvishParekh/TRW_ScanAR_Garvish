using TMPro;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class jsonConverter : MonoBehaviour
{
    public static jsonConverter instance;
    [Header("<size=15>USER INTERFACE")]
    [SerializeField] private TMP_Text informationText;

    [Header("<size=15>USER JSON")]
    [SerializeField] private string jsonURL;
    public TrwData data = new TrwData();
    [SerializeField] private string jsonString;

    private void Awake()
    {
        if (instance != null && instance == this)
        {
            Destroy(gameObject);    
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        informationText.text = "Please wait... \nChecking internet connection.....";
        StartCoroutine(checkInternetConnection((isConnected) =>
        {
            // handle connection status here
            if (isConnected)
            {
                StartCoroutine(nameof(FetchBookData), jsonURL);
            }
            else
            {
                informationText.text += "\nPlease check your internet connection!";
            }
        }));
    }

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");

        yield return request;
        if (request.error != null)
            action(false);
        else
            action(true);
    }

    IEnumerator FetchBookData(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            informationText.text += "\nFetching books";

            jsonString = uwr.downloadHandler.text;  
            data = JsonUtility.FromJson<TrwData>(jsonString);
            
            yield return new WaitForSeconds(2);
            informationText.text += "\nLoading music files";

            yield return new WaitForSeconds(1);

            informationText.text += "\nAll set";
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneData.MAINMENU);
        }
    }
}