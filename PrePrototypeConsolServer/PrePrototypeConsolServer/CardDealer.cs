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

        #region 각 플레이어 데이터 전송
        private void sendPacket_P1(string str)
        {
            Console.WriteLine("SEND==" + str);//DEBUG
            this.gameRoom.userList[0].Send(str);
        }
        private void sendPacket_P2(string str)
        {
            Console.WriteLine("SEND==" + str);//DEBUG
            this.gameRoom.userList[1].Send(str);
        }
        #endregion 각 플레이어 데이터 전송

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

            p1_character = (gameBoard.P1_PlayerZone.Id + ",");
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

        #region 데이터 수신 관련
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
        #endregion 데이터 수신 관련


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

        #region 게임 시작 처리

        public void firstDistribute(UserContext aContext, string command)
        {
            if (aContext.Equals(player1))
            {
                sendPacket_P1("FirstDistribute;");
            }
            else if(aContext.Equals(player2)){ 
                sendPacket_P2("FirstDistribute;");
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
        #endregion 게임 시작 처리

        /// <summary>
        /// 턴 바꾸기
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        public void changeTurn(UserContext aContext, string command)
        {
            if (aContext.Equals(player1))
            {
                tm.Turn = player2;
                sendPacket_P2("Message;" + "당신의 턴입니다.");
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

            distributeCard();   //카드 분배
        }

        /// <summary>
        /// 카드 분배
        /// </summary>
        public void distributeCard()
        {
            sendPacket_P1("Distribute;");
            sendPacket_P2("Distribute;");
        }
        
        internal void packetProc(UserContext aContext, string command, string what)
        {
            string[] position = command.Split(';');

            string myMessage = what+";" + position[1] + ";" +
                position[2] + ";";
            string opponentMessage = what+";" +
                (Convert.ToInt32(position[1]) + 1) + ";" +
                position[2] + ";";

            if (aContext.Equals(player1))
            {
                sendPacket_P1(myMessage);
                sendPacket_P2(opponentMessage);
                Console.WriteLine(myMessage);//DEBUG
                Console.WriteLine(opponentMessage);//DEBUG
            }
            else if (aContext.Equals(player2))
            {
                sendPacket_P1(opponentMessage);
                sendPacket_P2(myMessage);
                Console.WriteLine(opponentMessage);//DEBUG
                Console.WriteLine(myMessage);//DEBUG
            }
        }
        /// <summary>
        /// 자원 생산
        /// </summary>
        /// <param name="aContext"></param>
        /// <param name="command"></param>
        internal void MatchCard(UserContext aContext, string command)
        {
            string[] position = command.Split(';');

            string myMessage = "MatchCard;"
                + position[1] + ";" 
                + position[2] + ";"
                + position[3] + ";"
                + position[4] + ";";

            for (int i = 0; i < position.Length; i++)
            {
                if (position[i].Equals("200"))
                {
                    position[i] = "100";
                }else if (position[i].Equals("100"))
                {
                    position[i] = "200";
                }

                if (i != 2 && i != 4)
                {
                    if (position[i].Equals("5"))
                    {
                        position[i] = "6";
                    }
                    else if (position[i].Equals("6"))
                    {
                        position[i] = "5";
                    }
                }
            }
                
            string opponentMessage = "MatchCard;" 
                + position[1] + ";" 
                + position[2] + ";"
                + position[3] + ";"
                + position[4] + ";";

            if (aContext.Equals(player1))
            {
                sendPacket_P1(myMessage);
                sendPacket_P2(opponentMessage);
                Console.WriteLine(myMessage);//DEBUG
                Console.WriteLine(opponentMessage);//DEBUG
            }
            else if (aContext.Equals(player2))
            {
                sendPacket_P1(opponentMessage);
                sendPacket_P2(myMessage);
                Console.WriteLine(opponentMessage);//DEBUG
                Console.WriteLine(myMessage);//DEBUG
            }
        }



        //======[게임 승패 결정]=====
        private void judgment_Winner(String winner)
        {
        }
        
    }
}
