using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class JsonTestClass
{
    public string test;

    public JsonTestClass(string test)
    {
        this.test = test;
    }
}
public class NetworkTest : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(UnityWebRequestGet());
        StartCoroutine(UnityWebRequestPost());
    }

    IEnumerator UnityWebRequestGet()
    {
        string url = "http://localhost:8080/game/test";

        UnityWebRequest www = UnityWebRequest.Get(url);// get방식 객체 생성

        yield return www.SendWebRequest();// 요청 후 응답이 올때 까지 기다림

        if(www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }

    IEnumerator UnityWebRequestPost()
    {
        JsonTestClass jsonTestClass = new JsonTestClass("test");
        string json = JsonUtility.ToJson(jsonTestClass);
        Debug.Log(json);

        string url = "http://localhost:8080/game/test";

        UnityWebRequest www = UnityWebRequest.Post(url, json);// post방식 객체 생성
        byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();// 요청 후 응답이 올때 까지 기다림

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("ERROR");
        }

    }
}