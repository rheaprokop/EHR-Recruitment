using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mail;
using EHR.Model;
using System.Text;
using ehr_email;

namespace EHR.Main
{
    public partial class HireCandidate : System.Web.UI.Page
    {
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc myMisc = new ObjectsMisc();
        CandidateModel _cand = new CandidateModel();
        MiscModel _misc = new MiscModel();
        DBModel _db = new DBModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            string candidate = (string)(Session["candidate"]);
            btnBackToProfile.PostBackUrl = "~/Main/ViewCandidate.aspx?candidate=" + candidate;

            if (Page.IsPostBack == false)
            {
                calOnBoardDate.Visible = false;
                txtCandidateName.Text = myQry.GetCandidateName(candidate);
                PopulateRequestCtrol(candidate);
                PopulateJobsCtrol(candidate);
                LoadDeptData(candidate);


                StringBuilder sb = new StringBuilder();

                sb.Append("Job Position: <br />");
                sb.Append("The Start Salary will be: _____ CZK / gross in total monthly <br /> " +
                        "In case of wage or bonus increase after probation period, the increase is " +
                        "effective from the first calendar day of the subsequent month after the end " +
                        "of probation period. ");
                sb.Append("On board date is on ____  at ___ ");
                sb.Append("Address: ______");

                string parameters = sb.ToString();
                linkToOutlook.NavigateUrl = "mailto:rhea_prokop@wistron.com?body=" + parameters;
            }
        } 

        protected void OnBoardDate_Click(object sender, ImageClickEventArgs e)
        {
            if (calOnBoardDate.Visible == false)
            {
                calOnBoardDate.Visible = true;
            }
            else
            {
                calOnBoardDate.Visible = false;

            }
        }

        protected void CalendarOnBoardDate_Selection_Change(object sender, EventArgs e)
        {
            foreach (DateTime day in calOnBoardDate.SelectedDates)
            {
                txtOnBoardDate.Text = "";
                txtOnBoardDate.Text += day.Date.ToString("dd/MM/yyyy");
                calOnBoardDate.Visible = false;
            }
        }

        private void PopulateRequestCtrol(string candidateID)
        {
            string OraQry = "Select Distinct (REQUESTID) From EREC_CANDIDATEINTDETAILS Where CANDIDATEID='" + candidateID + "'";

            DataTable myDT = myQry.GetTable(OraQry);
            ddlForRequest.DataSource = myDT;
            ddlForRequest.DataTextField = "REQUESTID";
            ddlForRequest.DataValueField = "REQUESTID";
            ddlForRequest.DataBind();
            ddlForRequest.Items.Insert(0, new ListItem("-- Select One --", ""));
        }

        private void PopulateJobsCtrol(string candidateID)
        {
            string OraQry = "Select POSITION FROM EREC_CANDIDATEJOBS Where CANDIDATEID='" + candidateID + "'";

            DataTable myDT = myQry.GetTable(OraQry);

            ddlJobPosition.DataSource = myDT;
            ddlJobPosition.DataTextField = "POSITION";
            ddlJobPosition.DataValueField = "POSITION";
            ddlJobPosition.DataBind();
            ddlJobPosition.Items.Insert(0, new ListItem("-- Select One --", ""));

        }

        protected void SendEmailToCandidate_Click(object sender, EventArgs e)
        {

            SendEmailToCandidate(); 
        }

        private void SendEmailToCandidate()
        {
            try
            {

                string candidate = (string)(Session["candidate"]);

                //email acceptance letter to candidate
                SendAcceptanceLetter(candidate);

                DateTime MyDate = DateTime.Now;
                string bd = txtOnBoardDate.Text + " " + ddlToTime.SelectedValue.ToString(); // 25/12/2012 21:02:44

                string onboard = txtOnBoardDate.Text = txtOnBoardDate.Text + " " + ddlToTime.SelectedValue;
                _db.GetConn();
                string UpdateCand = "Update EREC_CANDIDATES Set HIRINGSTATUS = 'Acceptance Letter Sent', OFFERSENT = 'YES',  " +
                    " OFFERFORREQUESTID = '" + ddlForRequest.SelectedValue + "', " +
                    "OFFERPOSITION = '" + ddlJobPosition.SelectedValue + "', " +
                    "OFFERONBOARDDATE = to_date('" + bd + "', 'dd/mm/yyyy hh24:mi:ss'), " +
                    "OFFERDEPT = '" + ddlForDepartment.SelectedValue + "', " +
                    "OFFERADDRESS = '" + ddlWorkPlace.SelectedValue + "' " +
                    "Where CANDIDATEID = '" + candidate + "'";

                OleDbCommand myComand = new OleDbCommand(UpdateCand, _db.conn);
                myComand.ExecuteNonQuery();


                myMisc.ShowMessage("Successfully sent acceptance email to candidate: " + _cand.GetCandidateName(candidate), this.Page);
                Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + candidate);


            }
            catch (Exception ex)
            {
            }
        }
        
        private void LoadDeptData(string id)
        {
            string OracleQry = "Select Distinct(SUITABLEFORDEPT) From EREC_CANDIDATEINTDETAILS Where CANDIDATEID = '" + id + "'";
            DataTable myDT = myQry.GetTable(OracleQry);

            ddlForDepartment.DataSource = myDT;
            ddlForDepartment.DataTextField = "SUITABLEFORDEPT";
            ddlForDepartment.DataValueField = "SUITABLEFORDEPT";
            ddlForDepartment.DataBind();
        }

        private void SendAcceptanceLetter(string candidate)
        {
            if (_misc.IsEmailNotificationTest("Candidate_Job_Acceptance", "Acceptance Letter: " + candidate, candidate) == false)
            {
                string mail_type = "Candidate_Job_Acceptance";
                string sender_ = "noreply@wistron.com ";

                string recipient = _cand.GetCandidateEmailAddress(candidate);
                string recipient_name = _cand.GetCandidateName(candidate);
                string cc = "";
                string subject = "WCZ Wistron - Acceptance Letter";

                StringBuilder sb = new StringBuilder();

                sb.Append("Job Position: <strong>" + ddlJobPosition.SelectedValue + "</strong>;");
                sb.Append("The Start Salary will be: <strong>" + txtSalary.Text + " CZK / gross in total monthly</strong>. " + 
                        "In case of wage or bonus increase after probation period, the increase is " +
                        "effective from the first calendar day of the subsequent month after the end " + 
                        "of probation period.; ");
                sb.Append("On board date is on <strong>" + txtOnBoardDate.Text + "</strong> at <strong>" + ddlToTime.SelectedValue + "</strong>;");
                sb.Append("Address: <strong>" + ddlWorkPlace.SelectedValue + "</strong>;"); 

                string parameters = sb.ToString();

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
