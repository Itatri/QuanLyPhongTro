namespace QuanLyPhongTro.Control
{
    partial class TaoQuanLyPhong
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
            this.dtpk_NgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSoNuoc = new System.Windows.Forms.TextBox();
            this.txtSodien = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaKhuVuc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.RtxtGhiChu = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.cbbTrangThai = new System.Windows.Forms.ComboBox();
            this.txtTienCoc = new System.Windows.Forms.TextBox();
            this.DTPK_NgayVao = new System.Windows.Forms.DateTimePicker();
            this.txtTenPhong = new System.Windows.Forms.TextBox();
            this.cbbMaLoaiPhong = new System.Windows.Forms.ComboBox();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnmoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpk_NgayHetHan
            // 
            this.dtpk_NgayHetHan.Location = new System.Drawing.Point(158, 344);
            this.dtpk_NgayHetHan.Name = "dtpk_NgayHetHan";
            this.dtpk_NgayHetHan.Size = new System.Drawing.Size(274, 22);
            this.dtpk_NgayHetHan.TabIndex = 26;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 346);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 16);
            this.label16.TabIndex = 25;
            this.label16.Text = "Ngày hết hạn";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSoNuoc);
            this.panel3.Controls.Add(this.txtSodien);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Location = new System.Drawing.Point(487, 263);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(413, 127);
            this.panel3.TabIndex = 22;
            // 
            // txtSoNuoc
            // 
            this.txtSoNuoc.Location = new System.Drawing.Point(176, 75);
            this.txtSoNuoc.Name = "txtSoNuoc";
            this.txtSoNuoc.Size = new System.Drawing.Size(186, 22);
            this.txtSoNuoc.TabIndex = 23;
            this.txtSoNuoc.TextChanged += new System.EventHandler(this.txtSoNuoc_TextChanged);
            // 
            // txtSodien
            // 
            this.txtSodien.Location = new System.Drawing.Point(176, 18);
            this.txtSodien.Name = "txtSodien";
            this.txtSodien.Size = new System.Drawing.Size(186, 22);
            this.txtSodien.TabIndex = 22;
            this.txtSodien.TextChanged += new System.EventHandler(this.txtSodien_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 16);
            this.label12.TabIndex = 20;
            this.label12.Text = "sô kí điện bắt đầu:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 16);
            this.label13.TabIndex = 21;
            this.label13.Text = "Khối nước bắt đầu:";
            // 
            // txtMaKhuVuc
            // 
            this.txtMaKhuVuc.Enabled = false;
            this.txtMaKhuVuc.Location = new System.Drawing.Point(623, 110);
            this.txtMaKhuVuc.Name = "txtMaKhuVuc";
            this.txtMaKhuVuc.Size = new System.Drawing.Size(277, 22);
            this.txtMaKhuVuc.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(323, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 16);
            this.label11.TabIndex = 18;
            this.label11.Text = "tên phòng tự nhập";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(301, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(166, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "max phingf tự sinh tăng dần";
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDangKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(522, 999);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(165, 55);
            this.btnDangKy.TabIndex = 15;
            this.btnDangKy.Text = "tạo phòng";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // RtxtGhiChu
            // 
            this.RtxtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RtxtGhiChu.Location = new System.Drawing.Point(158, 415);
            this.RtxtGhiChu.Name = "RtxtGhiChu";
            this.RtxtGhiChu.Size = new System.Drawing.Size(742, 96);
            this.RtxtGhiChu.TabIndex = 16;
            this.RtxtGhiChu.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.dtpk_NgayHetHan);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtMaKhuVuc);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.RtxtGhiChu);
            this.panel1.Controls.Add(this.cbbTrangThai);
            this.panel1.Controls.Add(this.txtTienCoc);
            this.panel1.Controls.Add(this.DTPK_NgayVao);
            this.panel1.Controls.Add(this.txtTenPhong);
            this.panel1.Controls.Add(this.cbbMaLoaiPhong);
            this.panel1.Controls.Add(this.txtMaPhong);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(35, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(932, 590);
            this.panel1.TabIndex = 13;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(697, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(206, 16);
            this.label17.TabIndex = 18;
            this.label17.Text = "hiện lên khu vực từ chỗ đăng nhập";
            // 
            // cbbTrangThai
            // 
            this.cbbTrangThai.FormattingEnabled = true;
            this.cbbTrangThai.Location = new System.Drawing.Point(158, 272);
            this.cbbTrangThai.Name = "cbbTrangThai";
            this.cbbTrangThai.Size = new System.Drawing.Size(277, 24);
            this.cbbTrangThai.TabIndex = 15;
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.Location = new System.Drawing.Point(623, 192);
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.Size = new System.Drawing.Size(277, 22);
            this.txtTienCoc.TabIndex = 14;
            this.txtTienCoc.TextChanged += new System.EventHandler(this.txtTienCoc_TextChanged);
            // 
            // DTPK_NgayVao
            // 
            this.DTPK_NgayVao.Location = new System.Drawing.Point(158, 193);
            this.DTPK_NgayVao.Name = "DTPK_NgayVao";
            this.DTPK_NgayVao.Size = new System.Drawing.Size(277, 22);
            this.DTPK_NgayVao.TabIndex = 13;
            // 
            // txtTenPhong
            // 
            this.txtTenPhong.Location = new System.Drawing.Point(158, 110);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new System.Drawing.Size(277, 22);
            this.txtTenPhong.TabIndex = 12;
            // 
            // cbbMaLoaiPhong
            // 
            this.cbbMaLoaiPhong.FormattingEnabled = true;
            this.cbbMaLoaiPhong.Location = new System.Drawing.Point(623, 26);
            this.cbbMaLoaiPhong.Name = "cbbMaLoaiPhong";
            this.cbbMaLoaiPhong.Size = new System.Drawing.Size(277, 24);
            this.cbbMaLoaiPhong.TabIndex = 10;
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.Enabled = false;
            this.txtMaPhong.Location = new System.Drawing.Point(158, 28);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(277, 22);
            this.txtMaPhong.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 416);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Ghi chú:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Trạng thái: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tiền cọc:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ngày tạo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên phòng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(484, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mã khu vực:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã loại phòng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã phòng:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(35, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(932, 53);
            this.panel2.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tạo phòng";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 746);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1230, 210);
            this.dataGridView1.TabIndex = 16;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnmoi
            // 
            this.btnmoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnmoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmoi.ForeColor = System.Drawing.Color.White;
            this.btnmoi.Location = new System.Drawing.Point(932, 999);
            this.btnmoi.Name = "btnmoi";
            this.btnmoi.Size = new System.Drawing.Size(165, 55);
            this.btnmoi.TabIndex = 17;
            this.btnmoi.Text = "mới";
            this.btnmoi.UseVisualStyleBackColor = false;
            this.btnmoi.Click += new System.EventHandler(this.btnmoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(727, 999);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(165, 55);
            this.btnXoa.TabIndex = 18;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // TaoQuanLyPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnmoi);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TaoQuanLyPhong";
            this.Size = new System.Drawing.Size(1247, 1076);
            this.Load += new System.EventHandler(this.TaoQuanLyPhong_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpk_NgayHetHan;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSoNuoc;
        private System.Windows.Forms.TextBox txtSodien;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMaKhuVuc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.RichTextBox RtxtGhiChu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbbTrangThai;
        private System.Windows.Forms.TextBox txtTienCoc;
        private System.Windows.Forms.DateTimePicker DTPK_NgayVao;
        private System.Windows.Forms.TextBox txtTenPhong;
        private System.Windows.Forms.ComboBox cbbMaLoaiPhong;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnmoi;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnXoa;
    }
}
