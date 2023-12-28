using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRankPanelScript : MonoBehaviour
{
    private UICharacterRankScreenPanel uICharacterRankScreenPanel;

    void Start()
    {
        uICharacterRankScreenPanel = GameObject.Find("PanelObjectScript").GetComponent<UICharacterRankScreenPanel>();
    }

    public void PanelActive(bool isBool)
    {
        uICharacterRankScreenPanel.SetActive(isBool);
    }
}
