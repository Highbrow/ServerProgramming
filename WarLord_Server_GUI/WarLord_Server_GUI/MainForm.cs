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
        ServerConnector sc;
        public MainForm()
        {
            InitializeComponent();
            sc = new ServerConnector();
        }

        private void serverStart_btn_Click(object sender, EventArgs e)
        {
            if (sc.StartServer() == 1)
            {
                MessageBox.Show("서버를 시작합니다.");
            }
        }

        private void serverStop_btn_Click(object sender, EventArgs e)
        {
            if (sc.StopServer() == 1)
            {
                MessageBox.Show("서버를 중지합니다.");
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Abort your Server?", "Notice", MessageBoxButtons.YesNo);

            // MessageBox 의 [예] 버튼 클릭시 발생하는 이벤트
            if (result == DialogResult.Yes)
            {
                e.Cancel = true;
            }
            // MessageBox 의 [아니오] 버튼이 클릭되었을 경우 - 이벤트 취소
            e.Cancel = (result == DialogResult.No);
        }
    }
}
