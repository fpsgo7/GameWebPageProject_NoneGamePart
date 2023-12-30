using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRankPanelScript : MonoBehaviour
{
    private UICharacterRankScreenPanel uICharacterRankScreenPanel;
    private List<GameObject> gameCharacterRankObjects = new List<GameObject>();

    void Start()
    {
        uICharacterRankScreenPanel = GameObject.Find("PanelObjectScript").GetComponent<UICharacterRankScreenPanel>();
    }

    public void PanelActive(bool isBool, List<GameCharacterRankInfo> gameCharacterRankInfos)
    {
        for (int i = 0; i < gameCharacterRankInfos.Count; i++)
        {
            if (i >= gameCharacterRankObjects.Count)
            {
                GameObject rankGameObject
                = Instantiate<GameObject>(uICharacterRankScreenPanel.gameCharacterRankPrefab, uICharacterRankScreenPanel.gridSetting.transform);
                gameCharacterRankObjects.Add(rankGameObject);
            }
            UICharacterRankPanel rankItem = gameCharacterRankObjects[i].GetComponent<UICharacterRankPanel>();
            rankItem.Init(
                gameCharacterRankInfos[i].Rank + "",
                gameCharacterRankInfos[i].Email,
                gameCharacterRankInfos[i].Nickname,
                gameCharacterRankInfos[i].HighScore + ""
                );
        }

        uICharacterRankScreenPanel.SetActive(isBool);

    }
    
}
