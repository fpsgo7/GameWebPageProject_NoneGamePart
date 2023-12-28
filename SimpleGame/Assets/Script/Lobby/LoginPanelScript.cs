using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanelScript : MonoBehaviour
{
    private UILoginPanel loginPanel;

    void Start()
    {
        loginPanel = GameObject.Find("PanelObjectScript").GetComponent<UILoginPanel>();
    }

    public void LoginActive(bool isActive, bool isLogin)
    {
        if (isActive)
        {
            if (isLogin)
            {
                loginPanel.SetActive(false);
            }
            else
            {
                loginPanel.loginInfoErrorPanel.SetActive(true);
            }
        }
        else
        {
            loginPanel.loginServerErrorPanel.SetActive(true);
        }
    }
}
