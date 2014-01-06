using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.GameLogic_B;

namespace TestConsoleClient.GameLogic_B
{
    class GameSkillManager
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


        public Card_Control skill_card = null;       

        public bool skill_16 = false;
        public bool skill_8_p1 = false;
        public bool skill_8_p2 = false;
        public bool skill_21 = false;



        public ConcurrentDictionary<string, Delegate> SkillDictionary = new ConcurrentDictionary<string, Delegate>();
        public delegate void skillDelegate(Card_Control card_con);

        public bool canTargetSkill21(Card_Control card_con)
        {
            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.P2_WarZone.Contains(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (GameBoard.P1_WarZone.Contains(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool canTargetSkill16(Card_Control card_con)
        {
            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.P2_WarZone.Contains(card_con) || GameBoard.P2_PlayerZone.Equals(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (GameBoard.P1_WarZone.Contains(card_con) || GameBoard.P1_PlayerZone.Equals(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        protected void skill_1_vamfire(Card_Control card_con)
        {
            
        }
        protected void skill_8_BigDevil(Card_Control card_con)
        {
            GamePlayManager.Instance.tombCard = card_con;
            if (GamePlayManager.Instance.thisturn)
            {
                skill_8_p1 = true;
            }
            else
            {
                skill_8_p2 = true;
            }
            
        }
        protected void skill_9_vamfire(Card_Control card_con)
        {
            if (GamePlayManager.Instance.thisturn){

                List<Card_Control> die_CardControl = new List<Card_Control>();
                foreach (Card_Control cc in GameBoard.P2_WarZone)
                {
                    int hp = cc.card.Hp - 3;
                    cc.lb_aphp.Text = cc.card.Ap + " / " + hp;

                    if (hp <= 0)
                    {
                        die_CardControl.Add(cc);
                    }
                }
                foreach (Card_Control card_c in die_CardControl)
                {
                    GamePlayManager.Instance.moveZone(card_c, PLAYER2_TOMBZONE);
                }
            }
            else
            {
                List<Card_Control> die_CardControl = new List<Card_Control>();
                foreach (Card_Control cc in GameBoard.P1_WarZone)
                {
                    int hp = cc.card.Hp - 3;
                    cc.lb_aphp.Text = cc.card.Ap + " / " + hp;

                    if (hp <= 0)
                    {
                        die_CardControl.Add(cc);
                    }
                }
                foreach (Card_Control card_c in die_CardControl)
                {
                    GamePlayManager.Instance.moveZone(card_c, PLAYER1_TOMBZONE);
                }
            }
        }

        public void skill_16_DanceOfFire(Card_Control card_con)
        {
            if (skill_16)
            {
                if (canTargetSkill16(card_con))
                {
                    card_con.card.Hp -= 2;
                    card_con.lb_aphp.Text = card_con.card.Ap + " / " + card_con.card.Hp;

                    if (GamePlayManager.Instance.thisturn)
                    {
                        if (card_con.card.Hp <= 0)
                        {
                            GamePlayManager.Instance.moveZone(card_con, PLAYER1_TOMBZONE);
                        }
                    }
                    else
                    {
                        if (card_con.card.Hp <= 0)
                        {
                            GamePlayManager.Instance.moveZone(card_con, PLAYER2_TOMBZONE);
                        }
                    }

                    GamePlayManager.Instance.popCard(skill_card);
                    skill_card = null;
                    skill_16 = false;
                }
                else
                {
                    MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                    skill_16 = false;
                }
            }
            else
            {
                MessageBox.Show("공격할 대상을 선택하세요");
                skill_16 = true;
            }
        }


        protected void skill_21_succubus(Card_Control card_con)
        {
            if (skill_21)
            {
                if (canTargetSkill21(card_con))
                {
                    if (GamePlayManager.Instance.thisturn)
                    {
                        GamePlayManager.Instance.moveZone(card_con, PLAYER1_WARZONE);
                    }
                    else
                    {
                        GamePlayManager.Instance.moveZone(card_con, PLAYER2_WARZONE);
                    }
                    GamePlayManager.Instance.popCard(skill_card);
                    skill_card = null;
                    skill_21 = false;
                }
                else
                {
                    MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                    skill_21 = false;
                }
            }
            else
            {
                MessageBox.Show("원하는 대상을 선택하세요");
                skill_21 = true;
            }
        }

        public GameSkillManager()
        {
            SkillDictionary.TryAdd("1", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("2", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("3", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("4", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("5", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("6", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("7", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("8", new skillDelegate(skill_8_BigDevil));
            SkillDictionary.TryAdd("9", new skillDelegate(skill_9_vamfire));
            SkillDictionary.TryAdd("10", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("11", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("12", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("13", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("14", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("15", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("16", new skillDelegate(skill_16_DanceOfFire));
            SkillDictionary.TryAdd("17", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("18", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("19", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("20", new skillDelegate(skill_1_vamfire));
            SkillDictionary.TryAdd("21", new skillDelegate(skill_21_succubus));
        }


        /**********************************************
          ***********[[ Singleton 적용 ]]****************
          ***********************************************/
        static GameSkillManager GameSkillManagerInstance = null;
        static readonly object padlock = new object();

        public static GameSkillManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (GameSkillManagerInstance == null)
                    {
                        GameSkillManagerInstance = new GameSkillManager();
                    }
                    return GameSkillManagerInstance;
                }
            }
        }
    }
}
