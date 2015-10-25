using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using EHR.Recruitment;
using System.Web.Mail; 

namespace EHR.Recruitment
{
    public partial class AddDetails : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc myMisc = new ObjectsMisc(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            string candidateID = Request.QueryString["candidate"];
            lblCandidateTitle.Text = myQry.GetCandidateName(candidateID);
            if (Page.IsPostBack == false)
            {
                myObjs.GetConn();
                LoadHRData(candidateID);
                LoadDept(candidateID);
                calPosiOnBoardDate.Visible = false;
                calInterviewDate.Visible = false;
                PopulateJobFamily();
                LoadRequestsIDForCandidate(candidateID);
                myObjs.MyConn.Close();
                GetLastInterviewDate(candidateID);
                LoadPlantInTxt(ddlRequestID.SelectedValue);
                LoadJobs();
            }
        }

        //LOAD ALL DATA

        private void LoadDept(string candidateID)
        {
            string ForDept;
            string filterQry = "Select SUITABLEFORDEPT From CANDIDATEINTDETAILS Where CANDIDATEID='" + candidateID + "'";
            OleDbCommand myCDFil = new OleDbCommand(filterQry, myObjs.MyConn);
            OleDbDataReader myRDFil = myCDFil.ExecuteReader();


            if (myRDFil.Read())
            {

                ForDept = Convert.ToString(myRDFil["SUITABLEFORDEPT"]);

            }
            else
            {
                ForDept = "0";
            }

            if (ForDept != "0")
            {
                ddlDepartment.SelectedValue = ForDept;
            }
            DataTable myDt = myObjs.GetTable(myQry.GetDeptID());
            ddlDepartment.DataSource = myDt;
            ddlDepartment.DataValueField = "DEPTID";
            ddlDepartment.DataTextField = "DESCR";
            ddlDepartment.DataBind();

        }


        private void GetLastInterviewDate(string candidateID)
        {
            myObjs.GetConn(); 
            // default date format is 
            string OracleQry = "select TO_CHAR(INTERVIEWDATE, 'dd.MM.yyyy') As INTDATE  from INTERVIEWINVITE " +
                "Where INTERVIEWINVID = (Select max(INTERVIEWINVID) from INTERVIEWINVITE)  AND CANDIDATEID='" + candidateID + "' order by INTERVIEWDATE DESC";

            OleDbCommand mycmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();
            if (myrd.Read())
            {
                txtInterviewDate.Text = Convert.ToString(myrd["INTDATE"]);

            }
            else
            {
                txtInterviewDate.Text = "";
            }
             
        }

        private void LoadHRData(string candidateID)
        {
            string IntrvBy;
            string filterQry = "Select INTERVIEWEDBY From CANDIDATEINTDETAILS Where CANDIDATEID='" + candidateID + "'";
            OleDbCommand myCDFil = new OleDbCommand(filterQry, myObjs.MyConn);
            OleDbDataReader myRDFil = myCDFil.ExecuteReader();


            if (myRDFil.Read())
            {

                IntrvBy = Convert.ToString(myRDFil["INTERVIEWEDBY"]);

            }
            else
            {
                IntrvBy = "0";
            }

            if (IntrvBy != "0")
            {
                ddlInterviewedBy.SelectedValue = IntrvBy;
            }

            string OracleQry = "Select EMPLID, NAME_A From PS.PS_SUB_WCZ_AT_VW_A WHERE DEPTID = 'MD0H00' " +
                "AND COMP_DESCR_A = 'Active'";

            DataTable myDT = myQry.GetTable(OracleQry);

            ddlInterviewedBy.DataSource = myDT;
            ddlInterviewedBy.DataValueField = "EMPLID";
            ddlInterviewedBy.DataTextField = "NAME_A";
            ddlInterviewedBy.DataBind();

            ddlInterviewedBy.Items.Insert(0, new ListItem("-- Select One --", "0"));


        }

        //calendar objects


        protected void InterviewDate_Click(object sender, ImageClickEventArgs e)
        {
            if (calInterviewDate.Visible == false)
            {
                calInterviewDate.Visible = true;
            }
            else
            {
                calInterviewDate.Visible = false;

            }
        }

