namespace AutoAviso
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridAviso = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridSEO = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.chkLikeSeoVideo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkViewYoutube = new System.Windows.Forms.CheckBox();
            this.chkViewRuVideo = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAviso)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSEO)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 196);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1104, 261);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridAviso);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(1096, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Aviso";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridAviso
            // 
            this.dataGridAviso.AllowUserToAddRows = false;
            this.dataGridAviso.AllowUserToDeleteRows = false;
            this.dataGridAviso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAviso.Location = new System.Drawing.Point(18, 25);
            this.dataGridAviso.Name = "dataGridAviso";
            this.dataGridAviso.ReadOnly = true;
            this.dataGridAviso.RowHeadersWidth = 51;
            this.dataGridAviso.Size = new System.Drawing.Size(1056, 189);
            this.dataGridAviso.TabIndex = 9;
            this.dataGridAviso.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAviso_CellContentClick);
            this.dataGridAviso.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridAviso_MouseClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridSEO);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(1096, 235);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SEO";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridSEO
            // 
            this.dataGridSEO.AllowUserToAddRows = false;
            this.dataGridSEO.AllowUserToDeleteRows = false;
            this.dataGridSEO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSEO.Location = new System.Drawing.Point(14, 21);
            this.dataGridSEO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridSEO.Name = "dataGridSEO";
            this.dataGridSEO.ReadOnly = true;
            this.dataGridSEO.RowHeadersWidth = 51;
            this.dataGridSEO.RowTemplate.Height = 24;
            this.dataGridSEO.Size = new System.Drawing.Size(1070, 115);
            this.dataGridSEO.TabIndex = 0;
            this.dataGridSEO.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSEO_CellContentClick);
            this.dataGridSEO.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridSEO_MouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Size = new System.Drawing.Size(1096, 235);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkLikeSeoVideo
            // 
            this.chkLikeSeoVideo.AutoSize = true;
            this.chkLikeSeoVideo.Location = new System.Drawing.Point(16, 17);
            this.chkLikeSeoVideo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkLikeSeoVideo.Name = "chkLikeSeoVideo";
            this.chkLikeSeoVideo.Size = new System.Drawing.Size(76, 17);
            this.chkLikeSeoVideo.TabIndex = 2;
            this.chkLikeSeoVideo.Text = "Like Video";
            this.chkLikeSeoVideo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkViewRuVideo);
            this.groupBox1.Controls.Add(this.chkViewYoutube);
            this.groupBox1.Controls.Add(this.chkLikeSeoVideo);
            this.groupBox1.Location = new System.Drawing.Point(362, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(218, 153);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SEO FAST";
            // 
            // chkViewYoutube
            // 
            this.chkViewYoutube.AutoSize = true;
            this.chkViewYoutube.Location = new System.Drawing.Point(16, 59);
            this.chkViewYoutube.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewYoutube.Name = "chkViewYoutube";
            this.chkViewYoutube.Size = new System.Drawing.Size(92, 17);
            this.chkViewYoutube.TabIndex = 3;
            this.chkViewYoutube.Text = "View Youtube";
            this.chkViewYoutube.UseVisualStyleBackColor = true;
            // 
            // chkViewRuVideo
            // 
            this.chkViewRuVideo.AutoSize = true;
            this.chkViewRuVideo.Location = new System.Drawing.Point(16, 38);
            this.chkViewRuVideo.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewRuVideo.Name = "chkViewRuVideo";
            this.chkViewRuVideo.Size = new System.Drawing.Size(96, 17);
            this.chkViewRuVideo.TabIndex = 4;
            this.chkViewRuVideo.Text = "View Ru Video";
            this.chkViewRuVideo.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 466);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAviso)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSEO)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridAviso;
        private System.Windows.Forms.DataGridView dataGridSEO;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkLikeSeoVideo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkViewRuVideo;
        private System.Windows.Forms.CheckBox chkViewYoutube;
    }
}

