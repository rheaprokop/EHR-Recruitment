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
    public partial class Status : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            string requestID = Request.QueryString["RequestID"];
            LoadApprovalStatus(requestID);
        }

        protected void LoadApprovalStatus(string RequestID)
        {

            string OraApprovalLogs = "Select REQAPPROVALLOGS.REQUESTID, APPROVALFLOW.FLOW, " +
                   "PS.PS_SUB_WCZ_AT_VW_A.NAME_A as SIGNEE,  REQAPPROVALLOGS.APPROVALSTATUS  " +
                   "From REQAPPROVALLOGS INNER JOIN APPROVALFLOW On   " +
                   "REQAPPROVALLOGS.FLOWID = APPROVALFLOW.FLOWID  And  " +
                   "REQAPPROVALLOGS.REQUESTID = '" + RequestID + "' JOIN PS.PS_SUB_WCZ_AT_VW_A  " +
                   "ON PS.PS_SUB_WCZ_AT_VW_A.EMPLID = REQAPPROVALLOGS.PIC Order By REQAPPROVALLOGS.FLOWID ASC ";

            DataTable myDT = myObjs.GetTable(OraApprovalLogs);
            gridShowStatus.DataSource = myDT;
            gridShowStatus.DataBind();
        }

      /* protected void GridShowStatus_RowDataBound(object sender, GridViewRowEventArgs e)
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
         } */
    }
}
