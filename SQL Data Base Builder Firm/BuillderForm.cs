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
using System.Diagnostics;

namespace SQL_Data_Base_Builder_Firm
{
    enum RowState
    {
        Existet,
        New,
        Modified,
        ModifiedNew,
        Deleted

    }
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();

        int selectRow;
        public Form1()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("Фамилия", "Фамилия");
            dataGridView1.Columns.Add("Имя", "Имя");
            dataGridView1.Columns.Add("Отчество", "Отчество");
            dataGridView1.Columns.Add("Телефон", "Телефон");
            dataGridView1.Columns.Add("Примечание", "Примечание");
           
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ClearFields()
        {
            textBox_id.Text = "";
            textBox_FirstNum.Text = "";
            textBox_MidleNum.Text = "";
            textBox_EndNum.Text = "";
            textBox_Phone.Text = "";
            textBox_Note.Text = "";
            
            
           
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0),record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), RowState.ModifiedNew);

        }
        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from Jobs";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader =  command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);

            }
            reader.Close();

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewBuilder newBuilder = new NewBuilder();
            newBuilder.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectRow];
                textBox_id.Text = row.Cells[0].Value.ToString();
                textBox_FirstNum.Text = row.Cells[1].Value.ToString();
                textBox_MidleNum.Text = row.Cells[2].Value.ToString();
                textBox_EndNum.Text = row.Cells[3].Value.ToString();
                textBox_Phone.Text = row.Cells[4].Value.ToString();
                textBox_Note.Text = row.Cells[5].Value.ToString();

                
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView1);
            ClearFields();
        }


        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from Jobs where concat(id,Фамилия, Имя, Отчество, Телефон, Примечание) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(search, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty) 
            {
                dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
            
        }
        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var idOne = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Jobs where id = {idOne}  ";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var fam = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var otchestvo = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var pol = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var primeh = dataGridView1.Rows[index].Cells[5].Value.ToString();


                    var changeQuery = $"update Jobs set Фамилия = '{fam}', Имя = '{name}', Отчество = '{otchestvo}', Телефон = '{pol}', Примечание = '{primeh}' where id = '{id}' ";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBase.closeConnection();
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = textBox_id.Text;
            var fam = textBox_FirstNum.Text;
            var nam = textBox_MidleNum.Text;
            var oth = textBox_EndNum.Text;
            var phon = textBox_Phone.Text;
            var not = textBox_Note.Text;
            
            

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                
                dataGridView1.Rows[selectedRowIndex].SetValues(id,fam, nam, oth, phon, not);
                dataGridView1.Rows[selectedRowIndex].Cells[6].Value = RowState.Modified;
            }

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void textBox_id_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
