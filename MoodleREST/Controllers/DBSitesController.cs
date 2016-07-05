using System.Web.Http;
using MoodleREST.Controllers.Queries_and_C_String;
using System;
using System.Web;

namespace MoodleREST.Controllers
{
    public class DBSitesController : ApiController
    {
        [System.Web.Http.HttpGet]
        public String[] sitesGetter() {
            return SitesGetterMYSQL.returnSitesAssociatedWithDatabaseID("Server=localhost;Database=dbproperties;Uid=user", System.Web.HttpContext.Current.Request["databaseIndex"]);
        }
    }
}
