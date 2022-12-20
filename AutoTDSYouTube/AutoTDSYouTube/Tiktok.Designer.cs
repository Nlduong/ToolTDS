namespace AutoTDSYouTube
{
    partial class Tiktok
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblWaitingTime = new System.Windows.Forms.Label();
            this.lblXu = new System.Windows.Forms.Label();
            this.txtMaxJob = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkTKsub = new System.Windows.Forms.CheckBox();
            this.chkCommentTT = new System.Windows.Forms.CheckBox();
            this.chkLikeTT = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(853, 9);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(708, 9);
            this.lblNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(69, 16);
            this.lblNumber.TabIndex = 4;
            this.lblNumber.Text = "lblNumber";
            // 
            // lblWaitingTime
            // 
            this.lblWaitingTime.AutoSize = true;
            this.lblWaitingTime.Location = new System.Drawing.Point(854, 127);
            this.lblWaitingTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWaitingTime.Name = "lblWaitingTime";
            this.lblWaitingTime.Size = new System.Drawing.Size(97, 16);
            this.lblWaitingTime.TabIndex = 6;
            this.lblWaitingTime.Text = "lblWaitingTime";
            // 
            // lblXu
            // 
            this.lblXu.AutoSize = true;
            this.lblXu.Location = new System.Drawing.Point(854, 161);
            this.lblXu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXu.Name = "lblXu";
            this.lblXu.Size = new System.Drawing.Size(14, 16);
            this.lblXu.TabIndex = 7;
            this.lblXu.Text = "0";
            // 
            // txtMaxJob
            // 
            this.txtMaxJob.Location = new System.Drawing.Point(601, 9);
            this.txtMaxJob.Name = "txtMaxJob";
            this.txtMaxJob.Size = new System.Drawing.Size(100, 22);
            this.txtMaxJob.TabIndex = 8;
            this.txtMaxJob.Text = "100";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(1, 254);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(956, 141);
            this.dataGridView1.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "#";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nhiệm vụ";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "LinkYouTube";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 225;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Trạng Thái";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 125;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(853, 57);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 44);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkTKsub
            // 
            this.chkTKsub.AutoSize = true;
            this.chkTKsub.Checked = true;
            this.chkTKsub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTKsub.Location = new System.Drawing.Point(21, 22);
            this.chkTKsub.Name = "chkTKsub";
            this.chkTKsub.Size = new System.Drawing.Size(136, 20);
            this.chkTKsub.TabIndex = 11;
            this.chkTKsub.Text = "Subscride TikTok";
            this.chkTKsub.UseVisualStyleBackColor = true;
            // 
            // chkCommentTT
            // 
            this.chkCommentTT.AutoSize = true;
            this.chkCommentTT.Checked = true;
            this.chkCommentTT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommentTT.Location = new System.Drawing.Point(21, 48);
            this.chkCommentTT.Name = "chkCommentTT";
            this.chkCommentTT.Size = new System.Drawing.Size(132, 20);
            this.chkCommentTT.TabIndex = 12;
            this.chkCommentTT.Text = "Comment TikTok";
            this.chkCommentTT.UseVisualStyleBackColor = true;
            // 
            // chkLikeTT
            // 
            this.chkLikeTT.AutoSize = true;
            this.chkLikeTT.Checked = true;
            this.chkLikeTT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLikeTT.Location = new System.Drawing.Point(21, 74);
            this.chkLikeTT.Name = "chkLikeTT";
            this.chkLikeTT.Size = new System.Drawing.Size(100, 20);
            this.chkLikeTT.TabIndex = 13;
            this.chkLikeTT.Text = "Like TikTok";
            this.chkLikeTT.UseVisualStyleBackColor = true;
            // 
            // Tiktok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 450);
            this.Controls.Add(this.chkLikeTT);
            this.Controls.Add(this.chkCommentTT);
            this.Controls.Add(this.chkTKsub);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtMaxJob);
            this.Controls.Add(this.lblXu);
            this.Controls.Add(this.lblWaitingTime);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Tiktok";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblWaitingTime;
        private System.Windows.Forms.Label lblXu;
        private System.Windows.Forms.TextBox txtMaxJob;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.CheckBox chkTKsub;
        private System.Windows.Forms.CheckBox chkCommentTT;
        private System.Windows.Forms.CheckBox chkLikeTT;
    }
}

