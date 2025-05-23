﻿namespace QuanLyPhongTro.Control
{
    partial class ThongTinPhong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThongTinPhong));
            this.MaDichVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenDichVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.textBoxTienCoc = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDienTich = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePickerHanTro = new System.Windows.Forms.DateTimePicker();
            this.RtxtGhiChu = new System.Windows.Forms.RichTextBox();
            this.txtSoNuoc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSodien = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxTienPhong = new System.Windows.Forms.TextBox();
            this.txtTenPhong = new System.Windows.Forms.TextBox();
            this.txtMaPhong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewDichVu1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.buttonLuu = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDichVu1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // MaDichVu
            // 
            this.MaDichVu.DataPropertyName = "MaDichVu";
            this.MaDichVu.HeaderText = "Mã Dich Vu";
            this.MaDichVu.MinimumWidth = 6;
            this.MaDichVu.Name = "MaDichVu";
            this.MaDichVu.Visible = false;
            // 
            // DonGia
            // 
            this.DonGia.HeaderText = "Đơn giá";
            this.DonGia.MinimumWidth = 6;
            this.DonGia.Name = "DonGia";
            // 
            // TenDichVu
            // 
            this.TenDichVu.HeaderText = "Tên Dịch Vụ";
            this.TenDichVu.MinimumWidth = 6;
            this.TenDichVu.Name = "TenDichVu";
            // 
            // Chon
            // 
            this.Chon.DataPropertyName = "Chon";
            this.Chon.HeaderText = "Chọn";
            this.Chon.MinimumWidth = 6;
            this.Chon.Name = "Chon";
            this.Chon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // textBoxTienCoc
            // 
            this.textBoxTienCoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTienCoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBoxTienCoc.Location = new System.Drawing.Point(196, 417);
            this.textBoxTienCoc.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTienCoc.Name = "textBoxTienCoc";
            this.textBoxTienCoc.Size = new System.Drawing.Size(413, 29);
            this.textBoxTienCoc.TabIndex = 7;
            this.textBoxTienCoc.TextChanged += new System.EventHandler(this.textBoxTienCoc_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDienTich);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBoxTienCoc);
            this.panel1.Controls.Add(this.dateTimePickerHanTro);
            this.panel1.Controls.Add(this.RtxtGhiChu);
            this.panel1.Controls.Add(this.txtSoNuoc);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtSodien);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.textBoxTienPhong);
            this.panel1.Controls.Add(this.txtTenPhong);
            this.panel1.Controls.Add(this.txtMaPhong);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(20, 146);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 675);
            this.panel1.TabIndex = 35;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtDienTich
            // 
            this.txtDienTich.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDienTich.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDienTich.Location = new System.Drawing.Point(196, 262);
            this.txtDienTich.Margin = new System.Windows.Forms.Padding(2);
            this.txtDienTich.Name = "txtDienTich";
            this.txtDienTich.Size = new System.Drawing.Size(413, 29);
            this.txtDienTich.TabIndex = 4;
            this.txtDienTich.TextChanged += new System.EventHandler(this.txtDienTich_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(24, 266);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 20);
            this.label10.TabIndex = 24;
            this.label10.Text = "Diện tích :";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label9.Location = new System.Drawing.Point(153, 26);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(306, 33);
            this.label9.TabIndex = 22;
            this.label9.Text = "THÔNG TIN PHÒNG";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // dateTimePickerHanTro
            // 
            this.dateTimePickerHanTro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerHanTro.Location = new System.Drawing.Point(196, 469);
            this.dateTimePickerHanTro.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerHanTro.Name = "dateTimePickerHanTro";
            this.dateTimePickerHanTro.Size = new System.Drawing.Size(413, 26);
            this.dateTimePickerHanTro.TabIndex = 8;
            this.dateTimePickerHanTro.ValueChanged += new System.EventHandler(this.dateTimePickerHanTro_ValueChanged);
            // 
            // RtxtGhiChu
            // 
            this.RtxtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RtxtGhiChu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtGhiChu.Location = new System.Drawing.Point(28, 534);
            this.RtxtGhiChu.Margin = new System.Windows.Forms.Padding(2);
            this.RtxtGhiChu.Name = "RtxtGhiChu";
            this.RtxtGhiChu.Size = new System.Drawing.Size(581, 128);
            this.RtxtGhiChu.TabIndex = 9;
            this.RtxtGhiChu.Text = "";
            this.RtxtGhiChu.TextChanged += new System.EventHandler(this.RtxtGhiChu_TextChanged);
            // 
            // txtSoNuoc
            // 
            this.txtSoNuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoNuoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSoNuoc.Location = new System.Drawing.Point(196, 368);
            this.txtSoNuoc.Margin = new System.Windows.Forms.Padding(2);
            this.txtSoNuoc.Name = "txtSoNuoc";
            this.txtSoNuoc.Size = new System.Drawing.Size(413, 29);
            this.txtSoNuoc.TabIndex = 6;
            this.txtSoNuoc.TextChanged += new System.EventHandler(this.txtSoNuoc_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(24, 422);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Tiền cọc :";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txtSodien
            // 
            this.txtSodien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSodien.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSodien.Location = new System.Drawing.Point(196, 309);
            this.txtSodien.Margin = new System.Windows.Forms.Padding(2);
            this.txtSodien.Name = "txtSodien";
            this.txtSodien.Size = new System.Drawing.Size(413, 29);
            this.txtSodien.TabIndex = 5;
            this.txtSodien.TextChanged += new System.EventHandler(this.txtSodien_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(24, 470);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "Thời hạn trọ :";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label12.Location = new System.Drawing.Point(24, 313);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(164, 20);
            this.label12.TabIndex = 20;
            this.label12.Text = "Số kí điện bắt đầu :";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label13.Location = new System.Drawing.Point(24, 372);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(164, 20);
            this.label13.TabIndex = 21;
            this.label13.Text = "Khối nước bắt đầu :";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // textBoxTienPhong
            // 
            this.textBoxTienPhong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTienPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBoxTienPhong.Location = new System.Drawing.Point(196, 214);
            this.textBoxTienPhong.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTienPhong.Name = "textBoxTienPhong";
            this.textBoxTienPhong.Size = new System.Drawing.Size(413, 29);
            this.textBoxTienPhong.TabIndex = 3;
            this.textBoxTienPhong.TextChanged += new System.EventHandler(this.textBoxTienPhong_TextChanged);
            // 
            // txtTenPhong
            // 
            this.txtTenPhong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenPhong.Location = new System.Drawing.Point(196, 157);
            this.txtTenPhong.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new System.Drawing.Size(413, 29);
            this.txtTenPhong.TabIndex = 2;
            this.txtTenPhong.TextChanged += new System.EventHandler(this.txtTenPhong_TextChanged);
            // 
            // txtMaPhong
            // 
            this.txtMaPhong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaPhong.Enabled = false;
            this.txtMaPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtMaPhong.Location = new System.Drawing.Point(196, 99);
            this.txtMaPhong.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.Size = new System.Drawing.Size(413, 29);
            this.txtMaPhong.TabIndex = 1;
            this.txtMaPhong.TextChanged += new System.EventHandler(this.txtMaPhong_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(24, 217);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tiền phòng :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(24, 508);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ghi chú :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(24, 158);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên phòng :";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(24, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã phòng :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dataGridViewDichVu1
            // 
            this.dataGridViewDichVu1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDichVu1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDichVu1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDichVu1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Chon,
            this.TenDichVu,
            this.DonGia,
            this.MaDichVu});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDichVu1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDichVu1.Location = new System.Drawing.Point(675, 172);
            this.dataGridViewDichVu1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewDichVu1.Name = "dataGridViewDichVu1";
            this.dataGridViewDichVu1.RowHeadersWidth = 51;
            this.dataGridViewDichVu1.RowTemplate.Height = 24;
            this.dataGridViewDichVu1.Size = new System.Drawing.Size(969, 649);
            this.dataGridViewDichVu1.TabIndex = 38;
            this.dataGridViewDichVu1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDichVu_CellClick);
            this.dataGridViewDichVu1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDichVu1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(537, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(444, 52);
            this.label1.TabIndex = 3;
            this.label1.Text = "THÔNG TIN PHÒNG";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.pictureBoxIcon);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.dataGridViewDichVu1);
            this.panel3.Location = new System.Drawing.Point(14, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1656, 968);
            this.panel3.TabIndex = 39;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.bedroom;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxIcon.Location = new System.Drawing.Point(986, 29);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(58, 52);
            this.pictureBoxIcon.TabIndex = 59;
            this.pictureBoxIcon.TabStop = false;
            this.pictureBoxIcon.Click += new System.EventHandler(this.pictureBoxIcon_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(671, 146);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 20);
            this.label6.TabIndex = 39;
            this.label6.Text = "DỊCH VỤ PHÒNG";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnUpdate);
            this.panel4.Controls.Add(this.buttonLuu);
            this.panel4.Controls.Add(this.btnQuayLai);
            this.panel4.Location = new System.Drawing.Point(3, 855);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1653, 68);
            this.panel4.TabIndex = 0;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(1291, 17);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(348, 46);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // buttonLuu
            // 
            this.buttonLuu.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLuu.ForeColor = System.Drawing.Color.White;
            this.buttonLuu.Image = ((System.Drawing.Image)(resources.GetObject("buttonLuu.Image")));
            this.buttonLuu.Location = new System.Drawing.Point(654, 17);
            this.buttonLuu.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLuu.Name = "buttonLuu";
            this.buttonLuu.Size = new System.Drawing.Size(348, 46);
            this.buttonLuu.TabIndex = 11;
            this.buttonLuu.Text = "Lưu";
            this.buttonLuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonLuu.UseVisualStyleBackColor = false;
            this.buttonLuu.Click += new System.EventHandler(this.buttonLuu_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnQuayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Image = ((System.Drawing.Image)(resources.GetObject("btnQuayLai.Image")));
            this.btnQuayLai.Location = new System.Drawing.Point(17, 17);
            this.btnQuayLai.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(348, 46);
            this.btnQuayLai.TabIndex = 12;
            this.btnQuayLai.Text = "Quay lại";
            this.btnQuayLai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuayLai.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click_1);
            // 
            // ThongTinPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ThongTinPhong";
            this.Size = new System.Drawing.Size(1684, 1002);
            this.Load += new System.EventHandler(this.ThongTinPhong_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDichVu1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaDichVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenDichVu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chon;
        private System.Windows.Forms.Button buttonLuu;
        private System.Windows.Forms.TextBox textBoxTienCoc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePickerHanTro;
        private System.Windows.Forms.RichTextBox RtxtGhiChu;
        private System.Windows.Forms.TextBox txtSoNuoc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSodien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxTienPhong;
        private System.Windows.Forms.TextBox txtTenPhong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewDichVu1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMaPhong;
        private System.Windows.Forms.TextBox txtDienTich;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
    }
}
