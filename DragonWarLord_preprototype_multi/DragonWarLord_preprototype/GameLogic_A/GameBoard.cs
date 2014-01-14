using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonWarLord_preprototype;

namespace WarLord_Server_GUI.GameLogic_A
{
    class GameBoard
    {
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

        //=====[ 카드 덱 ]=====
        public static List<Card_Control> My_CardDeck = new List<Card_Control>();
        public static List<Card_Control> Opponent_CardDeck = new List<Card_Control>();
        //=====[플레이어]=====
        public static Card_Control My_PlayerZone = new Card_Control();
        public static Card_Control Opponent_PlayerZone = new Card_Control();
        //=====[ 플레이어 핸드 존]=====
        public static List<Card_Control> My_HandsZone = new List<Card_Control>();
        public static List<Card_Control> Opponent_HandsZone = new List<Card_Control>();
        //=====[ 전장 ]=====
        public static List<Card_Control> My_WarZone = new List<Card_Control>();
        public static List<Card_Control> Opponent_WarZone = new List<Card_Control>();
        //=====[ 마나 존 ]=====
        public static List<Card_Control> My_ManaZone = new List<Card_Control>();
        public static List<Card_Control> Opponent_ManaZone = new List<Card_Control>();
        //=====[ 무덤 존 ]=====
        public static List<Card_Control> My_TombZone = new List<Card_Control>();
        public static List<Card_Control> Opponent_TombZone = new List<Card_Control>();



        #region 싱글톤
        /// <summary>
        /// 싱글톤
        /// </summary>
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
        #endregion 싱글톤
    }
}
