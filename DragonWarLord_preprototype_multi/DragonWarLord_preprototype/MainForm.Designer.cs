namespace DragonWarLord_preprototype
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Opponent_hands_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.Turn_btn = new System.Windows.Forms.Button();
            this.Opponent_warZone_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.My_warZone_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.My_cnt_dark = new System.Windows.Forms.Label();
            this.My_cnt_fire = new System.Windows.Forms.Label();
            this.My_remain_dark = new System.Windows.Forms.Label();
            this.My_remain_fire = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.My_Mana_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.Opponent_Mana_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.My_Tomb_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.Opponent_Tomb_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.My_use_all = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Opponent_cnt_dark = new System.Windows.Forms.Label();
            this.Opponent_cnt_fire = new System.Windows.Forms.Label();
            this.Opponent_remain_dark = new System.Windows.Forms.Label();
            this.Opponent_remain_fire = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.Opponent_use_all = new System.Windows.Forms.Label();
            this.Opponent_Player = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.My_Player = new System.Windows.Forms.FlowLayoutPanel();
            this.My_hands_frame = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // Opponent_hands_frame
            // 
            this.Opponent_hands_frame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Opponent_hands_frame.AutoScroll = true;
            this.Opponent_hands_frame.BackColor = System.Drawing.Color.ForestGreen;
            this.Opponent_hands_frame.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.hands_back;
            this.Opponent_hands_frame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Opponent_hands_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Opponent_hands_frame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Opponent_hands_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.Opponent_hands_frame.Location = new System.Drawing.Point(238, 12);
            this.Opponent_hands_frame.Margin = new System.Windows.Forms.Padding(0);
            this.Opponent_hands_frame.MaximumSize = new System.Drawing.Size(1013, 260);
            this.Opponent_hands_frame.MinimumSize = new System.Drawing.Size(1013, 240);
            this.Opponent_hands_frame.Name = "Opponent_hands_frame";
            this.Opponent_hands_frame.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.Opponent_hands_frame.Size = new System.Drawing.Size(1013, 260);
            this.Opponent_hands_frame.TabIndex = 0;
            // 
            // Turn_btn
            // 
            this.Turn_btn.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.Turn_btn.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Turn_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Turn_btn.Location = new System.Drawing.Point(1265, 489);
            this.Turn_btn.Name = "Turn_btn";
            this.Turn_btn.Size = new System.Drawing.Size(193, 58);
            this.Turn_btn.TabIndex = 1;
            this.Turn_btn.Text = "턴종료";
            this.Turn_btn.UseVisualStyleBackColor = false;
            this.Turn_btn.Click += new System.EventHandler(this.Turn_btn_Click);
            // 
            // Opponent_warZone_frame
            // 
            this.Opponent_warZone_frame.AllowDrop = true;
            this.Opponent_warZone_frame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Opponent_warZone_frame.AutoScroll = true;
            this.Opponent_warZone_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Opponent_warZone_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.Opponent_warZone_frame.Location = new System.Drawing.Point(238, 276);
            this.Opponent_warZone_frame.Name = "Opponent_warZone_frame";
            this.Opponent_warZone_frame.Size = new System.Drawing.Size(1013, 240);
            this.Opponent_warZone_frame.TabIndex = 0;
            // 
            // My_warZone_frame
            // 
            this.My_warZone_frame.AllowDrop = true;
            this.My_warZone_frame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.My_warZone_frame.AutoScroll = true;
            this.My_warZone_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.My_warZone_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.My_warZone_frame.Location = new System.Drawing.Point(238, 520);
            this.My_warZone_frame.Name = "My_warZone_frame";
            this.My_warZone_frame.Size = new System.Drawing.Size(1013, 240);
            this.My_warZone_frame.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.My_cnt_dark, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.My_cnt_fire, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.My_remain_dark, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.My_remain_fire, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1261, 613);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(216, 141);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::DragonWarLord_preprototype.Properties.Resources.fire;
            this.pictureBox2.Location = new System.Drawing.Point(4, 74);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 63);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // My_cnt_dark
            // 
            this.My_cnt_dark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.My_cnt_dark.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.My_cnt_dark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.My_cnt_dark.Location = new System.Drawing.Point(75, 1);
            this.My_cnt_dark.Name = "My_cnt_dark";
            this.My_cnt_dark.Size = new System.Drawing.Size(64, 69);
            this.My_cnt_dark.TabIndex = 3;
            this.My_cnt_dark.Text = "0";
            this.My_cnt_dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // My_cnt_fire
            // 
            this.My_cnt_fire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.My_cnt_fire.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.My_cnt_fire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.My_cnt_fire.Location = new System.Drawing.Point(75, 71);
            this.My_cnt_fire.Name = "My_cnt_fire";
            this.My_cnt_fire.Size = new System.Drawing.Size(64, 69);
            this.My_cnt_fire.TabIndex = 3;
            this.My_cnt_fire.Text = "0";
            this.My_cnt_fire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // My_remain_dark
            // 
            this.My_remain_dark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.My_remain_dark.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.My_remain_dark.ForeColor = System.Drawing.Color.Green;
            this.My_remain_dark.Location = new System.Drawing.Point(146, 1);
            this.My_remain_dark.Name = "My_remain_dark";
            this.My_remain_dark.Size = new System.Drawing.Size(66, 69);
            this.My_remain_dark.TabIndex = 3;
            this.My_remain_dark.Text = "0";
            this.My_remain_dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // My_remain_fire
            // 
            this.My_remain_fire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.My_remain_fire.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.My_remain_fire.ForeColor = System.Drawing.Color.Green;
            this.My_remain_fire.Location = new System.Drawing.Point(146, 71);
            this.My_remain_fire.Name = "My_remain_fire";
            this.My_remain_fire.Size = new System.Drawing.Size(66, 69);
            this.My_remain_fire.TabIndex = 3;
            this.My_remain_fire.Text = "0";
            this.My_remain_fire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::DragonWarLord_preprototype.Properties.Resources.dark;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // My_Mana_frame
            // 
            this.My_Mana_frame.AutoScroll = true;
            this.My_Mana_frame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.My_Mana_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.My_Mana_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.My_Mana_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.My_Mana_frame.Location = new System.Drawing.Point(1257, 763);
            this.My_Mana_frame.Name = "My_Mana_frame";
            this.My_Mana_frame.Size = new System.Drawing.Size(220, 240);
            this.My_Mana_frame.TabIndex = 3;
            // 
            // Opponent_Mana_frame
            // 
            this.Opponent_Mana_frame.AutoScroll = true;
            this.Opponent_Mana_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Opponent_Mana_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Opponent_Mana_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.Opponent_Mana_frame.Location = new System.Drawing.Point(1261, 12);
            this.Opponent_Mana_frame.Name = "Opponent_Mana_frame";
            this.Opponent_Mana_frame.Size = new System.Drawing.Size(220, 258);
            this.Opponent_Mana_frame.TabIndex = 3;
            // 
            // My_Tomb_frame
            // 
            this.My_Tomb_frame.AutoScroll = true;
            this.My_Tomb_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.My_Tomb_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.My_Tomb_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.My_Tomb_frame.Location = new System.Drawing.Point(12, 763);
            this.My_Tomb_frame.Name = "My_Tomb_frame";
            this.My_Tomb_frame.Size = new System.Drawing.Size(220, 240);
            this.My_Tomb_frame.TabIndex = 3;
            // 
            // Opponent_Tomb_frame
            // 
            this.Opponent_Tomb_frame.AutoScroll = true;
            this.Opponent_Tomb_frame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Opponent_Tomb_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Opponent_Tomb_frame.Location = new System.Drawing.Point(12, 12);
            this.Opponent_Tomb_frame.Name = "Opponent_Tomb_frame";
            this.Opponent_Tomb_frame.Size = new System.Drawing.Size(220, 258);
            this.Opponent_Tomb_frame.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("함초롬바탕 확장", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(27, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 35);
            this.label2.TabIndex = 4;
            this.label2.Text = "TOMB_ZONE";
            // 
            // My_use_all
            // 
            this.My_use_all.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.My_use_all.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.My_use_all.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.My_use_all.ForeColor = System.Drawing.Color.Red;
            this.My_use_all.Location = new System.Drawing.Point(1483, 663);
            this.My_use_all.Name = "My_use_all";
            this.My_use_all.Size = new System.Drawing.Size(51, 42);
            this.My_use_all.TabIndex = 3;
            this.My_use_all.Text = "0";
            this.My_use_all.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.pictureBox3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Opponent_cnt_dark, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.Opponent_cnt_fire, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.Opponent_remain_dark, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.Opponent_remain_fire, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox6, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1261, 276);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(216, 141);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = global::DragonWarLord_preprototype.Properties.Resources.fire;
            this.pictureBox3.Location = new System.Drawing.Point(4, 74);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 63);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // Opponent_cnt_dark
            // 
            this.Opponent_cnt_dark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Opponent_cnt_dark.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Opponent_cnt_dark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Opponent_cnt_dark.Location = new System.Drawing.Point(75, 1);
            this.Opponent_cnt_dark.Name = "Opponent_cnt_dark";
            this.Opponent_cnt_dark.Size = new System.Drawing.Size(64, 69);
            this.Opponent_cnt_dark.TabIndex = 3;
            this.Opponent_cnt_dark.Text = "0";
            this.Opponent_cnt_dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Opponent_cnt_fire
            // 
            this.Opponent_cnt_fire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Opponent_cnt_fire.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Opponent_cnt_fire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Opponent_cnt_fire.Location = new System.Drawing.Point(75, 71);
            this.Opponent_cnt_fire.Name = "Opponent_cnt_fire";
            this.Opponent_cnt_fire.Size = new System.Drawing.Size(64, 69);
            this.Opponent_cnt_fire.TabIndex = 3;
            this.Opponent_cnt_fire.Text = "0";
            this.Opponent_cnt_fire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Opponent_remain_dark
            // 
            this.Opponent_remain_dark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Opponent_remain_dark.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Opponent_remain_dark.ForeColor = System.Drawing.Color.Green;
            this.Opponent_remain_dark.Location = new System.Drawing.Point(146, 1);
            this.Opponent_remain_dark.Name = "Opponent_remain_dark";
            this.Opponent_remain_dark.Size = new System.Drawing.Size(66, 69);
            this.Opponent_remain_dark.TabIndex = 3;
            this.Opponent_remain_dark.Text = "0";
            this.Opponent_remain_dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Opponent_remain_fire
            // 
            this.Opponent_remain_fire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Opponent_remain_fire.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Opponent_remain_fire.ForeColor = System.Drawing.Color.Green;
            this.Opponent_remain_fire.Location = new System.Drawing.Point(146, 71);
            this.Opponent_remain_fire.Name = "Opponent_remain_fire";
            this.Opponent_remain_fire.Size = new System.Drawing.Size(66, 69);
            this.Opponent_remain_fire.TabIndex = 3;
            this.Opponent_remain_fire.Text = "0";
            this.Opponent_remain_fire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox6.Image = global::DragonWarLord_preprototype.Properties.Resources.dark;
            this.pictureBox6.Location = new System.Drawing.Point(4, 4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(64, 63);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // Opponent_use_all
            // 
            this.Opponent_use_all.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Opponent_use_all.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Opponent_use_all.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Opponent_use_all.ForeColor = System.Drawing.Color.Red;
            this.Opponent_use_all.Location = new System.Drawing.Point(1483, 324);
            this.Opponent_use_all.Name = "Opponent_use_all";
            this.Opponent_use_all.Size = new System.Drawing.Size(51, 42);
            this.Opponent_use_all.TabIndex = 3;
            this.Opponent_use_all.Text = "0";
            this.Opponent_use_all.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Opponent_Player
            // 
            this.Opponent_Player.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Opponent_Player.AutoSize = true;
            this.Opponent_Player.BackColor = System.Drawing.Color.Transparent;
            this.Opponent_Player.Location = new System.Drawing.Point(1537, 188);
            this.Opponent_Player.Margin = new System.Windows.Forms.Padding(0);
            this.Opponent_Player.Name = "Opponent_Player";
            this.Opponent_Player.Size = new System.Drawing.Size(140, 202);
            this.Opponent_Player.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label6.Location = new System.Drawing.Point(1261, 420);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "Attribute";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label7.Location = new System.Drawing.Point(1347, 420);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 21);
            this.label7.TabIndex = 5;
            this.label7.Text = "cost";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label8.Location = new System.Drawing.Point(1408, 420);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 21);
            this.label8.TabIndex = 5;
            this.label8.Text = "Remain";
            // 
            // My_Player
            // 
            this.My_Player.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.My_Player.AutoSize = true;
            this.My_Player.BackColor = System.Drawing.Color.Transparent;
            this.My_Player.Location = new System.Drawing.Point(1537, 613);
            this.My_Player.Margin = new System.Windows.Forms.Padding(0);
            this.My_Player.Name = "My_Player";
            this.My_Player.Size = new System.Drawing.Size(140, 202);
            this.My_Player.TabIndex = 3;
            // 
            // My_hands_frame
            // 
            this.My_hands_frame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.My_hands_frame.AutoScroll = true;
            this.My_hands_frame.BackColor = System.Drawing.Color.ForestGreen;
            this.My_hands_frame.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.hands_back;
            this.My_hands_frame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.My_hands_frame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.My_hands_frame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.My_hands_frame.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.My_hands_frame.Location = new System.Drawing.Point(238, 763);
            this.My_hands_frame.Margin = new System.Windows.Forms.Padding(0);
            this.My_hands_frame.MaximumSize = new System.Drawing.Size(1013, 240);
            this.My_hands_frame.MinimumSize = new System.Drawing.Size(1013, 240);
            this.My_hands_frame.Name = "My_hands_frame";
            this.My_hands_frame.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.My_hands_frame.Size = new System.Drawing.Size(1013, 240);
            this.My_hands_frame.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("함초롬바탕 확장", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(27, 725);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 35);
            this.label1.TabIndex = 4;
            this.label1.Text = "TOMB_ZONE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(1261, 585);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Attribute";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(1347, 585);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "cost";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label5.Location = new System.Drawing.Point(1408, 585);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "Remain";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label9.Location = new System.Drawing.Point(1578, 585);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 21);
            this.label9.TabIndex = 5;
            this.label9.Text = "PLAYER1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("함초롬바탕 확장", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label10.Location = new System.Drawing.Point(1578, 420);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 21);
            this.label10.TabIndex = 5;
            this.label10.Text = "PLAYER2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(1719, 1012);
            this.Controls.Add(this.My_hands_frame);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Opponent_Tomb_frame);
            this.Controls.Add(this.Opponent_use_all);
            this.Controls.Add(this.My_use_all);
            this.Controls.Add(this.My_Player);
            this.Controls.Add(this.Opponent_Player);
            this.Controls.Add(this.Opponent_Mana_frame);
            this.Controls.Add(this.My_Tomb_frame);
            this.Controls.Add(this.My_Mana_frame);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Turn_btn);
            this.Controls.Add(this.Opponent_warZone_frame);
            this.Controls.Add(this.Opponent_hands_frame);
            this.Controls.Add(this.My_warZone_frame);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warlord";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel Opponent_hands_frame;
        private System.Windows.Forms.Button Turn_btn;
        public System.Windows.Forms.FlowLayoutPanel Opponent_warZone_frame;
        public System.Windows.Forms.FlowLayoutPanel My_warZone_frame;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label My_cnt_dark;
        public System.Windows.Forms.Label My_cnt_fire;
        public System.Windows.Forms.FlowLayoutPanel My_Mana_frame;
        public System.Windows.Forms.FlowLayoutPanel Opponent_Mana_frame;
        public System.Windows.Forms.FlowLayoutPanel My_Tomb_frame;
        public System.Windows.Forms.FlowLayoutPanel Opponent_Tomb_frame;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label My_remain_dark;
        public System.Windows.Forms.Label My_remain_fire;
        public System.Windows.Forms.Label My_use_all;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.Label Opponent_cnt_dark;
        public System.Windows.Forms.Label Opponent_cnt_fire;
        public System.Windows.Forms.Label Opponent_remain_dark;
        public System.Windows.Forms.Label Opponent_remain_fire;
        private System.Windows.Forms.PictureBox pictureBox6;
        public System.Windows.Forms.Label Opponent_use_all;
        public System.Windows.Forms.FlowLayoutPanel Opponent_Player;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.FlowLayoutPanel My_Player;
        public System.Windows.Forms.FlowLayoutPanel My_hands_frame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}