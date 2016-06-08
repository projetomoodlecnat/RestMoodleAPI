using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;

namespace MoodleREST.Controllers.MSSQL
{
    public class SelectorMSSQLController : ApiController
    {
        public class SelectorMYSQLController : ApiController
        {
            [System.Web.Http.HttpPost]
            public List<object> postSelect()
            {
                List<object> listaResultados;
                List<object> listaResultados2 = new List<object>();
                try
                {
                    using (SqlConnection connection = new SqlConnection(System.Web.HttpContext.Current.Request["connectionString"]))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(System.Web.HttpContext.Current.Request["query"], connection));
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
                    listaResultados = new List<object>();
                    listaResultados.Add(ex.ToString());
                }
                return listaResultados2;
            }

            public List<object> getSelect()
            {
                List<object> listaResultados;
                List<object> listaResultados2 = new List<object>();
                try
                {
                    using (SqlConnection connection = new SqlConnection(System.Web.HttpContext.Current.Request["connectionString"]))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(String.Format("select * from {0}", System.Web.HttpContext.Current.Request["tblname"]), connection));
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
}
