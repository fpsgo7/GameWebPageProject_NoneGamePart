 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour
{
    public UILoginPanel loginPanel;
    public UIMakeGameCharacterPanel makeGameCharacterPanel;
    public UICharacterScreenPanel characterScreenPanel;

    private UserHttpRequest userHttpRequest;
    private GameCharacterHttpRequest gameCharacterHttpRequest;
    private GameHighScoreHttpRequest gameHighScoreHttpRequest;

    private void Start()
    {
        userHttpRequest = GameObject.Find("WebRequestScript").GetComponent<UserHttpRequest>();
        gameCharacterHttpRequest = GameObject.Find("WebRequestScript").GetComponent<GameCharacterHttpRequest>();
        gameHighScoreHttpRequest = GameObject.Find("WebRequestScript").GetComponent<GameHighScoreHttpRequest>();
    }

    /// <summary>
    /// 로그인 버튼 클릭
    /// </summary>
    public void LoginButton_Click()
    {
        string email = loginPanel.getEmailInputField_Text();
        string password = loginPanel.getPasswordInputField_Text();
        userHttpRequest.login(email, password);
    }
    /// <summary>
    /// 캐릭터 생성 버튼 클릭
    /// </summary>
    public void CreateCharacterButton_Click()
    {
        string nickname = makeGameCharacterPanel.getNicknameInputField_Text();
        gameCharacterHttpRequest.CreateGameCharacter(UserInfo.Email, nickname);
    }

    /// <summary>
    /// 점수 입력 버튼 클릭
    /// </summary>
    public void InputScoreButton_Click()
    {
        int newScore = int.Parse(characterScreenPanel.gameScoreInputField.text);
        gameHighScoreHttpRequest.SetGameHighScore(newScore);
    }

    /// <summary>
    /// 게임 랭크 리프레시 버튼 클릭
    /// </summary>
    public void GameRankRefreshButton_Click()
    {
        gameHighScoreHttpRequest.GetGameRanks();
    }
}
