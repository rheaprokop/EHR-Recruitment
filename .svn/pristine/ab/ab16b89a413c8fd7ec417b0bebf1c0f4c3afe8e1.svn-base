using System;
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
    public partial class Inbox : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc myMisc = new ObjectsMisc(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                string EmplID = (string)(Session["UserID"]);
                PopulateInbox(EmplID);
            }
        }

        private void PopulateInbox(string EmplID)
        {
            //string InboxQry = "Select * from InboxApp where EMPLID = '" + EmplID + "' Order by InboxID ASC";
            string InboxQry = "Select INBOXAPP.INBOXID, INBOXAPP.FLOWID AS FLOWID, " + 
                              "INBOXAPP.REQUESTID AS REQUESTID, REQUESTS.REQUESTOR AS REQUESTOR, " +
                              "PS.PS_SUB_WCZ_AT_VW_A.NAME_A as NAME_A, INBOXAPP.APPROVED, INBOXAPP.APPROVALDATE AS APP,  " +
                              "INBOXAPP.ACTION AS ACTION from INBOXAPP " +
                              "join REQUESTS " +
                              "on REQUESTS.REQUESTID = INBOXAPP.REQUESTID " +
                              "join PS.PS_SUB_WCZ_AT_VW_A " +
                              "on PS.PS_SUB_WCZ_AT_VW_A.EMPLID = REQUESTS.REQUESTOR " +
                              "where INBOXAPP.EMPLID = '" + EmplID + "' Order by INBOXAPP.APPROVED ASC"; 

            DataTable myDT = myObjs.GetTable(InboxQry);
            gridInbox.DataSource = myDT;
            gridInbox.DataBind();  

        }

        protected void GridInbox_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[5].Text;
                if (status == "1")
                {
                    e.Row.Cells[5].Text = "---Done---";
                }
                else
                {
                    e.Row.Cells[5].Text = "<span style='color: red; font-weight: bold;'>Waiting For Approval</span>";
                }

                string appDate = e.Row.Cells[6].Text;
                if (appDate == null)
                {
                    e.Row.Cells[6].Text = "--- NA ---";
                }
                

            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gridInbox.PageIndex = e.NewPageIndex;
            //gridInbox.DataBind();
            string EmplID = (string)(Session["UserID"]);
            PopulateInbox(EmplID);
        }
    }
}
