using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DummyApiCall : MonoBehaviour
{
    [SerializeField] private TMP_Text responseText;
    string postURL = "https://fjhvpt2agopjiry532bdnxmbye0disxo.lambda-url.ap-south-1.on.aws";
    string jsonPayloadPage1 = "{\"bookId\": 1, \"pageId\": 1,\"trackId\": 1, \"path\": \"TRW_Reader_2/Page_1/Track_1/TRW_Reader_2_Page_1_V.m4v\"}";

    string jsonPayloadPage2 = "{\"bookId\": 1, \"pageId\": 2,\"trackId\": 1, \"path\": \"TRW_Reader_2/Page_1/Track_1/TRW_Reader_2_Page_1_V.m4v\"}";

    string jsonPayloadPage3 = "{\"bookId\": 2, \"pageId\": 3,\"trackId\": 1, \"path\": \"TRW_Reader_2/Page_1/Track_1/TRW_Reader_2_Page_1_V.m4v\"}";
   
    public void _Reder1Page1()
        => StartCoroutine(nameof(Upload), jsonPayloadPage1);

    public void _Reder1Page2()
        => StartCoroutine(nameof(Upload), jsonPayloadPage2);

    public void _Reder2Page1()
        => StartCoroutine(nameof(Upload), jsonPayloadPage3);

    IEnumerator Upload(string payload)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(postURL, payload, "application/json"))
        {
            //www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string returnString = www.downloadHandler.text;
                Debug.Log("Form upload complete!" + returnString);
                responseText.text = "Response: " + returnString;
            }
        }
    }
}
