using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model; 


namespace EHR.Recruitment
{
    public partial class Approval : System.Web.UI.Page
    {

        QueryOleDB myQueries = new QueryOleDB();
        ObjectsOleDB myObjs = new ObjectsOleDB();
        ObjectsMisc myMics = new ObjectsMisc();

        protected void Page_Load(object sender, EventArgs e)
        {
            HidePanel();
            Run();
        }

        private void Run()
        {
            string a = Request.QueryString["action"];  
            GetAction(a);
             
            if (Page.IsPostBack == false)
            { 
                LoadDepartment();
                LoadHRData(ddlHRReviewer);
                LoadHRData(ddlHRManager);
            }

        }

        private string IsAgencyFromRequest(string requestID)
        {
            myObjs.GetConn();
            string s;
            string OracleReq = "Select IsAgency From EREC_REQUESTS where REQUESTID='" + requestID + "'";
            OleDbCommand myReq = new OleDbCommand(OracleReq, myObjs.MyConn);
            OleDbDataReader myRD = myReq.ExecuteReader();
            if (myRD.Read())
            {
                 s = Convert.ToString(myRD["IsAgency"]);

            }
            else
            {
                s = "not found";
            }
            return s;
        }

        private void HidePanel()
        {
            gridShowStatus.Visible = false;
            pnlViewApprovalFlow.Visible = false;
            pnlCreateApprovalFlow.Visible = false;
            pnlRequestStatus.Visible = false; 
            pnlAssignPIC.Visible = false;
            pnlViewManager.Visible = false;
            pnlPICAssignAPic.Visible = false;
            pnlBaseOnRequest.Visible = false;
            pnlApprover.Visible = false;
        }

        private void GetAction(string a)
        {
            HidePanel();
            string toDo = Request.QueryString["do"];
            string FlowID = Request.QueryString["id"];
            string RequestID = Request.QueryString["RequestID"];
            string status = Request.QueryString["status"];
            string isAgency = IsAgencyFromRequest(RequestID);
            switch (a)
            {
                case "view":
                    btnViewAll.Enabled = false;
                    pnlViewApprovalFlow.Visible = true;
                    IsNotAgencyFlow();
                    IsAgencyFlow();
                    string t = Request.QueryString["t"];
                    if (t == "0")
                    {
                        t = "APPROVALFLOW";
                    }
                    else if(t == "1")
                    {
                        t = "APPROVALFLOWFORAGENCY";
                    }
                    if (toDo == "enabled")
                    {
                        SwitchAble(true, FlowID, t);
                    }
                    else if (toDo == "disabled") 
                    {
                        SwitchAble(false, FlowID, t);
                    }
                    break;
                case "add":
                    pnlCreateApprovalFlow.Visible = true; 
                    
                    break;
                case "request":
                    pnlRequestStatus.Visible = true;
                    btnFind.Enabled = false;
                    if (status == "show")
                    {
                        pnlRequestStatus.Visible = true;
                        gridShowStatus.Visible = true;
                        if (isAgency == "0")
                        {

                            LoadApprovalStatus(RequestID);
                        }

                        if (isAgency == "1")
                        {
                            LoadApprovalStatusIsAgency(RequestID);
                        }
                    }
                    break;
                case "assign":
                    pnlAssignPIC.Visible = true;
                    btnAssign.Enabled = false;
                     if(Page.IsPostBack == false)
                     {
                         LoadHRData(ddlHRRecruiter);
                         LoadHRData(ddlHRTrainer);
                         LoadHRData(ddlPSEncoder);
                         SetADefault();
                         SetADefaultApprover();
                     } 
                     
                    break;
                case "approver":
                    pnlAssignPIC.Visible = false;
                    pnlApprover.Visible = true;
                    btnApprover.Enabled = false;
                    if (Page.IsPostBack == false)
                    {
                        LoadHRData(ddlHRReviewer);
                        LoadHRData(ddlHRManager);
                        SetADefaultApprover();
                    }

                    break;         
                
            }
        }


