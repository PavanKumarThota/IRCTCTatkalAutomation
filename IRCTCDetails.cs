using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using mshtml;
using System.Data.OleDb;
using System.Runtime.InteropServices;
//using System.Data.SqlServerCe;

namespace IRCTC
{
    public partial class IRCTCDetails : Form
    {
        public static NotifyIcon notifyIcon;
        public static string Travelname = string.Empty;
        public static int SavedFormDetailsRowIndex;
        public static IEAutomation.IEClass ie = new IEAutomation.IEClass();
        public static List<int> Adults = new List<int>();
        public static List<int> Infants = new List<int>();
        public static string FromStation = string.Empty;
        public static string ToStation = string.Empty;
        public static string TravelDate = string.Empty;
        public static string Datedmyyyy = string.Empty;
        public static string CmbQuota = string.Empty;
        public static string TicketClass = string.Empty;
        public static string TrainName = string.Empty;
        public static string passname1 = string.Empty;
        public static string passname2 = string.Empty;
        public static string passname3 = string.Empty;
        public static string passname4 = string.Empty;
        public static string passage1 = string.Empty;
        public static string passage2 = string.Empty;
        public static string passage3 = string.Empty;
        public static string passage4 = string.Empty;
        public static string passGender1 = string.Empty;
        public static string passGender2 = string.Empty;
        public static string passGender3 = string.Empty;
        public static string passGender4 = string.Empty;
        public static string passbrth1 = string.Empty;
        public static string passbrth2 = string.Empty;
        public static string passbrth3 = string.Empty;
        public static string passbrth4 = string.Empty;
        public static string passmeal1 = string.Empty;
        public static string passmeal2 = string.Empty;
        public static string passmeal3 = string.Empty;
        public static string passmeal4 = string.Empty;
        public static string Boardingstation = string.Empty;
        public static string passchildname1 = string.Empty;
        public static string passchildname2 = string.Empty;
        public static string passchildage1 = string.Empty;
        public static string passchildage2 = string.Empty;
        public static string passchildgen1 = string.Empty;
        public static string passchildgen2 = string.Empty;
        public static bool considerforautoupgradation = false;
        public static string mobileno = string.Empty;
        public static bool onlyconfirmbirth = false;
        public static string paymenttype = string.Empty;
        public static string bankname = string.Empty;
        public static int banknameselectedindex;
        public static string cardtype = string.Empty;
        public static string cardno = string.Empty;
        public static string cardname = string.Empty;
        public static string expyear = string.Empty;
        public static string expmonth = string.Empty;
        public static string cvv = string.Empty;


        public static OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=IRCTCAccessDb.accdb");

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        //public static SqlCeConnection con = new SqlCeConnection(ConfigurationManager.ConnectionStrings["IRCTCConnectiostring"].ConnectionString);//@"Data Source=C:\Users\pavan\documents\visual studio 2010\Projects\IRCTC\IRCTCDb.sdf");
        //public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["IRCTC"].ConnectionString);//@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Dimmer\Documents\Visual Studio 2013\Projects\Manage components\Manage components\Database1.mdf;Integrated Security=True");
        //public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pavan\Documents\Visual Studio 2015\Projects\IRCTC\IRCTCDb.mdf");
        
        /// <summary>
        /// 
        /// </summary>
        public IRCTCDetails()
        {
            InitializeComponent();
           
        }

        public IRCTCDetails(List<int> adults,List<int> infants)
        {
            Adults = adults;
            Infants = infants;
        }

        public IRCTCDetails(string TravelName)
        {
            Travelname = TravelName;
        }

