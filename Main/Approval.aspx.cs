using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using System.Text;
using EHR.Recruitment;
using ehr_email;

namespace EHR.Views.Recruitment
{
    public partial class Approval1 : System.Web.UI.Page
    {
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel();
        GlobalApprovalModel _appv = new GlobalApprovalModel();
        GlobalAccountModel _accnt = new GlobalAccountModel();
        MiscModel _misc = new MiscModel(); 
        QueryOleDB MyQueries = new QueryOleDB();
        ObjectsMisc MyMisc = new ObjectsMisc();
        EmailBody MyEmail = new EmailBody();
        Request req = new Request(); 

        //Populate Page (GetRequests)
        //Hide/Unhide Actions (IsAgencyRequest)
        //Populate Approver Grid (PopulateGridApprover / GridApprover_RowDataBound)
        //Do SignOff Action (BtnSignOff_Click)

        protected void Page_Load(object sender, EventArgs e)
        {
            string RequestID = Request.QueryString["appv"];

            GetRequests(RequestID); 

            if (Page.IsPostBack == false)
            {   
                PopulateGridApprover(RequestID);

                //if logged user is not an approver
                if (_appv.IsApprover(RequestID, Master.UserID()) == false)
                {
                    pnlApproverStatusMsg.Visible = false;
                    pnlApprovalActions.Visible = false;
                }
                else
                {
                    //hide approval actions if not current approver
                    if (_appv.IsCurrentLoggedApprover(RequestID, Master.UserID()) == false)
                    {
                        pnlApprovalActions.Visible = false;
                        pnlApproverStatusMsg.Visible = true;
                    }
                    else
                    {
                        pnlApprovalActions.Visible = true;
                        pnlApproverStatusMsg.Visible = false;
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

        protected void BtnSignOff_Click(object sender, EventArgs e)
        {
            string approval;
            switch (radioSignOffButtons.SelectedValue)
            {
                case "A":
                    _appv.ApprovedAction(txtRequestID.Text, _accnt.ReadCookie("user"), txtApproversRemarks.Text);
                    //insert hr recruiter, ps trainer and ps encoder data to approval transaction after approval
                    //process is done. 

                    if (_appv.CountOpen(txtRequestID.Text) == 0 && _appv.CountWaiting(txtRequestID.Text) == 0)
                    {
                        InsertPICToApprovalTransaction(GetFlowID(hiddenIsAgencyVal.Value, txtDeptCode.Text), txtPlant.Text); 
                    }
                    approval = "Approved";
                    break;

                case "R":
                    approval = "Rejected";
                    _appv.RejectedAction(txtRequestID.Text, _accnt.ReadCookie("user"), txtApproversRemarks.Text); 
                    break;
                default:
                    approval = "";
                    break;
            }

            MyMisc.ShowMessage("You have " + approval + " successfully.", this.Page);
            Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/Approval.aspx?appv=" + txtRequestID.Text + "&a=681559B44B2CC43C82AA343C925331D3");
           // Response.Redirect("http://" + _misc.GetWebAppRoot() + "/Main/Approval.aspx?appv=" + txtRequestID.Text + "&a=681559B44B2CC43C82AA343C925331D3"); 
        }

        private void InsertPICToApprovalTransaction(string appflowid, string plant)
        {
            if (plant == "Site Support") plant = "SU";
            
            string sql = "SELECT APPROVERVALUE, APPROVERDESC, APPEMPLID FROM PS.PS_APPROVER WHERE CATEGORY = '" + plant + "'" +
                           "AND APPROVERVALUE IN ('HR_RECRUITER', 'HR_TRAINOR', 'PS_ENCODER') " +
                           "ORDER BY APPROVERID ASC";
 
                _db.GetConn();
                OleDbCommand cmdInsert = new OleDbCommand(sql, _db.conn);
                OleDbDataReader _rdInsert = cmdInsert.ExecuteReader();
                while (_rdInsert.Read())
                {
                    
                    string appvalue = Convert.ToString(_rdInsert["APPROVERVALUE"]);
                    string appdesc = Convert.ToString(_rdInsert["APPROVERDESC"]);
                    string appemplid = Convert.ToString(_rdInsert["APPEMPLID"]);
                    int appseq = GetLastAppSeq(txtRequestID.Text);
                    int newAppseq = appseq + 1; 
                    // insert all approvers to the approver logs
                    string sqlAppv = "Insert into APPROVALTRANSACTION " +
                           "(REQID, APPFLOWID, APPSEQ, APPROVERVALUE, APPROVERDESC, APPEMPLID, ISNOTIFYONLY) " +
                        //"(:reqid, :appflowid, :appseq, :appvalue, :appdesc, :appemplid)";
                           "VALUES ('" + txtRequestID.Text + "', '" + appflowid + "', '" + newAppseq + "', " +
                           " '" + appvalue + "', '" + appdesc + "', '" + appemplid + "', 'Y')";
                    OleDbCommand _dbCmd = new OleDbCommand(sqlAppv, _db.conn);
                    _dbCmd.ExecuteNonQuery(); 
                }  

                string sqlUpdateStatus = "UPDATE EREC_REQUESTS SET RESULT = 'Approved' WHERE REQUESTID = '" + txtRequestID.Text + "'";
                OleDbCommand cmdUpdateStatus = new OleDbCommand(sqlUpdateStatus, _db.conn);
                cmdUpdateStatus.ExecuteNonQuery(); 
                
            //notify HR_Recruiter that process has been approved

                NotifyPIC(txtRequestID.Text, "HR_RECRUITER", "Notify_HR_Recruiter", "HR Recruiter");
                

             
        }

        public string GetFlowID(string isAgency, string deptid)
        {
            string plant, flowid;
            string sql = "SELECT PLANT_ID_A FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = '" + txtRequestorDeptID.Text + "'";
            _db.GetConn();
            OleDbCommand cmdFlowID = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rdFlowID = cmdFlowID.ExecuteReader();

            if (_rdFlowID.Read())
            {
                plant = Convert.ToString(_rdFlowID["PLANT_ID_A"]);
                if (plant == "SU")
                {
                    flowid = "AF004";
                }
                else
                {
                    if (isAgency == "0")
                    {
                        flowid = "AF002";
                    }
                    else if (isAgency == "1")
                    {
                        flowid = "AF003";
                    }
                    else
                    {
                        flowid = "";
                    }
                }
            }
            else
            {
                flowid = "";
            }

            return flowid;

        }

        public int GetLastAppSeq(string requestid)
        {
            string sql = "select max(appseq) from approvaltransaction where reqid = '" + requestid + "'";

            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count;
        }

        public void NotifyPIC(string reqid, string approverval, string emailtype, string to)
        {
            string appemplid;
            string sql = "SELECT APPEMPLID FROM APPROVALTRANSACTION WHERE APPROVERVALUE = '" + approverval + "' " +
                    "AND REQID = '" + reqid + "'";
            _db.GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rdAppemplid = cmd.ExecuteReader();
            if (_rdAppemplid.Read())
            {
                appemplid = Convert.ToString(_rdAppemplid["APPEMPLID"]);
                string[] emailto = appemplid.Split(',');

                StringBuilder sb = new StringBuilder();

                foreach (string email in emailto)
                {
                    sb.Append(_ps.GetEmplEmailAdrress(email.Trim()) + ",");
                }
                string email_addresses = sb.ToString();

                if (_misc.IsEmailNotificationTest(emailtype, "You have request to approve: " + reqid, reqid) == false)
                {
                    string mail_type = emailtype;
                    string sender_ = "noreply@wistron.com ";

                    string recipient = email_addresses;
                    string recipient_name = to;
                    string cc = "";
                    string subject = "EHR: Recruitment - HR Recruiter Notification";
                    string parameters = "Request Link: " + reqid;

                    Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                }
            }
            else
            {
                appemplid = "";
            }
            
        }
    }
}
