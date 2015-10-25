using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using System.Data.SqlClient;

namespace EHR.Recruitment
{
    public partial class Request : System.Web.UI.Page
    {
        DBModel _db = new DBModel(); 
        QueryOleDB myQueries  = new QueryOleDB();
        ObjectsOleDB MyObjsDB = new ObjectsOleDB();
        ObjectsMisc myMisc    = new ObjectsMisc();
        GlobalApprovalModel myApproval = new GlobalApprovalModel();
        GlobalAccountModel _accnt = new GlobalAccountModel(); 

        string RequesterName;
        string DeptID;
        string FirstName;

        protected void Page_Load(object sender, EventArgs e)
        { 
            
            if (!IsPostBack)
            {
                PopulatePlants();
                
                Run(Master.UserID());
            }
           
        }

        

        private void Run(string EmplID)
        {
             
            GetPanel();
            if (Page.IsPostBack == false)
            {
                calOnBoardDate.Visible = false;
                pnlErrorBelow.Visible = false;
                pnlError.Visible = false;
                LoadData(EmplID);
                 
            }
            else
            {
               
            }

        }

        private void PopulatePlants()
        {
            string sql = "SELECT DISTINCT(PLANT_ID_A) PLANT_ID_A FROM PS.PS_SUB_WCZORG_VW_A";

            DataTable _dt = _db.GetTable(sql);

            ddlPlant.DataSource = _dt;
            ddlPlant.DataValueField = "PLANT_ID_A";
            ddlPlant.DataTextField = "PLANT_ID_A";
            ddlPlant.DataBind(); 

            if (!String.IsNullOrEmpty(GetUserPlant()))
            {
                ddlPlant.SelectedValue = GetUserPlant();
            }

            
             
        }

        private string GetUserPlant()
        {

            string sql = "SELECT PLANT_ID_A FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = '" + txtRequestorDeptID.Text + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                return Convert.ToString(rd["PLANT_ID_A"]);
            }
            else
            {
                return ""; 
            }
        }

        private void GetPanel()
        {
            string page = Request.QueryString["form"];
            switch (page)
            {
                case "create":
                    pnlCreate.Visible = true; 
                    pnlEditRequest.Visible = false;
                    break; 
                case "find": 
                    pnlCreate.Visible = false;
                    pnlEditRequest.Visible = false;
                    break;
                case "edit":
                    pnlEditRequest.Visible = true; 
                    pnlCreate.Visible = false; 
                    break;
                default:                   
                    pnlCreate.Visible = false; 
                    pnlEditRequest.Visible = false;
                    break;

            }
        }

        protected void ddlRequestType_SelectIndexChanged(object sender, EventArgs e)
        {
            string _filterValueRep = ddlRequestType.SelectedValue;
            PnlMainForm.Visible = true;
            switch (_filterValueRep)
            {
                case "Increase":

                    ddlIfIncrease.Visible = true;
                    lblIncrease.Visible = true;

                    ddlReplacement.Visible = false;
                    lblReplacement.Visible = false;
                    txtReplacementTo.Visible = false;
                    lblReplacementTo.Visible = false;

                    ddlReplacement.SelectedValue = "";
                    txtReplacementTo.Text = ""; 


                    break;
                case "Replacement":

                    ddlReplacement.Visible = true;
                    lblReplacement.Visible = true;
                    txtReplacementTo.Visible = true;
                    lblReplacementTo.Visible = true;
                    ddlIfIncrease.Visible = false;
                    lblIncrease.Visible = false;
                    ddlIfIncrease.SelectedValue = ""; 
                    break;
                default:

                    ddlIfIncrease.Visible = false;
                    lblIncrease.Visible = false; 
                    ddlReplacement.Visible = false;
                    lblReplacement.Visible = false;
                    txtReplacementTo.Visible = false;
                    lblReplacementTo.Visible = false;

                    ddlIfIncrease.SelectedValue = "";
                    ddlReplacement.SelectedValue = "";
                    txtReplacementTo.Text = ""; 
                    break;
            }
        }

        protected void ddlIsAgencySelected_SelectIndexChanged(object sender, EventArgs e)
        {
            string _filterVal = ddlIsAgencySelected.SelectedValue;
            /* "" = no option selected
              0  = non agency reqiest
              1  =  agency;*/

            if (_filterVal == "")
            {
                PnlMainForm.Visible = false;

            }
            else
            {
                PnlMainForm.Visible = true;
                switch (_filterVal)
                {
                    case "0":
                        string IsNotAgency = "IsNotAgency";
                        LoadAgencyControls(IsNotAgency);
                        break;
                    case "1":
                        string IsAgency = "IsAgency";
                        LoadAgencyControls(IsAgency);
                        
                        break;
                    default:
                        LoadAgencyControls("");
                        break;
                }

            }
        }

