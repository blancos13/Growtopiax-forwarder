
namespace Growtopiax
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.userBox = new DarkUI.Controls.DarkTextBox();
            this.passBox = new DarkUI.Controls.DarkTextBox();
            this.darkLabel1 = new DarkUI.Controls.DarkLabel();
            this.darkLabel2 = new DarkUI.Controls.DarkLabel();
            this.loginB = new DarkUI.Controls.DarkButton();
            this.regB = new DarkUI.Controls.DarkButton();
            this.upperRgb = new System.Windows.Forms.Panel();
            this.rgb = new System.Windows.Forms.Timer(this.components);
            this.rgb19 = new System.Windows.Forms.Timer(this.components);
            this.minimizeB = new System.Windows.Forms.Button();
            this.exitB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userBox
            // 
            this.userBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.userBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.userBox, "userBox");
            this.userBox.Name = "userBox";
            this.userBox.TextChanged += new System.EventHandler(this.userBox_TextChanged);
            // 
            // passBox
            // 
            this.passBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.passBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.passBox, "passBox");
            this.passBox.Name = "passBox";
            // 
            // darkLabel1
            // 
            resources.ApplyResources(this.darkLabel1, "darkLabel1");
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Click += new System.EventHandler(this.darkLabel1_Click);
            // 
            // darkLabel2
            // 
            resources.ApplyResources(this.darkLabel2, "darkLabel2");
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Name = "darkLabel2";
            // 
            // loginB
            // 
            resources.ApplyResources(this.loginB, "loginB");
            this.loginB.Name = "loginB";
            this.loginB.Click += new System.EventHandler(this.loginB_Click_1);
            // 
            // regB
            // 
            resources.ApplyResources(this.regB, "regB");
            this.regB.Name = "regB";
            this.regB.Click += new System.EventHandler(this.regB_Click_1);
            // 
            // upperRgb
            // 
            resources.ApplyResources(this.upperRgb, "upperRgb");
            this.upperRgb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.upperRgb.Name = "upperRgb";
            this.upperRgb.Paint += new System.Windows.Forms.PaintEventHandler(this.upperRgb_Paint);
            // 
            // rgb
            // 
            this.rgb.Enabled = true;
            this.rgb.Interval = 3;
            this.rgb.Tick += new System.EventHandler(this.rgb_Tick_1);
            // 
            // minimizeB
            // 
            this.minimizeB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            resources.ApplyResources(this.minimizeB, "minimizeB");
            this.minimizeB.FlatAppearance.BorderSize = 0;
            this.minimizeB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.minimizeB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.minimizeB.ForeColor = System.Drawing.Color.White;
            this.minimizeB.Name = "minimizeB";
            this.minimizeB.UseVisualStyleBackColor = false;
            this.minimizeB.Click += new System.EventHandler(this.minimizeB_Click_1);
            // 
            // exitB
            // 
            this.exitB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            resources.ApplyResources(this.exitB, "exitB");
            this.exitB.FlatAppearance.BorderSize = 0;
            this.exitB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exitB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.exitB.ForeColor = System.Drawing.Color.White;
            this.exitB.Name = "exitB";
            this.exitB.UseVisualStyleBackColor = false;
            this.exitB.Click += new System.EventHandler(this.exitB_Click_1);
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.Controls.Add(this.exitB);
            this.Controls.Add(this.minimizeB);
            this.Controls.Add(this.upperRgb);
            this.Controls.Add(this.regB);
            this.Controls.Add(this.loginB);
            this.Controls.Add(this.darkLabel2);
            this.Controls.Add(this.darkLabel1);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.userBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkTextBox userBox;
        private DarkUI.Controls.DarkTextBox passBox;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private DarkUI.Controls.DarkLabel darkLabel2;
        private DarkUI.Controls.DarkButton loginB;
        private DarkUI.Controls.DarkButton regB;
        private System.Windows.Forms.Panel upperRgb;
        private System.Windows.Forms.Timer rgb;
        private System.Windows.Forms.Timer rgb19;
        private System.Windows.Forms.Button minimizeB;
        private System.Windows.Forms.Button exitB;
    }
}