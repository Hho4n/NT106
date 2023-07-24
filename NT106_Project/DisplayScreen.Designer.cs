namespace NT106_Project
{
    partial class DisplayScreen
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
            this.MainScreen = new System.Windows.Forms.PictureBox();
            this.cb_Mouse_ct = new System.Windows.Forms.CheckBox();
            this.cb_Keycb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // MainScreen
            // 
            this.MainScreen.Location = new System.Drawing.Point(-2, 12);
            this.MainScreen.Name = "MainScreen";
            this.MainScreen.Size = new System.Drawing.Size(802, 422);
            this.MainScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainScreen.TabIndex = 0;
            this.MainScreen.TabStop = false;
            this.MainScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainScreen_MouseClick);
            this.MainScreen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainScreen_MouseDoubleClick);
            this.MainScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainScreen_MouseDown);
            this.MainScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainScreen_MouseUp);
            // 
            // cb_Mouse_ct
            // 
            this.cb_Mouse_ct.AutoSize = true;
            this.cb_Mouse_ct.Location = new System.Drawing.Point(-2, 428);
            this.cb_Mouse_ct.Name = "cb_Mouse_ct";
            this.cb_Mouse_ct.Size = new System.Drawing.Size(115, 20);
            this.cb_Mouse_ct.TabIndex = 2;
            this.cb_Mouse_ct.Text = "Mouse Control";
            this.cb_Mouse_ct.UseVisualStyleBackColor = true;
            this.cb_Mouse_ct.CheckedChanged += new System.EventHandler(this.cb_Mouse_ct_CheckedChanged);
            // 
            // cb_Keycb
            // 
            this.cb_Keycb.AutoSize = true;
            this.cb_Keycb.Location = new System.Drawing.Point(119, 428);
            this.cb_Keycb.Name = "cb_Keycb";
            this.cb_Keycb.Size = new System.Drawing.Size(133, 20);
            this.cb_Keycb.TabIndex = 3;
            this.cb_Keycb.Text = "Keyboard Control";
            this.cb_Keycb.UseVisualStyleBackColor = true;
            this.cb_Keycb.CheckedChanged += new System.EventHandler(this.cb_Keycb_CheckedChanged);
            // 
            // DisplayScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cb_Keycb);
            this.Controls.Add(this.cb_Mouse_ct);
            this.Controls.Add(this.MainScreen);
            this.Name = "DisplayScreen";
            this.Text = "Screen";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DisplayScreen_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DisplayScreen_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.MainScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cb_Mouse_ct;
        private System.Windows.Forms.CheckBox cb_Keycb;
        private System.Windows.Forms.PictureBox MainScreen;
    }
}