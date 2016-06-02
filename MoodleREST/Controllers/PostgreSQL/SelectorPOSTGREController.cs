using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Odbc;
using System.Data;

namespace MoodleREST.Controllers.PostgreSQL
{
    public class SelectorPOSTGREController : ApiController
    {
        [System.Web.Http.HttpPost]
        public List<object> postSelect()
        {
            List<object> listaResultados;
            List<object> listaResultados2 = new List<object>();
            try
            {
                using (OdbcConnection connection = new OdbcConnection(System.Web.HttpContext.Current.Request["dbconnector"]))
                {
                    connection.Open();
                    OdbcDataAdapter adapter = new OdbcDataAdapter(new OdbcCommand(System.Web.HttpContext.Current.Request["query"], connection));
                    DataTable dta = new DataTable();
                    adapter.Fill(dta);
                    foreach (DataRow dr in dta.Rows)
                    {
                        listaResultados = new List<object>();
                        for (int i = 0; i < dr.ItemArray.Length; i++)
                        {
                            listaResultados.Add(dr.ItemArray[i].ToString());
                        }
                        listaResultados2.Add(listaResultados);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                listaResultados2 = new List<object>();
                listaResultados2.Add(ex.ToString());
            }
            return listaResultados2;
        }

        public List<object> getSelect()
        {
            List<object> listaResultados;
            List<object> listaResultados2 = new List<object>();
            try
            {
                using (OdbcConnection connection = new OdbcConnection(System.Web.HttpContext.Current.Request["dbconnector"]))
                {
                    connection.Open();
                    OdbcDataAdapter adapter = new OdbcDataAdapter(new OdbcCommand(String.Format("select * from {0}", System.Web.HttpContext.Current.Request["tblname"]), connection));
                    DataTable dta = new DataTable();
                    adapter.Fill(dta);
                    foreach (DataRow dr in dta.Rows)
                    {
                        listaResultados = new List<object>();
                        for (int i = 0; i < dr.ItemArray.Length; i++)
                        {
                            listaResultados.Add(dr.ItemArray[i].ToString());
                        }
                        listaResultados2.Add(listaResultados);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                listaResultados2 = new List<object>();
                listaResultados2.Add(ex.ToString());
            }
            return listaResultados2;
        }
    }
}
