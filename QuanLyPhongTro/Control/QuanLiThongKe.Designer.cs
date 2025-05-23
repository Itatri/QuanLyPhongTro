﻿namespace QuanLyPhongTro.Control
{
    partial class QuanLiThongKe
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDT = new System.Windows.Forms.DataGridView();
            this.btnTK = new System.Windows.Forms.Button();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTK = new System.Windows.Forms.ComboBox();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDT
            // 
            this.dgvDT.AllowUserToAddRows = false;
            this.dgvDT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDT.Location = new System.Drawing.Point(53, 213);
            this.dgvDT.Name = "dgvDT";
            this.dgvDT.RowHeadersWidth = 51;
            this.dgvDT.RowTemplate.Height = 24;
            this.dgvDT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDT.Size = new System.Drawing.Size(1579, 733);
            this.dgvDT.TabIndex = 40;
            // 
            // btnTK
            // 
            this.btnTK.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTK.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTK.ForeColor = System.Drawing.Color.White;
            this.btnTK.Location = new System.Drawing.Point(1121, 151);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(100, 31);
            this.btnTK.TabIndex = 46;
            this.btnTK.Text = "Thống kê";
            this.btnTK.UseVisualStyleBackColor = false;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // cboNam
            // 
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(965, 150);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(124, 31);
            this.cboNam.TabIndex = 44;
            // 
            // cboThang
            // 
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(769, 150);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(124, 31);
            this.cboThang.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(908, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 19);
            this.label3.TabIndex = 42;
            this.label3.Text = "Năm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(698, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tháng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(563, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(526, 52);
            this.label1.TabIndex = 41;
            this.label1.Text = "THỐNG KÊ DOANH THU";
            // 
            // cboTK
            // 
            this.cboTK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTK.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTK.FormattingEnabled = true;
            this.cboTK.Items.AddRange(new object[] {
            "Thống kê doanh thu trong tháng",
            "Thống kê doanh thu trong năm",
            "Thống kê dịch vụ theo tháng",
            "Thống kê dịch vụ theo năm"});
            this.cboTK.Location = new System.Drawing.Point(269, 150);
            this.cboTK.Name = "cboTK";
            this.cboTK.Size = new System.Drawing.Size(413, 31);
            this.cboTK.TabIndex = 51;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.work_plan;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxIcon.Location = new System.Drawing.Point(1099, 47);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(58, 52);
            this.pictureBoxIcon.TabIndex = 58;
            this.pictureBoxIcon.TabStop = false;
            // 
            // QuanLiThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.cboTK);
            this.Controls.Add(this.dgvDT);
            this.Controls.Add(this.btnTK);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "QuanLiThongKe";
            this.Size = new System.Drawing.Size(1684, 1002);
            this.Load += new System.EventHandler(this.QuanLiThongKe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDT;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTK;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
    }
}
