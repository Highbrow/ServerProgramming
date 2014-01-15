using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;
using WebSocketSharp;

namespace DragonWarLord_preprototype
{
    public partial class MainForm : Form
    {
        GameBoard GB;
        CardDealer cardDealer;
        public static MainForm mf = null;
        public MainForm()
        {
            mf = this;
            GB = GameBoard.Instance;
            cardDealer = CardDealer.Instance;
            cardDealer.mainForm = this;
            InitializeComponent();
            NetworkManager.ws.Send("CreatedRoom_OK;");
        }

        #region GUI 컨트롤을 위한 Invoke 집합
        delegate void invokeProc();     //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProcCardControl(Card_Control card_con);     //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProctext(string text);  //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProcbtn(bool p);//쓰레드로부터 안전한 처리를 위한 invoke delegate

        public void EnabledButton(bool p)
        {
            if (Turn_btn.InvokeRequired)
            {
                invokeProcbtn call = new invokeProcbtn(EnabledButton);
                this.Invoke(call, p);
            }
            else
            {
                Turn_btn.Enabled = p;
            }
        }

        #region 플레이어1 존 UI 컨트롤 Invoke
        /// <summary>
        /// 플레이어1 존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_My_Player(Card_Control card_con)
        {
            if (My_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_My_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_Player.Controls.Add(card_con);
            }
        }
        public void remove_My_Player(Card_Control card_con)
        {
            if (My_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_My_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_Player.Controls.Remove(card_con);
            }
        }

        /// <summary>
        /// 플레이어1 핸드존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_My_Hands(Card_Control card_con)
        {
            if (My_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_My_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_hands_frame.Controls.Add(card_con);
            }
        }
        public void remove_My_Hands(Card_Control card_con)
        {
            if (My_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_My_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_hands_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 전투존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_My_WarZone(Card_Control card_con)
        {
            if (My_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_My_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_warZone_frame.Controls.Add(card_con);
            }
        }
        public void remove_My_WarZone(Card_Control card_con)
        {
            if (My_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_My_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_warZone_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 무덤존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_My_TombZone(Card_Control card_con)
        {
            if (My_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_My_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_Tomb_frame.Controls.Add(card_con);
            }
        }
        public void remove_My_TombZone(Card_Control card_con)
        {
            if (My_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_My_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_Tomb_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 마나존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_My_ManaZone(Card_Control card_con)
        {
            if (My_Mana_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_My_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_Mana_frame.Controls.Add(card_con);
            }
        }
        public void remove_My_ManaZone(Card_Control card_con)
        {
            if (My_Mana_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_My_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_Mana_frame.Controls.Remove(card_con);
            }
        }
        #endregion

        #region 플레이어1 코스트 UI 컨트롤 Invoke
        /// <summary>
        /// 남은 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_remain_dark(string str)
        {
            if (My_remain_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_remain_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.My_remain_dark.Text = str;
            }
        }
        /// <summary>
        /// 남은 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_remain_fire(string str)
        {
            if (My_remain_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_remain_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.My_remain_fire.Text = str;
            }
        }
        /// <summary>
        /// 사용한 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_remain_all(string str)
        {
            if (My_use_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_remain_all);
                this.Invoke(call, str);
            }
            else
            {
                this.My_use_all.Text = str;
            }
        }

        /// <summary>
        /// 보유 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_cnt_dark(string str)
        {
            if (My_cnt_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_cnt_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.My_cnt_dark.Text = str;
            }
        }

        /// <summary>
        /// 보유 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_cnt_fire(string str)
        {
            if (My_cnt_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_cnt_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.My_cnt_fire.Text = str;
            }
        }

        #endregion

        #region 플레이어2 존 UI 컨트롤 Invoke
        /// <summary>
        /// 플레이어2 존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_Opponent_Player(Card_Control card_con)
        {
            if (Opponent_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_Opponent_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_Player.Controls.Add(card_con);
            }
        }
        public void remove_Opponent_Player(Card_Control card_con)
        {
            if (Opponent_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_Opponent_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_Player.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어2 핸드존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_Opponent_Hands(Card_Control card_con)
        {
            if (Opponent_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_Opponent_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_hands_frame.Controls.Add(card_con);
            }
        }
        public void remove_Opponent_Hands(Card_Control card_con)
        {
            if (Opponent_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_Opponent_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_hands_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어2 전투존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_Opponent_WarZone(Card_Control card_con)
        {
            if (Opponent_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_Opponent_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_warZone_frame.Controls.Add(card_con);
            }
        }
        public void remove_Opponent_WarZone(Card_Control card_con)
        {
            if (Opponent_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_Opponent_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_warZone_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어2 무덤존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_Opponent_TombZone(Card_Control card_con)
        {
            if (Opponent_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_Opponent_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_Tomb_frame.Controls.Add(card_con);
            }
        }
        public void remove_Opponent_TombZone(Card_Control card_con)
        {
            if (Opponent_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_Opponent_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_Tomb_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 마나존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_Opponent_ManaZone(Card_Control card_con)
        {
            if (Opponent_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_Opponent_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_Mana_frame.Controls.Add(card_con);
            }
        }
        public void remove_Opponent_ManaZone(Card_Control card_con)
        {
            if (Opponent_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_Opponent_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_Mana_frame.Controls.Remove(card_con);
            }
        }

        #endregion

        #region 플레이어2 코스트 UI 컨트롤 Invoke
        /// <summary>
        /// 남은 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_remain_dark(string str)
        {
            if (Opponent_remain_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_remain_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_remain_dark.Text = str;
            }
        }
        /// <summary>
        /// 남은 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_remain_fire(string str)
        {
            if (Opponent_remain_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_remain_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_remain_fire.Text = str;
            }
        }
        /// <summary>
        /// 사용한 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_remain_all(string str)
        {
            if (Opponent_use_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_remain_all);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_use_all.Text = str;
            }
        }

        /// <summary>
        /// 보유 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_cnt_dark(string str)
        {
            if (Opponent_cnt_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_cnt_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_cnt_dark.Text = str;
            }
        }

        /// <summary>
        /// 보유 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_cnt_fire(string str)
        {
            if (Opponent_cnt_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_cnt_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_cnt_fire.Text = str;
            }
        }

        #endregion

        #endregion


        private void Turn_btn_Click(object sender, EventArgs e)
        {
            NetworkManager.ws.Send("EndTurn;");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Abort your Server?", "Notice", MessageBoxButtons.YesNo);

            // MessageBox 의 [예] 버튼 클릭시 발생하는 이벤트
            if (result == DialogResult.Yes)
            {
                //t1.Interrupt();
                //runnable_refresh = false;
                if (NetworkManager.ws.IsAlive)
                {
                    NetworkManager.ws.Close();
                }
                Application.ExitThread();
                Environment.Exit(0);
            }
            // MessageBox 의 [아니오] 버튼이 클릭되었을 경우 - 이벤트 취소
            e.Cancel = (result == DialogResult.No);
        }

        
    }
}
