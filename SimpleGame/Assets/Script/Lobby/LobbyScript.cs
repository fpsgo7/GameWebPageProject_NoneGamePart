using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour
{
    public UILoginPanel loginPanel;
    public UIMakeGameCharacterPanel makeGameCharacterPanel;

    public Accentication accentication = new Accentication();
    public GameCharacterMysql gameCharacterMysql = new GameCharacterMysql();

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
            loginPanel.loginPanel.SetActive(false);
            // 캐릭터가 이미 있을 경우 메인 화면으로 넘어간다.
            if (gameCharacterMysql.GetMyGameCharacter(UserInfo.Email)>0)
            {
                Debug.Log("메인 화면으로");
            }
            // 없을 경우에는 케릭터 생성 화면으로 이동한다.
            else
            {
                makeGameCharacterPanel.makeGameCharacterPanel.SetActive(true);
            }
            

        }
        else
        {
            // 로그인 실패
            loginPanel.loginErrorPanel.SetActive(true);
        }

    }

    public void CreateCharacterButton_Click()
    {
        string nickname = makeGameCharacterPanel.getNicknameInputField_Text();
        if (gameCharacterMysql.createGameCharacter(UserInfo.Email, nickname) == 1)
            Debug.Log("메인 화면으로");
        
    }
}
