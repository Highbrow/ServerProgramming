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

namespace DragonWarLord_preprototype
{
    public partial class MainForm : Form
    {
        public static MainForm mainForm = null;
        //========================
        GamePlayManager GM;
        GameBoard GB;

        public MainForm()
        {
            mainForm = this;
            GM = GamePlayManager.Instance;
            GB = GameBoard.Instance;
            InitializeComponent();
            NetworkManager.ws.Send("CreatedRoom_OK;");
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
        private void Turn_btn_Click(object sender, EventArgs e)
        {
            NetworkManager.ws.Send("EndTurn;");
            
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

        #region 플레이어의 캐릭터
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
        #endregion
        #region 플레이어의 핸즈존
        /// <summary>
        /// 플레이어1 핸즈존
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
        #endregion
        #region 플레이어의 배틀존
        /// <summary>
        /// 플레이어1 배틀존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_My_BattleZone(Card_Control card_con)
        {
            if (My_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_My_BattleZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_warZone_frame.Controls.Add(card_con);
            }
        }
        public void remove_My_BattleZone(Card_Control card_con)
        {
            if (My_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_My_BattleZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.My_warZone_frame.Controls.Remove(card_con);
            }
        }
        #endregion
        #region 플레이어의 무덤존
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
        #endregion
        #region 플레이어의 마나존
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
        #endregion

        #region 플레이어1 코스트 UI 컨트롤 Invoke

        #region 플레이어의 현재 남아있는 암흑 코스트
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
        public string getText_My_remain_dark()
        {
            return My_remain_dark.Text;
        }
        #endregion
        #region 플레이어의 현재 남아있는 불 코스트
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
        public string getText_My_remain_fire()
        {
            return My_remain_fire.Text;
        }
        #endregion
        #region 플레이어의 현재 사용한 혼합 코스트
        /// <summary>
        /// 사용한 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_use_all(string str)
        {
            if (My_use_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_use_all);
                this.Invoke(call, str);
            }
            else
            {
                this.My_use_all.Text = str;
            }
        }
        public string getText_My_use_all()
        {
            return My_use_all.Text;
        }
        #endregion
        #region 플레이어의 현재 남아있는 전체 코스트
        /// <summary>
        /// 남은 전체 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_My_remain_all(string str)
        {
            if (My_remain_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_My_remain_all);
                this.Invoke(call, str);
            }
            else
            {
                this.My_remain_all.Text = str;
            }
        }
        public string getText_My_remain_all()
        {
            return My_remain_all.Text;
        }
        #endregion
        #region 플레이어의 전체 암흑 코스트
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
        public string getText_My_cnt_dark()
        {
            return My_cnt_dark.Text;
        }
        #endregion
        #region 플레이어의 전체 불 코스트
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
        public string getText_My_cnt_fire()
        {
            return My_cnt_fire.Text;
        }
        #endregion
        
        #endregion

        #region 플레이어2 존 UI 컨트롤 Invoke


        #region 상대방의 캐릭터
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
        #endregion
        #region 상대방의 핸즈존
        /// <summary>
        /// 플레이어2 핸즈존
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
        #endregion
        #region 상대방의 배틀존
        /// <summary>
        /// 플레이어2 배틀존
        /// </summary>
        /// <param name="card_con"></param>
        public void add_Opponent_BattleZone(Card_Control card_con)
        {
            if (Opponent_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(add_Opponent_BattleZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_warZone_frame.Controls.Add(card_con);
            }
        }
        public void remove_Opponent_BattleZone(Card_Control card_con)
        {
            if (Opponent_warZone_frame.InvokeRequired)
            {
                invokeProcCardControl call = new invokeProcCardControl(remove_Opponent_BattleZone);
                this.Invoke(call, card_con);
            }
            else
            {
                this.Opponent_warZone_frame.Controls.Remove(card_con);
            }
        }
        #endregion
        #region 상대방의 무덤존
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
        #endregion
        #region 상대방의 마나존
        /// <summary>
        /// 플레이어2 마나존
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

        #endregion

        #region 플레이어2 코스트 UI 컨트롤 Invoke
        
        #region 상대방의 현재 남아있는 암흑 코스트
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
        public string getText_Opponent_remain_dark()
        {
            return Opponent_remain_dark.Text;
        }
        #endregion
        #region 상대방의 현재 남아있는 불 코스트
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
        public string getText_Opponent_remain_fire()
        {
            return Opponent_remain_fire.Text;
        }
        #endregion
        #region 상대방의 현재 사용한 혼합 코스트
        /// <summary>
        /// 사용한 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_use_all(string str)
        {
            if (Opponent_use_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_use_all);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_use_all.Text = str;
            }
        }
        public string getText_Opponent_use_all()
        {
            return Opponent_use_all.Text;
        }
        #endregion
        #region 상대방의 현재 남아있는 전체 코스트
        /// <summary>
        /// 남은 아무 코스트
        /// </summary>
        /// <param name="str"></param>
        public void setText_Opponent_remain_all(string str)
        {
            if (Opponent_remain_all.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_Opponent_remain_all);
                this.Invoke(call, str);
            }
            else
            {
                this.Opponent_remain_all.Text = str;
            }
        }
        public string getText_Opponent_remain_all()
        {
            return Opponent_remain_all.Text;
        }
        #endregion
        #region 상대방의 전체 암흑 코스트
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
        public string getText_Opponent_cnt_darK()
        {
            return Opponent_cnt_dark.Text;
        }
        #endregion
        #region 상대방의 전체 불 코스트
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
        public string getText_Opponent_cnt_fire()
        {
            return Opponent_cnt_fire.Text;
        }
        #endregion
        
        #endregion

        #endregion



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