        private void LoadAgencyControls(string IsAgencyRequest)
        {
            switch (IsAgencyRequest)
            {
                case "IsNotAgency":
                    AllNonAgencyCtrl(true);
                    AllAgencyCtrol(false);
                    break;
                case "IsAgency":
                    AllAgencyCtrol(true);
                    AllNonAgencyCtrl(false);
                    break;
                default:
                    AllNonAgencyCtrl(false);
                    break;

            }
        }

        private void AllNonAgencyCtrl(bool a)
        {
            lblIntendedBoardDate.Visible = a;
            lblIntendedBoardDateAskterisk.Visible = a;
            txtOnBoardDate.Visible = a;
            pnlRequisitionType.Visible = a;
            pnlEducationInfo.Visible = a;
            lblNoteExperience.Visible = a;
            txtNoteExperience.Visible = a;  
            reqtxtOnBoardDate.Enabled = a;
            btnCalOnBoardDate.Visible = a;
        }


        private void AllAgencyCtrol(bool a)
        {
            lblLengthEmplDate.Visible = a;
            lblLengthEmplDateAskterisk.Visible = a;
            txtLengthDate.Visible = a;
            reqLengthDate.Visible = a;
            txtOtherRequirements.Visible = a;
            lblOtherRequirements.Visible = a;
            
             
        }

        private void HideControls()
        {
                PnlMainForm.Visible = false;  
                ddlIfIncrease.Visible = false;
                lblIncrease.Visible = false; 
                ddlReplacement.Visible = false;
                lblReplacement.Visible = false;
                lblReplacementTo.Visible = false;
                txtReplacementTo.Visible = false;
                lblReplacement.Visible = false;
                lblReplacementTo.Visible = false;
                txtReplacementTo.Visible = false; 
                pnlViewingRequest.Visible = false;
                 
             
        }

        private void HideEmplTypeOnly()
        {
            ddlIfIncrease.Visible = false;
            lblIncrease.Visible = false;

            ddlReplacement.Visible = false;
            lblReplacement.Visible = false;
            lblReplacementTo.Visible = false;
            txtReplacementTo.Visible = false;
            lblReplacement.Visible = false;
            lblReplacementTo.Visible = false;
            txtReplacementTo.Visible = false;
        }

        // DATA LOADING
        private void LoadData(string EmplID)
        {
            if (Page.IsPostBack == true)
            {
                
                
            }
            else
            {
                LoadEmployeeData(EmplID);
                HideControls(); 
                 
               
            }
        }

        private void LoadEmployeeData(string EmplID)
        {
            try
            {
                myQueries.GetEmployeeName(EmplID, out RequesterName, out DeptID, out FirstName);

                string deptprefix = DeptID.Substring(0, 4);
                string OracleQry = "SELECT DEPTID FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID LIKE '%" + deptprefix + "%'";
                DataTable myDT = myQueries.GetTable(OracleQry);
                ddlDepartment.DataSource = myDT;
                ddlDepartment.DataTextField = "DeptID";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
                ddlDepartment.SelectedValue = txtRequestorDeptID.Text;
                //ddlDepartment.SelectedValue = DeptID;
                ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", ""));
                txtRequestorName.Text = RequesterName;
                txtRequestorDeptID.Text = DeptID;

                //Load Departments

                MyObjsDB.MyConn.Close();
            }
            catch (Exception ex)
            {
            }
        }
          
       
        protected void ddlEducation_SelectedIndexChanged(object sender, EventArgs e)
        {
            PnlMainForm.Visible = true;
            string _filterValue = ddlEducation.SelectedValue;

            if (_filterValue == "University")
            {
                txtMajorIn.Enabled = true;
            }
            else
            {
                txtMajorIn.Enabled = false;
            }
        }

