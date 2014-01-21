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
using DragonWarLord_preprototype.GameLogic_B;
using DragonWarLord_preprototype.CardLibrary;
using System.Resources;

namespace DragonWarLord_preprototype
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

        delegate void invokeProcCardString(string str);//쓰레드로부터 안전한 처리를 위한 invoke delegate
        delegate void invokeProcCard(bool p);//쓰레드로부터 안전한 처리를 위한 invoke delegate

        public void setCardEnabled(bool p)
        {
            if (this.InvokeRequired)
            {
                invokeProcCard call = new invokeProcCard(setCardEnabled);
                this.Invoke(call, p);
            }
            else
            {
                this.Enabled = p;
            }
        }

        
        public void setText_lb_aphp(string str)
        {
            if (lb_aphp.InvokeRequired)
            {
                invokeProcCardString call = new invokeProcCardString(setText_lb_aphp);
                this.Invoke(call, str);
            }
            else
            {
                lb_aphp.Text = str;
            }
        }


        public void setText_lb_name(string str)
        {
            if (lb_name.InvokeRequired)
            {
                invokeProcCardString call = new invokeProcCardString(setText_lb_name);
                this.Invoke(call, str);
            }
            else
            {
                lb_name.Text = str;
            }
        }
        


        public void Card_refresh()
        {
            this.card.TurnAP = card.Ap;
            this.card.TurnHP = card.Hp;
            this.setText_lb_name(card.Name);  //카드 이름 부여
            this.setText_lb_aphp(card.Ap + " / " + card.Hp);
        }

        private void Card_Control_Load(object sender, EventArgs e)
        {
            this.setText_lb_name(card.Name);  //카드 이름 부여
            string[] s_con = card.Consumption.Split(';');

            if (Convert.ToInt32(s_con[0]) > 0)  //불
            {
                this.attribute_layout.Controls.Add(new PictureBox() //사진 추가
                {
                    Image = global::DragonWarLord_preprototype.Properties.Resources.fire,
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
                    Image = global::DragonWarLord_preprototype.Properties.Resources.dark,
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
                    Image = global::DragonWarLord_preprototype.Properties.Resources.nothing,
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
            this.pb_image.Image = global::DragonWarLord_preprototype.Properties.Resources.ghost;
            ResourceManager rm = DragonWarLord_preprototype.Properties.Resources.ResourceManager;
            this.pb_image.Image = (Bitmap)rm.GetObject(card.Image_file);
            this.pb_image.SizeMode = PictureBoxSizeMode.StretchImage;
            this.lb_type.Text = card.Type + "-" + card.Species; //종류 및 세부종류
            this.lb_information.Text = card.Information;    //설명
            this.lb_aphp.Text = card.Ap + " / " + card.Hp;

        }

        //=====[더블 클릭 이벤트]=====
        private void Card_Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TurnManager.Turn)
            {
                //======[ 핸드 존일 경우 처리 ]=====
                if (this.position == GameBoard.MY_HANDSZONE)
                {
                    if (e.Button == MouseButtons.Right) //마우스 오른쪽 더블 클릭
                    {
                        NetworkManager.ws.Send("MakeResource;" + GamePlayManager.Instance.findCard(this));
                    }
                    else  //마우스 왼쪽 더블 클릭
                    {
                        NetworkManager.ws.Send("UseCard;" + GamePlayManager.Instance.findCard(this));
                    }
                }
            }
            
        }

        //=====[클릭 이벤트]=====
        public void Card_Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (TurnManager.Turn)
            {
                if (GameSkillManager.Instance.skill_16)
                {
                    GamePlayManager.Instance.gameSkill("16", this);
                }
                else if (GameSkillManager.Instance.skill_18)
                {
                    GamePlayManager.Instance.gameSkill("18", this);
                }
                else if (GameSkillManager.Instance.skill_19)
                {
                    GamePlayManager.Instance.gameSkill("19", this);
                }
                else if (GameSkillManager.Instance.skill_20)
                {
                    GamePlayManager.Instance.gameSkill("20", this);
                }
                else if (GameSkillManager.Instance.skill_21)
                {
                    GamePlayManager.Instance.gameSkill("21", this);
                }
                else if (GameSkillManager.Instance.skill_17_first)
                {
                    GamePlayManager.Instance.gameSkill("17", this);
                }
                else if (GameSkillManager.Instance.skill_17_second)
                {
                    GamePlayManager.Instance.gameSkill("17", this);
                }
                else
                {
                    GamePlayManager.Instance.CardSelectProc(this);
                }
            }
        }

    }
}
