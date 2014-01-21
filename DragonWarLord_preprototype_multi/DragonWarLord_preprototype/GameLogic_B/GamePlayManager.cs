using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DragonWarLord_preprototype;
using DragonWarLord_preprototype.CardLibrary;
using DragonWarLord_preprototype.GameLogic_B;
using WarLord_Server_GUI.GameLogic_A;

namespace WarLord_Server_GUI.GameLogic_B
{
    class GamePlayManager
    {
        public delegate void dchangeStatus(Card_Control card_con);

        public event dchangeStatus cardPop;
        public event dchangeStatus endturn;
        public event dchangeStatus cardDestruction;

        public bool program_run = true;

        public bool CMR = true; //마나 생성 가능 유무

        //=====[ flag 관련 ]=====
        //public bool canMakeResource = true; //마나 생성 가능 유무


        private GamePlayManager()
        {
            cardPop = new dchangeStatus(cardPop_EventProc);
            endturn = new dchangeStatus(endturn_EventProc);
            cardDestruction = new dchangeStatus(destruction_EventProc);
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
                GameBoard.My_PlayerZone.position = GameBoard.MY_PLAYERZONE;
                MainForm.mainForm.add_My_Player(GameBoard.My_PlayerZone);
            }
            else if (position == GameBoard.OPPONENT_PLAYERZONE)
            {
                GameBoard.Opponent_PlayerZone = DataBaseManager.makeMainPlayer(p[0]);
                GameBoard.Opponent_PlayerZone.position = GameBoard.OPPONENT_PLAYERZONE;
                MainForm.mainForm.add_Opponent_Player(GameBoard.Opponent_PlayerZone);
            }
            else
            {
                MessageBox.Show("카드 생성위치가 잘못되었습니다.");
            }
        }

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
            }
            else if (position == GameBoard.OPPONENT_CARDDECK)
            {
                DataBaseManager.inputCard(p, false);
            }
            else
            {
                MessageBox.Show("카드 생성위치가 잘못되었습니다.");
            }
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

        //=====[ 턴 종료 ]=====
        public bool EndOfTurn()
        {
            InitFlag(); //flag 초기화
            turnFlag();

            endturn(null);

            return TurnManager.Turn;
        }

        public void InitFlag()
        {
            //====[재 활성화 ]======
            foreach (Card_Control card in GameBoard.My_HandsZone)
            {
                card.activatable = true;
                card.setCardEnabled(true);
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.Opponent_HandsZone)
            {
                card.activatable = true;
                card.setCardEnabled(true);
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.My_BattleZone)
            {
                card.activatable = true;
                card.setCardEnabled(true);
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.Opponent_BattleZone)
            {
                card.activatable = true;
                card.setCardEnabled(true);
                card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
                card.Card_refresh();
            }
            CMR= true;
        }

        public void turnFlag()
        {
            if (TurnManager.Turn)
            {
                foreach (Card_Control card in GameBoard.My_HandsZone)
                {
                    card.activatable = true;
                    card.setCardEnabled(true);
                    card.Card_refresh();
                }
                foreach (Card_Control card in GameBoard.Opponent_HandsZone)
                {
                    card.activatable = false;

                    card.setCardEnabled(false);
                    card.Card_refresh();
                }
            }
            else
            {
                foreach (Card_Control card in GameBoard.My_HandsZone)
                {
                    card.activatable = false;
                    card.setCardEnabled(false);
                    card.Card_refresh();
                }
                foreach (Card_Control card in GameBoard.Opponent_HandsZone)
                {
                    card.activatable = true;
                    card.setCardEnabled(true);
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
            My_remain_dark = Convert.ToInt32(MainForm.mainForm.My_cnt_dark.Text);   //현재 암흑 마나
            My_remain_fire = Convert.ToInt32(MainForm.mainForm.My_cnt_fire.Text);   //현재 불 마나
            Opponent_remain_dark = Convert.ToInt32(MainForm.mainForm.Opponent_cnt_dark.Text);   //현재 암흑 마나
            Opponent_remain_fire = Convert.ToInt32(MainForm.mainForm.Opponent_cnt_fire.Text);   //현재 불 마나
            refreshMana();
        }
        //=====[마나 새로고침]=====
        private void refreshMana()
        {
            MainForm.mainForm.setText_My_remain_dark(My_remain_dark.ToString());
            MainForm.mainForm.setText_My_remain_fire(My_remain_fire.ToString());
            MainForm.mainForm.setText_My_use_all(all_count.ToString());

            MainForm.mainForm.setText_Opponent_remain_dark(Opponent_remain_dark.ToString());
            MainForm.mainForm.setText_Opponent_remain_fire(Opponent_remain_fire.ToString());
            MainForm.mainForm.setText_Opponent_use_all(all_count.ToString());
        }



        //=====[ 마나 생성 ]=====
        public void makeMana(Card_Control card_con)
        {
            if (GamePlayManager.Instance.CMR)   //마나생성이 가능할 경우
            {
                if (TurnManager.Turn)
                {
                    moveZone(card_con, GameBoard.MY_MANAZONE);   //마나존으로 이동
                    if (card_con.card.Attribute.Equals("암흑"))
                    {
                        MainForm.mainForm.setText_My_cnt_dark((Convert.ToInt32(MainForm.mainForm.My_cnt_dark.Text) + 1).ToString());
                        My_remain_dark++;
                    }
                    else if (card_con.card.Attribute.Equals("불"))
                    {
                        MainForm.mainForm.setText_My_cnt_fire((Convert.ToInt32(MainForm.mainForm.My_cnt_fire.Text) + 1).ToString());
                        My_remain_fire++;
                    }
                }
                else
                {
                    moveZone(card_con, GameBoard.OPPONENT_MANAZONE);   //마나존으로 이동
                    if (card_con.card.Attribute.Equals("암흑"))
                    {
                        MainForm.mainForm.setText_Opponent_cnt_dark((Convert.ToInt32(MainForm.mainForm.Opponent_cnt_dark.Text) + 1).ToString());
                        Opponent_remain_dark++;
                    }
                    else if (card_con.card.Attribute.Equals("불"))
                    {
                        MainForm.mainForm.setText_Opponent_cnt_fire((Convert.ToInt32(MainForm.mainForm.Opponent_cnt_fire.Text) + 1).ToString());
                        Opponent_remain_fire++;
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

        public int My_remain_dark = 0;
        public int My_remain_fire = 0;
        public int Opponent_remain_dark = 0;
        public int Opponent_remain_fire = 0;

        //====[마나 확인]====
        public bool canPopCard(Card_Control card_con)
        {
            string[] consump = card_con.card.Consumption.Split(';');
            int need_dark = Convert.ToInt32(consump[1]);    //필요 암흑 마나
            int need_fire = Convert.ToInt32(consump[0]);    //필요 불 마나
            int need_all = Convert.ToInt32(consump[2]);     //필요 아무 마나

            if (TurnManager.Turn)
            {
                //===내기위한 마나 처리
                if (My_remain_dark >= need_dark && My_remain_fire >= need_fire && (((My_remain_dark - need_dark) + (My_remain_fire - need_fire)) - all_count) >= need_all)
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
                if (Opponent_remain_dark >= need_dark && Opponent_remain_fire >= need_fire && (((Opponent_remain_dark - need_dark) + (Opponent_remain_fire - need_fire)) - all_count) >= need_all)
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

            if (TurnManager.Turn)
            {
                //===내기위한 마나 처리
                if (My_remain_dark >= need_dark && My_remain_fire >= need_fire && (((My_remain_dark - need_dark) + (My_remain_fire - need_fire)) - all_count) > need_all)
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
                if (Opponent_remain_dark >= need_dark && Opponent_remain_fire >= need_fire && (((Opponent_remain_dark - need_dark) + (Opponent_remain_fire - need_fire)) - all_count) > need_all)
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

            if (TurnManager.Turn)
            {
                //----[마나 연산]----
                My_remain_dark -= need_dark;
                My_remain_fire -= need_fire;
                all_count += need_all;
                refreshMana();//마나 새로고침
            }
            else
            {
                //----[마나 연산]----
                Opponent_remain_dark -= need_dark;
                Opponent_remain_fire -= need_fire;
                all_count += need_all;

                refreshMana();//마나 새로고침
            }

        }

        internal void popCard(Card_Control card_con)
        {
            if (TurnManager.Turn)
            {
                if (card_con.card.Type.Equals("마법"))
                {
                    procMana(card_con);
                    moveZone(card_con, GameBoard.MY_TOMBZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("미니언"))
                {
                    procMana(card_con);
                    moveZone(card_con, GameBoard.MY_BATTLEZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("드래곤"))
                {
                    procMana(card_con);
                    moveZone(card_con, GameBoard.MY_BATTLEZONE);//카드 내기
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
                    moveZone(card_con, GameBoard.OPPONENT_TOMBZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("미니언"))
                {
                    procMana(card_con);
                    moveZone(card_con, GameBoard.OPPONENT_BATTLEZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("드래곤"))
                {
                    procMana(card_con);
                    moveZone(card_con, GameBoard.OPPONENT_BATTLEZONE);//카드 내기
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
                    selectCard.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_deselect;
                }
                if (targetCard != null)
                {
                    targetCard.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_deselect;
                }
                selectCard = null;
                targetCard = null;
            }//===============================
            else
            {//=====[ 동작 ]=====
                card_con.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_select;
                if (selectCard == null)
                {
                    selectCard = card_con;
                }
                else if (selectCard.Equals(card_con))
                {
                    selectCard.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_deselect;
                    selectCard = null;
                    targetCard = null;
                }
                else
                {
                    targetCard = card_con;
                    //=====[전투 처리 메소드 추가]=====
                    if (targetCard != null && selectCard != null)
                    {
                        canMatchCheck(selectCard, targetCard);  //매치 가능여부 체크 후 매치
                    }
                    ///////////////////////////////////
                    selectCard.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_deselect;
                    targetCard.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_deselect;
                    selectCard = null;
                    targetCard = null;
                }
            }
        }

        public void canMatchCheck(Card_Control selectCard, Card_Control targetCard)
        {
            if (TurnManager.Turn)
            {
                if (GameBoard.My_BattleZone.Contains(selectCard) && (GameBoard.Opponent_BattleZone.Contains(targetCard) || GameBoard.Opponent_PlayerZone.Equals(targetCard)))
                {
                    NetworkManager.ws.Send("MatchCard;" + GamePlayManager.Instance.findCard(selectCard) + GamePlayManager.Instance.findCard(targetCard));
                }
            }
            else
            {
                if (GameBoard.Opponent_BattleZone.Contains(selectCard) && (GameBoard.My_BattleZone.Contains(targetCard) || GameBoard.My_PlayerZone.Equals(targetCard)))
                {
                    NetworkManager.ws.Send("MatchCard;" + GamePlayManager.Instance.findCard(selectCard) + GamePlayManager.Instance.findCard(targetCard));
                }
            }
        }

        //=====[ Match ]=====
        public void MatchCard(Card_Control selectCard, Card_Control targetCard)
        {
            if (GameSkillManager.Instance.list_skill19.Contains(selectCard) || GameSkillManager.Instance.list_skill19.Contains(targetCard))
            {
                if (TurnManager.Turn)
                {
                    moveZone(selectCard, GameBoard.MY_TOMBZONE);
                    moveZone(targetCard, GameBoard.OPPONENT_TOMBZONE);
                }
                else
                {
                    moveZone(selectCard, GameBoard.OPPONENT_TOMBZONE);
                    moveZone(targetCard, GameBoard.MY_TOMBZONE);
                }
            }
            else
            {
                if (targetCard.card.Skill.Equals("13"))
                {
                    if (!GameSkillManager.Instance.list_skill13_used.Contains(targetCard))
                    {
                        targetCard.card.TurnHP += 2;
                        targetCard.card.TurnAP += 2;

                        selectCard.card.TurnHP -= targetCard.card.TurnAP;
                        targetCard.card.TurnHP -= selectCard.card.TurnAP;
                        GameSkillManager.Instance.list_skill13_used.Add(targetCard);
                    }
                    else
                    {
                        selectCard.card.TurnHP -= targetCard.card.TurnAP;
                        targetCard.card.TurnHP -= selectCard.card.TurnAP;
                    }
                }
                else
                {
                    selectCard.card.TurnHP -= targetCard.card.TurnAP;
                    targetCard.card.TurnHP -= selectCard.card.TurnAP;
                }
            }

            if (TurnManager.Turn)
            {
                if (GameSkillManager.Instance.list_skill18.Contains(selectCard))
                {
                    GameBoard.My_PlayerZone.card.TurnHP += selectCard.card.TurnAP;
                }
                if (GameSkillManager.Instance.list_skill18.Contains(targetCard))
                {
                    GameBoard.Opponent_PlayerZone.card.TurnHP += targetCard.card.TurnAP;
                }
            }
            else
            {
                if (GameSkillManager.Instance.list_skill18.Contains(selectCard))
                {
                    GameBoard.Opponent_PlayerZone.card.TurnHP += selectCard.card.TurnAP;
                }
                if (GameSkillManager.Instance.list_skill18.Contains(targetCard))
                {
                    GameBoard.My_PlayerZone.card.TurnHP += targetCard.card.TurnAP;
                }
            }


            GameBoard.My_PlayerZone.setText_lb_aphp(GameBoard.My_PlayerZone.card.TurnAP + " / " + GameBoard.My_PlayerZone.card.TurnHP);
            GameBoard.Opponent_PlayerZone.setText_lb_aphp(GameBoard.Opponent_PlayerZone.card.TurnAP + " / " + GameBoard.Opponent_PlayerZone.card.TurnHP);

            selectCard.setText_lb_aphp(selectCard.card.TurnAP + " / " + selectCard.card.TurnHP);
            targetCard.setText_lb_aphp(targetCard.card.TurnAP + " / " + targetCard.card.TurnHP);

            if (TurnManager.Turn)
            {
                if (selectCard.card.TurnHP <= 0)
                {
                    moveZone(selectCard, GameBoard.MY_TOMBZONE);
                }
                if (targetCard.card.TurnHP <= 0)
                {
                    moveZone(targetCard, GameBoard.OPPONENT_TOMBZONE);
                }
            }
            else
            {
                if (selectCard.card.TurnHP <= 0)
                {
                    moveZone(selectCard, GameBoard.OPPONENT_TOMBZONE);
                }
                if (targetCard.card.TurnHP <= 0)
                {
                    moveZone(targetCard, GameBoard.MY_TOMBZONE);
                }
            }
            selectCard.activatable = false;
            selectCard.setCardEnabled(false);
        }


        //=====[카드 불러오기]=====
        private string strConn = "Server=14.63.170.220;Database=dragonwarlord;Uid=haeggong2;Pwd=Rhdtm11;";
        public MySqlConnection conn;
        public MySqlCommand cmd;
        private string query = "select * from user where _id = '1'";


        public void inputCard()
        {
            conn = new MySqlConnection(strConn);
            conn.Open();

            createCard();
            conn.Close();

            shuffle(GameBoard.My_CardDeck, 10);
            shuffle(GameBoard.Opponent_CardDeck, 10);

        }

        public void createCard()
        {
            query = "select * from card";
            cmd = new MySqlCommand(query);

            cmd.Connection = conn;
            MySqlDataReader reader = cmd.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.GetInt16(10); i++)
                    {
                        moveZone(new Card_Control()
                        {
                            card = new Card()
                            {
                                Name = reader.GetString(1),
                                Attribute = reader.GetString(2),
                                Type = reader.GetString(3),
                                Class = reader.GetString(4),
                                Species = (reader.IsDBNull(5)) ? "" : reader.GetString(5),
                                Consumption = reader.GetString(6),
                                Ap = reader.GetInt16(7),
                                Hp = reader.GetInt16(8),
                                Rp = reader.GetInt16(9),
                                Limited_amount = reader.GetInt16(10),
                                Skill = reader.GetString(11),
                                Information = (reader.IsDBNull(12)) ? "" : reader.GetString(12),
                                Image_file = reader.GetString(13),
                                TurnAP = reader.GetInt16(7),
                                TurnHP = reader.GetInt16(8),
                            }
                        }, GameBoard.MY_CARDDECK);

                        moveZone(new Card_Control()
                        {
                            card = new Card()
                            {
                                Name = reader.GetString(1),
                                Attribute = reader.GetString(2),
                                Type = reader.GetString(3),
                                Class = reader.GetString(4),
                                Species = (reader.IsDBNull(5)) ? "" : reader.GetString(5),
                                Consumption = reader.GetString(6),
                                Ap = reader.GetInt16(7),
                                Hp = reader.GetInt16(8),
                                Rp = reader.GetInt16(9),
                                Limited_amount = reader.GetInt16(10),
                                Skill = reader.GetString(11),
                                Information = (reader.IsDBNull(12)) ? "" : reader.GetString(12),
                                Image_file = reader.GetString(13),
                                TurnAP = reader.GetInt16(7),
                                TurnHP = reader.GetInt16(8),
                            }
                        }, GameBoard.OPPONENT_CARDDECK);

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

        }
        
        //=====[ 플레이어 생성 ]=====
        public void makeMainPlayer()
        {
            Card_Control MY = new Card_Control()
            {
                card = new Card()
                {
                    Name = "장석이",
                    Attribute = "",
                    Type = "플레이어",
                    Class = "플레이어",
                    Species = "기획/개발",
                    Consumption = "0;0;0",
                    Ap = 0,
                    Hp = 30,
                    Rp = 100,
                    Limited_amount = 1,
                    Skill = "",
                    Information = "노코멘트",
                    Image_file = "jang",
                    TurnAP = 0,
                    TurnHP = 30,
                }
            };
            Card_Control OPPONENT = new Card_Control()
            {
                card = new Card()
                {
                    Name = "오테리",
                    Attribute = "",
                    Type = "플레이어",
                    Class = "플레이어",
                    Species = "기획자",
                    Consumption = "0;0;0",
                    Ap = 0,
                    Hp = 30,
                    Rp = 100,
                    Limited_amount = 1,
                    Skill = "",
                    Information = "노코멘트",
                    Image_file = "taelimoh",
                    TurnAP = 0,
                    TurnHP = 30,
                }
            };
            GameBoard.My_PlayerZone = MY;
            GameBoard.Opponent_PlayerZone = OPPONENT;
            MainForm.mainForm.My_Player.Controls.Add(MY);
            MainForm.mainForm.Opponent_Player.Controls.Add(OPPONENT);
        }

        //=====[ 카드 섞기 ]=====
        private static Random _rnd = new Random();

        public void shuffle(List<Card_Control> cardDeckShuffle, int numberOfTimesToShuffle)
        {
            List<Card_Control> newList = new List<Card_Control>();
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
        public void firstDistribute()
        {
            moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);
            moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);
            moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);
            moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);
            moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);

            moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
            moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
            moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
            moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
            moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
        }

        //=====[ 카드 분배 ]=====
        public void Distribute()
        {
            if (TurnManager.Turn)
            {
                moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);
                moveZone(GameBoard.My_CardDeck[0], GameBoard.MY_HANDSZONE);
            }
            else
            {
                moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
                moveZone(GameBoard.Opponent_CardDeck[0], GameBoard.OPPONENT_HANDSZONE);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////

        //======[게임 승패 결정]=====
        private void judgment_Winner(String winner)
        {
            MessageBox.Show("[" + winner + "] 님이 승리하셨습니다.");
            Application.Restart();
        }
        public Card_Control tombCard;

        //======[카드 존 이동]========
        public void moveZone(Card_Control card_con, int position)
        {
            //=====[기존 리스트 탐색]=====
            int pre_position = 0;

            if (GameBoard.My_CardDeck.Contains(card_con))
            {
                pre_position = GameBoard.MY_CARDDECK;
            }
            else if (GameBoard.Opponent_CardDeck.Contains(card_con))
            {
                pre_position = GameBoard.OPPONENT_CARDDECK;
            }
            else if (GameBoard.My_HandsZone.Contains(card_con))
            {
                pre_position = GameBoard.MY_HANDSZONE;
            }
            else if (GameBoard.Opponent_HandsZone.Contains(card_con))
            {
                pre_position = GameBoard.OPPONENT_HANDSZONE;
            }
            else if (GameBoard.My_BattleZone.Contains(card_con))
            {
                pre_position = GameBoard.MY_BATTLEZONE;
            }
            else if (GameBoard.Opponent_BattleZone.Contains(card_con))
            {
                pre_position = GameBoard.OPPONENT_BATTLEZONE;
            }
            else if (GameBoard.My_ManaZone.Contains(card_con))
            {
                pre_position = GameBoard.MY_MANAZONE;
            }
            else if (GameBoard.Opponent_ManaZone.Contains(card_con))
            {
                pre_position = GameBoard.OPPONENT_MANAZONE;
            }
            else if (GameBoard.My_TombZone.Contains(card_con))
            {
                pre_position = GameBoard.MY_TOMBZONE;
            }
            else if (GameBoard.Opponent_TombZone.Contains(card_con))
            {
                pre_position = GameBoard.OPPONENT_TOMBZONE;
            }
            else if (GameBoard.My_PlayerZone.Equals(card_con))
            {
                pre_position = GameBoard.MY_PLAYERZONE;
            }
            else if (GameBoard.Opponent_PlayerZone.Equals(card_con))
            {
                pre_position = GameBoard.OPPONENT_PLAYERZONE;
            }
            else
            {
                pre_position = 0;
            }

            //=====[기본 리스트에서 삭제]=====
            switch (pre_position)
            {
                case GameBoard.MY_CARDDECK:
                    GameBoard.My_CardDeck.Remove(card_con);
                    break;
                case GameBoard.OPPONENT_CARDDECK:
                    GameBoard.Opponent_CardDeck.Remove(card_con);
                    break;
                case GameBoard.MY_HANDSZONE:
                    GameBoard.My_HandsZone.Remove(card_con);                   
                    MainForm.mainForm.remove_My_Hands(card_con);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Remove(card_con);
                    MainForm.mainForm.remove_Opponent_Hands(card_con);
                    break;
                case GameBoard.MY_BATTLEZONE:
                    GameBoard.My_BattleZone.Remove(card_con);
                    MainForm.mainForm.remove_My_BattleZone(card_con);
                    break;
                case GameBoard.OPPONENT_BATTLEZONE:
                    GameBoard.Opponent_BattleZone.Remove(card_con);
                    MainForm.mainForm.remove_Opponent_BattleZone(card_con);
                    break;
                case GameBoard.MY_MANAZONE:
                    GameBoard.My_ManaZone.Remove(card_con);
                    MainForm.mainForm.remove_My_ManaZone(card_con);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Remove(card_con);
                    MainForm.mainForm.remove_Opponent_ManaZone(card_con);
                    break;
                case GameBoard.MY_TOMBZONE:
                    GameBoard.My_TombZone.Remove(card_con);
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    GameBoard.Opponent_TombZone.Remove(card_con);
                    break;
                case GameBoard.MY_PLAYERZONE:
                    judgment_Winner("OPPONENT");
                    break;
                case GameBoard.OPPONENT_PLAYERZONE:
                    judgment_Winner("MY");
                    break;
            }
            //=====[새로운 포지션 배치]=====
            card_con.position = position;
            switch (position)
            {
                case GameBoard.MY_CARDDECK:
                    GameBoard.My_CardDeck.Add(card_con);
                    break;
                case GameBoard.OPPONENT_CARDDECK:
                    GameBoard.Opponent_CardDeck.Add(card_con);
                    break;
                case GameBoard.MY_HANDSZONE:
                    GameBoard.My_HandsZone.Add(card_con);
                    MainForm.mainForm.add_My_Hands(card_con);
                    card_con.activatable = true;
                    card_con.setCardEnabled(true);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Add(card_con);
                    MainForm.mainForm.add_Opponent_Hands(card_con);
                    card_con.activatable = true;
                    card_con.setCardEnabled(true);
                    break;
                case GameBoard.MY_BATTLEZONE:
                    GameBoard.My_BattleZone.Add(card_con);                   
                    MainForm.mainForm.add_My_BattleZone(card_con);
                    card_con.activatable = false;
                    card_con.setCardEnabled(false);
                    break;
                case GameBoard.OPPONENT_BATTLEZONE:
                    GameBoard.Opponent_BattleZone.Add(card_con);
                    MainForm.mainForm.add_Opponent_BattleZone(card_con);;
                    card_con.activatable = false;
                    card_con.setCardEnabled(false);
                    break;
                case GameBoard.MY_MANAZONE:
                    GameBoard.My_ManaZone.Add(card_con);
                    MainForm.mainForm.add_My_ManaZone(card_con);
                    card_con.activatable = false;
                    card_con.setCardEnabled(false);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Add(card_con);                   
                    MainForm.mainForm.add_Opponent_ManaZone(card_con);
                    card_con.activatable = false;
                    card_con.setCardEnabled(false);
                    break;
                case GameBoard.MY_TOMBZONE:
                    if (card_con.Equals(tombCard))
                    {
                        GameSkillManager.Instance.skill_8_My = false;
                    }
                    else
                    {
                        if (GameSkillManager.Instance.skill_8_My)
                        {
                            GameBoard.My_PlayerZone.card.Hp++;
                            GameBoard.My_PlayerZone.lb_aphp.Text = GameBoard.My_PlayerZone.card.Ap + " / " + GameBoard.My_PlayerZone.card.Hp;
                        }
                        if (GameSkillManager.Instance.skill_8_Opponent)
                        {
                            GameBoard.Opponent_PlayerZone.card.Hp++;
                            GameBoard.Opponent_PlayerZone.lb_aphp.Text = GameBoard.Opponent_PlayerZone.card.Ap + " / " + GameBoard.Opponent_PlayerZone.card.Hp;
                        }
                    }

                    GameBoard.My_TombZone.Add(card_con);
                    MainForm.mainForm.add_My_TombZone(card_con);
                    card_con.activatable = false;
                    card_con.setCardEnabled(false);

                    cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    if (card_con.Equals(tombCard))
                    {
                        GameSkillManager.Instance.skill_8_Opponent = false;
                    }
                    else
                    {
                        if (GameSkillManager.Instance.skill_8_My)
                        {
                            GameBoard.My_PlayerZone.card.Hp++;
                            GameBoard.My_PlayerZone.lb_aphp.Text = GameBoard.My_PlayerZone.card.Ap + " / " + GameBoard.My_PlayerZone.card.Hp;
                        }
                        if (GameSkillManager.Instance.skill_8_Opponent)
                        {
                            GameBoard.Opponent_PlayerZone.card.Hp++;
                            GameBoard.Opponent_PlayerZone.lb_aphp.Text = GameBoard.Opponent_PlayerZone.card.Ap + " / " + GameBoard.Opponent_PlayerZone.card.Hp;
                        }
                    }

                    GameBoard.Opponent_TombZone.Add(card_con);
                    MainForm.mainForm.add_Opponent_TombZone(card_con);
                    card_con.activatable = false;
                    card_con.setCardEnabled(false);

                    cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
            }
        }

        public string findCard(Card_Control card)
        {
            int index = 0;
            switch (card.position)
            {
                case GameBoard.MY_CARDDECK:
                    card.position = GameBoard.MY_CARDDECK;
                    index = GameBoard.My_CardDeck.IndexOf(card);
                    break;
                case GameBoard.OPPONENT_CARDDECK:
                    card.position = GameBoard.OPPONENT_CARDDECK;
                    index = GameBoard.Opponent_CardDeck.IndexOf(card);
                    break;
                case GameBoard.MY_HANDSZONE:
                    card.position = GameBoard.MY_HANDSZONE;
                    index = GameBoard.My_HandsZone.IndexOf(card);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    card.position = GameBoard.OPPONENT_HANDSZONE;
                    index = GameBoard.Opponent_HandsZone.IndexOf(card);
                    break;
                case GameBoard.MY_BATTLEZONE:
                    card.position = GameBoard.MY_BATTLEZONE;
                    index = GameBoard.My_BattleZone.IndexOf(card);
                    break;
                case GameBoard.OPPONENT_BATTLEZONE:
                    card.position = GameBoard.OPPONENT_BATTLEZONE;
                    index = GameBoard.Opponent_BattleZone.IndexOf(card);
                    break;
                case GameBoard.MY_MANAZONE:
                    card.position = GameBoard.MY_MANAZONE;
                    index = GameBoard.My_ManaZone.IndexOf(card);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    card.position = GameBoard.OPPONENT_MANAZONE;
                    index = GameBoard.Opponent_ManaZone.IndexOf(card);
                    break;
                case GameBoard.MY_TOMBZONE:
                    card.position = GameBoard.MY_TOMBZONE;
                    index = GameBoard.My_TombZone.IndexOf(card);
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    card.position = GameBoard.OPPONENT_TOMBZONE;
                    index = GameBoard.Opponent_TombZone.IndexOf(card);
                    break;
                case GameBoard.MY_PLAYERZONE:
                    card.position = GameBoard.MY_PLAYERZONE;
                    index = 0;
                    break;
                case GameBoard.OPPONENT_PLAYERZONE:
                    card.position = GameBoard.OPPONENT_PLAYERZONE;
                    index = 0;
                    break;
            }
            return (card.position + ";"+ index + ";");
        }

        public Card_Control findCard(string position, string index)
        {
            Card_Control card = null;
            switch (Convert.ToInt32(position))
            {
                case GameBoard.MY_CARDDECK:
                    card = GameBoard.My_CardDeck[Convert.ToInt32(index)];
                    break;
                case GameBoard.OPPONENT_CARDDECK:                   
                    card = GameBoard.Opponent_CardDeck[Convert.ToInt32(index)];
                    break;
                case GameBoard.MY_HANDSZONE:
                    card = GameBoard.My_HandsZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    card = GameBoard.Opponent_HandsZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.MY_BATTLEZONE:
                    card = GameBoard.My_BattleZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.OPPONENT_BATTLEZONE:
                    card = GameBoard.Opponent_BattleZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.MY_MANAZONE:
                    card = GameBoard.My_ManaZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    card = GameBoard.Opponent_ManaZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.MY_TOMBZONE:
                    card = GameBoard.My_TombZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    card = GameBoard.Opponent_TombZone[Convert.ToInt32(index)];
                    break;
                case GameBoard.MY_PLAYERZONE:
                    card = GameBoard.My_PlayerZone;
                    break;
                case GameBoard.OPPONENT_PLAYERZONE:
                    card = GameBoard.Opponent_PlayerZone;
                    break;
            }
            return card;
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
