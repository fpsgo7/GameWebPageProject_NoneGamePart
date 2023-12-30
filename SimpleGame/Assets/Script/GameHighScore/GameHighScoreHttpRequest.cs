using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpdateGameHighScore
{
    public string email;
    public long newScore;

    public UpdateGameHighScore(string email, long newScore)
    {
        this.email = email;
        this.newScore = newScore;
    }
}
public class GameHighScoreHttpRequest : MonoBehaviour
{
    private List<GameCharacterRankInfo> gameCharacterRankInfos = new List<GameCharacterRankInfo>();
    private CharacterRankPanelScript characterRankPanelScript;
    private CharacterPanelScript characterPanelScript;
    void Start()
    {
        characterRankPanelScript = GameObject.Find("LobbyScript").GetComponent<CharacterRankPanelScript>();
        characterPanelScript = GameObject.Find("LobbyScript").GetComponent<CharacterPanelScript>();
    }

    public void GetGameRank()
    {

        // ȸ�� ������ ĳ���� ������ ������ 
        // ���� Ŭ������ �� �����Ѵ�.
        StartCoroutine(WebRequestScript.WebRequestGetIE("/game/gameHighScore", (answer) =>
        {
            try
            {
                gameCharacterRankInfos.Clear();
                JArray jArray = JArray.Parse(answer);
                for (int i = 0; i < jArray.Count; i++)
                {
                    GameCharacterRankInfo gameCharacterRankInfo = new GameCharacterRankInfo();
                    gameCharacterRankInfo.Rank = i + 1;
                    gameCharacterRankInfo.Email = jArray[i]["email"].ToString();
                    gameCharacterRankInfo.Nickname = jArray[i]["gameCharacterNickname"].ToString();
                    gameCharacterRankInfo.HighScore = (long)jArray[i]["highScore"];
                    gameCharacterRankInfo.LastedTime = (DateTime)jArray[i]["lastedTime"];
                    gameCharacterRankInfos.Add(gameCharacterRankInfo);
                }
                characterRankPanelScript.PanelActive(true, gameCharacterRankInfos);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);

            }
        }));
    }

    public void SetGameHighScore(long newScore)
    {
        UpdateGameHighScore updateGameHighScore = new UpdateGameHighScore(UserInfo.Email, newScore);
        string json = JsonUtility.ToJson(updateGameHighScore);

        StartCoroutine(WebRequestScript.WebRequestPostIE("/game/gameHighScore",json, (answer) =>
        {
            try
            {
                JObject jObject = JObject.Parse(answer);

                if (jObject["highScore"] != null)
                {
                    Debug.Log("���� ĳ���� ���� ������Ʈ�� �����Ͽ����ϴ�..");
                    GameCharacterInfo.HighScore = (long)jObject["highScore"];
                    characterPanelScript.PanelActive(true);
                    GetGameRank();
                }
                else
                {
                    Debug.Log("���� ĳ���� ���� ������Ʈ�� �����Ͽ����ϴ�.");
                }
            }
            catch (Exception e)
            {
                Debug.Log("���� ��ſ� �����Ͽ����ϴ�.");

            }
        }));
    }
}
