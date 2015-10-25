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
    public partial class Reports : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserID = (string)(Session["UserID"]);
            string a = Request.QueryString["view"];
            if (UserID == "")
            {
                Response.Redirect("../Default.aspx");
            }
            txtSearchRequest.Attributes.Add("onfocus", "this.value=''");
            txtCandidate.Attributes.Add("onfocus", "this.value=''");
            PopulateRequests(txtSearchRequest.Text);
            PopulateCandidate(txtCandidate.Text);

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
                                      "REQUESTs.REQUIREDPERSON, APPROVALFLOW.FLOW AS SIGNEE, REQUESTS.RESULT  AS STATUS " +
                                      "from REQUESTS INNER JOIN APPROVALFLOW " +
                                      "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                                      "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  " +
                                      "ORDER BY REQUESTS.REQUESTID DESC ";
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

                OracleQry = "SELECT REQUESTS.REQUESTID, REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                      "REQUESTs.REQUIREDPERSON, APPROVALFLOW.FLOW AS SIGNEE, REQUESTS.RESULT  AS STATUS " +
                      "from REQUESTS INNER JOIN APPROVALFLOW " +
                      "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                      "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  " +
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
                                      "REQUESTS.DEPTCODE, REQUESTS.REQUIREDPERSON, APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
                                      "from REQUESTS INNER JOIN APPROVALFLOW " +
                                      "ON REQUESTS.STATUS = APPROVALFLOW.FLOWID JOIN PS.PS_SUB_WCZ_AT_VW_A " +
                                      "ON REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  ORDER BY REQUESTID DESC "; 
            }
            else
            {
                string _filterValQry = "Where REQUESTS.REQUESTID LIKE '%" + _filterVal + "%' " +
                                       "OR REQUESTS.REQUESTOR LIKE '%" + _filterVal + "%' " +
                                       "OR PS.PS_SUB_WCZ_AT_VW_A.NAME_A LIKE '%" + _filterVal + "%' ";

                OracleQry = "SELECT REQUESTS.REQUESTID,  PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                       "REQUESTS.DEPTCODE, REQUESTS.REQUIREDPERSON, APPROVALFLOW.FLOW, REQUESTS.RESULT  " +
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
            string ddlOption = ddlCandidates.SelectedValue;
            if (ddlOption == "")
            {

                OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                            " HIRINGSTATUS as STATUS From CANDIDATES " +
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
                            " HIRINGSTATUS as STATUS From CANDIDATES " +
                      OraQry;
                if (myObjs.GetTable(OracleQry).Rows.Count == 0)
                {
                    pnlError.Visible = true;
                    lbl_CanError.Text = "No Record Found from your search.";
                }
                BindDataCand(OracleQry, gridCandidates);
            }   
        }

        private void PopulateCandidate(string _filterVal)
        {
            string OracleQry;
            if (_filterVal == "Enter Candidate ID, Name" || _filterVal == "")
            {
                OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                            " HIRINGSTATUS as STATUS From CANDIDATES " +
                            "ORDER BY RECORDEDDATE DESC";
            }
            else
            {
                string _filterValQry = "Where CANDIDATEID LIKE '%" + _filterVal + "%' " +
                                       "OR LASTNAME LIKE '%" + _filterVal + "%' " +
                                       "OR FIRSTNAME LIKE '%" + _filterVal + "%' ";

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
            BindDataCand(OracleQry, gridCandidates);
        }

        private void BindDataCand(string OracleQry, GridView grid)
        {
            DataTable myRequests = myObjs.GetTable(OracleQry);
            grid.DataSource = myRequests;
            grid.DataBind();

            if(myObjs.GetTable(OracleQry).Rows.Count != 0){
            grid.HeaderRow.Cells[1].Text = "Candidate ID";
            grid.HeaderRow.Cells[2].Text = "Candidate";
            grid.HeaderRow.Cells[3].Text = "Status"; 
            }
            myObjs.MyConn.Close();


        }

        private void BindDataRequest(string OracleQry, GridView grd)
        {
            DataTable myRequests = myObjs.GetTable(OracleQry);
            grd.DataSource = myRequests;
            grd.DataBind();

            if (myObjs.GetTable(OracleQry).Rows.Count != 0)
            {

                grd.HeaderRow.Cells[0].Text = "View";
                grd.HeaderRow.Cells[1].Text = "Request ID";
                grd.HeaderRow.Cells[2].Text = "Name";
                grd.HeaderRow.Cells[3].Text = "Request Dept";
                grd.HeaderRow.Cells[4].Text = "# of Person(s)";
                grd.HeaderRow.Cells[5].Text = "Last Approved By";
                grd.HeaderRow.Cells[6].Text = "Request Status";
            }
            myObjs.MyConn.Close();
             
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gridAllRequest.PageIndex = e.NewPageIndex;
            gridAllRequest.DataBind();
            PopulateRequests(txtSearchRequest.Text);
        }


        protected void OnPagingCandidate(object sender, GridViewPageEventArgs e)
        {
            gridCandidates.PageIndex = e.NewPageIndex;
            gridCandidates.DataBind();
            PopulateCandidate(txtCandidate.Text);
        }
    }
}
