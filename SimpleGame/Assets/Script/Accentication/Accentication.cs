using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;



/// <summary>
/// ȸ�� ���� ��� Ŭ����
/// </summary>
public class Accentication : MySqlDB
{

    /// <summary>
    /// �α��� ���
    /// </summary>
    public long Login(string email , string password)
    {
        // ��й�ȣ�� Ʋ�����
        if (!CheckPassword(email, password))
            return -1;
        // ���� ���� ��ȯ�ϱ�
        return SetUserInfo(email);
       
    }
    /// <summary>
    /// ��й�ȣ üũ�ϱ�
    /// ��ȣȭ�� ���� ��ȣȭ�Ͽ� üũ�Ѵ�.
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
    /// Static Ŭ���� UserInfo�� �� �Ҵ��ϱ�
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
                // �α��� ���� �Է� Ȯ��
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
