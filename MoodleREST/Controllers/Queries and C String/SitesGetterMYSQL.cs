using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace MoodleREST.Controllers.Queries_and_C_String
{
    public static class SitesGetterMYSQL
    {
        public static String[] returnSitesAssociatedWithDatabaseID (String connectionString, String id)
        {
            try
            {
                using (MySqlConnection msqcon = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter msda = new MySqlDataAdapter("select * from `database_sites` where id=" + id, msqcon);
                    DataTable dta = new DataTable();
                    msda.Fill(dta);
                    String[] results = new String[dta.Rows.Count];
                    int i = 0;
                    foreach (DataRow dt in dta.Rows)
                    {
                        results[i] = dt.ItemArray[1].ToString();
                        i++;
                    }
                    return results;
                }
            } catch (Exception ex)
            {
                return new String[] { ex.Message };
            }
        }
    }
}