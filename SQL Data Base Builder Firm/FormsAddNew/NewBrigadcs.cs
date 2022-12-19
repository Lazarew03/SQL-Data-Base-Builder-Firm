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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQL_Data_Base_Builder_Firm
{
    public partial class NewBrigadcs : Form
    {
        DataBase data = new DataBase();
        public NewBrigadcs()
        {
            InitializeComponent();
        }

        private void NewBrigadcs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.openConnection();

            var firsNum = textBox1.Text;
            var MidleNum = textBox2.Text;
           

            var addQuare = $"insert into Brigad(КодБригады, КодРабочего) values ('{firsNum}','{MidleNum}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Бригадир добавлен");
            data.closeConnection();
        }
    }
}
