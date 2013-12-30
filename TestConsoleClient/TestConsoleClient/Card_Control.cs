using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WarLord_Server_GUI.GameLogic_A;
using WarLord_Server_GUI.GameLogic_B;

namespace TestConsoleClient
{
    public partial class Card_Control : UserControl
    {
        public string card_Name { get; set; }
        public string card_type { get; set; }
        public string card_Attribute { get; set; }
        public string card_Consumption { get; set; }
        public int card_Ap { get; set; }
        public int card_Hp { get; set; }
        public string card_Information { get; set; }
        public string card_Species { get; set; }

        private MainForm mainForm;

        public Card_Control(MainForm mainForm)
        {
            // TODO: Complete member initialization
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void Card_Control_Load(object sender, EventArgs e)
        {
            this.lb_name.Text = card_Name;  //카드 이름 부여
            string[] s_con = card_Consumption.Split(';');

            if (Convert.ToInt32(s_con[0]) > 0)  //불
            {
                this.attribute_layout.Controls.Add(new PictureBox() //사진 추가
                {
                    Image = global::TestConsoleClient.Properties.Resources.fire,
                    Size = new System.Drawing.Size(16, 16),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                });
                this.attribute_layout.Controls.Add(new Label()
                {
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = true,
                    Text = s_con[0]
                });
            }

            if (Convert.ToInt32(s_con[1]) > 0)  //암흑
            {
                this.attribute_layout.Controls.Add(new PictureBox() //사진 추가
                {
                    Image = global::TestConsoleClient.Properties.Resources.dark,
                    Size = new System.Drawing.Size(16, 16),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                });
                this.attribute_layout.Controls.Add(new Label()
                {
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = true,
                    Text = s_con[1]
                });
            }

            if (Convert.ToInt32(s_con[2]) > 0)  //아무거나
            {
                this.attribute_layout.Controls.Add(new PictureBox() //사진 추가
                {
                    Image = global::TestConsoleClient.Properties.Resources.nothing,
                    Size = new System.Drawing.Size(16, 16),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                });
                this.attribute_layout.Controls.Add(new Label()
                {
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = true,
                    Text = s_con[2]
                });
            }

            this.pb_image.Image = global::TestConsoleClient.Properties.Resources.ghost;
            this.pb_image.SizeMode = PictureBoxSizeMode.StretchImage;
            this.lb_type.Text = card_type + "-" + card_Species; //종류 및 세부종류
            this.lb_information.Text = card_Information;    //설명
            this.lb_aphp.Text = card_Ap + " / " + card_Hp;
        }

        private void Card_Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (GamePlayManager.Instance.thisturn)
                {
                    Card c = GameBoard.P1_HandsZone[mainForm.p1_hands_frame.Controls.IndexOf(this)];
                    GameBoard.P1_HandsZone.Remove(c);   //데이터 저장 공간 이동
                    GameBoard.P1_ManaZone.Add(c);    //데이터 저장 공간 이동

                    mainForm.p1_hands_frame.Controls.Remove(this);

                    if (card_Attribute.Equals("암흑"))
                    {
                        mainForm.p1_cnt_dark.Text = (Convert.ToInt32(mainForm.p1_cnt_dark.Text) + 1).ToString();
                    }
                    else if (card_Attribute.Equals("불"))
                    {
                        mainForm.p1_cnt_fire.Text = (Convert.ToInt32(mainForm.p1_cnt_fire.Text) + 1).ToString();
                    }
                }
                else
                {
                    Card c = GameBoard.P2_HandsZone[mainForm.p2_hands_frame.Controls.IndexOf(this)];
                    GameBoard.P2_HandsZone.Remove(c);   //데이터 저장 공간 이동
                    GameBoard.P2_ManaZone.Add(c);    //데이터 저장 공간 이동

                    mainForm.p2_hands_frame.Controls.Remove(this);

                    if (card_Attribute.Equals("암흑"))
                    {
                        mainForm.p2_cnt_dark.Text = (Convert.ToInt32(mainForm.p2_cnt_dark.Text) + 1).ToString();
                    }
                    else if (card_Attribute.Equals("불"))
                    {
                        mainForm.p2_cnt_fire.Text = (Convert.ToInt32(mainForm.p2_cnt_fire.Text) + 1).ToString();
                    }
                }
            }
            else
            {
                if (GamePlayManager.Instance.thisturn)
                {
                    //string[] s_con = card_Consumption.Split(';');
                    Card c = GameBoard.P1_HandsZone[mainForm.p1_hands_frame.Controls.IndexOf(this)];
                    GameBoard.P1_HandsZone.Remove(c);   //데이터 저장 공간 이동
                    GameBoard.P1_WarZone.Add(c);    //데이터 저장 공간 이동

                    mainForm.p1_hands_frame.Controls.Remove(this);
                    mainForm.p1_warZone_frame.Controls.Add(this);
                }
                else
                {
                    Card c = GameBoard.P2_HandsZone[mainForm.p2_hands_frame.Controls.IndexOf(this)];
                    GameBoard.P2_HandsZone.Remove(c);   //데이터 저장 공간 이동
                    GameBoard.P2_WarZone.Add(c);    //데이터 저장 공간 이동

                    mainForm.p2_hands_frame.Controls.Remove(this);
                    mainForm.p2_warZone_frame.Controls.Add(this);
                }
            }
        }

        Card selectCard = null;
        Card targetCard = null;
        Color defaultColor;

        private void Card_Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectCard == null)
            {
                defaultColor = this.BackColor;
                this.BackColor = Color.FromArgb(139, 159, 221);
                selectCard = GameBoard.P2_HandsZone[mainForm.p2_hands_frame.Controls.IndexOf(this)];
            }
            else if(selectCard != null && targetCard == null)
            {
                defaultColor = this.BackColor;
                this.BackColor = Color.FromArgb(139, 159, 221);
                targetCard = GameBoard.P2_HandsZone[mainForm.p2_hands_frame.Controls.IndexOf(this)];

                //====처리해주고 ==
                selectCard = null;
                targetCard = null;
            }
        }
    }
}
