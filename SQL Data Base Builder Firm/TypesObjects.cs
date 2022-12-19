using SQL_Data_Base_Builder_Firm.FormsAddNew;
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
    public partial class TypesObjects : Form
    {
        DataBase dataBase = new DataBase();
        int selectRow;
        public TypesObjects()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {

            dataGridView2.Columns.Add("КодТипа", "КодТипа");
            dataGridView2.Columns.Add("НаименованиеТипа", "НаименованиеТипа");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiedNew);

        }
        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from TypesObjects";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);

            }
            reader.Close();

        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from TypesObjects where concat(КодТипа, НаименованиеТипа) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(search, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TypesObjects_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView2);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView2);
        }

        private void FirstName_label_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectRow];

                textBox_CodeType.Text = row.Cells[0].Value.ToString();
                textBox_NameType.Text = row.Cells[1].Value.ToString();


            }
        }

        private void NewNote_button_Click(object sender, EventArgs e)
        {
            NewTypesObjects newTypes = new NewTypesObjects();
            newTypes.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView2);
        }
        private void deleteRow()
        {
            int index = dataGridView2.CurrentCell.RowIndex;

            dataGridView2.Rows[index].Visible = false;

            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[2].Value = RowState.Deleted;
                return;
            }
            dataGridView2.Rows[index].Cells[2].Value = RowState.Deleted;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView2);
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            deleteRow();
        }
        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Name = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from TypesObjects where КодТипа = {Name}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {

                    var fam = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView2.Rows[index].Cells[1].Value.ToString();


                    var changeQuery = $"update TypesObjects set НаименованиеТипа = '{name}' where КодТипа = '{fam}' ";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBase.closeConnection();
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Update();
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;

            var brig = textBox_CodeType.Text;
            var job = textBox_NameType.Text;


            if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView2.Rows[selectedRowIndex].SetValues(brig, job);
                dataGridView2.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
        }
    }
}
