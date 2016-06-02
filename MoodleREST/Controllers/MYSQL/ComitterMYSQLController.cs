using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoodleREST.Controllers.MYSQL
{
    public class ComitterMYSQLController : ApiController
    {
        [System.Web.Http.HttpPost]
        public String postCommit()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(System.Web.HttpContext.Current.Request["dbconnector"]))
                {
                    connection.Open();
                    MySqlCommand msc = new MySqlCommand(System.Web.HttpContext.Current.Request["query"], connection);
                    String rowsAffected = msc.ExecuteNonQuery().ToString();
                    connection.Close();
                    return String.Format("Commit realizado com sucesso. {0} linhas afetadas.", rowsAffected);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
