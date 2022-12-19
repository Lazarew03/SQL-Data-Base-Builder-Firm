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

namespace SQL_Data_Base_Builder_Firm
{
    public partial class Rgistr_in_BD : Form
    {
        DataBase dataBase = new DataBase();
        public Rgistr_in_BD()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Rgistr_in_BD_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Сheckгser())

            {
                return;
            }
            var login = textBox1.Text;
            var password = textBox2.Text;
            string quer = $"insert into register(login, password) values('{login}', '{password}')";
            SqlCommand command = new SqlCommand(quer, dataBase.getConnection());

            dataBase.openConnection();
            
            if (command.ExecuteNonQuery()==1  )
            {
                MessageBox.Show("Аккаунт создан");
                inRgister inRgister = new inRgister();
                this.Hide();
                inRgister.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            dataBase.closeConnection();
            

        }
        private Boolean Сheckгser()
        {
            var login = textBox1.Text;
            var password = textBox2.Text;
            

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string quertString = $"select  login, password from register where login = '{login}' and password = '{password}'";
            SqlCommand command = new SqlCommand(quertString, dataBase.getConnection());
            adapter.SelectCommand = command;

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0 )
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
