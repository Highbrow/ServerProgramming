using DragonWarLord_preprototype.CommandLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;

namespace DragonWarLord_preprototype
{
    class CardDealer
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

        internal void inputCard(string[] p, int position)
        {
            if (position == PLAYER1_CARDDECK)
            {
                GameBoard.P1_CardDeck = DataBaseManager.inputCard(p);
            }else if (position == PLAYER2_CARDDECK)
            {
                GameBoard.P2_CardDeck = DataBaseManager.inputCard(p);
            }
            else
            {
                MessageBox.Show("카드 생성위치가 잘못되었습니다.");
            }
        }
        internal void makeMainPlayer(string[] p, int position)
        {
            if (position == PLAYER1_PLAYERZONE)
            {
                GameBoard.P1_PlayerZone = DataBaseManager.makeMainPlayer(p[0]);
            }
            else if (position == PLAYER2_PLAYERZONE)
            {
                GameBoard.P2_PlayerZone = DataBaseManager.makeMainPlayer(p[0]);
            }
            else
            {
                MessageBox.Show("카드 생성위치가 잘못되었습니다.");
            }
        }

        internal void firstDistribute()
        {
            throw new NotImplementedException();
        }

        internal void distribute()
        {
            throw new NotImplementedException();
        }

        internal void GameDefaultSetting()
        {
            throw new NotImplementedException();
        }

        #region 싱글톤
        static CardDealer CardDealerInstance = null;
        static readonly object padlock = new object();
        /// <summary>
        /// Singleton 적용
        /// </summary>
        public static CardDealer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (CardDealerInstance == null)
                    {
                        CardDealerInstance = new CardDealer();
                    }
                    return CardDealerInstance;
                }
            }
        }

        #endregion
    }
}
