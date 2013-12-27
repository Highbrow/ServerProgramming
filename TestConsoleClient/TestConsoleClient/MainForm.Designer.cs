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
            this.Player1_Hands_zone = new System.Windows.Forms.ListView();
            this.Player2_Hands_zone = new System.Windows.Forms.ListView();
            this.Player1_warZone_listview = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // Player1_Hands_zone
            // 
            this.Player1_Hands_zone.Location = new System.Drawing.Point(12, 631);
            this.Player1_Hands_zone.Name = "Player1_Hands_zone";
            this.Player1_Hands_zone.Size = new System.Drawing.Size(1180, 200);
            this.Player1_Hands_zone.TabIndex = 0;
            this.Player1_Hands_zone.UseCompatibleStateImageBehavior = false;
            this.Player1_Hands_zone.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Player1_Hands_zone_ColumnClick);
            this.Player1_Hands_zone.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Player1_Hands_zone_MouseDoubleClick);
            // 
            // Player2_Hands_zone
            // 
            this.Player2_Hands_zone.Location = new System.Drawing.Point(12, 13);
            this.Player2_Hands_zone.Name = "Player2_Hands_zone";
            this.Player2_Hands_zone.Size = new System.Drawing.Size(1180, 200);
            this.Player2_Hands_zone.TabIndex = 1;
            this.Player2_Hands_zone.UseCompatibleStateImageBehavior = false;
            // 
            // Player1_warZone_listview
            // 
            this.Player1_warZone_listview.Location = new System.Drawing.Point(109, 425);
            this.Player1_warZone_listview.Name = "Player1_warZone_listview";
            this.Player1_warZone_listview.Size = new System.Drawing.Size(970, 200);
            this.Player1_warZone_listview.TabIndex = 0;
            this.Player1_warZone_listview.UseCompatibleStateImageBehavior = false;
            // 
            // listView3
            // 
            this.listView3.Location = new System.Drawing.Point(109, 219);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(970, 200);
            this.listView3.TabIndex = 0;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 843);
            this.Controls.Add(this.Player2_Hands_zone);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.Player1_warZone_listview);
            this.Controls.Add(this.Player1_Hands_zone);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView Player1_Hands_zone;
        private System.Windows.Forms.ListView Player2_Hands_zone;
        private System.Windows.Forms.ListView Player1_warZone_listview;
        private System.Windows.Forms.ListView listView3;
    }
}