using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ĳ���͵��� ������ �����ֱ����� �г��� ������Ʈ�� �������ִ�
/// ��ũ��Ʈ
/// </summary>
public class UICharactersScreenPanel : MonoBehaviour
{
    public GameObject charactersScreenPanel;

    public void SetActive(bool isBool)
    {
        charactersScreenPanel.SetActive(isBool);
    }
}
