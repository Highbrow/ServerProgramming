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
        
        /// <summary>
        /// 카드 이동 < 중요!! >
        /// </summary>
        /// <param name="p"></param>
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
                    card.position = GameBoard.MY_CARDDECK;
                    break;
                case GameBoard.OPPONENT_CARDDECK:
                    GameBoard.Opponent_CardDeck.Add(card);
                    card.position = GameBoard.OPPONENT_CARDDECK;
                    break;
                case GameBoard.MY_HANDSZONE:
                    GameBoard.My_HandsZone.Add(card);
                    card.position = GameBoard.MY_HANDSZONE;
                    mainForm.add_My_Hands(card);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Add(card);
                    card.position = GameBoard.OPPONENT_HANDSZONE;
                    mainForm.add_Opponent_Hands(card);
                    break;
                case GameBoard.MY_WARZONE:
                    GameBoard.My_WarZone.Add(card);
                    card.position = GameBoard.MY_WARZONE;
                    mainForm.add_My_WarZone(card);
                    card.activatable = false;
                    card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimgused;
                    break;
                case GameBoard.OPPONENT_WARZONE:
                    GameBoard.Opponent_WarZone.Add(card);
                    card.position = GameBoard.OPPONENT_WARZONE;
                    card.activatable = false;
                    card.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimgused;
                    mainForm.add_Opponent_WarZone(card);
                    break;
                case GameBoard.MY_MANAZONE:
                    GameBoard.My_ManaZone.Add(card);
                    card.position = GameBoard.MY_MANAZONE;
                    mainForm.add_My_ManaZone(card);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Add(card);
                    card.position = GameBoard.OPPONENT_MANAZONE;
                    mainForm.add_Opponent_ManaZone(card);
                    break;
                case GameBoard.MY_TOMBZONE:
                    GameBoard.My_TombZone.Add(card);
                    card.position = GameBoard.MY_TOMBZONE;
                    mainForm.add_My_TombZone(card);
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    GameBoard.Opponent_TombZone.Add(card);
                    card.position = GameBoard.OPPONENT_TOMBZONE;
                    mainForm.add_Opponent_TombZone(card);
                    break;
            }
        }
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
            else if (GameBoard.My_WarZone.Contains(card_con))
            {
                pre_position = GameBoard.MY_WARZONE;
            }
            else if (GameBoard.Opponent_WarZone.Contains(card_con))
            {
                pre_position = GameBoard.OPPONENT_WARZONE;
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
                    mainForm.remove_My_Hands(card_con);
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Remove(card_con);
                    mainForm.remove_Opponent_Hands(card_con);
                    break;
                case GameBoard.MY_WARZONE:
                    GameBoard.My_WarZone.Remove(card_con);
                    mainForm.remove_My_WarZone(card_con);
                    break;
                case GameBoard.OPPONENT_WARZONE:
                    GameBoard.Opponent_WarZone.Remove(card_con);
                    mainForm.remove_Opponent_WarZone(card_con);
                    break;
                case GameBoard.MY_MANAZONE:
                    GameBoard.My_ManaZone.Remove(card_con);
                    mainForm.remove_My_ManaZone(card_con);
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Remove(card_con);
                    mainForm.remove_Opponent_ManaZone(card_con);
                    break;
                case GameBoard.MY_TOMBZONE:
                    GameBoard.My_TombZone.Remove(card_con);
                    break;
                case GameBoard.OPPONENT_TOMBZONE:
                    GameBoard.Opponent_TombZone.Remove(card_con);
                    break;
                case GameBoard.MY_PLAYERZONE:
                    break;
                case GameBoard.OPPONENT_PLAYERZONE:
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
                    mainForm.add_My_Hands(card_con);
                    card_con.activatable = true;
                    break;
                case GameBoard.OPPONENT_HANDSZONE:
                    GameBoard.Opponent_HandsZone.Add(card_con);
                    mainForm.add_Opponent_Hands(card_con);
                    card_con.activatable = true;
                    break;
                case GameBoard.MY_WARZONE:
                    GameBoard.My_WarZone.Add(card_con);
                    mainForm.add_My_WarZone(card_con);
                    card_con.activatable = false;
                    break;
                case GameBoard.OPPONENT_WARZONE:
                    GameBoard.Opponent_WarZone.Add(card_con);
                    mainForm.add_Opponent_WarZone(card_con);
                    card_con.activatable = false;
                    break;
                case GameBoard.MY_MANAZONE:
                    GameBoard.My_ManaZone.Add(card_con);
                    mainForm.add_My_ManaZone(card_con);
                    card_con.activatable = false;
                    break;
                case GameBoard.OPPONENT_MANAZONE:
                    GameBoard.Opponent_ManaZone.Add(card_con);
                    mainForm.add_Opponent_ManaZone(card_con);
                    card_con.activatable = false;
                    break;
                case GameBoard.MY_TOMBZONE:
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
                    //cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
                case GameBoard.OPPONENT_TOMBZONE:
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

                    //cardDestruction(card_con);//=====[카드 파괴 이벤트 호출]=====

                    break;
            }
        }

        public bool canMakeResource = true; //마나 생성 가능 유무
        public void MakeResource(Card_Control card_con)
        {
            if (this.canMakeResource)   //마나생성이 가능할 경우
            {
                NetworkManager.ws.Send("MakeResource;" + card_con.position + ";"
                + GameBoard.My_HandsZone.IndexOf(card_con)+";" + GameBoard.MY_MANAZONE+";");

                CardDealer.Instance.canMakeResource = false;   //한턴에 한번만 가능하도록
            }
            else
            {
                MessageBox.Show("이번 턴에는 마나를 더 이상 생성 할 수 없습니다.");
            }
        }

        #region 카드 사용 관련
        /// <summary>
        /// 카드 내기
        /// </summary>
        /// <param name="card_Control"></param>
        internal void useCard(Card_Control card_Control)
        {
            card_Control.activatable = false;
            card_Control.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.backimgused;

            NetworkManager.ws.Send("MoveCard;" + card_Control.position + ";"
                + GameBoard.My_HandsZone.IndexOf(card_Control)+";" + GameBoard.MY_WARZONE+";");
        }

        /// <summary>
        /// 카드 사용 가능 여부 판단
        /// </summary>
        /// <param name="card_Control"></param>
        internal bool canUseCard(Card_Control card_Control)
        {
            if (card_Control.activatable)
            {
                string[] consump = card_Control.card.Consumption.Split(';');
                int need_dark = Convert.ToInt32(consump[1]);    //필요 암흑 마나
                int need_fire = Convert.ToInt32(consump[0]);    //필요 불 마나
                int need_all = Convert.ToInt32(consump[2]);     //필요 아무 마나

                if (GameBoard.Instance.My_RemainResource_dark >= need_dark && GameBoard.Instance.My_RemainResource_fire >= need_fire
                    && (((GameBoard.Instance.My_RemainResource_dark - need_dark) + (GameBoard.Instance.My_RemainResource_fire - need_fire)) - GameBoard.Instance.My_UsedResource) >= need_all)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("이 카드를 사용하기 위한 자원이 부족합니다.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("이미 사용한 카드입니다.");
                return false;
            }
            
        }

        #endregion 카드 사용 관련


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
                    canMatchCheck(selectCard, targetCard);  //매치 가능여부 체크 후 매치
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
            if (selectCard.activatable) //활동력이 있어야 가능
            {
                if (TurnManager.Turn)
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

            if (TurnManager.Turn)
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

            if (TurnManager.Turn)
            {
                if (selectCard.card.thisTurnHP <= 0)
                {
                    moveZone(selectCard, GameBoard.MY_TOMBZONE);
                }
                if (targetCard.card.thisTurnHP <= 0)
                {
                    moveZone(targetCard, GameBoard.OPPONENT_TOMBZONE);
                }
            }
            else
            {
                if (selectCard.card.thisTurnHP <= 0)
                {
                    moveZone(selectCard, GameBoard.OPPONENT_TOMBZONE);
                }
                if (targetCard.card.thisTurnHP <= 0)
                {
                    moveZone(targetCard, GameBoard.MY_TOMBZONE);
                }
            }
            //selectCard.activatable = false;
        }
        //=====[게임 스킬 관련]=====
        public void gameSkill(String skill, Card_Control card_con)
        {
            Delegate dg;
            GameSkillManager.Instance.SkillDictionary.TryGetValue(skill, out dg);
            dg.DynamicInvoke(card_con);
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
