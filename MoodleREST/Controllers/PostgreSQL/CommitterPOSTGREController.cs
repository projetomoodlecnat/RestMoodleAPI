using System;
using System.Web.Http;
using Npgsql;

namespace MoodleREST.Controllers.PostgreSQL
{
    public class CommitterPOSTGREController : ApiController
    {
        [System.Web.Http.HttpPost]
        public String postCommit()
        {
            try
            {
                int selectedIndex = int.Parse(System.Web.HttpContext.Current.Request["connectionIndex"]);
                String[,] connectionStrings = (String[,])System.Web.HttpContext.Current.Application["ConnectionStrings"];
                NpgsqlConnection connection = new NpgsqlConnection(connectionStrings[selectedIndex, 0]);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(System.Web.HttpContext.Current.Request["query"], connection);
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
