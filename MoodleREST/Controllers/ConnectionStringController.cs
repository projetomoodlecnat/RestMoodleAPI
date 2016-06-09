﻿using System;
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
                if (System.Web.HttpContext.Current.Application["ConnectionStrings"] == null)
                {
                    ds.ReadXml(AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/ConnectionStrings.xml");
                    System.Web.HttpContext.Current.Application.Add("ConnectionStrings", ds);
                }
                ds = (DataSet)System.Web.HttpContext.Current.Application["ConnectionStrings"];
                int selectedIndex = int.Parse(HttpContext.Current.Request["connectionStringIndex"]);
                return new String[] { ds.Tables[0].Rows[selectedIndex][0].ToString(), ds.Tables[0].Rows[selectedIndex][1].ToString(), ds.Tables[0].Rows[selectedIndex][2].ToString(), ds.Tables[0].Rows[selectedIndex][3].ToString() };
            } catch (Exception ex)
            {
                return new String[] { ex.ToString() };
            }

        }
    }
}
