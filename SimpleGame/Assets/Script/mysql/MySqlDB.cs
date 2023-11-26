using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data.MySqlClient;
using System;
/// <summary>
/// MySql과 연결하기위한 클래스
/// </summary>
public class MySqlDB
{
    // DB 연결정보
    private MySqlConnection conn = null;
    private string DBip = "RDS_END_POINT";// 로컬 호스트 ip
    private string DBname = "RDS_DB_NAME";// db 이름
    private string DBid = "RDS_USER"; // 관리자 id
    private string DBpw = "RDS_PW"; // 관리자 비번

    protected MySqlConnection getConn()
    {
        return conn;
    }
    /// <summary>
    /// db 열기
    /// </summary>
    protected void connect()
    {
        //DB정보 입력
        string sqlDatabase = "Server=" + DBip + ";Database=" + DBname + ";UserId=" + DBid + ";Password=" + DBpw + ";CharSet=utf8";
        conn = new MySqlConnection(sqlDatabase);
        //접속하기
        try
        {
            conn.Open();
            //접속이 되면 OPEN이라고 나타남
            //Debug.Log("SQL의 접속 상태 : " + conn.State);
        }
        catch (Exception msg)
        {
            Debug.Log(msg); //기타다른오류가 나타나면 오류에 대한 내용이 나타남
        }
    }

    /// <summary>
    /// db 닫기
    /// </summary>
    protected void DisConnect()
    {
        conn.Close();
        //접속이 끊기면 Close가 나타남 
        //Debug.Log("SQL의 접속 상태 : " + conn.State);
    }

}
