using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ĳ���� ����â ����
/// </summary>
public class MakeGameCharacterPanelScript : MonoBehaviour
{
    private UIMakeGameCharacterPanel uIMakeGameCharacterPanel;
   
    void Start()
    {
        uIMakeGameCharacterPanel = GameObject.Find("PanelObjectScript").GetComponent<UIMakeGameCharacterPanel>();
    }

    public void PanelActive(bool isBool)
    {
        uIMakeGameCharacterPanel.SetActive(isBool);
    }

    public void CreateFail(bool isBool)
    {
        uIMakeGameCharacterPanel.createFaildPanel.SetActive(isBool);
    }

}
