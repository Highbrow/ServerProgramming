using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_B;

namespace TestConsoleClient
{
    public partial class zoneForm : Form
    {
        public zoneForm()
        {
            InitializeComponent();
        }

        public void selectZone(List<Card_Control> list)
        {
            foreach (Card_Control card_con in list)
            {
                zone_dialog.Controls.Add(card_con);
            }
        }

        private void zoneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GamePlayManager.Instance.mainForm.Enabled = true;
        }
    }
}
