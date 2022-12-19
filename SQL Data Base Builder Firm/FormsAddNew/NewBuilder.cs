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
    public partial class NewBuilder : Form
    {
        DataBase data = new DataBase();
        public NewBuilder()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.openConnection();

            var firsNum = textBox1.Text;
            var MidleNum = textBox2.Text;
            var EndNum = textBox3.Text;
            var Phone = textBox4.Text;
            var Note = textBox5.Text;

            var addQuare = $"insert into Jobs(Фамилия, Имя, Отчество, Телефон, Примечание) values ('{firsNum}','{MidleNum}','{EndNum}','{Phone}','{Note}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Рабочий добавлен");
            data.closeConnection();
        }

        private void NewBuilder_Load(object sender, EventArgs e)
        {

        }
    }
}
