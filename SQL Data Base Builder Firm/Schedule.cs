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
    public partial class Schedule : Form
    {
        enum RowState
        {
            Existet,
            New,
            Modified,
            ModifiedNew,
            Deleted

        }
        public Schedule()
        {
            InitializeComponent();
        }
        DataBase dataBase = new DataBase();

        int selectRow;
       
        private void CreateColumns()
        {

            dataGridView1.Columns.Add("NumПП", "№ ПП");
            dataGridView1.Columns.Add("Объект", "Объект");
            dataGridView1.Columns.Add("ОписаниеРабот", "Описание работ");
            dataGridView1.Columns.Add("КодБригады", "Код бригады");
            dataGridView1.Columns.Add("С", "С");
            dataGridView1.Columns.Add("По", "По");
            dataGridView1.Columns.Add("Примечание", "Примечание");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetInt32(3), record.GetString(4), record.GetString(5), record.GetString(6),RowState.ModifiedNew);

        }
        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from Schedule";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);

            }
            reader.Close();

        }
        private void Schedule_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView1);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from Schedule where concat(NumПП,Объект, ОписаниеРабот, КодБригады, С, По, Примечание) like '%" + textBox1.Text + "%'";
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
            RefresDataGrid(dataGridView1);
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NewNote_button_Click(object sender, EventArgs e)
        {
            NewSchedule schedule= new NewSchedule();
            schedule.Show();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectRow];

                textBox_NumPP.Text = row.Cells[0].Value.ToString();
                textBox_Objekt.Text = row.Cells[1].Value.ToString();
                textBox_JobsData.Text = row.Cells[2].Value.ToString();
                textBox_CodeBrigad.Text = row.Cells[3].Value.ToString();
                textBox_In.Text = row.Cells[4].Value.ToString();
                textBox_On.Text = row.Cells[5].Value.ToString();
                textBox_In.Text = row.Cells[6].Value.ToString();

            }
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = textBox_NumPP.Text;
            var fam = textBox_Objekt.Text;
            var jobdat = textBox_JobsData.Text;
            var nam = textBox_CodeBrigad.Text;
            var oth = textBox_In.Text;
            var phon = textBox_On.Text;
            var not = textBox_Note.Text;
            



            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {

                dataGridView1.Rows[selectedRowIndex].SetValues(id, fam,jobdat, nam, oth, phon, not);
                dataGridView1.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Change();
        }
        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var idOne = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Schedule where NumПП = {idOne}  ";

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
                    var not = dataGridView1.Rows[index].Cells[6].Value.ToString();

                    var changeQuery = $"update Schedule set Объект = '{fam}', ОписаниеРабот = '{name}', КодБригады = '{otchestvo}', С = '{pol}', По = '{primeh}', Примечание = '{not}' where NumПП = '{id}' ";

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

        private void Delete_button_Click(object sender, EventArgs e)
        {
            deleteRow();
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
            dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }
    }
}
