﻿using System;
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
    public partial class NewBrigadS : Form
    {
        DataBase data = new DataBase();
        public NewBrigadS()
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
            

            var addQuare = $"insert into BrigadS(КодБригады, Название, Бригадир, Примечание) values ('{firsNum}','{MidleNum}','{EndNum}','{Phone}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Бригада добавлена");
            data.closeConnection();
        }
    }
}
