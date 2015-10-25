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
using System.Text;
using ehr_email;
namespace EHR.Main
{
    public partial class InterviewResult : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc myMisc = new ObjectsMisc();
        DBModel _db = new DBModel();
        MiscModel _misc = new MiscModel();
        CandidateModel _cand = new CandidateModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            string candidateID = (string)(Session["candidate"]);
            btnBackToProfile.PostBackUrl = "~/Main/ViewCandidate.aspx?candidate=" + candidateID;
            lblCandidateTitle.Text = myQry.GetCandidateName(candidateID);
            if (Page.IsPostBack == false)
            {
                myObjs.GetConn();
                 
                LoadDept(candidateID);
                calPosiOnBoardDate.Visible = false;
                calInterviewDate.Visible = false; 
                LoadRequestsIDForCandidate(candidateID);
                myObjs.MyConn.Close();
                GetLastInterviewDate(candidateID);
                LoadPlantInTxt(ddlRequestID.SelectedValue);
                txtInterviewedBy.Text = InterviewedBy(ddlRequestID.SelectedValue);
                LoadJobs(ddlDepartment.SelectedValue);
                
            }
        }

        //LOAD ALL DATA

        protected void DdlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            LoadJobs(ddlDepartment.SelectedValue); 
        }

        private void LoadDept(string candidateID)
        {
            
            DataTable myDt = myObjs.GetTable(myQry.GetDeptID());
            ddlDepartment.DataSource = myDt;
            ddlDepartment.DataValueField = "DEPTID";
            ddlDepartment.DataTextField = "DESCR";
            ddlDepartment.DataBind();

        }

        private string InterviewedBy(string requestid)
        {
            _db.GetConn();
            string interviewedBy; 
            string sql = "SELECT APPEMPLID FROM APPROVALTRANSACTION WHERE APPROVERVALUE = 'HR_RECRUITER' AND " +
                        "REQID = '" + requestid + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                interviewedBy = Convert.ToString(rd["APPEMPLID"]);
            }
            else
            {
                interviewedBy = "";
            }

            return interviewedBy; 
        }


        private void GetLastInterviewDate(string candidateID)
        {
            myObjs.GetConn();
            // default date format is 
            string OracleQry = "select TO_CHAR(INTERVIEWDATE, 'dd.MM.yyyy') As INTDATE  from EREC_INTERVIEWINVITE " +
                "Where INTERVIEWINVID = (Select max(INTERVIEWINVID) from EREC_INTERVIEWINVITE)  AND CANDIDATEID='" + candidateID + "' order by INTERVIEWDATE DESC";

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
            if (listFromJobs.SelectedItem != null)
            {
                ListItem li = listFromJobs.SelectedItem;
                listFromJobs.Items.Remove(li);
                li.Selected = false;
                listToJobs.Items.Add(li);
            }

           
        }

        protected void BtnRemoveClick(object sender, EventArgs e)
        {
            if (listToJobs.SelectedItem != null)
            {
                ListItem li = listToJobs.SelectedItem;
                listToJobs.Items.Remove(li);
                li.Selected = false;
                listFromJobs.Items.Add(li);
            }
        }

        protected void CalInterviewDate_Selection_Change(object sender, EventArgs e)
        {
            foreach (DateTime day in calInterviewDate.SelectedDates)
            {
                txtInterviewDate.Text = "";
                txtInterviewDate.Text += day.Date.ToString("dd.MM.yyyy");
                calInterviewDate.Visible = false;
            }
        }
        protected void CalPosiOnBoardDate_Selection_Change(object sender, EventArgs e)
        {
            foreach (DateTime day in calPosiOnBoardDate.SelectedDates)
            {
                txtOnBoardDate.Text = "";
                txtOnBoardDate.Text += day.Date.ToString("dd.MM.yyyy");
                calPosiOnBoardDate.Visible = false;
            }
        }

        

        private void LoadJobs(string dept)
        {
            string deptid = dept.Substring(0, 4);
            string sql = "SELECT JOB_BUSI_TITLE FROM PS.PS_JOB";

            DataTable dt = _db.GetTable(sql);

            listFromJobs.DataSource = dt;
            listFromJobs.DataValueField = "JOB_BUSI_TITLE";
            listFromJobs.DataTextField = "JOB_BUSI_TITLE";
            listFromJobs.DataBind();        

 
        }


        private void LoadRequestsIDForCandidate(string candidateID)
        {
            string OracReq = "Select distinct(REQUESTID) From EREC_INTERVIEWINVITE Where CandidateID = '" + candidateID + "' ";

            DataTable myDT = myObjs.GetTable(OracReq);
            ddlRequestID.DataSource = myDT;
            ddlRequestID.DataTextField = "REQUESTID";
            ddlRequestID.DataValueField = "REQUESTID";
            ddlRequestID.DataBind();
        }

        private void SaveInterviewInfo()
        {
            myObjs.GetConn();

            string id = (string)(Session["candidate"]);
            string requestID = ddlRequestID.SelectedValue.ToString();

            string OraUpdateStats = "Update EREC_CANDIDATES set HIRINGSTATUS = 'Interviewed' " +
                                "Where CANDIDATEID = '" + id + "'";
            OleDbCommand mycmdupdated = new OleDbCommand(OraUpdateStats, myObjs.MyConn);
            mycmdupdated.ExecuteNonQuery();

            string OracleCand = "Insert Into EREC_CANDIDATEINTDETAILS (REQUESTID, CANDIDATEID, POSIONBOARDDATE, " +
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
            myCmd.Parameters.AddWithValue("':interviewedby'", txtInterviewedBy.Text);
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

                string OraExists = "Select count(POSITION) as COUNT from EREC_CANDIDATEJOBS Where POSITION = '" + v + "' " +
                                   "AND CANDIDATEID = '" + id + "'";
                OleDbCommand myExists = new OleDbCommand(OraExists, myObjs.MyConn);
                int numRows = Convert.ToInt32(myExists.ExecuteScalar());
                if (numRows == 0)
                {
                    string OracleQry = "Insert into EREC_CANDIDATEJOBS VALUES " +
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

            myMisc.ShowMessage("Successfully saved candidate data: " + _cand.GetCandidateName(id), this.Page);
            Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + id);
    
            
        }

        //Save Interview Details
        protected void SavedInterviewInfo_Click(object sender, EventArgs e)
        {
            string candidateID = (string)(Session["candidate"]);
            if (Page.IsValid)
            {
                SaveInterviewInfo();
            }
        }

         

        protected void ViewCandidate_Click(object sender, EventArgs e)
        {
            string candidate = Request.QueryString["candidate"];
            Session["candidateID"] = candidate;

            Response.Redirect("~/Main/ViewCandidate.aspx?action=view&candidate=" + candidate);
        }


        private void LoadPlantInTxt(string RequestID)
        {
            string OracleQry = "Select PLANT from EREC_REQUESTS Where REQUEStID = '" + ddlRequestID.SelectedValue + "'";
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

        protected void RejectCandidateInfo_Click(object sender, EventArgs e)
        {
             string candidateID = (string)(Session["candidate"]);
             SaveInterviewInfo();
             RejectedCandiateNotification(candidateID);
             
             string sql = "UPDATE EREC_CANDIDATES SET HIRINGSTATUS = 'Rejected' WHERE CANDIDATEID = '" + candidateID + "'";
             _db.GetConn();
             OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
             cmd.ExecuteNonQuery();
             _db.conn.Close();

             myMisc.ShowMessage("Successfully rejected: " + _cand.GetCandidateName(candidateID), this.Page);
             Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + candidateID);
        }

        protected void SaveAndSendLetter_Click(object sender, EventArgs e)
        {
            string candidateID = (string)(Session["candidate"]);
            SaveInterviewInfo();

            myMisc.ShowMessage("Successfully sent letter to : " + _cand.GetCandidateName(candidateID), this.Page);
            Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/HireCandidate.aspx?candidate=" + candidateID);
             
        }

        private void RejectedCandiateNotification(string candidate)
        {
            if (_misc.IsEmailNotificationTest("Notify_Candidate_Reject", "WCZ Recruitment: " + candidate, candidate) == false)
            {
                string mail_type = "Notify_Candidate_Reject";
                string sender_ = "noreply@wistron.com ";

                string recipient = _cand.GetCandidateEmailAddress(candidate);
                string recipient_name = _cand.GetCandidateName(candidate);
                string cc = "";
                string subject = "WCZ Recruitment";


                string parameters = "Applied Position: <strong>" + _cand.GetCandidateAppliedPosition(ddlRequestID.SelectedValue) + "</strong>";

                try
                {
                    Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                }
                catch (Exception ex)
                {
                    string errormsg = ex.Message;
                }
            }
        }
    }
}
