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
    public class ConnectionStringController : ApiController
    {
        DataSet ds = new DataSet();
        public String[] GetConnectionQueryFromXML()
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["ds"] == null)
                {
                    ds.ReadXml(AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/QuerySelector.xml");
                    System.Web.HttpContext.Current.Application.Add("ds", ds);
                }
                ds = (DataSet)System.Web.HttpContext.Current.Application["ds"];
                return new String[] { ds.Tables[0].Rows[int.Parse(Request.RequestUri.Query.Substring(this.Request.RequestUri.Query.IndexOf('=') + 1))][0].ToString(), ds.Tables[0].Rows[int.Parse(Request.RequestUri.Query.Substring(this.Request.RequestUri.Query.IndexOf('=') + 1))][1].ToString() };
            } catch (Exception ex)
            {
                return new String[] { ex.ToString() };
            }

        }
    }
}
