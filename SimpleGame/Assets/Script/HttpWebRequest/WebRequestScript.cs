using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 코루틴 방식으로 요청해서 실행중 
/// </summary>
public static class WebRequestScript
{
    private const string FRONTURL = "http://localhost:8080";// local
    //private const string FRONTURL = "";

    ///// <summary>
    ///// 웹 요청 결과를 스트링으로 반환
    ///// </summary>
    ///// <param name="behindURL">사용 url</param>
    ///// <returns>요청 결과로오는 json 값 </returns>
    //public static string WebRequestGet(string behindURL)
    //{
    //    string URL = string.Format("{0}{1}", FRONTURL, behindURL);

    //    UnityWebRequest www = UnityWebRequest.Get(URL);// get방식 객체 생성

    //    www.SendWebRequest();

    //    if (www.error == null)
    //    {
    //        return www.downloadHandler.text;
    //    }
    //    else
    //    {
    //        return "ERROR";
    //    }
    //}

    ///// <summary>
    ///// 웹 post 요청
    ///// </summary>
    ///// <param name="behindURL">사용할 url</param>
    ///// <param name="json">보낼 정보 json형 자료</param>
    ///// <returns>성공시 json 값, 실패시 "ERROR"</returns>
    //public static string WebRequestPost(string behindURL, string json)
    //{
    //    string URL = string.Format("{0}{1}", FRONTURL, behindURL);

    //    UnityWebRequest www = UnityWebRequest.Post(URL, json);// post방식 객체 생성
    //    byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
    //    www.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //    www.SetRequestHeader("Content-Type", "application/json");

    //    www.SendWebRequest();

    //    if (www.error == null)
    //    {
    //        Debug.Log(www.downloadHandler.text);
    //        return www.downloadHandler.text;
    //    }
    //    else
    //    {
    //        return "ERROR";
    //    }

    //}

    /// <summary>
    /// 코루틴 사용 웹 get 요청
    /// </summary>
    /// <param name="behindURL"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator WebRequestGetIE(string behindURL, Action<string> callback)
    {
        string URL = string.Format("{0}{1}", FRONTURL, behindURL);

        UnityWebRequest www = UnityWebRequest.Get(URL);// get방식 객체 생성

        yield return www.SendWebRequest();// 요청 후 응답이 올때 까지 기다림

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
    /// 코루틴 사용 웹 Post 요청
    /// </summary>
    /// <param name="behindURL"></param>
    /// <param name="json"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator WebRequestPostIE(string behindURL, string json, Action<string> callback)
    {
        string URL = string.Format("{0}{1}", FRONTURL, behindURL);

        UnityWebRequest www = UnityWebRequest.Post(URL, json);// post방식 객체 생성
        byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();// 요청 후 응답이 올때 까지 기다림

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