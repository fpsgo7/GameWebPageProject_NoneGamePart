using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class GameHighScoreMysql : MySqlDB
{
    /// <summary>
    /// 점수 순서대로 정보를 가져올 것
    /// </summary>
    /// <returns></returns>
    public List<GameCharacterRankInfo> getGameCharacterRankInfos()
    {
        List<GameCharacterRankInfo> gameCharacterRankInfos
            = new List<GameCharacterRankInfo>();

        string sqlQuery = "SELECT * FROM  game_high_scores ORDER BY high_score DESC, lasted_time ASC ";

        connect();
        // connect()메서드에서 conn의 객체를 생성하므로 connect()뒤에 사용할것
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());
        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            long rankCount = 1;
            while (mySqlDataReader.Read())
            {
                GameCharacterRankInfo gameCharacterRankInfo
                    = new GameCharacterRankInfo();
                gameCharacterRankInfo.Rank = rankCount++;
                gameCharacterRankInfo.Email = (string)mySqlDataReader["email"];
                gameCharacterRankInfo.Nickname = (string)mySqlDataReader["game_character_nickname"];
                gameCharacterRankInfo.HighScore = (long)mySqlDataReader["high_score"];
                gameCharacterRankInfo.LastedTime = (DateTime)mySqlDataReader["lasted_time"];
                gameCharacterRankInfos.Add(gameCharacterRankInfo);
            }
            DisConnect();
            return gameCharacterRankInfos;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return null;
        }
    }
}
