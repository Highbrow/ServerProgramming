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
        protected static ConcurrentDictionary<string, Connection> OnlineConnections = new ConcurrentDictionary<string, Connection>();
        public WebSocketServer aServer;
        public static Connection conn;

        public ServerConnector()
        {
            aServer = new WebSocketServer(9001, System.Net.IPAddress.Any)
            {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnected = OnConnect,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
            };
            /*
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Warlord WebSocket Server";
            Console.WriteLine("Running Warlord WebSocket Server ...");
            Console.WriteLine("[Type \"exit\" and hit enter to stop the server]");

            var command = string.Empty;
            while (command != "exit")
            {
                command = Console.ReadLine();
            }
            */
        }
        public int StartServer()
        {
            try
            {
                aServer.Start();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message.ToString());
                return -1;  //연결 실패 : -1
            }
            return 1;   //연결 성공 : 1
        }
        public int StopServer()
        {
            try
            {
                aServer.Stop();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return -1;  //연결 실패 : -1
            }
            return 1;   //연결 성공 : 1
        }

        public static void OnConnect(UserContext aContext)
        {
            Console.WriteLine("Client Connected From : " + aContext.ClientAddress.ToString());
            var conn = new Connection { Context = aContext };
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

            OnlineConnections.TryRemove(aContext.ClientAddress.ToString(), out conn);
            try
            {
                conn.timer.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

        }
    }

    public class Connection
    {
        public System.Threading.Timer timer;
        public UserContext Context { get; set; }
        public Connection()
        {
            this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 1000);
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