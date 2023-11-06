using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ڽ��� ĳ���͸� �����ֱ����� 
/// �г��� ������Ʈ���� �����ϱ����� 
/// ��ũ��Ʈ
/// </summary>
public class UICharacterScreenPanel : MonoBehaviour
{
    public Text userNicknameText;
    public Text gameCharacterNicknameText;
    public Text scoreText;
    public GameObject characterScreenPanel;

    public void SetActive(bool isBool)
    {
        characterScreenPanel.SetActive(isBool);
    }
}
