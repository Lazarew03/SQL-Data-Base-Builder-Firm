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

    public partial class BrigadS : Form
    {
        enum RowState
        {
            Existet,
            New,
            Modified,
            ModifiedNew,
            Deleted

        }
        DataBase dataBase = new DataBase();
        int selectRow;
        public BrigadS()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {

            dataGridView2.Columns.Add("КодБригады", "КодБригады");
            dataGridView2.Columns.Add("Название", "Название");
            dataGridView2.Columns.Add("Бригадир", "Бригадир");
            dataGridView2.Columns.Add("Примечание", "Примечание");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void ClearFields()
        {
            textBox_CodBrigadS.Text = "";
            textBox_Name.Text = "";
            textBox_Brigadir.Text = "";
            textBox_Primeh.Text = "";




        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState.ModifiedNew);

        }
        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from BrigadS";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);

            }
            reader.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from BrigadS where concat(КодБригады, Название, Бригадир, Примечание) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(search, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();


        }
        private void BrigadS_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView2);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var idOne = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from BrigadS where КодБригады = {idOne}  ";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var fam = dataGridView2.Rows[index].Cells[1].Value.ToString();
                    var name = dataGridView2.Rows[index].Cells[2].Value.ToString();
                    var otchestvo = dataGridView2.Rows[index].Cells[3].Value.ToString();
                   


                    var changeQuery = $"update BrigadS set Название = '{fam}', Бригадир = '{name}', Примечание = '{otchestvo}' where КодБригады = '{id}' ";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBase.closeConnection();
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;

            var id = textBox_CodBrigadS.Text;
            var fam = textBox_Name.Text;
            var nam = textBox_Brigadir.Text;
            var oth = textBox_Primeh.Text;
            
            if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {

                dataGridView2.Rows[selectedRowIndex].SetValues(id, fam, nam, oth);
                dataGridView2.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
            }

        }
        private void deleteRow()
        {
            int index = dataGridView2.CurrentCell.RowIndex;

            dataGridView2.Rows[index].Visible = false;

            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[4].Value = RowState.Deleted;
                return;
            }
            dataGridView2.Rows[index].Cells[4].Value = RowState.Deleted;
        }
        private void FirstName_label_Click(object sender, EventArgs e)
        {

        }

        private void NewNote_button_Click(object sender, EventArgs e)
        {
            NewBrigadS newBrigadS = new NewBrigadS();
            newBrigadS.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView2);
            ClearFields();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView2);
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void textBox_CodBrigadS_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectRow];

                textBox_CodBrigadS.Text = row.Cells[0].Value.ToString();
                textBox_Name.Text = row.Cells[1].Value.ToString();
                textBox_Brigadir.Text = row.Cells[2].Value.ToString();
                textBox_Primeh.Text = row.Cells[3].Value.ToString();


            }
        }
    }
}
