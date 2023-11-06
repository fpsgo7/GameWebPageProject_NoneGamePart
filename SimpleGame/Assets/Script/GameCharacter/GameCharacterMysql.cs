using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;

public class GameCharacterMysql : MySqlDB
{
    /// <summary>
    /// 캐릭터 정보 가져오기
    /// </summary>
    public int GetMyGameCharacter(String email)
    {
        connect();

        string sqlQuery = String.Format("SELECT * FROM gamecharacters " +
           "where email = '{0}'", email);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {

                GameCharacterInfo.Id = (int)mySqlDataReader["id"];
                GameCharacterInfo.Email = (string)mySqlDataReader["email"];
                GameCharacterInfo.Nickname = (string)mySqlDataReader["nickname"];
                GameCharacterInfo.HighScore = (int)mySqlDataReader["high_score"];
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
        string sqlQuery = String.Format("INSERT INTO gamecharacters " +
           " (`email`, `nickname`, `high_score`)"+
           "VALUES('{0}', '{1}', 0)"
           , email,nickname);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            GetMyGameCharacter(email);
            DisConnect();
            return 1;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return -1;
        }
    }
}
