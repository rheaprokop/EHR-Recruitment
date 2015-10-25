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
    public partial class Jobs : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearch.Attributes.Add("onfocus", "this.value=''");
           
            if (!IsPostBack)
            {
                pnlError.Visible = false;
                PopulateJobs();
                PopulateGrid();
            }
            else
            {
              
            }
        }

        private void PopulateJobs()
        {
            
            string OraJobs = "select distinct(PS.PS_JOB.JOB_BUSI_TITLE) from PS.PS_JOB ";
            DataTable myDT = myObjs.GetTable(OraJobs);
            ddlPnlJobs.DataSource = myDT;
            ddlPnlJobs.DataTextField = "JOB_BUSI_TITLE";
            ddlPnlJobs.DataValueField = "JOB_BUSI_TITLE";
            ddlPnlJobs.DataBind();

            ddlPnlJobs.Items.Insert(0, new ListItem("-- View All --", "0"));

           
        }


        
        protected void DdlPnlJobs_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlPnlJobs.SelectedValue == "0")
            {
                string OraQry = "Select * From CANDIDATEJOBS Order by CANDIDATEID DESC";
                BindData(OraQry, gridJobs);
            }
            else
            {
                string OraQry = "Select * From CANDIDATEJOBS " + 
                                "Where POSITION LIKE '%" + ddlPnlJobs.SelectedValue + "%'"   + 
                                "Order by CANDIDATEID DESC";
                BindData(OraQry, gridJobs);
            }

            
        }

        private void PopulateGrid()
        {
            string OraQry = "Select * From CANDIDATEJOBS Order by CANDIDATEID DESC";
            BindData(OraQry, gridJobs);
        }

        protected void BtnGoSearch_Click(object sender, EventArgs e)
        {

            if (txtSearch.Text == "" || txtSearch.Text == "Enter Candidate ID, Enter Job")
            {
                string OraQry = "Select * From CANDIDATEJOBS Order by CANDIDATEID DESC";
                BindData(OraQry, gridJobs);
            }
            else
            {
                string OraQry = "Select * From CANDIDATEJOBS " +
                           "Where POSITION LIKE '%" + txtSearch.Text + "%' " +
                           "OR CANDIDATEID LIKE '%" + txtSearch.Text + "%'" +
                           "Order by CANDIDATEID DESC";
 
                BindData(OraQry, gridJobs);
            }
        }

        private void BindData(string OracleQry, GridView grd)
        {
            DataTable myRequests = myObjs.GetTable(OracleQry);
            grd.DataSource = myRequests;
            grd.DataBind();
            myObjs.MyConn.Close();

            if (myObjs.GetTable(OracleQry).Rows.Count == 0)
            {
                pnlError.Visible = true;
                lblError.Text = "No Result from your request.";
            }

        }
    }
}
