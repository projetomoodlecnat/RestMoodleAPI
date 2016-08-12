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
using System.Runtime.Serialization;

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
                        using (MySqlConnection connection = new MySqlConnection(connectionStrings[selectedIndex, 0]))
                        {
                            if (HttpContext.Current.Request["password"].Equals(""))
                            {
                                throw new InvalidFieldException("user_password_null");
                            }
                            if (HttpContext.Current.Request["user"].Equals(""))
                            {
                                throw new InvalidFieldException("user_non_existent");
                            }
                            connection.Open();
                            MySqlCommand sqc = new MySqlCommand("select id, username, password from mdl_user where username=@username", connection);
                            MySqlParameter msqp = new MySqlParameter("@username", MySqlDbType.VarChar);
                            msqp.Value = HttpContext.Current.Request["username"].ToString();
                            sqc.Parameters.Add(msqp);
                            sqc.Prepare();
                            MySqlDataReader msqr = sqc.ExecuteReader();
                            if (msqr.Read())
                            {
                                String passwordFromDB = msqr.GetString(2);
                                Crypter crypter = Crypter.GetCrypter(passwordFromDB);
                                if (passwordFromDB.Equals(crypter.Crypt(HttpContext.Current.Request["password"], passwordFromDB.TrimEnd().TrimStart())))
                                {
                                    output = String.Format("user_authenticated;{0};{1}", msqr.GetInt32(0), msqr.GetString(1));
                                }
                                else
                                {
                                    output = "user_login_incorrect";
                                }
                            }
                            else
                            {
                                throw new DataException("user_non_existent");
                            }
                            connection.Close();
                        }
                        break;
                    case "postgre":
                        using (NpgsqlConnection connection = new NpgsqlConnection(connectionStrings[selectedIndex, 0]))
                        {
                            if (HttpContext.Current.Request["password"].Equals(""))
                            {
                                throw new InvalidFieldException("user_password_null");
                            }
                            if (HttpContext.Current.Request["user"].Equals(""))
                            {
                                throw new InvalidFieldException("user_non_existent");
                            }
                            connection.Open();
                            NpgsqlCommand sqc = new NpgsqlCommand("select id, username, password from mdl_user where username=@username", connection);
                            sqc.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Varchar, HttpContext.Current.Request["username"].ToString());
                            sqc.Prepare();
                            NpgsqlDataReader msqr = sqc.ExecuteReader();
                            if (msqr.Read())
                            {
                                String passwordFromDB = msqr.GetString(2);
                                Crypter crypter = Crypter.GetCrypter(passwordFromDB);
                                if (passwordFromDB.Equals(crypter.Crypt(HttpContext.Current.Request["password"], passwordFromDB.TrimEnd().TrimStart())))
                                {
                                    output = String.Format("user_authenticated;{0};{1}", msqr.GetInt32(0), msqr.GetString(1));
                                }
                                else
                                {
                                    output = "user_login_incorrect";
                                }
                            }
                            else
                            {
                                throw new DataException("user_non_existent");
                            }
                            connection.Close();
                        }
                        break;
                    case "mssql":
                        using (SqlConnection connection = new SqlConnection(connectionStrings[selectedIndex, 0]))
                        {
                            if (HttpContext.Current.Request["password"].Equals(""))
                            {
                                throw new InvalidFieldException("user_password_null");
                            }
                            if (HttpContext.Current.Request["user"].Equals(""))
                            {
                                throw new InvalidFieldException("user_non_existent");
                            }
                            connection.Open();
                            SqlCommand sqc = new SqlCommand("select id, username, password from mdl_user where username=@username", connection);
                            sqc.Parameters.AddWithValue("@username", HttpContext.Current.Request["username"].ToString());
                            sqc.Prepare();
                            SqlDataReader msqr = sqc.ExecuteReader();
                            if (msqr.Read())
                            {
                                String passwordFromDB = msqr.GetString(2);
                                Crypter crypter = Crypter.GetCrypter(passwordFromDB);
                                if (passwordFromDB.Equals(crypter.Crypt(HttpContext.Current.Request["password"], passwordFromDB.TrimEnd().TrimStart())))
                                {
                                    output = String.Format("user_authenticated;{0};{1}", msqr.GetInt32(0), msqr.GetString(1));
                                }
                                else
                                {
                                    output = "user_login_incorrect";
                                }
                            }
                            else
                            {
                                throw new DataException("user_non_existent");
                            }
                            connection.Close();
                        }
                        break;
                    default:
                        throw new InvalidFieldException("database_type_invalid");
                }
            }
            catch (Exception ex)
            {
                output = ex.Message;
                Console.WriteLine(ex.Message);
            }
            return output;
        }

        //PropertyInfo pi = crypter.Properties.GetType().GetProperties()[3];
        //pi.SetValue(crypter.Properties, false, null);
        //crypter.Properties.Add(CrypterOption.Variant, BlowfishCrypterVariant.Corrected);

        [Serializable]
        private class InvalidFieldException : Exception
        {
            public InvalidFieldException()
            {
            }

            public InvalidFieldException(string message) : base(message)
            {
            }

            public InvalidFieldException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected InvalidFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
