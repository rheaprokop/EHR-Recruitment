using System;

using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

namespace EHR.Views.Shared
{
    public partial class Inbox : System.Web.UI.Page
    {
        AccountModel _accnt = new AccountModel();
        DBModel _db = new DBModel();
        MiscModel _misc = new MiscModel();
        EncryptDecryptQueryString enc = new EncryptDecryptQueryString(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                PopulateApprove();
                PopulateRejected();
            }
            PopulateInbox();
            pnlDialog.Visible = false;
            if (Request.QueryString["del"] == "1")
            {
                pnlDialog.Visible = true;
                _misc.GetMsgBox("29", pnlDialog, lblErrorMsg);
            }
            else
            {
                pnlDialog.Visible = false;
            }
            string reqID = Request.QueryString["id"]; 
                if (reqID != null)
                {
                    RedirectPage(reqID);
                } 
        }

        private void RedirectPage(string reqID)
        {
            string id;
            string query = "SELECT COUNT(REQUESTID) FROM STATUS_CHANGE WHERE REQUESTID = '" + reqID + "'";
            int count = _db.GetExecuteScalar(query);
            if (count > 1)
            {
                Response.Redirect("~/Views/Status/RQ07.aspx?id=" + reqID);

                 
            }
            else if (count == 1)
            {

                id = reqID;
                string strURL = "~/Views/Status/RQ04.aspx?";
                if (HttpContext.Current != null)
                {
                    string strURLWithData = strURL +
                      EncryptQueryString(reqID);
                    HttpContext.Current.Response.Redirect(strURLWithData);
                }
                else
                { }
                 
                
            }
            else
            {
                 
            }
        }

        public string EncryptQueryString(string strQueryString)
        {
            EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
            return objEDQueryString.Encrypt(strQueryString, "r0b1nr0y");
        }
                  
        /// <summary>
        /// gridWaiting
        /// </summary>

        private void PopulateInbox()
        {
            string sql = "SELECT DISTINCT(B.REQUESTID) REQID,  B.REQUESTORID REQUESTOR, C.NAME_A NAME_A, " + 
                        "B.REQUESTORDEPTID DEPTID, B.REQDATE REQDATE " +
                        "FROM APPROVALTRANSACTION A, STATUS_CHANGE B, PS.PS_SUB_WCZ_AT_VW_A   C " + 
                        "WHERE  C.EMPLID = B.REQUESTORID AND A.APPEMPLID = '" + _accnt.GetUserSessID() + "' AND A.RESULT = 'W' " + 
                        "AND A.REQID = B.TRANSID ORDER BY B.REQUESTID DESC";
            
            DataTable _dt = _db.GetTable(sql);

            if (_dt.Rows.Count >= 1)
            {
                lblWaiting.Visible = true;
                lblWaiting.Text = "You have " + _dt.Rows.Count + " requests to approved. <br />";

                gridWaiting.DataSource = _dt;
                gridWaiting.DataBind();
                GetWaitingLink();
            }
            else
            {
                gridWaiting.Visible = false;
                lblWaiting.Visible = true;
                lblWaiting.Text = "No Request To Approve.";
            }
        }


        protected void OnPagingWaiting(object sender, GridViewPageEventArgs e)
        {
            gridWaiting.PageIndex = e.NewPageIndex;
            gridWaiting.DataBind();
            //SetData();
        }

        /// <summary>
        /// gridSigned
        /// </summary>
        /// 
        private void PopulateApprove()
        {
            string sql = "SELECT DISTINCT(B.REQUESTID) REQID,  B.REQUESTORID REQSTRID, B.REQUESTORDEPTID DEPTID, " +
                         "B.REQDATE REQDATE,  A.REQID TRANSID FROM APPROVALTRANSACTION A, STATUS_CHANGE B " +
                         "WHERE  A.ACTUALAPPROVER = '" + _accnt.GetUserSessID() + "' AND A.RESULT = 'A' AND A.REQID = B.TRANSID AND  " +
                         "A.DELETED IS NULL ORDER BY B.REQUESTID DESC";

            DataTable _dt = _db.GetTable(sql);
            if (_dt.Rows.Count >= 1)
            { 
                gridApproved.DataSource = _dt;
                gridApproved.DataBind();
            }
            else
            {
                gridApproved.Visible = false;
                lblApproved.Visible = true;
                lblApproved.Text = "No Request Approved."; 
            }  
        }

