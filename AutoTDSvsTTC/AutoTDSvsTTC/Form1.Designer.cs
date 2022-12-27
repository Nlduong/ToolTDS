namespace AutoTDSvsTTC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbControlMain = new System.Windows.Forms.TabControl();
            this.TDSControl = new System.Windows.Forms.TabPage();
            this.dataGridTDS = new System.Windows.Forms.DataGridView();
            this.txtTokenTDS = new System.Windows.Forms.TextBox();
            this.txtPassTDS = new System.Windows.Forms.TextBox();
            this.txtIdTDS = new System.Windows.Forms.TextBox();
            this.txtpassGoogle = new System.Windows.Forms.TextBox();
            this.txtIdGoogle = new System.Windows.Forms.TextBox();
            this.btnThemTKTDS = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TTCControl = new System.Windows.Forms.TabPage();
            this.lblTime = new System.Windows.Forms.Label();
            this.tbControlMain.SuspendLayout();
            this.TDSControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTDS)).BeginInit();
            this.SuspendLayout();
            // 
            // tbControlMain
            // 
            this.tbControlMain.Controls.Add(this.TDSControl);
            this.tbControlMain.Controls.Add(this.TTCControl);
            this.tbControlMain.Location = new System.Drawing.Point(0, 222);
            this.tbControlMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbControlMain.Name = "tbControlMain";
            this.tbControlMain.SelectedIndex = 0;
            this.tbControlMain.Size = new System.Drawing.Size(1539, 399);
            this.tbControlMain.TabIndex = 0;
            // 
            // TDSControl
            // 
            this.TDSControl.Controls.Add(this.dataGridTDS);
            this.TDSControl.Controls.Add(this.txtTokenTDS);
            this.TDSControl.Controls.Add(this.txtPassTDS);
            this.TDSControl.Controls.Add(this.txtIdTDS);
            this.TDSControl.Controls.Add(this.txtpassGoogle);
            this.TDSControl.Controls.Add(this.txtIdGoogle);
            this.TDSControl.Controls.Add(this.btnThemTKTDS);
            this.TDSControl.Controls.Add(this.label5);
            this.TDSControl.Controls.Add(this.label4);
            this.TDSControl.Controls.Add(this.label3);
            this.TDSControl.Controls.Add(this.label2);
            this.TDSControl.Controls.Add(this.label1);
            this.TDSControl.Location = new System.Drawing.Point(4, 25);
            this.TDSControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TDSControl.Name = "TDSControl";
            this.TDSControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TDSControl.Size = new System.Drawing.Size(1531, 370);
            this.TDSControl.TabIndex = 0;
            this.TDSControl.Text = "TDS Control";
            this.TDSControl.UseVisualStyleBackColor = true;
            // 
            // dataGridTDS
            // 
            this.dataGridTDS.AllowUserToAddRows = false;
            this.dataGridTDS.AllowUserToDeleteRows = false;
            this.dataGridTDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTDS.Location = new System.Drawing.Point(0, 154);
            this.dataGridTDS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridTDS.Name = "dataGridTDS";
            this.dataGridTDS.ReadOnly = true;
            this.dataGridTDS.RowHeadersWidth = 51;
            this.dataGridTDS.Size = new System.Drawing.Size(1528, 209);
            this.dataGridTDS.TabIndex = 11;
            this.dataGridTDS.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTDS_CellContentClick);
            this.dataGridTDS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridTDS_MouseClick);
            // 
            // txtTokenTDS
            // 
            this.txtTokenTDS.Location = new System.Drawing.Point(1057, 31);
            this.txtTokenTDS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTokenTDS.Name = "txtTokenTDS";
            this.txtTokenTDS.Size = new System.Drawing.Size(156, 22);
            this.txtTokenTDS.TabIndex = 10;
            // 
            // txtPassTDS
            // 
            this.txtPassTDS.Location = new System.Drawing.Point(783, 31);
            this.txtPassTDS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassTDS.Name = "txtPassTDS";
            this.txtPassTDS.Size = new System.Drawing.Size(151, 22);
            this.txtPassTDS.TabIndex = 9;
            // 
            // txtIdTDS
            // 
            this.txtIdTDS.Location = new System.Drawing.Point(547, 31);
            this.txtIdTDS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdTDS.Name = "txtIdTDS";
            this.txtIdTDS.Size = new System.Drawing.Size(132, 22);
            this.txtIdTDS.TabIndex = 8;
            // 
            // txtpassGoogle
            // 
            this.txtpassGoogle.Location = new System.Drawing.Point(329, 31);
            this.txtpassGoogle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtpassGoogle.Name = "txtpassGoogle";
            this.txtpassGoogle.Size = new System.Drawing.Size(132, 22);
            this.txtpassGoogle.TabIndex = 7;
            // 
            // txtIdGoogle
            // 
            this.txtIdGoogle.Location = new System.Drawing.Point(89, 30);
            this.txtIdGoogle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdGoogle.Name = "txtIdGoogle";
            this.txtIdGoogle.Size = new System.Drawing.Size(132, 22);
            this.txtIdGoogle.TabIndex = 6;
            // 
            // btnThemTKTDS
            // 
            this.btnThemTKTDS.Location = new System.Drawing.Point(1319, 27);
            this.btnThemTKTDS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThemTKTDS.Name = "btnThemTKTDS";
            this.btnThemTKTDS.Size = new System.Drawing.Size(185, 28);
            this.btnThemTKTDS.TabIndex = 5;
            this.btnThemTKTDS.Text = "Thêm Tài khoản";
            this.btnThemTKTDS.UseVisualStyleBackColor = true;
            this.btnThemTKTDS.Click += new System.EventHandler(this.btnThemTKTDS_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(959, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Token TDS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(701, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pass TDS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Id TDS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pass Google";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id Google";
            // 
            // TTCControl
            // 
            this.TTCControl.Location = new System.Drawing.Point(4, 25);
            this.TTCControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TTCControl.Name = "TTCControl";
            this.TTCControl.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TTCControl.Size = new System.Drawing.Size(1531, 370);
            this.TTCControl.TabIndex = 1;
            this.TTCControl.Text = "TTC Control";
            this.TTCControl.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(90, 34);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(38, 16);
            this.lblTime.TabIndex = 12;
            this.lblTime.Text = "Time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1541, 624);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.tbControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "AutoTDS_TCC";
            this.tbControlMain.ResumeLayout(false);
            this.TDSControl.ResumeLayout(false);
            this.TDSControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbControlMain;
        private System.Windows.Forms.TabPage TDSControl;
        private System.Windows.Forms.TabPage TTCControl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThemTKTDS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTokenTDS;
        private System.Windows.Forms.TextBox txtPassTDS;
        private System.Windows.Forms.TextBox txtIdTDS;
        private System.Windows.Forms.TextBox txtpassGoogle;
        private System.Windows.Forms.TextBox txtIdGoogle;
        private System.Windows.Forms.DataGridView dataGridTDS;
        private System.Windows.Forms.Label lblTime;
    }
}