        public IRCTCDetails(int dtRowIndex)
        {
            SavedFormDetailsRowIndex = dtRowIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateform())
                {
                    FromStation = jpform_fromStation.Text.ToString();
                    ToStation = jpform_toStation.Text.ToString();
                    TravelDate = jpform_journeyDateInputDate.Value.ToString("dd-MM-yyyy");
                    Datedmyyyy = jpform_journeyDateInputDate.Value.ToString("d-M-yyyy");
                    CmbQuota = cmbQuota.SelectedItem.ToString();
                    TrainName = txtTrainName.Text.ToString();
                    TicketClass = cmbClass.SelectedItem.ToString();
                    Boardingstation = txtboardingstation.Text.ToString();
                    //passenger details
                    if (!string.IsNullOrEmpty(txt_pass_name0.Text.ToString()))
                    {
                        passname1 = txt_pass_name0.Text.ToString();
                        passage1 = txt_pass_age0.Text.ToString();
                        passGender1 = cmb_pass_gender0.SelectedItem.ToString();
                        passbrth1 = cmb_pass_bp0.SelectedItem.ToString();
                        passmeal1 = cmb_Meal_p0.SelectedItem.ToString();
                    }
                    if (!string.IsNullOrEmpty(txt_pass_name1.Text.ToString()))
                    {
                        passname2 = txt_pass_name1.Text.ToString();
                        passage2 = txt_pass_age1.Text.ToString();
                        passGender2 = cmb_pass_gender1.SelectedItem.ToString();
                        passbrth2 = cmb_pass_bp1.SelectedItem.ToString();
                        passmeal2 = cmb_Meal_p1.SelectedItem.ToString();
                    }
                    if (!string.IsNullOrEmpty(txt_pass_name2.Text.ToString()))
                    {
                        passname3 = txt_pass_name2.Text.ToString();
                        passage3 = txt_pass_age2.Text.ToString();
                        passGender3 = cmb_pass_gender2.SelectedItem.ToString();
                        passbrth3 = cmb_pass_bp2.SelectedItem.ToString();
                        passmeal3 = cmb_Meal_p2.SelectedItem.ToString();
                    }
                    if (!string.IsNullOrEmpty(txt_pass_name3.Text.ToString()))
                    {
                        passname4 = txt_pass_name3.Text.ToString();
                        passage4 = txt_pass_age3.Text.ToString();
                        passbrth4 = cmb_pass_bp3.SelectedItem.ToString();
                        passGender4 = cmb_pass_gender3.SelectedItem.ToString();
                        passmeal4 = cmb_Meal_p3.SelectedItem.ToString();
                    }
                    if(!string.IsNullOrEmpty(txt_pass_name_child0.Text.ToString()))
                    {
                        passchildname1 = txt_pass_name_child0.Text.ToString();
                        passchildage1 = cmb_pass_Child_age0.SelectedItem.ToString();
                        passchildgen1 = cmb_pass_Gender_child0.SelectedItem.ToString();
                    }
                    if (!string.IsNullOrEmpty(txt_pass_name_child1.Text.ToString()))
                    {
                        passchildname2 = txt_pass_name_child1.Text.ToString();
                        passchildage2 = cmb_pass_Child_age1.SelectedItem.ToString();
                        passchildgen2 = cmb_pass_Gender_child1.SelectedItem.ToString();
                    }

                    //other details
                    if (chk_considerforautoupgradation.Checked)
                    {
                        considerforautoupgradation = true;
                    }

                    if (chk_onlyifconfirmberths.Checked)
                    {
                        onlyconfirmbirth = true;
                    }


                    if (!string.IsNullOrEmpty(txtMobileno.Text.ToString()))
                    {
                        mobileno = txtMobileno.Text.ToString();
                    }

                    FormDetailsAutomation frm = new FormDetailsAutomation();
                   
                    if (!ie.isIEObjectAvailable("IRCTC Next Generation eTicketing System"))
                    {
                        DisplayBalloonTip("Process Started", 500);
                        ie.StartNewMSIE("www.irctc.co.in");
                        KillBalloonTip();

                        //focus the window
                        ShowWindow((IntPtr)ie.IEObject.HWND, 3);


                        ie.WaitForIEReadyState();
                        //ie.WaitForDocReadyState();

                        //if there is no internet connection page wont load
                        if (!ie.isIEObjectAvailable("IRCTC Next Generation eTicketing System"))
                        {
                            DialogResult res = Show("Please check internet connection and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Show();
                        }
                        else
                        {
                            BringWindowToTop((IntPtr)ie.IEObject.HWND);
                            //StartAutomation();
                            this.Visible = false;
                            //this.Close();
                            LoginToIRCTC();
                            frm.ShowDialog();
                        }
                    }
                    else
                    {

                        BringWindowToTop((IntPtr)ie.IEObject.HWND);
                        ShowWindow((IntPtr)ie.IEObject.HWND, 3);
                        SetFocus((IntPtr)ie.IEObject.HWND);
                        //StartAutomation();
                        this.Visible = false;
                        //this.Close();
                        LoginToIRCTC();
                        frm.ShowDialog();
                    }

                   

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception--" + ex.Message);
                Environment.Exit(0);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        //private void StartAutomation()
        //{
        //    try
        //    {
        //        DisplayBalloonTip("Inserting Username and Password", 250);
        //        KillBalloonTip();
        //        LoginToIRCTC();
        //        TravelDetails();
        //        PassengerDetails();
        //        OtherDetails();
        //        SummaryAndPayment();

        //        DisplayBalloonTip("Process Completed", 250);
        //        KillBalloonTip();
        //        Environment.Exit(0);
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("Exception--"+ex.Message);
        //        Environment.Exit(0);
        //    }
            
               
        //}

        /// <summary>
        /// 
        /// </summary>
        public static void SummaryAndPayment()
        {
            try
            {
                do
                {
                    Thread.Sleep(10);

                } while (!ie.isIEObjectAvailable("Book Ticket - Journey Summary"));
                ie.WaitForIEReadyState();
                BringWindowToTop((IntPtr)ie.IEObject.HWND);
                ShowWindow((IntPtr)ie.IEObject.HWND, 3);
                SetFocus((IntPtr)ie.IEObject.HWND);
                SendKeyStroke_API(35, 1);
                do
                {
                    Thread.Sleep(10);

                } while (!ie.ClickElementByInnerText(paymenttype));

                if (paymenttype == "Net Banking")
                {
                    ClickonNetBankingDetails();
                }

                if (paymenttype == "Payment Gateway /Credit /Debit Cards")
                {
                    ClickonCreditCardDetails();
                }

                if (paymenttype == "Debit Card with PIN")
                {
                    ClickonDebitCardDetails();
                }

                if (bankname.ToLower() != "Visa/Master Card HDFC BANK".ToLower())
                {
                    ie.ClickElementByValue("Make Payment");
                }
                else
                {
                    CardPaymentDetails();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception--" + ex.Message);
                Environment.Exit(0);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        //public static void OtherDetails()
        //{
        //    try
        //    {
        //        //other details
        //        if (chk_considerforautoupgradation.Checked)
        //        {
        //            ie.ClickElementByName("addPassengerForm:autoUpgrade");
        //        }

        //        if (chk_onlyifconfirmberths.Checked)
        //        {
        //            ie.ClickElementByName("addPassengerForm:onlyConfirmBerths");
        //        }

        //        if (!string.IsNullOrEmpty(txtMobileno.Text.ToString()))
        //        {
        //            //mobile no
        //            ie.SetInputElementValueByName("addPassengerForm:mobileNo", txtMobileno.Text.ToString());
        //        }
               
        //        //do
        //        //{
        //        //    if(!ie.setFocusToInputElementByID("j_captcha"))
        //        //    {
        //        //        ie.setFocusToInputElementByID("nlpAnswer");
        //        //    }
                    
        //        //    //if (!ie.isIEObjectAvailable("Book Ticket - Journey Summary"))
        //        //    //{
        //        //    //    TravelName frm = new TravelName(0);
        //        //    //    frm.ShowDialog();
        //        //    //}

        //        //    //if (!ie.SetInputElementValueByName("j_captcha", Travelname))
        //        //    //{
        //        //    //    ie.SetInputElementValueByName("nlpAnswer", Travelname);
        //        //    //}

        //        //    //ie.ClickElementByValue(" Next ");
        //        //    Thread.Sleep(2000);//

        //        //} while (ie.FindInnerText("captcha image"));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Exception--"+ex.Message);
        //        Environment.Exit(0);
        //    }
            
        //}


        /// <summary>
        /// 
        /// </summary>
        public static void PassengerDetails()
        {
            try
            {
                do
                {
                    Thread.Sleep(250);

                } while (!ie.isIEObjectAvailable("Book Ticket - Passengers Information"));
                ie.WaitForIEReadyState();

                

                //DisplayBalloonTip("Inserting Passenger Details", 500);
                //KillBalloonTip();

                do
                {
                    Thread.Sleep(250);

                } while (!ie.SetInputElementValueByTrimmedName("addPassengerForm:psdetail:0:p", passname1));

                if (!string.IsNullOrEmpty(Boardingstation))
                {
                    string[] a = Boardingstation.ToString().Split('-');
                    //boarding station
                    ie.SelectOptionByName("addPassengerForm:boardingStation", a[1].ToUpper().TrimStart());
                }


                //passenger details
                if (!string.IsNullOrEmpty(passname1))
                {
                    ie.SetInputElementValueByTrimmedName("addPassengerForm:psdetail:0:p", passname1);
                    ie.SetInputElementValueByName("addPassengerForm:psdetail:0:psgnAge", passage1);
                    ie.SelectOptionByName("addPassengerForm:psdetail:0:psgnGender", passGender1);
                    if (!string.IsNullOrEmpty(passbrth1))
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:0:berthChoice", passbrth1);
                    }

                    if (passmeal1.ToString().ToLower().Trim() == "non-veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:0:foodChoice", "N");
                    }
                    else if (passmeal1.ToString().ToLower().Trim() == "veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:0:foodChoice", "V");
                    }
                    else
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:0:foodChoice", "D");
                    }

                }
                if (!string.IsNullOrEmpty(passname2))
                {
                    ie.SetInputElementValueByTrimmedName("addPassengerForm:psdetail:1:p", passname2);
                    ie.SetInputElementValueByName("addPassengerForm:psdetail:1:psgnAge", passage2);
                    ie.SelectOptionByName("addPassengerForm:psdetail:1:psgnGender", passGender2);
                    if (!string.IsNullOrEmpty(passbrth2))
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:1:berthChoice", passbrth2);
                    }

                    if (passmeal2.ToString().ToLower().Trim() == "non-veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:1:foodChoice", "N");
                    }
                    else if (passmeal2.ToString().ToLower().Trim() == "veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:1:foodChoice", "V");
                    }
                    else
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:1:foodChoice", "D");
                    }
                }
                if (!string.IsNullOrEmpty(passname3))
                {
                    ie.SetInputElementValueByTrimmedName("addPassengerForm:psdetail:2:p", passname3);
                    ie.SetInputElementValueByName("addPassengerForm:psdetail:2:psgnAge", passage3);
                    ie.SelectOptionByName("addPassengerForm:psdetail:2:psgnGender", passGender3);
                    if (!string.IsNullOrEmpty(passbrth3))
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:2:berthChoice", passbrth3);
                    }

