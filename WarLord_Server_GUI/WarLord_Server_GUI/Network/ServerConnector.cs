﻿using Alchemy;
using Alchemy.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.Managers;

namespace WarLord_Server_GUI.Network
{
    class ServerConnector
    {
        //=====[소켓 생성 및 초기화]=====
        public MainForm _mf;
        public CommandProcessManager _cpm;
        public WebSocketServer _aServer;
        public static Connection conn;
        public static ConcurrentDictionary<string, Connection> OnlineConnections = new ConcurrentDictionary<string, Connection>();

        public void ConnectServer(MainForm mf)
        {
            this._mf = mf;
            this._cpm = CommandProcessManager.Instance;
            _aServer = new WebSocketServer(9001, System.Net.IPAddress.Any)   //소켓 생성
            {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnected = OnConnect,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };
        }

        //=====[서버 시작]=====
        public void StartServer()
        {
            try
            {
                _aServer.Start();

                _mf.LogOutPut("Running Warlord Server..");
                _mf.LogOutPut("[hit Stop Button to stop the server]");
                MessageBox.Show("서버를 시작합니다.");
                _mf._isRunningServer = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        //=====[서버 종료]=====
        public void StopServer()
        {
            try
            {
                _aServer.Stop();
                //OnlineConnections.Clear();

                _mf.LogOutPut("Stop Server..");
                MessageBox.Show("서버를 중지합니다.");
                _mf._isRunningServer = false;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        //=====[연결]=====
        public void OnConnect(UserContext aContext)
        {
            _mf.LogOutPut("Client Connected From : " + aContext.ClientAddress.ToString());
            var conn = new Connection { Context = aContext };   //Connection
            OnlineConnections.TryAdd(aContext.ClientAddress.ToString(), conn);
        }

        //=====[데이터 수신]=====
        public void OnReceive(UserContext aContext)
        {
            try
            {
                _cpm.CommandProcess(aContext.DataFrame.AsRaw());    //CPM에게 수신데이터 전송
            }
            catch (Exception ex)
            {
                _mf.LogOutPut(ex.Message.ToString());
            }

        }
        //=====[데이터 송신]=====
        public void OnSend(UserContext aContext)
        {
            _mf.LogOutPut("Data Sent To : " + aContext.ClientAddress.ToString());
        }

        //=====[연결 끊김]=====
        public void OnDisconnect(UserContext aContext)
        {
            _mf.LogOutPut("Client Disconnected : " + aContext.ClientAddress.ToString());

            OnlineConnections.TryRemove(aContext.ClientAddress.ToString(), out conn);
            try
            {
                //conn.timer.Dispose();
            }
            catch (Exception ex)
            {
                _mf.LogOutPut(ex.Message.ToString());
            }
        }
        /**********************************************
        ***********[[ Singleton 적용 ]]****************
        ***********************************************/
        static ServerConnector ServerConnectorInstance = null;
        static readonly object padlock = new object();
        public static ServerConnector Instance
        {
            get
            {
                lock (padlock)
                {
                    if (ServerConnectorInstance == null)
                    {
                        ServerConnectorInstance = new ServerConnector();
                    }
                    return ServerConnectorInstance;
                }
            }
        }
    }
}