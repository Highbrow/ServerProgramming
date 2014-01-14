using Alchemy;
using Alchemy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrePrototypeConsolServer
{
    class GameRoom
    {
        public List<UserContext> userList;

        public CardDealer _Dealer;
        public GameBoard _GameBoard;

        public ServerCommandProc SCP;

        public GameRoom(UserContext Player1, UserContext Player2)
        {
            userList = new List<UserContext>();
            userList.Add(Player1);
            userList.Add(Player2);
            _GameBoard = new GameBoard();
            SCP = new ServerCommandProc();
            _Dealer = new CardDealer(this, ref _GameBoard,ref SCP);
            SCP.dealer = _Dealer;
            broadCasting("CreatedRoom;");
        }

        public void broadCasting(string message)
        {
            foreach (UserContext user in userList)
            {
                user.Send(message);
            }
        }
    }
}