        private void SwitchAble(bool a, string FlowID, string table)
        {
            if (a == true)
            {
                string OracleQry = "Update " + table + " set Status = '1' WHERE FlowID = '" + FlowID + "'";
                myQueries.GetExecuteNonQuery(OracleQry);
            }
            else
            {
                string OracleQry = "Update " + table + " set Status = '0' WHERE FlowID = '" + FlowID + "'";
                myQueries.GetExecuteNonQuery(OracleQry);

            }

            Response.Redirect("Approval.aspx?action=view");
        }


        protected void GridApprovalIsNotAgency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            
            string EmplID = e.Row.Cells[2].Text;
           
            string status = e.Row.Cells[4].Text;
            string id = e.Row.Cells[0].Text;
            string ApproverInfo = e.Row.Cells[3].Text;
            //Get the name of approver from emplid
            e.Row.Cells[2].Text = myQueries.GetFullName(EmplID);
            if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "--NA--";
            }

            //Get The approver based on form
            string OracleQry = "Select REQUESTTYPE From FRMAPPROVERTYPE Where FRMAPPROVERID = '" + ApproverInfo + "'";
            myObjs.GetConn();
            OleDbCommand myCmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            if (myRD.Read())
            {
                e.Row.Cells[3].Text = Convert.ToString(myRD["REQUESTTYPE"]);
            }

