using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class GameCharacterMysql : MySqlDB
{
    /// <summary>
    /// 캐릭터 정보 가져오기
    /// </summary>
    public long GetMyGameCharacter(String email)
    {
        connect();

        string sqlQuery = String.Format("SELECT * FROM game_characters " +
           "where email = '{0}'", email);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {

                GameCharacterInfo.Id = (long)mySqlDataReader["id"];
                GameCharacterInfo.Email = (string)mySqlDataReader["email"];
                GameCharacterInfo.Nickname = (string)mySqlDataReader["nickname"];
                GameCharacterInfo.HighScore = (long)mySqlDataReader["high_score"];
                // 캐릭터 정보 입력 확인
                //Debug.LogFormat("캐릭터 정보 확인 id={0} email={1} nickname={2} score={3}",
                //    GameCharacterInfo.Id, GameCharacterInfo.Email, GameCharacterInfo.Nickname, GameCharacterInfo.HighScore);
            }
            DisConnect();
            return GameCharacterInfo.Id;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return -1;
        }
    }
    /// <summary>
    /// 캐릭터 생성하기
    /// </summary>
    public int createGameCharacter(string email, string nickname)
    {
        connect();
        // INSERT INTO `mywebgameproject`.`gamecharacters` (`id`, `email`, `nickname`, `high_score`) ;
        string sqlQuery = String.Format("INSERT INTO game_characters " +
           " (`email`, `nickname`, `high_score`)"+
           "VALUES('{0}', '{1}', 0)"
           , email,nickname);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            DisConnect();
            GetMyGameCharacter(email);
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
