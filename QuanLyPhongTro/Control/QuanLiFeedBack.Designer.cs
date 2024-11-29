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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLiFeedBack));
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewFeedBack = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoiDungPhanHoi = new System.Windows.Forms.TextBox();
            this.panelThongTinDanCu = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPhanHoi = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLoc = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.comboboxTrangThai = new System.Windows.Forms.ComboBox();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.buttonRefeshFB = new System.Windows.Forms.Button();
            this.btnGuiPhanHoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedBack)).BeginInit();
            this.panelThongTinDanCu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(17, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "Danh sách phản hồi";
            // 
            // dataGridViewFeedBack
            // 
            this.dataGridViewFeedBack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFeedBack.Location = new System.Drawing.Point(21, 132);
            this.dataGridViewFeedBack.Name = "dataGridViewFeedBack";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFeedBack.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFeedBack.Size = new System.Drawing.Size(1620, 486);
            this.dataGridViewFeedBack.TabIndex = 31;
            this.dataGridViewFeedBack.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFeedBack_CellContentClick);
            this.dataGridViewFeedBack.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewFeedBack_CellFormatting);
            this.dataGridViewFeedBack.SelectionChanged += new System.EventHandler(this.dataGridViewFeedBack_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(522, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(545, 52);
            this.label1.TabIndex = 30;
            this.label1.Text = " PHẢN HỒI CỦA CƯ DÂN";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label14.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label14.Location = new System.Drawing.Point(13, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(145, 20);
            this.label14.TabIndex = 39;
            this.label14.Text = "Chi tiết phản hồi ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(14, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Phòng ";
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaPhong.Location = new System.Drawing.Point(84, 57);
            this.txtMaPhong.Multiline = true;
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.ReadOnly = true;
            this.txtMaPhong.Size = new System.Drawing.Size(185, 26);
            this.txtMaPhong.TabIndex = 8;
            this.txtMaPhong.TextChanged += new System.EventHandler(this.txtMaPhong_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(14, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nội dung";
            // 
            // txtNoiDungPhanHoi
            // 
            this.txtNoiDungPhanHoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtNoiDungPhanHoi.Location = new System.Drawing.Point(84, 105);
            this.txtNoiDungPhanHoi.Multiline = true;
            this.txtNoiDungPhanHoi.Name = "txtNoiDungPhanHoi";
            this.txtNoiDungPhanHoi.ReadOnly = true;
            this.txtNoiDungPhanHoi.Size = new System.Drawing.Size(1517, 55);
            this.txtNoiDungPhanHoi.TabIndex = 10;
            this.txtNoiDungPhanHoi.TextChanged += new System.EventHandler(this.txtHoTenCuDan_TextChanged);
            // 
            // panelThongTinDanCu
            // 
            this.panelThongTinDanCu.BackColor = System.Drawing.Color.White;
            this.panelThongTinDanCu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelThongTinDanCu.Controls.Add(this.btnGuiPhanHoi);
            this.panelThongTinDanCu.Controls.Add(this.label6);
            this.panelThongTinDanCu.Controls.Add(this.label5);
            this.panelThongTinDanCu.Controls.Add(this.txtPhanHoi);
            this.panelThongTinDanCu.Controls.Add(this.label14);
            this.panelThongTinDanCu.Controls.Add(this.label3);
            this.panelThongTinDanCu.Controls.Add(this.txtMaPhong);
            this.panelThongTinDanCu.Controls.Add(this.label4);
            this.panelThongTinDanCu.Controls.Add(this.txtNoiDungPhanHoi);
            this.panelThongTinDanCu.Location = new System.Drawing.Point(21, 624);
            this.panelThongTinDanCu.Name = "panelThongTinDanCu";
            this.panelThongTinDanCu.Size = new System.Drawing.Size(1620, 330);
            this.panelThongTinDanCu.TabIndex = 40;
            this.panelThongTinDanCu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelThongTinDanCu_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(13, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 20);
            this.label6.TabIndex = 42;
            this.label6.Text = "Trả lời phản hồi ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(14, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Nội dung";
            // 
            // txtPhanHoi
            // 
            this.txtPhanHoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPhanHoi.Location = new System.Drawing.Point(84, 214);
            this.txtPhanHoi.Multiline = true;
            this.txtPhanHoi.Name = "txtPhanHoi";
            this.txtPhanHoi.Size = new System.Drawing.Size(1517, 55);
            this.txtPhanHoi.TabIndex = 41;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBoxIcon);
            this.panel1.Controls.Add(this.buttonLoc);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.comboboxTrangThai);
            this.panel1.Controls.Add(this.buttonRefeshFB);
            this.panel1.Controls.Add(this.panelThongTinDanCu);
            this.panel1.Controls.Add(this.dataGridViewFeedBack);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(15, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1656, 968);
            this.panel1.TabIndex = 41;
            // 
            // buttonLoc
            // 
            this.buttonLoc.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.buttonLoc.ForeColor = System.Drawing.Color.Transparent;
            this.buttonLoc.Location = new System.Drawing.Point(1472, 98);
            this.buttonLoc.Name = "buttonLoc";
            this.buttonLoc.Size = new System.Drawing.Size(117, 26);
            this.buttonLoc.TabIndex = 44;
            this.buttonLoc.Text = "Lọc ";
            this.buttonLoc.UseVisualStyleBackColor = false;
            this.buttonLoc.Click += new System.EventHandler(this.buttonLoc_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(1213, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 18);
            this.label12.TabIndex = 45;
            this.label12.Text = "Trạng Thái";
            // 
            // comboboxTrangThai
            // 
            this.comboboxTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboboxTrangThai.FormattingEnabled = true;
            this.comboboxTrangThai.Location = new System.Drawing.Point(1307, 98);
            this.comboboxTrangThai.Name = "comboboxTrangThai";
            this.comboboxTrangThai.Size = new System.Drawing.Size(153, 26);
            this.comboboxTrangThai.TabIndex = 43;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.satisfaction;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxIcon.Location = new System.Drawing.Point(1064, 14);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(58, 52);
            this.pictureBoxIcon.TabIndex = 56;
            this.pictureBoxIcon.TabStop = false;
            // 
            // buttonRefeshFB
            // 
            this.buttonRefeshFB.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.RefeshIcon;
            this.buttonRefeshFB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRefeshFB.Location = new System.Drawing.Point(1605, 99);
            this.buttonRefeshFB.Name = "buttonRefeshFB";
            this.buttonRefeshFB.Size = new System.Drawing.Size(36, 27);
            this.buttonRefeshFB.TabIndex = 39;
            this.buttonRefeshFB.UseVisualStyleBackColor = true;
            this.buttonRefeshFB.Click += new System.EventHandler(this.buttonRefeshFB_Click);
            // 
            // btnGuiPhanHoi
            // 
            this.btnGuiPhanHoi.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGuiPhanHoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiPhanHoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnGuiPhanHoi.ForeColor = System.Drawing.Color.White;
            this.btnGuiPhanHoi.Image = ((System.Drawing.Image)(resources.GetObject("btnGuiPhanHoi.Image")));
            this.btnGuiPhanHoi.Location = new System.Drawing.Point(707, 275);
            this.btnGuiPhanHoi.Name = "btnGuiPhanHoi";
            this.btnGuiPhanHoi.Size = new System.Drawing.Size(219, 46);
            this.btnGuiPhanHoi.TabIndex = 41;
            this.btnGuiPhanHoi.Text = "Gửi phản hồi";
            this.btnGuiPhanHoi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuiPhanHoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuiPhanHoi.UseVisualStyleBackColor = false;
            this.btnGuiPhanHoi.Click += new System.EventHandler(this.btnGuiPhanHoi_Click);
            // 
            // QuanLiFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.panel1);
            this.Name = "QuanLiFeedBack";
            this.Size = new System.Drawing.Size(1684, 1002);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedBack)).EndInit();
            this.panelThongTinDanCu.ResumeLayout(false);
            this.panelThongTinDanCu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewFeedBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRefeshFB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNoiDungPhanHoi;
        private System.Windows.Forms.Panel panelThongTinDanCu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGuiPhanHoi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPhanHoi;
        private System.Windows.Forms.Button buttonLoc;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboboxTrangThai;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
    }
}