            if (status == "0")
            {
                e.Row.Cells[4].Text = "<a href='Approval.aspx?action=view&do=enabled&t=0&id=" + id + "'> " +
                                      "<span style='color: #ff0000;'>Disabled</span></a>";

            }
            else
            {

                e.Row.Cells[4].Text = "<a href='Approval.aspx?action=view&do=disabled&t=0&id=" + id + "'> " +
                                      "<span style='color: #003cff;'>Enabled</span></a>";
            }
        }

       protected void GridApprovalIsAgency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;

            string status = e.Row.Cells[4].Text;
            string id = e.Row.Cells[0].Text;
            string EmplID = e.Row.Cells[2].Text;
            string ApproverInfo = e.Row.Cells[3].Text;
            //Get the name of approver from emplid
            e.Row.Cells[2].Text = myQueries.GetFullName(EmplID);
            if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "--NA--";
            }
            //Get The approver based on form
            string OracleQry = "Select REQUESTTYPE From FRMAPPROVERTYPE Where FRMAPPROVERID = '" + ApproverInfo + "'";
            myObjs.GetConn();
            OleDbCommand myCmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            if (myRD.Read())
            {
                e.Row.Cells[3].Text = Convert.ToString(myRD["REQUESTTYPE"]);
            }


            if (status == "0")
            {
                e.Row.Cells[4].Text = "<a href='Approval.aspx?action=view&do=enabled&t=1&id=" + id + "'> " +
                                      "<span style='color: #ff0000;'>Disabled</span></a>";

            }
            else
            {

                e.Row.Cells[4].Text = "<a href='Approval.aspx?action=view&do=disabled&t=1&id=" + id + "'> " +
                                      "<span style='color: #003cff;'>Enabled</span></a>";
            }
        }

        private void IsNotAgencyFlow()
        {
            string OracleQry = "Select FlowID, Flow, PIC, FRMPIC, Status From ApprovalFlow Where IsAgency = '0' order by FlowID";

            DataTable myDT = myObjs.GetTable(OracleQry);
            gridApprovalIsNotAgency.DataSource = myDT;
            gridApprovalIsNotAgency.DataBind();

            gridApprovalIsNotAgency.HeaderRow.Cells[1].Text = "Flow Title";
            gridApprovalIsNotAgency.HeaderRow.Cells[2].Text = "Approver";
            gridApprovalIsNotAgency.HeaderRow.Cells[3].Text = "Approver Based on Form";
            gridApprovalIsNotAgency.HeaderRow.Cells[4].Text = "Status";
            
        }

        private void IsAgencyFlow()
        {
            string OracleQry = "Select FlowID, Flow, PIC, FRMPIC, Status From APPROVALFLOWFORAGENCY Where IsAgency = '1' order by FlowID";
           
            
            DataTable myDT = myObjs.GetTable(OracleQry);

            gridApprovalIsAgency.DataSource = myDT;
            gridApprovalIsAgency.DataBind();

            gridApprovalIsAgency.HeaderRow.Cells[1].Text = "Flow Title";
            gridApprovalIsAgency.HeaderRow.Cells[2].Text = "Approver";
            gridApprovalIsAgency.HeaderRow.Cells[3].Text = "Approver Based on Form";
            gridApprovalIsAgency.HeaderRow.Cells[4].Text = "Status"; 
        }
 
        protected void FindFormForRequest(object sender, EventArgs e)
        {

            string _filterVal;
            if (txtRequestFrmID.Text != "")
            {
                _filterVal = "Where REQUESTID = '" + txtRequestFrmID.Text + "' ";
            }
            else
            {
                _filterVal = "";
            }
           
            
            gridRequestStatus.Visible = true;
            string OracleQry = "SELECT REQUESTS.REQUESTID, REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                                " APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
                                "from REQUESTS INNER JOIN APPROVALFLOW " +
                                "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                                "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID " + _filterVal + 
                                "ORDER BY REQUESTID DESC";

            DataTable myDT = myObjs.GetTable(OracleQry);
            gridRequestStatus.DataSource = myDT;
            gridRequestStatus.DataBind();
        }

        protected void FindFormForPIC(object sender, EventArgs e)
        { 
        }


        protected void PopulateRequestTable(GridView grid, string _filterVal)
        {
            
            string OracleQry = "";

            if(_filterVal != "")
            {

                OracleQry = "Select Requests.RequestId, Requests.Requestor, " +
                 " ps.ps_sub_wcz_at_vw_a.NAME_A as Name, " +
                 "Requests.RequiredPerson as Required, Requests.Status From Requests, " +
                 "PS.PS_SUB_WCZ_AT_VW_A " +
                 "Where Requests.Requestor =  ps_sub_wcz_at_vw_a.EMPLID AND RequestID = '" + _filterVal + "'";
               
            }
            else    
            {
                OracleQry = "Select Requests.RequestId, Requests.Requestor, " +
                       " ps.ps_sub_wcz_at_vw_a.NAME_A as Name, " +
                      "Requests.RequiredPerson as Required, Requests.Status From Requests, " +
                      "PS.PS_SUB_WCZ_AT_VW_A " +
                      "Where Requests.Requestor =  ps_sub_wcz_at_vw_a.EMPLID";

            }
             

             DataTable myDT = myObjs.GetTable(OracleQry);
             grid.DataSource = myDT;
             grid.DataBind();

             //LoadApprovalStatus(_filterVal);
        }


        protected void GridRequestStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridRequestStatus.PageIndex = e.NewPageIndex;
            
        }

        protected void GridAssignPIC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        { 
        }


        private void LoadDepartment()
        {
            DataTable myDt = myObjs.GetTable(myQueries.GetDeptID());
            ddlDepartment.DataSource = myDt;
            ddlDepartment.DataTextField = "Descr";
            ddlDepartment.DataValueField = "DeptID";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Insert(0, new ListItem("-- Select One --", ""));
        }

        protected void LoadApprovalStatus(string RequestID)
        {
            gridShowStatus.Visible = true;
            gridRequestStatus.Visible = false;
            string OracleQry = "Select REQAPPROVALLOGS.REQUESTID, APPROVALFLOW.FLOW, " +
                   "PS.PS_SUB_WCZ_AT_VW_A.NAME_A as SIGNEE,  REQAPPROVALLOGS.APPROVALSTATUS " + 
                   "From REQAPPROVALLOGS INNER JOIN APPROVALFLOW On   " +
                   "REQAPPROVALLOGS.FLOWID = APPROVALFLOW.FLOWID  And  " +
                   "REQAPPROVALLOGS.REQUESTID = '" + RequestID + "' JOIN PS.PS_SUB_WCZ_AT_VW_A  " +
                   "ON PS.PS_SUB_WCZ_AT_VW_A.EMPLID = REQAPPROVALLOGS.PIC " + 
                   " Order By REQAPPROVALLOGS.FLOWID ASC ";

            DataTable myDT = myQueries.GetTable(OracleQry);
            gridShowStatus.DataSource = myDT;
            gridShowStatus.DataBind();
        }


        protected void LoadApprovalStatusIsAgency(string RequestID)
        {
            gridShowStatus.Visible = true;
            gridRequestStatus.Visible = false;
            string OracleQry = "Select REQAPPROVALLOGS.REQUESTID, APPROVALFLOWFORAGENCY.FLOW, " +
                   "PS.PS_SUB_WCZ_AT_VW_A.NAME_A as SIGNEE,  REQAPPROVALLOGS.APPROVALSTATUS " +
                   "From REQAPPROVALLOGS INNER JOIN APPROVALFLOWFORAGENCY On   " +
                   "REQAPPROVALLOGS.FLOWID = APPROVALFLOWFORAGENCY.FLOWID  And  " +
                   "REQAPPROVALLOGS.REQUESTID = '" + RequestID + "' JOIN PS.PS_SUB_WCZ_AT_VW_A  " +
                   "ON PS.PS_SUB_WCZ_AT_VW_A.EMPLID = REQAPPROVALLOGS.PIC " +
                   " Order By REQAPPROVALLOGS.FLOWID ASC ";

            DataTable myDT = myQueries.GetTable(OracleQry);
            gridShowStatus.DataSource = myDT;
            gridShowStatus.DataBind();
        }

        protected void GridShowStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        { 
                string signed = e.Row.Cells[3].Text;
                switch (signed)
                {
                    case "0":
                        e.Row.Cells[3].Text = "Not Signed";
                        break;
                    case "1":
                        e.Row.Cells[3].Text = "Approved";
                        break;
                    case "2":
                        e.Row.Cells[3].Text = "Need Updates";
                        break;
                    case "3":
                        e.Row.Cells[3].Text = "Rejected";
                        break;
                    default:
                        break;
                }
         }

        private void LoadPIC()
        {
            string filterValDept = ddlDepartment.SelectedValue.ToString(); 
            DataTable myDT = myObjs.GetTable(myQueries.GetEmplFrmDept(filterValDept));
            ddlAssignAPIC.DataSource = myDT;
            ddlAssignAPIC.DataTextField = "Name_A";
            ddlAssignAPIC.DataValueField = "EmplID";
            ddlAssignAPIC.DataBind();

        }

        protected void DlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPICAssignAPic.Visible = true;
            LoadPIC();
        }

        protected void DdlAssignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPICAssignAPic.Visible = true;
            string _filterVal = ddlAssignment.SelectedValue.ToString();

            if (_filterVal == "1") // Assign A PIC
            {
                
                pnlPICAssignAPic.Visible = true;
                pnlBaseOnRequest.Visible = false;
            }
            else if (_filterVal == "2") // Based on Request Form
            {
               
                pnlPICAssignAPic.Visible = false;
                pnlBaseOnRequest.Visible = true;
            }
        }

        private void SaveApprovalData()
        {
            string OracleQry = "insert into approvalflow (FLOWID) Values(APPROVER_SEQ.NEXTVAL)";

             if (ddlAssignment.SelectedValue.ToString() == "1")
            {
             
            }
            else if (ddlAssignment.SelectedValue.ToString() == "2")
            {
            }
             
            if (Page.IsValid)
            {
                myObjs.GetExecuteNonQuery(OracleQry);
            }
        }

        protected void IsSave(object sender, EventArgs e)
        {
            SaveApprovalData();
            Response.Redirect("Approval.aspx?action=view");
        }

        protected void IsCancel(object sender, EventArgs e)
        {

            Response.Redirect("Approval.aspx?action=view");
        }

        protected void IsSaveAnother(object sender, EventArgs e)
        {
            SaveApprovalData();
            Response.Redirect("Approval.aspx?action=add");
        }

        protected void IsSavePIC(object sender, EventArgs e)
        {
            myObjs.GetConn();

            string OracleQry; 
         

            OracleQry = "Update BASEDATA Set HRRECRUITER = :HRrecruiter, " + 
                            "HRTRAINER = :HRtrainer, " +
                            "PSENCODER = :PSencoder";
          
            OleDbCommand mycmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            mycmd.Parameters.AddWithValue("':HRrecruiter'", ddlHRRecruiter.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':HRtrainer'", ddlHRTrainer.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':PSencoder'", ddlPSEncoder.SelectedValue.ToString()); 
            mycmd.ExecuteNonQuery();

            
            lblMessage.Text = "You have successfully updated the PIC list.";
                
        }

    
        public void LoadHRData(DropDownList ddl)
        {

            string OracleQry = "Select EMPLID, NAME_A From PS.PS_SUB_WCZ_AT_VW_A WHERE DEPTID = 'MD0L20' ";
            try
            {
                DataTable myDT = myQueries.GetTable(OracleQry);
                ddl.DataSource = myDT;
                ddl.DataValueField = "EMPLID";
                ddl.DataTextField = "NAME_A";
                ddl.DataBind();

                ddl.Items.Insert(0, new ListItem("-- Select One --", ""));
            }
            catch (Exception e)
            {
            }
        }

        private void SetADefault()
        {
            myObjs.GetConn();
            string OracleQry = "Select HRRECRUITER, HRTRAINER, PSENCODER From BASEDATA";
            OleDbCommand MyCMD = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader MyRD = MyCMD.ExecuteReader();
             
            if (MyRD.Read())
            {
                ddlHRRecruiter.SelectedValue =  Convert.ToString(MyRD["HRRECRUITER"]);
                ddlHRTrainer.SelectedValue   =  Convert.ToString(MyRD["HRTRAINER"]);
                ddlPSEncoder.SelectedValue   = Convert.ToString(MyRD["PSENCODER"]);
            }
            myObjs.MyConn.Close();
              
        }

        private void IsSession()
        {
            myObjs.GetConn();
            string UserID = (string)(Session["UserID"]);
            if (UserID == null)
            {
                string currentURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["uri"] = currentURL;
                Response.Redirect("~/Account/Default.aspx?s=0");
            }
            else
            {
                GetRoleAccess(UserID); 
            }
            myObjs.MyConn.Close();
        }
        private void GetRoleAccess(string EmplID)
        {
            myQueries.GetConn();
            string roleAcc;
            string OracleQry = "Select ROLE From DBUSERS Where EMPLID = '" + EmplID + "'";
            OleDbCommand mycmd = new OleDbCommand(OracleQry, myQueries.MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();

            if (myrd.Read())
            {

                roleAcc = Convert.ToString(myrd["ROLE"]);


                switch (roleAcc)
                {
                    case "1": // Dev
                       
                        break;
                    case "2": // IT Support
                        break;
                    case "3": // HR Reviewer
                        btnViewRequests.Visible = false;
                        btnFind.Visible = false;
                        btnAssign.Visible = true;
                        btnViewAll.Visible = false;
                        btnApprover.Visible = false;
                        break;
                    case "4": // HR Trainer
                        btnApprover.Visible = false;
                        btnAssign.Visible = false;
                        btnViewAll.Visible = false;
                        break;
                    case "5": // HR Recruiter 
                        btnApprover.Visible = false;
                        btnAssign.Visible = false;
                        btnViewAll.Visible = false;
                        break;
                    case "6": // PS ENCODER                        
                        btnApprover.Visible = false;
                        break;
                    case "7": // Requestor
                        btnApprover.Visible = false;
                        break;
                    case "8": // Approver
                        btnApprover.Visible = false;
                        break;
                    default:
                        break;

                }
            }
        }

        protected void IsSaveApprover_Click(object sender, EventArgs e)
        {
            myObjs.GetConn();

            //update APPROVALFLOW table For HR REVIEWER
            string UpdateReviewerQry = "Update APPROVALFLOW set PIC = '" + ddlHRReviewer.SelectedValue + "' " + 
                                       "where FLOWID = '1'";
            OleDbCommand myReviewerCMD = new OleDbCommand(UpdateReviewerQry, myObjs.MyConn);
            myReviewerCMD.ExecuteNonQuery();

            //update APPROVALFLOWFORAGENCY table For HR REVIEWER
            string UpdateReviewerQryIsA = "Update APPROVALFLOWFORAGENCY set PIC = '" + ddlHRReviewer.SelectedValue + "' " +
                            "where FLOWID = '1'";
            OleDbCommand myReviewerCMDIsA = new OleDbCommand(UpdateReviewerQryIsA, myObjs.MyConn);
            myReviewerCMDIsA.ExecuteNonQuery();

            //update APPROVALFLOW table For HR MANAGER
            string UpdateHRManagerQry = "Update APPROVALFLOW set PIC = '" + ddlHRManager.SelectedValue + "' " +
                           "where FLOWID = '3'";
            OleDbCommand myHRManagerCMD = new OleDbCommand(UpdateHRManagerQry, myObjs.MyConn);
            myHRManagerCMD.ExecuteNonQuery();

            //Check if person exists in PS
            string CheckGMFromPS = "Select count(EMPLID) From PS.PS_SUB_WCZ_AT_VW_A Where EMPLID = '" + txtGenManager.Text + "'";
            OleDbCommand cmCountPS = new OleDbCommand(CheckGMFromPS, myObjs.MyConn);
            Int32 count = Convert.ToInt32(cmCountPS.ExecuteScalar());
            if (count != 0)
            {

                //update APPROVALFLOW table For GEN MANAGER
                string UpdateGenManager = "Update APPROVALFLOW set PIC = '" + txtGenManager.Text + "' " +
                   "where FLOWID = '5'";
                OleDbCommand myGenManagerCMD = new OleDbCommand(UpdateGenManager, myObjs.MyConn);
                myGenManagerCMD.ExecuteNonQuery();

            } 

            //update APPROVALFLOWFORAGENCY table For  GEN MANAGER
            string UpdateGenManagerIsA = "Update APPROVALFLOWFORAGENCY set PIC = '" + txtGenManager.Text + "' " +
                            "where FLOWID = '4'";
            OleDbCommand myGenManagerIsA = new OleDbCommand(UpdateGenManagerIsA, myObjs.MyConn);
            myGenManagerIsA.ExecuteNonQuery();
            lblMessage.Text = "Update Successfully!";
            myMics.ShowMessage("Updated Successfully", this.Page);
            myObjs.MyConn.Close();
            Response.Redirect("~/Recruitment/Approval.aspx?action=approver");
        }


        private void SetADefaultApprover()
        {
            myObjs.GetConn();
            string OracleQryReviewer = "Select PIC  From APPROVALFLOW Where FLOWID = '1'";
            OleDbCommand MyCMDReviewer = new OleDbCommand(OracleQryReviewer, myObjs.MyConn);
            OleDbDataReader MyRDReviewer = MyCMDReviewer.ExecuteReader();

            if (MyRDReviewer.Read())
            {
                ddlHRReviewer.SelectedValue = Convert.ToString(MyRDReviewer["PIC"]);   
            }

            string OracleQryHRManager = "Select PIC  From APPROVALFLOW Where FLOWID = '3'";
            OleDbCommand MyCMDOracleQryHRManager = new OleDbCommand(OracleQryHRManager, myObjs.MyConn);
            OleDbDataReader MyRDHRManager = MyCMDOracleQryHRManager.ExecuteReader();

            if (MyRDHRManager.Read())
            {
                ddlHRManager.SelectedValue = Convert.ToString(MyRDHRManager["PIC"]);
            }

            string OracleGetManager = "Select PIC From APPROVALFLOW Where FLOWID = '5'";
            OleDbCommand myCMDGetManager = new OleDbCommand(OracleGetManager, myObjs.MyConn);
            OleDbDataReader myRDGetManager = myCMDGetManager.ExecuteReader();

            if (myRDGetManager.Read())
            {
                txtGenManager.Text = Convert.ToString(myRDGetManager["PIC"]);
            }

            myObjs.MyConn.Close();
            

        }


        protected void BtnGetManagerName_Click(object sender, EventArgs e)
        {
            if (myQueries.GetFullName(txtGenManager.Text) == "0")
            {
                lblName.Text = "ID Does not exists";
            }
            else
            {
                lblName.Text = myQueries.GetFullName(txtGenManager.Text);
            }
        }
    }
}
