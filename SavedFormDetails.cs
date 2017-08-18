using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
//using System.Data.SqlServerCe;

namespace IRCTC
{
    public partial class SavedFormDetails : Form
    {
       

        public  DataTable dtsavedformdetails = new DataTable();
        

        /// <summary>
        /// parameterized constructor
        /// </summary>
        /// <param name="dt"></param>
        public SavedFormDetails(DataTable dt)
        {
            InitializeComponent();
            //dtsavedformdetails.Columns.Add(new DataColumn("Select", typeof(bool)));
            dtsavedformdetails.Columns.Add("TravelName");
            dtsavedformdetails.Columns.Add("From Station");
            dtsavedformdetails.Columns.Add("To Station");
            dtsavedformdetails.Columns.Add("Train Name");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtsavedformdetails.Rows.Add();
                dtsavedformdetails.Rows[i]["TravelName"] = dt.Rows[i]["TravelName"].ToString();
                dtsavedformdetails.Rows[i]["From Station"] = dt.Rows[i]["FromStation"].ToString();
                dtsavedformdetails.Rows[i]["To Station"] = dt.Rows[i]["ToStation"].ToString();
                dtsavedformdetails.Rows[i]["Train Name"] = dt.Rows[i]["TrainName"].ToString();

            }
            
            DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
            CheckBox chk = new CheckBox();
            CheckboxColumn.Name = "Select";
            dgvSavedFormDetails.Columns.Add(CheckboxColumn);
            dgvSavedFormDetails.DataSource = dtsavedformdetails;
            dgvSavedFormDetails.Columns[1].ReadOnly = true;
            dgvSavedFormDetails.Columns[2].ReadOnly = true;
            dgvSavedFormDetails.Columns[3].ReadOnly = true;
            dgvSavedFormDetails.Columns[4].ReadOnly = true;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public SavedFormDetails()
        {
            InitializeComponent();
            IRCTCDetails.con.Open();
            OleDbCommand cmd = new OleDbCommand("select * from PassengerDetails", IRCTCDetails.con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd.CommandText, IRCTCDetails.con);
            //SqlCommand cmd = new SqlCommand("select * from PassengerDetails", IRCTCDetails.con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd.CommandText, IRCTCDetails.con);
            da.Fill(dtsavedformdetails);
            IRCTCDetails.con.Close();
            DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
            CheckBox chk = new CheckBox();
            CheckboxColumn.Name = "Add";
            dgvSavedFormDetails.Columns.Add(CheckboxColumn);
            dgvSavedFormDetails.DataSource = dtsavedformdetails;
            dgvSavedFormDetails.Columns[1].ReadOnly = true;
            dgvSavedFormDetails.Columns[2].ReadOnly = true;
            dgvSavedFormDetails.Columns[3].ReadOnly = true;
            dgvSavedFormDetails.Columns[4].ReadOnly = true;
            
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void dgvSavedFormDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==0)
            {
                if (dgvSavedFormDetails.Columns.Contains("Select"))
                {
                    if (Convert.ToBoolean(dgvSavedFormDetails.Rows[e.RowIndex].Cells["Select"].Value) == false)
                    {
                        for (int i = 0; i <= dgvSavedFormDetails.Rows.Count-1; i++)
                        {
                            dgvSavedFormDetails.Rows[i].Cells["Select"].Value = false;
                        }
                    }
                    
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvSavedFormDetails.Columns.Contains("Add"))
            {
                List<int> adults = new List<int>();
                List<int> infant = new List<int>();
                for (int i = 0; i < dgvSavedFormDetails.Rows.Count; i++)
                {
                    if (dgvSavedFormDetails.Rows[i].Cells[0].Value != null)
                    {
                        bool checkedcell = (bool)dgvSavedFormDetails.Rows[i].Cells[0].Value;
                        if (checkedcell == true)
                        {
                            if (dgvSavedFormDetails.Rows[i].Cells["BerthPreference"].Value.ToString().ToLower() != "null")
                            {
                                adults.Add(i);
                            }
                            else
                            {
                                infant.Add(i);
                            }
                        }
                    }
                }
                if (adults.Count() > 4 || infant.Count() > 2)
                {
                    IRCTCDetails obj = new IRCTCDetails();
                    obj.Show("Max Four Passengers and Two Children Allowed", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (adults.Count() == 0 && infant.Count() == 0)
                {
                    IRCTCDetails obj = new IRCTCDetails();
                    obj.Show("Please Select the passengers and Click on Ok", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    
                    IRCTCDetails obj = new IRCTCDetails(adults, infant);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                
            }

            if (dgvSavedFormDetails.Columns.Contains("Select"))
            {
                int DtRowindex;
                bool flag = false;
                for (int i = 0; i < dgvSavedFormDetails.Rows.Count; i++)
                {
                    if (dgvSavedFormDetails.Rows[i].Cells[0].Value != null)
                    {
                        bool checkedcell = (bool)dgvSavedFormDetails.Rows[i].Cells[0].Value;
                        if (checkedcell == true)
                        {
                            DtRowindex = i;
                            IRCTCDetails frm = new IRCTCDetails(DtRowindex);
                            flag = true;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
                if (!flag)
                {
                    IRCTCDetails obj = new IRCTCDetails();
                    obj.Show("Please Select the Checkbox and Click on Ok", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
    }
}
