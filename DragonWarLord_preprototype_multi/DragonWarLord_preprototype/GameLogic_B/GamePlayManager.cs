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
    class CardDealer
    {
        public delegate void dchangeStatus(Card_Control card_con);

        public event dchangeStatus cardPop;
        public event dchangeStatus endturn;
        public event dchangeStatus cardDestruction;

        private CardDealer()
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
        public void EndOfTurn()
        {
            InitFlag(); //flag 초기화
            turnFlag();

            endturn(null);  //이벤트 발생
        }

        public void InitFlag()
        {
            //카드 행동력 초기화 추가해야함
            //카드 공격력과 생명력 초기화
            //card.Card_refresh();
        }

        public void turnFlag()
        {
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
            my_remain_dark = Convert.ToInt32(mainForm.My_cnt_dark.Text);   //현재 암흑 마나
            my_remain_fire = Convert.ToInt32(mainForm.My_cnt_fire.Text);   //현재 불 마나
            opponent_remain_dark = Convert.ToInt32(mainForm.Opponent_cnt_dark.Text);   //현재 암흑 마나
            opponent_remain_fire = Convert.ToInt32(mainForm.Opponent_cnt_fire.Text);   //현재 불 마나
            //refreshMana(); 마나 새로고침 (표현)
        }

        #region 카드 내는 행동

        int all_count = 0;
        public int my_remain_dark = 0;
        public int my_remain_fire = 0;
        public int opponent_remain_dark = 0;
        public int opponent_remain_fire = 0;

        internal void procMana(Card_Control card_con)
        {
            string[] consump = card_con.card.Consumption.Split(';');
            int need_dark = Convert.ToInt32(consump[1]);    //필요 암흑 마나
            int need_fire = Convert.ToInt32(consump[0]);    //필요 불 마나
            int need_all = Convert.ToInt32(consump[2]);     //필요 아무 마나

            if (thisturn)
            {
                //----[마나 연산]----
                my_remain_dark -= need_dark;
                my_remain_fire -= need_fire;
                all_count += need_all;
            }
            else
            {
                //----[마나 연산]----
                opponent_remain_dark -= need_dark;
                opponent_remain_fire -= need_fire;
                all_count += need_all;
            }
        }

        internal void popCard(Card_Control card_con)
        {
            //턴확인
            //카드 type 확인
            //자원 처리
            //카드 이동
            cardPop(card_con);  //카드내기 이벤트발생
        }

        #endregion 카드 내는 행동
    }
}
