using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class GameCharacterMysql : MySqlDB
{
    /// <summary>
    /// ĳ���� ���� ��������
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
                // ĳ���� ���� �Է� Ȯ��
                //Debug.LogFormat("ĳ���� ���� Ȯ�� id={0} email={1} nickname={2} score={3}",
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
    /// ĳ���� �����ϱ�
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

    /// <summary>
    /// ���� ������� ������ ������ ��
    /// </summary>
    /// <returns></returns>
    public List<GameCharacterRankInfo> getGameCharacterRankInfos()
    {
        List<GameCharacterRankInfo> gameCharacterRankInfos
            = new List<GameCharacterRankInfo>();
       
        string sqlQuery = "SELECT * FROM  gamecharacters ORDER BY high_score DESC";

        connect();
        // connect()�޼��忡�� conn�� ��ü�� �����ϹǷ� connect()�ڿ� ����Ұ�
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());
        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            int count = 0;
            int rankCount = 1;
            while (mySqlDataReader.Read())
            {
                GameCharacterRankInfo gameCharacterRankInfo
                    = new GameCharacterRankInfo();
                //gameCharacterRankInfo.Rank = ++rankCount;
                gameCharacterRankInfo.Email = (string)mySqlDataReader["email"];
                gameCharacterRankInfo.Nickname = (string)mySqlDataReader["nickname"];
                gameCharacterRankInfo.HighScore = (int)mySqlDataReader["high_score"];
                if (count > 0)
                {
                    if (gameCharacterRankInfos[count - 1].HighScore == gameCharacterRankInfo.HighScore)
                        gameCharacterRankInfo.Rank = rankCount;
                    else
                        gameCharacterRankInfo.Rank = ++rankCount;
                }
                else
                {
                    gameCharacterRankInfo.Rank = rankCount;
                }
                gameCharacterRankInfos.Add(gameCharacterRankInfo);
                count++;
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