        protected void chkAllApproved_OnSelectedChanged(object sender, EventArgs e)
        {
            var listControl = (CheckBox)sender;
            var row = (GridViewRow)listControl.NamingContainer;
            var item = listControl.Checked;

            foreach (GridViewRow gvrow in gridApproved.Rows)
            { 
                string transno = (string)gridApproved.DataKeys[gvrow.RowIndex].Value;
                CheckBox chk = (CheckBox)gvrow.FindControl("chkApproved");
                 
            }
        }

        protected void btnApproved_DeleteApproved(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gridApproved.Rows)
            {
                string appv;
                string transno = (string)gridApproved.DataKeys[gvrow.RowIndex].Value;

                //RadioButtonList rblAnswer = (RadioButtonList)gvrow.FindControl("rd");
                CheckBox chkSelected = (CheckBox)gvrow.FindControl("chkApproved");
                if (chkSelected.Checked)
                {
                    DeleteApprovedRecord(transno);
                }
            }

            Response.Redirect("Inbox.aspx?del=1");
        }
           
        private void DeleteApprovedRecord(string reqid)
        { 
            string sql = "UPDATE APPROVALTRANSACTION SET DELETED = '1' " +
                "WHERE ACTUALAPPROVER = '" + _accnt.GetUserSessID() + "' AND REQID = '" + reqid + "' " +
                "AND RESULT = 'A'";
            _db.GetExecuteNonQuery(sql);

        }

        protected void OnPagingApproved(object sender, GridViewPageEventArgs e)
        {
            gridApproved.PageIndex = e.NewPageIndex;
            gridApproved.DataBind();
            PopulateApprove();
        }

        /// <summary>
        /// gridRejected
        /// </summary>
        /// 

        private void PopulateRejected()
        {
            string sql = "SELECT DISTINCT(B.REQUESTID) REQID,  B.REQUESTORID REQSTRID, " +
                "B.REQUESTORDEPTID DEPTID, B.EMPLID EMPLOYEE,  B.REQDATE REQDATE, A.REQID  TRANSID " +
                "FROM APPROVALTRANSACTION A, STATUS_CHANGE B WHERE  A.ACTUALAPPROVER = '" + _accnt.GetUserSessID() + "' " +
                "AND A.RESULT = 'R' AND A.REQID = B.TRANSID AND A.DELETED IS NULL  ORDER BY B.REQUESTID DESC";

            DataTable _dt = _db.GetTable(sql);
            if (_dt.Rows.Count >= 1)
            {
                gridRejected.DataSource = _dt;
                gridRejected.DataBind();
            }
            else
            {
                gridRejected.Visible = false;
                lblRejected.Visible = true;
                lblRejected.Text = "No Request Rejected.";
                btnReject_Click.Visible = false;
            }  
        }

        protected void btnRejected_DeleteRejected(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in gridRejected.Rows)
            {
                
                string transno = (string)gridRejected.DataKeys[gvrow.RowIndex].Value;
 
                CheckBox chkSelected = (CheckBox)gvrow.FindControl("chkRejected");
                if (chkSelected.Checked)
                {
                    DeleteRejectedRecord(transno);
                }
            }

            Response.Redirect("Inbox.aspx?del=1");
        }

        private void DeleteRejectedRecord(string reqid)
        {
            string sql = "UPDATE APPROVALTRANSACTION SET DELETED = '1' " +
                "WHERE ACTUALAPPROVER = '" + _accnt.GetUserSessID() + "' AND REQID = '" + reqid + "' " +
                "AND RESULT = 'R'";
            _db.GetExecuteNonQuery(sql);

        }


        protected void OnPagingRejected(object sender, GridViewPageEventArgs e)
        {
            gridRejected.PageIndex = e.NewPageIndex;
            gridRejected.DataBind();
           // SetData();
        }

        
        
        /// <summary>
        /// ///// MISCELLANEOUS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridWaiting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string str = e.Row.Cells[1].Text;
            }
        }

        private void GetWaitingLink()
        {
            foreach (GridViewRow gvrow in gridWaiting.Rows)
            {
                HyperLink linkWaiting = (HyperLink)gridWaiting.FindControl("linkReqID");
                //linkWaiting.NavigateUrl = "r";
                //linkWaiting.NavigateUrl = "r";
                //string test = linkWaiting + "1";
            }
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

        

         
    }
     
}
