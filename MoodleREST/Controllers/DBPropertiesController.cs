using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace MoodleREST.Controllers
{
    public class DBPropertiesController : ApiController
    {
        DataSet ds = new DataSet();
        public List<String> GetConnectionQueryFromXML()
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["DBProperties"] == null)
                {
                    ds.ReadXml(AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/DBProperties.xml");
                    System.Web.HttpContext.Current.Application.Add("DBProperties", ds);
                }
                ds = (DataSet)System.Web.HttpContext.Current.Application["DBProperties"];
                int selectedIndex = int.Parse(HttpContext.Current.Request["index"]);
                int rowsQuantity = ds.Tables[0].Rows[selectedIndex].ItemArray.Count();
                List<String> returnList = new List<String>();
                for (int i = 0; i < rowsQuantity; i++)
                {
                    returnList.Add(ds.Tables[0].Rows[selectedIndex][i].ToString());
                }
                return returnList;
            } catch (Exception ex)
            {
                return new List<String>() { ex.ToString() };
            }

        }
    }
}
