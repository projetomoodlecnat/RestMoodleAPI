using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MoodleREST.Controllers
{
    public class QueriesController : ApiController
    {
        DataSet ds = new DataSet();
        public String[] GetConnectionQueryFromXML()
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["queries"] == null)
                {
                    ds.ReadXml(AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/Queries.xml");
                    System.Web.HttpContext.Current.Application.Add("queries", ds);
                }
                ds = (DataSet)System.Web.HttpContext.Current.Application["queries"];
                int selectedIndex = int.Parse(HttpContext.Current.Request["queryIndex"]);
                return new String[] { ds.Tables[0].Rows[selectedIndex][0].ToString() };
            }
            catch (Exception ex)
            {
                return new String[] { ex.ToString() };
            }

        }
    }
}
