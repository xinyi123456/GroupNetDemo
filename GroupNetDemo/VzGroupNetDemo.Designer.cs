namespace GroupNetDemo
{
    partial class VzGroupNetDemo
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCloseDevice = new System.Windows.Forms.Button();
            this.btnOpenDevice = new System.Windows.Forms.Button();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxDeviceIP = new System.Windows.Forms.TextBox();
            this.labelPwd = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelDeviceIp = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.labelPlateResult = new System.Windows.Forms.Label();
            this.btnGroupOpen = new System.Windows.Forms.Button();
            this.btnGroupClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewOpenDevice = new System.Windows.Forms.ListView();
            this.btnstopplay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPlateResult1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPlateResult2 = new System.Windows.Forms.Label();
            this.lblentryimg = new System.Windows.Forms.Label();
            this.lblexitimg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(2, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(301, 252);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(309, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(302, 252);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox3.Location = new System.Drawing.Point(3, 301);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(301, 245);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox4.Location = new System.Drawing.Point(309, 301);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(302, 245);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCloseDevice);
            this.groupBox1.Controls.Add(this.btnOpenDevice);
            this.groupBox1.Controls.Add(this.textBoxPwd);
            this.groupBox1.Controls.Add(this.textBoxPort);
            this.groupBox1.Controls.Add(this.textBoxUserName);
            this.groupBox1.Controls.Add(this.textBoxDeviceIP);
            this.groupBox1.Controls.Add(this.labelPwd);
            this.groupBox1.Controls.Add(this.labelPort);
            this.groupBox1.Controls.Add(this.labelUserName);
            this.groupBox1.Controls.Add(this.labelDeviceIp);
            this.groupBox1.Location = new System.Drawing.Point(617, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 140);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // btnCloseDevice
            // 
            this.btnCloseDevice.Location = new System.Drawing.Point(146, 93);
            this.btnCloseDevice.Name = "btnCloseDevice";
            this.btnCloseDevice.Size = new System.Drawing.Size(49, 23);
            this.btnCloseDevice.TabIndex = 9;
            this.btnCloseDevice.Text = "关闭";
            this.btnCloseDevice.UseVisualStyleBackColor = true;
            this.btnCloseDevice.Click += new System.EventHandler(this.btnCloseDevice_Click);
            // 
            // btnOpenDevice
            // 
            this.btnOpenDevice.Location = new System.Drawing.Point(49, 93);
            this.btnOpenDevice.Name = "btnOpenDevice";
            this.btnOpenDevice.Size = new System.Drawing.Size(45, 23);
            this.btnOpenDevice.TabIndex = 8;
            this.btnOpenDevice.Text = "打开";
            this.btnOpenDevice.UseVisualStyleBackColor = true;
            this.btnOpenDevice.Click += new System.EventHandler(this.btnOpenDevice_Click);
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.Location = new System.Drawing.Point(176, 53);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.PasswordChar = '*';
            this.textBoxPwd.Size = new System.Drawing.Size(40, 21);
            this.textBoxPwd.TabIndex = 7;
            this.textBoxPwd.Text = "admin";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(176, 25);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(40, 21);
            this.textBoxPort.TabIndex = 6;
            this.textBoxPort.Text = "80";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(49, 54);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(89, 21);
            this.textBoxUserName.TabIndex = 5;
            this.textBoxUserName.Text = "admin";
            // 
            // textBoxDeviceIP
            // 
            this.textBoxDeviceIP.Location = new System.Drawing.Point(49, 25);
            this.textBoxDeviceIP.Name = "textBoxDeviceIP";
            this.textBoxDeviceIP.Size = new System.Drawing.Size(89, 21);
            this.textBoxDeviceIP.TabIndex = 4;
            this.textBoxDeviceIP.Text = "192.168.4.79";
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Location = new System.Drawing.Point(144, 54);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(35, 12);
            this.labelPwd.TabIndex = 3;
            this.labelPwd.Text = "密码:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(144, 28);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 12);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "端口:";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(8, 54);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(47, 12);
            this.labelUserName.TabIndex = 1;
            this.labelUserName.Text = "用户名:";
            // 
            // labelDeviceIp
            // 
            this.labelDeviceIp.AutoSize = true;
            this.labelDeviceIp.Location = new System.Drawing.Point(6, 28);
            this.labelDeviceIp.Name = "labelDeviceIp";
            this.labelDeviceIp.Size = new System.Drawing.Size(47, 12);
            this.labelDeviceIp.TabIndex = 0;
            this.labelDeviceIp.Text = "设备IP:";
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(617, 401);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(65, 27);
            this.btnOutput.TabIndex = 6;
            this.btnOutput.Text = "视频播放";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(780, 401);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(65, 27);
            this.btnManual.TabIndex = 7;
            this.btnManual.Text = "手动触发";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(2, 564);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(847, 194);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // labelPlateResult
            // 
            this.labelPlateResult.AutoSize = true;
            this.labelPlateResult.Location = new System.Drawing.Point(0, 549);
            this.labelPlateResult.Name = "labelPlateResult";
            this.labelPlateResult.Size = new System.Drawing.Size(101, 12);
            this.labelPlateResult.TabIndex = 9;
            this.labelPlateResult.Text = "组网车牌识别结果";
            // 
            // btnGroupOpen
            // 
            this.btnGroupOpen.Location = new System.Drawing.Point(617, 355);
            this.btnGroupOpen.Name = "btnGroupOpen";
            this.btnGroupOpen.Size = new System.Drawing.Size(109, 29);
            this.btnGroupOpen.TabIndex = 14;
            this.btnGroupOpen.Text = "开启接收组网结果";
            this.btnGroupOpen.UseVisualStyleBackColor = true;
            this.btnGroupOpen.Click += new System.EventHandler(this.btnGroupOpen_Click);
            // 
            // btnGroupClose
            // 
            this.btnGroupClose.Location = new System.Drawing.Point(736, 355);
            this.btnGroupClose.Name = "btnGroupClose";
            this.btnGroupClose.Size = new System.Drawing.Size(109, 28);
            this.btnGroupClose.TabIndex = 15;
            this.btnGroupClose.Text = "停止接收组网结果";
            this.btnGroupClose.UseVisualStyleBackColor = true;
            this.btnGroupClose.Click += new System.EventHandler(this.btnGroupClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(629, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "设备列表";
            // 
            // listViewOpenDevice
            // 
            this.listViewOpenDevice.HideSelection = false;
            this.listViewOpenDevice.Location = new System.Drawing.Point(617, 207);
            this.listViewOpenDevice.Name = "listViewOpenDevice";
            this.listViewOpenDevice.Size = new System.Drawing.Size(228, 114);
            this.listViewOpenDevice.TabIndex = 19;
            this.listViewOpenDevice.UseCompatibleStateImageBehavior = false;
            // 
            // btnstopplay
            // 
            this.btnstopplay.Location = new System.Drawing.Point(699, 401);
            this.btnstopplay.Name = "btnstopplay";
            this.btnstopplay.Size = new System.Drawing.Size(65, 27);
            this.btnstopplay.TabIndex = 20;
            this.btnstopplay.Text = "停止播放";
            this.btnstopplay.UseVisualStyleBackColor = true;
            this.btnstopplay.Click += new System.EventHandler(this.btnstopplay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "车牌识别结果:";
            // 
            // lblPlateResult1
            // 
            this.lblPlateResult1.AutoSize = true;
            this.lblPlateResult1.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblPlateResult1.Location = new System.Drawing.Point(90, 260);
            this.lblPlateResult1.Name = "lblPlateResult1";
            this.lblPlateResult1.Size = new System.Drawing.Size(0, 12);
            this.lblPlateResult1.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "车牌识别结果：";
            // 
            // lblPlateResult2
            // 
            this.lblPlateResult2.AutoSize = true;
            this.lblPlateResult2.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblPlateResult2.Location = new System.Drawing.Point(403, 260);
            this.lblPlateResult2.Name = "lblPlateResult2";
            this.lblPlateResult2.Size = new System.Drawing.Size(0, 12);
            this.lblPlateResult2.TabIndex = 24;
            // 
            // lblentryimg
            // 
            this.lblentryimg.AutoSize = true;
            this.lblentryimg.Location = new System.Drawing.Point(1, 286);
            this.lblentryimg.Name = "lblentryimg";
            this.lblentryimg.Size = new System.Drawing.Size(53, 12);
            this.lblentryimg.TabIndex = 26;
            this.lblentryimg.Text = "入口图片";
            // 
            // lblexitimg
            // 
            this.lblexitimg.AutoSize = true;
            this.lblexitimg.Location = new System.Drawing.Point(308, 286);
            this.lblexitimg.Name = "lblexitimg";
            this.lblexitimg.Size = new System.Drawing.Size(53, 12);
            this.lblexitimg.TabIndex = 27;
            this.lblexitimg.Text = "出口图片";
            // 
            // VzGroupNetDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 758);
            this.Controls.Add(this.lblexitimg);
            this.Controls.Add(this.lblentryimg);
            this.Controls.Add(this.lblPlateResult2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPlateResult1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnstopplay);
            this.Controls.Add(this.listViewOpenDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGroupClose);
            this.Controls.Add(this.btnGroupOpen);
            this.Controls.Add(this.labelPlateResult);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "VzGroupNetDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "组网Demo";
            this.Load += new System.EventHandler(this.VzGroupNetDemo_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VzGroupNetDemo_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelDeviceIp;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxDeviceIP;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Button btnOpenDevice;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button btnCloseDevice;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelPlateResult;
        private System.Windows.Forms.Button btnGroupOpen;
        private System.Windows.Forms.Button btnGroupClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewOpenDevice;
        private System.Windows.Forms.Button btnstopplay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPlateResult1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPlateResult2;
        private System.Windows.Forms.Label lblentryimg;
        private System.Windows.Forms.Label lblexitimg;
    }
}

