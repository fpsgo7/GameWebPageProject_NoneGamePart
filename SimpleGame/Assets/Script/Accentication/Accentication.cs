using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;

/// <summary>
/// 회원 관련 기능 클래스
/// </summary>
public class Accentication : MySqlDB
{

    /// <summary>
    /// 로그인 기능
    /// </summary>
    public int Login(string email , string password)
    {
        connect();
        string sqlQuery = String.Format("SELECT * FROM users " +
            "where email = '{0}' AND password = '{1}'", email,password);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try{
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {

                UserInfo.Id = (int)mySqlDataReader["id"];
                UserInfo.Email = (string)mySqlDataReader["email"];
                UserInfo.Password = (string)mySqlDataReader["password"];
                UserInfo.Nickname = (string)mySqlDataReader["nickname"];
                // 로그인 정보 입력 확인
                //Debug.LogFormat("id={0} email={1} password={2} nickname={3}",
                //    UserInfo.Id, UserInfo.Email, UserInfo.Password, UserInfo.Nickname);
            }
            DisConnect();
            return UserInfo.Id;
            
        }catch(Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return -1;
        }
        
    }
}
