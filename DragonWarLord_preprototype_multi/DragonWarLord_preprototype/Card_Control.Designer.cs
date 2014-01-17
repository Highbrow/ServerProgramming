using System.Drawing;
using System.Windows.Forms;
namespace DragonWarLord_preprototype
{
    partial class Card_Control
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_information = new System.Windows.Forms.Label();
            this.lb_aphp = new System.Windows.Forms.Label();
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.lb_type = new System.Windows.Forms.Label();
            this.attribute_layout = new System.Windows.Forms.FlowLayoutPanel();
            this.card_panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.card_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.BackColor = System.Drawing.Color.Transparent;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb_name.Font = new System.Drawing.Font("함초롬바탕 확장", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_name.ForeColor = System.Drawing.Color.Black;
            this.lb_name.Location = new System.Drawing.Point(0, 0);
            this.lb_name.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(33, 15);
            this.lb_name.TabIndex = 1;
            this.lb_name.Text = "이름";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_name.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // lb_information
            // 
            this.lb_information.AutoEllipsis = true;
            this.lb_information.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lb_information.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_information.Font = new System.Drawing.Font("함초롬바탕 확장", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_information.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lb_information.Location = new System.Drawing.Point(6, 106);
            this.lb_information.Name = "lb_information";
            this.lb_information.Size = new System.Drawing.Size(154, 88);
            this.lb_information.TabIndex = 3;
            this.lb_information.Text = "설명";
            this.lb_information.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_information.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_information.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // lb_aphp
            // 
            this.lb_aphp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_aphp.AutoSize = true;
            this.lb_aphp.BackColor = System.Drawing.Color.Transparent;
            this.lb_aphp.Font = new System.Drawing.Font("휴먼둥근헤드라인", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_aphp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lb_aphp.Location = new System.Drawing.Point(49, 194);
            this.lb_aphp.Name = "lb_aphp";
            this.lb_aphp.Size = new System.Drawing.Size(72, 17);
            this.lb_aphp.TabIndex = 7;
            this.lb_aphp.Text = "AP/HP";
            this.lb_aphp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lb_aphp.UseMnemonic = false;
            this.lb_aphp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_aphp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // pb_image
            // 
            this.pb_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_image.Location = new System.Drawing.Point(6, 18);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(154, 98);
            this.pb_image.TabIndex = 4;
            this.pb_image.TabStop = false;
            this.pb_image.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.pb_image.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // lb_type
            // 
            this.lb_type.AutoSize = true;
            this.lb_type.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lb_type.Font = new System.Drawing.Font("함초롬바탕 확장", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_type.ForeColor = System.Drawing.Color.Yellow;
            this.lb_type.Location = new System.Drawing.Point(7, 87);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(33, 15);
            this.lb_type.TabIndex = 6;
            this.lb_type.Text = "타입";
            this.lb_type.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_type.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_type.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // attribute_layout
            // 
            this.attribute_layout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.attribute_layout.AutoSize = true;
            this.attribute_layout.BackColor = System.Drawing.Color.White;
            this.attribute_layout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.attribute_layout.Font = new System.Drawing.Font("새굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.attribute_layout.Location = new System.Drawing.Point(150, 18);
            this.attribute_layout.Name = "attribute_layout";
            this.attribute_layout.Size = new System.Drawing.Size(10, 20);
            this.attribute_layout.TabIndex = 5;
            this.attribute_layout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.attribute_layout.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // card_panel
            // 
            this.card_panel.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.card_deselect;
            this.card_panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.card_panel.Controls.Add(this.attribute_layout);
            this.card_panel.Controls.Add(this.lb_type);
            this.card_panel.Controls.Add(this.pb_image);
            this.card_panel.Controls.Add(this.lb_aphp);
            this.card_panel.Controls.Add(this.lb_information);
            this.card_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card_panel.Location = new System.Drawing.Point(0, 0);
            this.card_panel.Name = "card_panel";
            this.card_panel.Size = new System.Drawing.Size(166, 213);
            this.card_panel.TabIndex = 8;
            this.card_panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.card_panel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // Card_Control
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.card_panel);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Name = "Card_Control";
            this.Size = new System.Drawing.Size(166, 213);
            this.Load += new System.EventHandler(this.Card_Control_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.card_panel.ResumeLayout(false);
            this.card_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_name;
        private Label lb_information;
        public Label lb_aphp;
        private PictureBox pb_image;
        private Label lb_type;
        private FlowLayoutPanel attribute_layout;
        public Panel card_panel;
        
    }
}
