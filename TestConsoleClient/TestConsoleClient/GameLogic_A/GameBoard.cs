using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameBoard
    {
        //=====[ 카드 덱 ]=====
        public static List<Card> P1_CardDeck = new List<Card>();
        public static List<Card> P2_CardDeck = new List<Card>();
        //=====[ 플레이어 핸드 존]=====
        public static List<Card> P1_HandsZone = new List<Card>();
        public static List<Card> P2_HandsZone = new List<Card>();
        //=====[ 전장 ]=====
        public static List<Card> P1_WarZone = new List<Card>();
        public static List<Card> P2_WarZone = new List<Card>();
        //=====[ 마나 존 ]=====
        public static List<Card> P1_ManaZone = new List<Card>();
        public static List<Card> P2_ManaZone = new List<Card>();
        //=====[ 무덤 존 ]=====
        public static List<Card> P1_TombZone = new List<Card>();
        public static List<Card> P2_TombZone = new List<Card>();

        /**********************************************
        ***********[[ Singleton 적용 ]]****************
        ***********************************************/
        static GameBoard GameBoardInstance = null;
        static readonly object padlock = new object();

        public static GameBoard Instance
        {
            get
            {
                lock (padlock)
                {
                    if (GameBoardInstance == null)
                    {
                        GameBoardInstance = new GameBoard();
                    }
                    return GameBoardInstance;
                }
            }
        }






      
    }
}
