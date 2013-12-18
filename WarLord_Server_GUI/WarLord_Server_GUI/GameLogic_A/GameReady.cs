using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.Network;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameReady
    {
        public static ConcurrentDictionary<string, Connection> GameReadyConnections = new ConcurrentDictionary<string, Connection>();

        public void MatchingVS()
        {

        }
    }
}
