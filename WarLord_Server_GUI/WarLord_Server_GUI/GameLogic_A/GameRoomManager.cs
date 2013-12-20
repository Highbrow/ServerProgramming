using Alchemy.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.Network;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameRoomManager
    {
        public static ConcurrentDictionary<string, Connection> GameReadyConnections = new ConcurrentDictionary<string, Connection>();

        private List<UserContext> _ReadyUserList;   //대전 신청 유저
        private List<GameRoom> _GameRoomList;   //방 리스트

        private GameRoomManager()
        {
            this._ReadyUserList = new List<UserContext>();
            this._GameRoomList = new List<GameRoom>();
            GameRoom.eventDeleteRoom += new GameRoom.dDeleteRoom(delGameRoom);
        }

        //=====[ 대전 신청자 추가 ]=====
        public void addReadyUser(ref UserContext user){
            _ReadyUserList.Add(user);
        }
        public void delReadyUser(ref UserContext user)
        {
            _ReadyUserList.Remove(user);
        }

        //=====[ 방 추가 ]=====
        public void addGameRoom(ref UserContext p1, ref UserContext p2)
        {
            _GameRoomList.Add(new GameRoom(ref p1,ref p2));
        }

        //=====[ 방 삭제 이벤트 처리 ]=====
        public void delGameRoom(ref GameRoom room)
        {
            _GameRoomList.Remove(room);
        }


        /**********************************************
        ***********[[ Singleton 적용 ]]****************
        ***********************************************/
        static GameRoomManager RoomManagerInstance = null;
        static readonly object padlock = new object();

        public static GameRoomManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (RoomManagerInstance == null)
                    {
                        RoomManagerInstance = new GameRoomManager();
                    }
                    return RoomManagerInstance;
                }
            }
        }

    }
}
