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
using System.Threading;
using System.Reflection;
using DragonWarLord_preprototype.CardLibrary;
using System.Resources;
using WarLord_Server_GUI.GameLogic_B;
using DragonWarLord_preprototype.GameLogic_B;

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

        public void Card_refresh()
        {
            this.card.thisTurnAP = card.Ap;
            this.card.thisTurnHP = card.Hp;
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
                        CardDealer.Instance.MakeResource(this);    //마나생성
                    }
                    else  //마우스 왼쪽 더블 클릭
                    {
                        if (CardDealer.Instance.canUseCard(this))
                        {
                            if (this.card.Skill.Equals("1"))
                            {
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("2"))
                            {
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("3"))
                            {
                                CardDealer.Instance.gameSkill("3", this);
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("4"))
                            {
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("5"))
                            {
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("7"))
                            {
                                CardDealer.Instance.gameSkill("7", this);
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("8"))
                            {
                                CardDealer.Instance.gameSkill("8", this);
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("9"))
                            {
                                CardDealer.Instance.gameSkill("9", this);
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("11"))
                            {
                                CardDealer.Instance.gameSkill("11", this);
                                CardDealer.Instance.useCard(this);
                            }
                            else if (this.card.Skill.Equals("16"))
                            {
                                GameSkillManager.Instance.skill_card = this;    //자신 등록
                                CardDealer.Instance.gameSkill("16", this);
                            }
                            else if (this.card.Skill.Equals("17"))
                            {
                                GameSkillManager.Instance.skill_card = this;    //자신 등록
                                CardDealer.Instance.gameSkill("17", this);
                            }
                            else if (this.card.Skill.Equals("18"))
                            {
                                GameSkillManager.Instance.skill_card = this;    //자신 등록
                                CardDealer.Instance.gameSkill("18", this);
                            }
                            else if (this.card.Skill.Equals("19"))
                            {
                                GameSkillManager.Instance.skill_card = this;    //자신 등록
                                CardDealer.Instance.gameSkill("19", this);
                            }
                            else if (this.card.Skill.Equals("20"))
                            {
                                GameSkillManager.Instance.skill_card = this;    //자신 등록
                                CardDealer.Instance.gameSkill("20", this);
                            }
                            else if (this.card.Skill.Equals("21"))
                            {
                                GameSkillManager.Instance.skill_card = this;    //자신 등록
                                CardDealer.Instance.gameSkill("21", this);
                            }
                            else
                            {
                                CardDealer.Instance.useCard(this);
                            }
                        }
                        else
                        {
                        }
                    }
                }
                CardDealer.Instance.CardSelectProc(null);
            }
        }

        //=====[클릭 이벤트]=====
        public void Card_Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (TurnManager.Turn)
            {
                if (GameSkillManager.Instance.skill_16)
                {
                    CardDealer.Instance.gameSkill("16", this);
                }
                else if (GameSkillManager.Instance.skill_18)
                {
                    CardDealer.Instance.gameSkill("18", this);
                }
                else if (GameSkillManager.Instance.skill_19)
                {
                    CardDealer.Instance.gameSkill("19", this);
                }
                else if (GameSkillManager.Instance.skill_20)
                {
                    CardDealer.Instance.gameSkill("20", this);
                }
                else if (GameSkillManager.Instance.skill_21)
                {
                    CardDealer.Instance.gameSkill("21", this);
                }
                else if (GameSkillManager.Instance.skill_17_first)
                {
                    CardDealer.Instance.gameSkill("17", this);
                }
                else if (GameSkillManager.Instance.skill_17_second)
                {
                    CardDealer.Instance.gameSkill("17", this);
                }
                else
                {
                    CardDealer.Instance.CardSelectProc(this);
                }
            }
        }
    }
}
