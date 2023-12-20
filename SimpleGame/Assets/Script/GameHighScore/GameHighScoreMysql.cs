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
    /// <summary>
    /// 점수 추가하기
    /// </summary>
    public int createGameHighScore(String email, string nickname, int newScore)
    {
        connect();
        // sql 문 예
        //INSERT INTO `game_high_scores` (`email`, `game_character_nickname`, `high_score`) VALUES ('1@1', '111', '1');
        string sqlQuery = String.Format("INSERT INTO game_high_scores " +
           " (`email`, `game_character_nickname`, `high_score`)" +
           "VALUES('{0}', '{1}', {2})"
           , email, nickname,newScore);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());
        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            return 1;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return -1;
        }
    }
    /// <summary>
    /// 점수 입력하기
    /// </summary>
    public int updateGameScore(string email, int newScore)
    {
        //UPDATE game_high_scores SET `high_score` = '50' WHERE (`email` = '1@1');
        connect();
        string sqlQuery = String.Format("UPDATE game_high_scores " +
            "SET `high_score` = {1} " +
            "WHERE (`email` = '{0}')" 
           , email, newScore);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            DisConnect();
            return 1;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return -1;
        }
    }
}
