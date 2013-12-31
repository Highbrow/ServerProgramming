namespace TestConsoleClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.p1_hands_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.p2_hands_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.Turn_btn = new System.Windows.Forms.Button();
            this.p2_warZone_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.p1_warZone_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.p1_cnt_dark = new System.Windows.Forms.Label();
            this.p1_cnt_fire = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.p2_cnt_dark = new System.Windows.Forms.Label();
            this.p2_cnt_fire = new System.Windows.Forms.Label();
            this.p1_Mana_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.p2_Mana_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.p1_Tomb_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.p2_Tomb_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // p1_hands_frame
            // 
            this.p1_hands_frame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.p1_hands_frame.AutoScroll = true;
            this.p1_hands_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1_hands_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.p1_hands_frame.Location = new System.Drawing.Point(238, 705);
            this.p1_hands_frame.Name = "p1_hands_frame";
            this.p1_hands_frame.Size = new System.Drawing.Size(1013, 225);
            this.p1_hands_frame.TabIndex = 0;
            // 
            // p2_hands_frame
            // 
            this.p2_hands_frame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.p2_hands_frame.AutoScroll = true;
            this.p2_hands_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2_hands_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.p2_hands_frame.Location = new System.Drawing.Point(238, 12);
            this.p2_hands_frame.Name = "p2_hands_frame";
            this.p2_hands_frame.Size = new System.Drawing.Size(1013, 225);
            this.p2_hands_frame.TabIndex = 0;
            // 
            // Turn_btn
            // 
            this.Turn_btn.Location = new System.Drawing.Point(1257, 466);
            this.Turn_btn.Name = "Turn_btn";
            this.Turn_btn.Size = new System.Drawing.Size(75, 23);
            this.Turn_btn.TabIndex = 1;
            this.Turn_btn.Text = "턴종료";
            this.Turn_btn.UseVisualStyleBackColor = true;
            this.Turn_btn.Click += new System.EventHandler(this.Turn_btn_Click);
            // 
            // p2_warZone_frame
            // 
            this.p2_warZone_frame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.p2_warZone_frame.AutoScroll = true;
            this.p2_warZone_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2_warZone_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.p2_warZone_frame.Location = new System.Drawing.Point(238, 243);
            this.p2_warZone_frame.Name = "p2_warZone_frame";
            this.p2_warZone_frame.Size = new System.Drawing.Size(1013, 225);
            this.p2_warZone_frame.TabIndex = 0;
            // 
            // p1_warZone_frame
            // 
            this.p1_warZone_frame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.p1_warZone_frame.AutoScroll = true;
            this.p1_warZone_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1_warZone_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.p1_warZone_frame.Location = new System.Drawing.Point(238, 474);
            this.p1_warZone_frame.Name = "p1_warZone_frame";
            this.p1_warZone_frame.Size = new System.Drawing.Size(1013, 225);
            this.p1_warZone_frame.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.29032F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.70968F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.p1_cnt_dark, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.p1_cnt_fire, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1257, 558);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(125, 141);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Image = global::TestConsoleClient.Properties.Resources.dark;
            this.pictureBox1.Location = new System.Drawing.Point(4, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 39);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox2.Image = global::TestConsoleClient.Properties.Resources.fire;
            this.pictureBox2.Location = new System.Drawing.Point(4, 86);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 39);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // p1_cnt_dark
            // 
            this.p1_cnt_dark.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.p1_cnt_dark.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.p1_cnt_dark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.p1_cnt_dark.Location = new System.Drawing.Point(49, 14);
            this.p1_cnt_dark.Name = "p1_cnt_dark";
            this.p1_cnt_dark.Size = new System.Drawing.Size(72, 42);
            this.p1_cnt_dark.TabIndex = 3;
            this.p1_cnt_dark.Text = "0";
            this.p1_cnt_dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p1_cnt_fire
            // 
            this.p1_cnt_fire.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.p1_cnt_fire.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.p1_cnt_fire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.p1_cnt_fire.Location = new System.Drawing.Point(49, 84);
            this.p1_cnt_fire.Name = "p1_cnt_fire";
            this.p1_cnt_fire.Size = new System.Drawing.Size(72, 42);
            this.p1_cnt_fire.TabIndex = 3;
            this.p1_cnt_fire.Text = "0";
            this.p1_cnt_fire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.29032F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.70968F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.p2_cnt_dark, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.p2_cnt_fire, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(107, 243);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(125, 141);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox4.Image = global::TestConsoleClient.Properties.Resources.dark;
            this.pictureBox4.Location = new System.Drawing.Point(4, 16);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(38, 39);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox5.Image = global::TestConsoleClient.Properties.Resources.fire;
            this.pictureBox5.Location = new System.Drawing.Point(4, 86);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(38, 39);
            this.pictureBox5.TabIndex = 1;
            this.pictureBox5.TabStop = false;
            // 
            // p2_cnt_dark
            // 
            this.p2_cnt_dark.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.p2_cnt_dark.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.p2_cnt_dark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.p2_cnt_dark.Location = new System.Drawing.Point(49, 14);
            this.p2_cnt_dark.Name = "p2_cnt_dark";
            this.p2_cnt_dark.Size = new System.Drawing.Size(72, 42);
            this.p2_cnt_dark.TabIndex = 3;
            this.p2_cnt_dark.Text = "0";
            this.p2_cnt_dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p2_cnt_fire
            // 
            this.p2_cnt_fire.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.p2_cnt_fire.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.p2_cnt_fire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.p2_cnt_fire.Location = new System.Drawing.Point(49, 84);
            this.p2_cnt_fire.Name = "p2_cnt_fire";
            this.p2_cnt_fire.Size = new System.Drawing.Size(72, 42);
            this.p2_cnt_fire.TabIndex = 3;
            this.p2_cnt_fire.Text = "0";
            this.p2_cnt_fire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // p1_Mana_frame
            // 
            this.p1_Mana_frame.AutoScroll = true;
            this.p1_Mana_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.p1_Mana_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1_Mana_frame.Location = new System.Drawing.Point(1257, 705);
            this.p1_Mana_frame.Name = "p1_Mana_frame";
            this.p1_Mana_frame.Size = new System.Drawing.Size(220, 225);
            this.p1_Mana_frame.TabIndex = 3;
            // 
            // p2_Mana_frame
            // 
            this.p2_Mana_frame.AutoScroll = true;
            this.p2_Mana_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.p2_Mana_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2_Mana_frame.Location = new System.Drawing.Point(12, 12);
            this.p2_Mana_frame.Name = "p2_Mana_frame";
            this.p2_Mana_frame.Size = new System.Drawing.Size(220, 225);
            this.p2_Mana_frame.TabIndex = 3;
            // 
            // p1_Tomb_frame
            // 
            this.p1_Tomb_frame.AutoScroll = true;
            this.p1_Tomb_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.p1_Tomb_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1_Tomb_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.p1_Tomb_frame.Location = new System.Drawing.Point(12, 705);
            this.p1_Tomb_frame.Name = "p1_Tomb_frame";
            this.p1_Tomb_frame.Size = new System.Drawing.Size(220, 225);
            this.p1_Tomb_frame.TabIndex = 3;
            // 
            // p2_Tomb_frame
            // 
            this.p2_Tomb_frame.AutoScroll = true;
            this.p2_Tomb_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.p2_Tomb_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p2_Tomb_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.p2_Tomb_frame.Location = new System.Drawing.Point(1257, 12);
            this.p2_Tomb_frame.Name = "p2_Tomb_frame";
            this.p2_Tomb_frame.Size = new System.Drawing.Size(220, 225);
            this.p2_Tomb_frame.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("새굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(100, 683);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "무덤";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("새굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(1345, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "무덤";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1489, 970);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.p2_Tomb_frame);
            this.Controls.Add(this.p2_Mana_frame);
            this.Controls.Add(this.p1_Tomb_frame);
            this.Controls.Add(this.p1_Mana_frame);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Turn_btn);
            this.Controls.Add(this.p2_warZone_frame);
            this.Controls.Add(this.p2_hands_frame);
            this.Controls.Add(this.p1_warZone_frame);
            this.Controls.Add(this.p1_hands_frame);
            this.Name = "MainForm";
            this.Text = "Warlord";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel p1_hands_frame;
        public System.Windows.Forms.FlowLayoutPanel p2_hands_frame;
        private System.Windows.Forms.Button Turn_btn;
        public System.Windows.Forms.FlowLayoutPanel p2_warZone_frame;
        public System.Windows.Forms.FlowLayoutPanel p1_warZone_frame;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label p1_cnt_dark;
        public System.Windows.Forms.Label p1_cnt_fire;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        public System.Windows.Forms.Label p2_cnt_dark;
        public System.Windows.Forms.Label p2_cnt_fire;
        public System.Windows.Forms.FlowLayoutPanel p1_Mana_frame;
        public System.Windows.Forms.FlowLayoutPanel p2_Mana_frame;
        public System.Windows.Forms.FlowLayoutPanel p1_Tomb_frame;
        public System.Windows.Forms.FlowLayoutPanel p2_Tomb_frame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}