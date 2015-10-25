using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model; 

namespace EHR.Main
{
    public partial class AssignAgent : System.Web.UI.Page
    {
        PSModel _ps = new PSModel();
        DBModel _db = new DBModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calStartDate.Visible = false;
                calEndDate.Visible = false;
                txtEmplName.Text = _ps.GetName(Master.UserID());
                txtEmployeeDeptID.Text = _ps.GetEmplDepartment(Master.UserID());

                PopulateDeptID();
                PopulateEmplForDept(txtEmployeeDeptID.Text);
            }

           
        }
        //CALENDAR MANI..
        protected void ViewOnBoardCal_OnClick(object sender, ImageClickEventArgs e)
        { 
            if (calStartDate.Visible == false)
            {
                calStartDate.Visible = true;
            }
            else
            {
                calStartDate.Visible = false; 
            } 
        }

        protected void ViewCalEndDate_Click(object sender, ImageClickEventArgs e)
        {

            if (calEndDate.Visible == false)
            {
                calEndDate.Visible = true;
            }
            else
            {
                calEndDate.Visible = false;

            }
        }

        protected void CalStartDate_OnChanged(object sender, EventArgs e)
        {
            foreach (DateTime day in calStartDate.SelectedDates)
            {
                txtStartDate.Text = "";
                txtStartDate.Text += day.Date.ToString("d/M/yyyy");
                calStartDate.Visible = false;
            }
        }

        protected void CalEndDate_Changed(object sender, EventArgs e)
        {
            foreach (DateTime day in calEndDate.SelectedDates)
            {
                txtEndBox.Text = "";
                txtEndBox.Text += day.Date.ToString("d/M/yyyy");
                calEndDate.Visible = false;
            }
        }

        protected void DdlAgentDeptID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAgentDeptID.SelectedValue != "0")
            {
                PopulateEmplForDept(ddlAgentDeptID.SelectedValue);
            }
            else
            {
                PopulateEmplForDept(txtEmployeeDeptID.Text);
            }
        }

        private void PopulateEmplForDept(string deptid)
        {  

            string sql = "SELECT EMPLID, NAME_A FROM PS.PS_SUB_WCZ_AT_VW_A WHERE DEPTID = '" + deptid + "'";
            DataTable _dtDept = _db.GetTable(sql);
            ddlAgent.DataSource = _dtDept;
            ddlAgent.DataTextField = "NAME_A";
            ddlAgent.DataValueField = "EMPLID";
            ddlAgent.DataBind(); 
        }

        private void PopulateDeptID()
        {
            string sql = "SELECT DEPTID FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID LIKE '%MD%'";
            DataTable _dt = _db.GetTable(sql);
            ddlAgentDeptID.DataSource = _dt;
            ddlAgentDeptID.DataTextField = "DEPTID";
            ddlAgentDeptID.DataValueField = "DEPTID";
            ddlAgentDeptID.DataBind();

            if (txtEmployeeDeptID.Text != "")
            {
                ddlAgentDeptID.SelectedValue = txtEmployeeDeptID.Text;

            }
            else
            {
                ddlAgentDeptID.Items.Insert(0, new ListItem("-- Select Dept --", "0"));
            }
            
        }

        protected void BtnInsertAgent_Click(object sender, EventArgs e)
        { 
            string dateStart = txtStartDate.Text;
            string dateEnd = txtEndBox.Text;
            string sql = "INSERT INTO AGENTS(APPLICATIONID, AGENTID, START_DATE_TIME, " + 
                "END_DATE_TIME, REMARKS, APPEMPLID) VALUES('4', '" + ddlAgent.SelectedValue+ "', " +
                "to_date('" + dateStart + "', 'dd/MM/yyyy'), to_date('" + dateEnd + "', 'dd/MM/yyyy'), " + 
                "'" + txtRemarks.Text + "', '" + Master.UserID() + "')";
            _db.GetConn();
            _db.GetExecuteNonQuery(sql); 
            
        }


    }
}
