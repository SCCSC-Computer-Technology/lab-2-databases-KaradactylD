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
 * CPT 206 - LAB 2 - Population Data
 * Jan 18 2025
 */

namespace KCrumpton_CPT_206_LAB_2_PopulationDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cityBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.cityBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cityDBDataSet);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cityDBDataSet.City' table. You can move, or remove it, as needed.
            this.cityTableAdapter.Fill(this.cityDBDataSet.City);

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.tableAdapterManager.UpdateAll(this.cityDBDataSet);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void detailsBtn_Click(object sender, EventArgs e)
        {
            Details details = new Details();
            details.ShowDialog();
        }
    }
}
