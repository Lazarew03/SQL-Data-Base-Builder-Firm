using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQL_Data_Base_Builder_Firm
{
    internal class DataBase
    {
        


        SqlConnection conn = new SqlConnection("Data Source=LAZURICH;Initial Catalog=building_firm; Integrated Security = true");

        public void openConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
           
        }
        public void closeConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        
        public SqlConnection getConnection()
        {
            return conn;
        }
        
    }
}
