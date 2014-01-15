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

        #region 공통 부분

        /// <summary>
        /// 카드 이동
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_MoveCard(UserContext aContext, string command)
        {
            dealer.MoveCard(aContext, command);
        }

        /// <summary>
        /// 카드 자원으로 쓸게요
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_MakeResource(UserContext aContext, string command)
        {
            dealer.MakeResource(aContext, command);
        }

        /// <summary>
        /// 턴 종료 합니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_EndTurn(UserContext aContext, string command)
        {
            dealer.changeTurn(aContext, command);
        }

        #endregion 공통 부분

        #region 게임 준비 과정 부분

        /// <summary>
        /// 대전 신청합니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_GameReady(UserContext aContext, string command)
        {           
            ServerManager.ReadyUser.Enqueue(aContext);
            aContext.Send("GameWait;");
            ServerManager.CheckCanPlay();
        }

        /// <summary>
        /// 방 만든거 확인 했습니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_CreatedRoom_OK(UserContext aContext, string command)
        {
            dealer.sendCharacter(aContext, command);
        }

        /// <summary>
        /// 제 캐릭터 받았습니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_YourCharacter_OK(UserContext aContext, string command)
        {
            dealer.sendCharacter(aContext, command);
        }

        /// <summary>
        /// 상대 캐릭터 받았습니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_OpponentCharacter_OK(UserContext aContext, string command)
        {
            dealer.sendCardDeck(aContext, command);
        }

        /// <summary>
        /// 제 카드 받았습니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_YourCardDeck_OK(UserContext aContext, string command)
        {
            dealer.sendCardDeck(aContext, command);
        }

        /// <summary>
        /// 상대 카드 받았습니다.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_OpponentCardDeck_OK(UserContext aContext, string command)
        {
            dealer.firstDistribute(aContext, command);
            dealer.StartTheGame(aContext, command); //이렇게 넣으면 안되는데 일단 ㄱㄱ -0-;;;;;
        }

        /// <summary>
        /// 게임 시작 해주십시오.
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        protected override void cmdF_StartGame_OK(UserContext aContext, string command)
        {
            dealer.StartTheGame(aContext, command);
        }

        #endregion 게임 준비 과정 부분
    }
}
