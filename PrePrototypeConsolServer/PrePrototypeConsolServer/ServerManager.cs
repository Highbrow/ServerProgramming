using Alchemy;
using Alchemy.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class ServerManager
    {
        public WebSocketServer _wServer;
        public static ConcurrentDictionary<string, Connection> OnlineConnections = new ConcurrentDictionary<string, Connection>();


        public ServerManager(){
            try
            {
                _wServer = new WebSocketServer(9001, System.Net.IPAddress.Any)   //소켓 생성
                {
                    OnReceive = OnReceive,
                    OnSend = OnSend,
                    OnConnected = OnConnect,
                    OnDisconnect = OnDisconnect,
                    TimeOut = new TimeSpan(0, 5, 0)
                };
                _wServer.Start();
                displayString("Running Warlord Server..");
                displayString("[hit Stop Button to stop the server]");
                displayString("서버를 시작합니다.");
                
            }
            catch (Exception e)
            {
                displayString(e.Message.ToString());
            }
        }
        public static void OnConnect(UserContext aContext)
        {
            Console.WriteLine("Client Connected From : " + aContext.ClientAddress.ToString());
            var conn= new Connection {Context=aContext};
            OnlineConnections.TryAdd(aContext.ClientAddress.ToString(), conn);
        }

        public static void OnReceive(UserContext aContext)
        {
            try
            {
                Console.WriteLine("Data Received From [" + aContext.ClientAddress.ToString() + "] - " + aContext.DataFrame.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
        }
        public static void OnSend(UserContext aContext)
        {            
            Console.WriteLine("Data Sent To : " + aContext.ClientAddress.ToString());
        }
        public static void OnDisconnect(UserContext aContext)
        {
            Console.WriteLine("Client Disconnected : " + aContext.ClientAddress.ToString());
            Connection conn;
            OnlineConnections.TryRemove(aContext.ClientAddress.ToString(),out conn);

            conn.timer.Dispose();
        }
        //=====[출력 관련]=====
        static public void displayString(string str)
        {
            Console.WriteLine(str);
        }

        /**********************************************
        ***********[[ Singleton 적용 ]]****************
        ***********************************************/
        static ServerManager ServerConnectorInstance = null;
        static readonly object padlock = new object();
        public static ServerManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (ServerConnectorInstance == null)
                    {
                        ServerConnectorInstance = new ServerManager();
                    }
                    return ServerConnectorInstance;
                }
            }
        }


    }
}
