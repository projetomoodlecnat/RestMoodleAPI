using System;
using System.Data;
using System.Web;
using System.Web.Http;
using MoodleREST.Controllers.Queries_and_C_String;
using Newtonsoft.Json;

namespace MoodleREST.Controllers
{
    public class DBPropertiesController : ApiController
    {
        public String GET_DatabaseTypeAndQueries() {
            try
            {
                int selectedIndex = int.Parse(HttpContext.Current.Request["index"]);
                String databaseType = ConnectionGetterMYSQL.returnTable(selectedIndex, "Server=localhost;Database=dbproperties;Uid=user");
                if(HttpContext.Current.Application["Queries"] == null) {
                    HttpContext.Current.Application.Add("Queries", QueriesGetterMYSQL.returnTable("Server=localhost;Database=dbproperties;Uid=user"));
                }
                selectedIndex++;
                DataTable dt = (DataTable)HttpContext.Current.Application["Queries"];
                return "[{\"databaseType\":\"" + databaseType + "\"}," + JsonConvert.SerializeObject(dt.Select("idconexao=" + selectedIndex.ToString()).CopyToDataTable()).Substring(1);
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
