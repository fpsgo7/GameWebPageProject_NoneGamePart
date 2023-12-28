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
/// <summary>
/// ���� ���� http �۾��� �����Ѵ�.
/// </summary>
public class UserHttpRequest : MonoBehaviour 
{
    private LoginPanelScript loginPanelScript;
    private MakeGameCharacterPanelScript makeGameCharacterPanelScript;
    private CharacterPanelScript characterPanelScript;
    private CharacterRankPanelScript characterRankPanelScript;

    private void Start()
    {
        loginPanelScript = GameObject.Find("LobbyScript").GetComponent<LoginPanelScript>();
        makeGameCharacterPanelScript = GameObject.Find("LobbyScript").GetComponent<MakeGameCharacterPanelScript>();
        characterPanelScript = GameObject.Find("LobbyScript").GetComponent<CharacterPanelScript>();
        characterRankPanelScript = GameObject.Find("LobbyScript").GetComponent<CharacterRankPanelScript>();
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

                    if (jObject["isGameCharacter"].ToString().Equals("true"))
                    {
                        GameCharacterInfo.Email = jObject["userEmail"].ToString();
                        GameCharacterInfo.Nickname = jObject["gameCharacterNickname"].ToString();
                        GameCharacterInfo.HighScore = (long)jObject["gameCharacterHighScore"];

                        loginPanelScript.LoginActive(true, true);
                        characterPanelScript.PanelActive(true);
                        characterRankPanelScript.PanelActive(true);
                    }
                    else
                    {
                        loginPanelScript.LoginActive(true, true);
                        makeGameCharacterPanelScript.PanelActive(true);
                    }
                }
                else if (jObject["isUser"].ToString().Equals("false"))
                {
                    Debug.Log("��й�ȣ �Ǵ� ���̵� �߸� �Ǿ����ϴ�.");
                    loginPanelScript.LoginActive(true, false);
                }
            }
            catch(Exception e)
            {
                Debug.Log("���� ������ ��Ȱ���� �ʽ��ϴ�.");
                loginPanelScript.LoginActive(false, false);
            }
        }));
    }
}
