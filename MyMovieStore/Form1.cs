using MyMovieStore.Common;
using MyMovieStore.DBAction;
using MyMovieStore.Operation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMovieStore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // add new customer implementation
        private void AddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // add customer only works when selected tab = customer
                if (TabControl.SelectedTab == TabControl.TabPages["Customers"])
                {
                    string name, address, phone;
                    name = txtName.Text.Trim();
                    address = txtAddress.Text.Trim();
                    phone = txtPhone.Text.Trim();

                    if (Checks.Empty(name))
                    {
                        MessageBox.Show("Customer name is required!");
                        txtName.Focus();
                    }
                    else if (Checks.Empty(address))
                    {
                        MessageBox.Show("Customer address is required!");
                        txtAddress.Focus();
                    }
                    else if (Checks.Empty(phone))
                    {
                        MessageBox.Show("Customer phone number is required!");
                        txtPhone.Focus();
                    }
                    else if (!Checks.checkMobileNumber(phone))
                    {
                        MessageBox.Show("please provide a valid phone number!");
                        txtPhone.Focus();
                    }
                    else
                    {
                        CustomerData data = new CustomerData();
                        data.Name = name;
                        data.Address = address;
                        data.Phone = long.Parse(phone);
                        if (new CustomerAction().AddCustomer(data))
                        {

                            BindDdlCustomer(); // method calling to Bind comboBox Customer
                            BindGridCustomer(); // method calling to Bind GridView Customer
                            MessageBox.Show("New customer added!");

                            txtAddress.Clear();
                            txtName.Clear();
                            txtPhone.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Unable to add this customer!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select CUSTOMERS tab first!");
                }

            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindDdlCustomer(); // method calling

            BindGridCustomer(); // method calling to bind customer grid
            BindDdlMovie(); // method calling to Bind comboBox movie

            BindDDLRentedMovies(); // method calling to Bind comboBox Rented movie
        }


        // Bind comboBox Rented movie
        private void BindDDLRentedMovies()
        {
            DataTable dsRented = new RentalMovieAction().GetRentedMoviesDDL();
            ddlRentedMovies.DisplayMember = "Title";
            ddlRentedMovies.ValueMember = "MovieID";
            ddlRentedMovies.DataSource = dsRented;
        }

        //bind movies grid
        private void BindGridMovies()
        {
            DataTable ds = new MovieAction().GetAllMovies();
            gvMovies.DataSource = ds;
            gvMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        //Bind Customer Grid to show data
        private void BindGridCustomer()
        {
            DataTable ds = new CustomerAction().GetAllCustomers();
            CustomersDataGridView.DataSource = ds;
            CustomersDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        // Bind rented movies grid
        private void BindGridRentedMovies()
        {
            DataTable ds = new RentalMovieAction().GetAllRentedData();
            gvRental.DataSource = ds;
            gvRental.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        //Bind ComboBox Custoemr to show data
        private void BindDdlCustomer()
        {
            DataTable ds = new CustomerAction().GetAllCustomers();
            ddlCustomer.DisplayMember = "Name";
            ddlCustomer.ValueMember = "CustId";
            ddlCustomer.DataSource = ds;
            
        }

        // Bind ddl movies Available for rent
        private void BindDdlMovie()
        {
            DataTable dsMovie = new MovieAction().GetMoviesForDDL();
            ddlMovie.DisplayMember = "Title";
            ddlMovie.ValueMember = "MovieID";
            ddlMovie.DataSource = dsMovie;

        }

        // to delete a customer
        private void DeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string custId = ddlCustomer.SelectedValue.ToString();
                
                    DialogResult result = MessageBox.Show("Are you sure to delete this customer?", "Alert", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (new CustomerAction().DeleteCustomer(custId))
                        {
                            BindDdlCustomer(); // method calling to Bind comboBox Customer
                            BindGridCustomer(); // method calling to Bind GridView Customer
                            MessageBox.Show("Customer Deleted!");
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete this customer");
                        }
                    }
                    else
                    {
                        // Not deleted
                    }

            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Can not delete this customer as this customer rented a movie!");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Update customer info
        private void UpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string custId = ddlCustomer.SelectedValue.ToString();
                string name, address, phone;
                name = txtName.Text.Trim();
                address = txtAddress.Text.Trim();
                phone = txtPhone.Text.Trim();

                if (Checks.Empty(name))
                {
                    MessageBox.Show("Customer name is required!");
                    txtName.Focus();
                }
                else if (Checks.Empty(address))
                {
                    MessageBox.Show("Customer address is required!");
                    txtAddress.Focus();
                }
                else if (Checks.Empty(phone))
                {
                    MessageBox.Show("Customer phone number is required!");
                    txtPhone.Focus();
                }
                else if (!Checks.checkMobileNumber(phone))
                {
                    MessageBox.Show("please provide a valid phone number!");
                    txtPhone.Focus();
                }
                else
                {
                    CustomerData data = new CustomerData();
                    data.Name = name;
                    data.Address = address;
                    data.Phone = long.Parse(phone);
                    data.CustId = int.Parse(custId);
                    if (new CustomerAction().UpdateCustomer(data))
                    {
                        
                        BindDdlCustomer(); // method calling to Bind comboBox Customer
                        BindGridCustomer(); // method calling to Bind GridView Customer
                        MessageBox.Show("Customer updated!");

                        txtAddress.Clear();
                        txtName.Clear();
                        txtPhone.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Unable to update this customer!");
                    }
                }

            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }

        // Add movies 
        private void btnAddMOvie_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabControl.SelectedTab == TabControl.TabPages["Movies"])
                {
                    string Title, RentCost, Year, Rating, Genre;
                    Title = txtTitle.Text.Trim();
                    RentCost = txtRentedCost.Text.Trim();
                    Rating = txtRating.Text.Trim();
                    Year = txtYear.Text.Trim();
                    Genre = txtGenre.Text.Trim();

                    if (Checks.Empty(Title))
                    {
                        MessageBox.Show("Movie title is required!");
                    }
                    else if (Checks.Empty(Year))
                    {
                        MessageBox.Show("Movie released year is required!");
                    }
                    else if (!Checks.checkNumber(Year))
                    {
                        MessageBox.Show("Please enter a valid year!");
                    }
                    else if (Checks.Empty(Genre))
                    {
                        MessageBox.Show("Movie genre is required!");
                    }
                    else if (Checks.Empty(RentCost))
                    {
                        MessageBox.Show("Movie rent cost is required!");
                    }
                    else if (!Checks.checkNumber(RentCost))
                    {
                        MessageBox.Show("Please enter a valid cost!");
                    }
                    else if (Checks.Empty(Rating))
                    {
                        MessageBox.Show("Movie rating is required!");
                    }
                    else if (!Checks.checkNumber(Rating))
                    {
                        MessageBox.Show("Please enter a valid rating!");
                    }
                    else
                    {
                        MovieData data = new MovieData();
                        data.Title = Title;
                        data.Year = Year;
                        data.Rating = int.Parse(Rating);
                        data.Genre = Genre;
                        data.RentCost = int.Parse(RentCost);
                        if (new MovieAction().AddMovie(data))
                        {

                            BindGridMovies(); // method calling to Bind Grid Movies
                            BindDdlMovie(); // method calling to Bind ComboBox Movies
                            MessageBox.Show("Movie Inseted!");

                            txtTitle.Clear();
                            txtYear.Clear();
                            txtRating.Clear();
                            txtGenre.Clear();
                            txtRentedCost.Clear();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select the MOVIES tab first!");
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //your specific tabname
            if (TabControl.SelectedTab == TabControl.TabPages["Movies"])
            {
                BindGridMovies(); // method calling to Bind Grid Movies
                BindDdlMovie(); // method calling to Bind ComboBox Movies

            }
            else if (TabControl.SelectedTab == TabControl.TabPages["Customers"])
            {
                BindDdlCustomer(); // method calling to Bind comboBox Customer

                BindGridCustomer(); // method calling to Bind Grid Customer
            }
            else if (TabControl.SelectedTab == TabControl.TabPages["RentedMovies"])
            {
                BindGridRentedMovies(); //method calling to Bind Grid for all rental movies
                BindDDLRentedMovies(); //method calling to Bind comboBox for rental movies only
            }
        }

        // To delete any movie
        private void btnDelMOvie_Click(object sender, EventArgs e)
        {
            try
            {
                string MovieId = ddlMovie.SelectedValue.ToString();

                DialogResult result = MessageBox.Show("Are you sure to delete this movie?", "Alert", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (new MovieAction().DeleteMovie(MovieId))
                    {
                        BindDdlMovie(); // method calling to Bind comboBox movie
                        BindGridMovies(); // method calling to Bind GridView movie
                        MessageBox.Show("Movie Deleted!");
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete this movie");
                    }
                }
                else
                {
                    // Not deleted
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Can not delete this movie as it is rented by a customer!");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Update Movies Info
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string MovieId = ddlMovie.SelectedValue.ToString();

                string Title, RentCost, Year, Rating, Genre;
                Title = txtTitle.Text.Trim();
                RentCost = txtRentedCost.Text.Trim();
                Rating = txtRating.Text.Trim();
                Year = txtYear.Text.Trim();
                Genre = txtGenre.Text.Trim();

                if (Checks.Empty(Title))
                {
                    MessageBox.Show("Movie title is required!");
                }
                else if (Checks.Empty(Year))
                {
                    MessageBox.Show("Movie released year is required!");
                }
                else if (!Checks.checkNumber(Year))
                {
                    MessageBox.Show("Please enter a valid year!");
                }
                else if (Checks.Empty(Genre))
                {
                    MessageBox.Show("Movie genre is required!");
                }
                else if (Checks.Empty(RentCost))
                {
                    MessageBox.Show("Movie rent cost is required!");
                }
                else if (!Checks.checkNumber(RentCost))
                {
                    MessageBox.Show("Please enter a valid cost!");
                }
                else if (Checks.Empty(Rating))
                {
                    MessageBox.Show("Movie rating is required!");
                }
                else if (!Checks.checkNumber(Rating))
                {
                    MessageBox.Show("Please enter a valid rating!");
                }
                else
                {
                    MovieData data = new MovieData();
                    data.Title = Title;
                    data.Year = Year;
                    data.Rating = int.Parse(Rating);
                    data.Genre = Genre;
                    data.RentCost = int.Parse(RentCost);
                    data.MovieId = int.Parse(MovieId);
                    if (new MovieAction().UpdateMovie(data))
                    {

                        BindGridMovies(); // method calling to Bind Grid Movies
                        BindDdlMovie(); // method calling to Bind ComboBox Movies
                        MessageBox.Show("Movie Updated!");

                        txtTitle.Clear();
                        txtYear.Clear();
                        txtRating.Clear();
                        txtGenre.Clear();
                        txtRentedCost.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Unable to update this movie!");

                    }
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }

        // Get Movie Cost 
        public int GetMovieCost( int MovieID)
        {
            SqlCommand cmd = DataConnection.GetConnection().CreateCommand();
            cmd.CommandText = " select RentalCost from movies where MovieID=@MovieID"; // stored procedure
            
            cmd.Parameters.AddWithValue("@MovieID", MovieID);
            int RentalCost = 0;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                RentalCost = int.Parse(reader["RentalCost"].ToString());
            }
            reader.Close();
            cmd.Dispose();
            return RentalCost;
        }

        // Issue  movie to customer
        private void btnIssueMovie_Click(object sender, EventArgs e)
        {
            try
            {
                string CustId, MovieId, IssueDate, ReturnDate;

                CustId = ddlCustomer.SelectedValue.ToString();
                MovieId = ddlMovie.SelectedValue.ToString();
                IssueDate = dtIsuue.Value.ToShortDateString();
                ReturnDate = dtReturn.Value.ToShortDateString();

                int totalDays = Convert.ToInt32((DateTime.Parse(ReturnDate) - DateTime.Parse(IssueDate)).TotalDays);

                int RentalCost = GetMovieCost(int.Parse(MovieId)); // Getting rental cost for that movie

                int TotalRent = totalDays * RentalCost; // Calculated the TotalRent

                if (DateTime.Parse(IssueDate) > DateTime.Parse(ReturnDate))
                {
                    MessageBox.Show("Issue date can not b greater than retun date");
                }
                else
                {
                    RentalData data = new RentalData();
                    data.CustId = int.Parse(CustId);
                    data.MovieId = int.Parse(MovieId);
                    data.RentedDate = DateTime.Parse(IssueDate);
                    data.ReturnDate = DateTime.Parse(ReturnDate);
                    data.TotalRent = TotalRent;

                    if (new RentalMovieAction().RentMovie(data))
                    {
                       // Set Flag to "Rented" when movie goes on rent
                        if (new RentalMovieAction().UpdateMovieFlag(data.MovieId))
                        {
                            BindGridMovies(); //method calling to Bind Grid movies
                            BindGridRentedMovies(); //method calling to Bind Grid for all rental movies
                            BindDDLRentedMovies(); // method calling to Bind comboBox Rented movie
                            MessageBox.Show("Movie rented successfully!");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to rent this movie");
                    }
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
            
        }

        //Customer returns movie and available it for rent again
        private void btnReturnMovie_Click(object sender, EventArgs e)
        {
            try
            {
                int MoviID = int.Parse(ddlRentedMovies.SelectedValue.ToString());
                if (new RentalMovieAction().ReturnMovie(MoviID))
                {
                    BindDdlMovie(); // method calling to Bind comboBox movie
                    BindDDLRentedMovies(); // method calling to Bind comboBox Rented movie
                    BindGridMovies(); // method calling to Bind Grid movie
                    BindGridRentedMovies(); //method calling to Bind Grid for all rental movies

                    MessageBox.Show("Movie Returned Successfully!");
                }
                else
                {
                    MessageBox.Show("Unable to return this movie!");
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }

        // Show all rented movies
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["RentedMovies"])
            {
                try
                {   //method calling to Bind Grid for all rental movies
                    BindGridRentedMovies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select RENTED MOVIES tab first!");
            }
        }

        // show customers who bought most movies
        private void btnCustMostMovie_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["RentedMovies"])
            {
                try
                {
                    DataTable dt = new RentalMovieAction().CustomersWhoBorrowMostMovies();
                    gvRental.DataSource = dt;
                    gvRental.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select RENTED MOVIES tab first!");
            }
        }

        // show most popular movies
        private void btnMostPopulrMovies_Click(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["RentedMovies"])
            {
                try
                {
                    DataTable dt = new RentalMovieAction().MostPopularMovies();
                    gvRental.DataSource = dt;
                    gvRental.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select RENTED MOVIES tab first!");
            }
        }

        //Form closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    DialogResult result = MessageBox.Show("Do you really want to exit?", "Alert", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Connection to Database closed on form closing
                        DataConnection.CloseConnection();
                        Application.ExitThread();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
