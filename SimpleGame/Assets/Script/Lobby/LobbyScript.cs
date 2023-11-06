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
            loginPanel.loginPanel.SetActive(false);
            // ĳ���Ͱ� �̹� ���� ��� ���� ȭ������ �Ѿ��.
            if (gameCharacterMysql.GetMyGameCharacter(UserInfo.Email)>0)
            {
                Debug.Log("���� ȭ������");
            }
            // ���� ��쿡�� �ɸ��� ���� ȭ������ �̵��Ѵ�.
            else
            {
                makeGameCharacterPanel.makeGameCharacterPanel.SetActive(true);
            }
            

        }
        else
        {
            // �α��� ����
            loginPanel.loginErrorPanel.SetActive(true);
        }

    }

    public void CreateCharacterButton_Click()
    {
        string nickname = makeGameCharacterPanel.getNicknameInputField_Text();
        if (gameCharacterMysql.createGameCharacter(UserInfo.Email, nickname) == 1)
            Debug.Log("���� ȭ������");
        
    }
}
