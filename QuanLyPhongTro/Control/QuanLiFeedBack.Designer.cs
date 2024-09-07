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
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.buttonRefeshFB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(21, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "Danh sách feedback";
            // 
            // dataGridViewFeedBack
            // 
            this.dataGridViewFeedBack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFeedBack.Location = new System.Drawing.Point(25, 91);
            this.dataGridViewFeedBack.Name = "dataGridViewFeedBack";
            this.dataGridViewFeedBack.Size = new System.Drawing.Size(888, 666);
            this.dataGridViewFeedBack.TabIndex = 31;
            this.dataGridViewFeedBack.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFeedBack_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(297, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 31);
            this.label1.TabIndex = 30;
            this.label1.Text = "QUẢN LÍ FEEDBACK";
            // 
            // txtTimKiemFeedBack
            // 
            this.txtTimKiemFeedBack.Location = new System.Drawing.Point(624, 59);
            this.txtTimKiemFeedBack.Multiline = true;
            this.txtTimKiemFeedBack.Name = "txtTimKiemFeedBack";
            this.txtTimKiemFeedBack.Size = new System.Drawing.Size(153, 24);
            this.txtTimKiemFeedBack.TabIndex = 34;
            // 
            // buttonTimKiemFeedBack
            // 
            this.buttonTimKiemFeedBack.Location = new System.Drawing.Point(783, 58);
            this.buttonTimKiemFeedBack.Name = "buttonTimKiemFeedBack";
            this.buttonTimKiemFeedBack.Size = new System.Drawing.Size(91, 27);
            this.buttonTimKiemFeedBack.TabIndex = 35;
            this.buttonTimKiemFeedBack.Text = "Tìm kiếm";
            this.buttonTimKiemFeedBack.UseVisualStyleBackColor = true;
            this.buttonTimKiemFeedBack.Click += new System.EventHandler(this.buttonTimKiemFeedBack_Click);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.FeedbackIcon;
            this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxIcon.Location = new System.Drawing.Point(594, 12);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(37, 36);
            this.pictureBoxIcon.TabIndex = 33;
            this.pictureBoxIcon.TabStop = false;
            // 
            // buttonRefeshFB
            // 
            this.buttonRefeshFB.BackgroundImage = global::QuanLyPhongTro.Properties.Resources.RefeshIcon;
            this.buttonRefeshFB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRefeshFB.Location = new System.Drawing.Point(877, 58);
            this.buttonRefeshFB.Name = "buttonRefeshFB";
            this.buttonRefeshFB.Size = new System.Drawing.Size(36, 27);
            this.buttonRefeshFB.TabIndex = 39;
            this.buttonRefeshFB.UseVisualStyleBackColor = true;
            this.buttonRefeshFB.Click += new System.EventHandler(this.buttonRefeshFB_Click);
            // 
            // QuanLiFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonRefeshFB);
            this.Controls.Add(this.buttonTimKiemFeedBack);
            this.Controls.Add(this.txtTimKiemFeedBack);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewFeedBack);
            this.Controls.Add(this.label1);
            this.Name = "QuanLiFeedBack";
            this.Size = new System.Drawing.Size(935, 780);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
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
    }
}
