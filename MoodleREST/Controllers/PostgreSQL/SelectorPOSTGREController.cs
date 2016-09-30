using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data.Odbc;
using Npgsql;
using System.Data;

namespace MoodleREST.Controllers.PostgreSQL
{
    public class SelectorPOSTGREController : ApiController
    {
        [HttpPost]
        [System.Web.Mvc.ValidateInput(false)]
        public List<object> postSelect()
        {
            List<object> listaResultados;
            List<object> listaResultados2 = new List<object>();
            try
            {
                int selectedIndex = int.Parse(System.Web.HttpContext.Current.Request["connectionIndex"]);
                String[,] connectionStrings = (String[,])System.Web.HttpContext.Current.Application["ConnectionStrings"];
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionStrings[selectedIndex, 0]))
                {
                    connection.Open();
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(new NpgsqlCommand(System.Web.HttpContext.Current.Request["query"], connection));
                    DataTable dta = new DataTable();
                    adapter.Fill(dta);
                    listaResultados = new List<object>();
                    foreach (DataColumn dtc in dta.Columns)
                    {
                        listaResultados.Add(dtc.ToString().ToUpper());
                    }
                    listaResultados2.Add(listaResultados);
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

        // Deprecated

        public List<object> getSelect()
        {
            List<object> listaResultados;
            List<object> listaResultados2 = new List<object>();
            try
            {
                using (OdbcConnection connection = new OdbcConnection(System.Web.HttpContext.Current.Request["connectionString"]))
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