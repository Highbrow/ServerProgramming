using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLord_Server_GUI.GameLogic_A;

namespace TestConsoleClient
{
    public partial class MainForm : Form
    {
        static void Main(string[] args)
        {
            Application.Run(new MainForm());
        }

        //========================
        process game;
        public MainForm()
        {
            game = new process();
            InitializeComponent();
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
            GameBasicSetting();
        }

        private void GameBasicSetting()
        {

            Player1_Hands_zone.BeginUpdate();
            Player2_Hands_zone.BeginUpdate();
            Player1_warZone_listview.BeginUpdate();

            // 뷰모드 지정
            Player1_Hands_zone.View = View.Details;
            Player2_Hands_zone.View = View.Details;
            Player1_warZone_listview.View = View.Details;

            foreach (var card in game.gb.P1_HandsZone)
            {
                ListViewItem lvi = new ListViewItem(card.Name);
                lvi.SubItems.Add(card.Type);
                lvi.SubItems.Add(card.Attribute);
                lvi.SubItems.Add(card.Consumption);
                lvi.SubItems.Add(card.Ap.ToString());
                lvi.SubItems.Add(card.Hp.ToString());
                lvi.SubItems.Add(card.Information);
                lvi.ImageIndex = 0;
                // ListViewItem객체를 Items 속성에 추가
                Player1_Hands_zone.Items.Add(lvi);
            }
            foreach (var card in game.gb.P2_HandsZone)
            {
                ListViewItem lvi = new ListViewItem(card.Name);
                lvi.SubItems.Add(card.Type);
                lvi.SubItems.Add(card.Attribute);
                lvi.SubItems.Add(card.Consumption);
                lvi.SubItems.Add(card.Ap.ToString());
                lvi.SubItems.Add(card.Hp.ToString());
                lvi.SubItems.Add(card.Information);
                lvi.ImageIndex = 0;
                // ListViewItem객체를 Items 속성에 추가
                Player2_Hands_zone.Items.Add(lvi);
            }

            // 컬럼명과 컬럼사이즈 지정
            Player1_Hands_zone.Columns.Add("이름", 150, HorizontalAlignment.Left);
            Player1_Hands_zone.Columns.Add("종류", 50, HorizontalAlignment.Left);
            Player1_Hands_zone.Columns.Add("속성", 50, HorizontalAlignment.Left);
            Player1_Hands_zone.Columns.Add("필요마나", 50, HorizontalAlignment.Left);
            Player1_Hands_zone.Columns.Add("공격력", 50, HorizontalAlignment.Left);
            Player1_Hands_zone.Columns.Add("생명력", 50, HorizontalAlignment.Left);
            Player1_Hands_zone.Columns.Add("설명", 700, HorizontalAlignment.Left);

            // 컬럼명과 컬럼사이즈 지정
            Player2_Hands_zone.Columns.Add("이름", 150, HorizontalAlignment.Left);
            Player2_Hands_zone.Columns.Add("종류", 50, HorizontalAlignment.Left);
            Player2_Hands_zone.Columns.Add("속성", 50, HorizontalAlignment.Left);
            Player2_Hands_zone.Columns.Add("필요마나", 50, HorizontalAlignment.Left);
            Player2_Hands_zone.Columns.Add("공격력", 50, HorizontalAlignment.Left);
            Player2_Hands_zone.Columns.Add("생명력", 50, HorizontalAlignment.Left);
            Player2_Hands_zone.Columns.Add("설명", 700, HorizontalAlignment.Left);



            Player1_warZone_listview.Columns.Add("이름", 150, HorizontalAlignment.Left);
            Player1_warZone_listview.Columns.Add("종류", 50, HorizontalAlignment.Left);
            Player1_warZone_listview.Columns.Add("속성", 50, HorizontalAlignment.Left);
            Player1_warZone_listview.Columns.Add("필요마나", 50, HorizontalAlignment.Left);
            Player1_warZone_listview.Columns.Add("공격력", 50, HorizontalAlignment.Left);
            Player1_warZone_listview.Columns.Add("생명력", 50, HorizontalAlignment.Left);
            Player1_warZone_listview.Columns.Add("설명", 700, HorizontalAlignment.Left);

            // 리스뷰를 Refresh하여 보여줌
            Player1_Hands_zone.EndUpdate();
            Player2_Hands_zone.EndUpdate();
            Player1_warZone_listview.EndUpdate();
        }

        private void Player1_Hands_zone_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //this.Player1_Hands_zone.Sort();
        }

        private void Player1_Hands_zone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(Player1_Hands_zone.FocusedItem.Index+"");

            if (game.thisturn)
            {
                game.gb.P1_WarZone.Add(game.gb.P1_HandsZone[Player1_Hands_zone.FocusedItem.Index]);
                game.gb.P1_HandsZone.RemoveAt(Player1_Hands_zone.FocusedItem.Index);
            }
            else
            {
                game.gb.P2_WarZone.Add(game.gb.P2_HandsZone[Player1_Hands_zone.FocusedItem.Index]);
                game.gb.P2_HandsZone.RemoveAt(Player1_Hands_zone.FocusedItem.Index);
            }

            Player1_Hands_zone.EndUpdate();
        }

    }
}
