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
    public class MovieAction
    {
        // Add Movie
        public bool AddMovie(MovieData data)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "insert into movies values(@Title,@Year,@RentCost,@Genre,@Rating,@Flag)";
            cmd.Parameters.AddWithValue("@Title", data.Title);
            cmd.Parameters.AddWithValue("@Year", data.Year);
            cmd.Parameters.AddWithValue("@RentCost", data.RentCost);
            cmd.Parameters.AddWithValue("@Genre", data.Genre);
            cmd.Parameters.AddWithValue("@Rating", data.Rating);
            cmd.Parameters.AddWithValue("@Flag", "Available");
            bool ans = cmd.ExecuteNonQuery() > 0;
            cmd.Dispose();
            return ans;
        }

        // Get My Movies
        public DataTable GetAllMovies()
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();

            cmd.CommandText = "SELECT * FROM Movies";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        // Get Available Movies for comboBox
        public DataTable GetMoviesForDDL()
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();

            cmd.CommandText = "SELECT * FROM Movies where flag=@flag";
            cmd.Parameters.AddWithValue("@flag", "Available");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cmd.Dispose();
            return ds;
        }

        // Delete Movie
        public bool DeleteMovie(string MovieId)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "delete movies where MovieId=@MovieId";
            cmd.Parameters.AddWithValue("@MovieId", MovieId);
            bool ans = cmd.ExecuteNonQuery() > 0;
            cmd.Dispose();
            return ans;
        }


        // Update Movie
        public bool UpdateMovie(MovieData data)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = "update movies set title=@title,year=@year,Rating=@rating,genre=@genre,RentalCost=@rentcost where movieId=@movieId";
            cmd.Parameters.AddWithValue("@movieId", data.MovieId);
            cmd.Parameters.AddWithValue("@title", data.Title);
            cmd.Parameters.AddWithValue("@year", data.Year);
            cmd.Parameters.AddWithValue("@rating", data.RentCost);
            cmd.Parameters.AddWithValue("@genre", data.Genre);
            cmd.Parameters.AddWithValue("@rentcost", data.Rating);
            bool ans = cmd.ExecuteNonQuery() > 0;
            cmd.Dispose();
            return ans;
        }

    }
}
