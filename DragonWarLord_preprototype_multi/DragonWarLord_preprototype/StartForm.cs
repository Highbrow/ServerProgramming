using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (ready_btn.InvokeRequired)
            {
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

        public Point ptRect = new Point(0, 0);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ptRect.X = e.X;
            ptRect.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pt = new Point(this.Location.X + e.X - ptRect.X, this.Location.Y + e.Y - ptRect.Y);
                this.Location = pt;
            }
        }

    }
}
