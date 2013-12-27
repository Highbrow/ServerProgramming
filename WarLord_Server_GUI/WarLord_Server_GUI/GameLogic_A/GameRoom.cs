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
        private List<Player> _userList;
        GameRoom gr;
        Dealer dealer;
        GameBoard board;
        
        public GameRoom(Player p1,Player p2)
        {
            board = new GameBoard();        // 게임판 생성
            gr = this;
            _userList = new List<Player>();
            _userList.Add(p1);
            _userList.Add(p2);

            dealer = new Dealer(ref p1, ref p2, ref board);    //딜러 생성
            
            //ServerConnector.eventDisconnectUser += new ServerConnector.dDisconnectUser(delUser);
        }

        //=====[ User 추가 ]=====
        public void addUser(ref Player user)
        {
            if (!_userList.Contains(user))
            {
                _userList.Add(user);
            }
            else
            {

            }
        }
        //=====[ User 삭제 ]=====
        public delegate void dDeleteRoom(ref GameRoom room);
        public static event dDeleteRoom eventDeleteRoom;

        public void delUser(Player user)
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

        public List<Player> getUserList()
        {
            return _userList;
        }
    }
}
