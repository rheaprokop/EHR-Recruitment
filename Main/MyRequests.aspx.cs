﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EHR.Model;


namespace EHR.Recruitment
{
    public partial class MyRequests : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc MyMisc = new ObjectsMisc();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowBox();
             
            if (IsPostBack)
                GetData();
            PopulateRequests(txtRequestFrmID.Text);
        }

        protected void IsFindRequest(object sender, EventArgs e)
        {
            PopulateRequests(txtRequestFrmID.Text);
        }

        private void PopulateRequests(string _filter)
        {
            string OracleQry;
            string EmplID = Master.UserID();
            if (_filter == "")
            {

                OracleQry = "SELECT EREC_REQUESTS.REQUESTID, EREC_REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                     "EREC_REQUESTS.DEPTCODE, EREC_REQUESTS.JOBTITLE AS POSITION, EREC_REQUESTS.HIRED, EREC_REQUESTS.REQUIREDPERSON,  EREC_REQUESTS.RESULT  " +
                      "from EREC_REQUESTS INNER JOIN  " +
                     " PS.PS_SUB_WCZ_AT_VW_A " +
                     "ON EREC_REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID Where DELETED = '0' " +
                                      "AND REQUESTOR = '" + EmplID + "' ORDER BY REQUESTID DESC ";

            }
            else
            {
                string Qry = "Where DELETED = '0' AND REQUESTOR = '" + EmplID + "' AND EREC_REQUESTS.REQUESTID LIKE '" + _filter + "%' OR EREC_REQUESTS.REQUESTOR LIKE '" + _filter + "%' OR " +
                            "PS.PS_SUB_WCZ_AT_VW_A.NAME_A LIKE '" + _filter + "%' OR EREC_REQUESTS.RESULT LIKE '" + _filter + "%' ORDER BY REQUESTID DESC ";

                OracleQry = "SELECT EREC_REQUESTS.REQUESTID, EREC_REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                 "EREC_REQUESTS.DEPTCODE, EREC_REQUESTS.JOBTITLE AS POSITION, EREC_REQUESTS.HIRED, EREC_REQUESTS.REQUIREDPERSON,  EREC_REQUESTS.RESULT  " +
                          "from EREC_REQUESTS INNER JOIN  " +
                          "PS.PS_SUB_WCZ_AT_VW_A " +
                          "ON EREC_REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID  " +
                          Qry;

            }

            if (myObjs.GetTable(OracleQry).Rows.Count == 0)
            {
                OracleQry = "SELECT EREC_REQUESTS.REQUESTID, EREC_REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                      "EREC_REQUESTS.DEPTCODE, EREC_REQUESTS.JOBTITLE AS POSITION, EREC_REQUESTS.HIRED, EREC_REQUESTS.REQUIREDPERSON, EREC_REQUESTS.RESULT  " +
                      "from EREC_REQUESTS INNER JOIN  " +
                      "PS.PS_SUB_WCZ_AT_VW_A " +
                      "ON EREC_REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID Where DELETED = '0' " +
                      "AND REQUESTOR = '" + EmplID + "' ORDER BY REQUESTID DESC ";


                MyMisc.ShowMessage("No Record Found.", this.Page);
            }

            DataTable myRequests = myObjs.GetTable(OracleQry);
            gridMyRequests.DataSource = myRequests;
            gridMyRequests.DataBind();
            
        }

        private void GetData()
        {
            ArrayList arr;
            if (ViewState["SelectedRecords"] != null)
                arr = (ArrayList)ViewState["SelectedRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)gridMyRequests.HeaderRow
                                .Cells[0].FindControl("chkAll");
            for (int i = 0; i < gridMyRequests.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(gridMyRequests.DataKeys[i].Value))
                    {
                        arr.Add(gridMyRequests.DataKeys[i].Value);
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)gridMyRequests.Rows[i]
                                       .Cells[0].FindControl("chk");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(gridMyRequests.DataKeys[i].Value))
                        {
                            arr.Add(gridMyRequests.DataKeys[i].Value);
                        }
                    }
                    else
                    {
                        if (arr.Contains(gridMyRequests.DataKeys[i].Value))
                        {
                            arr.Remove(gridMyRequests.DataKeys[i].Value);
                        }
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
        }


        private void SetData()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)gridMyRequests.HeaderRow
                                    .Cells[0].FindControl("chkAll");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            for (int i = 0; i < gridMyRequests.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gridMyRequests.Rows[i]
                                .Cells[0].FindControl("chk");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(gridMyRequests.DataKeys[i].Value);
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
            hfCount.Value = (arr.Count - currentCount).ToString();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            SetData();
            gridMyRequests.AllowPaging = false;
            gridMyRequests.DataBind();
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            count = arr.Count;
            for (int i = 0; i < gridMyRequests.Rows.Count; i++)
            {
                if (arr.Contains(gridMyRequests.DataKeys[i].Value))
                {
                    DeleteRecord(gridMyRequests.DataKeys[i].Value.ToString());
                    arr.Remove(gridMyRequests.DataKeys[i].Value);
                }
            }
            ViewState["SelectedRecords"] = arr;
            hfCount.Value = "0";
            gridMyRequests.AllowPaging = true;
            PopulateRequests(txtRequestFrmID.Text);
            ShowMessage(count);
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gridMyRequests.PageIndex = e.NewPageIndex;
            gridMyRequests.DataBind();
            SetData();
        }

        private void DeleteRecord(string RequestID)
        {
            myObjs.GetConn();
            // string query = "delete from Requests " +
            //                "where RequestID=:RequestID";

            string query = "Update EREC_REQUESTS set DELETED = '1' where RequestID=:RequestID";
            OleDbCommand cmd = new OleDbCommand(query, myObjs.MyConn);
            cmd.Parameters.AddWithValue("':RequestID'", RequestID);

            cmd.ExecuteNonQuery();
            myObjs.MyConn.Close();
        }


        private void ShowMessage(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert('");
            sb.Append(count.ToString());
            sb.Append(" records deleted.');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(),
                            "script", sb.ToString());
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
                    pnlRequestManager.Visible = false;
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
                    btnViewAll.Visible = false; 
                    pnlRequestManager.Visible = false;
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

        protected void GridMyRequests_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                //switch (status)
                //{
                //    case "Update For Changes":
                //        e.Row.Cells[2].Enabled = true;
                //        break;
                //    case "Approved":
                //        e.Row.Cells[2].Enabled = false;
                //        break;
                //    case "Rejected":
                //        e.Row.Cells[2].Enabled = false;
                //        break;
                //    case "Approval Process":
                //        e.Row.Cells[2].Enabled = false;
                //        break;
                //    default:
                //        e.Row.Cells[2].Enabled = false;
                //        break;
                //}

            }
        }


       

        private void ShowBox()
        {
            string result = Request.QueryString["result"];
            if (Page.IsPostBack == false)
            {
                switch (result)
                {
                    case "0":
                        MyMisc.ShowMessage("You have created a new request successfully.", this.Page);
                        break;
                    default:
                        break;
                }
            }
        }
       
    }
}