        protected void ViewOnBoardCal_OnClick(object sender, ImageClickEventArgs e)
        {
            if (calPosiOnBoardDate.Visible == false)
            {
                calPosiOnBoardDate.Visible = true;
            }
            else
            {
                calPosiOnBoardDate.Visible = false;

            }
        }

        protected void BtnAddClick(object sender, EventArgs e)
        {
            if (listFromJobs.SelectedIndex == -1)
            {
                return;
            }

            ListItem item = new ListItem();
            item.Value = listFromJobs.SelectedItem.Value;
            item.Text = listFromJobs.SelectedItem.Text;

            listToJobs.Items.Add(item);
            listFromJobs.Items.Remove(item);
        }

        protected void BtnRemoveClick(object sender, EventArgs e)
        {
            if (listToJobs.SelectedIndex == -1)
            {
                return;
            }

            ListItem item = new ListItem();
            item.Value = listFromJobs.SelectedItem.Value;
            item.Text = listFromJobs.SelectedItem.Text;

            listFromJobs.Items.Add(item);
            listToJobs.Items.Remove(item);
        }

        protected void CalInterviewDate_Selection_Change(object sender, EventArgs e)
        {
            foreach (DateTime day in calInterviewDate.SelectedDates)
            {
                txtInterviewDate.Text = "";
                txtInterviewDate.Text += day.Date.ToString("MM/dd/yyyy");
                calInterviewDate.Visible = false;
            }
        }
        protected void CalPosiOnBoardDate_Selection_Change(object sender, EventArgs e)
        {
            foreach (DateTime day in calPosiOnBoardDate.SelectedDates)
            {
                txtOnBoardDate.Text = "";
                txtOnBoardDate.Text += day.Date.ToString("yyyy/MM/dd");
                calPosiOnBoardDate.Visible = false;
            }
        }

        //LISTBOX ENTITIES
        protected void DdlJobFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            string OraJobs;
            if (ddlJobFamily.SelectedValue == "All")
            {
                OraJobs = "SELECT DISTINCT(JOB_CODE), JOB_BUSI_TITLE From PS.PS_JOB";
            }
            else
            {
                OraJobs = "SELECT DISTINCT(JOB_CODE), JOB_BUSI_TITLE From PS.PS_JOB Where JOB_FAMILY='" + ddlJobFamily.SelectedValue.ToString() + "'";
            }

