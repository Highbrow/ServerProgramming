using DragonWarLord_preprototype;
using DragonWarLord_preprototype.CardLibrary;
using DragonWarLord_preprototype.GameLogic_B;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;

namespace WarLord_Server_GUI.GameLogic_B
{
    class GamePlayManager
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

        public delegate void dchangeStatus(Card_Control card_con);

        public event dchangeStatus cardPop;
        public event dchangeStatus endturn;

        public event dchangeStatus cardDestruction;

        private GamePlayManager()
        {
            cardPop = new dchangeStatus(cardPop_EventProc);
            endturn = new dchangeStatus(endturn_EventProc);
            cardDestruction = new dchangeStatus(destruction_EventProc);
        }

        /// <summary>
        /// 게임 준비
        /// </summary>
        public void GameDefaultSetting()
        {
            foreach (Card_Control card in GameBoard.My_HandsZone)
            {
                mainForm.add_My_Hands(card);
            }
            foreach (var card in GameBoard.Opponent_HandsZone)
            {
                mainForm.add_Opponent_Hands(card);
            }
            InitFlag();
            turnFlag();
        }

        //=====[ 카드 파괴 이벤트 처리 ]=====
        public void destruction_EventProc(Card_Control card_con)
        {
            if (card_con.card.Skill.Equals("7"))    //====[스킬7카드 죽음]====
            {
                GameSkillManager.Instance.dis_skill_7_proc(card_con);
            }
            if (card_con.card.Skill.Equals("13"))
            {
                GameSkillManager.Instance.list_skill13.Remove(card_con);
            }
            if (card_con.card.Skill.Equals("3"))    //====[스킬3카드 죽음]====
            {
                GameSkillManager.Instance.list_skill3.Remove(card_con);
            }

            if (GameSkillManager.Instance.list_skill3.Count > 0)    //====[스킬3카드 동작]====
            {
                GameSkillManager.Instance.skill_3_proc(card_con);

            }

        }

        //=====[ 카드내기 이벤트 처리 ]=====
        public void cardPop_EventProc(Card_Control card_con)
        {
            if (GameSkillManager.Instance.skill_7_use)
            {
                GameSkillManager.Instance.skill_7_proc(card_con);
                GameSkillManager.Instance.skill_7_use = false;
            }
        }

        //=====[ 턴종료 이벤트 처리 ]=====
        public void endturn_EventProc(Card_Control card_con)
        {
            for (int i = 0; i < GameSkillManager.Instance.list_skill7.Count; i++)
            {
                GameSkillManager.Instance.skill_7_proc(GameSkillManager.Instance.list_skill7[i]);
            }
            for (int i = 0; i < GameSkillManager.Instance.list_skill3.Count; i++)
            {
                GameSkillManager.Instance.skill_3_proc(GameSkillManager.Instance.list_skill3[i]);
            }
            GameSkillManager.Instance.list_skill13_used.Clear();
        }


        public DragonWarLord_preprototype.MainForm mainForm { get; set; }
        public bool program_run = true;
        public bool thisturn = true; // true : 1,  false : 2

        //=====[ 턴 종료 ]=====
        public bool EndOfTurn()
        {
            InitFlag(); //flag 초기화
            if (thisturn)
            {
                thisturn = false;
            }
            else
            {
                thisturn = true;
            }
            distribute();   //패돌리기
            turnFlag();

            endturn(null);

            return thisturn;
        }

        //=====[ flag 관련 ]=====
        public bool canMakeMana = true; //마나 생성 가능 유무

