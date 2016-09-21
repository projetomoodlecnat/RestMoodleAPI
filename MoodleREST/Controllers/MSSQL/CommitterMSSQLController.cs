using System;
using System.Data.SqlClient;
using System.Web.Http;

namespace MoodleREST.Controllers.MSSQL
{
    public class CommitterMSSQLController : ApiController
    {
        [HttpPost]
        public String postCommit()
        {
            try
            {
                int selectedIndex = int.Parse(System.Web.HttpContext.Current.Request["connectionIndex"]);
                String[,] connectionStrings = (String[,])System.Web.HttpContext.Current.Application["ConnectionStrings"];
                using (SqlConnection connection = new SqlConnection(connectionStrings[selectedIndex, 0]))
                {
                    connection.Open();
                    SqlCommand msc = new SqlCommand(System.Web.HttpContext.Current.Request["query"], connection);
                    String rowsAffected = msc.ExecuteNonQuery().ToString();
                    connection.Close();
                    return String.Format("Commit efetuado com sucesso. {0} linhas afetadas.", rowsAffected);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
