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

namespace SQL_Data_Base_Builder_Firm.FormsAddNew
{
    public partial class NewTypesObjects : Form
    {
        DataBase  data = new DataBase();
        public NewTypesObjects()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.openConnection();

            var firsNum = textBox1.Text;
            var MidleNum = textBox2.Text;


            var addQuare = $"insert into TypesObjects(КодТипа, НаименованиеТипа) values ('{firsNum}','{MidleNum}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Тип объекта добавлен");
            data.closeConnection();
        }

        private void NewTypesObjects_Load(object sender, EventArgs e)
        {

        }
    }
}
