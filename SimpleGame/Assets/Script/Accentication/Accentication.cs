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
    public long Login(string email , string password)
    {
        // 비밀번호가 틀릴경우
        if (!CheckPassword(email, password))
            return -1;
        // 유저 정보 반환하기
        return SetUserInfo(email);
       
    }
    /// <summary>
    /// 비밀번호 체크하기
    /// 암호화된 값을 복호화하여 체크한다.
    /// </summary>
    private bool CheckPassword(string email,string password)
    {
        string realPassword=null;
        connect();

        string sqlQuery = String.Format("SELECT * FROM users " +
           "where email = '{0}'", email);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                realPassword = (string)mySqlDataReader["password"];
            }
            DisConnect();

            if (BCrypt.Net.BCrypt.Verify(password, realPassword))
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return false;
        }
    }
    /// <summary>
    /// Static 클래스 UserInfo에 값 할당하기
    /// </summary>
    private long SetUserInfo(string email)
    {
        connect();
        string sqlQuery = String.Format("SELECT * FROM users " +
            "where email = '{0}'", email);
        MySqlCommand cmd = new MySqlCommand(sqlQuery, getConn());

        try
        {
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                UserInfo.Id = (long)mySqlDataReader["id"];
                UserInfo.Email = (string)mySqlDataReader["email"];
                UserInfo.Password = (string)mySqlDataReader["password"];
                UserInfo.Nickname = (string)mySqlDataReader["nickname"];
                // 로그인 정보 입력 확인
                //Debug.LogFormat("id={0} email={1} password={2} nickname={3}",
                //    UserInfo.Id, UserInfo.Email, UserInfo.Password, UserInfo.Nickname);
            }
            DisConnect();
            return UserInfo.Id;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            DisConnect();
            return -1;
        }
    }
}
