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
            foreach (Card_Control card in GameBoard.P1_HandsZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.P2_HandsZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.P1_WarZone)
            {
                card.activatable = true;
                card.Enabled = true;
                card.Card_refresh();
            }
            foreach (Card_Control card in GameBoard.P2_WarZone)
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
                foreach (Card_Control card in GameBoard.P1_HandsZone)
                {
                    card.activatable = true;
                    card.Enabled = true;
                    card.Card_refresh();
                }
                foreach (Card_Control card in GameBoard.P2_HandsZone)
                {

                    card.activatable = false;
                    card.Enabled = false;
                    card.Card_refresh();
                }
            }
            else
            {
                foreach (Card_Control card in GameBoard.P1_HandsZone)
                {
                    card.activatable = false;
                    card.Enabled = false;
                    card.Card_refresh();
                }
                foreach (Card_Control card in GameBoard.P2_HandsZone)
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
            p1_remain_dark = Convert.ToInt32(mainForm.p1_cnt_dark.Text);   //현재 암흑 마나
            p1_remain_fire = Convert.ToInt32(mainForm.p1_cnt_fire.Text);   //현재 불 마나
            p2_remain_dark = Convert.ToInt32(mainForm.p2_cnt_dark.Text);   //현재 암흑 마나
            p2_remain_fire = Convert.ToInt32(mainForm.p2_cnt_fire.Text);   //현재 불 마나
            refreshMana();
        }
        //=====[마나 새로고침]=====
        private void refreshMana()
        {
            mainForm.p1_remain_dark.Text = p1_remain_dark.ToString();
            mainForm.p1_remain_fire.Text = p1_remain_fire.ToString();
            mainForm.p1_use_all.Text = all_count.ToString();

            mainForm.p2_remain_dark.Text = p2_remain_dark.ToString();
            mainForm.p2_remain_fire.Text = p2_remain_fire.ToString();
            mainForm.p2_use_all.Text = all_count.ToString();
        }


        //=====[ 마나 생성 ]=====
        public void makeMana(Card_Control card_con)
        {
            if (GamePlayManager.Instance.canMakeMana)   //마나생성이 가능할 경우
            {
                if (GamePlayManager.Instance.thisturn)
                {
                    moveZone(card_con, PLAYER1_MANAZONE);   //마나존으로 이동
                    if (card_con.card.Attribute.Equals("암흑"))
                    {
                        mainForm.p1_cnt_dark.Text = (Convert.ToInt32(mainForm.p1_cnt_dark.Text) + 1).ToString();
                        p1_remain_dark++;
                    }
                    else if (card_con.card.Attribute.Equals("불"))
                    {
                        mainForm.p1_cnt_fire.Text = (Convert.ToInt32(mainForm.p1_cnt_fire.Text) + 1).ToString();
                        p1_remain_fire++;
                    }
                }
                else
                {
                    moveZone(card_con, PLAYER2_MANAZONE);   //마나존으로 이동
                    if (card_con.card.Attribute.Equals("암흑"))
                    {
                        mainForm.p2_cnt_dark.Text = (Convert.ToInt32(mainForm.p2_cnt_dark.Text) + 1).ToString();
                        p2_remain_dark++;
                    }
                    else if (card_con.card.Attribute.Equals("불"))
                    {
                        mainForm.p2_cnt_fire.Text = (Convert.ToInt32(mainForm.p2_cnt_fire.Text) + 1).ToString();
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
                    moveZone(card_con, PLAYER1_TOMBZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("미니언"))
                {
                    procMana(card_con);
                    moveZone(card_con, PLAYER1_WARZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("드래곤"))
                {
                    procMana(card_con);
                    moveZone(card_con, PLAYER1_WARZONE);//카드 내기
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
                    moveZone(card_con, PLAYER2_TOMBZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("미니언"))
                {
                    procMana(card_con);
                    moveZone(card_con, PLAYER2_WARZONE);//카드 내기
                }
                else if (card_con.card.Type.Equals("드래곤"))
                {
                    procMana(card_con);
                    moveZone(card_con, PLAYER2_WARZONE);//카드 내기
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
                if (GameBoard.P1_WarZone.Contains(selectCard) && (GameBoard.P2_WarZone.Contains(targetCard) || GameBoard.P2_PlayerZone.Equals(targetCard)))
                {
                    MatchCard(selectCard, targetCard);
                }
            }
            else
            {
                if (GameBoard.P2_WarZone.Contains(selectCard) && (GameBoard.P1_WarZone.Contains(targetCard) || GameBoard.P1_PlayerZone.Equals(targetCard)))
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
                    moveZone(selectCard, PLAYER1_TOMBZONE);
                    moveZone(targetCard, PLAYER2_TOMBZONE);
                }
                else
                {
                    moveZone(selectCard, PLAYER2_TOMBZONE);
                    moveZone(targetCard, PLAYER1_TOMBZONE);
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
                    GameBoard.P1_PlayerZone.card.thisTurnHP += selectCard.card.thisTurnAP;
                }
                if (GameSkillManager.Instance.list_skill18.Contains(targetCard))
                {
                    GameBoard.P2_PlayerZone.card.thisTurnHP += targetCard.card.thisTurnAP;
                }
            }
            else
            {
                if (GameSkillManager.Instance.list_skill18.Contains(selectCard))
                {
                    GameBoard.P2_PlayerZone.card.thisTurnHP += selectCard.card.thisTurnAP;
                }
                if (GameSkillManager.Instance.list_skill18.Contains(targetCard))
                {
                    GameBoard.P1_PlayerZone.card.thisTurnHP += targetCard.card.thisTurnAP;
                }
            }


            GameBoard.P1_PlayerZone.lb_aphp.Text = GameBoard.P1_PlayerZone.card.thisTurnAP + " / " + GameBoard.P1_PlayerZone.card.thisTurnHP;
            GameBoard.P2_PlayerZone.lb_aphp.Text = GameBoard.P2_PlayerZone.card.thisTurnAP + " / " + GameBoard.P2_PlayerZone.card.thisTurnHP;
            selectCard.lb_aphp.Text = selectCard.card.thisTurnAP + " / " + selectCard.card.thisTurnHP;
            targetCard.lb_aphp.Text = targetCard.card.thisTurnAP + " / " + targetCard.card.thisTurnHP;

            if (thisturn)
            {
                if (selectCard.card.thisTurnHP <= 0)
                {
                    moveZone(selectCard, PLAYER1_TOMBZONE);
                }
                if (targetCard.card.thisTurnHP <= 0)
                {
                    moveZone(targetCard, PLAYER2_TOMBZONE);
                }
            }
            else
            {
                if (selectCard.card.thisTurnHP <= 0)
                {
                    moveZone(selectCard, PLAYER2_TOMBZONE);
                }
                if (targetCard.card.thisTurnHP <= 0)
                {
                    moveZone(targetCard, PLAYER1_TOMBZONE);
                }
            }
            selectCard.activatable = false;
            selectCard.Enabled = false;
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

            shuffle(GameBoard.P1_CardDeck, 10);
            shuffle(GameBoard.P2_CardDeck, 10);

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
                                thisTurnAP = reader.GetInt16(7),
                                thisTurnHP = reader.GetInt16(8),
                            }
                        }, PLAYER1_CARDDECK);

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
                                thisTurnAP = reader.GetInt16(7),
                                thisTurnHP = reader.GetInt16(8),
                            }
                        }, PLAYER2_CARDDECK);

                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

        }
        /*
        //=====[카드 불러오기]=====
        public void inputCard()
        {
            string strExcelFile = @"D:\Highbrow\GitHub\Warlord\ServerProgramming\DragonWarLord_preprototype\DragonWarLord_preprototype\res\card.xlsx";
            string strConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                     + strExcelFile
                                     + ";Extended Properties='Excel 8.0;HDR=YES'";
            OleDbConnection conn = new OleDbConnection(strConnStr);
            conn.Open();

            // 엑셀로부터 데이타 읽기
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int i = 0; i < Convert.ToInt32(dr[10]); i++)
                {
                    moveZone(new Card_Control()
                    {
                        card = new Card()
                        {
                            Name = dr[1].ToString(),
                            Attribute = dr[2].ToString(),
                            Type = dr[3].ToString(),
                            Class = dr[4].ToString(),
                            Species = dr[5].ToString(),
                            Consumption = dr[6].ToString(),
                            Ap = Convert.ToInt32(dr[7]),
                            Hp = Convert.ToInt32(dr[8]),
                            Rp = Convert.ToInt32(dr[9]),
                            Limited_amount = Convert.ToInt32(dr[10]),
                            Skill = dr[11].ToString(),
                            Information = dr[12].ToString(),
                            thisTurnAP = Convert.ToInt32(dr[7]),
                            thisTurnHP = Convert.ToInt32(dr[8]),
                        }
                    }, PLAYER1_CARDDECK);

                    moveZone(new Card_Control()
                    {
                        card = new Card()
                        {
                            Name = dr[1].ToString(),
                            Attribute = dr[2].ToString(),
                            Type = dr[3].ToString(),
                            Class = dr[4].ToString(),
                            Species = dr[5].ToString(),
                            Consumption = dr[6].ToString(),
                            Ap = Convert.ToInt32(dr[7]),
                            Hp = Convert.ToInt32(dr[8]),
                            Rp = Convert.ToInt32(dr[9]),
                            Limited_amount = Convert.ToInt32(dr[10]),
                            Skill = dr[11].ToString(),
                            Information = dr[12].ToString(),
                            thisTurnAP = Convert.ToInt32(dr[7]),
                            thisTurnHP = Convert.ToInt32(dr[8]),
                        }
                    }, PLAYER2_CARDDECK);
                }
            }



            conn.Close();

            shuffle(GameBoard.P1_CardDeck, 10);
            shuffle(GameBoard.P2_CardDeck, 10);

        }
        */
        //=====[ 플레이어 생성 ]=====
        public void makeMainPlayer()
        {
            Card_Control player1 = new Card_Control()
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
                    thisTurnAP = 0,
                    thisTurnHP = 30,
                }
            };
            Card_Control player2 = new Card_Control()
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
                    thisTurnAP = 0,
                    thisTurnHP = 30,
                }
            };
            GameBoard.P1_PlayerZone = player1;
            GameBoard.P2_PlayerZone = player2;
            mainForm.p1_Player.Controls.Add(player1);
            mainForm.p2_Player.Controls.Add(player2);
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
            moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);
            moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);
            moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);

            moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);
            moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);
            moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);

            if (thisturn)
            {
                moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);
                moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);
            }
            else
            {
                moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);
                moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);
            }
        }

        //=====[ 카드 분배 ]=====
        public void distribute()
        {
            if (thisturn)
            {
                moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);
                moveZone(GameBoard.P1_CardDeck[0], PLAYER1_HANDSZONE);
            }
            else
            {
                moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);
                moveZone(GameBoard.P2_CardDeck[0], PLAYER2_HANDSZONE);
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

            if (GameBoard.P1_CardDeck.Contains(card_con))
            {
                pre_position = PLAYER1_CARDDECK;
            }
            else if (GameBoard.P2_CardDeck.Contains(card_con))
            {
                pre_position = PLAYER2_CARDDECK;
            }
            else if (GameBoard.P1_HandsZone.Contains(card_con))
            {
                pre_position = PLAYER1_HANDSZONE;
            }
            else if (GameBoard.P2_HandsZone.Contains(card_con))
            {
                pre_position = PLAYER2_HANDSZONE;
            }
            else if (GameBoard.P1_WarZone.Contains(card_con))
            {
                pre_position = PLAYER1_WARZONE;
            }
            else if (GameBoard.P2_WarZone.Contains(card_con))
            {
                pre_position = PLAYER2_WARZONE;
            }
            else if (GameBoard.P1_ManaZone.Contains(card_con))
            {
                pre_position = PLAYER1_MANAZONE;
            }
            else if (GameBoard.P2_ManaZone.Contains(card_con))
            {
                pre_position = PLAYER2_MANAZONE;
            }
            else if (GameBoard.P1_TombZone.Contains(card_con))
            {
                pre_position = PLAYER1_TOMBZONE;
            }
            else if (GameBoard.P2_TombZone.Contains(card_con))
            {
                pre_position = PLAYER2_TOMBZONE;
            }
            else if (GameBoard.P1_PlayerZone.Equals(card_con))
            {
                pre_position = PLAYER1_PLAYERZONE;
            }
            else if (GameBoard.P2_PlayerZone.Equals(card_con))
            {
                pre_position = PLAYER2_PLAYERZONE;
            }
            else
            {
                pre_position = 0;
            }

            //=====[기본 리스트에서 삭제]=====
            switch (pre_position)
            {
                case PLAYER1_CARDDECK:
                    GameBoard.P1_CardDeck.Remove(card_con);
                    break;
                case PLAYER2_CARDDECK:
                    GameBoard.P2_CardDeck.Remove(card_con);
                    break;
                case PLAYER1_HANDSZONE:
                    GameBoard.P1_HandsZone.Remove(card_con);
                    mainForm.p1_hands_frame.Controls.Remove(card_con);
                    break;
                case PLAYER2_HANDSZONE:
                    GameBoard.P2_HandsZone.Remove(card_con);
                    mainForm.p2_hands_frame.Controls.Remove(card_con);
                    break;
                case PLAYER1_WARZONE:
                    GameBoard.P1_WarZone.Remove(card_con);
                    mainForm.p1_warZone_frame.Controls.Remove(card_con);
                    break;
                case PLAYER2_WARZONE:
                    GameBoard.P2_WarZone.Remove(card_con);
                    mainForm.p2_warZone_frame.Controls.Remove(card_con);
                    break;
                case PLAYER1_MANAZONE:
                    GameBoard.P1_ManaZone.Remove(card_con);
                    mainForm.p1_Mana_frame.Controls.Remove(card_con);
                    break;
                case PLAYER2_MANAZONE:
                    GameBoard.P2_ManaZone.Remove(card_con);
                    mainForm.p2_Mana_frame.Controls.Remove(card_con);
                    break;
                case PLAYER1_TOMBZONE:
                    GameBoard.P1_TombZone.Remove(card_con);
                    break;
                case PLAYER2_TOMBZONE:
                    GameBoard.P2_TombZone.Remove(card_con);
                    break;
                case PLAYER1_PLAYERZONE:
                    judgment_Winner("PLAYER2");
                    break;
                case PLAYER2_PLAYERZONE:
                    judgment_Winner("PLAYER1");
                    break;
            }
            //=====[새로운 포지션 배치]=====
            card_con.position = position;
            switch (position)
            {
                case PLAYER1_CARDDECK:
                    GameBoard.P1_CardDeck.Add(card_con);
                    break;
                case PLAYER2_CARDDECK:
                    GameBoard.P2_CardDeck.Add(card_con);
                    break;
                case PLAYER1_HANDSZONE:
                    GameBoard.P1_HandsZone.Add(card_con);
                    mainForm.p1_hands_frame.Controls.Add(card_con);
                    card_con.activatable = true;
                    card_con.Enabled = true;
                    break;
                case PLAYER2_HANDSZONE:
                    GameBoard.P2_HandsZone.Add(card_con);
                    mainForm.p2_hands_frame.Controls.Add(card_con);
                    card_con.activatable = true;
                    card_con.Enabled = true;
                    break;
                case PLAYER1_WARZONE:
                    GameBoard.P1_WarZone.Add(card_con);
                    mainForm.p1_warZone_frame.Controls.Add(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case PLAYER2_WARZONE:
                    GameBoard.P2_WarZone.Add(card_con);
                    mainForm.p2_warZone_frame.Controls.Add(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case PLAYER1_MANAZONE:
                    GameBoard.P1_ManaZone.Add(card_con);
                    mainForm.p1_Mana_frame.Controls.Add(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case PLAYER2_MANAZONE:
                    GameBoard.P2_ManaZone.Add(card_con);
                    mainForm.p2_Mana_frame.Controls.Add(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;
                    break;
                case PLAYER1_TOMBZONE:
                    if (card_con.Equals(tombCard))
                    {
                        GameSkillManager.Instance.skill_8_p1 = false;
                    }
                    else
                    {
                        if (GameSkillManager.Instance.skill_8_p1)
                        {
                            GameBoard.P1_PlayerZone.card.Hp++;
                            GameBoard.P1_PlayerZone.lb_aphp.Text = GameBoard.P1_PlayerZone.card.Ap + " / " + GameBoard.P1_PlayerZone.card.Hp;
                        }
                        if (GameSkillManager.Instance.skill_8_p2)
                        {
                            GameBoard.P2_PlayerZone.card.Hp++;
                            GameBoard.P2_PlayerZone.lb_aphp.Text = GameBoard.P2_PlayerZone.card.Ap + " / " + GameBoard.P2_PlayerZone.card.Hp;
                        }
                    }

                    GameBoard.P1_TombZone.Add(card_con);
                    mainForm.p1_Tomb_frame.Controls.Add(card_con);
                    card_con.activatable = false;
                    card_con.Enabled = false;

                    cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
                case PLAYER2_TOMBZONE:
                    if (card_con.Equals(tombCard))
                    {
                        GameSkillManager.Instance.skill_8_p2 = false;
                    }
                    else
                    {
                        if (GameSkillManager.Instance.skill_8_p1)
                        {
                            GameBoard.P1_PlayerZone.card.Hp++;
                            GameBoard.P1_PlayerZone.lb_aphp.Text = GameBoard.P1_PlayerZone.card.Ap + " / " + GameBoard.P1_PlayerZone.card.Hp;
                        }
                        if (GameSkillManager.Instance.skill_8_p2)
                        {
                            GameBoard.P2_PlayerZone.card.Hp++;
                            GameBoard.P2_PlayerZone.lb_aphp.Text = GameBoard.P2_PlayerZone.card.Ap + " / " + GameBoard.P2_PlayerZone.card.Hp;
                        }
                    }

                    GameBoard.P2_TombZone.Add(card_con);
                    mainForm.p2_Tomb_frame.Controls.Add(card_con);
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
