using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRankPanelScript : MonoBehaviour
{
    private UICharacterRankScreenPanel uICharacterRankScreenPanel;
    private UICharacterScreenPanel uIcharacterScreenPanel;
    private List<GameCharacterRankInfo> gameCharacterRankInfos;
    private List<GameObject> gameCharacterRankObjects = new List<GameObject>();

    void Start()
    {
        uICharacterRankScreenPanel = GameObject.Find("PanelObjectScript").GetComponent<UICharacterRankScreenPanel>();
        uIcharacterScreenPanel = GameObject.Find("PanelObjectScript").GetComponent<UICharacterScreenPanel>();
    }

    public void PanelActive(bool isBool, List<GameCharacterRankInfo> gameCharacterRankInfos)
    {
        foreach (GameCharacterRankInfo gameCharacterRankInfo in gameCharacterRankInfos)
        {
            GameObject rankGameObject
                = Instantiate<GameObject>(uICharacterRankScreenPanel.gameCharacterRankPrefab, uICharacterRankScreenPanel.gridSetting.transform);
            gameCharacterRankObjects.Add(rankGameObject);
        }
        for (int i = 0; i < gameCharacterRankObjects.Count; i++)
        {
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
