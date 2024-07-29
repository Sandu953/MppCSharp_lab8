
namespace client
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLogin = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(277, 281);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(216, 59);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(277, 135);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(216, 31);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(162, 138);
            label1.Name = "label1";
            label1.Size = new Size(100, 25);
            label1.TabIndex = 3;
            label1.Text = "Username: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 192);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 4;
            label2.Text = "Password:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(277, 192);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(216, 31);
            textBox2.TabIndex = 5;
            textBox2.UseSystemPasswordChar = true;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 450);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnLogin);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Button btnLogin;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
    }
}
