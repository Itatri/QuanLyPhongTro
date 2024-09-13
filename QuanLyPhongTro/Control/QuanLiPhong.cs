using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace QuanLyPhongTro.Control
{
    public partial class QuanLiPhong : UserControl
    {
        private string connectionString = @"Data Source=DOHHUY\SQLEXPRESS01;Initial Catalog=Test;Integrated Security=True;Encrypt=False";
        private SqlConnection con;
        public QuanLiPhong()
        {
            InitializeComponent();
        }

        private void QuanLiPhong_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            RefreshDataGridView();
        }
        private void RefreshDataGridView()
        {
            string query = "SELECT * FROM PHONG";
            DataTable dataTable = ExecuteQuery(query);
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        //
        private DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        //
        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        private void btnDangKyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                DanKyTaiKhoanKhachHang f = new DanKyTaiKhoanKhachHang();
                mainForm.ShowControl(f); // Truyền đối tượng f vào phương thức ShowControl
            }
        }

        private void btnTaoPhong_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                TaoQuanLyPhong f = new TaoQuanLyPhong();
                mainForm.ShowControl(f);
            }
        }
    }
}
