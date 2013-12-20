using Alchemy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.Network;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameRoom
    {
        private List<UserContext> _userList;
        GameRoom gr;
        
        public GameRoom(ref UserContext p1,ref UserContext p2)
        {
            gr = this;
            _userList = new List<UserContext>();
            _userList.Add(p1);
            _userList.Add(p2);
            ServerConnector.eventDisconnectUser += new ServerConnector.dDisconnectUser(delUser);
        }

        //=====[ User 추가 ]=====
        public void addUser(ref UserContext user)
        {
            _userList.Add(user);
        }
        //=====[ User 삭제 ]=====
        public delegate void dDeleteRoom(ref GameRoom room);
        public static event dDeleteRoom eventDeleteRoom;

        public void delUser(ref UserContext user)
        {
            if (_userList.Count() != 0)
            {
                _userList.Remove(user);
            }
            else
            {
                eventDeleteRoom(ref gr); //=====[방 폭파!!]=====
            }
        }

        public List<UserContext> getUserList()
        {
            return _userList;
        }
    }
}
