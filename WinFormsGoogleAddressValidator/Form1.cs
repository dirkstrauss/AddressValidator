using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidateAddress;

namespace WinFormsGoogleAddressValidator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnValidateAddress_Click(object sender, EventArgs e)
        {

            var result = addressApi.CheckAddress(txtNumber.Text, txtStreet.Text, txtCity.Text, txtState.Text, txtZip.Text, "xml");
            if (result == addressApi.Result.Succeeded)
            {
                string output = $"Formatted: {addressApi.FormattedAddress}{Environment.NewLine}Longnitude: {addressApi.Longnitude}{Environment.NewLine}Lattitude: {addressApi.Lattitude}";
                txtResult.Text = output;
            }
            else
                txtResult.Text = addressApi.Error;
        }
    }
}
