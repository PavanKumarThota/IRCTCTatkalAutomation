using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace IRCTC
{
    public partial class TravelName : Form
    {
        public TravelName()
        {
            InitializeComponent();
        }
        public IRCTCDetails obj = new IRCTCDetails();
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTravelName.Text.ToString()))
            {
                DataTable dt = new DataTable();
                IRCTCDetails.con.Open();
                OleDbCommand cmdTrainNames = new OleDbCommand("Select * from SavedFormDetails where TravelName='" + txtTravelName.Text.ToString().Trim() + "'", IRCTCDetails.con);
                OleDbDataAdapter da2 = new OleDbDataAdapter(cmdTrainNames.CommandText, IRCTCDetails.con);
                //SqlCeCommand cmdTrainNames = new SqlCeCommand("Select * from SavedFormDetails where TravelName='" + txtTravelName.Text.ToString().Trim() + "'", IRCTCDetails.con);
                //SqlCeDataAdapter da2 = new SqlCeDataAdapter(cmdTrainNames.CommandText, IRCTCDetails.con);
                da2.Fill(dt);
                IRCTCDetails.con.Close();
                if (dt.Rows.Count == 0)
                {
                    obj = new IRCTCDetails(txtTravelName.Text.ToString().Trim());
                    this.Close();
                }
                else
                {
                    obj.Show("TravelName Already Existed,Please use different name and try again", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Show();
                }
            }
            else
            {
                obj.Show("TravelName Should not be blank", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Show();
            }
        }

      
    }
}