                    if (passmeal3.ToString().ToLower().Trim() == "non-veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:2:foodChoice", "N");
                    }
                    else if (passmeal3.ToString().ToLower().Trim() == "veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:2:foodChoice", "V");
                    }
                    else
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:2:foodChoice", "D");
                    }
                }
                if (!string.IsNullOrEmpty(passname4))
                {
                    ie.SetInputElementValueByTrimmedName("addPassengerForm:psdetail:3:p", passname4);
                    ie.SetInputElementValueByName("addPassengerForm:psdetail:3:psgnAge", passage4);
                    ie.SelectOptionByName("addPassengerForm:psdetail:3:psgnGender", passGender4);
                    if (!string.IsNullOrEmpty(passbrth4))
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:3:berthChoice", passbrth4);
                    }

                    if (passmeal4.ToString().ToLower().Trim() == "non-veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:3:foodChoice", "N");
                    }
                    else if (passmeal4.ToString().ToLower().Trim() == "veg")
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:3:foodChoice", "V");
                    }
                    else
                    {
                        ie.SelectOptionByName("addPassengerForm:psdetail:3:foodChoice", "D");
                    }
                }


                //child details
                if (!string.IsNullOrEmpty(passchildname1))
                {
                    string[] age = passchildage1.ToString().Split('-');
                    ie.SetInputElementValueByName("addPassengerForm:childInfoTable:0:infantName", passchildname1);
                    ie.SelectOptionByName("addPassengerForm:childInfoTable:0:infantAge", age[1].TrimStart());
                    ie.SelectOptionByName("addPassengerForm:childInfoTable:0:infantGender", passchildgen1);

                }

                if (!string.IsNullOrEmpty(passchildname2))
                {
                    string[] age1 = passchildage2.ToString().Split('-');
                    ie.SetInputElementValueByName("addPassengerForm:childInfoTable:1:infantName", passchildname2);
                    ie.SelectOptionByName("addPassengerForm:childInfoTable:1:infantAge", age1[1].TrimStart());
                    ie.SelectOptionByName("addPassengerForm:childInfoTable:1:infantGender", passchildgen2);
                }

                if (considerforautoupgradation)
                {
                    ie.ClickElementByName("addPassengerForm:autoUpgrade");
                }

                if (onlyconfirmbirth)
                {
                    ie.ClickElementByName("addPassengerForm:onlyConfirmBerths");
                }

                if (!string.IsNullOrEmpty(mobileno))
                {
                    //mobile no
                    ie.SetInputElementValueByName("addPassengerForm:mobileNo", mobileno);
                }
                BringWindowToTop((IntPtr)ie.IEObject.HWND);
                ShowWindow((IntPtr)ie.IEObject.HWND, 3);
                SetFocus((IntPtr)ie.IEObject.HWND);
                SendKeyStroke_API(35, 1);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception--"+ex.Message);
                Environment.Exit(0);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        public static void TravelDetails()
        {
            try
            {
                do
                {
                    Thread.Sleep(250);

                } while (!ie.isIEObjectAvailable("E-Ticketing"));
                ie.WaitForIEReadyState();

                do
                {
                    Thread.Sleep(1000);

                } while (!ie.SetInputElementValueByName("jpform:fromStation", FromStation));

                //train details
                //ie.SetInputElementValueByName("jpform:fromStation", jpform_fromStation.Text.ToString());
                ie.SetInputElementValueByName("jpform:toStation", ToStation);
                ie.SetInputElementValueByName("jpform:journeyDateInputDate", TravelDate);
                ie.ClickElementByName("jpform:jpsubmit");

                do
                {
                    Thread.Sleep(1000);

                } while (!ie.isIEObjectAvailable("Journey Planner"));
                ie.WaitForIEReadyState();



                string optionValue = string.Empty;
                if (CmbQuota == "General")
                {
                    optionValue = "GN";
                }
                if (CmbQuota == "Tatkal")
                {
                    optionValue = "TQ";
                }
                if (CmbQuota == "Premium Tatkal")
                {
                    optionValue = "PT";
                }
                if (CmbQuota == "Ladies")
                {
                    optionValue = "LD";
                }

                do
                {
                    Thread.Sleep(1000);

                } while (!ie.ClickElementByValue(optionValue));



                string[] trainName =TrainName.ToString().Split('-');
                string trainNo = trainName[1].TrimStart(' ');
                #region commented
                //if (optionValue == "TQ")
                //{
                //    TimeSpan Acstart = new TimeSpan(10, 0, 0); //10 o'clock
                //    TimeSpan Slstart = new TimeSpan(11, 00, 0);//11 o'clock
                //    TimeSpan flag = new TimeSpan(10, 57, 0);
                //    TimeSpan now = DateTime.Now.TimeOfDay;

                //    if(now<Acstart)
                //    {
                //        do
                //        {
                //            Thread.Sleep(1000);
                //            now = DateTime.Now.TimeOfDay;

                //        } while (!(now >= Acstart));
                //    }
                //    if (TicketClass == "SL")
                //    {
                //        do
                //        {
                //            Thread.Sleep(1000);
                //            now = DateTime.Now.TimeOfDay;

                //        } while (!(now >= Slstart));
                //    }
                //}
                #endregion
                int i = 0;
                do
                {
                    Thread.Sleep(1000);
                    i++;
                    if (i == 10)
                        break;

                } while (!ie.ClickAnchorElement(trainNo.TrimEnd(' '), TicketClass));
                i = 0;
                do
                {
                    Thread.Sleep(1000);
                    if (i == 10)
                        break;
                } while (!ie.ClickAnchorElement(trainNo.TrimEnd(' '), Datedmyyyy, "Book Now"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception---"+ex.Message);
                Environment.Exit(0);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoginToIRCTC()
        {
            try
            {
                do
                {
                    Thread.Sleep(250);

                } while (!ie.SetInputElementValueByName(j_username.Name, j_username.Text.ToString()));

                //login info
                //ie.SetInputElementValueByName(j_username.Name, j_username.Text.ToString());
                ie.SetInputElementValueByName(j_password.Name, j_password.Text.ToString());

                //do
                //{

                //    //login info
                //    ie.SetInputElementValueByName(j_username.Name, j_username.Text.ToString());
                //    ie.SetInputElementValueByName(j_password.Name, j_password.Text.ToString());
                //    ////set focus to captcha textbox
                //    //if (!ie.isIEObjectAvailable("E-Ticketing"))
                //    //{
                //    //    TravelName frm = new TravelName(0);
                //    //    frm.ShowDialog();
                //    //}
                //    //ie.SetInputElementValueByName("j_captcha", Travelname);
                //    //ie.ClickElementByName("submit");
                //    //Thread.Sleep(2000);

                //} while (ie.ClickElementByValue("OK"));//loginerrorpanelok

            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception--"+ex.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ClickonDebitCardDetails()
        {
            try
            {
                switch (banknameselectedindex)
                {
                    case 0: ie.ClickElementByValueAndName("DEBIT_CARD", "3");
                        break;
                    case 1: ie.ClickElementByValueAndName("DEBIT_CARD", "5");
                        break;
                    case 2: ie.ClickElementByValueAndName("DEBIT_CARD", "9");
                        break;
                    case 3: ie.ClickElementByValueAndName("DEBIT_CARD", "15");
                        break;
                    case 4: ie.ClickElementByValueAndName("DEBIT_CARD", "16");
                        break;
                    case 5: ie.ClickElementByValueAndName("DEBIT_CARD", "19");
                        break;
                    case 6: ie.ClickElementByValueAndName("DEBIT_CARD", "25");
                        break;
                    case 7: ie.ClickElementByValueAndName("DEBIT_CARD", "26");
                        break;
                    case 8: ie.ClickElementByValueAndName("DEBIT_CARD", "41");
                        break;
                    case 9: ie.ClickElementByValueAndName("DEBIT_CARD", "57");
                        break;
                    case 10: ie.ClickElementByValueAndName("DEBIT_CARD", "69");
                        break;
                    case 11: ie.ClickElementByValueAndName("DEBIT_CARD", "66");
                        break;
                    case 12: ie.ClickElementByValueAndName("DEBIT_CARD", "86");
                        break;
                }
            }
            catch 
            {
                
                
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ClickonCreditCardDetails()
        {
            try
            {
                switch (banknameselectedindex)
                {
                    case 0: ie.ClickElementByValueAndName("CREDIT_CARD", "4");
                        break;
                    case 1: ie.ClickElementByValueAndName("CREDIT_CARD", "17");
                        break;
                    case 2: ie.ClickElementByValueAndName("CREDIT_CARD", "21");
                        break;
                    case 3: ie.ClickElementByValueAndName("CREDIT_CARD", "27");
                        break;
                    case 4: ie.ClickElementByValueAndName("CREDIT_CARD", "58");
                        break;
                }
            }
            catch 
            {
                
               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CardPaymentDetails()
        {
            try
            {
                if (cardtype.ToLower() == "master")
                {
                    ie.SelectOptionByName("card_type", "MC");
                }
                else
                {
                    ie.SelectOptionByName("card_type", "VISA");
                }
                ie.SetInputElementValueByName("card_no", cardno);
                ie.SelectOptionByName("card_expiry_mon", expmonth.ToString().Substring(0, 2));
                ie.SetInputElementValueByName("card_expiry_year", expyear.ToString().Trim());
                ie.SetInputElementValueByName("cvv_no", cvv.Trim());
                ie.SetInputElementValueByName("card_name", cardname.Trim());
                ie.setFocusToInputElementByID("captcha_txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception--"+ex.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ClickonNetBankingDetails()
        {
            try
            {
                switch (banknameselectedindex)
                {
                    case 0: ie.ClickElementByValueAndName("NETBANKING", "1");
                        break;
                    case 1: ie.ClickElementByValueAndName("NETBANKING", "22");
                        break;
                    case 2: ie.ClickElementByValueAndName("NETBANKING", "29");
                        break;
                    case 3: ie.ClickElementByValueAndName("NETBANKING", "28");
                        break;
                    case 4: ie.ClickElementByValueAndName("NETBANKING", "31");
                        break;
                    case 5: ie.ClickElementByValueAndName("NETBANKING", "34");
                        break;
                    case 6: ie.ClickElementByValueAndName("NETBANKING", "35");
                        break;
                    case 7: ie.ClickElementByValueAndName("NETBANKING", "38");
                        break;
                    case 8: ie.ClickElementByValueAndName("NETBANKING", "39");
                        break;
                    case 9: ie.ClickElementByValueAndName("NETBANKING", "36");
                        break;
                    case 10: ie.ClickElementByValueAndName("NETBANKING", "37");
                        break;
                    case 11: ie.ClickElementByValueAndName("NETBANKING", "42");
                        break;
                    case 12: ie.ClickElementByValueAndName("NETBANKING", "43");
                        break;
                    case 13: ie.ClickElementByValueAndName("NETBANKING", "40");
                        break;
                    case 14: ie.ClickElementByValueAndName("NETBANKING", "46");
                        break;
                    case 15: ie.ClickElementByValueAndName("NETBANKING", "44");
                        break;
                    case 16: ie.ClickElementByValueAndName("NETBANKING", "45");
                        break;
                    case 17: ie.ClickElementByValueAndName("NETBANKING", "50");
                        break;
                    case 18: ie.ClickElementByValueAndName("NETBANKING", "48");
                        break;
                    case 19: ie.ClickElementByValueAndName("NETBANKING", "54");
                        break;
                    case 20: ie.ClickElementByValueAndName("NETBANKING", "53");
                        break;
                    case 21: ie.ClickElementByValueAndName("NETBANKING", "52");
                        break;
                    case 22: ie.ClickElementByValueAndName("NETBANKING", "56");
                        break;
                    case 23: ie.ClickElementByValueAndName("NETBANKING", "60");
                        break;
                    case 24: ie.ClickElementByValueAndName("NETBANKING", "64");
                        break;
                    case 25: ie.ClickElementByValueAndName("NETBANKING", "67");
                        break;
                    case 26:
                        ie.ClickElementByValueAndName("NETBANKING", "81");
                        break;
                    case 27:
                        ie.ClickElementByValueAndName("NETBANKING", "80");
                        break;
                }
            }
            catch
            {
                
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveFrmDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateform())
                {
                    TravelName frm = new TravelName();
                    frm.ShowDialog();
                    string cfa = "No";
                    string Bocfba = "No";
                    string tI = "No";
                    if (chk_considerforautoupgradation.Checked)
                    {
                        cfa = "Yes";
                    }
                    if (chk_onlyifconfirmberths.Checked)
                    {
                        Bocfba = "Yes";
                    }
                    if (chkTravelinsurance.Checked)
                    {
                        tI = "Yes";
                    }
                    con.Open();
                    String cmd = string.Empty;
                    if (grpCardDetails.Enabled)
                    {
                        cmd = "Insert into SavedFormDetails (TravelName,UserID,[Password],FromStation,ToStation,TrainName,Class,BoardingStation," +
                        "ConsiderForAutoUpgradation,BookOnlyConfirmBirthsAllocated,TravelInsurance,MobileNo,PaymentType," +
                        "BankName,CardType,CardNumber,ExpiryMonth,ExpiryYear,Cvv,NameOnCard,[Date],Quota) values ('" + Travelname + "','" + j_username.Text + "'," +
                        "'" + j_password.Text + "','" + jpform_fromStation.Text + "','" + jpform_toStation.Text + "','" + txtTrainName.Text + "'," +
                        "'" + cmbClass.SelectedItem.ToString() + "','" + txtboardingstation.Text + "','" + cfa + "','" + Bocfba + "','" + tI + "','" + txtMobileno.Text + "'," +
                        "'" + cmbPaymentType.SelectedItem.ToString() + "','" + cmbBankName.SelectedItem.ToString() + "','" + cmbCardType.SelectedItem.ToString() + "'," +
                        "'" + txtCardNumber.Text + "','" + cmbExpmonth.SelectedItem.ToString() + "','" + txtExpiryYear.Text + "','" + txtCVV.Text + "','" + txtNameonCard.Text + "','" + jpform_journeyDateInputDate.Value.ToString("dd-MM-yyyy") + "','"+cmbQuota.SelectedItem.ToString()+"')";
                    }
                    else
                    {
                        cmd = "Insert into SavedFormDetails (TravelName,UserID,[Password],FromStation,ToStation,TrainName,Class,BoardingStation," +
                        "ConsiderForAutoUpgradation,BookOnlyConfirmBirthsAllocated,TravelInsurance,MobileNo,PaymentType," +
                        "BankName,CardType,CardNumber,ExpiryMonth,ExpiryYear,Cvv,NameOnCard,[Date],Quota) values ('" + Travelname + "','" + j_username.Text + "'," +
                        "'" + j_password.Text + "','" + jpform_fromStation.Text + "','" + jpform_toStation.Text + "','" + txtTrainName.Text + "'," +
                        "'" + cmbClass.SelectedItem.ToString() + "','" + txtboardingstation.Text + "','" + cfa + "','" + Bocfba + "','" + tI + "','" + txtMobileno.Text + "'," +
                        "'" + cmbPaymentType.SelectedItem.ToString() + "','" + cmbBankName.SelectedItem.ToString() + "','null'," +
                        "'null','null','null','null','null','" + jpform_journeyDateInputDate.Value.ToString("dd-MM-yyyy") + "','"+cmbQuota.SelectedItem.ToString()+"')";
                    }
                    OleDbCommand saveFormDetails = new OleDbCommand(cmd, con);
                    saveFormDetails.ExecuteNonQuery();
                    //SqlCommand saveFormDetails = new SqlCommand(cmd, con);
                    //SqlCeCommand saveFormDetails = new SqlCeCommand(cmd, con);
                    //saveFormDetails.ExecuteNonQuery();
                    con.Close();
                    DialogResult res = Show("Do you want add Passengers Names to Passengers List", "UserMessage", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        SavePassengerNames();
                    }

                    Show("Saved Details Sucessfully", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        private void SavePassengerNames()
        {
            try
            {
                string cmd = string.Empty;
                //SqlCommand passengernames;
                OleDbCommand passengernames;
                string birthPreference = "No";
                con.Open();
                if (cmb_pass_bp0.SelectedIndex == -1)
                {
                    cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name0.Text.ToString() + "','" + txt_pass_age0.Text.ToString() + "','" + cmb_pass_gender0.SelectedItem.ToString() + "','" + birthPreference + "','"+cmb_Meal_p0.SelectedItem.ToString()+"')";
                }
                else
                {
                    cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name0.Text.ToString() + "','" + txt_pass_age0.Text.ToString() + "','" + cmb_pass_gender0.SelectedItem.ToString() + "','" + cmb_pass_bp0.SelectedItem.ToString() + "','" + cmb_Meal_p0.SelectedItem.ToString() + "')";
                }
                passengernames = new OleDbCommand(cmd, con);
                passengernames.ExecuteNonQuery();
                //passengernames = new SqlCommand(cmd, con);
                //passengernames.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(txt_pass_name1.Text.ToString()) && !string.IsNullOrEmpty(txt_pass_age1.Text.ToString()) && cmb_pass_gender1.SelectedIndex != -1)
                {
                    if (cmb_pass_bp1.SelectedIndex == -1)
                    {
                        cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name1.Text.ToString() + "','" + txt_pass_age1.Text.ToString() + "','" + cmb_pass_gender1.SelectedItem.ToString() + "','" + birthPreference + "','" + cmb_Meal_p1.SelectedItem.ToString() + "')";
                    }
                    else
                    {
                        cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name1.Text.ToString() + "','" + txt_pass_age1.Text.ToString() + "','" + cmb_pass_gender1.SelectedItem.ToString() + "','" + cmb_pass_bp1.SelectedItem.ToString() + "','" + cmb_Meal_p1.SelectedItem.ToString() + "')";

                    }
                    passengernames = new OleDbCommand(cmd, con);
                    passengernames.ExecuteNonQuery();
                    //passengernames = new SqlCommand(cmd, con);
                    //passengernames.ExecuteNonQuery();
                }
                if (!string.IsNullOrEmpty(txt_pass_name2.Text.ToString()) && !string.IsNullOrEmpty(txt_pass_age2.Text.ToString()) && cmb_pass_gender2.SelectedIndex != -1)
                {
                    if (cmb_pass_bp2.SelectedIndex == -1)
                    {
                        cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name2.Text.ToString() + "','" + txt_pass_age2.Text.ToString() + "','" + cmb_pass_gender2.SelectedItem.ToString() + "','" + birthPreference + "','" + cmb_Meal_p2.SelectedItem.ToString() + "')";
                    }
                    else
                    {
                        cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name2.Text.ToString() + "','" + txt_pass_age2.Text.ToString() + "','" + cmb_pass_gender2.SelectedItem.ToString() + "','" + cmb_pass_bp2.SelectedItem.ToString() + "','" + cmb_Meal_p2.SelectedItem.ToString() + "')";
                    }
                    passengernames = new OleDbCommand(cmd, con);
                    passengernames.ExecuteNonQuery();
                    //passengernames = new SqlCommand(cmd, con);
                    //passengernames.ExecuteNonQuery();
                }
                if (!string.IsNullOrEmpty(txt_pass_name3.Text.ToString()) && !string.IsNullOrEmpty(txt_pass_age3.Text.ToString()) && cmb_pass_gender3.SelectedIndex != -1)
                {
                    if (cmb_pass_bp3.SelectedIndex == -1)
                    {
                        cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name3.Text.ToString() + "','" + txt_pass_age3.Text.ToString() + "','" + cmb_pass_gender3.SelectedItem.ToString() + "','" + birthPreference + "','" + cmb_Meal_p3.SelectedItem.ToString() + "')";
                    }
                    else
                    {
                        cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name3.Text.ToString() + "','" + txt_pass_age3.Text.ToString() + "','" + cmb_pass_gender3.SelectedItem.ToString() + "','" + cmb_pass_bp3.SelectedItem.ToString() + "','" + cmb_Meal_p3.SelectedItem.ToString() + "')";
                    }
                    passengernames = new OleDbCommand(cmd, con);
                    passengernames.ExecuteNonQuery();
                    //passengernames = new SqlCommand(cmd, con);
                    //passengernames.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(txt_pass_name_child0.Text.ToString()) && cmb_pass_Child_age0.SelectedIndex != -1 && cmb_pass_Gender_child0.SelectedIndex != -1)
                {
                    cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name_child0.Text.ToString() + "','" + cmb_pass_Child_age0.SelectedItem.ToString() + "','" + cmb_pass_Gender_child0.SelectedItem.ToString() + "','null','null')";
                    passengernames = new OleDbCommand(cmd, con);
                    passengernames.ExecuteNonQuery();
                    //passengernames = new SqlCommand(cmd, con);
                    //passengernames.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(txt_pass_name_child1.Text.ToString()) && cmb_pass_Child_age1.SelectedIndex != -1 && cmb_pass_Gender_child1.SelectedIndex != -1)
                {
                    cmd = "Insert into PassengerDetails (PassengerName,Age,Gender,BerthPreference,Meal) values ('" + txt_pass_name_child1.Text.ToString() + "','" + cmb_pass_Child_age1.SelectedItem.ToString() + "','" + cmb_pass_Gender_child1.SelectedItem.ToString() + "','null','null')";
                    passengernames = new OleDbCommand(cmd, con);
                    passengernames.ExecuteNonQuery();
                    //passengernames = new SqlCommand(cmd, con);
                    //passengernames.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("PRIMARY KEY constraint"))
                {
                    Show("Existing Passenger Names Cannot be saved", "UserMessage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean validateform()
        {
            try
            {
                string[] errormsg = new string[11];
                int i = 0;
                if (string.IsNullOrEmpty(j_username.Text.ToString()) || string.IsNullOrEmpty(j_password.Text.ToString()))
                {
                    errormsg[i] = "Login Details UserId/Password Should not be Blank";
                    i++;
                }

                if (string.IsNullOrEmpty(jpform_fromStation.Text.ToString()) || string.IsNullOrEmpty(jpform_toStation.Text.ToString()))
                {
                    errormsg[i] = "Journey Details FromStation/ToStation Should not be Blank";
                    i++;
                }

                if (string.IsNullOrEmpty(txtTrainName.Text.ToString()) || cmbClass.SelectedIndex == -1 || cmbQuota.SelectedIndex==-1)
                {
                    errormsg[i] = "Train Details TrainName/Class Should not be Blank";
                    i++;
                }

                if (cmbPaymentType.SelectedIndex == -1 || cmbBankName.SelectedIndex == -1)
                {
                    errormsg[i] = "Payment Details PaymentType/BankName Should not be Blank";
                    i++;
                }
                else
                {
                    paymenttype = cmbPaymentType.SelectedItem.ToString();
                    bankname = cmbBankName.SelectedItem.ToString();
                    banknameselectedindex = cmbBankName.SelectedIndex;
                }

                if (cmbBankName.SelectedIndex != -1)
                {
                    if (cmbBankName.SelectedItem.ToString().ToLower() == "Visa/Master Card HDFC BANK".ToLower())
                    {
                        if (cmbCardType.SelectedIndex == -1 || string.IsNullOrEmpty(txtCardNumber.Text.ToString()) || cmbExpmonth.SelectedIndex == -1 || string.IsNullOrEmpty(txtExpiryYear.Text.ToString()) || string.IsNullOrEmpty(txtCVV.Text.ToString()) || string.IsNullOrEmpty(txtNameonCard.Text.ToString()))
                        {
                            errormsg[i] = "Please check Card Details - Should not be Blank";
                            i++;
                        }
                        else
                        {
                            cardtype = cmbCardType.SelectedItem.ToString();
                            cardno = txtCardNumber.Text.ToString();
                            expmonth = cmbExpmonth.SelectedItem.ToString();
                            expyear = txtExpiryYear.Text.ToString();
                            cardname = txtNameonCard.Text.ToString();
                            cvv = txtCVV.Text.ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(txt_pass_name0.Text.ToString()) || string.IsNullOrEmpty(txt_pass_age0.Text.ToString()) || cmb_pass_gender0.SelectedIndex == -1 || cmb_Meal_p0.SelectedIndex==-1)
                {
                    errormsg[i] = "Please Check Passenger details S.No 1 - Should not be Blank Except BirthPreference(Atleast One Passenger is Required)";
                    i++;
                }

                if (!string.IsNullOrEmpty(txt_pass_name1.Text.ToString()) || !string.IsNullOrEmpty(txt_pass_age1.Text.ToString()) || cmb_pass_gender1.SelectedIndex != -1 || cmb_Meal_p1.SelectedIndex != -1)
                {
                    if (string.IsNullOrEmpty(txt_pass_name1.Text.ToString()) || string.IsNullOrEmpty(txt_pass_age1.Text.ToString()) || cmb_pass_gender1.SelectedIndex == -1 || cmb_Meal_p1.SelectedIndex == -1)
                    {
                        errormsg[i] = "Please Check Passenger details S.No 2 - Should not be Blank Except BirthPreference";
                        i++;
                    }

                }
                if (!string.IsNullOrEmpty(txt_pass_name2.Text.ToString()) || !string.IsNullOrEmpty(txt_pass_age2.Text.ToString()) || cmb_pass_gender2.SelectedIndex != -1 || cmb_Meal_p2.SelectedIndex != -1)
                {
                    if (string.IsNullOrEmpty(txt_pass_name2.Text.ToString()) || string.IsNullOrEmpty(txt_pass_age2.Text.ToString()) || cmb_pass_gender2.SelectedIndex == -1 || cmb_Meal_p2.SelectedIndex == -1)
                    {
                        errormsg[i] = "Please Check Passenger details S.No 3 - Should not be Blank Except BirthPreference";
                        i++;
                    }
                }
                if (!string.IsNullOrEmpty(txt_pass_name3.Text.ToString()) || !string.IsNullOrEmpty(txt_pass_age3.Text.ToString()) || cmb_pass_gender3.SelectedIndex != -1 || cmb_Meal_p3.SelectedIndex != -1)
                {
                    if (string.IsNullOrEmpty(txt_pass_name3.Text.ToString()) || string.IsNullOrEmpty(txt_pass_age3.Text.ToString()) || cmb_pass_gender3.SelectedIndex == -1 || cmb_Meal_p3.SelectedIndex == -1)
                    {
                        errormsg[i] = "Please Check Passenger details S.No 4 - Should not be Blank Except BirthPreference";
                        i++;
                    }
                }

                if (!string.IsNullOrEmpty(txt_pass_name_child0.Text.ToString()) || cmb_pass_Child_age0.SelectedIndex != -1 || cmb_pass_Gender_child0.SelectedIndex != -1)
                {
                    if (string.IsNullOrEmpty(txt_pass_name_child0.Text.ToString()) || cmb_pass_Child_age0.SelectedIndex == -1 || cmb_pass_Gender_child0.SelectedIndex == -1)
                    {
                        errormsg[i] = "Please Check Children Details S.No 1 - Should not be Blank";
                        i++;
                    }
                }

                if (!string.IsNullOrEmpty(txt_pass_name_child1.Text.ToString()) || cmb_pass_Child_age1.SelectedIndex != -1 || cmb_pass_Gender_child1.SelectedIndex != -1)
                {
                    if (string.IsNullOrEmpty(txt_pass_name_child1.Text.ToString()) || cmb_pass_Child_age1.SelectedIndex == -1 || cmb_pass_Gender_child1.SelectedIndex == -1)
                    {
                        errormsg[i] = "Please Check Children Details S.No 2 - Should not be Blank";
                        i++;
                    }
                }

                if (!string.IsNullOrEmpty(errormsg[0]))
                {
                    DialogResult res = Show(string.Join("\n", errormsg).TrimEnd(), "UserMesaage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception--"+ex.Message);
                return false;
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetSavedDetails_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtSavedFormDetails = new DataTable();
                DataTable dtPassengersDetails = new DataTable();
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select * from SavedFormDetails", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd.CommandText, con);
                //SqlCommand cmd = new SqlCommand("Select * from SavedFormDetails", con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd.CommandText, con);
                da.Fill(dtSavedFormDetails);
                con.Close();
                //da.Dispose();
                if (dtSavedFormDetails.Rows.Count == 0)
                {
                    Show("Saved Details Not Found", "User Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SavedFormDetails frm1 = new SavedFormDetails(dtSavedFormDetails);
                    var res =  frm1.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        ClearFormDetails();

                        j_username.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["UserID"].ToString();
                        j_password.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["Password"].ToString();
                        jpform_fromStation.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["FromStation"].ToString();
                        jpform_toStation.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["ToStation"].ToString();
                        txtTrainName.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["TrainName"].ToString();
                        cmbClass.SelectedIndex = cmbClass.FindStringExact(dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["Class"].ToString());
                        txtboardingstation.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["BoardingStation"].ToString();
                        if (dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["ConsiderForAutoUpgradation"].ToString().ToLower() != "no")
                        {
                            chk_considerforautoupgradation.Checked = true;
                        }
                        if (dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["BookOnlyConfirmBirthsAllocated"].ToString().ToLower() != "no")
                        {
                            chk_onlyifconfirmberths.Checked = true;
                        }
                        if (dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["TravelInsurance"].ToString().ToLower() != "no")
                        {
                            chkTravelinsurance.Checked = true;
                        }
                        cmbQuota.SelectedIndex = cmbQuota.FindStringExact(dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["Quota"].ToString());
                        txtMobileno.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["MobileNo"].ToString();
                        cmbPaymentType.SelectedIndex = cmbPaymentType.FindStringExact(dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["PaymentType"].ToString());
                        cmbBankName.SelectedIndex = cmbBankName.FindStringExact(dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["BankName"].ToString());
                        if (dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["CardType"].ToString().ToLower() != "null")
                        {
                            cmbCardType.SelectedIndex = cmbCardType.FindStringExact(dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["CardType"].ToString());
                            txtCardNumber.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["CardNumber"].ToString();
                            cmbExpmonth.SelectedIndex = cmbExpmonth.FindStringExact(dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["ExpiryMonth"].ToString());
                            txtExpiryYear.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["ExpiryYear"].ToString();
                            txtCVV.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["Cvv"].ToString();
                            txtNameonCard.Text = dtSavedFormDetails.Rows[SavedFormDetailsRowIndex]["NameOnCard"].ToString();
                        }
                        Show("Please Select Journey Date as Date is not Populated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearFormDetails()
        {
            try
            {
                j_username.Clear();
                j_password.Clear();
                jpform_fromStation.Clear();
                jpform_toStation.Clear();
                txtTrainName.Clear();
                cmbClass.SelectedIndex = -1;
                txtboardingstation.Clear();
                chk_considerforautoupgradation.Checked = false;
                chk_onlyifconfirmberths.Checked = false;
                chkTravelinsurance.Checked = false;
                txtMobileno.Clear();
                cmbPaymentType.SelectedIndex = -1;
                cmbBankName.SelectedIndex = -1;
                cmbQuota.SelectedIndex = -1;

                cmbCardType.SelectedIndex = -1;
                txtCardNumber.Clear();
                cmbExpmonth.SelectedIndex = -1;
                txtExpiryYear.Clear();
                txtCVV.Clear();
                txtNameonCard.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messagetype"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public DialogResult Show(string message, string messagetype, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            Form topmostForm = new Form();

            topmostForm.Size = new System.Drawing.Size(1, 1);
            topmostForm.StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
            topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10, rect.Right + 10);
            topmostForm.Show();

            topmostForm.Focus();
            topmostForm.BringToFront();
            topmostForm.TopMost = true;

            DialogResult result = MessageBox.Show(topmostForm, message, messagetype, buttons, icon);
            topmostForm.Dispose();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Combinationkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SendKeyStroke_API(int Combinationkey, int key)
        {
            try
            {
                keybd_event((byte)Combinationkey, 0x45, 0, (UIntPtr)0);
                keybd_event((byte)key, 0x45, 0, (UIntPtr)0);
                keybd_event((byte)key, 0x45, 0x2, (UIntPtr)0);
                keybd_event((byte)Combinationkey, 0x45, 0x2, (UIntPtr)0);
                return true;
            }
            catch
            {

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            lblTimer.Text = DateTime.Now.ToLongTimeString();
            //btnBookNow.Enabled = false;
            grpCardDetails.Enabled = false;
            cmbBankName.Enabled = false;
            jpform_journeyDateInputDate.Format = DateTimePickerFormat.Custom;
            jpform_journeyDateInputDate.CustomFormat = "dd-MM-yyyy";
            jpform_journeyDateInputDate.MinDate = DateTime.Now;

            AutoCompleteStringCollection namesCollection_Stations = new AutoCompleteStringCollection();
            AutoCompleteStringCollection namesCollection_TrainNames = new AutoCompleteStringCollection();
            
            //con.Open();

            //for stations autopopulate
            DataTable Stations = new DataTable();
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "Select * from Stations";
            cmd.Connection = con;
            //cmd.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd.CommandText, con);
            //MessageBox.Show("Record Submitted", "Congrats");
            con.Close();
            //SqlCommand cmd = new SqlCommand("Select * from Stations", con);
            //SqlCeCommand cmdStations = new SqlCeCommand("Select * from Stations", con);
            //SqlCeDataAdapter da = new SqlCeDataAdapter(cmdStations.CommandText, con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd.CommandText, con);
            da.Fill(Stations);
            da.Dispose();
            for (int i = 0; i < Stations.Rows.Count; i++)
            {
                namesCollection_Stations.Add(Stations.Rows[i][0].ToString());
            }

            //for train names autopopulate
            DataTable TrainNames = new DataTable();
            OleDbCommand cmdTrainNames = new OleDbCommand("Select * from Train_Names", con);
            OleDbDataAdapter da2 = new OleDbDataAdapter(cmdTrainNames.CommandText, con);
            //SqlCeCommand cmdTrainNames = new SqlCeCommand("Select * from Train_Names", con);
            //SqlCeDataAdapter da2 = new SqlCeDataAdapter(cmdTrainNames.CommandText, con);
            //SqlCommand cmdTrainNames = new SqlCommand("Select * from Train_Names", con);
            //SqlDataAdapter da2 = new SqlDataAdapter(cmdTrainNames.CommandText, con);
            da2.Fill(TrainNames);
            da2.Dispose();
            for (int i = 0; i < TrainNames.Rows.Count; i++)
            {
                namesCollection_TrainNames.Add(TrainNames.Rows[i][1].ToString() +" "+ "-" +" "+ TrainNames.Rows[i][0].ToString());
            }

           
            con.Close();

            jpform_fromStation.AutoCompleteCustomSource = namesCollection_Stations ;
            jpform_toStation.AutoCompleteCustomSource = namesCollection_Stations;
            txtboardingstation.AutoCompleteCustomSource = namesCollection_Stations;
            txtTrainName.AutoCompleteCustomSource = namesCollection_TrainNames;

        }


        
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="ballontipText"></param>
       /// <returns></returns>
        private Boolean DisplayBalloonTip(String ballontipText,int duration)
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.BalloonTipTitle = "IRCTC User Information";
            notifyIcon.BalloonTipText = ballontipText;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(duration);
            return true; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean KillBalloonTip()
        {
            notifyIcon.Icon = null;
            notifyIcon.BalloonTipTitle = string.Empty;
            notifyIcon.BalloonTipText = string.Empty;
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtboardingstation_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_age0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_age1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_age2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_age3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_name0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_name1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_name2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_name3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_name_child0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pass_name_child1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jpform_fromStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jpform_toStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentType.SelectedIndex != -1)
            {
                cmbBankName.Enabled = true;
                if (cmbBankName.Items.Count != 0 || cmbBankName.Items.Count > 0)
                {
                    cmbBankName.Items.Clear();
                }

                if (cmbPaymentType.SelectedItem.ToString() == "Net Banking")
                {
                    cmbBankName.Items.Add("State Bank of India");
                    //cmbBankName.Items.Add("State Bank of India and Associates");
                    cmbBankName.Items.Add("Federal Bank");
                    cmbBankName.Items.Add("Indian Bank");
                    cmbBankName.Items.Add("Union Bank of India");
                    cmbBankName.Items.Add("Andhra Bank");
                    cmbBankName.Items.Add("Punjab National Bank");
                    cmbBankName.Items.Add("Allahabad Bank");
                    cmbBankName.Items.Add("Vijaya Bank");
                    cmbBankName.Items.Add("AXIS Bank");
                    cmbBankName.Items.Add("HDFC Bank");
                    cmbBankName.Items.Add("Bank of Baroda");
                    cmbBankName.Items.Add("Karnataka Bank");
                    cmbBankName.Items.Add("Oriental Bank of Commerce");
                    cmbBankName.Items.Add("Karur Vysya Bank");
                    cmbBankName.Items.Add("Kotak Mahindra Bank");
                    cmbBankName.Items.Add("ICICI Bank");
                    cmbBankName.Items.Add("IndusInd Bank");
                    cmbBankName.Items.Add("Central Bank of India");
                    cmbBankName.Items.Add("Bank of India");
                    cmbBankName.Items.Add("Syndicate Bank");
                    cmbBankName.Items.Add("Bank of Maharashatra");
                    cmbBankName.Items.Add("IDBI Bank");
                    cmbBankName.Items.Add("Corporation Bank");
                    cmbBankName.Items.Add("Yes Bank");
                    cmbBankName.Items.Add("Nepal SBI Bank Ltd.");
                    cmbBankName.Items.Add("South Indian Bank");
                    cmbBankName.Items.Add("Canara Bank");
                    cmbBankName.Items.Add("City Union Bank");

                }

                if (cmbPaymentType.SelectedItem.ToString() == "Payment Gateway /Credit /Debit Cards")
                {
                    cmbBankName.Items.Add("Visa/Master Card ICICI BANK");
                    cmbBankName.Items.Add("Visa/Master Card CITI BANK");
                    cmbBankName.Items.Add("Visa/Master Card HDFC BANK");
                    cmbBankName.Items.Add("American Express");
                    //cmbBankName.Items.Add("Visa/Master Card AXIS BANK");
                    cmbBankName.Items.Add("RuPay Card Kotak Bank");
                }

                if (cmbPaymentType.SelectedItem.ToString() == "Debit Card with PIN")
                {
                    cmbBankName.Items.Add("State Bank of India");
                    cmbBankName.Items.Add("Indian Overseas Bank");
                    cmbBankName.Items.Add("Punjab National Bank");
                    cmbBankName.Items.Add("Indian Bank");
                    cmbBankName.Items.Add("Union Bank of India");
                    cmbBankName.Items.Add("Bank of India");
                    cmbBankName.Items.Add("Andhra Bank");
                    cmbBankName.Items.Add("Canara Bank");
                    //cmbBankName.Items.Add("CITI Bank");
                    cmbBankName.Items.Add("ICICI Bank");
                    cmbBankName.Items.Add("HDFC Bank");
                    cmbBankName.Items.Add("Central Bank of India");
                    cmbBankName.Items.Add("AXIS Bank");
                    cmbBankName.Items.Add("United Bank Of India");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentType.SelectedIndex != -1)
            {
                if (cmbBankName.SelectedItem.ToString() == "Visa/Master Card HDFC BANK")
                {
                    grpCardDetails.Enabled = true;
                }
                else
                {
                    grpCardDetails.Enabled = false;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnklblSavedPassengers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DataTable dtPassengerdetails = new DataTable();
                con.Open();
                OleDbCommand cmd = new OleDbCommand("select * from PassengerDetails", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd.CommandText, con);
                //SqlCommand cmd = new SqlCommand("select * from PassengerDetails", con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd.CommandText, con);
                da.Fill(dtPassengerdetails);
                con.Close();
                if (dtPassengerdetails.Rows.Count == 0)
                {
                    Show("Passengers list not found", "Usermessage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SavedFormDetails frm = new SavedFormDetails();
                    var res = frm.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        ClearPassengerDetails();

                        
                        switch (Adults.Count)
                        {
                            case 1: txt_pass_name0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["PassengerName"].ToString();
                                txt_pass_age0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Age"].ToString();
                                cmb_pass_gender0.SelectedIndex = cmb_pass_gender0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Gender"].ToString());
                                cmb_pass_bp0.SelectedIndex = cmb_pass_bp0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["BerthPreference"].ToString());
                                cmb_Meal_p0.SelectedIndex = cmb_Meal_p0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Meal"].ToString());
                                break;
                            case 2: txt_pass_name0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["PassengerName"].ToString();
                                txt_pass_age0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Age"].ToString();
                                cmb_pass_gender0.SelectedIndex = cmb_pass_gender0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Gender"].ToString());
                                cmb_pass_bp0.SelectedIndex = cmb_pass_bp0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["BerthPreference"].ToString());
                                cmb_Meal_p0.SelectedIndex = cmb_Meal_p0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Meal"].ToString());
                                txt_pass_name1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["PassengerName"].ToString();
                                txt_pass_age1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Age"].ToString();
                                cmb_pass_gender1.SelectedIndex = cmb_pass_gender1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Gender"].ToString());
                                cmb_pass_bp1.SelectedIndex = cmb_pass_bp1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["BerthPreference"].ToString());
                                cmb_Meal_p1.SelectedIndex = cmb_Meal_p1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Meal"].ToString());
                                break;
                            case 3: txt_pass_name0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["PassengerName"].ToString();
                                txt_pass_age0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Age"].ToString();
                                cmb_pass_gender0.SelectedIndex = cmb_pass_gender0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Gender"].ToString());
                                cmb_pass_bp0.SelectedIndex = cmb_pass_bp0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["BerthPreference"].ToString());
                                cmb_Meal_p0.SelectedIndex = cmb_Meal_p0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Meal"].ToString());
                                txt_pass_name1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["PassengerName"].ToString();
                                txt_pass_age1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Age"].ToString();
                                cmb_pass_gender1.SelectedIndex = cmb_pass_gender1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Gender"].ToString());
                                cmb_pass_bp1.SelectedIndex = cmb_pass_bp1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["BerthPreference"].ToString());
                                cmb_Meal_p1.SelectedIndex = cmb_Meal_p1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Meal"].ToString());
                                txt_pass_name2.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["PassengerName"].ToString();
                                txt_pass_age2.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["Age"].ToString();
                                cmb_pass_gender2.SelectedIndex = cmb_pass_gender2.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["Gender"].ToString());
                                cmb_pass_bp2.SelectedIndex = cmb_pass_bp2.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["BerthPreference"].ToString());
                                cmb_Meal_p2.SelectedIndex = cmb_Meal_p2.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["Meal"].ToString());
                                break;
                            case 4: txt_pass_name0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["PassengerName"].ToString();
                                txt_pass_age0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Age"].ToString();
                                cmb_pass_gender0.SelectedIndex = cmb_pass_gender0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Gender"].ToString());
                                cmb_pass_bp0.SelectedIndex = cmb_pass_bp0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["BerthPreference"].ToString());
                                cmb_Meal_p0.SelectedIndex = cmb_Meal_p0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[0])]["Meal"].ToString());
                                txt_pass_name1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["PassengerName"].ToString();
                                txt_pass_age1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Age"].ToString();
                                cmb_pass_gender1.SelectedIndex = cmb_pass_gender1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Gender"].ToString());
                                cmb_pass_bp1.SelectedIndex = cmb_pass_bp1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["BerthPreference"].ToString());
                                cmb_Meal_p1.SelectedIndex = cmb_Meal_p1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[1])]["Meal"].ToString());
                                txt_pass_name2.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["PassengerName"].ToString();
                                txt_pass_age2.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["Age"].ToString();
                                cmb_pass_gender2.SelectedIndex = cmb_pass_gender2.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["Gender"].ToString());
                                cmb_pass_bp2.SelectedIndex = cmb_pass_bp2.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["BerthPreference"].ToString());
                                cmb_Meal_p2.SelectedIndex = cmb_Meal_p2.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[2])]["Meal"].ToString());
                                txt_pass_name3.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[3])]["PassengerName"].ToString();
                                txt_pass_age3.Text = dtPassengerdetails.Rows[Convert.ToInt32(Adults[3])]["Age"].ToString();
                                cmb_pass_gender3.SelectedIndex = cmb_pass_gender3.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[3])]["Gender"].ToString());
                                cmb_pass_bp3.SelectedIndex = cmb_pass_bp3.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[3])]["BerthPreference"].ToString());
                                cmb_Meal_p3.SelectedIndex = cmb_Meal_p3.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Adults[3])]["Meal"].ToString());
                                break;
                        }
                        

                        switch (Infants.Count)
                        {
                            case 1: txt_pass_name_child0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Infants[0])]["PassengerName"].ToString();
                                cmb_pass_Child_age0.SelectedIndex = cmb_pass_Child_age0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Infants[0])]["Age"].ToString());
                                cmb_pass_Gender_child0.SelectedIndex = cmb_pass_Gender_child0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Infants[0])]["Gender"].ToString());
                                break;
                            case 2: txt_pass_name_child0.Text = dtPassengerdetails.Rows[Convert.ToInt32(Infants[0])]["PassengerName"].ToString();
                                cmb_pass_Child_age0.SelectedIndex = cmb_pass_Child_age0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Infants[0])]["Age"].ToString());
                                cmb_pass_Gender_child0.SelectedIndex = cmb_pass_Gender_child0.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Infants[0])]["Gender"].ToString());
                                txt_pass_name_child1.Text = dtPassengerdetails.Rows[Convert.ToInt32(Infants[1])]["PassengerName"].ToString();
                                cmb_pass_Child_age1.SelectedIndex = cmb_pass_Child_age1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Infants[1])]["Age"].ToString());
                                cmb_pass_Gender_child1.SelectedIndex = cmb_pass_Gender_child1.FindStringExact(dtPassengerdetails.Rows[Convert.ToInt32(Infants[1])]["Gender"].ToString());
                                break;
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearPassengerDetails()
        {
            try
            {
                //clear passenger details
                txt_pass_name0.Clear();
                txt_pass_age0.Clear();
                cmb_pass_gender0.SelectedIndex = -1;
                cmb_pass_bp0.SelectedIndex = -1;
                cmb_Meal_p0.SelectedIndex = -1;

                txt_pass_name1.Clear();
                txt_pass_age1.Clear();
                cmb_pass_gender1.SelectedIndex = -1;
                cmb_pass_bp1.SelectedIndex = -1;
                cmb_Meal_p1.SelectedIndex = -1;

                txt_pass_name2.Clear();
                txt_pass_age2.Clear();
                cmb_pass_gender2.SelectedIndex = -1;
                cmb_pass_bp2.SelectedIndex = -1;
                cmb_Meal_p2.SelectedIndex = -1;

                txt_pass_name3.Clear();
                txt_pass_age3.Clear();
                cmb_pass_gender3.SelectedIndex = -1;
                cmb_pass_bp3.SelectedIndex = -1;
                cmb_Meal_p3.SelectedIndex = -1;

                //clear child details
                txt_pass_name_child0.Clear();
                cmb_pass_Child_age0.SelectedIndex = -1;
                cmb_pass_Gender_child0.SelectedIndex = -1;

                txt_pass_name_child1.Clear();
                cmb_pass_Child_age1.SelectedIndex = -1;
                cmb_pass_Gender_child1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtExpiryYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCVV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNameonCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Character Validation(only Character allowed)
            if (Char.IsControl(e.KeyChar) != true && Char.IsNumber(e.KeyChar) == true)
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void Insert()
        {
            DataTable dt1 = new DataTable();
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= C:\\Stations.xlsx ;Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                OleDbDataAdapter da = new OleDbDataAdapter(cmd.CommandText, objConn);
                da.Fill(dt1);
                da.Dispose();
                objConn.Close();
               
            }
            //SqlConnection con = new SqlConnection(@"Data Source=C:\Users\pavan\documents\visual studio 2010\Projects\IRCTC\IRCTCDb.sdf");
            con.Open();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                //SqlCommand cmd1 = new SqlCommand("INSERT INTO Stations(StationName_Code) VALUES ('"+dt1.Rows[i][0].ToString()+"')",con);
                //cmd1.ExecuteNonQuery();
            }
            con.Close();
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToLongTimeString();
            //TimeSpan start = new TimeSpan(9, 59, 0); //10 o'clock
            //TimeSpan end = new TimeSpan(11, 30, 0); //12 o'clock
            //TimeSpan now = DateTime.Now.TimeOfDay;
            //if (cmbQuota.SelectedIndex != -1)
            //{
            //    if (cmbQuota.SelectedItem.ToString() == "Tatkal" || cmbQuota.SelectedItem.ToString() == "Premium Tatkal")
            //    {
            //        if ((now > start) && (now < end))
            //        {
            //            btnBookNow.Enabled = true;
            //        }
            //        else
            //        {
            //            btnBookNow.Enabled = false;
            //        }
            //    }
            //    else
            //    {
            //        btnBookNow.Enabled = true;
            //    }
            //}
        }

       
    }
}
