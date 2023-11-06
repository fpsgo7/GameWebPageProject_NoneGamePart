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
    public GameObject loginErrorPanel;
    public GameObject loginPanel;

    public string getEmailInputField_Text()
    {
        return emailInpuField.text;
    }

    public string getPasswordInputField_Text()
    {
        return passwordInputField.text;
    }
}
