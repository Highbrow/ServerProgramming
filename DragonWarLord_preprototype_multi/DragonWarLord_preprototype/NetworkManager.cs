using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_B;
using WebSocketSharp;

namespace DragonWarLord_preprototype
{
    class NetworkManager
    {
        public static WebSocket ws;
        public NetworkManager()
        {
            try
            {
                ws = new WebSocket("ws://127.0.0.1:9001");
                ws.OnOpen += (object sender, System.EventArgs e) =>
                {
                    MessageBox.Show("open");
                };
                ws.OnMessage += ws_OnMessage;
                ws.Connect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void start()
        {
            GamePlayManager.Instance.GameDefaultSetting();
        }

        void ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Data.Equals("ready"))
            {
                GamePlayManager.Instance.inputCard();     //데이터 베이스의 카드 호출
                GamePlayManager.Instance.makeMainPlayer();   //메인 캐릭터 생성
                GamePlayManager.Instance.firstDistribute();   //첫 카드 지급
                GamePlayManager.Instance.distribute();    //턴마다 카드 배급 관련
            }
        }

        #region 싱글톤
        static NetworkManager NetworkManagerInstance = null;
        static readonly object padlock = new object();
        /// <summary>
        /// Singleton 적용
        /// </summary>
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
