using Alchemy;
using Alchemy.Classes;
using Alchemy.Handlers.WebSocket;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class CardDealer
    {
        //====[Client Zone index]=====
        public const int MY_CARDDECK = 1;
        public const int OPPONENT_CARDDECK = 2;
        public const int MY_HANDSZONE = 3;
        public const int OPPONENT_HANDSZONE = 4;
        public const int MY_WARZONE = 5;
        public const int OPPONENT_WARZONE = 6;
        public const int MY_MANAZONE = 7;
        public const int OPPONENT_MANAZONE = 8;
        public const int MY_TOMBZONE = 9;
        public const int OPPONENT_TOMBZONE = 10;
        public const int MY_PLAYERZONE = 100;
        public const int OPPONENT_PLAYERZONE = 200;

        public GameBoard gameBoard;
        private GameRoom gameRoom;
        private ServerCommandProc SCP;
        private OnEventDelegate _OnReceive_Player1;
        private OnEventDelegate _OnReceive_Player2;
        public UserContext player1;
        public UserContext player2;

        public TurnManager tm;


        public CardDealer(GameRoom gameRoom, ref GameBoard _GameBoard, ref ServerCommandProc SCP)
        {
            this.gameRoom = gameRoom;
            this.gameBoard = _GameBoard;
            this.SCP = SCP;
            this.player1 = this.gameRoom.userList[0];
            this.player2 = this.gameRoom.userList[1];

            //====[이벤트 등록]====
            _OnReceive_Player1 = new OnEventDelegate(OnReceive);
            this.player1.SetOnReceive(_OnReceive_Player1);

            _OnReceive_Player2 = new OnEventDelegate(OnReceive);
            this.player2.SetOnReceive(_OnReceive_Player2);

            //====[게임 준비]====
            ReadyForGame();
        }


        #region 게임 준비 과정
        string p1_card = null;
        string p2_card = null;
        string p1_character = null;
        string p2_character = null;

        private void ReadyForGame()
        {
            new DataBaseManager(ref gameBoard);  //카드 생성
            shuffle(gameBoard.P1_CardDeck, 10); //셔플
            shuffle(gameBoard.P2_CardDeck, 10); //셔플

            foreach (Card card in gameBoard.P1_CardDeck)
            {
                p1_card += card.Id + ",";
            }            
            foreach (Card card in gameBoard.P2_CardDeck)
            {
                p2_card += card.Id + ",";
            }

            p1_character = (gameBoard.P1_PlayerZone.Id+",");
            p2_character = (gameBoard.P2_PlayerZone.Id + ",");

            Console.WriteLine(p1_card);//DEBUG
            Console.WriteLine(p2_card);//DEBUG
            Console.WriteLine(p1_character);//DEBUG
            Console.WriteLine(p2_character);//DEBUG
        }

        private static Random _rnd = new Random();
        /// <summary>
        /// 카드 셔플
        /// </summary>
        /// <param name="cardDeckShuffle"></param>
        /// <param name="numberOfTimesToShuffle"></param>
        public void shuffle(List<Card> cardDeckShuffle, int numberOfTimesToShuffle)
        {
            List<Card> newList = new List<Card>();
            for (int i = 0; i < numberOfTimesToShuffle; i++)
            {
                while (cardDeckShuffle.Count > 0)
                {
                    int index = _rnd.Next(cardDeckShuffle.Count);
                    newList.Add(cardDeckShuffle[index]);
                    cardDeckShuffle.RemoveAt(index);
                }
                cardDeckShuffle.AddRange(newList);
                newList.Clear();
            }
        }

        #endregion 게임 준비 과정

        

        /// <summary>
        /// 수신 처리
        /// </summary>
        /// <param name="aContext"></param>
        public void OnReceive(UserContext aContext)
        {
            string data = aContext.DataFrame.ToString();
            Console.WriteLine(aContext.ClientAddress.ToString() + "=" + data);//DEBUG
            string[] command = data.Split(';');
            Delegate dg;
            SCP.CommandDic.TryGetValue(command[0], out dg);
            dg.DynamicInvoke(aContext, data);
        }

        /// <summary>
        /// 카드 덱 보내기
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="str"></param>
        public void sendCardDeck(UserContext aContext, string str)
        {
            string[] command = str.Split(';');
            if (command[0].Equals("OpponentCharacter_OK"))
            {
                if (aContext.Equals(player1))
                {
                    sendPacket_P1("YourCardDeck;" + p1_card);
                }
                else if (aContext.Equals(player2))
                {
                    sendPacket_P2("YourCardDeck;" + p2_card);
                }
                else
                {
                    Console.WriteLine("OpponentCharacter_OK : Error");    //DEBUG
                }
            }
            else if (command[0].Equals("YourCardDeck_OK"))
            {
                if (aContext.Equals(player1))
                {
                    sendPacket_P1("OpponentCardDeck;" + p2_card);
                }
                else if (aContext.Equals(player2))
                {
                    sendPacket_P2("OpponentCardDeck;" + p1_card);
                }
                else
                {
                    Console.WriteLine("YourCardDeck_OK : Error");    //DEBUG
                }
            }
            
        }

        /// <summary>
        /// 캐릭터 보내기
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="str"></param>
        public void sendCharacter(UserContext aContext, string str)
        {
            string[] command = str.Split(';');
            if (command[0].Equals("CreatedRoom_OK"))
            {
                if (aContext.Equals(player1))
                {
                    sendPacket_P1("YourCharacter;" + p1_character);
                }
                else if (aContext.Equals(player2))
                {
                    sendPacket_P2("YourCharacter;" + p2_character);
                }
                else
                {
                    Console.WriteLine("CreatedRoom_OK : Error");    //DEBUG
                }
            }
            else if (command[0].Equals("YourCharacter_OK"))
            {
                if (aContext.Equals(player1))
                {
                    sendPacket_P1("OpponentCharacter;" + p2_character);
                }
                else if (aContext.Equals(player2))
                {
                    sendPacket_P2("OpponentCharacter;" + p1_character);
                }
                else
                {
                    Console.WriteLine("YourCharacter_OK : Error");    //DEBUG
                }
            }
        }


        bool player1_ReadyForStart = false;
        bool player2_ReadyForStart = false;
        /// <summary>
        /// 게임 시작
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        public void StartTheGame(UserContext aContext, string command)
        {           
            if (aContext.Equals(player1) && player2_ReadyForStart)
            {
                Random rn = new Random();
                int number = rn.Next(0, 1000);
                if (number % 2 == 0)
                {
                    sendPacket_P1("Message;" + "상대방이 당신에게 우선권을 주었습니다. 시작하십시오!");
                    sendPacket_P2("Message;" + "당신은 너그러운 마음으로 상대방에게 우선권을 주었습니다.");
                    tm = new TurnManager()
                    {
                        Player1 = player1,
                        Player2 = player2,
                        Turn = player1
                    };
                    sendPacket_P1("YourTurn;");
                    sendPacket_P2("OpponentTurn;");
                }
                else
                {
                    sendPacket_P2("Message;" + "상대방이 당신에게 우선권을 주었습니다. 시작하십시오!");
                    sendPacket_P1("Message;" + "당신은 너그러운 마음으로 상대방에게 우선권을 주었습니다.");
                    tm = new TurnManager()
                    {
                        Player1 = player1,
                        Player2 = player2,
                        Turn = player2
                    };
                    sendPacket_P2("YourTurn;");
                    sendPacket_P1("OpponentTurn;");
                }
                
            }
            else if ((aContext.Equals(player2) && player1_ReadyForStart))
            {
                Random rn = new Random();
                int number = rn.Next(0, 1000);
                if (number % 2 == 0)
                {
                    sendPacket_P1("Message;" + "상대방이 당신에게 우선권을 주었습니다. 시작하십시오!");
                    sendPacket_P2("Message;" + "당신은 너그러운 마음으로 상대방에게 우선권을 주었습니다.");
                    tm = new TurnManager()
                    {
                        Player1 = player1,
                        Player2 = player2,
                        Turn = player1
                    };
                    sendPacket_P1("YourTurn;");
                    sendPacket_P2("OpponentTurn;");
                }
                else
                {
                    sendPacket_P2("Message;" + "상대방이 당신에게 우선권을 주었습니다. 시작하십시오!");
                    sendPacket_P1("Message;" + "당신은 너그러운 마음으로 상대방에게 우선권을 주었습니다.");
                    tm = new TurnManager()
                    {
                        Player1 = player1,
                        Player2 = player2,
                        Turn = player2
                    };
                    sendPacket_P2("YourTurn;");
                    sendPacket_P1("OpponentTurn;");
                }
            }
            else
            {
                if (aContext.Equals(player1))
                {
                    player1_ReadyForStart = true;
                    sendPacket_P1("Message;" + "레디! 상대방이 준비를 마치길 기다리는 중..");
                }
                else if (aContext.Equals(player2))
                {
                    player2_ReadyForStart = true;
                    sendPacket_P2("Message;" + "레디! 상대방이 준비를 마치길 기다리는 중..");
                }
            }
        }

        public void changeTurn(UserContext aContext, string command)
        {
            if (aContext.Equals(player1))
            {
                tm.Turn = player2;
                sendPacket_P2("Message;"+"당신의 턴입니다.");
                sendPacket_P2("YourTurn;");
                sendPacket_P1("OpponentTurn;");
            }
            else if (aContext.Equals(player2))
            {
                tm.Turn = player1;
                sendPacket_P1("Message;" + "당신의 턴입니다.");
                sendPacket_P1("YourTurn;");
                sendPacket_P2("OpponentTurn;");
            }
        }

        public void firstDistribute(UserContext aContext, string command)
        {
            if (aContext.Equals(player1))
            {
                moveZone(gameBoard.P1_CardDeck[0], GameBoard.PLAYER1_HANDSZONE);
                sendPacket_P1("MoveCard;" + MY_CARDDECK + ";" + "0;" + MY_HANDSZONE + ";");
                sendPacket_P1("MoveCard;" + OPPONENT_CARDDECK + ";" + "0;" + OPPONENT_HANDSZONE + ";");
                moveZone(gameBoard.P1_CardDeck[0], GameBoard.PLAYER1_HANDSZONE);
                sendPacket_P1("MoveCard;" + MY_CARDDECK+";" + "0;" + MY_HANDSZONE + ";");
                sendPacket_P1("MoveCard;" + OPPONENT_CARDDECK + ";" + "0;" + OPPONENT_HANDSZONE + ";");
                moveZone(gameBoard.P1_CardDeck[0], GameBoard.PLAYER1_HANDSZONE);
                sendPacket_P1("MoveCard;" + MY_CARDDECK + ";" + "0;" + MY_HANDSZONE + ";");
                sendPacket_P1("MoveCard;" + OPPONENT_CARDDECK + ";" + "0;" + OPPONENT_HANDSZONE + ";");
            }
            else if (aContext.Equals(player2))
            {
                moveZone(gameBoard.P2_CardDeck[0], GameBoard.PLAYER2_HANDSZONE);
                sendPacket_P2("MoveCard;" + MY_CARDDECK + ";" + "0;" + MY_HANDSZONE + ";");
                sendPacket_P2("MoveCard;" + OPPONENT_CARDDECK + ";" + "0;" + OPPONENT_HANDSZONE + ";");
                moveZone(gameBoard.P2_CardDeck[0], GameBoard.PLAYER2_HANDSZONE);
                sendPacket_P2("MoveCard;" + MY_CARDDECK + ";" + "0;" + MY_HANDSZONE + ";");
                sendPacket_P2("MoveCard;" + OPPONENT_CARDDECK + ";" + "0;" + OPPONENT_HANDSZONE + ";");
                moveZone(gameBoard.P2_CardDeck[0], GameBoard.PLAYER2_HANDSZONE);
                sendPacket_P2("MoveCard;" + MY_CARDDECK + ";" + "0;" + MY_HANDSZONE + ";");
                sendPacket_P2("MoveCard;" + OPPONENT_CARDDECK + ";" + "0;" + OPPONENT_HANDSZONE + ";");
            }
        }

        private void sendPacket_P1(string str)
        {
            Console.WriteLine("SEND=="+str);//DEBUG
            this.gameRoom.userList[0].Send(str);
        }
        private void sendPacket_P2(string str)
        {
            Console.WriteLine("SEND==" + str);//DEBUG
            this.gameRoom.userList[1].Send(str);
        }


       

        
        /// <summary>
        /// 카드 존 이동
        /// </summary>
        /// <param name="card_con"></param> 카드
        /// <param name="position"></param> 이동 목적지
        public void moveZone(Card card, int position)
        {
            //=====[기존 리스트 탐색]=====
            int pre_position = 0;

            if (gameBoard.P1_CardDeck.Contains(card))
            {
                pre_position = GameBoard.PLAYER1_CARDDECK;
            }
            else if (gameBoard.P2_CardDeck.Contains(card))
            {
                pre_position = GameBoard.PLAYER2_CARDDECK;
            }
            else if (gameBoard.P1_HandsZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER1_HANDSZONE;
            }
            else if (gameBoard.P2_HandsZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER2_HANDSZONE;
            }
            else if (gameBoard.P1_WarZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER1_WARZONE;
            }
            else if (gameBoard.P2_WarZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER2_WARZONE;
            }
            else if (gameBoard.P1_ManaZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER1_MANAZONE;
            }
            else if (gameBoard.P2_ManaZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER2_MANAZONE;
            }
            else if (gameBoard.P1_TombZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER1_TOMBZONE;
            }
            else if (gameBoard.P2_TombZone.Contains(card))
            {
                pre_position = GameBoard.PLAYER2_TOMBZONE;
            }
            else if (gameBoard.P1_PlayerZone.Equals(card))
            {
                pre_position = GameBoard.PLAYER1_PLAYERZONE;
            }
            else if (gameBoard.P2_PlayerZone.Equals(card))
            {
                pre_position = GameBoard.PLAYER2_PLAYERZONE;
            }
            else
            {
                pre_position = 0;
            }

            //=====[기본 리스트에서 삭제]=====
            switch (pre_position)
            {
                case GameBoard.PLAYER1_CARDDECK:
                    gameBoard.P1_CardDeck.Remove(card);
                    break;
                case GameBoard.PLAYER2_CARDDECK:
                    gameBoard.P2_CardDeck.Remove(card);
                    break;
                case GameBoard.PLAYER1_HANDSZONE:
                    gameBoard.P1_HandsZone.Remove(card);
                    break;
                case GameBoard.PLAYER2_HANDSZONE:
                    gameBoard.P2_HandsZone.Remove(card);
                    break;
                case GameBoard.PLAYER1_WARZONE:
                    gameBoard.P1_WarZone.Remove(card);
                    break;
                case GameBoard.PLAYER2_WARZONE:
                    gameBoard.P2_WarZone.Remove(card);
                    break;
                case GameBoard.PLAYER1_MANAZONE:
                    gameBoard.P1_ManaZone.Remove(card);
                    break;
                case GameBoard.PLAYER2_MANAZONE:
                    gameBoard.P2_ManaZone.Remove(card);
                    break;
                case GameBoard.PLAYER1_TOMBZONE:
                    gameBoard.P1_TombZone.Remove(card);
                    break;
                case GameBoard.PLAYER2_TOMBZONE:
                    gameBoard.P2_TombZone.Remove(card);
                    break;
                case GameBoard.PLAYER1_PLAYERZONE:
                    judgment_Winner("PLAYER2");
                    break;
                case GameBoard.PLAYER2_PLAYERZONE:
                    judgment_Winner("PLAYER1");
                    break;
            }
            //=====[새로운 포지션 배치]=====
            card.position = position;
            switch (position)
            {
                case GameBoard.PLAYER1_CARDDECK:
                    gameBoard.P1_CardDeck.Add(card);
                    break;
                case GameBoard.PLAYER2_CARDDECK:
                    gameBoard.P2_CardDeck.Add(card);
                    break;
                case GameBoard.PLAYER1_HANDSZONE:
                    gameBoard.P1_HandsZone.Add(card);
                    break;
                case GameBoard.PLAYER2_HANDSZONE:
                    gameBoard.P2_HandsZone.Add(card);
                    break;
                case GameBoard.PLAYER1_WARZONE:
                    gameBoard.P1_WarZone.Add(card);
                    break;
                case GameBoard.PLAYER2_WARZONE:
                    gameBoard.P2_WarZone.Add(card);
                    break;
                case GameBoard.PLAYER1_MANAZONE:
                    gameBoard.P1_ManaZone.Add(card);
                    break;
                case GameBoard.PLAYER2_MANAZONE:
                    gameBoard.P2_ManaZone.Add(card);
                    break;
                case GameBoard.PLAYER1_TOMBZONE:
                    gameBoard.P1_TombZone.Add(card);
                    break;
                case GameBoard.PLAYER2_TOMBZONE:
                    gameBoard.P2_TombZone.Add(card);
                    break;
            }
        }

        //======[게임 승패 결정]=====
        private void judgment_Winner(String winner)
        {
        }
    }
}
