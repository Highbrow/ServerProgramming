using Alchemy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class ServerCommandProc : ServerCommandLibrary
    {
        public CardDealer dealer;

        protected override void cmdF_GameReady(UserContext aContext, string command)
        {           
            ServerManager.ReadyUser.Enqueue(aContext);
            aContext.Send("GameWait;");
            ServerManager.CheckCanPlay();
        }

        protected override void cmdF_CreatedRoom_OK(UserContext aContext, string command)
        {
            dealer.sendCharacter(aContext, command);
        }

        protected override void cmdF_YourCharacter_OK(UserContext aContext, string command)
        {
            dealer.sendCharacter(aContext, command);
        }

        protected override void cmdF_OpponentCharacter_OK(UserContext aContext, string command)
        {
            dealer.sendCardDeck(aContext, command);
        }

        protected override void cmdF_YourCardDeck_OK(UserContext aContext, string command)
        {
            dealer.sendCardDeck(aContext, command);
        }

        protected override void cmdF_OpponentCardDeck_OK(UserContext aContext, string command)
        {
            dealer.firstDistribute(aContext, command);
            dealer.StartTheGame(aContext, command); //이렇게 넣으면 안되는데 일단 ㄱㄱ -0-;;;;;
        }

        protected override void cmdF_StartGame_OK(UserContext aContext, string command)
        {
            dealer.StartTheGame(aContext, command);
        }

        protected override void cmdF_EndTurn(UserContext aContext, string command)
        {
            dealer.changeTurn(aContext, command);
        }
    }
}
