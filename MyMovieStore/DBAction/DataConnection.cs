using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieStore.DBAction
{
    public class DataConnection
    {
        private static SqlConnection con = null;

        private DataConnection() { }

        //Connection with Database
        public static SqlConnection GetConnection()
        {
            if (con == null)
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
                con.Open();
            }

            return con; // if connection object already exists, then return the last connection object
        }

        //Close the Connection from database
        public static void CloseConnection()
        {
            if (con != null)
            {
                con.Dispose();
            }
            con = null;
        }

    }
}
