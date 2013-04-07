namespace Snake
{
    partial class FormMain
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.butonStartGame = new System.Windows.Forms.Button();
            this.buttonStartChat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(629, 29);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(288, 213);
            this.txtLog.TabIndex = 4;
            // 
            // butonStartGame
            // 
            this.butonStartGame.Location = new System.Drawing.Point(629, 327);
            this.butonStartGame.Name = "butonStartGame";
            this.butonStartGame.Size = new System.Drawing.Size(75, 23);
            this.butonStartGame.TabIndex = 5;
            this.butonStartGame.Text = "Start Game";
            this.butonStartGame.UseVisualStyleBackColor = true;
            this.butonStartGame.Click += new System.EventHandler(this.buttonStartGame);
            // 
            // buttonStartChat
            // 
            this.buttonStartChat.Location = new System.Drawing.Point(722, 327);
            this.buttonStartChat.Name = "buttonStartChat";
            this.buttonStartChat.Size = new System.Drawing.Size(75, 23);
            this.buttonStartChat.TabIndex = 6;
            this.buttonStartChat.Text = "Start Chat";
            this.buttonStartChat.UseVisualStyleBackColor = true;
            this.buttonStartChat.Click += new System.EventHandler(this.buttonStartChat_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 622);
            this.Controls.Add(this.buttonStartChat);
            this.Controls.Add(this.butonStartGame);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SnakeServer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button butonStartGame;
        private System.Windows.Forms.Button buttonStartChat;
    }
}
