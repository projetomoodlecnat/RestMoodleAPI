using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;
using CryptSharp;
using System.Reflection;

namespace MoodleREST.Controllers
{
    public class AuthenticateController : ApiController
    {
        [HttpPost]
        public String postSelector()
        {
            String output = "";
            try
            {
                int selectedIndex = int.Parse(HttpContext.Current.Request["connectionIndex"]);
                String[,] connectionStrings = (String[,])HttpContext.Current.Application["ConnectionStrings"];
                switch (HttpContext.Current.Request["databaseType"])
                {
                    case "mysql":
                        break;
                    case "postgre":
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionStrings[selectedIndex, 0]))
                        {
                            connection.Open();
                            NpgsqlCommand sqc = new NpgsqlCommand("select username, password from mdl_user where username=@username", connection);
                            sqc.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Varchar, HttpContext.Current.Request["username"].ToString());
                            sqc.Prepare();
                            NpgsqlDataReader msqr = sqc.ExecuteReader();
                            if (msqr.Read())
                            {
                                String passwordFromDB = msqr.GetString(1);
                                String cryptedPass = passwordFromDB.Substring(passwordFromDB.LastIndexOf(".") + 1);
                                String hash = passwordFromDB.Substring(0, passwordFromDB.LastIndexOf("."));
                                Crypter crypter = Crypter.GetCrypter(passwordFromDB);
                                PropertyInfo pi = crypter.Properties.GetType().GetProperties()[3];
                                pi.SetValue(crypter.Properties, false, null);
                                crypter.Properties.Add(CrypterOption.Variant, BlowfishCrypterVariant.Corrected);
                                if (cryptedPass.Equals(crypter.Crypt(HttpContext.Current.Request["username"], hash)))
                                {
                                    output = "user_authenticated";
                                }
                                else
                                {
                                    output = "user_login_failed";
                                }
                            }
                            else
                            {
                                output = "non_existant_user";
                            }
                            connection.Close();
                        }
                        break;
                    case "mssql":
                        break;
                    default:
                        throw new ArgumentException("Argumento de tipo de base inválido!");
                }
            }
            catch (Exception ex)
            {
                output = "error";
                Console.WriteLine(ex.Message);
            }
            return output;
        }
    }
}
