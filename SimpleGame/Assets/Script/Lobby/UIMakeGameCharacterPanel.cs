using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ĳ���� ���� �г� ������Ʈ�� �����ϱ����� ����
/// </summary>
public class UIMakeGameCharacterPanel : MonoBehaviour
{
    public Button createGameCharacterButton;
    public InputField nicknameInputField;
    public GameObject makeGameCharacterPanel;

    public string getNicknameInputField_Text()
    {
        return nicknameInputField.text;
    }
}
