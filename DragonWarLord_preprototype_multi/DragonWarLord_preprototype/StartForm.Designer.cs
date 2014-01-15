namespace DragonWarLord_preprototype
{
    partial class StartForm
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
            this.ready_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.exit_btn = new System.Windows.Forms.Button();
            this.lb_status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ready_btn
            // 
            this.ready_btn.Location = new System.Drawing.Point(197, 385);
            this.ready_btn.Name = "ready_btn";
            this.ready_btn.Size = new System.Drawing.Size(75, 23);
            this.ready_btn.TabIndex = 0;
            this.ready_btn.Text = "READY";
            this.ready_btn.UseVisualStyleBackColor = true;
            this.ready_btn.Click += new System.EventHandler(this.ready_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = global::DragonWarLord_preprototype.Properties.Resources.ghost;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(259, 274);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // exit_btn
            // 
            this.exit_btn.Location = new System.Drawing.Point(13, 385);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(75, 23);
            this.exit_btn.TabIndex = 0;
            this.exit_btn.Text = "EXIT";
            this.exit_btn.UseVisualStyleBackColor = true;
            this.exit_btn.Click += new System.EventHandler(this.exit_btn_Click);
            // 
            // lb_status
            // 
            this.lb_status.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lb_status.Font = new System.Drawing.Font("휴먼둥근헤드라인", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_status.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_status.Location = new System.Drawing.Point(13, 294);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(259, 88);
            this.lb_status.TabIndex = 2;
            this.lb_status.Text = "Dragon WarLord Pre-Prototype";
            this.lb_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DragonWarLord_preprototype.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(284, 420);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.ready_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartForm";
            this.Load += new System.EventHandler(this.StartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ready_btn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Label lb_status;
    }
}