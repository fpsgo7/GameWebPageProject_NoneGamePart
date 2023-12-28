using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

[System.Serializable]
public class LoginInfo
{
    public string email;
    public string password;

    public LoginInfo(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}

public class UserHttpRequest : MonoBehaviour 
{
    private LoginPanelScript loginScript;

    private void Start()
    {
        loginScript = GameObject.Find("LobbyScript").GetComponent<LoginPanelScript>();
    }

    public void login(string email, string password)
    {
        LoginInfo loginInfo = new LoginInfo(email, password);
        string json = JsonUtility.ToJson(loginInfo);

        // 회원 정보와 캐릭터 정보를 가져와 
        // 정적 클래스에 각 저장한다.
        StartCoroutine(WebRequestScript.WebRequestPostIE("/game/user", json, (answer) =>
        {
            try
            {
                JObject jObject = JObject.Parse(answer);
                if (jObject["isUser"].ToString().Equals("true"))
                {
                    UserInfo.Email = jObject["userEmail"].ToString();
                    UserInfo.Nickname = jObject["userNickname"].ToString();
                    Debug.Log(
                    UserInfo.Email + "\n" +
                    UserInfo.Nickname);
                    if (jObject["isGameCharacter"].ToString().Equals("true"))
                    {
                        GameCharacterInfo.Email = jObject["userEmail"].ToString();
                        GameCharacterInfo.Nickname = jObject["gameCharacterNickname"].ToString();
                        GameCharacterInfo.HighScore = (long)jObject["gameCharacterHighScore"];


                        Debug.Log(
                        GameCharacterInfo.Email + "\n" +
                        GameCharacterInfo.Nickname + "\n" +
                         GameCharacterInfo.HighScore);
                        loginScript.LoginActive(true, true);
                        // 게임 캐릭터 창으로 이동함
                    }
                    else
                    {
                        loginScript.LoginActive(true, true);
                        // 게임 캐릭터 생성창으로 이동함
                    }
                }
                else if (jObject["isUser"].ToString().Equals("false"))
                {
                    Debug.Log("비밀번호 또는 아이디가 잘못 되었습니다.");
                    loginScript.LoginActive(true, false);
                }
            }
            catch(Exception e)
            {
                Debug.Log("게임 접속이 원활하지 않습니다.");
                loginScript.LoginActive(false, false);
            }
        }));
    }
}
