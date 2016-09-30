using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoodleREST.Controllers.MSSQL
{
    public class SelectorMSSQLController : ApiController
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
                using (SqlConnection connection = new SqlConnection(connectionStrings[selectedIndex, 0]))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(System.Web.HttpContext.Current.Request["query"], connection));
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
                listaResultados = new List<object>();
                listaResultados.Add(ex.ToString());
            }
            return listaResultados2;
        }
    }
}
