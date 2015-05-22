namespace SmartBox.Console.Install
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbSmartBox = new System.Windows.Forms.TextBox();
            this.tbSmartBoxApp = new System.Windows.Forms.TextBox();
            this.tbSmartBoxAppOut = new System.Windows.Forms.TextBox();
            this.btnInstall = new System.Windows.Forms.Button();
            this.tbDataSource = new System.Windows.Forms.TextBox();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbSmartBox
            // 
            this.tbSmartBox.Location = new System.Drawing.Point(252, 12);
            this.tbSmartBox.Name = "tbSmartBox";
            this.tbSmartBox.Size = new System.Drawing.Size(118, 21);
            this.tbSmartBox.TabIndex = 0;
            this.tbSmartBox.Text = "SmartBox";
            // 
            // tbSmartBoxApp
            // 
            this.tbSmartBoxApp.Location = new System.Drawing.Point(252, 39);
            this.tbSmartBoxApp.Name = "tbSmartBoxApp";
            this.tbSmartBoxApp.Size = new System.Drawing.Size(118, 21);
            this.tbSmartBoxApp.TabIndex = 1;
            this.tbSmartBoxApp.Text = "SmartBoxApp";
            // 
            // tbSmartBoxAppOut
            // 
            this.tbSmartBoxAppOut.Location = new System.Drawing.Point(252, 66);
            this.tbSmartBoxAppOut.Name = "tbSmartBoxAppOut";
            this.tbSmartBoxAppOut.Size = new System.Drawing.Size(118, 21);
            this.tbSmartBoxAppOut.TabIndex = 2;
            this.tbSmartBoxAppOut.Text = "SmartBoxAppOut";
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(376, 147);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 3;
            this.btnInstall.Text = "初始化数据";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // tbDataSource
            // 
            this.tbDataSource.Location = new System.Drawing.Point(252, 93);
            this.tbDataSource.Name = "tbDataSource";
            this.tbDataSource.Size = new System.Drawing.Size(118, 21);
            this.tbDataSource.TabIndex = 5;
            this.tbDataSource.Text = "192.168.200.146";
            // 
            // tbUID
            // 
            this.tbUID.Location = new System.Drawing.Point(252, 120);
            this.tbUID.Name = "tbUID";
            this.tbUID.Size = new System.Drawing.Size(118, 21);
            this.tbUID.TabIndex = 6;
            this.tbUID.Text = "sa";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(252, 147);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(118, 21);
            this.tbPassword.TabIndex = 7;
            this.tbPassword.Text = "123456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "数据库SmartBox名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "数据库SmartBoxApp名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "数据库SmartBoxAppOut名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "服务器IP：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(211, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "UID：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "PASSWORD：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 189);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUID);
            this.Controls.Add(this.tbDataSource);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.tbSmartBoxAppOut);
            this.Controls.Add(this.tbSmartBoxApp);
            this.Controls.Add(this.tbSmartBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "控制台安装程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSmartBox;
        private System.Windows.Forms.TextBox tbSmartBoxApp;
        private System.Windows.Forms.TextBox tbSmartBoxAppOut;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.TextBox tbDataSource;
        private System.Windows.Forms.TextBox tbUID;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

