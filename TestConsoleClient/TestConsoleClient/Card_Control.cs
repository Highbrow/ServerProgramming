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
using System.Threading;
using System.Reflection;

namespace TestConsoleClient
{
    public partial class Card_Control : UserControl
    {
        internal Card card { get; set; }
        internal int position { get; set; }

        internal bool activatable = true;

        public Card_Control()
        {
            InitializeComponent();
        }

        public void Card_refresh()
        {
            this.lb_name.Text = card.Name;  //카드 이름 부여
            this.lb_aphp.Text = card.Ap + " / " + card.Hp;
        }

        private void Card_Control_Load(object sender, EventArgs e)
        {
            this.lb_name.Text = card.Name;  //카드 이름 부여
            string[] s_con = card.Consumption.Split(';');

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
            this.lb_type.Text = card.Type + "-" + card.Species; //종류 및 세부종류
            this.lb_information.Text = card.Information;    //설명
            this.lb_aphp.Text = card.Ap + " / " + card.Hp;
        }

        //=====[더블 클릭 이벤트]=====
        private void Card_Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //======[ 핸드 존일 경우 처리 ]=====
            if (this.position == GamePlayManager.PLAYER1_HANDSZONE || this.position == GamePlayManager.PLAYER2_HANDSZONE)
            {
                if (e.Button == MouseButtons.Right) //마우스 오른쪽 더블 클릭
                {
                    GamePlayManager.Instance.makeMana(this);    //마나생성
                    GamePlayManager.Instance.canMakeMana = false;   //한턴에 한번만 가능하도록
                }
                else  //마우스 왼쪽 더블 클릭
                {
                    GamePlayManager.Instance.popCard(this); //카드내기
                }
            }

            //=====카드 선택 초기화(null)전달
            GamePlayManager.Instance.CardSelectProc(null);
        }


        //=====[클릭 이벤트]=====
        private void Card_Control_MouseClick(object sender, MouseEventArgs e)
        {
            GamePlayManager.Instance.CardSelectProc(this);
        }

    }
}
