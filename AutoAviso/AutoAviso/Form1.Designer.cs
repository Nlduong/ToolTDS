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
            this.dataGridProfitcent = new System.Windows.Forms.DataGridView();
            this.chkLikeSeoVideo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkViewRuVideo = new System.Windows.Forms.CheckBox();
            this.chkViewYoutube = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTrafficAviso = new System.Windows.Forms.CheckBox();
            this.chkViewYTBAviso = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkviewVideoPro = new System.Windows.Forms.CheckBox();
            this.chkRuVideoPro = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAviso)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSEO)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProfitcent)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 196);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1104, 261);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridAviso);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
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
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
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
            this.dataGridSEO.Margin = new System.Windows.Forms.Padding(2);
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
            this.tabPage3.Controls.Add(this.dataGridProfitcent);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(1096, 235);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Profitcentr";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridProfitcent
            // 
            this.dataGridProfitcent.AllowUserToAddRows = false;
            this.dataGridProfitcent.AllowUserToDeleteRows = false;
            this.dataGridProfitcent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProfitcent.Location = new System.Drawing.Point(0, 34);
            this.dataGridProfitcent.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridProfitcent.Name = "dataGridProfitcent";
            this.dataGridProfitcent.ReadOnly = true;
            this.dataGridProfitcent.RowHeadersWidth = 51;
            this.dataGridProfitcent.RowTemplate.Height = 24;
            this.dataGridProfitcent.Size = new System.Drawing.Size(1070, 115);
            this.dataGridProfitcent.TabIndex = 1;
            this.dataGridProfitcent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridProfitcent_CellContentClick);
            this.dataGridProfitcent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridProfitcent_MouseClick);
            // 
            // chkLikeSeoVideo
            // 
            this.chkLikeSeoVideo.AutoSize = true;
            this.chkLikeSeoVideo.Location = new System.Drawing.Point(16, 17);
            this.chkLikeSeoVideo.Margin = new System.Windows.Forms.Padding(2);
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
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(218, 153);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SEO FAST";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTrafficAviso);
            this.groupBox2.Controls.Add(this.chkViewYTBAviso);
            this.groupBox2.Location = new System.Drawing.Point(41, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(218, 153);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aviso";
            // 
            // chkTrafficAviso
            // 
            this.chkTrafficAviso.AutoSize = true;
            this.chkTrafficAviso.Location = new System.Drawing.Point(16, 38);
            this.chkTrafficAviso.Margin = new System.Windows.Forms.Padding(2);
            this.chkTrafficAviso.Name = "chkTrafficAviso";
            this.chkTrafficAviso.Size = new System.Drawing.Size(56, 17);
            this.chkTrafficAviso.TabIndex = 4;
            this.chkTrafficAviso.Text = "Traffic";
            this.chkTrafficAviso.UseVisualStyleBackColor = true;
            // 
            // chkViewYTBAviso
            // 
            this.chkViewYTBAviso.AutoSize = true;
            this.chkViewYTBAviso.Location = new System.Drawing.Point(16, 17);
            this.chkViewYTBAviso.Margin = new System.Windows.Forms.Padding(2);
            this.chkViewYTBAviso.Name = "chkViewYTBAviso";
            this.chkViewYTBAviso.Size = new System.Drawing.Size(79, 17);
            this.chkViewYTBAviso.TabIndex = 2;
            this.chkViewYTBAviso.Text = "View Video";
            this.chkViewYTBAviso.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkRuVideoPro);
            this.groupBox3.Controls.Add(this.chkviewVideoPro);
            this.groupBox3.Location = new System.Drawing.Point(676, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(218, 153);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Profitcent";
            // 
            // chkviewVideoPro
            // 
            this.chkviewVideoPro.AutoSize = true;
            this.chkviewVideoPro.Location = new System.Drawing.Point(16, 17);
            this.chkviewVideoPro.Margin = new System.Windows.Forms.Padding(2);
            this.chkviewVideoPro.Name = "chkviewVideoPro";
            this.chkviewVideoPro.Size = new System.Drawing.Size(79, 17);
            this.chkviewVideoPro.TabIndex = 2;
            this.chkviewVideoPro.Text = "View Video";
            this.chkviewVideoPro.UseVisualStyleBackColor = true;
            // 
            // chkRuVideoPro
            // 
            this.chkRuVideoPro.AutoSize = true;
            this.chkRuVideoPro.Location = new System.Drawing.Point(16, 38);
            this.chkRuVideoPro.Margin = new System.Windows.Forms.Padding(2);
            this.chkRuVideoPro.Name = "chkRuVideoPro";
            this.chkRuVideoPro.Size = new System.Drawing.Size(96, 17);
            this.chkRuVideoPro.TabIndex = 3;
            this.chkRuVideoPro.Text = "View Ru Video";
            this.chkRuVideoPro.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 466);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAviso)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSEO)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProfitcent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridAviso;
        private System.Windows.Forms.DataGridView dataGridSEO;
        private System.Windows.Forms.CheckBox chkLikeSeoVideo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkViewRuVideo;
        private System.Windows.Forms.CheckBox chkViewYoutube;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkTrafficAviso;
        private System.Windows.Forms.CheckBox chkViewYTBAviso;
        private System.Windows.Forms.DataGridView dataGridProfitcent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkviewVideoPro;
        private System.Windows.Forms.CheckBox chkRuVideoPro;
    }
}

