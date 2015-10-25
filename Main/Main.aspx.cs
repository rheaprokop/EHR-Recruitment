using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;

namespace EHR.Views.Recruitment
{
    public partial class Main : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQueries = new QueryOleDB();
        GlobalAccountModel _account = new GlobalAccountModel();
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel(); 

        protected void Page_Load(object sender, EventArgs e)
        { 
            LoadActivities();

            MainAccessView();
        }


        private void RequesterRequestQry(string EmplID)
        {
            myQueries.GetConn();
            string roleAcc; 
            string OracleQryQ = "Select ROLE From DBUSERS Where EMPLID = '" + EmplID + "'";
            OleDbCommand mycmd = new OleDbCommand(OracleQryQ, myQueries.MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();

            if (myrd.Read())
            {
                roleAcc = Convert.ToString(myrd["ROLE"]);                
 
            }
        }


        protected void GetDataOnGrid(string qry, GridView grd)
        {
            DataTable myDT = myObjs.GetTable(qry);
            grd.DataSource = myDT;
            grd.DataBind();            
        }
  

        private void LoadActivities()
        {
            string UserID = (string)(Session["UserID"]);
            string OraQry = "select ACTIVITYDESC from activities " + 
                    "where rownum <= 8 AND " + 
                    "EMPLID = '" + UserID + "' " + 
                    "order by ACTIVITYID DESC";
            DataTable myDt = myObjs.GetTable(OraQry);
             
        } 

        protected void GridRecentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[7].Text;
                if (status == "Approved")
                {
                    e.Row.Cells[7].Text = "Recruitment";
                    
                }
            }
        }

        private void MainAccessView()
        {
            string sql;
            if (_ps.GetEmplDepartment(Master.UserID()) == "MD0H00")
            {
                pnlHRPanel.Visible = true;

                sql = "SELECT EREC_REQUESTS.REQUESTID, EREC_REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                       "EREC_REQUESTS.DEPTCODE, EREC_REQUESTS.JOBTITLE AS POSITION, EREC_REQUESTS.HIRED, " +
                       "EREC_REQUESTS.REQUIREDPERSON, EREC_REQUESTS.RESULT  " +
                                      "from EREC_REQUESTS INNER JOIN " +
                                      " PS.PS_SUB_WCZ_AT_VW_A " +
                                      "ON EREC_REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID Where EREC_REQUESTS.DELETED = '0' ORDER BY EREC_REQUESTS.REQUESTID DESC ";
            }
            else
            {

                sql = "SELECT EREC_REQUESTS.REQUESTID, EREC_REQUESTS.REQUESTOR, PS.PS_SUB_WCZ_AT_VW_A.NAME_A as Name, " +
                 "EREC_REQUESTS.DEPTCODE, EREC_REQUESTS.JOBTITLE AS POSITION, EREC_REQUESTS.HIRED, EREC_REQUESTS.REQUIREDPERSON,  EREC_REQUESTS.RESULT  " +
                  "from EREC_REQUESTS INNER JOIN  " +
                 " PS.PS_SUB_WCZ_AT_VW_A " +
                 "ON EREC_REQUESTS.REQUESTOR = PS_SUB_WCZ_AT_VW_A.EMPLID Where DELETED = '0' " +
                                  "AND REQUESTOR = '" + Master.UserID() + "' ORDER BY REQUESTID DESC ";
            }

            gridRecentRequest.DataSource = _db.GetTable(sql);
            gridRecentRequest.DataBind(); 

        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        { 
            gridRecentRequest.PageIndex = e.NewPageIndex;
            gridRecentRequest.DataBind();
            MainAccessView();
        }
    }
}
