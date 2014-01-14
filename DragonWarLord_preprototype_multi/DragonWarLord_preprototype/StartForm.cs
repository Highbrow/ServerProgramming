using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace DragonWarLord_preprototype
{
    public partial class StartForm : Form
    {
        NetworkManager network;
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            NetworkManager.Instance.startForm = this;
            network = NetworkManager.Instance;
        }

        private void ready_btn_Click(object sender, EventArgs e)
        {
            setEnabledButton(false);
            setText_lb_status("Connecting to Server..");
            NetworkManager.ws.Connect();    //서버 연결
        }

        delegate void invokeProctext(string text);  //쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProcbool(bool tf);  //쓰레드로부터 안전한 처리를 위한 invoke delegate

        public void setText_lb_status(string text)
        {
            if (lb_status.InvokeRequired)
            {
                invokeProctext call = new invokeProctext(setText_lb_status);
                this.Invoke(call, text);
            }
            else
            {
                this.lb_status.Text = text;
            }
        }

        public void setEnabledButton(bool tf)
        {
            if(ready_btn.InvokeRequired){
                invokeProcbool call = new invokeProcbool(setEnabledButton);
                    this.Invoke(call, tf);
            }
            else
            {
                this.ready_btn.Enabled = tf;
            }
        }

        /// <summary>
        /// Main 함수
        /// </summary>
        /// <param name="args"></param>
        static public StartForm startForm;
        [STAThread]
        static void Main(string[] args)
        {
            startForm = new StartForm();
            DialogResult dialog = startForm.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                startForm.Close();
                Application.Run(new MainForm());
            }
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            if (NetworkManager.ws.IsAlive)
            {
                NetworkManager.ws.Close();
            }
            Application.ExitThread();
            Environment.Exit(0);
        }

    }
}