        private void IfCheckDays()
        {

            chkDays.Items[0].Value = "0";
            chkDays.Items[1].Value = "0";
            chkDays.Items[2].Value = "0";
            chkDays.Items[3].Value = "0";
            chkDays.Items[4].Value = "0";
            chkDays.Items[5].Value = "0";
            chkDays.Items[6].Value = "0";

            if (chkDays.Items[0].Selected)
            { 
                chkDays.Items[0].Value = "1";
               
            }
            if (chkDays.Items[1].Selected)
            {
                chkDays.Items[1].Value = "1";
            }
            if (chkDays.Items[2].Selected)
            {
                chkDays.Items[2].Value = "1";
            }
            if (chkDays.Items[3].Selected)
            {
                chkDays.Items[3].Value = "1";
            }
            if (chkDays.Items[4].Selected)
            {
                chkDays.Items[4].Value = "1";
            }
            if (chkDays.Items[5].Selected)
            {
                chkDays.Items[5].Value = "1";
            }
            if (chkDays.Items[6].Selected)
            {
                chkDays.Items[6].Value = "1";
            }
 
        }

        private string GetPlantID(string deptid)
        {
            _db.GetConn();
            string plantid;
            string sql = "SELECT PLANT_ID_A FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = '" + deptid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                plantid = Convert.ToString(rd["PLANT_ID_A"]);
            }
            else
            {
                plantid = "";
            }

