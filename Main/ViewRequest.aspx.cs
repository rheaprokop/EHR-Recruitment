using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using System.Text;
namespace EHR.Main
{
    public partial class ViewRequest : System.Web.UI.Page
    {
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel();
        GlobalApprovalModel _appv = new GlobalApprovalModel();
        GlobalAccountModel _accnt = new GlobalAccountModel();
        MiscModel _misc = new MiscModel();
        QueryOleDB MyQueries = new QueryOleDB();
        ObjectsMisc MyMisc = new ObjectsMisc();
        EmailBody MyEmail = new EmailBody();
        

        //Populate Page (GetRequests)
        //Hide/Unhide Actions (IsAgencyRequest)
        //Populate Approver Grid (PopulateGridApprover / GridApprover_RowDataBound)
        //Do SignOff Action (BtnSignOff_Click)

        protected void Page_Load(object sender, EventArgs e)
        {
            string RequestID = Request.QueryString["id"];

            GetRequests(RequestID);

            if (Page.IsPostBack == false)
            {
                PopulateGridApprover(RequestID);

                //if logged user is not an approver
                if (_appv.IsApprover(RequestID, Master.UserID()) == false)
                {
                     
                    pnlApprovalActions.Visible = false;
                }
                else
                {
                    //hide approval actions if not current approver
                    if (_appv.IsCurrentLoggedApprover(RequestID, Master.UserID()) == false)
                    {
                        pnlApprovalActions.Visible = false; 
                    }
                    else
                    {
                        pnlApprovalActions.Visible = true; 
                    }
                }

            }
        }

        //populate page with single record
        private void GetRequests(string RequestID)
        {
            string OracleQry = "Select REQUESTID, REQUESTDATETIME, REQUESTOR, REQUESTORDEPTID, ISAGENCY, " +
                               "SITE, PLANT, DEPTCODE, REQUIREDPERSON, JOBTITLE, TO_CHAR(ONBOARDDATE,'dd.MM.YYYY') AS ONBOARDDATE, " +
                               "LENGTHDATE_ISA, TYPE_NONA, FROMTIME, TOTIME, IFINCREASE_NONA, IfReplacement_NonA, " +
                               "ISREPLAMENTTO_NONA, EmployeeCat, CONTRACTTYPE_NONA,  " +
                               "MAXIMUMSALARY, NOTEMANPOWER, WORKINGPLACE, WORKINGTIME, ISMON, " +
                               "ISTUE, ISWED, ISTHU, ISFRI, ISSAT, ISSUN, WEEKENDS, NOTEWORKTIME, LANGUAGE1," +
                               "Level1, Language2, Level2, EDUCATION_NONA, MAJOR_NONA, REQUIREDWORKEXP, " +
                               "NOTEOTHER, NOTEWORKEXP_NONA " +
                               "From EREC_REQUESTS where RequestID = '" + RequestID + "'";

            OleDbDataReader myReader = _db.GetDataReader(OracleQry);
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
                hiddenIsAgencyVal.Value = IsAgency;
                IsAgencyRequest(IsAgency);
                 
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
                txtMaximumSalary.Text = Convert.ToString(myReader["MAXIMUMSALARY"]);
                txtNoteManpower.Text = Convert.ToString(myReader["NOTEMANPOWER"]);
                string WorkPlace = Convert.ToString(myReader["WORKINGPLACE"]);

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

            _db.conn.Close();

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

        private void PopulateGridApprover(string reqid)
        {
            string sql = "SELECT REQID, APPROVERDESC, APPEMPLID, ACTUALAPPROVER, RESULT, DATESIGNED, REMARKS " +
                        "FROM APPROVALTRANSACTION WHERE REQID = '" + reqid + "' AND ISNOTIFYONLY IS NULL ORDER BY APPSEQ ASC";
            DataTable _dt = _db.GetTable(sql);
            gridApprover.DataSource = _dt;
            gridApprover.DataBind();

        }

        protected void GridApprover_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string appemplid = e.Row.Cells[1].Text;

                string[] approvers = appemplid.Split(',');

                StringBuilder sb = new StringBuilder();
                foreach (string appv in approvers)
                {
                    sb.Append(_ps.GetName(appv.Trim()) + "<br />");
                }

                e.Row.Cells[1].Text = sb.ToString();

                string status = e.Row.Cells[3].Text;
                switch (status)
                {
                    case "R":
                        e.Row.Cells[3].Text = "Rejected";
                        break;
                    case "W":
                        e.Row.Cells[3].Text = "Waiting";
                        break;
                    case "O":
                        e.Row.Cells[3].Text = "";
                        break;
                    case "A":
                        e.Row.Cells[3].Text = "Approved";
                        break;
                }
            }
        }
    }
}
