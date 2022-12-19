using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Data_Base_Builder_Firm
{
    public partial class inRgister : Form
    {

        DataBase dataBase = new DataBase();
        public inRgister()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"select  login, password from register where login = '{login}' and password = '{password}'";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы вошли успешно");
                MenuBuilldFirm meny = new MenuBuilldFirm();
                this.Hide();
                meny.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Rgistr_in_BD rgistr_In_BD = new Rgistr_in_BD();
            this.Hide();
            rgistr_In_BD.ShowDialog();
            this.Show();
            
        }

        private void inRgister_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
