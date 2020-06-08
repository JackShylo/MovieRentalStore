using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieRentalStore
{
    // TODO: 
    // Ideally the Handlers would return any errors with information
    // What can I unit test?
    // Rental_Cost column is unused

    public partial class Form1 : Form
    {
        // Instantiate Class
        Movies myMovies = new Movies();
        RentedMovies myRentedMovies = new RentedMovies();
        Customers myCustomers = new Customers();

        // Properties
        public TextBox[] movieTextBoxes { get; set; }
        public RadioButton[] movieRadioButtons { get; set; }
        public TextBox[] customerTextBoxes { get; set; }
        public TextBox[] rentedMovieTextBoxes { get; set; }
        public TextBox[] pageTabButtons { get; set; }

        public string selectedData { get; set; }

        // Constructor
        public Form1()
        {
            InitializeComponent();
            LoadDB();
            // Define front end components
            movieTextBoxes = new TextBox[] { txtMovieID, txtMovieRating, txtMovieTitle, txtMovieYear, txtMovieCopies, txtMoviePlot, txtMovieGenre, txtMovieDate };
            movieRadioButtons = new RadioButton[] { rbnAvailable, rbnRentedOut };
            customerTextBoxes = new TextBox[] { txtCustID, txtFirstName, txtLastName, txtAddress, txtPhone };
            rentedMovieTextBoxes = new TextBox[] { txtRMID, txtFirstName, txtLastName, txtAddress, txtPhone, txtMovieTitle, txtMovieRentalCost };
        }

        private void LoadDB()
        {
            // Default DataSource is Movies
            dgvMain.DataSource = myMovies.ReadEntries();
            selectedData = "Movies";
        }

        /*                  */
        /* CUSTOMER SECTION */
        /*                  */

        // Create Customer
        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string[] insertArr = { txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhone.Text };
            if (myCustomers.CreateEntry(insertArr))
            {
                MessageBox.Show("Added Entry");
                LoadDB();
            }
            else
            {
                MessageBox.Show("Failed to Add Entry!");
                LoadDB();
            }
        }

        // Read Customers
        private void btnCustomersTab_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = myCustomers.ReadEntries();
            selectedData = "Customers";
            updateMovieRadioButtons();
        }

        // Update Customer
        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string[] updateArr = { txtCustID.Text, txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhone.Text };
            if (myCustomers.UpdateEntry(updateArr))
            {
                MessageBox.Show("Updated Entry!");
                LoadDB();
            }
            else
            {
                MessageBox.Show("Failed to Update Entry!");
                LoadDB();
            }
        }

        // Delete Customer
        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string ID = txtCustID.Text;
            myCustomers.DeleteEntry(ID);
        }

        /*                  */
        /*  MOVIE SECTION   */
        /*                  */

        // Create Movie
        private void btnMovieCreate_Click(object sender, EventArgs e)
        {
            string[] insertArr = { txtMovieRating.Text, txtMovieTitle.Text, txtMovieYear.Text, txtMovieRentalCost.Text, txtMovieCopies.Text, txtMoviePlot.Text, txtMovieGenre.Text };
            if (myMovies.CreateMovie(insertArr))
            {
                MessageBox.Show("Added Movie");
                LoadDB();
            }
            else
            {
                MessageBox.Show("Failed to add Movie!");
                LoadDB();
            }
        }

        // Read Movies
        private void btnMoviesTab_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = myMovies.ReadEntries();
            selectedData = "Movies";
            updateMovieRadioButtons();
        }

        // Update Movie
        private void btnMovieUpdate_Click(object sender, EventArgs e)
        {
            string[] updateArr = { txtMovieID.Text, txtMovieRating.Text, txtMovieTitle.Text, txtMovieYear.Text, txtMovieRentalCost.Text, txtMovieCopies.Text, txtMoviePlot.Text, txtMovieGenre.Text };
            if (myMovies.UpdateMovie(updateArr))
            {
                MessageBox.Show("Updated Movie!");
                LoadDB();
            }
            else
            {
                MessageBox.Show("Failed to update Movie!");
                LoadDB();
            }
        }

        // Delete Movie
        private void btnMovieDelete_Click(object sender, EventArgs e)
        {
            string ID = txtMovieID.Text;
            myMovies.DeleteMovie(ID);
        }

        /*                       */
        /* RENTED MOVIES SECTION */
        /*                       */

        // Read RentedMovies 
        private void btnRentedMoviesTab_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = myRentedMovies.ReadEntries();
            selectedData = "RentedMovies";
            updateMovieRadioButtons();
        }

        // Read Most Popular Movies
        private void btnMostPopularTab_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = myRentedMovies.ReadMostPopularMovies();
            selectedData = "MostPopular";
            updateMovieRadioButtons();
        }

        // Read Best Customers
        private void btnBestCustomersTab_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = myRentedMovies.ReadBestCustomers();
            selectedData = "BestCustomers";
            updateMovieRadioButtons();
        }

        // Read Available Movies
        private void rbnAvailable_CheckedChanged(object sender, EventArgs e)
        {
            dgvMain.DataSource = myRentedMovies.ReadAvailableMovies();
            selectedData = "AvailableMovies";
            updateMovieRadioButtons();
        }

        // Read Non Available Movies
        private void rbnRentedOut_CheckedChanged(object sender, EventArgs e)
        {
            dgvMain.DataSource = myRentedMovies.ReadRentedOutMovies();
            selectedData = "RentedOut";
            updateMovieRadioButtons();
        }

        /*                     */
        /* ISSUE MOVIE SECTION */
        /*                     */
        private void btnIssueMovie_Click(object sender, EventArgs e)
        {
            string[] insertArr = { txtMovieID.Text, txtCustID.Text, DateTime.Now.ToString(), "" };

            if (myRentedMovies.IssueMovie(insertArr))
            {
                MessageBox.Show("Successfully issued Movie!");
                LoadDB();
            }
            else
            {
                MessageBox.Show("Failed to issue Movie!");
                LoadDB();
            }
        }

        private void btnReturnMovie_Click(object sender, EventArgs e)
        {
            string[] updateArr = { DateTime.Now.ToString(), txtRMID.Text };
            if (myRentedMovies.ReturnMovie(updateArr))
            {
                MessageBox.Show("Successfully returned Movie!");
                LoadDB();
            }
            else
            {
                MessageBox.Show("Failed to returned Movie!");
                LoadDB();
            }
        }

        /*                         */
        /* FRONT END LOGIC SECTION */
        /*                         */

        /*                  */
        /* Cell Click Logic */
        /*                  */
        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                // Gets current page number and fills data accordingly
                switch (selectedData)
                {
                    case "Movies":
                        // if Movies is selected
                        for (int i = 0; i < movieTextBoxes.Length; i++)
                        {
                            movieTextBoxes[i].Text = dgvMain.Rows[e.RowIndex].Cells[i].Value.ToString();
                        }
                        if (Int16.Parse(txtMovieYear.Text) > DateTime.Now.Year - 5)
                        {
                            txtMovieRentalCost.Text = "$5.0000";
                        }
                        else if (Int16.Parse(txtMovieYear.Text) < DateTime.Now.Year - 5)
                        {
                            txtMovieRentalCost.Text = "$2.0000";
                        }
                        else
                        {
                            txtMovieRentalCost.Text = "";
                        }
                        break;
                    case "Customers":
                        // if Customers is selected
                        for (int i = 0; i < customerTextBoxes.Length; i++)
                        {
                            customerTextBoxes[i].Text = dgvMain.Rows[e.RowIndex].Cells[i].Value.ToString();
                        }
                        break;
                    case "RentedMovies":
                        // CLEAR TEXT BOXES
                        for (int i = 0; i < movieTextBoxes.Length; i++)
                        {
                            movieTextBoxes[i].Text = "";
                        }
                        for (int i = 0; i < customerTextBoxes.Length; i++)
                        {
                            customerTextBoxes[i].Text = "";
                        }

                        // if RentedMovies is selected fill with relevant data
                        for (int i = 0; i < rentedMovieTextBoxes.Length; i++)
                        {
                            rentedMovieTextBoxes[i].Text = dgvMain.Rows[e.RowIndex].Cells[i].Value.ToString();
                        }
                        break;
                }
            }
            catch
            {
            }
        }

        // Movie Radio Buttons Logic
        private void updateMovieRadioButtons()
        {
            switch (selectedData)
            {
                case "Movies":
                    for (int i = 0; i < movieRadioButtons.Length; i++)
                    {
                        movieRadioButtons[i].Enabled = true;
                    }
                    break;
                default:
                    for (int i = 0; i < movieRadioButtons.Length; i++)
                    {
                        movieRadioButtons[i].Enabled = false;
                    }
                    break;
            }
        }
    }
}
