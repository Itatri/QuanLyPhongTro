namespace QuanLyPhongTro.Control
{
    partial class QuanLyPhieuThu
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
            this.dgvPT = new System.Windows.Forms.DataGridView();
            this.XemChiTiet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTao = new System.Windows.Forms.Button();
            this.buttonRefesh = new System.Windows.Forms.Button();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPT
            // 
            this.dgvPT.AllowUserToAddRows = false;
            this.dgvPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.XemChiTiet,
            this.TrangThai});
            this.dgvPT.Location = new System.Drawing.Point(38, 175);
            this.dgvPT.Name = "dgvPT";
            this.dgvPT.RowHeadersWidth = 51;
            this.dgvPT.RowTemplate.Height = 24;
            this.dgvPT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPT.Size = new System.Drawing.Size(1579, 760);
            this.dgvPT.TabIndex = 0;
            this.dgvPT.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPT_CellContentClick);
            this.dgvPT.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPT_CellFormatting);
            this.dgvPT.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvPT_RowPrePaint);
            // 
            // XemChiTiet
            // 
            this.XemChiTiet.DataPropertyName = "XemChiTiet";
            this.XemChiTiet.HeaderText = "";
            this.XemChiTiet.MinimumWidth = 6;
            this.XemChiTiet.Name = "XemChiTiet";
            this.XemChiTiet.Text = "Xem Chi Tiết";
            this.XemChiTiet.UseColumnTextForButtonValue = true;
            this.XemChiTiet.Width = 125;
            // 
            // TrangThai
            // 
            this.TrangThai.DataPropertyName = "TrangThai";
            this.TrangThai.HeaderText = "Đã Thanh Toán";
            this.TrangThai.MinimumWidth = 6;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TrangThai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TrangThai.Visible = false;
            this.TrangThai.Width = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(546, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(538, 52);
            this.label1.TabIndex = 5;
            this.label1.Text = "DANH SÁCH PHIẾU THU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(380, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tháng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(627, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Năm";
            // 
            // cboThang
            // 
            this.cboThang.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(452, 115);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(155, 31);
            this.cboThang.TabIndex = 7;
            // 
            // cboNam
            // 
            this.cboNam.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(689, 115);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(144, 31);
            this.cboNam.TabIndex = 7;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoc.ForeColor = System.Drawing.Color.Snow;
            this.btnLoc.Location = new System.Drawing.Point(853, 113);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(100, 34);
            this.btnLoc.TabIndex = 8;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(1200, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tìm Kiếm";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox1.Location = new System.Drawing.Point(1303, 115);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(314, 30);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnTao
            // 
            this.btnTao.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTao.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTao.ForeColor = System.Drawing.Color.Snow;
            this.btnTao.Location = new System.Drawing.Point(38, 111);
            this.btnTao.Name = "btnTao";
            this.btnTao.Size = new System.Drawing.Size(133, 37);
            this.btnTao.TabIndex = 11;
            this.btnTao.Text = "Tạo Phiếu Thu";
            this.btnTao.UseVisualStyleBackColor = false;
            this.btnTao.Click += new System.EventHandler(this.btnTao_Click);
            // 
            // buttonRefesh
            // 
            this.buttonRefesh.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.RefeshIcon;
            this.buttonRefesh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRefesh.Location = new System.Drawing.Point(963, 113);
            this.buttonRefesh.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRefesh.Name = "buttonRefesh";
            this.buttonRefesh.Size = new System.Drawing.Size(48, 33);
            this.buttonRefesh.TabIndex = 39;
            this.buttonRefesh.UseVisualStyleBackColor = true;
            this.buttonRefesh.Click += new System.EventHandler(this.buttonRefesh_Click);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.invoice;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxIcon.Location = new System.Drawing.Point(1089, 29);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(58, 52);
            this.pictureBoxIcon.TabIndex = 59;
            this.pictureBoxIcon.TabStop = false;
            // 
            // QuanLyPhieuThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.buttonRefesh);
            this.Controls.Add(this.btnTao);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPT);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.Name = "QuanLyPhieuThu";
            this.Size = new System.Drawing.Size(1656, 968);
            this.Load += new System.EventHandler(this.QuanLyPhieuThu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTao;
        private System.Windows.Forms.Button buttonRefesh;
        private System.Windows.Forms.DataGridViewButtonColumn XemChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
    }
}
