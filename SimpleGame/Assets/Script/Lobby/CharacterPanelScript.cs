using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelScript : MonoBehaviour
{
    private UICharacterScreenPanel uICharacterScreenPanel;

    void Start()
    {
        uICharacterScreenPanel = GameObject.Find("PanelObjectScript").GetComponent<UICharacterScreenPanel>();
    }

    public void PanelActive(bool isBool)
    {
        uICharacterScreenPanel.userNicknameText.text = UserInfo.Nickname;
        uICharacterScreenPanel.gameCharacterNicknameText.text = GameCharacterInfo.Nickname;
        uICharacterScreenPanel.scoreText.text = GameCharacterInfo.HighScore.ToString();
        uICharacterScreenPanel.SetActive(isBool);
    }
}
