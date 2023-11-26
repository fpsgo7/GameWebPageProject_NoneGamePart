using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class GameHighScoreMysql : MySqlDB
{
    /// <summary>
    /// ���� ������� ������ ������ ��
    /// </summary>
    /// <returns></returns>
    public List<GameCharacterRankInfo> getGameCharacterRankInfos()
    {
        List<GameCharacterRankInfo> gameCharacterRankInfos
            = new List<GameCharacterRankInfo>();

        string sqlQuery = "SELECT * FROM  game_high_scores ORDER BY high_score DESC, lasted_time ASC ";

        connect();
        // connect()�޼��忡�� conn�� ��ü�� �����ϹǷ� connect()�ڿ� ����Ұ�
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