            DataTable myLS = myObjs.GetTable(OraJobs);
            listFromJobs.DataSource = myLS;
            listFromJobs.DataTextField = "JOB_BUSI_TITLE";
            listFromJobs.DataValueField = "JOB_CODE";
            listFromJobs.DataBind();
        }

        private void LoadJobs()
        {
            string OraJobs = "SELECT DISTINCT(JOB_CODE), JOB_BUSI_TITLE From PS.PS_JOB";

            DataTable myLS = myObjs.GetTable(OraJobs);
            listFromJobs.DataSource = myLS;
            listFromJobs.DataTextField = "JOB_BUSI_TITLE";
            listFromJobs.DataValueField = "JOB_CODE";
            listFromJobs.DataBind();
        }

        //LOAD DATA to right listbox
        private void LoadToJobsExists()
        {
            string id = Request.QueryString["candidate"];
            string orajobs = "Select POSITION From CANDIDATEJOBS Where CANDIDATEID = '" + id + "'";
            myObjs.GetConn();
            DataTable mdt = myObjs.GetTable(orajobs);
            listToJobs.DataSource = mdt;
            listToJobs.DataTextField = "POSITION";
            listToJobs.DataValueField = "POSITION";
            listToJobs.DataBind();
        }

        private void LoadRequestsIDForCandidate(string candidateID)
        {
            string OracReq = "Select distinct(REQUESTID) From INTERVIEWINVITE Where CandidateID = '" + candidateID + "' ";

            DataTable myDT = myObjs.GetTable(OracReq);
            ddlRequestID.DataSource = myDT;
            ddlRequestID.DataTextField = "REQUESTID";
            ddlRequestID.DataValueField = "REQUESTID";
            ddlRequestID.DataBind();
        }

        //Save Interview Details
        protected void SavedInterviewInfo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                myObjs.GetConn();

                string id = Request.QueryString["candidate"];
                string requestID = ddlRequestID.SelectedValue.ToString();

                string OraUpdateStats = "Update CANDIDATES set HIRINGSTATUS = 'Interviewed' " +
                                    "Where CANDIDATEID = '" + id + "'";
                OleDbCommand mycmdupdated = new OleDbCommand(OraUpdateStats, myObjs.MyConn);
                mycmdupdated.ExecuteNonQuery();

                string OracleCand = "Insert Into CANDIDATEINTDETAILS (REQUESTID, CANDIDATEID, POSIONBOARDDATE, " +
                                "SALARYEXP, SUITABLEFORDEPT, INTERVIEWEDBY, INTERVIEWDATE, REMARKS, PLANT) VALUES (" +
                                ":requestID, :candidateID, :onboarddate, :salaryexp, :fordept, :interviewedby, " +
                                "to_date(:interviewdate,'dd.MM.yyyy'), :remarks, :plant)";


                myObjs.GetConn();
                OleDbCommand myCmd = new OleDbCommand(OracleCand, myObjs.MyConn);
                myCmd.Parameters.AddWithValue("':requestID'", requestID);
                myCmd.Parameters.AddWithValue("':candidateID'", id);
                myCmd.Parameters.AddWithValue("':onboarddate'", txtOnBoardDate.Text);
                myCmd.Parameters.AddWithValue("':salaryexp'", txtSalaryExp.Text);
                myCmd.Parameters.AddWithValue("':fordept'", ddlDepartment.SelectedValue.ToString());
                myCmd.Parameters.AddWithValue("':interviewedby'", ddlInterviewedBy.SelectedValue.ToString());
                myCmd.Parameters.AddWithValue("':interviewdate'", txtInterviewDate.Text);
                myCmd.Parameters.AddWithValue("':remarks'", txtRemarks.Text);
                myCmd.Parameters.AddWithValue("':plant'", ddlPlant.SelectedValue.ToString());
                try
                {
                    myCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    myMisc.ShowMessage(ex.Message, this.Page);
                }

                //insert data from listbox

                for (int i = 0; i < listToJobs.Items.Count; i++)
                {
                    string v = listToJobs.Items[i].Value;

                    string OraExists = "Select count(POSITION) as COUNT from CANDIDATEJOBS Where POSITION = '" + v + "' " +
                                       "AND CANDIDATEID = '" + id + "'";
                    OleDbCommand myExists = new OleDbCommand(OraExists, myObjs.MyConn);
                    int numRows = Convert.ToInt32(myExists.ExecuteScalar());
                    if (numRows == 0)
                    {
                        string OracleQry = "Insert into CANDIDATEJOBS VALUES " +
                                           "(CANDIDATEJOBSID_SEQ.NextVal, '" + id + "', '" + v + "')";
                        OleDbCommand cmdJobs = new OleDbCommand(OracleQry, myObjs.MyConn);
                        try
                        {
                            cmdJobs.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            myMisc.ShowMessage(ex.Message, this.Page);
                        }
                    }

                }
                myObjs.MyConn.Close();
                Response.Redirect("~/Recruitment/ViewCandidate.aspx?action=view&candidate=" + id);
            }
        }

        private void PopulateJobFamily()
        {
            DataTable myDT = myObjs.GetTable(myQry.GetJobFamily());
            ddlJobFamily.DataSource = myDT;
            ddlJobFamily.DataTextField = "JOB_FAMILY_DESC";
            ddlJobFamily.DataValueField = "JOB_FAMILY";
            ddlJobFamily.DataBind();
            ddlJobFamily.Items.Insert(0, new ListItem("-- All --", "All"));
        }

        protected void ViewCandidate_Click(object sender, EventArgs e)
        {
            string candidate = Request.QueryString["candidate"];
            Session["candidateID"] = candidate;

            Response.Redirect("~/Recruitment/ViewCandidate.aspx?action=view&candidate=" + candidate);
        }


        private void LoadPlantInTxt(string RequestID)
        {
            string OracleQry = "Select PLANT from REQUESTS Where REQUEStID = '" + ddlRequestID.SelectedValue + "'";
            OleDbCommand mycmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();
            if (myrd.Read())
            {
                ddlPlant.SelectedValue = Convert.ToString(myrd["PLANT"]);
            }
            else
            {
                ddlPlant.SelectedValue = "";
            }

        }

       
    }
}
