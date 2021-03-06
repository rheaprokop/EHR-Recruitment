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
    public partial class Candidate : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        ObjectsMisc myMisc = new ObjectsMisc();
        QueryOleDB myQry = new QueryOleDB(); 

        protected void Page_Load(object sender, EventArgs e)
        {
          
             

            string s = Request.QueryString["s"];
            if (s == "0")
            {
                myMisc.ShowMessage("Successfully Updated Record", this.Page);
            }
            if (s == "1")
            {
                myMisc.ShowMessage("Successfully Added Record", this.Page);
            }
            if (IsPostBack) 
                GetData();
                LoadCandidates(txtCandidateID.Text); 
        }

        protected void DdlCandidateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _filter = ddlCandidateFilter.SelectedValue;
            string qry;
            switch (_filter)
            {

                case "1":
                    qry = "Where HIRINGSTATUS = 'In File'";
                    break;
                case "2":
                    qry = "Where HIRINGSTATUS = 'For Interview'";
                    break;
                case "3":
                    qry = "Where HIRINGSTATUS = 'Interviewed'";
                    break;
                case "4":
                    qry = "Where HIRINGSTATUS = 'Acceptance Letter Sent'";
                    break;
                case "5":
                    qry = "Where HIRINGSTATUS = 'Accepted'";
                    break;
                case "6":
                    qry = "Where HIRINGSTATUS = 'On Board'";
                    break;
                case "7":
                    qry = "Where HIRINGSTATUS = 'In PS'";
                    break;
                case "8":
                    qry = "";
                    break; 
                default:
                    qry = "";
                    break;
            }

           
            string OracleQry = "Select CANDIDATEID,  INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME,  EMAILADDRESS AS EMAIL, " +
                               " HIRINGSTATUS From EREC_CANDIDATES " +
                               qry +
                               " ORDER BY CANDIDATEID DESC";

            if (myObjs.GetTable(OracleQry).Rows.Count == 0)
            {
                OracleQry = "Select CANDIDATEID,  INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME,  EMAILADDRESS AS EMAIL, " +
                                                " HIRINGSTATUS From EREC_CANDIDATES " +
                                                "ORDER BY CANDIDATEID DESC";
                myMisc.ShowMessage("No Record Found. Showing all records...", this.Page);
            }

            pnlViewCandidates.Visible = true;
            DataTable myDT = myObjs.GetTable(OracleQry);
            gridCandidates.DataSource = myDT;
            gridCandidates.DataBind();
            
            
        }
        
        private void LoadCandidates(string _filter)
        {
            string OracleQry;
            if (_filter == "")
            {
                OracleQry = "Select CANDIDATEID,  INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME,  EMAILADDRESS AS EMAIL, " +
                                 " HIRINGSTATUS From EREC_CANDIDATES " +
                                 "ORDER BY CANDIDATEID DESC";
            }
            else
            {
                OracleQry = "Select CANDIDATEID,  INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME,  EMAILADDRESS AS EMAIL, " +
                                 " HIRINGSTATUS From EREC_CANDIDATES Where CANDIDATEID LIKE '" + _filter + "' " +
                                 "OR LASTNAME LIKE '" + _filter + "%' " +
                                 "OR FIRSTNAME LIKE '" + _filter + "%' " +
                                 "OR EMAILADDRESS LIKE '" + _filter + "%' " +
                                 "OR HIRINGSTATUS LIKE '" + _filter + "%' " +
                                 "ORDER BY CANDIDATEID DESC";
            }
           

            if (myObjs.GetTable(OracleQry).Rows.Count == 0)
            {
                OracleQry = "Select CANDIDATEID,  INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME,  EMAILADDRESS AS EMAIL, " +
                                                " HIRINGSTATUS From EREC_CANDIDATES " +
                                                "ORDER BY CANDIDATEID DESC";
                myMisc.ShowMessage("No Record Found.  Showing all records...", this.Page);
            }

            pnlViewCandidates.Visible = true;
            DataTable myDT = myObjs.GetTable(OracleQry);
            gridCandidates.DataSource = myDT;
            gridCandidates.DataBind();
        }


        protected void FindCandidate_Click(object sender, EventArgs e)
        {
        }

        private void GetData()
        {
            ArrayList arr;
            if (ViewState["SelectedRecords"] != null)
                arr = (ArrayList)ViewState["SelectedRecords"];
            else
                arr = new ArrayList();
           CheckBox chkAll = (CheckBox)gridCandidates.HeaderRow
                               .Cells[0].FindControl("chkAll");
            for (int i = 0; i < gridCandidates.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(gridCandidates.DataKeys[i].Value))
                    {
                        arr.Add(gridCandidates.DataKeys[i].Value);
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)gridCandidates.Rows[i]
                                       .Cells[0].FindControl("chk");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(gridCandidates.DataKeys[i].Value))
                        {
                            arr.Add(gridCandidates.DataKeys[i].Value);
                        }
                    }
                    else
                    {
                        if (arr.Contains(gridCandidates.DataKeys[i].Value))
                        {
                            arr.Remove(gridCandidates.DataKeys[i].Value);
                        }
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
        }


        private void SetData()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)gridCandidates.HeaderRow
                                    .Cells[0].FindControl("chkAll");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            for (int i = 0; i < gridCandidates.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gridCandidates.Rows[i]
                                .Cells[0].FindControl("chk");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(gridCandidates.DataKeys[i].Value);
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
            gridCandidates.AllowPaging = false;
            gridCandidates.DataBind();
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            count = arr.Count;
            for (int i = 0; i < gridCandidates.Rows.Count; i++)
            {
                if (arr.Contains(gridCandidates.DataKeys[i].Value))
                {
                    DeleteRecord(gridCandidates.DataKeys[i].Value.ToString());
                    arr.Remove(gridCandidates.DataKeys[i].Value);
                }
            }
            ViewState["SelectedRecords"] = arr;
            hfCount.Value = "0";
            gridCandidates.AllowPaging = true;
            LoadCandidates(txtCandidateID.Text);
            ShowMessage(count);
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gridCandidates.PageIndex = e.NewPageIndex;
            gridCandidates.DataBind();
            SetData();
        }

        private void DeleteRecord(string CandidateID)
        {
            myObjs.GetConn();
            string query = "delete from EREC_CANDIDATES " +
                            "where CANDIDATEID=:CandidateID";

            OleDbCommand cmd = new OleDbCommand(query, myObjs.MyConn);
            cmd.Parameters.AddWithValue("':CandidateID'", CandidateID);

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


        

        private void GetAccessRole(string EmplID)
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
                    break;
                case "5": // HR Recruiter  
                    break;
                case "6": // PS ENCODER  
                    break;
                case "7": // Requestor  
                    break;
                case "8": // Approver  
                    break;
                default: 
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
