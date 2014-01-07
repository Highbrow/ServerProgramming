using System.Drawing;
using System.Windows.Forms;
namespace TestConsoleClient
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
            this.attribute_layout = new System.Windows.Forms.FlowLayoutPanel();
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.lb_type = new System.Windows.Forms.Label();
            this.lb_aphp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.BackColor = System.Drawing.Color.MidnightBlue;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb_name.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_name.ForeColor = System.Drawing.Color.Yellow;
            this.lb_name.Location = new System.Drawing.Point(0, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(31, 15);
            this.lb_name.TabIndex = 1;
            this.lb_name.Text = "이름";
            this.lb_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_name.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // lb_information
            // 
            this.lb_information.AutoEllipsis = true;
            this.lb_information.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lb_information.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_information.ForeColor = System.Drawing.Color.OrangeRed;
            this.lb_information.Location = new System.Drawing.Point(5, 108);
            this.lb_information.Name = "lb_information";
            this.lb_information.Size = new System.Drawing.Size(132, 88);
            this.lb_information.TabIndex = 3;
            this.lb_information.Text = "설명";
            this.lb_information.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_information.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_information.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // attribute_layout
            // 
            this.attribute_layout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.attribute_layout.AutoSize = true;
            this.attribute_layout.BackColor = System.Drawing.Color.White;
            this.attribute_layout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.attribute_layout.Font = new System.Drawing.Font("새굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.attribute_layout.Location = new System.Drawing.Point(131, 22);
            this.attribute_layout.Name = "attribute_layout";
            this.attribute_layout.Size = new System.Drawing.Size(10, 20);
            this.attribute_layout.TabIndex = 5;
            this.attribute_layout.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.attribute_layout.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // pb_image
            // 
            this.pb_image.Location = new System.Drawing.Point(5, 3);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(129, 115);
            this.pb_image.TabIndex = 4;
            this.pb_image.TabStop = false;
            this.pb_image.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.pb_image.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // lb_type
            // 
            this.lb_type.AutoSize = true;
            this.lb_type.BackColor = System.Drawing.Color.Maroon;
            this.lb_type.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_type.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lb_type.Location = new System.Drawing.Point(8, 91);
            this.lb_type.Name = "lb_type";
            this.lb_type.Size = new System.Drawing.Size(31, 15);
            this.lb_type.TabIndex = 6;
            this.lb_type.Text = "타입";
            this.lb_type.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_type.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_type.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // lb_aphp
            // 
            this.lb_aphp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_aphp.AutoSize = true;
            this.lb_aphp.BackColor = System.Drawing.Color.Transparent;
            this.lb_aphp.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_aphp.ForeColor = System.Drawing.Color.Yellow;
            this.lb_aphp.Location = new System.Drawing.Point(3, 194);
            this.lb_aphp.Name = "lb_aphp";
            this.lb_aphp.Size = new System.Drawing.Size(85, 15);
            this.lb_aphp.TabIndex = 7;
            this.lb_aphp.Text = "공격력/생명력";
            this.lb_aphp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lb_aphp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.lb_aphp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            // 
            // Card_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BackgroundImage = global::TestConsoleClient.Properties.Resources.backimg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.lb_aphp);
            this.Controls.Add(this.lb_information);
            this.Controls.Add(this.lb_type);
            this.Controls.Add(this.attribute_layout);
            this.Controls.Add(this.pb_image);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Name = "Card_Control";
            this.Size = new System.Drawing.Size(140, 213);
            this.Load += new System.EventHandler(this.Card_Control_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Card_Control_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_information;
        private System.Windows.Forms.PictureBox pb_image;
        private System.Windows.Forms.FlowLayoutPanel attribute_layout;
        private System.Windows.Forms.Label lb_type;
        public System.Windows.Forms.Label lb_aphp;
    }
}
