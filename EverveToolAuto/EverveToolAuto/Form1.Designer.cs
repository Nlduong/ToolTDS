namespace EverveToolAuto
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFailJobTT = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.chkLikeYouTube = new System.Windows.Forms.CheckBox();
            this.chkViewYoutube = new System.Windows.Forms.CheckBox();
            this.txtLikeYTBMaxPer = new System.Windows.Forms.TextBox();
            this.txtSubYTBMaxPer = new System.Windows.Forms.TextBox();
            this.chkTrafficView = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tổng job thất bại";
            // 
            // txtFailJobTT
            // 
            this.txtFailJobTT.Location = new System.Drawing.Point(227, 126);
            this.txtFailJobTT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFailJobTT.Name = "txtFailJobTT";
            this.txtFailJobTT.Size = new System.Drawing.Size(67, 22);
            this.txtFailJobTT.TabIndex = 3;
            this.txtFailJobTT.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTrafficView);
            this.groupBox2.Controls.Add(this.txtSubYTBMaxPer);
            this.groupBox2.Controls.Add(this.txtLikeYTBMaxPer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtFailJobTT);
            this.groupBox2.Controls.Add(this.chkViewYoutube);
            this.groupBox2.Controls.Add(this.chkLikeYouTube);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(339, 203);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cấu hình YouTube";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 340);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.Size = new System.Drawing.Size(1523, 294);
            this.dataGrid.TabIndex = 8;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseClick);
            // 
            // chkLikeYouTube
            // 
            this.chkLikeYouTube.AutoSize = true;
            this.chkLikeYouTube.Checked = true;
            this.chkLikeYouTube.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLikeYouTube.Location = new System.Drawing.Point(23, 27);
            this.chkLikeYouTube.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkLikeYouTube.Name = "chkLikeYouTube";
            this.chkLikeYouTube.Size = new System.Drawing.Size(107, 20);
            this.chkLikeYouTube.TabIndex = 3;
            this.chkLikeYouTube.Text = "Like Youtube";
            this.chkLikeYouTube.UseVisualStyleBackColor = true;
            // 
            // chkViewYoutube
            // 
            this.chkViewYoutube.AutoSize = true;
            this.chkViewYoutube.Checked = true;
            this.chkViewYoutube.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewYoutube.Location = new System.Drawing.Point(23, 57);
            this.chkViewYoutube.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkViewYoutube.Name = "chkViewYoutube";
            this.chkViewYoutube.Size = new System.Drawing.Size(117, 20);
            this.chkViewYoutube.TabIndex = 4;
            this.chkViewYoutube.Text = "View YouTube";
            this.chkViewYoutube.UseVisualStyleBackColor = true;
            // 
            // txtLikeYTBMaxPer
            // 
            this.txtLikeYTBMaxPer.Location = new System.Drawing.Point(211, 25);
            this.txtLikeYTBMaxPer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLikeYTBMaxPer.Name = "txtLikeYTBMaxPer";
            this.txtLikeYTBMaxPer.Size = new System.Drawing.Size(35, 22);
            this.txtLikeYTBMaxPer.TabIndex = 6;
            this.txtLikeYTBMaxPer.Text = "25";
            // 
            // txtSubYTBMaxPer
            // 
            this.txtSubYTBMaxPer.Location = new System.Drawing.Point(211, 57);
            this.txtSubYTBMaxPer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSubYTBMaxPer.Name = "txtSubYTBMaxPer";
            this.txtSubYTBMaxPer.Size = new System.Drawing.Size(35, 22);
            this.txtSubYTBMaxPer.TabIndex = 7;
            this.txtSubYTBMaxPer.Text = "20";
            // 
            // chkTrafficView
            // 
            this.chkTrafficView.AutoSize = true;
            this.chkTrafficView.Checked = true;
            this.chkTrafficView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrafficView.Location = new System.Drawing.Point(23, 90);
            this.chkTrafficView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkTrafficView.Name = "chkTrafficView";
            this.chkTrafficView.Size = new System.Drawing.Size(98, 20);
            this.chkTrafficView.TabIndex = 8;
            this.chkTrafficView.Text = "Traffic View";
            this.chkTrafficView.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1577, 666);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFailJobTT;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.CheckBox chkTrafficView;
        private System.Windows.Forms.TextBox txtSubYTBMaxPer;
        private System.Windows.Forms.TextBox txtLikeYTBMaxPer;
        private System.Windows.Forms.CheckBox chkViewYoutube;
        private System.Windows.Forms.CheckBox chkLikeYouTube;
    }
}

