//===================================================
//作    者：#AuthorName# 
//创建时间：#CreateTime#
//备    注：
//===================================================

using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TCPClient : MonoBehaviour {


    private Socket tcpSocket;


    // Use this for initialization
    void Start () {
		
	}

    public void connect()
    {
        //创建socket
        tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //连接服务器
        tcpSocket.Connect(IPAddress.Parse("127.0.0.1"), 12321);
        Debug.Log("连接服务器");

        ////接收消息
        //byte[] bt = new byte[1024];
        //int messgeLength = tcpSocket.Receive(bt);
        ////Debug.Log(ASCIIEncoding.UTF8.GetString(bt));

        SearchRequest search = new SearchRequest();
        search.ResultPerPage = 100;
        search.PageNumber = 100;
        byte[] temp = search.ToByteArray();

        //发送消息
        tcpSocket.Send(temp);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
