using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.GameLogic_B;

namespace DragonWarLord_preprototype.GameLogic_B
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

        
        
        public bool skill_8_p1 = false;
        public bool skill_8_p2 = false;
        public bool skill_21 = false;

        public ConcurrentDictionary<string, Delegate> SkillDictionary = new ConcurrentDictionary<string, Delegate>();
        public delegate void skillDelegate(Card_Control card_con);


        protected void defaultSkill(Card_Control card_con)
        {

        }

        #region 스킬 3 : 아군이 파괴 될 때마다 +1/+1 된다.
        /// <summary>
        /// 스킬 3 : 아군이 파괴 될 때마다 +1/+1 된다.
        /// </summary>
        public List<Card_Control> list_skill3 = new List<Card_Control>();       

        public void skill_3_Frankenstein(Card_Control card_con)
        {
            list_skill3.Add(card_con);
        }
        public void skill_3_proc(Card_Control card_con)
        {
            foreach (Card_Control item in list_skill3)
            {
                if (item.position == PLAYER1_WARZONE)
                {
                    if (card_con.position == PLAYER1_TOMBZONE)
                    {
                        item.card.thisTurnAP++;
                        item.card.thisTurnHP++;
                        item.card.Ap++;
                        item.card.Hp++;
                        item.lb_aphp.Text = item.card.thisTurnAP + " / " + item.card.thisTurnHP;
                    }
                }
                if (item.position == PLAYER2_WARZONE)
                {
                    if (card_con.position == PLAYER2_TOMBZONE)
                    {
                        item.card.thisTurnAP++;
                        item.card.thisTurnHP++;
                        item.card.Ap++;
                        item.card.Hp++;
                        item.lb_aphp.Text = item.card.thisTurnAP + " / " + item.card.thisTurnHP;
                        
                    }
                }
            }
        }

        #endregion

        #region 스킬 7 : 모든아군에게 +1/+1
        /// <summary>
        /// 스킬 7 : 모든아군에게 +1/+1
        /// </summary>
        public List<Card_Control> list_skill7 = new List<Card_Control>();
        public bool skill_7_use = false;
        public void skill_7_satan(Card_Control card_con)
        {
            list_skill7.Add(card_con);  //버프카드 저장
            skill_7_use = true;
        }
        public void skill_7_proc(Card_Control card_con)
        {
            if (GameBoard.My_WarZone.Contains(card_con))
            {
                foreach (Card_Control c in GameBoard.My_WarZone)
                {
                    if (!c.Equals(card_con))
                    {
                        c.card.thisTurnAP++;
                        c.card.thisTurnHP++;
                        c.lb_aphp.Text = c.card.thisTurnAP + " / " + c.card.thisTurnHP;
                    }
                }
            }

            if (GameBoard.Opponent_WarZone.Contains(card_con))
            {
                foreach (Card_Control c in GameBoard.Opponent_WarZone)
                {
                    if (!c.Equals(card_con))
                    {
                        c.card.thisTurnAP++;
                        c.card.thisTurnHP++;
                        c.lb_aphp.Text = c.card.thisTurnAP + " / " + c.card.thisTurnHP;
                    }
                }
            }
        }

        public void dis_skill_7_proc(Card_Control card_con)
        {
            if (list_skill7.Contains(card_con))
            {
                list_skill7.Remove(card_con);
            }

            if (card_con.position == PLAYER1_TOMBZONE)
            {
                foreach (Card_Control c in GameBoard.My_WarZone)
                {
                    if (!c.Equals(card_con))
                    {
                        c.card.thisTurnAP--;
                        c.card.thisTurnHP--;
                        c.lb_aphp.Text = c.card.thisTurnAP + " / " + c.card.thisTurnHP;
                    }
                }
            }
            if (card_con.position == PLAYER2_TOMBZONE)
            {
                foreach (Card_Control c in GameBoard.Opponent_WarZone)
                {
                    if (!c.Equals(card_con))
                    {
                        c.card.thisTurnAP--;
                        c.card.thisTurnHP--;
                        c.lb_aphp.Text = c.card.thisTurnAP + " / " + c.card.thisTurnHP;
                    }
                }
            }
        }

        #endregion

        #region 스킬 8 : 전장에서 카드가 하나 파괴 될 때마다 플레이어의 생명력이 +1 된다. 이효과는 이 카드가 파괴 될때까지 지속 되며 자기 자신의 파괴는 포함하지 않는다.

        /// <summary>
        /// 스킬 8 : 전장에서 카드가 하나 파괴 될 때마다 플레이어의 생명력이 +1 된다. 
        /// 이효과는 이 카드가 파괴 될때까지 지속 되며 자기 자신의 파괴는 포함하지 않는다.
        /// </summary>
        public Card_Control tombCard;
        protected void skill_8_BigDevil(Card_Control card_con)
        {
            tombCard = card_con;
            if (GamePlayManager.Instance.thisturn)
            {
                skill_8_p1 = true;
            }
            else
            {
                skill_8_p2 = true;
            }
            
        }
        #endregion

        #region 스킬 9 : 전장에 입장 시 모든 적군에게 3의 피해를 입힌다.
        /// <summary>
        /// 스킬 9 : 전장에 입장 시 모든 적군에게 3의 피해를 입힌다.
        /// </summary>

        protected void skill_9_vamfire(Card_Control card_con)
        {
            if (GamePlayManager.Instance.thisturn){

                List<Card_Control> die_CardControl = new List<Card_Control>();
                foreach (Card_Control cc in GameBoard.Opponent_WarZone)
                {
                    cc.card.thisTurnHP -= 3;
                    cc.lb_aphp.Text = cc.card.thisTurnAP + " / " + cc.card.thisTurnHP;

                    if (cc.card.thisTurnHP <= 0)
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
                foreach (Card_Control cc in GameBoard.My_WarZone)
                {
                    cc.card.thisTurnHP -= 3;
                    cc.lb_aphp.Text = cc.card.thisTurnAP + " / " + cc.card.thisTurnHP;

                    if (cc.card.thisTurnHP <= 0)
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
        #endregion

        #region 스킬 11 : 거대 좀비가 전장에 입장한 턴 동안에는 상대방 마나 덱에 있는 마력을 마음 껏 사용할 수 있다.
        /// <summary>
        /// 스킬 11 : 거대 좀비가 전장에 입장한 턴 동안에는 상대방 마나 덱에 있는 마력을 마음 껏 사용할 수 있다.
        /// </summary>
        public bool list_skill11 = false;

        public void skill_11_GiantZombie(Card_Control card_con)
        {
            if (GamePlayManager.Instance.thisturn)
            {
                //mana_dark = GamePlayManager.Instance.p1_remain_dark;
                //mana_fire = GamePlayManager.Instance.p1_remain_fire;
                GamePlayManager.Instance.p1_remain_dark += GamePlayManager.Instance.p2_remain_dark;
                GamePlayManager.Instance.p1_remain_fire += GamePlayManager.Instance.p2_remain_fire;
                GamePlayManager.Instance.mainForm.My_remain_dark.Text = GamePlayManager.Instance.p1_remain_dark.ToString();
                GamePlayManager.Instance.mainForm.My_remain_fire.Text = GamePlayManager.Instance.p1_remain_fire.ToString();
            }else{
                //mana_dark = GamePlayManager.Instance.p2_remain_dark;
                //mana_fire = GamePlayManager.Instance.p2_remain_fire;
                GamePlayManager.Instance.p2_remain_dark += GamePlayManager.Instance.p1_remain_dark;
                GamePlayManager.Instance.p2_remain_fire += GamePlayManager.Instance.p1_remain_fire;
                GamePlayManager.Instance.mainForm.My_remain_dark.Text = GamePlayManager.Instance.p2_remain_dark.ToString();
                GamePlayManager.Instance.mainForm.My_remain_fire.Text = GamePlayManager.Instance.p2_remain_fire.ToString();
            }
            
            list_skill11 = true;
        }

        #endregion

        #region 스킬 13 : 상대방의 턴에 공격 당할 시 +2/+2
        /// <summary>
        /// 스킬 13 : 상대방의 턴에 공격 당할 시 +2/+2
        /// </summary>
        public List<Card_Control> list_skill13 = new List<Card_Control>();
        public List<Card_Control> list_skill13_used = new List<Card_Control>();
        public void skill_13_Ghoul(Card_Control card_con)
        {
            list_skill13.Add(card_con);
        }

        #endregion

        #region 스킬 16 : 적에게 피해를 2준다.
        /// <summary>
        /// 스킬 16 : 적에게 피해를 2준다.
        /// </summary>
        public bool skill_16 = false;
        public void skill_16_DanceOfFire(Card_Control card_con)
        {
            if (skill_16)
            {
                if (canTargetSkill16(card_con))
                {
                    card_con.card.thisTurnHP -= 2;
                    card_con.lb_aphp.Text = card_con.card.thisTurnAP + " / " + card_con.card.thisTurnHP;

                    if (GamePlayManager.Instance.thisturn)
                    {
                        if (card_con.card.thisTurnHP <= 0)
                        {
                            GamePlayManager.Instance.moveZone(card_con, PLAYER1_TOMBZONE);
                        }
                    }
                    else
                    {
                        if (card_con.card.thisTurnHP <= 0)
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

                if (GamePlayManager.Instance.thisturn)
                {
                    foreach (Card_Control item in GameBoard.Opponent_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                    GameBoard.Opponent_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                }
                else
                {
                    foreach (Card_Control item in GameBoard.My_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                    GameBoard.My_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;

                }

            }
        }
        public bool canTargetSkill16(Card_Control card_con)
        {
            foreach (Card_Control item in GameBoard.Opponent_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            GameBoard.Opponent_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            foreach (Card_Control item in GameBoard.My_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            GameBoard.My_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;


            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.Opponent_WarZone.Contains(card_con) || GameBoard.Opponent_PlayerZone.Equals(card_con))
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
                if (GameBoard.My_WarZone.Contains(card_con) || GameBoard.My_PlayerZone.Equals(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region 스킬 17 : 적에게 피해를 2준다.
        /// <summary>
        /// 스킬 17 : 적에게 피해를 2준다.
        /// </summary>

        public bool skill_17_first = false;
        public Card_Control first_target;
        public Card_Control second_target;
        public bool skill_17_second = false;
        public void skill_17_DRZombiesPotions(Card_Control card_con)
        {
            if (skill_17_second)
            {
                if (canTargetSkill17(card_con))
                {
                    if (!card_con.Equals(first_target))
                    {
                        second_target = card_con;
                        skill_17_first = false;
                        skill_17_second = false;
                        if (GamePlayManager.Instance.thisturn)
                        {
                            GamePlayManager.Instance.moveZone(first_target, PLAYER1_TOMBZONE);
                        }
                        else
                        {
                            GamePlayManager.Instance.moveZone(first_target, PLAYER2_TOMBZONE);
                        }

                        second_target.card.thisTurnAP += 4;
                        second_target.lb_aphp.Text = second_target.card.thisTurnAP + " / " + second_target.card.thisTurnHP;

                        GamePlayManager.Instance.popCard(skill_card);
                    }
                    else
                    {
                        MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                        skill_17_first = false;
                        skill_17_second = false;
                        first_target = null;
                        second_target = null;
                    }   
                }
                else
                {
                    MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                    skill_17_first = false;
                    skill_17_second = false;
                    first_target = null;
                    second_target = null;
                }
            }
            else
            {
                if (skill_17_first)
                {
                    if (canTargetSkill17(card_con))
                    {
                        first_target = card_con;

                        MessageBox.Show("강화할 아군을 선택하세요");
                        skill_17_first = false;
                        skill_17_second = true;

                        if (GamePlayManager.Instance.thisturn)
                        {
                            foreach (Card_Control item in GameBoard.My_WarZone)
                            {
                                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                            }
                        }
                        else
                        {
                            foreach (Card_Control item in GameBoard.Opponent_WarZone)
                            {
                                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                        skill_17_first = false;
                        skill_17_second = false;
                        first_target = null;
                        second_target = null;
                    }
                }
                else
                {
                    MessageBox.Show("희생할 아군을 선택하세요");
                    skill_17_first = true;

                    if (GamePlayManager.Instance.thisturn)
                    {
                        foreach (Card_Control item in GameBoard.My_WarZone)
                        {
                            item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                        }
                    }
                    else
                    {
                        foreach (Card_Control item in GameBoard.Opponent_WarZone)
                        {
                            item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                        }

                    }
                }
            }
        }

        public bool canTargetSkill17(Card_Control card_con)
        {
            foreach (Card_Control item in GameBoard.My_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            foreach (Card_Control item in GameBoard.Opponent_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }

            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.My_WarZone.Contains(card_con))
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
                if (GameBoard.Opponent_WarZone.Contains(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region 스킬 18 : 플레이어는 이 마법이 걸린 전사가 일으키는 피해만큼의 생명력을 회복한다.
        /// <summary>
        /// 스킬 18 : 플레이어는 이 마법이 걸린 전사가 일으키는 피해만큼의 생명력을 회복한다.
        /// </summary>
        public bool skill_18 = false;
        public List<Card_Control> list_skill18 = new List<Card_Control>();

        public void skill_18_NightOfVampire(Card_Control card_con)
        {
            if (skill_18)
            {
                if (canTargetSkill18(card_con))
                {
                    list_skill18.Add(card_con);
                    GamePlayManager.Instance.popCard(skill_card);

                    skill_card = null;
                    skill_18 = false;
                }
                else
                {
                    MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                    skill_18 = false;
                }
            }
            else
            {
                MessageBox.Show("마법대상을 선택하세요");
                skill_18 = true;
                if (GamePlayManager.Instance.thisturn)
                {
                    foreach (Card_Control item in GameBoard.My_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                }
                else
                {
                    foreach (Card_Control item in GameBoard.Opponent_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }

                }

            }
        }

        public bool canTargetSkill18(Card_Control card_con)
        {
            foreach (Card_Control item in GameBoard.My_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            foreach (Card_Control item in GameBoard.Opponent_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }

            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.My_WarZone.Contains(card_con))
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
                if (GameBoard.Opponent_WarZone.Contains(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        #endregion

        #region 스킬 19 : 이 마법에 걸린 전사는 전투 시 자기 자신과 함께 상대를 파괴한다.
        /// <summary>
        /// 스킬 19 : 이 마법에 걸린 전사는 전투 시 자기 자신과 함께 상대를 파괴한다.
        /// </summary>
        public bool skill_19 = false;
        public List<Card_Control> list_skill19 = new List<Card_Control>();

        public void skill_19_WhisperOfDevil(Card_Control card_con)
        {
            if (skill_19)
            {
                if (canTargetSkill19(card_con))
                {
                    list_skill19.Add(card_con);
                    GamePlayManager.Instance.popCard(skill_card);

                    skill_card = null;
                    skill_19 = false;
                }
                else
                {
                    MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                    skill_19 = false;
                }
            }
            else
            {
                MessageBox.Show("마법대상을 선택하세요");
                skill_19 = true;
                if(GamePlayManager.Instance.thisturn){
                    foreach(Card_Control item in GameBoard.My_WarZone){
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                }else{
                    foreach (Card_Control item in GameBoard.Opponent_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                    
                }
                
            }
        }

        public bool canTargetSkill19(Card_Control card_con)
        {
            foreach (Card_Control item in GameBoard.My_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            foreach (Card_Control item in GameBoard.Opponent_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }

            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.My_WarZone.Contains(card_con))
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
                if (GameBoard.Opponent_WarZone.Contains(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        #endregion

        #region 스킬 20 : 마력 카드를 1장 파괴해 원하는 상대에게 5의 데미지를 준다. 단, 파괴 될 마력 카드는 이 카드를 시동하기 위해 사용 될 수 없다.
        /// <summary>
        /// 스킬 20 : 마력 카드를 1장 파괴해 원하는 상대에게 5의 데미지를 준다. 단, 파괴 될 마력 카드는 이 카드를 시동하기 위해 사용 될 수 없다.
        /// </summary>
        public bool skill_20 = false;
        public void skill_20_DevilsFruit(Card_Control card_con)
        {
            if (skill_20)
            {
                if (canTargetSkill20(card_con))
                {
                    card_con.card.thisTurnHP -= 5;
                    card_con.lb_aphp.Text = card_con.card.thisTurnAP + " / " + card_con.card.thisTurnHP;

                    if (GamePlayManager.Instance.thisturn)
                    {
                        if (card_con.card.thisTurnHP <= 0)
                        {
                            GamePlayManager.Instance.moveZone(card_con, PLAYER1_TOMBZONE);
                        }
                    }
                    else
                    {
                        if (card_con.card.thisTurnHP <= 0)
                        {
                            GamePlayManager.Instance.moveZone(card_con, PLAYER2_TOMBZONE);
                        }
                    }

                    GamePlayManager.Instance.popCard(skill_card);
                    if (GamePlayManager.Instance.thisturn)
                    {
                        if (GamePlayManager.Instance.p1_remain_dark >= GamePlayManager.Instance.p1_remain_fire)
                        {
                            GamePlayManager.Instance.p1_remain_dark -= 1;
                            int result = Convert.ToInt32(GamePlayManager.Instance.mainForm.My_cnt_dark.Text);
                            result -= 1;
                            GamePlayManager.Instance.mainForm.My_cnt_dark.Text = result.ToString();
                        }
                        else
                        {
                            GamePlayManager.Instance.p1_remain_fire -= 1;
                            int result = Convert.ToInt32(GamePlayManager.Instance.mainForm.My_cnt_fire.Text);
                            result -= 1;
                            GamePlayManager.Instance.mainForm.My_cnt_fire.Text = result.ToString();
                        }
                    }
                    else
                    {
                        if (GamePlayManager.Instance.p2_remain_dark >= GamePlayManager.Instance.p2_remain_fire)
                        {
                            GamePlayManager.Instance.p2_remain_dark -= 1;
                            int result = Convert.ToInt32(GamePlayManager.Instance.mainForm.Opponent_cnt_dark.Text);
                            result -= 1;
                            GamePlayManager.Instance.mainForm.Opponent_cnt_dark.Text = result.ToString();
                        }
                        else
                        {
                            GamePlayManager.Instance.p2_remain_fire -= 1;
                            int result = Convert.ToInt32(GamePlayManager.Instance.mainForm.Opponent_cnt_fire.Text);
                            result -= 1;
                            GamePlayManager.Instance.mainForm.Opponent_cnt_fire.Text = result.ToString();
                        }
                    }

                    skill_card = null;
                    skill_20 = false;
                }
                else
                {
                    MessageBox.Show("잘못된 대상입니다. 스킬을 취소합니다.");
                    skill_20 = false;
                }
            }
            else
            {
                MessageBox.Show("공격할 대상을 선택하세요");
                skill_20 = true;

                if (GamePlayManager.Instance.thisturn)
                {
                    foreach (Card_Control item in GameBoard.Opponent_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                    GameBoard.Opponent_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                }
                else
                {
                    foreach (Card_Control item in GameBoard.My_WarZone)
                    {
                        item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;
                    }
                    GameBoard.My_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.can_backimg_select;

                }

            }
        }
        public bool canTargetSkill20(Card_Control card_con)
        {
            foreach (Card_Control item in GameBoard.Opponent_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            GameBoard.Opponent_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;

            foreach (Card_Control item in GameBoard.My_WarZone)
            {
                item.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
            }
            GameBoard.My_PlayerZone.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;


            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.Opponent_WarZone.Contains(card_con) || GameBoard.Opponent_PlayerZone.Equals(card_con))
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
                if (GameBoard.My_WarZone.Contains(card_con) || GameBoard.My_PlayerZone.Equals(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region 스킬 21 : 전장에 나와 있는 적군 중 하나를 아군으로 만든다.
        /// <summary>
        /// 스킬21 : 전장에 나와 있는 적군 중 하나를 아군으로 만든다.
        /// </summary>
        /// <param name="card_con"></param>
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
        public bool canTargetSkill21(Card_Control card_con)
        {
            if (GamePlayManager.Instance.thisturn)
            {
                if (GameBoard.Opponent_WarZone.Contains(card_con))
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
                if (GameBoard.My_WarZone.Contains(card_con))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        public GameSkillManager()
        {
            SkillDictionary.TryAdd("1", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("2", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("3", new skillDelegate(skill_3_Frankenstein));
            SkillDictionary.TryAdd("4", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("5", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("6", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("7", new skillDelegate(skill_7_satan));
            SkillDictionary.TryAdd("8", new skillDelegate(skill_8_BigDevil));
            SkillDictionary.TryAdd("9", new skillDelegate(skill_9_vamfire));
            SkillDictionary.TryAdd("10", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("11", new skillDelegate(skill_11_GiantZombie));
            SkillDictionary.TryAdd("12", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("13", new skillDelegate(skill_13_Ghoul));
            SkillDictionary.TryAdd("14", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("15", new skillDelegate(defaultSkill));
            SkillDictionary.TryAdd("16", new skillDelegate(skill_16_DanceOfFire));
            SkillDictionary.TryAdd("17", new skillDelegate(skill_17_DRZombiesPotions));
            SkillDictionary.TryAdd("18", new skillDelegate(skill_18_NightOfVampire));
            SkillDictionary.TryAdd("19", new skillDelegate(skill_19_WhisperOfDevil));
            SkillDictionary.TryAdd("20", new skillDelegate(skill_20_DevilsFruit));
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
