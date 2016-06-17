using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web;

namespace MoodleREST.Controllers.Queries_and_C_String
{
    public static class ConnectionGetterMYSQL
    {
        public static String returnTable(int selectedIndex, String connectionString)
        {
            using (MySqlConnection msc = new MySqlConnection(connectionString))
            {
                try
                {
                    String[,] results = (String[,])HttpContext.Current.Application["ConnectionStrings"];
                    if (HttpContext.Current.Application["ConnectionStrings"] == null) {
                        msc.Open();
                        DataTable dta = new DataTable();
                        new MySqlDataAdapter("select * from `databases`", msc).Fill(dta);
                        results = new String[dta.Rows.Count, 2];
                        int resultsElement = 0;
                        foreach (DataRow dr in dta.Rows)
                        {
                            switch (dr["database_type"].ToString())
                            {
                                case "mysql":
                                    if (dr["Pwd"].ToString().Equals(""))
                                    {
                                        results[resultsElement, 0] = dr["connectionString"].ToString() + "User=" + dr["User"].ToString();
                                        results[resultsElement, 1] = dr["database_type"].ToString();
                                    }
                                    else
                                    {
                                        results[resultsElement, 0] = dr["connectionString"].ToString() + "User=" + dr["User"].ToString() + ";Pwd=" + dr["Pwd"];
                                        results[resultsElement, 1] = dr["database_type"].ToString();
                                    }
                                    break;
                                case "postgre":
                                    if (dr["Pwd"].ToString().Equals(""))
                                    {
                                        results[resultsElement, 0] = dr["connectionString"].ToString() + "User Id=" + dr["User"].ToString();
                                        results[resultsElement, 1] = dr["database_type"].ToString();
                                    }
                                    else
                                    {
                                        results[resultsElement, 0] = dr["connectionString"].ToString() + "User Id=" + dr["User"].ToString() + ";Pwd=" + dr["Pwd"];
                                        results[resultsElement, 1] = dr["database_type"].ToString();
                                    }
                                    break;
                                default:
                                    //nome inválido ou não suportado
                                    break;
                            }
                            resultsElement++;
                        }
                        HttpContext.Current.Application["ConnectionStrings"] = results;
                    }
                    return results[selectedIndex, 1].ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
