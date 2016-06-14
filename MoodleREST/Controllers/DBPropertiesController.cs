using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Http;
using MoodleREST.Controllers.Queries_and_C_String;
using Newtonsoft.Json;

namespace MoodleREST.Controllers
{
    public class DBPropertiesController : ApiController
    {
        public String GetConnectionQueryFromDatabase() {
            try
            {
                int selectedIndex = int.Parse(HttpContext.Current.Request["index"]);
                if (HttpContext.Current.Application["ConnectionStrings"] == null) {
                    HttpContext.Current.Application.Add("ConnectionStrings", ConnectionGetterMYSQL.returnTable("Server=localhost;Database=dbproperties;Uid=user"));
                }

                if(HttpContext.Current.Application["Queries"] == null) {
                    HttpContext.Current.Application.Add("Queries", QueriesGetterMYSQL.returnTable("Server=localhost;Database=dbproperties;Uid=user"));
                }
                List<Object> returnList = new List<Object>();
                String[] connectionStr = (String[])HttpContext.Current.Application["ConnectionStrings"];
                String connectionStrSelected = connectionStr[selectedIndex];
                selectedIndex++;
                DataTable dt = (DataTable)HttpContext.Current.Application["Queries"];
                return "[{\"connectionString\":\"" + connectionStrSelected + "\"}," + JsonConvert.SerializeObject(dt.Select("database_id=" + selectedIndex.ToString()).CopyToDataTable()).Substring(1);
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
