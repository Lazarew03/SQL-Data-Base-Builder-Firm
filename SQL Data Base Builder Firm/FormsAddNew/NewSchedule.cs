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
    
    public partial class NewSchedule : Form
    {
        DataBase data = new DataBase();
        public NewSchedule()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.openConnection();

            var firsNum = textBox1.Text;
            var MidleNum = textBox2.Text;
            var EndNum = textBox3.Text;
            var Phone = textBox4.Text;
            var Note = textBox5.Text;
            var Po = textBox6.Text;
            var Primech = textBox7.Text;

            var addQuare = $"insert into Schedule(NumПП, Объект, ОписаниеРабот, КодБригады, С, По, Примечание) values ('{firsNum}','{MidleNum}','{EndNum}','{Phone}','{Note}','{Po}','{Primech}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("График работ добавлен");
            data.closeConnection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewSchedule_Load(object sender, EventArgs e)
        {

        }
    }
}
