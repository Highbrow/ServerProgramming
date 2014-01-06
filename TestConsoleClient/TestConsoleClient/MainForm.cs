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

namespace TestConsoleClient
{
    public partial class MainForm : Form
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new MainForm());
        }

        //========================
        GamePlayManager GM;
        GameBoard GB;

        public MainForm()
        {
            GM = GamePlayManager.Instance;
            GM.mainForm = this;
            GB = GameBoard.Instance;
            InitializeComponent();
            GM.makeMainPlayer();   //메인 캐릭터 생성
            GM.inputCard();     //데이터 베이스의 카드 호출
            GM.firstDistribute();   //첫 카드 지급
            GM.distribute();    //턴마다 카드 배급 관련
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
            
            GameDefaultSetting();   //게임 기본 셋팅
        }

        private void GameDefaultSetting()
        {
            foreach (Card_Control card in GameBoard.P1_HandsZone)
            {
                p1_hands_frame.Controls.Add(card);
            }
            foreach (var card in GameBoard.P2_HandsZone)
            {
                p2_hands_frame.Controls.Add(card);
            }
            GM.InitFlag();
            GM.turnFlag();
        }

        private void Turn_btn_Click(object sender, EventArgs e)
        {
            GM.EndOfTurn();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Abort your Server?", "Notice", MessageBoxButtons.YesNo);

            // MessageBox 의 [예] 버튼 클릭시 발생하는 이벤트
            if (result == DialogResult.Yes)
            {
                //t1.Interrupt();
                //runnable_refresh = false;
                Application.ExitThread();
                Environment.Exit(0);
            }
            // MessageBox 의 [아니오] 버튼이 클릭되었을 경우 - 이벤트 취소
            e.Cancel = (result == DialogResult.No);
        }

    }
}
