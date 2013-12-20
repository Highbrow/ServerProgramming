using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.Network;

namespace WarLord_Server_GUI
{
    public partial class MainForm : Form
    {
        private ServerConnector _sc;    //서버 네트웍 관련 오브젝트
        delegate void invokeProc();     //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProctext(string text);  //쓰레드로부터 안전한 처리를 위한 invoke delegate

        public MainForm()
        {
            InitializeComponent();
            _sc = ServerConnector.Instance;
            _sc.ConnectServer(this);
            RefreshStatus();
            Thread t_serverStatus = new Thread(new ThreadStart(ThreadServerStatus));
            t_serverStatus.Start();
        }

        //=====[ 서버 상태 실시간 체크 쓰레드 ]=====
        void ThreadServerStatus()
        {
            while (true)
            {
                RefreshStatus();
                setTextClientCounter();
                Thread.Sleep(1000);
            }
        }
        //=====[클라이언트 수]=====
        private void setTextClientCounter()
        {
            if (lb_ClientCounter.InvokeRequired)
            {
                invokeProc call = new invokeProc(setTextClientCounter);
                this.Invoke(call, null);
            }
            else
            {
                lb_ClientCounter.Text = ServerConnector.OnlineConnections.Count().ToString();
            }
        }

        public void addClientMonitor(string data)
        {
            if (clientListBox.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(addClientMonitor);
                this.Invoke(call, data);
            }
            else
            {
                clientListBox.Items.Add(data);
            }
        }
        public void delClientMonitor(string data)
        {
            if (clientListBox.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(delClientMonitor);
                this.Invoke(call, data);
            }
            else
            {
                clientListBox.Items.Remove(data);
            }
            
        }
        


        //=========[ 서버 상태 갱신 ]=======
        public bool _isRunningServer = false;

        public void RefreshStatus()
        {
            if (serverStart_btn.InvokeRequired)
            {
                invokeProc call = new invokeProc(RefreshStatus);
                this.Invoke(call, null);
            }
            else
            {
                if (_isRunningServer)
                {
                    serverStart_btn.Enabled = false;    //시작버튼 disabled
                    serverStop_btn.Enabled = true;      //종료버튼 enabled
                }
                else
                {
                    serverStart_btn.Enabled = true;     //시작버튼 enabled
                    serverStop_btn.Enabled = false;     //종료버튼 disabled
                }
            }
        }

        /**********************************************
        *************[[ Button Event ]]****************
        ***********************************************/
        private void serverStart_btn_Click(object sender, EventArgs e)
        {
            _sc.StartServer();  //서버 시작
        }

        private void serverStop_btn_Click(object sender, EventArgs e)
        {
            _sc.StopServer();   //서버 종료
        }

        //=====[ Form 로드시 설정 이벤트 ]=====
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            int scrW = Screen.PrimaryScreen.WorkingArea.Width;
            int scrH = Screen.PrimaryScreen.WorkingArea.Height;
            int thisW = this.Width;
            int thisH = this.Height;
            int centerW = (scrW - thisW) / 2;
            int centerH = (scrH - thisH) / 2;
            this.DesktopLocation = new Point(centerW, centerH);
        }

        //=====[ 상단 종료 버튼 이벤트 ]=====
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

        public void LogOutPut(string text)
        {
            if (LogBox.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(LogOutPut);
                this.Invoke(call, text);
            }
            else
            {
                LogBox.AppendText(text + "\n");
            }
        }
    }
}
