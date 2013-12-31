using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsoleClient;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameBoard
    {
        //=====[ 카드 덱 ]=====
        public static List<Card_Control> P1_CardDeck = new List<Card_Control>();
        public static List<Card_Control> P2_CardDeck = new List<Card_Control>();
        //=====[ 플레이어 핸드 존]=====
        public static List<Card_Control> P1_HandsZone = new List<Card_Control>();
        public static List<Card_Control> P2_HandsZone = new List<Card_Control>();
        //=====[ 전장 ]=====
        public static List<Card_Control> P1_WarZone = new List<Card_Control>();
        public static List<Card_Control> P2_WarZone = new List<Card_Control>();
        //=====[ 마나 존 ]=====
        public static List<Card_Control> P1_ManaZone = new List<Card_Control>();
        public static List<Card_Control> P2_ManaZone = new List<Card_Control>();
        //=====[ 무덤 존 ]=====
        public static List<Card_Control> P1_TombZone = new List<Card_Control>();
        public static List<Card_Control> P2_TombZone = new List<Card_Control>();

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
