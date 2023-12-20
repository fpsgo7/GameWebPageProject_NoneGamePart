using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로비 스크립트 로비의 전체적인 동작을 관리한다.
/// </summary>
public class LobbyScript : MonoBehaviour
{
    public UILoginPanel loginPanel;
    public UIMakeGameCharacterPanel makeGameCharacterPanel;
    public UICharacterScreenPanel characterScreenPanel;
    public UICharactersScreenPanel charactersScreenPanel;

    public Accentication accentication = new Accentication();
    public GameCharacterMysql gameCharacterMysql = new GameCharacterMysql();
    public GameHighScoreMysql gameHighScoreMysql = new GameHighScoreMysql();


    private List<GameCharacterRankInfo> gameCharacterRankInfos;    // 게임 랭크 정보들을 담는 리스트
    private List<GameObject> gameCharacterRankObjects; // 게임 랭크 정보들을 위한 게임오브젝트 리스트 

    /// <summary>
    /// 로그인 버튼이 클릭된다.
    /// </summary>
    public void LoginButton_Click()
    {
        string email = loginPanel.getEmailInputField_Text();
        string password = loginPanel.getPasswordInputField_Text();
        if (accentication.Login(email, password)>0)
        {
            // 로그인 성공
            // 로그인 화면 닫기
            loginPanel.SetActive(false);
            // 캐릭터가 이미 있을 경우 메인 화면으로 넘어간다.
            if (gameCharacterMysql.GetMyGameCharacter(UserInfo.Email)>0)
            {
                ActiveGameCharacterPanel();
            }
            // 없을 경우에는 케릭터 생성 화면으로 이동한다.
            else
            {
                makeGameCharacterPanel.SetActive(true);
            }
            

        }
        else
        {
            // 로그인 실패
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
            ActiveGameCharacterPanel();
        }
        else
        {
            // 생성 실패
            makeGameCharacterPanel.createFaildPanel.SetActive(true);
        }
        
    }
    /// <summary>
    /// 게임캐릭터 패널 오픈
    /// </summary>
    private void ActiveGameCharacterPanel()
    {
        // 활성화할 패널의 값들을 수정한후 보여준다.
        // 게임 캐릭터 정보 가져오기
        getGameCharacterInfo();

        // 게임 랭크 구현하기 
        getGameRankInfo();
       
        
        // 패널 활성화
        characterScreenPanel.SetActive(true);
        charactersScreenPanel.SetActive(true);
    }
    /// <summary>
    /// 패널에 게임 캐릭터 정보 보여주기
    /// </summary>
    private void getGameCharacterInfo()
    {
        characterScreenPanel.userNicknameText.text = UserInfo.Nickname;
        characterScreenPanel.gameCharacterNicknameText.text = GameCharacterInfo.Nickname;
        characterScreenPanel.scoreText.text = string.Format("{0}", GameCharacterInfo.HighScore);
    }

    /// <summary>
    /// 패널에 게임 캐릭터 랭크 정보 보여주기
    /// </summary>
    private void getGameRankInfo()
    {
        // 게임 랭크 정보들을 담는 리스트
        gameCharacterRankInfos
           = gameHighScoreMysql.getGameCharacterRankInfos();
        // 게임 랭크 정보들을 위한 게임오브젝트 리스트 
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
        GameRankRefreshButton_Click();
    }

    /// <summary>
    /// 게임랭크 다시 업로드 버튼 클릭
    /// </summary>
    public void GameRankRefreshButton_Click()
    {
        // 게임 랭크 정보를 다시 가져온다.
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
            // 값 초기화
            rankItem.Init(
                gameCharacterRankInfos[i].Rank + "",
                gameCharacterRankInfos[i].Email,
                gameCharacterRankInfos[i].Nickname,
                gameCharacterRankInfos[i].HighScore + ""
                );
        }
    }
}
