﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using THEDARKKNIGHT.Network.UdpSocket;
using THEDARKKNIGHT.Network.UdpSocket.Component;
using UnityEngine;
namespace THEDARKKNIGHT.Example.UdpSocket
{

    public class UdpSocketClientTest : MonoBehaviour
    {
        UdpSocketClientMgr UdpClient;

        public string IPAddress;

        public int ListernPort;

        public int SenderPort;

        // Use this for initialization
        void Start()
        {
            UdpClient = new UdpSocketClientMgr(IPAddress, ListernPort);
            UdpClient.MessageKeeper = new MessageKeeper(new UMessageSolver());
            UdpClient.MessageSend = new MessageSender();
            UdpClient.ConnectToServer(IPAddress, SenderPort);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) {
                UdpClient.MessageSend.SendMsg(Encoding.UTF8.GetBytes("Hello world!"));
            }
        }
    }

}