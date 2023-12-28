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

        // ȸ�� ������ ĳ���� ������ ������ 
        // ���� Ŭ������ �� �����Ѵ�.
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
                        // ���� ĳ���� â���� �̵���
                    }
                    else
                    {
                        loginScript.LoginActive(true, true);
                        // ���� ĳ���� ����â���� �̵���
                    }
                }
                else if (jObject["isUser"].ToString().Equals("false"))
                {
                    Debug.Log("��й�ȣ �Ǵ� ���̵� �߸� �Ǿ����ϴ�.");
                    loginScript.LoginActive(true, false);
                }
            }
            catch(Exception e)
            {
                Debug.Log("���� ������ ��Ȱ���� �ʽ��ϴ�.");
                loginScript.LoginActive(false, false);
            }
        }));
    }
}
