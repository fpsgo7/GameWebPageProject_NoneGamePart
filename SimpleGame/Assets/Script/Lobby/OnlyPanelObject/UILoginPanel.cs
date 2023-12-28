using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  �α��� �г��� UI�� �����ϱ����� ����
/// </summary>
public class UILoginPanel : MonoBehaviour
{
    public Button loginButton;
    public InputField emailInpuField;
    public InputField passwordInputField;
    public GameObject loginServerErrorPanel;
    public GameObject loginInfoErrorPanel;
    public GameObject loginPanel;

    public void SetActive(bool isBool)
    {
        loginPanel.SetActive(isBool);
    }
    public string getEmailInputField_Text()
    {
        return emailInpuField.text;
    }

    public string getPasswordInputField_Text()
    {
        return passwordInputField.text;
    }

    
}