        public void InitFlag()
        {
            //====[재 활성화 ]======
            foreach (Card_Control card in GameBoard.My_HandsZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.Opponent_HandsZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.My_WarZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.Opponent_WarZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            //====[마나 생성 가능하도록]=====
            canMakeMana = true;
        }

        public void turnFlag()
        {
            if (thisturn)
            {
                foreach (Card_Control card in GameBoard.My_HandsZone)
                {
                    card.activatable = true;
                    card.Enabled = true;
                    card.Card_refresh();
                }
                foreach (Card_Control card in GameBoard.Opponent_HandsZone)
                {
                    card.activatable = false;
                    card.Enabled = false;
                    card.Card_refresh();
                }
            }
            else
            {
                foreach (Card_Control card in GameBoard.My_HandsZone)
                {
                    card.activatable = false;
                    card.Enabled = false;
                    card.Card_refresh();
                }
                foreach (Card_Control card in GameBoard.Opponent_HandsZone)
                {
                    card.activatable = true;
                    card.Enabled = true;
                    card.Card_refresh();
                }
            }

            initMana();//마나관련 초기화 작업
        }

        //=====[마나 초기화]=====
        public void initMana()
        {
            if (GameSkillManager.Instance.list_skill11)
            {
                GameSkillManager.Instance.list_skill11 = false;
            }
            all_count = 0;  //아무 마나 카운터 초기화
            p1_remain_dark = Convert.ToInt32(mainForm.My_cnt_dark.Text);   //현재 암흑 마나
            p1_remain_fire = Convert.ToInt32(mainForm.My_cnt_fire.Text);   //현재 불 마나
            p2_remain_dark = Convert.ToInt32(mainForm.Opponent_cnt_dark.Text);   //현재 암흑 마나
            p2_remain_fire = Convert.ToInt32(mainForm.Opponent_cnt_fire.Text);   //현재 불 마나
            refreshMana();
        }
        //=====[마나 새로고침]=====
        private void refreshMana()
        {
            mainForm.setText_My_remain_dark(p1_remain_dark.ToString());
            mainForm.setText_My_remain_fire(p1_remain_fire.ToString());
            mainForm.setText_My_use_all(all_count.ToString());

            mainForm.setText_Opponent_remain_dark(p2_remain_dark.ToString());
            mainForm.setText_Opponent_remain_fire(p2_remain_fire.ToString());
            mainForm.setText_Opponent_use_all(all_count.ToString());
        }

        /// <summary>
        /// 마나 생성
        /// </summary>
        /// <param name="card_con"></param>
        public void makeMana(Card_Control card_con)
        {
            if (GamePlayManager.Instance.canMakeMana)   //마나생성이 가능할 경우
            {
                if (GamePlayManager.Instance.thisturn)
                {
                    moveZone(card_con, MY_MANAZONE);   //마나존으로 이동
                    if (card_con.card.Attribute.Equals("암흑"))
                    {
                        mainForm.setText_My_cnt_dark((Convert.ToInt32(mainForm.My_cnt_dark.Text) + 1).ToString());
                        p1_remain_dark++;
                    }
                    else if (card_con.card.Attribute.Equals("불"))
                    {
                        mainForm.setText_My_cnt_fire((Convert.ToInt32(mainForm.My_cnt_fire.Text) + 1).ToString());
                        p1_remain_fire++;
                    }
                }
                else
                {
                    moveZone(card_con, OPPONENT_MANAZONE);   //마나존으로 이동
                    if (card_con.card.Attribute.Equals("암흑"))
                    {
                        mainForm.setText_Opponent_cnt_dark((Convert.ToInt32(mainForm.Opponent_cnt_dark.Text) + 1).ToString());
                        p2_remain_dark++;
                    }
                    else if (card_con.card.Attribute.Equals("불"))
                    {
                        mainForm.setText_Opponent_cnt_fire((Convert.ToInt32(mainForm.Opponent_cnt_fire.Text) + 1).ToString());
                        p2_remain_fire++;
                    }
                }
            }
            else
            {
                MessageBox.Show("이번 턴에는 마나를 더 이상 생성 할 수 없습니다.");
            }

            refreshMana();//마나 새로고침
        }

        #region 카드 내는 행동

        int all_count = 0;

        public int p1_remain_dark = 0;
        public int p1_remain_fire = 0;
        public int p2_remain_dark = 0;
        public int p2_remain_fire = 0;

        //====[마나 확인]====
        public bool canPopCard(Card_Control card_con)
        {
            string[] consump = card_con.card.Consumption.Split(';');
            int need_dark = Convert.ToInt32(consump[1]);    //필요 암흑 마나
            int need_fire = Convert.ToInt32(consump[0]);    //필요 불 마나
            int need_all = Convert.ToInt32(consump[2]);     //필요 아무 마나

            if (thisturn)
            {
                //===내기위한 마나 처리
                if (p1_remain_dark >= need_dark && p1_remain_fire >= need_fire && (((p1_remain_dark - need_dark) + (p1_remain_fire - need_fire)) - all_count) >= need_all)
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
                //===내기위한 마나 처리
                if (p2_remain_dark >= need_dark && p2_remain_fire >= need_fire && (((p2_remain_dark - need_dark) + (p2_remain_fire - need_fire)) - all_count) >= need_all)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //====[마나 확인]====
        public bool canPopCard(Card_Control card_con, string skill)
        {
            string[] consump = card_con.card.Consumption.Split(';');
            int need_dark = Convert.ToInt32(consump[1]);    //필요 암흑 마나
            int need_fire = Convert.ToInt32(consump[0]);    //필요 불 마나
            int need_all = Convert.ToInt32(consump[2]);     //필요 아무 마나

            if (thisturn)
            {
                //===내기위한 마나 처리
                if (p1_remain_dark >= need_dark && p1_remain_fire >= need_fire && (((p1_remain_dark - need_dark) + (p1_remain_fire - need_fire)) - all_count) > need_all)
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
                //===내기위한 마나 처리
                if (p2_remain_dark >= need_dark && p2_remain_fire >= need_fire && (((p2_remain_dark - need_dark) + (p2_remain_fire - need_fire)) - all_count) > need_all)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal void procMana(Card_Control card_con)
        {
            string[] consump = card_con.card.Consumption.Split(';');
            int need_dark = Convert.ToInt32(consump[1]);    //필요 암흑 마나
            int need_fire = Convert.ToInt32(consump[0]);    //필요 불 마나
            int need_all = Convert.ToInt32(consump[2]);     //필요 아무 마나

            if (thisturn)
            {
                //----[마나 연산]----
                p1_remain_dark -= need_dark;
                p1_remain_fire -= need_fire;
                all_count += need_all;
                refreshMana();//마나 새로고침
            }
            else
            {
                //----[마나 연산]----
                p2_remain_dark -= need_dark;
                p2_remain_fire -= need_fire;
                all_count += need_all;

                refreshMana();//마나 새로고침
            }

        }

        internal void popCard(Card_Control card_con)
        {
            if (thisturn)
            {
                if (card_con.card.Type.Equals("마법"))
                {
                    procMana(card_con);
                    moveZone(card_con, MY_TOMBZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("미니언"))
                {
                    procMana(card_con);
                    moveZone(card_con, MY_WARZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("드래곤"))
                {
                    procMana(card_con);
                    moveZone(card_con, MY_WARZONE);//카드 내기
                }
                else
                {
                    MessageBox.Show("잘못된 카드입니다.");
                }
            }
            else
            {
                if (card_con.card.Type.Equals("마법"))
                {
                    procMana(card_con);
                    moveZone(card_con, OPPONENT_TOMBZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("미니언"))
                {
                    procMana(card_con);
                    moveZone(card_con, OPPONENT_WARZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("드래곤"))
                {
                    procMana(card_con);
                    moveZone(card_con, OPPONENT_WARZONE);//카드 내기
                }
                else
                {
                    MessageBox.Show("잘못된 카드입니다.");
                }
            }

            cardPop(card_con);  //카드내기 이벤트발생
        }

        #endregion 카드 내는 행동

        //=====[게임 스킬 관련]=====
        public void gameSkill(String skill, Card_Control card_con)
        {
            Delegate dg;
            GameSkillManager.Instance.SkillDictionary.TryGetValue(skill, out dg);
            dg.DynamicInvoke(card_con);
        }

        //=====[ 카드 선택 관련 ]=====
        Card_Control selectCard = null;
        Card_Control targetCard = null;
        public void CardSelectProc(Card_Control card_con)
        {
            //====[초기화]====
            if (card_con == null)
            {
                if (selectCard != null)
                {
                    selectCard.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
                }
                if (targetCard != null)
                {
                    targetCard.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
                }
                selectCard = null;
                targetCard = null;
            }//===============================
            else
            {//=====[ 동작 ]=====
                card_con.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg_select;
                if (selectCard == null)
                {
                    selectCard = card_con;
                }
                else if (selectCard.Equals(card_con))
                {
                    selectCard.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
                    selectCard = null;
                    targetCard = null;
                }
                else
                {
                    targetCard = card_con;
                    //=====[전투 처리 메소드 추가]=====
                    canMatchCheck(selectCard, targetCard);  //매치 가능여부 체크 후 매치
                    ///////////////////////////////////
                    selectCard.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
                    targetCard.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimg;
                    selectCard = null;
                    targetCard = null;
                }
            }
        }

        public void canMatchCheck(Card_Control selectCard, Card_Control targetCard)
        {
            if (thisturn)
            {
                if (GameBoard.My_WarZone.Contains(selectCard) && (GameBoard.Opponent_WarZone.Contains(targetCard) || GameBoard.Opponent_PlayerZone.Equals(targetCard)))
                {
                    MatchCard(selectCard, targetCard);
                }
            }
            else
            {
                if (GameBoard.Opponent_WarZone.Contains(selectCard) && (GameBoard.My_WarZone.Contains(targetCard) || GameBoard.My_PlayerZone.Equals(targetCard)))
                {
                    MatchCard(selectCard, targetCard);
                }
            }
        }

        //=====[ Match ]=====
        public void MatchCard(Card_Control selectCard, Card_Control targetCard)
        {
            if (GameSkillManager.Instance.list_skill19.Contains(selectCard) || GameSkillManager.Instance.list_skill19.Contains(targetCard))
            {
                if (thisturn)
                {
                    moveZone(selectCard, MY_TOMBZONE);
                    moveZone(targetCard, OPPONENT_TOMBZONE);
                }
                else
                {
                    moveZone(selectCard, OPPONENT_TOMBZONE);
                    moveZone(targetCard, MY_TOMBZONE);
                }
            }
            else
            {
                if (targetCard.card.Skill.Equals("13"))
                {
                    if (!GameSkillManager.Instance.list_skill13_used.Contains(targetCard))
                    {
                        targetCard.card.thisTurnHP += 2;
                        targetCard.card.thisTurnAP += 2;

                        selectCard.card.thisTurnHP -= targetCard.card.thisTurnAP;
                        targetCard.card.thisTurnHP -= selectCard.card.thisTurnAP;
                        GameSkillManager.Instance.list_skill13_used.Add(targetCard);
                    }
                    else
                    {
                        selectCard.card.thisTurnHP -= targetCard.card.thisTurnAP;
                        targetCard.card.thisTurnHP -= selectCard.card.thisTurnAP;
                    }
                }
                else
                {
                    selectCard.card.thisTurnHP -= targetCard.card.thisTurnAP;
                    targetCard.card.thisTurnHP -= selectCard.card.thisTurnAP;
                }
            }

            if (thisturn)
            {
                if (GameSkillManager.Instance.list_skill18.Contains(selectCard))
                {
                    GameBoard.My_PlayerZone.card.thisTurnHP += selectCard.card.thisTurnAP;
                }
                if (GameSkillManager.Instance.list_skill18.Contains(targetCard))
                {
                    GameBoard.Opponent_PlayerZone.card.thisTurnHP += targetCard.card.thisTurnAP;
                }
            }
            else
            {
                if (GameSkillManager.Instance.list_skill18.Contains(selectCard))
                {
                    GameBoard.Opponent_PlayerZone.card.thisTurnHP += selectCard.card.thisTurnAP;
                }
                if (GameSkillManager.Instance.list_skill18.Contains(targetCard))
                {
                    GameBoard.My_PlayerZone.card.thisTurnHP += targetCard.card.thisTurnAP;
                }
            }


            GameBoard.My_PlayerZone.lb_aphp.Text = GameBoard.My_PlayerZone.card.thisTurnAP + " / " + GameBoard.My_PlayerZone.card.thisTurnHP;
            GameBoard.Opponent_PlayerZone.lb_aphp.Text = GameBoard.Opponent_PlayerZone.card.thisTurnAP + " / " + GameBoard.Opponent_PlayerZone.card.thisTurnHP;
            selectCard.lb_aphp.Text = selectCard.card.thisTurnAP + " / " + selectCard.card.thisTurnHP;
            targetCard.lb_aphp.Text = targetCard.card.thisTurnAP + " / " + targetCard.card.thisTurnHP;

            if (thisturn)
            {
                if (selectCard.card.thisTurnHP <= 0)
                {
                    moveZone(selectCard, MY_TOMBZONE);
                }
                if (targetCard.card.thisTurnHP <= 0)
                {
                    moveZone(targetCard, OPPONENT_TOMBZONE);
                }
            }
            else
            {
                if (selectCard.card.thisTurnHP <= 0)
                {
                    moveZone(selectCard, OPPONENT_TOMBZONE);
                }
                if (targetCard.card.thisTurnHP <= 0)
                {
                    moveZone(targetCard, MY_TOMBZONE);
                }
            }
            selectCard.activatable = false;
            selectCard.Enabled = false;
        }
        //=====[ 처음 카드 지급 ]=====
        public void firstDistribute()
        {
            moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);
            moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);
            moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);

            //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);
            //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);
            //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);

            if (thisturn)
            {
                //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);
                //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);
            }
            else
            {
                moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);
                moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);
            }
        }

        //=====[ 카드 분배 ]=====
        public void distribute()
        {
            if (thisturn)
            {
                moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);
                moveZone(GameBoard.My_CardDeck[0], MY_HANDSZONE);
            }
            else
            {
                //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);
                //moveZone(GameBoard.Opponent_CardDeck[0], OPPONENT_HANDSZONE);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 카드 존 이동
        /// </summary>
        /// <param name="card_con"></param> 카드
        /// <param name="position"></param> 이동 목적지
        public void moveZone(Card_Control card_con, int position)
        {
            //=====[기존 리스트 탐색]=====
            int pre_position = 0;

            if (GameBoard.My_CardDeck.Contains(card_con))
            {
                pre_position = MY_CARDDECK;
            }
            else if (GameBoard.Opponent_CardDeck.Contains(card_con))
            {
                pre_position = OPPONENT_CARDDECK;
            }
            else if (GameBoard.My_HandsZone.Contains(card_con))
            {
                pre_position = MY_HANDSZONE;
            }
            else if (GameBoard.Opponent_HandsZone.Contains(card_con))
            {
                pre_position = OPPONENT_HANDSZONE;
            }
            else if (GameBoard.My_WarZone.Contains(card_con))
            {
                pre_position = MY_WARZONE;
            }
            else if (GameBoard.Opponent_WarZone.Contains(card_con))
            {
                pre_position = OPPONENT_WARZONE;
            }
            else if (GameBoard.My_ManaZone.Contains(card_con))
            {
                pre_position = MY_MANAZONE;
            }
            else if (GameBoard.Opponent_ManaZone.Contains(card_con))
            {
                pre_position = OPPONENT_MANAZONE;
            }
            else if (GameBoard.My_TombZone.Contains(card_con))
            {
                pre_position = MY_TOMBZONE;
            }
            else if (GameBoard.Opponent_TombZone.Contains(card_con))
            {
                pre_position = OPPONENT_TOMBZONE;
            }
            else if (GameBoard.My_PlayerZone.Equals(card_con))
            {
                pre_position = MY_PLAYERZONE;
            }
            else if (GameBoard.Opponent_PlayerZone.Equals(card_con))
            {
                pre_position = OPPONENT_PLAYERZONE;
            }
            else
            {
                pre_position = 0;
            }

            //=====[기본 리스트에서 삭제]=====
            switch (pre_position)
            {
                case MY_CARDDECK:
                    GameBoard.My_CardDeck.Remove(card_con);
                    break;
                case OPPONENT_CARDDECK:
                    GameBoard.Opponent_CardDeck.Remove(card_con);
                    break;
                case MY_HANDSZONE:
                    GameBoard.My_HandsZone.Remove(card_con);
                    mainForm.remove_My_Hands(card_con);
                    break;
                case OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Remove(card_con);
                    mainForm.remove_Opponent_Hands(card_con);
                    break;
                case MY_WARZONE:
                    GameBoard.My_WarZone.Remove(card_con);
                    mainForm.remove_My_WarZone(card_con);
                    break;
                case OPPONENT_WARZONE:
                    GameBoard.Opponent_WarZone.Remove(card_con);
                    mainForm.remove_Opponent_WarZone(card_con);
                    break;
                case MY_MANAZONE:
                    GameBoard.My_ManaZone.Remove(card_con);
                    mainForm.remove_My_ManaZone(card_con);
                    break;
                case OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Remove(card_con);
                    mainForm.remove_Opponent_ManaZone(card_con);
                    break;
                case MY_TOMBZONE:
                    GameBoard.My_TombZone.Remove(card_con);
                    break;
                case OPPONENT_TOMBZONE:
                    GameBoard.Opponent_TombZone.Remove(card_con);
                    break;
                case MY_PLAYERZONE:
                    break;
                case OPPONENT_PLAYERZONE:                   
                    break;
            }
            //=====[새로운 포지션 배치]=====
            card_con.position = position;
            switch (position)
            {
                case MY_CARDDECK:
                    GameBoard.My_CardDeck.Add(card_con);
                    break;
                case OPPONENT_CARDDECK:
                    GameBoard.Opponent_CardDeck.Add(card_con);
                    break;
                case MY_HANDSZONE:
                    GameBoard.My_HandsZone.Add(card_con);
                    mainForm.add_My_Hands(card_con);
                    card_con.activatable = true;
                    card_con.Enabled = true;
                    break;
                case OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Add(card_con);
                    mainForm.add_Opponent_Hands(card_con);
                    card_con.activatable = true;
                    card_con.Enabled = true;
                    break;
                case MY_WARZONE:
                    GameBoard.My_WarZone.Add(card_con);
                    mainForm.add_My_WarZone(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case OPPONENT_WARZONE:
                    GameBoard.Opponent_WarZone.Add(card_con);
                    mainForm.add_Opponent_WarZone(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case MY_MANAZONE:
                    GameBoard.My_ManaZone.Add(card_con);
                    mainForm.add_My_ManaZone(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Add(card_con);
                    mainForm.add_Opponent_ManaZone(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case MY_TOMBZONE:
                    if (card_con.Equals(GameSkillManager.Instance.tombCard))
                    {
                        GameSkillManager.Instance.skill_8_p1 = false;
                    }
                    else
                    {
                        if (GameSkillManager.Instance.skill_8_p1)
                        {
                            GameBoard.My_PlayerZone.card.Hp++;
                            GameBoard.My_PlayerZone.lb_aphp.Text = GameBoard.My_PlayerZone.card.Ap + " / " + GameBoard.My_PlayerZone.card.Hp;
                        }
                        if (GameSkillManager.Instance.skill_8_p2)
                        {
                            GameBoard.Opponent_PlayerZone.card.Hp++;
                            GameBoard.Opponent_PlayerZone.lb_aphp.Text = GameBoard.Opponent_PlayerZone.card.Ap + " / " + GameBoard.Opponent_PlayerZone.card.Hp;
                        }
                    }

                    GameBoard.My_TombZone.Add(card_con);
                    mainForm.add_My_TombZone(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;

                    cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
                case OPPONENT_TOMBZONE:
                    if (card_con.Equals(GameSkillManager.Instance.tombCard))
                    {
                        GameSkillManager.Instance.skill_8_p2 = false;
                    }
                    else
                    {
                        if (GameSkillManager.Instance.skill_8_p1)
                        {
                            GameBoard.My_PlayerZone.card.Hp++;
                            GameBoard.My_PlayerZone.lb_aphp.Text = GameBoard.My_PlayerZone.card.Ap + " / " + GameBoard.My_PlayerZone.card.Hp;
                        }
                        if (GameSkillManager.Instance.skill_8_p2)
                        {
                            GameBoard.Opponent_PlayerZone.card.Hp++;
                            GameBoard.Opponent_PlayerZone.lb_aphp.Text = GameBoard.Opponent_PlayerZone.card.Ap + " / " + GameBoard.Opponent_PlayerZone.card.Hp;
                        }
                    }

                    GameBoard.Opponent_TombZone.Add(card_con);
                    mainForm.add_Opponent_TombZone(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;

                    cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
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
