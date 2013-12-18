using Alchemy;
using Alchemy.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarLord_Server_GUI.Network
{
    class ServerConnector
    {
        //=====[소켓 생성 및 초기화]=====
        public MainForm _mf;
        public WebSocketServer aServer;
        public static ConcurrentDictionary<string, Connection> OnlineConnections = new ConcurrentDictionary<string, Connection>();
        public static Connection conn;
        
        public void ConnectServer(MainForm mf){
            this._mf = mf;
            aServer = new WebSocketServer(9001, System.Net.IPAddress.Any)   //소켓 생성
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
                aServer.Start();

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
                aServer.Stop();
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
                _mf.LogOutPut("Data Received From [" + aContext.ClientAddress.ToString() + "] - " + aContext.DataFrame.ToString());
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
        static ServerConnector instance = null;
        static readonly object padlock = new object();
        public static ServerConnector Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServerConnector();
                    }
                    return instance;
                }
            }
        }
    }

    /**********************************************
    ***************[[ Connection ]]****************
    ***********************************************/
    public class Connection
    {
        //public System.Threading.Timer timer;
        public UserContext Context { get; set; }
        public Connection()
        {
            //this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object state)
        {
            try
            {
                Context.Send("[" + Context.ClientAddress.ToString() + "] " + System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}