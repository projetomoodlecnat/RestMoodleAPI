using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MoodleREST.Controllers.Queries_and_C_String
{
    public static class ConnectionGetterMYSQL
    {
        public static String[] returnTable(String connectionString)
        {
            using (MySqlConnection msc = new MySqlConnection(connectionString))
            {
                try
                {
                    msc.Open();
                    DataTable dta = new DataTable();
                    new MySqlDataAdapter("select * from `databases`", msc).Fill(dta);
                    String[] results = new String[dta.Rows.Count];
                    int resultsElement = 0;
                    foreach (DataRow dr in dta.Rows)
                    {
                        if (dr["Pwd"].ToString().Equals(""))
                        {
                            results[resultsElement] = dr["connectionString"].ToString() + "User=" + dr["User"].ToString();
                        } else
                        {
                            results[resultsElement] = dr["connectionString"].ToString() + "User=" + dr["User"].ToString() + ";Pwd=" + dr["Pwd"];
                        }
                        resultsElement++;
                    }
                    return results;
                }
                catch (Exception ex)
                {
                    String[] error = new String[1];
                    error[0] = ex.Message;
                    return error;
                }
            }
        }
    }
}
