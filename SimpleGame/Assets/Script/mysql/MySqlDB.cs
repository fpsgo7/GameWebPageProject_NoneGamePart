using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
/// <summary>
/// MySql�� �����ϱ����� Ŭ����
/// </summary>
public class MySqlDB
{
    // DB ��������
    private MySqlConnection conn = null;
    private string DBip = "RDS_END_POINT";// ���� ȣ��Ʈ ip
    private string DBname = "RDS_DB_NAME";// db �̸�
    private string DBid = "RDS_USER"; // ������ id
    private string DBpw = "RDS_PW"; // ������ ���

    protected MySqlConnection getConn()
    {
        return conn;
    }
    /// <summary>
    /// db ����
    /// </summary>
    protected void connect()
    {
        //DB���� �Է�
        string sqlDatabase = "Server=" + DBip + ";Database=" + DBname + ";UserId=" + DBid + ";Password=" + DBpw + ";CharSet=utf8";
        conn = new MySqlConnection(sqlDatabase);
        //�����ϱ�
        try
        {
            conn.Open();
            //������ �Ǹ� OPEN�̶�� ��Ÿ��
            //Debug.Log("SQL�� ���� ���� : " + conn.State);
        }
        catch (Exception msg)
        {
            Debug.Log(msg); //��Ÿ�ٸ������� ��Ÿ���� ������ ���� ������ ��Ÿ��
        }
    }

    /// <summary>
    /// db �ݱ�
    /// </summary>
    protected void DisConnect()
    {
        conn.Close();
        //������ ����� Close�� ��Ÿ�� 
        //Debug.Log("SQL�� ���� ���� : " + conn.State);
    }

}
