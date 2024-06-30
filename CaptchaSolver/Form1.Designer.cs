namespace CaptchaSolver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            txtBotToken = new TextBox();
            label1 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(74, 199);
            button1.Name = "button1";
            button1.Size = new Size(308, 42);
            button1.TabIndex = 0;
            button1.Text = "Mulai";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtBotToken
            // 
            txtBotToken.Location = new Point(74, 155);
            txtBotToken.Name = "txtBotToken";
            txtBotToken.Size = new Size(308, 27);
            txtBotToken.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(93, 52);
            label1.Name = "label1";
            label1.Size = new Size(276, 25);
            label1.TabIndex = 2;
            label1.Text = "2CAPTCHA SOLVER TELEGRAM";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(74, 120);
            label4.Name = "label4";
            label4.Size = new Size(141, 20);
            label4.TabIndex = 5;
            label4.Text = "Token Bot Telegram";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(74, 261);
            label5.Name = "label5";
            label5.Size = new Size(122, 20);
            label5.TabIndex = 6;
            label5.Text = "Pendapatan $     :";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(200, 261);
            label6.Name = "label6";
            label6.Size = new Size(17, 20);
            label6.TabIndex = 7;
            label6.Text = "0";
            label6.Click += label6_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(451, 338);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(txtBotToken);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "2CAPTCHA SOLVER TELE";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox txtBotToken;
        private Label label1;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
