using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonWarLord_preprototype.CommandLibrary
{
    abstract class ClientCommandLibrary
    {
        public static ConcurrentDictionary<string, Delegate> CommandDic = new ConcurrentDictionary<string, Delegate>();
        public delegate void cmdDelegate(string command);

        #region 공통 부분

        private readonly string cmd_MoveCard = "MoveCard";    //카드이동 시켜라
        abstract protected void cmdF_MoveCard(string command);


        private readonly string cmd_YourTurn = "YourTurn";    //니 차례야
        abstract protected void cmdF_YourTurn(string command);


        private readonly string cmd_OpponentTurn = "OpponentTurn";    //상대 차례다
        abstract protected void cmdF_OpponentTurn(string command);


        private readonly string cmd_Message = "Message";    //알림 띄워라
        abstract protected void cmdF_Message(string command);

        #endregion 공통 부분

        #region 게임 준비 과정 부분
        private readonly string cmd_GameWait = "GameWait";  //대전상대 찾아줄께 기다려라
        abstract protected void cmdF_GameWait(string command);


        private readonly string cmd_CreatedRoom = "CreatedRoom";    //방 만들었다
        abstract protected void cmdF_CreatedRoom(string command);


        private readonly string cmd_YourCharacter = "YourCharacter";    //니 캐릭터 받아라
        abstract protected void cmdF_YourCharacter(string command);


        private readonly string cmd_OpponentCharacter = "OpponentCharacter";    //상대방 캐릭터 받아라
        abstract protected void cmdF_OpponentCharacter(string command);


        private readonly string cmd_YourCardDeck = "YourCardDeck";    //니 카드 받아라
        abstract protected void cmdF_YourCardDeck(string command);


        private readonly string cmd_OpponentCardDeck = "OpponentCardDeck";    //상대방 카드 받아라
        abstract protected void cmdF_OpponentCardDeck(string command);


        private readonly string cmd_StartGame = "StartGame";    //게임 시작한다
        abstract protected void cmdF_StartGame(string command);

        #endregion 게임 준비 과정 부분

        public ClientCommandLibrary()
        {
            #region 공통 부분
            CommandDic.TryAdd(cmd_MoveCard, new cmdDelegate(cmdF_MoveCard));    //MoveCard
            CommandDic.TryAdd(cmd_YourTurn, new cmdDelegate(cmdF_YourTurn));    //YourTurn
            CommandDic.TryAdd(cmd_OpponentTurn, new cmdDelegate(cmdF_OpponentTurn));    //OpponentTurn
            CommandDic.TryAdd(cmd_Message, new cmdDelegate(cmdF_Message));    //Message
            #endregion 공통 부분

            #region 게임 준비 과정 부분
            CommandDic.TryAdd(cmd_GameWait, new cmdDelegate(cmdF_GameWait));    //GameWait
            CommandDic.TryAdd(cmd_CreatedRoom, new cmdDelegate(cmdF_CreatedRoom));  //CreatedRoom
            CommandDic.TryAdd(cmd_YourCharacter, new cmdDelegate(cmdF_YourCharacter));  //YourCharacter
            CommandDic.TryAdd(cmd_OpponentCharacter, new cmdDelegate(cmdF_OpponentCharacter));  //OpponentCharacter
            CommandDic.TryAdd(cmd_YourCardDeck, new cmdDelegate(cmdF_YourCardDeck));  //YourCardDeck
            CommandDic.TryAdd(cmd_OpponentCardDeck, new cmdDelegate(cmdF_OpponentCardDeck));  //OpponentCardDeck
            CommandDic.TryAdd(cmd_StartGame, new cmdDelegate(cmdF_StartGame));  //StartGame
            #endregion 게임 준비 과정 부분
        }
    }
}
