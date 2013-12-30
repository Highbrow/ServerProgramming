using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarLord_Server_GUI.GameLogic_A;

namespace WarLord_Server_GUI.GameLogic_B
{
    class GamePlayManager
    {
        public TestConsoleClient.MainForm mainForm { get; set; }
        public bool program_run = true;
        public bool thisturn = true; // true : 1,  false : 2

        public GamePlayManager()
        {
            inputCard();
            firstDistribute();
            distribute();
        }
        //=====[ Match ]=====
        public int[] MatchCard(Card selectCard, Card tagetCard)
        {
            int[] result = new int[2];
            result[0] = selectCard.Hp -= tagetCard.Ap;  // 본인 생명력
            result[1] = tagetCard.Hp -= selectCard.Ap;  // 대상 생명력

            return result;  // 0 : 본인 생명력, 1: 대상 생명력
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
                    GameBoard.P1_CardDeck.Add(new Card()
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
                    GameBoard.P2_CardDeck.Add(new Card()
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

            shuffle(GameBoard.P1_CardDeck, 10);
            shuffle(GameBoard.P2_CardDeck, 10);

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


        //=====[ 처음 카드 지급 ]=====
        private void firstDistribute()
        {
            GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
            GameBoard.P1_CardDeck.RemoveAt(0);
            GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
            GameBoard.P1_CardDeck.RemoveAt(0);
            GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
            GameBoard.P1_CardDeck.RemoveAt(0);

            GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
            GameBoard.P2_CardDeck.RemoveAt(0);
            GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
            GameBoard.P2_CardDeck.RemoveAt(0);
            GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
            GameBoard.P2_CardDeck.RemoveAt(0);

            if (thisturn)
            {
                GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
                GameBoard.P2_CardDeck.RemoveAt(0);
                GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
                GameBoard.P2_CardDeck.RemoveAt(0);
            }
            else
            {
                GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
                GameBoard.P1_CardDeck.RemoveAt(0);
                GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
                GameBoard.P1_CardDeck.RemoveAt(0);
            }
        }

        //=====[ 카드 분배 ]=====
        public void distribute()
        {
            if (thisturn)
            {
                GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
                GameBoard.P1_CardDeck.RemoveAt(0);
                GameBoard.P1_HandsZone.Add(GameBoard.P1_CardDeck[0]);
                GameBoard.P1_CardDeck.RemoveAt(0);
            }
            else
            {
                GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
                GameBoard.P2_CardDeck.RemoveAt(0);
                GameBoard.P2_HandsZone.Add(GameBoard.P2_CardDeck[0]);
                GameBoard.P2_CardDeck.RemoveAt(0);
            }
        }

        /**********************************************
       ***********[[ Singleton 적용 ]]****************
       ***********************************************/
        static GamePlayManager GamePlayManagerInstance = null;
        static readonly object padlock = new object();

        public static GamePlayManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (GamePlayManagerInstance == null)
                    {
                        GamePlayManagerInstance = new GamePlayManager();
                    }
                    return GamePlayManagerInstance;
                }
            }
        }

        
    }
}
