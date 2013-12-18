using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.Network;

namespace WarLord_Server_GUI
{
    public partial class MainForm : Form
    {

        private ServerConnector _sc;

        public MainForm()
        {
            InitializeComponent();
            _sc = new ServerConnector(this);
            RefreshStatus();
        }

        //=========[ 서버 상태 갱신 ]=======

        public bool _isRunningServer = false;
        public void RefreshStatus()
        {
            if (_isRunningServer)
            {
                serverStart_btn.Enabled = false;    //시작버튼 disabled
                serverStop_btn.Enabled = true;      //종료버튼 enabled
                //serverCheck_btn.Enabled = true;
            }
            else
            {
                serverStart_btn.Enabled = true;     //시작버튼 enabled
                serverStop_btn.Enabled = false;     //종료버튼 disabled
                //serverCheck_btn.Enabled = false;
            }
        }



        /**********************************************
        *************[[ Button Event ]]****************
        ***********************************************/
        private void serverStart_btn_Click(object sender, EventArgs e)
        {
            _sc.StartServer();  //서버 시작
            RefreshStatus();    //상태 갱신
        }

        private void serverStop_btn_Click(object sender, EventArgs e)
        {
            _sc.StopServer();   //서버 종료
            RefreshStatus();    //상태 갱신
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Abort your Server?", "Notice", MessageBoxButtons.YesNo);

            // MessageBox 의 [예] 버튼 클릭시 발생하는 이벤트
            if (result == DialogResult.Yes)
            {
                if (_isRunningServer)
                {
                    _sc.StopServer();
                }
                Application.ExitThread();
                Environment.Exit(0);
            }
            // MessageBox 의 [아니오] 버튼이 클릭되었을 경우 - 이벤트 취소
            e.Cancel = (result == DialogResult.No);
        }


        //==========[ Log 출력 ]============
        delegate void DLogOutPut(string text);

        public void LogOutPut(string text)
        {
            if (LogBox.InvokeRequired)
            {
                DLogOutPut call = new DLogOutPut(LogOutPut);
                this.Invoke(call, text);
            }
            else
            {
                LogBox.AppendText(text + "\n");
            }
            
        }
        
        //=========[ 나중에 실시간 작업 할 내용, 임의의 체크 버튼 ]=======
        private void checkServer_btn_Click(object sender, EventArgs e)
        {
            lb_ClientCounter.Text = ServerConnector.OnlineConnections.Count().ToString();
        }
    }
}
