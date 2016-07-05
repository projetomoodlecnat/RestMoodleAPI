using MySql.Data.MySqlClient;
using System;
using System.Web.Http;

namespace MoodleREST.Controllers.MYSQL
{
    public class CommitterMYSQLController : ApiController
    {
        [System.Web.Http.HttpPost]
        public String postCommit()
        {
            try
            {
                int selectedIndex = int.Parse(System.Web.HttpContext.Current.Request["connectionIndex"]);
                String[,] connectionStrings = (String[,])System.Web.HttpContext.Current.Application["ConnectionStrings"];
                using (MySqlConnection connection = new MySqlConnection(connectionStrings[selectedIndex, 0]))
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
