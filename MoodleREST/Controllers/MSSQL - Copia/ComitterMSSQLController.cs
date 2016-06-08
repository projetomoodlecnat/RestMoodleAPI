using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoodleREST.Controllers.MSSQL
{
    public class ComitterMSSQLController : ApiController
    {
        [System.Web.Http.HttpPost]
        public String postCommit()
        {
            try
            {
                SqlConnection connection = new SqlConnection(System.Web.HttpContext.Current.Request["connectionString"]);
                connection.Open();
                SqlCommand command = new SqlCommand(System.Web.HttpContext.Current.Request["query"], connection);
                int linhasAfetadas = command.ExecuteNonQuery();
                connection.Close();
                return String.Format("Query executada com sucesso. {0} linhas afetadas.", linhasAfetadas);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
