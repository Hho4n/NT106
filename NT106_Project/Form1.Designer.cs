namespace NT106_Project
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.txtMyIP = new System.Windows.Forms.TextBox();
            this.txtMyPort = new System.Windows.Forms.TextBox();
            this.txtPartnerIP = new System.Windows.Forms.TextBox();
            this.txtPartnerPort = new System.Windows.Forms.TextBox();
            this.Btn_OpenConnect = new System.Windows.Forms.Button();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Btn_ShareScreen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtMyIP
            // 
            this.txtMyIP.Location = new System.Drawing.Point(80, 183);
            this.txtMyIP.Name = "txtMyIP";
            this.txtMyIP.Size = new System.Drawing.Size(178, 22);
            this.txtMyIP.TabIndex = 0;
            // 
            // txtMyPort
            // 
            this.txtMyPort.Location = new System.Drawing.Point(80, 234);
            this.txtMyPort.Name = "txtMyPort";
            this.txtMyPort.Size = new System.Drawing.Size(178, 22);
            this.txtMyPort.TabIndex = 1;
            // 
            // txtPartnerIP
            // 
            this.txtPartnerIP.Location = new System.Drawing.Point(462, 183);
            this.txtPartnerIP.Name = "txtPartnerIP";
            this.txtPartnerIP.Size = new System.Drawing.Size(178, 22);
            this.txtPartnerIP.TabIndex = 2;
            // 
            // txtPartnerPort
            // 
            this.txtPartnerPort.Location = new System.Drawing.Point(462, 234);
            this.txtPartnerPort.Name = "txtPartnerPort";
            this.txtPartnerPort.Size = new System.Drawing.Size(178, 22);
            this.txtPartnerPort.TabIndex = 3;
            // 
            // Btn_OpenConnect
            // 
            this.Btn_OpenConnect.Location = new System.Drawing.Point(80, 300);
            this.Btn_OpenConnect.Name = "Btn_OpenConnect";
            this.Btn_OpenConnect.Size = new System.Drawing.Size(177, 47);
            this.Btn_OpenConnect.TabIndex = 4;
            this.Btn_OpenConnect.Text = "Open Connect";
            this.Btn_OpenConnect.UseVisualStyleBackColor = true;
            this.Btn_OpenConnect.Click += new System.EventHandler(this.Btn_OpenConnect_Click);
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Location = new System.Drawing.Point(462, 300);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(177, 47);
            this.Btn_Connect.TabIndex = 5;
            this.Btn_Connect.Text = "Connect";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // Btn_ShareScreen
            // 
            this.Btn_ShareScreen.Location = new System.Drawing.Point(268, 371);
            this.Btn_ShareScreen.Name = "Btn_ShareScreen";
            this.Btn_ShareScreen.Size = new System.Drawing.Size(177, 47);
            this.Btn_ShareScreen.TabIndex = 6;
            this.Btn_ShareScreen.Text = "Share Screen";
            this.Btn_ShareScreen.UseVisualStyleBackColor = true;
            this.Btn_ShareScreen.Click += new System.EventHandler(this.Btn_ShareScreen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Your IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Your Partner IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_ShareScreen);
            this.Controls.Add(this.Btn_Connect);
            this.Controls.Add(this.Btn_OpenConnect);
            this.Controls.Add(this.txtPartnerPort);
            this.Controls.Add(this.txtPartnerIP);
            this.Controls.Add(this.txtMyPort);
            this.Controls.Add(this.txtMyIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMyIP;
        private System.Windows.Forms.TextBox txtMyPort;
        private System.Windows.Forms.TextBox txtPartnerIP;
        private System.Windows.Forms.TextBox txtPartnerPort;
        private System.Windows.Forms.Button Btn_OpenConnect;
        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.Button Btn_ShareScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}

