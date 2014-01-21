using DragonWarLord_preprototype.CommandLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace DragonWarLord_preprototype
{
    class NetworkManager
    {
        ClientCommandProc commandP;
        public static WebSocket ws;
        public StartForm startForm;


        public NetworkManager()
        {
            commandP = new ClientCommandProc();
            ws = new WebSocket("ws://127.0.0.1:9001");
            ws.OnMessage += this.ws_OnMessage;
            ws.OnOpen += this.ws_OnOpen;
            ws.OnError += this.ws_OnError;
        }
        public void ws_OnOpen(object sender, EventArgs e)
        {
            startForm.setText_lb_status("Connected to Server.\n");
            NetworkManager.ws.Send("GameReady;");
        }
        public void ws_OnError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show("ERROR: " + e.Message);
            startForm.setText_lb_status("You can not Connect.");
            startForm.setEnabledButton(true);
        }
        public void ws_OnMessage(object sender, MessageEventArgs e)
        {
            string data = e.Data;
            string[] command = data.Split(';');
            Delegate dg;
            ClientCommandProc.CommandDic.TryGetValue(command[0], out dg);
            dg.DynamicInvoke(data);
        }

        #region 싱글톤
        static NetworkManager NetworkManagerInstance = null;
        static readonly object padlock = new object();
        /// <summary>
        /// 싱글톤
        /// </summary>
        /// <param name="startForm"></param>
        /// <returns></returns>
        public static NetworkManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (NetworkManagerInstance == null)
                    {
                        NetworkManagerInstance = new NetworkManager();
                    }
                    return NetworkManagerInstance;
                }
            }
        }

        #endregion


    }
}
