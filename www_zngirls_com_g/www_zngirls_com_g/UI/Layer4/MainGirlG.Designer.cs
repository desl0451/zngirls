namespace www_zngirls_com_g
{
    partial class MainGirlG
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.listView1 = new System.Windows.Forms.ListView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textBox2 = new DevExpress.XtraEditors.TextEdit();
            this.textBox3 = new DevExpress.XtraEditors.TextEdit();
            this.textBox4 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.groupControl1.Controls.Add(this.listView1);
            this.groupControl1.Location = new System.Drawing.Point(36, 182);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(846, 223);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "进度列表";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(16, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(810, 171);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(794, 135);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 27);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "终止";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(794, 79);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(87, 27);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "停止";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(794, 24);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(87, 27);
            this.simpleButton3.TabIndex = 12;
            this.simpleButton3.Text = "下载";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(52, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "保存位置";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(52, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "开始编号";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(52, 140);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "结束编号";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(125, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(647, 20);
            this.textBox2.TabIndex = 14;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(125, 80);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(647, 20);
            this.textBox3.TabIndex = 15;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(125, 139);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(647, 20);
            this.textBox4.TabIndex = 16;
            // 
            // MainGirlG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 420);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.Name = "MainGirlG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "美女写真图集http://www.zngirls.com/g/";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainGirlG_FormClosing);
            this.Load += new System.EventHandler(this.MainGirlG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBox2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ListView listView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textBox2;
        private DevExpress.XtraEditors.TextEdit textBox3;
        private DevExpress.XtraEditors.TextEdit textBox4;
    }
}