using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IRCTC
{
    public partial class FormDetailsAutomation : Form
    {
        public FormDetailsAutomation()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            IRCTCDetails.TravelDetails();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IRCTCDetails.PassengerDetails();
        }

        private void btnPaymentDetails_Click(object sender, EventArgs e)
        {
            IRCTCDetails.SummaryAndPayment();
        }
    }
}
