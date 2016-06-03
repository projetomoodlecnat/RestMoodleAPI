using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Odbc;

namespace MoodleREST.Controllers.PostgreSQL
{
    public class ComitterPOSTGREController : ApiController
    {
        [System.Web.Http.HttpPost]
        public String postCommit()
        {
            try
            {
                OdbcConnection connection = new OdbcConnection(System.Web.HttpContext.Current.Request["connectionString"]);
                connection.Open();
                OdbcCommand command = new OdbcCommand(System.Web.HttpContext.Current.Request["query"], connection);
                int linhasAfetadas = command.ExecuteNonQuery();
                connection.Close();
                return String.Format("Query executada com sucesso. {0} linhas afetadas.", linhasAfetadas);
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
