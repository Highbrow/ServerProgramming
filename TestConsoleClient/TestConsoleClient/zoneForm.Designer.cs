namespace TestConsoleClient
{
    partial class zoneForm
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
            this.zone_dialog = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // zone_dialog
            // 
            this.zone_dialog.AutoSize = true;
            this.zone_dialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zone_dialog.Location = new System.Drawing.Point(0, 0);
            this.zone_dialog.Name = "zone_dialog";
            this.zone_dialog.Size = new System.Drawing.Size(266, 346);
            this.zone_dialog.TabIndex = 0;
            // 
            // zoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(266, 346);
            this.Controls.Add(this.zone_dialog);
            this.Name = "zoneForm";
            this.Text = "zoneForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.zoneForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel zone_dialog;
    }
}