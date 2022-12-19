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

    public partial class Brigad : Form
    {
        DataBase dataBase = new DataBase();
        int selectRow;
        enum RowState
        {
            Existet,
            New,
            Modified,
            ModifiedNew,
            Deleted

        }
        public Brigad()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {

            dataGridView2.Columns.Add("КодРабочего", "КодРабочего");
            dataGridView2.Columns.Add("КодБригады", "КодБригады");
            dataGridView2.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), RowState.ModifiedNew);

        }
        private void ClearFields()
        {
            textBox_CodeBrigad.Text = "";
            textBox_CodeJobs.Text = "";
           



        }
        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from Brigad";
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

        private void Brigad_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView2);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectRow];

                textBox_CodeBrigad.Text = row.Cells[0].Value.ToString();
                textBox_CodeJobs.Text = row.Cells[1].Value.ToString();
               

            }
        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from Brigad where concat(КодБригады, КодРабочего) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(search, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView2);
            ClearFields();
        }

        private void FirstName_label_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView2);
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
        private void NewNote_button_Click(object sender, EventArgs e)
        {
            NewBrigadcs newBrigad = new NewBrigadcs();
            newBrigad.Show();
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
                    var deleteQuery = $"delete from Brigad where КодБригады = {Name}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {

                    var fam = dataGridView2.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView2.Rows[index].Cells[1].Value.ToString();
                   
                   
                    var changeQuery = $"update Brigad set КодРабочего = '{name}' where КодБригады = '{fam}' ";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBase.closeConnection();
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;

            var brig = textBox_CodeBrigad.Text;
            var job = textBox_CodeJobs.Text;
           

            if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {

                dataGridView2.Rows[selectedRowIndex].SetValues(brig, job);
                dataGridView2.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
            }

        }
        private void Delete_button_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }
    }
}
