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
    public partial class Forms : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearchRequest.Attributes.Add("onfocus", "this.value=''");
            txtCandidate.Attributes.Add("onfocus", "this.value=''");
            PopulateRequests(txtSearchRequest.Text);
            PopulateCandidate(txtCandidate.Text);
            IsSession();
            if (!Page.IsPostBack)
            {
                
                pnlError.Visible = false;
                pnlErrorReq.Visible = false;
            }
        }

        //REQUESTS REPORT STARTS HERE
        protected void BtnGoSearch_Click(object sender, EventArgs e)
        {
            PopulateRequests(txtSearchRequest.Text);
        }

        protected void DdlReportRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            string OracleQry;
            string ddlOption = ddlReportRequest.SelectedValue;
            if (ddlOption == "")
            {


                OracleQry = "SELECT REQUESTS.REQUESTID, REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                         "REQUESTS.DEPTCODE, REQUESTS.HIRED, REQUESTS.REQUIREDPERSON, APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
                         "from REQUESTS INNER JOIN APPROVALFLOW " +
                         "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                         "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  ORDER BY REQUESTID DESC "; 
            }
            else
            {
                string OraQry;

                switch (ddlOption)
                {
                    case "1":
                        OraQry = "Order By REQUESTDATETIME ";
                        break;
                    case "2":
                        OraQry = "Order By REQUESTID ";
                        break;
                    default:
                        OraQry = "Order By REQUESTID ";
                        break;
                }

                OracleQry = "SELECT REQUESTS.REQUESTID,  PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                         "REQUESTS.DEPTCODE, REQUESTS.HIRED, REQUESTS.REQUIREDPERSON, APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
                         "from REQUESTS INNER JOIN APPROVALFLOW " +
                         "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                         "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID   " +
                        OraQry;


                if (myObjs.GetTable(OracleQry).Rows.Count == 0)
                {
                    pnlErrorReq.Visible = true;
                    lblReqError.Text = "No Record Found from your search.";
                }
                BindDataRequest(OracleQry, gridAllRequest);
            }
        }

        private void PopulateRequests(string _filterVal)
        {
            string OracleQry;
            if (_filterVal == "Enter Employee ID, Requestor, Requestor ID" || _filterVal == "")
            {
                OracleQry = "SELECT REQUESTS.REQUESTID,  PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                         "REQUESTS.DEPTCODE, REQUESTS.JOBBUSITITLE, REQUESTS.HIRED, REQUESTS.REQUIREDPERSON, APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
                         "from REQUESTS INNER JOIN APPROVALFLOW " +
                         "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                         "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  WHERE ROWNUM <= 5 ORDER BY REQUESTID DESC "; 
            }
            else
            {
                string _filterValQry = "Where ROWNUM <= 5  AND REQUESTS.REQUESTID LIKE '%" + _filterVal + "%' " +
                                       "OR REQUESTS.REQUESTOR LIKE '%" + _filterVal + "%' " +
                                       "OR PS.PS_SUB_WCZ_AT_VW_A.NAME_A LIKE '%" + _filterVal + "%' ";

                OracleQry = "SELECT REQUESTS.REQUESTID, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                         "REQUESTS.DEPTCODE, REQUESTS.JOBBUSITITLE, REQUESTS.HIRED, REQUESTS.REQUIREDPERSON, APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
                         "from REQUESTS INNER JOIN APPROVALFLOW " +
                         "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                         "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  " + 
                      _filterValQry +
                      "ORDER BY REQUESTID DESC ";


            }
            if (myObjs.GetTable(OracleQry).Rows.Count == 0)
            {
                pnlErrorReq.Visible = true;
                lblReqError.Text = "No Record Found from your search.";
            }
            BindDataRequest(OracleQry, gridAllRequest);
        }

        //CANDIDATE REPORTS STARTS HERE

        protected void BtnGoCandidateSearch_Click(object sender, EventArgs e)
        {
            PopulateCandidate(txtCandidate.Text);
        }

        protected void DdlCandidates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string OracleQry;
            string ddlOption = ddlReportRequest.SelectedValue;
            if (ddlOption == "")
            {

                OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                            " HIRINGSTATUS as STATUS From CANDIDATES WHERE ROWNUM <= 5 " +
                            "ORDER BY RECORDEDDATE DESC";
            }
            else
            {
                string OraQry;

                switch (ddlOption)
                {
                    case "1":
                        OraQry = "Order By CANDIDATEID ";
                        break;
                    case "2":
                        OraQry = "Order By STATUS ";
                        break;
                    default:
                        OraQry = "Order By CANDIDATEID ";
                        break;
                }

                OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                            " HIRINGSTATUS as STATUS From CANDIDATES WHERE ROWNUM <= 5 " +
                      OraQry;
                if (myObjs.GetTable(OracleQry).Rows.Count == 0)
                {
                    pnlError.Visible = true;
                    lbl_CanError.Text = "No Record Found from your search.";
                }
                BindDataRequest(OracleQry, gridAllRequest);
            }
        }

        private void PopulateCandidate(string _filterVal)
        {
            string OracleQry;
            if (_filterVal == "Enter Candidate ID, Name" || _filterVal == "")
            {
                OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                            " HIRINGSTATUS as STATUS From CANDIDATES WHERE ROWNUM <= 5 " +
                            "ORDER BY RECORDEDDATE DESC";
            }
            else
            {
                string _filterValQry = "WHERE ROWNUM <= 5  AND CANDIDATEID LIKE '" + _filterVal + "%' " +
                                       "OR LASTNAME LIKE '" + _filterVal + "%' " +
                                       "OR FIRSTNAME LIKE '" + _filterVal + "%' ";

                OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                            " HIRINGSTATUS as STATUS From CANDIDATES " +
                            _filterValQry +
                            "ORDER BY CANDIDATEID DESC ";


            }
            if (myObjs.GetTable(OracleQry).Rows.Count == 0)
            {
                pnlError.Visible = true;
                lbl_CanError.Text = "No Record Found from your search.";
            }
            BindData(OracleQry, gridCandidates);
        }



        private void BindData(string OracleQry, GridView grd)
        {
            DataTable myRequests = myObjs.GetTable(OracleQry);
            grd.DataSource = myRequests;
            grd.DataBind();
            myObjs.MyConn.Close();


        }

        private void BindDataRequest(string OracleQry, GridView grd)
        {
            DataTable myRequests = myObjs.GetTable(OracleQry);
            grd.DataSource = myRequests;
            grd.DataBind();
            myObjs.MyConn.Close();

            if (grd.Rows.Count >= 1)
            {

                grd.HeaderRow.Cells[1].Text = "Request ID"; 
                grd.HeaderRow.Cells[2].Text = "Name";
                grd.HeaderRow.Cells[3].Text = "Request Dept";
                grd.HeaderRow.Cells[4].Text = "Position";
                grd.HeaderRow.Cells[5].Text = "Hired";
                grd.HeaderRow.Cells[6].Text = "# of Person(s)";
                grd.HeaderRow.Cells[7].Text = "Last Approved By";
                grd.HeaderRow.Cells[8].Text = "Request Status";
            }
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
                IsAllowedToPage(UserID);
                    GetAccessRole(UserID);
                
            }
            myObjs.MyConn.Close();
        }

        private void GetAccessRole(string EmplID)
        { 
             
            switch (myQry.RoleAccess(EmplID))
            {
                case "1": // Dev 
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = true;
                    break;
                case "2": // IT Support 
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = true;
                    break;
                case "3": // HR Reviewer 
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = false;
                    break;
                case "4": // HR Trainer 
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                case "5": // HR Recruiter 
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = true;
                    break;
                case "6": // PS ENCODER 
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                case "7": // Requestor 
                    pnlRequestManager.Visible = true;
                    pnlCandidateManager.Visible = false;
                    break;
                case "8": // Approver 
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;
                default:
                    pnlRequestManager.Visible = false;
                    pnlCandidateManager.Visible = false;
                    break;

            }
        }


        private void IsAllowedToPage(string EmplID)
        {
            switch (myQry.RoleAccess(EmplID))
            {
                case "1": // Dev 
                   
                    break;
                case "2": // IT Support 
                    
                    break;
                case "3": // HR Reviewer 
                    
                    break;
                case "4": // HR Trainer 
                    Response.Redirect("~/Account/Default.aspx?s=0");

                    break;
                case "5": // HR Recruiter 

                    break;
                case "6": // PS ENCODER 
                    Response.Redirect("~/Account/Default.aspx?s=0"); 

                    break;
                case "7": // Requestor 
                    Response.Redirect("~/Account/Default.aspx?s=0"); 
                    break;
                case "8": // Approver 
                    Response.Redirect("~/Account/Default.aspx?s=0"); 
                    break;
                default:
                    Response.Redirect("~/Account/Default.aspx?s=0"); 

                    break;

            }
        }
    }
}
