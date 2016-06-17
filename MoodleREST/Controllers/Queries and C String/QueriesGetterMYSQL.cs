using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MoodleREST.Controllers.Queries_and_C_String
{
    public static class QueriesGetterMYSQL
    {
        public static DataTable returnTable(String connectionString)
        {
            using (MySqlConnection msc = new MySqlConnection(connectionString))
            {
                try
                {
                    msc.Open();
                    DataTable dta = new DataTable();
                    new MySqlDataAdapter("select * from `database_queries`", msc).Fill(dta);
                    return dta;
                }
                catch (Exception ex)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("SQLException", typeof(String));
                    table.Rows.Add(ex.Message);
                    return table;
                }
            }
        }
    }
}