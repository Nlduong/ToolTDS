namespace CamelBTC
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.chkClaimGold = new System.Windows.Forms.CheckBox();
            this.chkClaimLog = new System.Windows.Forms.CheckBox();
            this.chkCheckMarket = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWaitTime = new System.Windows.Forms.TextBox();
            this.chkClaimRock = new System.Windows.Forms.CheckBox();
            this.chkClaimSteel = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(2, 185);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.Size = new System.Drawing.Size(1372, 233);
            this.dataGrid.TabIndex = 10;
            this.dataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellContentClick);
            // 
            // chkClaimGold
            // 
            this.chkClaimGold.AutoSize = true;
            this.chkClaimGold.Location = new System.Drawing.Point(87, 39);
            this.chkClaimGold.Name = "chkClaimGold";
            this.chkClaimGold.Size = new System.Drawing.Size(95, 20);
            this.chkClaimGold.TabIndex = 11;
            this.chkClaimGold.Text = "Claim Gold";
            this.chkClaimGold.UseVisualStyleBackColor = true;
            // 
            // chkClaimLog
            // 
            this.chkClaimLog.AutoSize = true;
            this.chkClaimLog.Location = new System.Drawing.Point(87, 65);
            this.chkClaimLog.Name = "chkClaimLog";
            this.chkClaimLog.Size = new System.Drawing.Size(89, 20);
            this.chkClaimLog.TabIndex = 12;
            this.chkClaimLog.Text = "Claim Log";
            this.chkClaimLog.UseVisualStyleBackColor = true;
            // 
            // chkCheckMarket
            // 
            this.chkCheckMarket.AutoSize = true;
            this.chkCheckMarket.Location = new System.Drawing.Point(243, 39);
            this.chkCheckMarket.Name = "chkCheckMarket";
            this.chkCheckMarket.Size = new System.Drawing.Size(111, 20);
            this.chkCheckMarket.TabIndex = 13;
            this.chkCheckMarket.Text = "Check Market";
            this.chkCheckMarket.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1129, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Time Waiting";
            // 
            // txtWaitTime
            // 
            this.txtWaitTime.Location = new System.Drawing.Point(1231, 111);
            this.txtWaitTime.Name = "txtWaitTime";
            this.txtWaitTime.Size = new System.Drawing.Size(100, 22);
            this.txtWaitTime.TabIndex = 14;
            this.txtWaitTime.Text = "200";
            // 
            // chkClaimRock
            // 
            this.chkClaimRock.AutoSize = true;
            this.chkClaimRock.Location = new System.Drawing.Point(87, 91);
            this.chkClaimRock.Name = "chkClaimRock";
            this.chkClaimRock.Size = new System.Drawing.Size(98, 20);
            this.chkClaimRock.TabIndex = 16;
            this.chkClaimRock.Text = "Claim Rock";
            this.chkClaimRock.UseVisualStyleBackColor = true;
            // 
            // chkClaimSteel
            // 
            this.chkClaimSteel.AutoSize = true;
            this.chkClaimSteel.Location = new System.Drawing.Point(87, 117);
            this.chkClaimSteel.Name = "chkClaimSteel";
            this.chkClaimSteel.Size = new System.Drawing.Size(97, 20);
            this.chkClaimSteel.TabIndex = 17;
            this.chkClaimSteel.Text = "Claim Steel";
            this.chkClaimSteel.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 450);
            this.Controls.Add(this.chkClaimSteel);
            this.Controls.Add(this.chkClaimRock);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWaitTime);
            this.Controls.Add(this.chkCheckMarket);
            this.Controls.Add(this.chkClaimLog);
            this.Controls.Add(this.chkClaimGold);
            this.Controls.Add(this.dataGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.CheckBox chkClaimGold;
        private System.Windows.Forms.CheckBox chkClaimLog;
        private System.Windows.Forms.CheckBox chkCheckMarket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWaitTime;
        private System.Windows.Forms.CheckBox chkClaimRock;
        private System.Windows.Forms.CheckBox chkClaimSteel;
    }
}

