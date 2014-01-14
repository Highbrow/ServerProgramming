using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrePrototypeConsolServer
{
    class GamePlayManager
    {
        public const int PLAYER1_CARDDECK = 1;
        public const int PLAYER2_CARDDECK = 2;
        public const int PLAYER1_HANDSZONE = 3;
        public const int PLAYER2_HANDSZONE = 4;
        public const int PLAYER1_WARZONE = 5;
        public const int PLAYER2_WARZONE = 6;
        public const int PLAYER1_MANAZONE = 7;
        public const int PLAYER2_MANAZONE = 8;
        public const int PLAYER1_TOMBZONE = 9;
        public const int PLAYER2_TOMBZONE = 10;
        public const int PLAYER1_PLAYERZONE = 100;
        public const int PLAYER2_PLAYERZONE = 200;

        //public delegate void dchangeStatus(Card card_con);

        //public event dchangeStatus cardPop;
        //public event dchangeStatus endturn;
        //public event dchangeStatus cardDestruction;

        private GamePlayManager()
        {
            //cardPop = new dchangeStatus(cardPop_EventProc);
            //endturn = new dchangeStatus(endturn_EventProc);
            //cardDestruction = new dchangeStatus(destruction_EventProc);
        }

        #region 싱글톤
        static GamePlayManager GamePlayManagerInstance = null;
        static readonly object padlock = new object();
        /// <summary>
        /// Singleton 적용
        /// </summary>
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

        #endregion
    }
}
