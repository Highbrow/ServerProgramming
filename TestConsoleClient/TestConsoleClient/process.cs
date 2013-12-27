﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.GameLogic_A;

namespace TestConsoleClient
{
    class process
    {
        public GameBoard gb;
        public Random r;
        public bool program_run = true;
        public bool thisturn = true; // true : 1,  false : 2
        public process()
        {
            gb = new GameBoard();
            r = new Random();
            gameInit();
        }

        private void gameInit()
        {
            inputCard();    //카드 생성
            firstDistribute();  //초기 카드 지급
            distribute();   //카드 분배

            /*
            while (program_run)
            {
                displayCard();
                inputdata();
                Console.Clear();
            }*/
        }

        private void putCard(int hands)
        {
            if (thisturn)
            {
                gb.P1_WarZone.Add(gb.P1_HandsZone[hands]);
                gb.P1_HandsZone.RemoveAt(hands);
            }
            else
            {
                gb.P2_WarZone.Add(gb.P2_HandsZone[hands]);
                gb.P2_HandsZone.RemoveAt(hands);
            }

        }
        private void matching(int select, int target)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("<<<<< 전투를 시작합니다 >>>>>");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            if (thisturn)
            {
                displayCard(gb.P1_WarZone[select]);
                Console.Write("vs");
                displayCard(gb.P2_WarZone[target]);
                Console.WriteLine();

                int[] result = MatchCard(gb.P1_WarZone[select], gb.P2_WarZone[target]);

                gb.P1_WarZone[select].Hp = result[0];
                gb.P2_WarZone[target].Hp = result[1];

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[[[ 전투 결과 ]]]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;

                displayCard(gb.P1_WarZone[select]);
                Console.Write("vs");
                displayCard(gb.P2_WarZone[target]);
                Console.WriteLine();


                if (result[0] <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("player1의 카드가 파괴되었습니다.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    gb.P1_WarZone.RemoveAt(select);
                }
                if (result[1] <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("player2의 카드가 파괴되었습니다.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    gb.P2_WarZone.RemoveAt(target);
                }

            }
            else
            {
                displayCard(gb.P2_WarZone[select]);
                Console.Write("vs");
                displayCard(gb.P1_WarZone[target]);
                Console.WriteLine();

                int[] result = MatchCard(gb.P2_WarZone[select], gb.P1_WarZone[target]);

                gb.P2_WarZone[select].Hp = result[0];
                gb.P1_WarZone[target].Hp = result[1];

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[[[ 전투 결과 ]]]");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;

                displayCard(gb.P2_WarZone[select]);
                Console.Write("vs");
                displayCard(gb.P1_WarZone[target]);
                Console.WriteLine();


                if (result[0] <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("player2의 카드가 파괴되었습니다.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    gb.P2_WarZone.RemoveAt(select);
                }
                if (result[1] <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("player1의 카드가 파괴되었습니다.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    gb.P1_WarZone.RemoveAt(target);
                }
            }
            Console.ReadLine();
        }
        private void displayCard(Card c)
        {
            Console.Write("[" + c.Ap + ":" + c.Hp + "]");
        }
        public int[] MatchCard(Card selectCard, Card tagetCard)
        {
            int[] result = new int[2];
            result[0] = selectCard.Hp -= tagetCard.Ap;  // 본인 생명력
            result[1] = tagetCard.Hp -= selectCard.Ap;  // 대상 생명력

            return result;  // 0 : 본인 생명력, 1: 대상 생명력
        }

        private void inputdata()
        {
            string command = Console.ReadLine();
            if (command.Equals("t"))
            {
                if (thisturn)
                {
                    thisturn = false;
                }
                else
                {
                    thisturn = true;
                }
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("턴을 종료합니다.");
                Console.ReadLine();
                Console.BackgroundColor = ConsoleColor.Black;
                distribute();   // 카드 분배
            }
            else if (command.Equals("a"))
            {      //======[ 공격 명령 ]======
                if (gb.P1_WarZone.Count <= 0 || gb.P2_WarZone.Count <= 0)
                {
                    Console.WriteLine("공격가능한 카드(자신or대상)가 없습니다.");
                    Console.ReadLine();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("카드를 선택하세요(index 번호) >> ");
                    int prekey = Convert.ToInt32(Console.ReadLine());

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("대상을 선택하세요(index 번호) >> ");
                    int key = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;
                    matching(prekey, key);
                }
            }
            else if (command.Equals("q"))
            {
                program_run = false;
            }
            else if (command.Equals("h"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("카드를 선택하세요(index 번호) >> ");
                int prekey = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Gray;

                putCard(prekey);
            }
        }

        private void firstDistribute()
        {
            gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
            gb.P1_CardDeck.RemoveAt(0);
            gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
            gb.P1_CardDeck.RemoveAt(0);
            gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
            gb.P1_CardDeck.RemoveAt(0);

            gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
            gb.P2_CardDeck.RemoveAt(0);
            gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
            gb.P2_CardDeck.RemoveAt(0);
            gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
            gb.P2_CardDeck.RemoveAt(0);

            if (thisturn)
            {
                gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
                gb.P2_CardDeck.RemoveAt(0);
                gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
                gb.P2_CardDeck.RemoveAt(0);
            }
            else
            {
                gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
                gb.P1_CardDeck.RemoveAt(0);
                gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
                gb.P1_CardDeck.RemoveAt(0);
            }
        }

        private void distribute()
        {
            if (thisturn)
            {
                gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
                gb.P1_CardDeck.RemoveAt(0);
                gb.P1_HandsZone.Add(gb.P1_CardDeck[0]);
                gb.P1_CardDeck.RemoveAt(0);
            }
            else
            {
                gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
                gb.P2_CardDeck.RemoveAt(0);
                gb.P2_HandsZone.Add(gb.P2_CardDeck[0]);
                gb.P2_CardDeck.RemoveAt(0);
            }
        }

        private void displayCard()
        {
            Console.WriteLine("[Player1 Deck] : " + gb.P1_CardDeck.Count);
            Console.WriteLine("[Player2 Deck] : " + gb.P2_CardDeck.Count);

            Console.WriteLine("########################################################");

            Console.WriteLine();
            Console.WriteLine();

            //=====[ Player2 Hands Zone ]=====
            Console.Write("[" + "Player2" + ":Hands] = ");

            foreach (Card c in gb.P2_HandsZone)
            {
                Console.Write("[" + c.Ap + ":" + c.Hp + "]");
            }
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            //=====[ Player2 war Zone ]=======
            Console.Write("[" + "Player2" + ":warzone] = ");
            foreach (Card c in gb.P2_WarZone)
            {
                if (c != null)
                {
                    Console.Write("[" + c.Ap + ":" + c.Hp + "]");
                }
            }
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("=========================================================");

            //=====[ Player1 war Zone ]=======
            Console.Write("[" + "Player1" + ":warzone] = ");
            foreach (Card c in gb.P1_WarZone)
            {
                if (c != null)
                {
                    Console.Write("[" + c.Ap + ":" + c.Hp + "]");
                }
            }
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            //=====[ Player1 Hands Zone ]=======
            Console.Write("[" + "Player1" + ":Hands] = ");
            foreach (Card c in gb.P1_HandsZone)
            {
                Console.Write("[" + c.Ap + ":" + c.Hp + "]");
            }
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine();
            Console.WriteLine();

            if (thisturn)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Player1");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("]");
                Console.Write("의 차례입니다.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("턴종료:t, 공격:a, 카드내기:h, 게임종료:q");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(")");
                Console.Write(" >>>>");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Player2");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("]");
                Console.Write("의 차례입니다.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("턴종료:t, 공격:a, 카드내기:h, 게임종료:q");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(")");
                Console.Write(" >>>>");
            }

        }

        //=====[ 카드 섞기 ]=====
        private static Random _rnd = new Random();

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

        public void inputCard()
        {
            string strExcelFile = @"D:\Highbrow\GitHub\Warlord\ServerProgramming\TestConsoleClient\TestConsoleClient\res\card.xlsx";
            string strConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                     + strExcelFile
                                     + ";Extended Properties='Excel 8.0;HDR=YES'";
            OleDbConnection conn = new OleDbConnection(strConnStr);
            conn.Open();

            // 엑셀로부터 데이타 읽기
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int i = 0; i < Convert.ToInt32(dr[10]); i++)
                {
                    gb.P1_CardDeck.Add(new Card()
                    {
                        Name = dr[1].ToString(),
                        Attribute = dr[2].ToString(),
                        Type = dr[3].ToString(),
                        Class = dr[4].ToString(),
                        Species = dr[5].ToString(),
                        Consumption = dr[6].ToString(),
                        Ap = Convert.ToInt32(dr[7]),
                        Hp = Convert.ToInt32(dr[8]),
                        Rp = Convert.ToInt32(dr[9]),
                        Limited_amount = Convert.ToInt32(dr[10]),
                        Skill = dr[11].ToString(),
                        Information = dr[12].ToString(),
                    });
                    gb.P2_CardDeck.Add(new Card()
                    {
                        Name = dr[1].ToString(),
                        Attribute = dr[2].ToString(),
                        Type = dr[3].ToString(),
                        Class = dr[4].ToString(),
                        Species = dr[5].ToString(),
                        Consumption = dr[6].ToString(),
                        Ap = Convert.ToInt32(dr[7]),
                        Hp = Convert.ToInt32(dr[8]),
                        Rp = Convert.ToInt32(dr[9]),
                        Limited_amount = Convert.ToInt32(dr[10]),
                        Skill = dr[11].ToString(),
                        Information = dr[12].ToString(),
                    });
                }
            }

            conn.Close();

            shuffle(gb.P1_CardDeck, 10);
            shuffle(gb.P2_CardDeck, 10);

        }


    }
}