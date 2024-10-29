namespace QuanLyPhongTro.Control
{
    partial class QuanLiPhong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLiPhong));
            this.button2 = new System.Windows.Forms.Button();
            this.btnDangKyTaiKhoan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTaoPhong = new System.Windows.Forms.Button();
            this.buttonTraPhong = new System.Windows.Forms.Button();
            this.cbbSapXep = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxTimPhong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(830, 20);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(217, 42);
            this.button2.TabIndex = 3;
            this.button2.Text = "Tạo phiếu thu";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnDangKyTaiKhoan
            // 
            this.btnDangKyTaiKhoan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDangKyTaiKhoan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKyTaiKhoan.ForeColor = System.Drawing.Color.White;
            this.btnDangKyTaiKhoan.Location = new System.Drawing.Point(424, 20);
            this.btnDangKyTaiKhoan.Margin = new System.Windows.Forms.Padding(2);
            this.btnDangKyTaiKhoan.Name = "btnDangKyTaiKhoan";
            this.btnDangKyTaiKhoan.Size = new System.Drawing.Size(217, 42);
            this.btnDangKyTaiKhoan.TabIndex = 2;
            this.btnDangKyTaiKhoan.Text = "Đăng ký tài khoản";
            this.btnDangKyTaiKhoan.UseVisualStyleBackColor = false;
            this.btnDangKyTaiKhoan.Click += new System.EventHandler(this.btnDangKyTaiKhoan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(652, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 55);
            this.label1.TabIndex = 4;
            this.label1.Text = "DANH SÁCH PHÒNG";
            // 
            // btnTaoPhong
            // 
            this.btnTaoPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnTaoPhong.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoPhong.ForeColor = System.Drawing.Color.White;
            this.btnTaoPhong.Location = new System.Drawing.Point(18, 20);
            this.btnTaoPhong.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaoPhong.Name = "btnTaoPhong";
            this.btnTaoPhong.Size = new System.Drawing.Size(217, 42);
            this.btnTaoPhong.TabIndex = 1;
            this.btnTaoPhong.Text = "Tạo phòng";
            this.btnTaoPhong.UseVisualStyleBackColor = false;
            this.btnTaoPhong.Click += new System.EventHandler(this.btnTaoPhong_Click);
            // 
            // buttonTraPhong
            // 
            this.buttonTraPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonTraPhong.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTraPhong.ForeColor = System.Drawing.Color.White;
            this.buttonTraPhong.Location = new System.Drawing.Point(1236, 20);
            this.buttonTraPhong.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTraPhong.Name = "buttonTraPhong";
            this.buttonTraPhong.Size = new System.Drawing.Size(217, 42);
            this.buttonTraPhong.TabIndex = 5;
            this.buttonTraPhong.Text = "Trả phòng";
            this.buttonTraPhong.UseVisualStyleBackColor = false;
            this.buttonTraPhong.Click += new System.EventHandler(this.buttonTraPhong_Click);
            // 
            // cbbSapXep
            // 
            this.cbbSapXep.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSapXep.FormattingEnabled = true;
            this.cbbSapXep.Location = new System.Drawing.Point(1382, 120);
            this.cbbSapXep.Margin = new System.Windows.Forms.Padding(2);
            this.cbbSapXep.Name = "cbbSapXep";
            this.cbbSapXep.Size = new System.Drawing.Size(198, 25);
            this.cbbSapXep.TabIndex = 20;
            this.cbbSapXep.SelectedIndexChanged += new System.EventHandler(this.cbbSapXep_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 160);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1620, 722);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1298, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "Sắp xếp:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBoxTimPhong);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbbSapXep);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1656, 972);
            this.panel1.TabIndex = 22;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(1585, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 38);
            this.button1.TabIndex = 25;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxTimPhong
            // 
            this.textBoxTimPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTimPhong.Location = new System.Drawing.Point(1019, 120);
            this.textBoxTimPhong.Name = "textBoxTimPhong";
            this.textBoxTimPhong.Size = new System.Drawing.Size(258, 26);
            this.textBoxTimPhong.TabIndex = 23;
            this.textBoxTimPhong.TextChanged += new System.EventHandler(this.textBoxTimPhong_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(881, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 19);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tìm kiếm phòng:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDangKyTaiKhoan);
            this.panel2.Controls.Add(this.buttonTraPhong);
            this.panel2.Controls.Add(this.btnTaoPhong);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Location = new System.Drawing.Point(3, 888);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1650, 77);
            this.panel2.TabIndex = 0;
            // 
            // QuanLiPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.Controls.Add(this.panel1);
            this.Name = "QuanLiPhong";
            this.Size = new System.Drawing.Size(1684, 1002);
            this.Load += new System.EventHandler(this.QuanLiPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDangKyTaiKhoan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTaoPhong;
        private System.Windows.Forms.Button buttonTraPhong;
        private System.Windows.Forms.ComboBox cbbSapXep;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxTimPhong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}
