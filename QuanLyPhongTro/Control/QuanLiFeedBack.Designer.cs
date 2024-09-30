namespace QuanLyPhongTro.Control
{
    partial class QuanLiFeedBack
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
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewFeedBack = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimKiemFeedBack = new System.Windows.Forms.TextBox();
            this.buttonTimKiemFeedBack = new System.Windows.Forms.Button();
            this.buttonRefeshFB = new System.Windows.Forms.Button();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoiDungPhanHoi = new System.Windows.Forms.TextBox();
            this.panelThongTinDanCu = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.panelThongTinDanCu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "Danh sách phản hồi";
            // 
            // dataGridViewFeedBack
            // 
            this.dataGridViewFeedBack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFeedBack.Location = new System.Drawing.Point(16, 91);
            this.dataGridViewFeedBack.Name = "dataGridViewFeedBack";
            this.dataGridViewFeedBack.Size = new System.Drawing.Size(993, 361);
            this.dataGridViewFeedBack.TabIndex = 31;
            this.dataGridViewFeedBack.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFeedBack_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(377, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 25);
            this.label1.TabIndex = 30;
            this.label1.Text = "QUẢN LÍ PHẢN HỒI";
            // 
            // txtTimKiemFeedBack
            // 
            this.txtTimKiemFeedBack.Location = new System.Drawing.Point(638, 59);
            this.txtTimKiemFeedBack.Multiline = true;
            this.txtTimKiemFeedBack.Name = "txtTimKiemFeedBack";
            this.txtTimKiemFeedBack.Size = new System.Drawing.Size(236, 24);
            this.txtTimKiemFeedBack.TabIndex = 34;
            // 
            // buttonTimKiemFeedBack
            // 
            this.buttonTimKiemFeedBack.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonTimKiemFeedBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTimKiemFeedBack.ForeColor = System.Drawing.Color.White;
            this.buttonTimKiemFeedBack.Location = new System.Drawing.Point(880, 58);
            this.buttonTimKiemFeedBack.Name = "buttonTimKiemFeedBack";
            this.buttonTimKiemFeedBack.Size = new System.Drawing.Size(91, 27);
            this.buttonTimKiemFeedBack.TabIndex = 35;
            this.buttonTimKiemFeedBack.Text = "Tìm kiếm";
            this.buttonTimKiemFeedBack.UseVisualStyleBackColor = false;
            this.buttonTimKiemFeedBack.Click += new System.EventHandler(this.buttonTimKiemFeedBack_Click);
            // 
            // buttonRefeshFB
            // 
            this.buttonRefeshFB.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.RefeshIcon;
            this.buttonRefeshFB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRefeshFB.Location = new System.Drawing.Point(974, 58);
            this.buttonRefeshFB.Name = "buttonRefeshFB";
            this.buttonRefeshFB.Size = new System.Drawing.Size(36, 27);
            this.buttonRefeshFB.TabIndex = 39;
            this.buttonRefeshFB.UseVisualStyleBackColor = true;
            this.buttonRefeshFB.Click += new System.EventHandler(this.buttonRefeshFB_Click);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.FeedbackIcon;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxIcon.Location = new System.Drawing.Point(600, 13);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(37, 36);
            this.pictureBoxIcon.TabIndex = 33;
            this.pictureBoxIcon.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label14.Location = new System.Drawing.Point(13, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(159, 20);
            this.label14.TabIndex = 39;
            this.label14.Text = "Nội dung phản hồi ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mã phòng :";
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Location = new System.Drawing.Point(81, 59);
            this.txtMaPhong.Multiline = true;
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(123, 26);
            this.txtMaPhong.TabIndex = 8;
            this.txtMaPhong.TextChanged += new System.EventHandler(this.txtMaPhong_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nội dung :";
            // 
            // txtNoiDungPhanHoi
            // 
            this.txtNoiDungPhanHoi.Location = new System.Drawing.Point(81, 112);
            this.txtNoiDungPhanHoi.Multiline = true;
            this.txtNoiDungPhanHoi.Name = "txtNoiDungPhanHoi";
            this.txtNoiDungPhanHoi.Size = new System.Drawing.Size(887, 188);
            this.txtNoiDungPhanHoi.TabIndex = 10;
            this.txtNoiDungPhanHoi.TextChanged += new System.EventHandler(this.txtHoTenCuDan_TextChanged);
            // 
            // panelThongTinDanCu
            // 
            this.panelThongTinDanCu.BackColor = System.Drawing.Color.White;
            this.panelThongTinDanCu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelThongTinDanCu.Controls.Add(this.label14);
            this.panelThongTinDanCu.Controls.Add(this.label3);
            this.panelThongTinDanCu.Controls.Add(this.txtMaPhong);
            this.panelThongTinDanCu.Controls.Add(this.label4);
            this.panelThongTinDanCu.Controls.Add(this.txtNoiDungPhanHoi);
            this.panelThongTinDanCu.Location = new System.Drawing.Point(16, 469);
            this.panelThongTinDanCu.Name = "panelThongTinDanCu";
            this.panelThongTinDanCu.Size = new System.Drawing.Size(993, 330);
            this.panelThongTinDanCu.TabIndex = 40;
            this.panelThongTinDanCu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelThongTinDanCu_Paint);
            // 
            // QuanLiFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelThongTinDanCu);
            this.Controls.Add(this.buttonRefeshFB);
            this.Controls.Add(this.buttonTimKiemFeedBack);
            this.Controls.Add(this.txtTimKiemFeedBack);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewFeedBack);
            this.Controls.Add(this.label1);
            this.Name = "QuanLiFeedBack";
            this.Size = new System.Drawing.Size(1024, 815);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.panelThongTinDanCu.ResumeLayout(false);
            this.panelThongTinDanCu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewFeedBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.TextBox txtTimKiemFeedBack;
        private System.Windows.Forms.Button buttonTimKiemFeedBack;
        private System.Windows.Forms.Button buttonRefeshFB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNoiDungPhanHoi;
        private System.Windows.Forms.Panel panelThongTinDanCu;
    }
}