            return plantid;
        }

        protected void BtnRecruitment_Save(object sender, EventArgs e)
        {
            try
            {
                IsSave();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        protected void DdlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(ddlPlant.SelectedValue))
                {
                    ddlPlant.SelectedValue = GetPlantID(ddlDepartment.SelectedValue);
                    GetJobTitle(ddlDepartment.SelectedValue);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetJobTitle(string deptid)
        {
            string sql;
            if (ddlPlant.SelectedValue == "CS")
            {
                 sql = "SELECT DISTINCT(JOB_DESCR) AS JOB_BUSI_TITLE  " + 
                    "FROM PS_SUB_WCZ_AT_VW_A WHERE DEPTID = '" + deptid + "'";
            }
            else
            {
                string dept = deptid.Substring(0, 4);
                sql = "select JOB_BUSI_TITLE from ps.ps_job " +
                    "WHERE TV_PLANT LIKE '%" + dept + "%' OR SV_PLANT LIKE '%" + dept + "%' " +
                    "OR SITE_FUNCTION LIKE '%" + dept + "%'"; 
            }

            DataTable _dt = _db.GetTable(sql);
            ddlJobTitle.DataSource = _dt;
            ddlJobTitle.DataTextField = "JOB_BUSI_TITLE";
            ddlJobTitle.DataValueField = "JOB_BUSI_TITLE";
            ddlJobTitle.DataBind(); 
            
        }
        
        //SUBMIT BUTTONS  

        private void IsSave()
        {
            string UserID = Master.UserID();
            string RequestID = myMisc.CreateRequestID();
            DateTime RequestDate = DateTime.Now;
            IfCheckDays();
            string OracleQry = "Insert Into EREC_REQUESTS (REQUESTDATETIME, REQUESTID, REQUESTOR, REQUESTORDEPTID, ISAGENCY, PLANT, DEPTCODE, " +
                               "JOBTITLE, REQUIREDPERSON, ONBOARDDATE, LENGTHDATE_ISA, TYPE_NONA, IFINCREASE_NONA, IFREPLACEMENT_NONA, " +
                               "ISREPLAMENTTO_NONA, EMPLOYEECAT, CONTRACTTYPE_NONA, MAXIMUMSALARY, " +
                               "WORKINGPLACE, WORKINGTIME, WEEKENDS, NOTEWORKTIME, NOTEMANPOWER, LANGUAGE1, LEVEL1, LANGUAGE2, LEVEL2, " +
                               "EDUCATION_NONA, MAJOR_NONA, REQUIREDWORKEXP, NOTEOTHER, NOTEREQUIREMENT_ISA, " +
                               "ISMON, ISTUE, ISWED, ISTHU, ISFRI, ISSAT, ISSUN, NOTEWORKEXP_NONA, FROMTIME, TOTIME, RESULT)" +
                               "Values ('" + RequestDate + "', '" + RequestID + "', '" + UserID + "', '" + txtRequestorDeptID.Text + "', " +
                               "'" + ddlIsAgencySelected.SelectedValue.ToString() + "', '" + ddlPlant.SelectedValue.ToString() + "', " +
                               "'" + ddlDepartment.SelectedValue.ToString() + "', " +
                               "'" + ddlJobTitle.SelectedValue.ToString() + "', '" + txtNoOfPerson.Text + "', to_date('" + txtOnBoardDate.Text + "','MM/dd/yyyy'), " +
                               "'" + txtLengthDate.Text + "', '" + ddlRequestType.SelectedValue.ToString() + "', '" + ddlIfIncrease.SelectedValue.ToString() + "', " +
                               "'" + ddlReplacement.SelectedValue.ToString() + "', '" + txtReplacementTo.Text + "', '" + ddlEmployeeCategory.SelectedValue.ToString() + "', " +  
                               "'" + ddlContractType.SelectedValue.ToString() + "', '" + txtMaximumSalary.Text + "', " +                               
                               "'" + ddlWorkPlace.SelectedValue.ToString() + "', '" + ddlWorkTime.SelectedValue.ToString() + "', " + 
                               "'" + ddlWeekends.SelectedValue.ToString() + "', '" + txtNoteWorkTime.Text + "', '" + txtNoteManpower.Text + "', " +
                               " '" + ddlLanguage1.SelectedValue.ToString() + "', '" + ddlLanguageLvl1.SelectedValue.ToString() + "', " + 
                               "'" + ddlLanguage2.SelectedValue.ToString() + "', '" + ddlLanguageLvl2.SelectedValue.ToString() + "', " +
                               "'" + ddlEducation.SelectedValue.ToString() + "', '" + txtMajorIn.Text + "', '" + txtRequiredWorkExp.Text + "', " +
                               "'" + txtRemarks.Text + "', '" + txtOtherRequirements.Text + "', " +
                               "'" + chkDays.Items[0].Value + "', '" + chkDays.Items[1].Value + "', '" + chkDays.Items[2].Value + "', " +
                               "'" + chkDays.Items[3].Value + "', '" + chkDays.Items[4].Value + "', '" + chkDays.Items[5].Value + "', " +
                               "'" + chkDays.Items[6].Value + "', '" + txtNoteExperience.Text + "', '" + ddlFromTime.SelectedValue.ToString() + "', " +
                               "'" + ddlToTime.SelectedValue.ToString() + "', 'Approval Process')";
             
    
            
            
            _db.GetConn();
            OleDbCommand MyCommand = new OleDbCommand(OracleQry, _db.conn);
            //MyCommand.Parameters.AddWithValue("':RequestDate'", RequestDate);
            //MyCommand.Parameters.AddWithValue("':RequestID'", RequestID);
            //MyCommand.Parameters.AddWithValue("':Requestor'", UserID);
            //MyCommand.Parameters.AddWithValue("':RequestorDeptID'", txtRequestorDeptID.Text);
            //MyCommand.Parameters.AddWithValue("':IsAgency'", ddlIsAgencySelected.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':Plant'", ddlPlant.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':DeptCode'", ddlDepartment.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':JobTitle'", ddlJobTitle.SelectedValue.ToString()); 
            //MyCommand.Parameters.AddWithValue("':RequiredPerson'", txtNoOfPerson.Text);
            //MyCommand.Parameters.AddWithValue("':OnBoardDate'", txtOnBoardDate.Text);
            //MyCommand.Parameters.AddWithValue("':LengthDateIsA'", txtLengthDate.Text);
            //MyCommand.Parameters.AddWithValue("':TypeNonA'", ddlRequestType.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':IfIncrease_NonA'", ddlIfIncrease.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':IfReplacement_NonA'", ddlReplacement.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':IsReplacementTo_NonA'", txtReplacementTo.Text); 
            //MyCommand.Parameters.AddWithValue("':EmployeeCat'", ddlEmployeeCategory.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':ContractType'", ddlContractType.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':MaximumSalary'", txtMaximumSalary.Text);
            //MyCommand.Parameters.AddWithValue("':WorkingPlace'", ddlWorkPlace.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':WorkTime'",ddlWorkTime.SelectedValue.ToString());;
            //MyCommand.Parameters.AddWithValue("':Weekends'", ddlWeekends.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':NoteWorkTime'", txtNoteWorkTime.Text);
            //MyCommand.Parameters.AddWithValue("':NoteManpower'", txtNoteManpower.Text);
            //MyCommand.Parameters.AddWithValue("':Language1'", ddlLanguage1.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':Level1'", ddlLanguageLvl1.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':Language2'", ddlLanguage2.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':Level2'", ddlLanguageLvl2.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':Education'", ddlEducation.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':MajorIn'", txtMajorIn.Text);
            //MyCommand.Parameters.AddWithValue("':RequiredWorkExp'", txtRequiredWorkExp.Text); 
            //MyCommand.Parameters.AddWithValue("':OtherRemarks'", txtRemarks.Text);
            //MyCommand.Parameters.AddWithValue("':OtherRequirements'", txtOtherRequirements.Text);
            //IfCheckDays();
            //MyCommand.Parameters.AddWithValue("':IsMon'", chkDays.Items[0].Value);
            //MyCommand.Parameters.AddWithValue("':IsTue'", chkDays.Items[1].Value);
            //MyCommand.Parameters.AddWithValue("':IsWed'", chkDays.Items[2].Value);
            //MyCommand.Parameters.AddWithValue("':IsThu'", chkDays.Items[3].Value);
            //MyCommand.Parameters.AddWithValue("':IsFri'", chkDays.Items[4].Value);
            //MyCommand.Parameters.AddWithValue("':IsSat'", chkDays.Items[5].Value);
            //MyCommand.Parameters.AddWithValue("':IsSun'", chkDays.Items[6].Value);
            //MyCommand.Parameters.AddWithValue("':NoteWorkExp'", txtNoteExperience.Text);
            //MyCommand.Parameters.AddWithValue("':FromTime'", ddlFromTime.SelectedValue.ToString());
            //MyCommand.Parameters.AddWithValue("':ToTime'", ddlToTime.SelectedValue.ToString());



            Validate();
            if (Page.IsValid)
            {
                try
                {
                    MyCommand.ExecuteNonQuery();

                    
                    //insert to activity table
                    string txtActivity = "Requested A New Employee. RequestID: " + RequestID;
                    myQueries.InsertActivityDesc(txtActivity, UserID);

                    //insert to approvaltransactiontable
                    string isAgency = ddlIsAgencySelected.SelectedValue.ToString();
                    myApproval.GetApproval(RequestID, UserID, GetFlowID(isAgency, RequestID), txtRequestorDeptID.Text);

                    Response.Redirect("MyRequests.aspx?result=0");
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    
                }
                
            }
            else
            {
                myMisc.ShowMessage("Error Creating Form", this.Page);
                lblValidation.Text = "There is an error in you form. Please fill up the necessary fields.";
                pnlError.Visible = true;
                pnlErrorBelow.Visible = true;
                lblError.Text = "There is an error in you form. Please fill up the necessary fields.";
            }
             
        }

        protected void IsSaveAnother(object sender, EventArgs e)
        {
            try
            {
                IsSave();
                Response.Redirect("~/Main/Request.aspx?form=create");
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public string GetFlowID(string isAgency, string deptid)
        {
            string plant, flowid; 
            string sql = "SELECT PLANT_ID_A FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = '" + txtRequestorDeptID.Text + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();

            if (_rd.Read())
            {
                plant = Convert.ToString(_rd["PLANT_ID_A"]);
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
         

        //CALENDAR MANI..
        protected void ViewOnBoardCal_OnClick(object sender, ImageClickEventArgs e)
        {
            

            if (calOnBoardDate.Visible == false)
            {
                calOnBoardDate.Visible = true;
            }
            else
            {
                calOnBoardDate.Visible = false;

            }
            
        }

        protected void CalOnBoardDate_Selection_Change(object sender, EventArgs e)
        {
            foreach (DateTime day in calOnBoardDate.SelectedDates)
            {
                txtOnBoardDate.Text = "";
                txtOnBoardDate.Text += day.Date.ToString("MM/dd/yyyy");
                calOnBoardDate.Visible = false;
            }
        }

      

        private void GetAccessRole(string EmplID)
        {

            switch (myQueries.RoleAccess(EmplID))
            {
                case "1": // Dev 
                    btnViewAllRequest.Visible = true;
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = true;
                    break;
                case "2": // IT Support 
                    btnViewAllRequest.Visible = true;
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = true;
                    break;
                case "3": // HR Reviewer 
                    btnViewAllRequest.Visible = true;
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = false;
                    break;
                case "4": // HR Trainer 
                    btnViewAllRequest.Visible = false;
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                case "5": // HR Recruiter 
                    btnViewAllRequest.Visible = true;
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = true;
                    break;
                case "6": // PS ENCODER 
                    btnViewAllRequest.Visible = false;
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                case "7": // Requestor 
                    btnViewAllRequest.Visible = false;
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                case "8": // Approver 
                    btnViewAllRequest.Visible = false;
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                default:
                    btnViewAllRequest.Visible = false;
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;

            }
        }
          
    }


   
   
}
