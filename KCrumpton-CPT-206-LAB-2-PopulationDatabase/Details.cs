using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/* Kara Crumpton
 * CPT 206 LAB 2 - Population Data
 * Jan 18 2025
 */
namespace KCrumpton_CPT_206_LAB_2_PopulationDatabase
{
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
            // Couldn't get the picture to change.. had to look this up to make it work
            cityBindingSource.PositionChanged += CityBindingSource_PositionChanged;
            cityDataGridView.ColumnHeaderMouseClick += CityDataGridView_ColumnHeaderMouseClick; // makes it change when you sort the columns
        }



        // Makes the picture change
        private void CityBindingSource_PositionChanged(object sender, EventArgs e)
        {
            // Get the row we're on 
            var currentRow = (DataRowView)cityBindingSource.Current;

            // Find the cityID from that
            var currentCityID = (int)currentRow["CityID"];

            // I named the picture files to match the cityID to make it easier
            string imagePath = System.IO.Path.Combine(Application.StartupPath, $"{currentCityID}.jpg");

            // Load the picture
            if (System.IO.File.Exists(imagePath))
            {
                picBox.Image = Image.FromFile(imagePath);
                picBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                // Set a default pic for added rows that don't have a picture
                picBox.Image = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "default.jpg"));
            }
        }

        // This makes the picture box update when they click the columns to sort the table
        private void CityDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Sort the stuff
            string columnName = cityDataGridView.Columns[e.ColumnIndex].Name;

            // Tell the box to update
            UpdatePictureBox();
        }


        private void UpdatePictureBox()
        {
            // Get the row
            var currentRow = (DataRowView)cityBindingSource.Current;
            var currentCityID = (int)currentRow["CityID"];

            // Get the picture
            string imagePath = System.IO.Path.Combine(Application.StartupPath, $"{currentCityID}.jpg");

            // Load the picture. Had to set the Zoom mode here, it didn't work just doing it in the properties?
            if (System.IO.File.Exists(imagePath))
            {
                picBox.Image = Image.FromFile(imagePath);
                picBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                // Default pic load if needed
                picBox.Image = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "default.jpg"));
            }
        }


        private void Details_Load(object sender, EventArgs e)
        {
            // Loads the stuff in the table
            this.cityTableAdapter.Fill(this.cityDBDataSet.City);


            // Gets the stuff
            var currentRow = (DataRowView)cityBindingSource.Current;

            // Retrieve the CityID to use for the pictures
            var currentCityID = (int)currentRow["CityID"];

            // I named the pics to match the CityID so this would be easier!
            string imagePath = System.IO.Path.Combine(Application.StartupPath, $"{currentCityID}.jpg");

            // Load the picture
            if (System.IO.File.Exists(imagePath))
            {
                picBox.Image = Image.FromFile(imagePath);
            }
            else
            {
                // Gotta have a default for records added by the user
                picBox.Image = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "default.jpg"));
            }

        }

        private void cityBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.cityBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cityDBDataSet);

        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void totalBtn_Click(object sender, EventArgs e)
        {
            int totalPopulation;
            totalPopulation = (int)cityTableAdapter.TotalPopulation();

            // Message Box... I learned how to change the buttons and whatnot last semester. :)
            MessageBox.Show("Total Population of All Cities: " + totalPopulation, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void avgBtn_Click(object sender, EventArgs e)
        {

            float avgPopulation;
            avgPopulation = (float)(cityTableAdapter.AveragePopulation());

            // The Average should show a decimal, but mine doesn't? It shows .00, but it should be .52... I tried it as decimal, double and float... ?
            MessageBox.Show($"Average Population of All Cities: {avgPopulation:F2}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void highBtn_Click(object sender, EventArgs e)
        {
            int maxPopulation;
            maxPopulation = (int)(cityTableAdapter.MaxPopulation());

            MessageBox.Show("Highest Population of All Cities: " + maxPopulation, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void lowBtn_Click(object sender, EventArgs e)
        {
            int minPopulation;
            minPopulation = (int)(cityTableAdapter.MinPopulation());

            MessageBox.Show("Lowest Population of All Cities: " + minPopulation, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}






            