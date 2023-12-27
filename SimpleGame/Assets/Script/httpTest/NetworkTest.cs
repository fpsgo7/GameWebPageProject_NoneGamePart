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

        UnityWebRequest www = UnityWebRequest.Get(url);// get��� ��ü ����

        yield return www.SendWebRequest();// ��û �� ������ �ö� ���� ��ٸ�

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

        UnityWebRequest www = UnityWebRequest.Post(url, json);// post��� ��ü ����
        byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();// ��û �� ������ �ö� ���� ��ٸ�

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