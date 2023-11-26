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

    public GameObject gridSetting;
    public GameObject gameCharacterRankPrefab;

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
        List<GameCharacterRankInfo> gameCharacterRankInfos
           = gameHighScoreMysql.getGameCharacterRankInfos();
        List<GameObject> gameCharacterRankObjects = new List<GameObject>();
        foreach (GameCharacterRankInfo gameCharacterRankInfo in gameCharacterRankInfos)
        {
            GameObject rankGameObject
                = Instantiate<GameObject>(gameCharacterRankPrefab, gridSetting.transform);
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
}
