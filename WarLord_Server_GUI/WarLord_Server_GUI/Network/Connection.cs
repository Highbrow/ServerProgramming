using Alchemy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.Managers;

namespace WarLord_Server_GUI.Network
{
    /**********************************************
    ***************[[ Connection ]]****************
    ***********************************************/
    public class Connection
    {
        public System.Threading.Timer timer;
        public UserContext Context { get; set; }
        GameRoomManager _grm;
        CommandProcessManager _cpm;

        public Connection()
        {
            this._grm = GameRoomManager.Instance;
            this._cpm = CommandProcessManager.Instance;
            this.timer = new System.Threading.Timer(this.TimerCallback, null, 0, 2000);
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
        public void receivePacket(UserContext aContext)
        {
             _cpm.CommandProcess(ref aContext);    //CPM에게 수신데이터 전송
        }
    }
}
