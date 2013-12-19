using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.Network;

namespace WarLord_Server_GUI.GameLogic_A
{
    class RoomManager
    {
        public static ConcurrentDictionary<string, Connection> GameReadyConnections = new ConcurrentDictionary<string, Connection>();


        /**********************************************
        ***********[[ Singleton 적용 ]]****************
        ***********************************************/
        static RoomManager RoomManagerInstance = null;
        static readonly object padlock = new object();

        public static RoomManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (RoomManagerInstance == null)
                    {
                        RoomManagerInstance = new RoomManager();
                    }
                    return RoomManagerInstance;
                }
            }
        }

    }
}
