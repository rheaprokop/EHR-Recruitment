using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model; 

namespace EHR.Views.Recruitment
{
    public partial class ApprovalPage : System.Web.UI.Page
    {
        ObjectsOleDB MyObjects = new ObjectsOleDB();
        QueryOleDB MyQueries = new QueryOleDB();
        ObjectsMisc MyMisc = new ObjectsMisc();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string reqID = Request.QueryString["id"];
                GetRequests(reqID);
            }
        }


        private void GetRequests(string RequestID)
        {
            string OracleQry = "Select REQUESTID, REQUESTDATETIME, REQUESTOR, REQUESTORDEPTID, ISAGENCY, " +
                               "SITE, PLANT, DEPTCODE, REQUIREDPERSON, JOBTITLE, TO_CHAR(ONBOARDDATE,'dd.MM.YYYY') AS ONBOARDDATE, " +
                               "LENGTHDATE_ISA, TYPE_NONA, FROMTIME, TOTIME, IFINCREASE_NONA, IfReplacement_NonA, " +
                               "ISREPLAMENTTO_NONA, EmployeeCat, CONTRACTTYPE_NONA, JOBFAMILY, JOBDESCRIPTION_NONA, " +
                               "JOBBUSITITLE, MAXIMUMSALARY, NOTEMANPOWER, WORKINGPLACE, WORKINGTIME, ISMON, " +
                               "ISTUE, ISWED, ISTHU, ISFRI, ISSAT, ISSUN, WEEKENDS, NOTEWORKTIME, LANGUAGE1," +
                               "Level1, Language2, Level2, EDUCATION_NONA, MAJOR_NONA, REQUIREDWORKEXP, " +
                               "NOTEOTHER, NOTEWORKEXP_NONA " +
                               "From EREC_REQUESTS where RequestID = :id";

            MyObjects.MyCommand.Parameters.AddWithValue("':id'", RequestID);

            OleDbDataReader myReader = MyObjects.MyReader(OracleQry);
            if (myReader.Read())
            {
                //GET THE REQUESTOR INFO 
                txtRequestID.Text = Convert.ToString(myReader["REQUESTID"]);
                string reqDate = Convert.ToString(myReader["REQUESTDATETIME"]);

                txtRequestedDate.Text = String.Format("{0:MM/dd/yy}", reqDate);

                txtRequestorEmplID.Text = Convert.ToString(myReader["Requestor"]);
                string EmplID = txtRequestorEmplID.Text;
                txtRequestor.Text = MyQueries.GetFullName(EmplID);
                txtRequestorDeptID.Text = Convert.ToString(myReader["RequestorDeptID"]);
                string IsAgency = Convert.ToString(myReader["IsAgency"]);
                IsAgencyRequest(IsAgency);
                txtSite.Text = Convert.ToString(myReader["Site"]);
                txtPlant.Text = Convert.ToString(myReader["Plant"]);
                if (txtPlant.Text == "SU")
                {
                    txtPlant.Text = "Site Support";
                }
                txtDeptCode.Text = Convert.ToString(myReader["DeptCode"]);
                txtNoOfPerson.Text = Convert.ToString(myReader["RequiredPerson"]) + " person(s) required.";
                string BusinessTitle = Convert.ToString(myReader["JobTitle"]);
                //string BusinessTitleJobCode;
                //txtJobTitle.Text = MyQueries.GetJobBusiTitleFromCode(BusinessTitle, out BusinessTitleJobCode);
                txtOnBoardDate.Text = Convert.ToString(myReader["ONBOARDDATE"]);
                txtLengthDate.Text = Convert.ToString(myReader["LengthDate_IsA"]);
                txtRequestType.Text = Convert.ToString(myReader["Type_NonA"]);
                txtFromTime.Text = Convert.ToString(myReader["FromTime"]);
                txtToTime.Text = Convert.ToString(myReader["ToTime"]);
                string Increase = Convert.ToString(myReader["IfIncrease_NonA"]);
                if (Increase != "")
                {
                    txtIncrease.Text = Increase;
                    pnlReplacement.Visible = false;
                }
                else
                {
                    pnlReplacement.Visible = true;
                    txtIncrease.Visible = false;
                    lblIncrease.Visible = false;

                }

                string Replacement = Convert.ToString(myReader["IfReplacement_NonA"]);

                if (Replacement != "")
                {
                    pnlReplacement.Visible = true;
                    txtReplacement.Text = Replacement;
                    txtReplacementTo.Text = Convert.ToString(myReader["ISREPLAMENTTO_NONA"]);
                }
                else
                {
                    pnlReplacement.Visible = false;
                    txtReplacement.Visible = false;
                    txtReplacement.Visible = false;
                    lblReplacement.Visible = false;
                    txtReplacementTo.Visible = false;
                    lblReplacementTo.Visible = false;
                }

                txtEmployeeCategory.Text = Convert.ToString(myReader["EmployeeCat"]);
                txtContractType.Text = Convert.ToString(myReader["CONTRACTTYPE_NONA"]);
                string JobFamily = Convert.ToString(myReader["JOBFAMILY"]);  
                txtMaximumSalary.Text = Convert.ToString(myReader["MAXIMUMSALARY"]);
                txtNoteManpower.Text = Convert.ToString(myReader["NOTEMANPOWER"]);
                string WorkPlace = Convert.ToString(myReader["WORKINGPLACE"]);
                GetWorkPlaceCode(WorkPlace);
                txtWorkTime.Text = Convert.ToString(myReader["WORKINGTIME"]);

                string IsMon = Convert.ToString(myReader["ISMON"]);
                if (IsMon != "0")
                {
                    txtMonday.Text = "Monday; ";
                }
                else
                {
                    txtMonday.Visible = false;
                }
                string IsTue = Convert.ToString(myReader["ISTUE"]);
                if (IsTue != "0")
                {
                    txtTuesday.Text = "Tuesday; ";
                }
                else
                {
                    txtTuesday.Visible = false;
                }

                string IsWed = Convert.ToString(myReader["ISWED"]);
                if (IsWed != "0")
                {
                    txtWednesday.Text = "Wednesday; ";
                }
                else
                {
                    txtWednesday.Visible = false;
                }

                string IsThu = Convert.ToString(myReader["ISTHU"]);

                if (IsThu != "0")
                {
                    txtThursday.Text = "Thursday; ";
                }
                else
                {
                    txtThursday.Visible = false;
                }
                string IsFri = Convert.ToString(myReader["ISFRI"]);
                if (IsFri != "0")
                {
                    txtFriday.Text = "Friday; ";
                }
                else
                {
                    txtFriday.Visible = false;
                }
                string IsSat = Convert.ToString(myReader["ISSAT"]);
                if (IsSat != "0")
                {
                    txtSaturday.Text = "Saturday; ";
                }
                else
                {
                    txtSaturday.Visible = false;
                }
                string IsSun = Convert.ToString(myReader["ISSUN"]);

                if (IsSun != "0")
                {
                    txtSunday.Text = "Sunday; ";
                }
                else
                {
                    txtSunday.Visible = false;
                }

                string IsWeekend = Convert.ToString(myReader["WEEKENDS"]);
                if (IsWeekend == "0")
                {
                    txtWeekend.Text = "No";
                }
                else if (IsWeekend == "1")
                {
                    txtWeekend.Text = "Yes";
                }

                txtNoteWorkTime.Text = Convert.ToString(myReader["NOTEWORKTIME"]);
                txtLanguage1.Text = Convert.ToString(myReader["LANGUAGE1"]);
                txtLevel1.Text = Convert.ToString(myReader["Level1"]);
                txtLanguage2.Text = Convert.ToString(myReader["Language2"]);
                txtLevel2.Text = Convert.ToString(myReader["Level2"]);
                txtEducation.Text = Convert.ToString(myReader["EDUCATION_NONA"]);
                txtMajorIn.Text = Convert.ToString(myReader["MAJOR_NONA"]);
                txtRequiredWork.Text = Convert.ToString(myReader["REQUIREDWORKEXP"]);
                if (txtRequiredWork.Text != "")
                {
                    txtRequiredWork.Text = txtRequiredWork.Text + " years experience";
                }
                txtRemarks.Text = Convert.ToString(myReader["NOTEOTHER"]);
                txtNoteExperience.Text = Convert.ToString(myReader["NOTEWORKEXP_NONA"]);
            }

            MyObjects.MyConn.Close();

        }

        private void GetWorkPlaceCode(string l)
        {
            switch (l)
            {
                case "1":
                    txtWorkPlace.Text = "CTPark Brno Turanka 1328/102, 62700 Brno-Slatina";
                    break;
                case "2":
                    txtWorkPlace.Text = "CTPark Modrice Evropska 868,66442 Modrice Brno-Venkov";
                    break;
                case "3":
                    txtWorkPlace.Text = "Prumyslova 1506, 69123 Pohorelice, Brno-Venkov";
                    break;
                case "4":
                    txtWorkPlace.Text = "Prumyslova 1507, 69123 Pohorelice, Brno-Venkov";
                    break;
                case "5":
                    txtWorkPlace.Text = "K Letisti 1792/1 66451 Slapanice, Brno-Venkov";
                    break;
            }
        }

        private void IsAgencyRequest(string i)
        {
            if (i == "0")
            {
                txtIsAgency.Text = "No";
                AllNonAgencyCtrl(true);
                AllAgencyCtrol(false);
            }
            else if (i == "1")
            {
                txtIsAgency.Text = "Yes";
                AllAgencyCtrol(true);
                AllNonAgencyCtrl(false);
                //hide non-agency controls & show agency controls  

            }
        }

        private void AllNonAgencyCtrl(bool a)
        {
            lblIntendedBoardDate.Visible = a;
            txtOnBoardDate.Visible = a;
            pnlRequisitionType.Visible = a;
            pnlEducationInfo.Visible = a;
            lblNoteExperience.Visible = a;
            txtNoteExperience.Visible = a;
            lblIncrease.Visible = a;
            txtIncrease.Visible = a;
            txtReplacement.Visible = a;
            txtReplacementTo.Visible = a;
            lblReplacement.Visible = a;


        }

        private void AllAgencyCtrol(bool a)
        {
            lblLengthEmplDate.Visible = a;
            txtLengthDate.Visible = a;

            txtOtherRequirements.Visible = a;
            lblOtherRequirements.Visible = a;

        }


        protected void LoadApprovalStatus(string RequestID)
        {
            string OracleQry = "Select REQAPPROVALLOGS.REQUESTID, APPROVALFLOW.FLOW,  " +
                   "PS.PS_SUB_WCZ_AT_VW_A.NAME_A as SIGNEE,  REQAPPROVALLOGS.APPROVALSTATUS " +
                   "From REQAPPROVALLOGS INNER JOIN APPROVALFLOW On   " +
                   "REQAPPROVALLOGS.FLOWID = APPROVALFLOW.FLOWID  And  " +
                   "REQAPPROVALLOGS.REQUESTID = '" + RequestID + "' JOIN PS.PS_SUB_WCZ_AT_VW_A  " +
                   "ON PS.PS_SUB_WCZ_AT_VW_A.EMPLID = REQAPPROVALLOGS.PIC " +
                   " Order By REQAPPROVALLOGS.FLOWID ASC ";

            DataTable myDT = MyQueries.GetTable(OracleQry);
            gridShowStatus.DataSource = myDT;
            gridShowStatus.DataBind();
        }
    }
}
