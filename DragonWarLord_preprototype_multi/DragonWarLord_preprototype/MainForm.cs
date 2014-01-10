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
using WarLord_Server_GUI.GameLogic_B;
using WebSocketSharp;

namespace DragonWarLord_preprototype
{
    public partial class MainForm : Form
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            Application.Run(new MainForm());
        }
        GameBoard GB;
        public MainForm()
        {
            GamePlayManager.Instance.mainForm = this;
            GB = GameBoard.Instance;
            
            InitializeComponent();
            NetworkManager.Instance.start();
        }

        #region GUI 컨트롤을 위한 Invoke 집합
        delegate void invokeProc();     //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProcCardControl(Card_Control card_con);     //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProctext(string text);  //쓰레드로부터 안전한 처리를 위한 invoke delegate

        #region 플레이어1 존 UI 컨트롤 Invoke
        /// <summary>
        /// 플레이어1 존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P1_Player(Card_Control card_con)
        {
            if (p1_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P1_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_Player.Controls.Add(card_con);
            }
        }
        public void remove_P1_Player(Card_Control card_con)
        {
            if (p1_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P1_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_Player.Controls.Remove(card_con);
            }
        }

        /// <summary>
        /// 플레이어1 핸드존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P1_Hands(Card_Control card_con)
        {
            if (p1_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P1_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_hands_frame.Controls.Add(card_con);
            }
        }
        public void remove_P1_Hands(Card_Control card_con)
        {
            if (p1_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P1_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_hands_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 전투존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P1_WarZone(Card_Control card_con)
        {
            if (p1_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P1_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_warZone_frame.Controls.Add(card_con);
            }
        }
        public void remove_P1_WarZone(Card_Control card_con)
        {
            if (p1_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P1_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_warZone_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 무덤존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P1_TombZone(Card_Control card_con)
        {
            if (p1_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P1_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_Tomb_frame.Controls.Add(card_con);
            }
        }
        public void remove_P1_TombZone(Card_Control card_con)
        {
            if (p1_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P1_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_Tomb_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 마나존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P1_ManaZone(Card_Control card_con)
        {
            if (p1_Mana_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P1_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_Mana_frame.Controls.Add(card_con);
            }
        }
        public void remove_P1_ManaZone(Card_Control card_con)
        {
            if (p1_Mana_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P1_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p1_Mana_frame.Controls.Remove(card_con);
            }
        }
        #endregion

        #region 플레이어1 코스트 UI 컨트롤 Invoke
        /// <summary>
        /// 남은 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p1_remain_dark(string str)
        {
            if (p1_remain_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p1_remain_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.p1_remain_dark.Text = str;
            }
        }
        /// <summary>
        /// 남은 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p1_remain_fire(string str)
        {
            if (p1_remain_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p1_remain_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.p1_remain_fire.Text = str;
            }
        }
        /// <summary>
        /// 사용한 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p1_use_all(string str)
        {
            if (p1_use_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p1_use_all);
                this.Invoke(call, str);
            }
            else
            {
                this.p1_use_all.Text = str;
            }
        }

        /// <summary>
        /// 보유 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p1_cnt_dark(string str)
        {
            if (p1_cnt_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p1_cnt_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.p1_cnt_dark.Text = str;
            }
        }

        /// <summary>
        /// 보유 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p1_cnt_fire(string str)
        {
            if (p1_cnt_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p1_cnt_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.p1_cnt_fire.Text = str;
            }
        }

        #endregion

        #region 플레이어2 존 UI 컨트롤 Invoke
        /// <summary>
        /// 플레이어2 존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P2_Player(Card_Control card_con)
        {
            if (p2_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P2_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_Player.Controls.Add(card_con);
            }
        }
        public void remove_P2_Player(Card_Control card_con)
        {
            if (p2_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P2_Player);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_Player.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어2 핸드존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P2_Hands(Card_Control card_con)
        {
            if (p2_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P2_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_hands_frame.Controls.Add(card_con);
            }
        }
        public void remove_P2_Hands(Card_Control card_con)
        {
            if (p2_hands_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P2_Hands);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_hands_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어2 전투존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P2_WarZone(Card_Control card_con)
        {
            if (p2_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P2_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_warZone_frame.Controls.Add(card_con);
            }
        }
        public void remove_P2_WarZone(Card_Control card_con)
        {
            if (p2_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P2_WarZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_warZone_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어2 무덤존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P2_TombZone(Card_Control card_con)
        {
            if (p2_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P2_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_Tomb_frame.Controls.Add(card_con);
            }
        }
        public void remove_P2_TombZone(Card_Control card_con)
        {
            if (p2_Tomb_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P2_TombZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_Tomb_frame.Controls.Remove(card_con);
            }
        }
        /// <summary>
        /// 플레이어1 마나존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_P2_ManaZone(Card_Control card_con)
        {
            if (p2_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_P2_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_Mana_frame.Controls.Add(card_con);
            }
        }
        public void remove_P2_ManaZone(Card_Control card_con)
        {
            if (p2_Player.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_P2_ManaZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.p2_Mana_frame.Controls.Remove(card_con);
            }
        }

        #endregion

        #region 플레이어2 코스트 UI 컨트롤 Invoke
        /// <summary>
        /// 남은 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p2_remain_dark(string str)
        {
            if (p2_remain_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p2_remain_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.p2_remain_dark.Text = str;
            }
        }
        /// <summary>
        /// 남은 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p2_remain_fire(string str)
        {
            if (p2_remain_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p2_remain_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.p2_remain_fire.Text = str;
            }
        }
        /// <summary>
        /// 사용한 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p2_use_all(string str)
        {
            if (p2_use_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p2_use_all);
                this.Invoke(call, str);
            }
            else
            {
                this.p2_use_all.Text = str;
            }
        }

        /// <summary>
        /// 보유 암흑 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p2_cnt_dark(string str)
        {
            if (p2_cnt_dark.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p2_cnt_dark);
                this.Invoke(call, str);
            }
            else
            {
                this.p2_cnt_dark.Text = str;
            }
        }

        /// <summary>
        /// 보유 불 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_p2_cnt_fire(string str)
        {
            if (p2_cnt_fire.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_p2_cnt_fire);
                this.Invoke(call, str);
            }
            else
            {
                this.p2_cnt_fire.Text = str;
            }
        }

        #endregion

        #endregion


        private void Turn_btn_Click(object sender, EventArgs e)
        {
            GamePlayManager.Instance.EndOfTurn();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int scrW = Screen.PrimaryScreen.WorkingArea.Width;
            int scrH = Screen.PrimaryScreen.WorkingArea.Height;
            int thisW = this.Width;
            int thisH = this.Height;
            int centerW = (scrW - thisW) / 2;
            int centerH = (scrH - thisH) / 2;
            this.DesktopLocation = new Point(centerW, centerH);
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
