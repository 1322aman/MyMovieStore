using MyMovieStore.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieStore.DBAction
{
    public class RentalMovieAction
    {
        // Add Movie
        public bool RentMovie(RentalData data)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "prcRentMovie"; // stored procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MovieID", data.MovieId);
            cmd.Parameters.AddWithValue("@CustID", data.CustId);
            cmd.Parameters.AddWithValue("@RentDate", data.RentedDate);
            cmd.Parameters.AddWithValue("@ReturnDate", data.ReturnDate);
            cmd.Parameters.AddWithValue("@TotalRent", data.TotalRent);
            bool ans = cmd.ExecuteNonQuery() > 0;
            cmd.Dispose();
            return ans;
        }

        // Get All RentedData
        public DataTable GetAllRentedData()
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            // stored procedure to show all rented movies order by isuue date asecending
            cmd.CommandText = "ShowRentedData"; 
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        // Add Movie
        public bool UpdateMovieFlag(int MovieID)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "prcUpdateMovieFlag"; // stored procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@movieID", MovieID);
            cmd.Parameters.AddWithValue("@flag", "Rented");

            bool ans = cmd.ExecuteNonQuery() > 0;
            cmd.Dispose();
            return ans;
        }

        // Get All RentedMovie for DDL where Flag='Rented'
        public DataTable GetRentedMoviesDDL()
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "select MovieID,Title from Movies where Flag= @Flag";
            cmd.Parameters.AddWithValue("@Flag", "Rented");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cmd.Dispose();
            return ds;
        }


        // Return Movie
        public bool ReturnMovie(int MovieID)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "prcReturnMovie"; // stored procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@movieID", MovieID);
            cmd.Parameters.AddWithValue("@flag", "Available");

            bool ans = cmd.ExecuteNonQuery() > 0;
            cmd.Dispose();
            return ans;
        }


        //Customers who borrows most movies
        public DataTable CustomersWhoBorrowMostMovies()
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "prcCustWhoBorrowMostMovies"; // store procedure
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        //Most popular movies
        public DataTable MostPopularMovies()
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "prcMostPopularMovies"; // store procedure
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        
    }
}
