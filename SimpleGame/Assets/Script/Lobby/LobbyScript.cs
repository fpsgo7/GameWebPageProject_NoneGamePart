 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour
{
    public UILoginPanel loginPanel;
    public UIMakeGameCharacterPanel makeGameCharacterPanel;
    public UICharacterScreenPanel characterScreenPanel;
    public UICharactersScreenPanel charactersScreenPanel;

    public Accentication accentication = new Accentication();
    public GameCharacterMysql gameCharacterMysql = new GameCharacterMysql();
    public GameHighScoreMysql gameHighScoreMysql = new GameHighScoreMysql();


    private List<GameCharacterRankInfo> gameCharacterRankInfos;    
    private List<GameObject> gameCharacterRankObjects; 

    /// <summary>
    /// 로그인 버튼 클릭
    /// </summary>
    public void LoginButton_Click()
    {
        string email = loginPanel.getEmailInputField_Text();
        string password = loginPanel.getPasswordInputField_Text();
        if (accentication.Login(email, password)>0)
        {

            loginPanel.SetActive(false);

            if (gameCharacterMysql.GetMyGameCharacter(UserInfo.Email)>0)
            {
                ActiveGameCharacterAndRankPanel();
            }
            else
            {
                makeGameCharacterPanel.SetActive(true);
            }
            

        }
        else
        {
            loginPanel.loginErrorPanel.SetActive(true);
        }

    }
    /// <summary>
    /// 캐릭터 생성 버튼 클릭
    /// </summary>
    public void CreateCharacterButton_Click()
    {
        string nickname = makeGameCharacterPanel.getNicknameInputField_Text();
        if (gameCharacterMysql.createGameCharacter(UserInfo.Email, nickname) == 1)
        {
            makeGameCharacterPanel.SetActive(false);
            ActiveGameCharacterAndRankPanel();
        }
        else
        {
            makeGameCharacterPanel.createFaildPanel.SetActive(true);
        }
        
    }
    /// <summary>
    /// 게임 캐릭터 패널 과 랭크 패널 활성화
    /// </summary>
    private void ActiveGameCharacterAndRankPanel()
    {
        
        getGameCharacterInfo();

        getGameRankInfo();
       
        characterScreenPanel.SetActive(true);
        charactersScreenPanel.SetActive(true);
    }
    /// <summary>
    /// 게임 캐릭터 정보 가져오기
    /// </summary>
    private void getGameCharacterInfo()
    {
        characterScreenPanel.userNicknameText.text = UserInfo.Nickname;
        characterScreenPanel.gameCharacterNicknameText.text = GameCharacterInfo.Nickname;
        characterScreenPanel.scoreText.text = string.Format("{0}", GameCharacterInfo.HighScore);
    }

    /// <summary>
    /// 게임 랭크 정보 가져오기
    /// </summary>
    private void getGameRankInfo()
    {
        gameCharacterRankInfos
           = gameHighScoreMysql.getGameCharacterRankInfos();
        gameCharacterRankObjects = new List<GameObject>();
        foreach (GameCharacterRankInfo gameCharacterRankInfo in gameCharacterRankInfos)
        {
            GameObject rankGameObject
                = Instantiate<GameObject>(charactersScreenPanel.gameCharacterRankPrefab, charactersScreenPanel.gridSetting.transform);
            gameCharacterRankObjects.Add(rankGameObject);
        }
        for (int i = 0; i < gameCharacterRankObjects.Count; i++)
        {
            UICharacterRankPanel rankItem = gameCharacterRankObjects[i].GetComponent<UICharacterRankPanel>();
            rankItem.Init(
                gameCharacterRankInfos[i].Rank + "",
                gameCharacterRankInfos[i].Email,
                gameCharacterRankInfos[i].Nickname,
                gameCharacterRankInfos[i].HighScore + ""
                );

        }
    }
    /// <summary>
    /// 점수 입력 버튼 클릭
    /// </summary>
    public void InputScoreButton_Click()
    {
        int newScore = int.Parse(characterScreenPanel.gameScoreInputField.text);
        if (gameHighScoreMysql.createGameHighScore(UserInfo.Email, GameCharacterInfo.Nickname, newScore) != 1)
        {
            if(gameHighScoreMysql.updateGameScore(UserInfo.Email, newScore) != 1)
            {
                characterScreenPanel.inputGameScoreFaildText.SetActive(true);
            }
        }
        int score = gameCharacterMysql.updateGameCharacter_highScore(newScore, UserInfo.Email);
        if(score != -1)
        {
            GameCharacterInfo.HighScore = score;
        }
        GameRankRefreshButton_Click();
    }

    /// <summary>
    /// 게임 랭크 리프레시 버튼 클릭
    /// </summary>
    public void GameRankRefreshButton_Click()
    {
        gameCharacterRankInfos
           = gameHighScoreMysql.getGameCharacterRankInfos();
        for (int i = 0; i < gameCharacterRankInfos.Count; i++)
        {
            if(i >= gameCharacterRankObjects.Count)
            {
                GameObject rankGameObject
                = Instantiate<GameObject>(charactersScreenPanel.gameCharacterRankPrefab, charactersScreenPanel.gridSetting.transform);
                    gameCharacterRankObjects.Add(rankGameObject);
            }
            UICharacterRankPanel rankItem = gameCharacterRankObjects[i].GetComponent<UICharacterRankPanel>();
            rankItem.Init(
                gameCharacterRankInfos[i].Rank + "",
                gameCharacterRankInfos[i].Email,
                gameCharacterRankInfos[i].Nickname,
                gameCharacterRankInfos[i].HighScore + ""
                );
        }

        characterScreenPanel.scoreText.text = string.Format("{0}", GameCharacterInfo.HighScore);
    }
}
