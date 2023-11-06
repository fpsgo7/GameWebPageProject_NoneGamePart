using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  로그인 패널의 UI에 접근하기위한 공간
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
