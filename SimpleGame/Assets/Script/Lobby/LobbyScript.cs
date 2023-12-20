 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ� ��ũ��Ʈ �κ��� ��ü���� ������ �����Ѵ�.
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


    private List<GameCharacterRankInfo> gameCharacterRankInfos;    // ���� ��ũ �������� ��� ����Ʈ
    private List<GameObject> gameCharacterRankObjects; // ���� ��ũ �������� ���� ���ӿ�����Ʈ ����Ʈ 

    /// <summary>
    /// �α��� ��ư�� Ŭ���ȴ�.
    /// </summary>
    public void LoginButton_Click()
    {
        string email = loginPanel.getEmailInputField_Text();
        string password = loginPanel.getPasswordInputField_Text();
        if (accentication.Login(email, password)>0)
        {
            // �α��� ����
            // �α��� ȭ�� �ݱ�
            loginPanel.SetActive(false);
            // ĳ���Ͱ� �̹� ���� ��� ���� ȭ������ �Ѿ��.
            if (gameCharacterMysql.GetMyGameCharacter(UserInfo.Email)>0)
            {
                ActiveGameCharacterPanel();
            }
            // ���� ��쿡�� �ɸ��� ���� ȭ������ �̵��Ѵ�.
            else
            {
                makeGameCharacterPanel.SetActive(true);
            }
            

        }
        else
        {
            // �α��� ����
            loginPanel.loginErrorPanel.SetActive(true);
        }

    }
    /// <summary>
    /// ĳ���� ���� ��ư Ŭ��
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
            // ���� ����
            makeGameCharacterPanel.createFaildPanel.SetActive(true);
        }
        
    }
    /// <summary>
    /// ����ĳ���� �г� ����
    /// </summary>
    private void ActiveGameCharacterPanel()
    {
        // Ȱ��ȭ�� �г��� ������ �������� �����ش�.
        // ���� ĳ���� ���� ��������
        getGameCharacterInfo();

        // ���� ��ũ �����ϱ� 
        getGameRankInfo();
       
        
        // �г� Ȱ��ȭ
        characterScreenPanel.SetActive(true);
        charactersScreenPanel.SetActive(true);
    }
    /// <summary>
    /// �гο� ���� ĳ���� ���� �����ֱ�
    /// </summary>
    private void getGameCharacterInfo()
    {
        characterScreenPanel.userNicknameText.text = UserInfo.Nickname;
        characterScreenPanel.gameCharacterNicknameText.text = GameCharacterInfo.Nickname;
        characterScreenPanel.scoreText.text = string.Format("{0}", GameCharacterInfo.HighScore);
    }

    /// <summary>
    /// �гο� ���� ĳ���� ��ũ ���� �����ֱ�
    /// </summary>
    private void getGameRankInfo()
    {
        // ���� ��ũ �������� ��� ����Ʈ
        gameCharacterRankInfos
           = gameHighScoreMysql.getGameCharacterRankInfos();
        // ���� ��ũ �������� ���� ���ӿ�����Ʈ ����Ʈ 
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
    /// ���� �Է� ��ư Ŭ��
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
        // ���� ĳ���� �ְ� ���� ����
        int score = gameCharacterMysql.updateGameCharacter_highScore(newScore, UserInfo.Email);
        if(score != -1)
        {
            GameCharacterInfo.HighScore = score;
        }
        // ���� ��ũ �ٽ� ���ε��ϱ�
        GameRankRefreshButton_Click();
    }

    /// <summary>
    /// ���ӷ�ũ �ٽ� ���ε� ��ư Ŭ��
    /// </summary>
    public void GameRankRefreshButton_Click()
    {
        // ���� ��ũ ������ �ٽ� �����´�.
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
            // �� �ʱ�ȭ
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
