using DragonWarLord_preprototype.CommandLibrary;
using DragonWarLord_preprototype.GameLogic_B;
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
        public DragonWarLord_preprototype.MainForm mainForm { get; set; }

        /// <summary>
        /// 카드 생성
        /// </summary>
        /// <param name="p"></param>
        /// <param name="position"></param>
        internal void inputCard(string[] p, int position)
        {
            if (position == GameBoard.MY_CARDDECK)
            {
                DataBaseManager.inputCard(p, true);
            }else if (position == GameBoard.OPPONENT_CARDDECK)
            {
                DataBaseManager.inputCard(p, false);
            }
            else
            {
                MessageBox.Show("카드 생성위치가 잘못되었습니다.");
            }
        }

        /// <summary>
        /// 플레이어 생성
        /// </summary>
        /// <param name="p"></param>
        /// <param name="position"></param>
        internal void makeMainPlayer(string[] p, int position)
        {
            if (position == GameBoard.MY_PLAYERZONE)
            {
                GameBoard.My_PlayerZone = DataBaseManager.makeMainPlayer(p[0]);
                mainForm.add_My_Player(GameBoard.My_PlayerZone);
            }
            else if (position == GameBoard.OPPONENT_PLAYERZONE)
            {
                GameBoard.Opponent_PlayerZone = DataBaseManager.makeMainPlayer(p[0]);
                mainForm.add_Opponent_Player(GameBoard.Opponent_PlayerZone);
            }
            else
            {
                MessageBox.Show("카드 생성위치가 잘못되었습니다.");
            }
        }

        internal void distribute()
        {
            throw new NotImplementedException();
        }

        internal void GameDefaultSetting()
        {
            throw new NotImplementedException();
        }



        internal void moveCard(string[] p)
        {
            Card_Control card = null;
            switch (Convert.ToInt32(p[1]))
            {
                case GameBoard.MY_CARDDECK:
                    card = GameBoard.My_CardDeck[Convert.ToInt32(p[2])];
                    GameBoard.My_CardDeck.RemoveAt(Convert.ToInt32(p[2]));
                    break;
                case GameBoard.OPPONENT_CARDDECK:
                    card = GameBoard.Opponent_CardDeck[Convert.ToInt32(p[2])];
                    GameBoard.Opponent_CardDeck.RemoveAt(Convert.ToInt32(p[2]));
                    break;
                case GameBoard.MY_HANDSZONE:
                    card = GameBoard.My_HandsZone[Convert.ToInt32(p[2])];
                    GameBoard.My_HandsZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_My_Hands(card);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    card = GameBoard.Opponent_HandsZone[Convert.ToInt32(p[2])];
                    GameBoard.Opponent_HandsZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_Opponent_Hands(card);
                    break;
                case GameBoard.MY_WARZONE:
                    card = GameBoard.My_WarZone[Convert.ToInt32(p[2])];
                    GameBoard.My_WarZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_My_WarZone(card);
                    break;
                case GameBoard.OPPONENT_WARZONE:
                    card = GameBoard.Opponent_WarZone[Convert.ToInt32(p[2])];
                    GameBoard.Opponent_WarZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_Opponent_WarZone(card);
                    break;
                case GameBoard.MY_MANAZONE:
                    card = GameBoard.My_ManaZone[Convert.ToInt32(p[2])];
                    GameBoard.My_ManaZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_My_ManaZone(card);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    card = GameBoard.Opponent_ManaZone[Convert.ToInt32(p[2])];
                    GameBoard.Opponent_ManaZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_Opponent_ManaZone(card);
                    break;
                case GameBoard.MY_TOMBZONE:
                    card = GameBoard.My_TombZone[Convert.ToInt32(p[2])];
                    GameBoard.My_TombZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_My_TombZone(card);
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    card = GameBoard.Opponent_TombZone[Convert.ToInt32(p[2])];
                    GameBoard.Opponent_TombZone.RemoveAt(Convert.ToInt32(p[2]));
                    mainForm.remove_Opponent_TombZone(card);
                    break;
            }

            switch(Convert.ToInt32(p[3])){
                case GameBoard.MY_CARDDECK:
                    GameBoard.My_CardDeck.Add(card);
                    break;
                case GameBoard.OPPONENT_CARDDECK:
                    GameBoard.Opponent_CardDeck.Add(card);
                    break;
                case GameBoard.MY_HANDSZONE:
                    GameBoard.My_HandsZone.Add(card);
                    mainForm.add_My_Hands(card);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Add(card);
                    mainForm.add_Opponent_Hands(card);
                    break;
                case GameBoard.MY_WARZONE:
                    GameBoard.My_WarZone.Add(card);
                    mainForm.add_My_WarZone(card);
                    break;
                case GameBoard.OPPONENT_WARZONE:
                    GameBoard.Opponent_WarZone.Add(card);
                    mainForm.add_Opponent_WarZone(card);
                    break;
                case GameBoard.MY_MANAZONE:
                    GameBoard.My_ManaZone.Add(card);
                    mainForm.add_My_ManaZone(card);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Add(card);
                    mainForm.add_Opponent_ManaZone(card);
                    break;
                case GameBoard.MY_TOMBZONE:
                    GameBoard.My_TombZone.Add(card);
                    mainForm.add_My_TombZone(card);
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    GameBoard.Opponent_TombZone.Add(card);
                    mainForm.add_Opponent_TombZone(card);
                    break;
            }
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
