using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 캐릭터 생성창 관리
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
