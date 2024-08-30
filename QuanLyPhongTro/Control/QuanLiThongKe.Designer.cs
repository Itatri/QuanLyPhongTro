namespace QuanLyPhongTro.Control
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
            this.btnTKThang = new System.Windows.Forms.Button();
            this.btnTKNam = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnXuat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTKThang
            // 
            this.btnTKThang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTKThang.Location = new System.Drawing.Point(507, 114);
            this.btnTKThang.Name = "btnTKThang";
            this.btnTKThang.Size = new System.Drawing.Size(140, 55);
            this.btnTKThang.TabIndex = 0;
            this.btnTKThang.Text = "Thống kê theo Tháng";
            this.btnTKThang.UseVisualStyleBackColor = true;
            // 
            // btnTKNam
            // 
            this.btnTKNam.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTKNam.Location = new System.Drawing.Point(671, 114);
            this.btnTKNam.Name = "btnTKNam";
            this.btnTKNam.Size = new System.Drawing.Size(140, 55);
            this.btnTKNam.TabIndex = 0;
            this.btnTKNam.Text = "Thống kê theo Năm";
            this.btnTKNam.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 195);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(944, 489);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnXuat
            // 
            this.btnXuat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.Location = new System.Drawing.Point(829, 114);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(140, 55);
            this.btnXuat.TabIndex = 0;
            this.btnXuat.Text = "Xuất";
            this.btnXuat.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 45);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quản Lý Thống Kê";
            // 
            // QuanLiThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.btnTKNam);
            this.Controls.Add(this.btnTKThang);
            this.Name = "QuanLiThongKe";
            this.Size = new System.Drawing.Size(1004, 710);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTKThang;
        private System.Windows.Forms.Button btnTKNam;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Label label1;
    }
}
