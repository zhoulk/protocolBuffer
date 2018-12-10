﻿//===================================================
//作    者：#AuthorName# 
//创建时间：#CreateTime#
//备    注：
//===================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TCPServer : MonoBehaviour {

    string ip = "127.0.0.1";
    int port = 12321;
    //服务器soket接收器
    private Socket severSocket;
    //客户端soket集合 List
    private List<Socket> clienList = new List<Socket>();

    // Use this for initialization
    void Start () {
        StartSever();
    }

    //启动服务器的脚本
    public void StartSever()
    {
        try
        {
            severSocket = new Socket(
              AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp
              );
            //将服务器的ip捆绑
            severSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));


            //为服务器sokect添加监听
            severSocket.Listen(10);
            Debug.Log("服务器启动成功");
            clienList = new List<Socket>();
            //开始服务器时 一般接受一个服务就会被挂起所以要用多线程来解决
            Thread threadAccept = new Thread(Accept);
            threadAccept.IsBackground = true;
            threadAccept.Start();
        }
        catch
        {
            Debug.Log("创建服务器失败");
        }
    }

    public void Accept()
    {
        Socket client = severSocket.Accept();
        IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
        Debug.Log(point.Address + ":【" + point.Port + "】连接成功");
        clienList.Add(client);


        Thread threadReceive = new Thread(ReceiveAndSend);
        threadReceive.IsBackground = true;
        threadReceive.Start(client);
        Accept();
    }

    public void ReceiveAndSend(object obj)
    {
        Socket client = obj as Socket;
        IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
        try
        {
            byte[] msg = new byte[1024 * 1024];
            int msgLen = client.Receive(msg);
            Debug.Log(msgLen);
            byte[] temp = new byte[msgLen];
            Buffer.BlockCopy(msg,0,temp,0,msgLen);
            SearchRequest search2 = SearchRequest.Parser.ParseFrom(temp);
            Debug.Log("Server " + search2);

            //string msgStr = Encoding.UTF8.GetString(msg, 0, msgLen);
            //Debug.Log("接收到的内容：" + msgStr);
            ////接下来只是将我们接收到的信息进行广播


        }
        catch
        {
            Debug.Log("无法接收到信息");
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnDestroy()
    {
  
    }
}
