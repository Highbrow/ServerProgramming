using Alchemy.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    abstract class ServerCommandLibrary
    {
        public ConcurrentDictionary<string, Delegate> CommandDic = new ConcurrentDictionary<string, Delegate>();
        public delegate void cmdDelegate(UserContext aContext, string command);



        #region 공통 부분

        private readonly string cmd_MoveCard = "MoveCard";    //카드 이동합니다.
        abstract protected void cmdF_MoveCard(UserContext aContext, string command);


        private readonly string cmd_MakeResource = "MakeResource";    //카드 자원으로 쓸게요.
        abstract protected void cmdF_MakeResource(UserContext aContext, string command);

        
        private readonly string cmd_EndTurn = "EndTurn";    //턴 종료 합니다.
        abstract protected void cmdF_EndTurn(UserContext aContext, string command);
        #endregion 공통 부분

        #region 게임 준비 과정 부분

        private readonly string cmd_GameReady = "GameReady";    //대전 신청합니다.
        abstract protected void cmdF_GameReady(UserContext aContext, string command);


        private readonly string cmd_CreatedRoom_OK = "CreatedRoom_OK";    //방 만든거 확인했습니다.
        abstract protected void cmdF_CreatedRoom_OK(UserContext aContext, string command);


        private readonly string cmd_YourCharacter_OK = "YourCharacter_OK";    //제 케릭터 받았습니다.
        abstract protected void cmdF_YourCharacter_OK(UserContext aContext, string command);


        private readonly string cmd_OpponentCharacter_OK = "OpponentCharacter_OK";    //상대 케릭터 받았습니다.
        abstract protected void cmdF_OpponentCharacter_OK(UserContext aContext, string command);


        private readonly string cmd_YourCardDeck_OK = "YourCardDeck_OK";    //제 카드 받았습니다.
        abstract protected void cmdF_YourCardDeck_OK(UserContext aContext, string command);


        private readonly string cmd_OpponentCardDeck_OK = "OpponentCardDeck_OK";    //상대방 카드 받았습니다.
        abstract protected void cmdF_OpponentCardDeck_OK(UserContext aContext, string command);


        private readonly string cmd_StartGame_OK = "StartGame_OK";    //게임 시작 해주십시오.
        abstract protected void cmdF_StartGame_OK(UserContext aContext, string command);

        #endregion 게임 준비 과정 부분

        public ServerCommandLibrary()
        {
            #region 공통 부분
            CommandDic.TryAdd(cmd_MoveCard, new cmdDelegate(cmdF_MoveCard));  //MoveCard
            CommandDic.TryAdd(cmd_MakeResource, new cmdDelegate(cmdF_MakeResource));  //MakeResource
            CommandDic.TryAdd(cmd_EndTurn, new cmdDelegate(cmdF_EndTurn));  //EndTurn
            #endregion 공통 부분
            #region 게임 준비 과정 부분
            CommandDic.TryAdd(cmd_GameReady, new cmdDelegate(cmdF_GameReady));  //GameReady
            CommandDic.TryAdd(cmd_CreatedRoom_OK, new cmdDelegate(cmdF_CreatedRoom_OK));  //CreatedRoom_OK
            CommandDic.TryAdd(cmd_YourCharacter_OK, new cmdDelegate(cmdF_YourCharacter_OK));  //YourCharacter_OK
            CommandDic.TryAdd(cmd_OpponentCharacter_OK, new cmdDelegate(cmdF_OpponentCharacter_OK));  //OpponentCharacter_OK
            CommandDic.TryAdd(cmd_YourCardDeck_OK, new cmdDelegate(cmdF_YourCardDeck_OK));  //YourCardDeck_OK
            CommandDic.TryAdd(cmd_OpponentCardDeck_OK, new cmdDelegate(cmdF_OpponentCardDeck_OK));  //OpponentCardDeck_OK
            CommandDic.TryAdd(cmd_StartGame_OK, new cmdDelegate(cmdF_StartGame_OK));  //StartGame_OK
            #endregion 게임 준비 과정 부분
        }
    }
}
