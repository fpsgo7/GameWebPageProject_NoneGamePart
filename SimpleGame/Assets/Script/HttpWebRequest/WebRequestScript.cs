using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// �ڷ�ƾ ������� ��û�ؼ� ������ 
/// </summary>
public static class WebRequestScript
{
    private const string FRONTURL = "http://localhost:8080";// local
    //private const string FRONTURL = "";

    /// <summary>
    /// �ڷ�ƾ ��� �� get ��û
    /// </summary>
    /// <param name="behindURL"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator WebRequestGet(string behindURL, Action<string> callback)
    {
        string URL = string.Format("{0}{1}", FRONTURL, behindURL);

        UnityWebRequest www = UnityWebRequest.Get(URL);// get��� ��ü ����

        yield return www.SendWebRequest();// ��û �� ������ �ö� ���� ��ٸ�

        if (www.error == null)
        {
            callback(www.downloadHandler.text);
        }
        else
        {
            callback("ERROR");
        }
    }

    /// <summary>
    /// �ڷ�ƾ ��� �� Post ��û
    /// </summary>
    /// <param name="behindURL"></param>
    /// <param name="json"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator WebRequestPost(string behindURL, string json, Action<string> callback)
    {
        string URL = string.Format("{0}{1}", FRONTURL, behindURL);

        UnityWebRequest www = UnityWebRequest.Post(URL, json);// post��� ��ü ����
        byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();// ��û �� ������ �ö� ���� ��ٸ�

        if (www.error == null)
        {
            callback(www.downloadHandler.text);
        }
        else
        {
            callback("ERROR");
        }

    }
}