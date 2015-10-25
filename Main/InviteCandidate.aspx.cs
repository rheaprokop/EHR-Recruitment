using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Text;
using EHR.Model;

using ehr_email;
using System.Net.Mail;
using System.Configuration;

namespace EHR.Main
{
    public partial class InviteCandidate : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQueries = new QueryOleDB();
        EmailBody myEmail = new EmailBody();
        ObjectsMisc myMisc = new ObjectsMisc();
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel();
        MiscModel _misc = new MiscModel();
        CandidateModel _cand = new CandidateModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            string RequestID;
            string RequestFromSession = (string)(Session["RequestID"]);
            
            string candidateID = (string)(Session["candidate"]);
            btnBackToProfile.PostBackUrl = "~/Main/ViewCandidate.aspx?candidate=" + candidateID;
            if (candidateID == "")
            {
                candidateID = Request.QueryString["candidate"];
            } 
            RequestID = Request.QueryString["request"]; 
            if (RequestID == "")
            {
                RequestID = RequestFromSession;
            }

            txtInterviewRequestID.Text = RequestID;

            txtOtherInterviewers.Attributes.Add("onfocus", "this.value=''"); 


            if (Page.IsPostBack == false)
            {
                calInterviewDate.Visible = false;
                LoadCandData(candidateID);

                if (RequestID != "")
                {
                    LoadRecruiter(RequestID);
                    LoadManager(RequestID);
                }
                PopulateRequests();
            }
        }

        private void LoadCandData(string id)
        {

            string OracleCand = "Select LASTNAME, FIRSTNAME, EMAILADDRESS " +
                                "from EREC_CANDIDATES " +
                                "where CANDIDATEID = '" + id + "'";
            myObjs.GetConn();
            OleDbCommand myCmd = new OleDbCommand(OracleCand, myObjs.MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            if (myRD.Read())
            {
                string Lastname = Convert.ToString(myRD["LASTNAME"]);
                string Firstname = Convert.ToString(myRD["FIRSTNAME"]);
                txtCandidateName.Text = Lastname + "," + Firstname;
                hiddenCandID.Value = id;
            }
        }

        private void LoadRecruiter(string RequestID)
        {
            _db.GetConn();
            string sql = "SELECT APPEMPLID FROM APPROVALTRANSACTION WHERE REQID = '" + RequestID + "' AND APPROVERVALUE = 'HR_RECRUITER'";
            try
            {
                OleDbCommand cmdRecruiter = new OleDbCommand(sql, _db.conn);
                OleDbDataReader _rd = cmdRecruiter.ExecuteReader();

                if (_rd.Read())
                {
                    txtRecruiter.Text = Convert.ToString(_rd["APPEMPLID"]); 
                }
                else
                {
                    txtRecruiter.Text = "No Recruiter Assigned";
                }

                _db.conn.Close();
            }
            catch (Exception ex)
            {
            }
          
        } 

        private void LoadManager(string requestID)
        {
            string OracleQry = "Select REQUESTOR From EREC_REQUESTS Where REQUESTID='" + requestID + "'";
            OleDbCommand myCMD = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRD = myCMD.ExecuteReader();
            if (myRD.Read())
            {
                string REQUESTOR = Convert.ToString(myRD["REQUESTOR"]);

                string OracMngr = "Select MANAGER_ID From PS.PS_SUB_WCZ_AT_VW_A Where EMPLID='" + REQUESTOR + "'";
                OleDbCommand myCMngr = new OleDbCommand(OracMngr, myObjs.MyConn);
                OleDbDataReader myRDrr = myCMngr.ExecuteReader();
                if (myRDrr.Read())
                {
                    //use during UAT
                    string ManagerID = Convert.ToString(myRDrr["MANAGER_ID"]);

                    //use during SIT
                    txtManagerName.Text = myQueries.GetFullName(ManagerID);
                    hiddenManagerID.Value = ManagerID;
                }
            }

        }

        private void PopulateRequests()
        {

            string OracleQry = "SELECT RequestID, requestor, jobtitle, requiredperson as required, hired, " +
                "deptcode as department, result as status from EREC_REQUESTS " +
                "where result = 'Approved' OR result = 'RECRUITMENT' AND DELETED = '0'";


            DataTable myRequests = myQueries.GetTable(OracleQry);
            gridAllRequest.DataSource = myRequests;
            gridAllRequest.DataBind();
            myObjs.MyConn.Close();

        }

        protected void InterviewDate_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DateTime day in calInterviewDate.SelectedDates)
            {
                txtInterviewDate.Text = "";
                txtInterviewDate.Text += day.ToString("dd.MM.yyyy");
                calInterviewDate.Visible = false;

            }
        }

        protected void InterviewDate_OnClick(object sender, ImageClickEventArgs e)
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
        protected void SendInterviewInv_Click(object sender, EventArgs e)
        {

            string candidateID = hiddenCandID.Value; 

            DateTime MyDate = DateTime.Now;
            string bd = txtInterviewDate.Text + " " + ddlToTime.SelectedValue.ToString(); // 25/12/2012 21:02:44


            myObjs.GetConn();
            string OracleIntInv = "Insert into EREC_INTERVIEWINVITE (INTERVIEWINVID,  " +
                                  " CANDIDATEID, HRRECRUITER, DEPTMNGR, REQUESTID, INTERVIEWDATE)" +
                                  "Values (INTERVIEWINV_SEQ.NextVal, '" + candidateID + "', " +
                                  "'" + txtRecruiter.Text + "', " +
                                  "'" + hiddenManagerID.Value + "', " +
                                  "'" + txtInterviewRequestID.Text + "', to_date('" + bd + "','dd.MM.yyyy hh24:mi:ss'))";
            OleDbCommand myCMDInv = new OleDbCommand(OracleIntInv, myObjs.MyConn);
            
                myCMDInv.ExecuteNonQuery();
 
                //send email to candidate 
                InviteCandiateForInterview(candidateID);

                // send email to requestor
                InviteRequestorForInterview(candidateID); 

                //send email to recruiter
                InviteRecruitersForInterview(candidateID);
               
                // send email to manager
                InviteManagerForInterview(candidateID);

                // send email to other additional employees invited
                InviteOthersForInterview(candidateID); 

                string UpdateCandStatus = "Update EREC_CANDIDATES Set HIRINGSTATUS = 'For Interview' " +
                                        "Where CANDIDATEID = '" + candidateID + "'";
                OleDbCommand myStats = new OleDbCommand(UpdateCandStatus, myObjs.MyConn);
                myStats.ExecuteNonQuery();
                 

                myMisc.ShowMessage("Successfully invited: " + txtCandidateName.Text, this.Page);
                Response.AddHeader("REFRESH", "1;URL=http://" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + candidateID);
             

        }

        protected void SaveInvitationOnly_Click(object sender, EventArgs e)
        {
            string candidateID = hiddenCandID.Value;

            string UserID = (string)(Session["UserID"]);

            DateTime MyDate = DateTime.Now;
            string bd = txtInterviewDate.Text + " " + ddlToTime.SelectedValue.ToString(); // 25/12/2012 21:02:44


            myObjs.GetConn();
            string OracleIntInv = "Insert into EREC_INTERVIEWINVITE (INTERVIEWINVID,  " +
                                  " CANDIDATEID, HRRECRUITER, DEPTMNGR, REQUESTID, INTERVIEWDATE)" +
                                  "Values (INTERVIEWINV_SEQ.NextVal, '" + candidateID + "', " +
                                  "'" + txtRecruiter.Text + "', " +
                                  "'" + hiddenManagerID.Value + "', " +
                                  "'" + txtInterviewRequestID.Text + "', to_date('" + bd + "','dd/MM/yyyy hh24:mi:ss'))";
            OleDbCommand myCMDInv = new OleDbCommand(OracleIntInv, myObjs.MyConn);
            try
            {

                myCMDInv.ExecuteNonQuery();  
                

                string UpdateCandStatus = "Update EREC_CANDIDATES Set HIRINGSTATUS = 'For Interview' " +
                                        "Where CANDIDATEID = '" + candidateID + "'";
                OleDbCommand myStats = new OleDbCommand(UpdateCandStatus, myObjs.MyConn);
                myStats.ExecuteNonQuery();
                btnSend.Enabled = false;
                myObjs.MyConn.Close();

                myMisc.ShowMessage("Successfully invited: " + txtCandidateName.Text, this.Page);
                Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + candidateID);



            }
            catch (Exception ex)
            {
                myMisc.ShowMessage(ex.Message, this.Page);
            }
        }

        private void InviteCandiateForInterview(string candidate)
        {
            if (_misc.IsEmailNotificationTest("Candidate_Interview_Invite", "Interview Invitation For: " + candidate, candidate) == false)
            {
                string mail_type = "Candidate_Interview_Invite";
                string sender_ = "noreply@wistron.com ";

                string recipient = _cand.GetCandidateEmailAddress(candidate);
                string recipient_name = "" ;
                string cc = "";
                string subject = "WCZ Recruitment - Pozvánka na pohovor";

                StringBuilder sb = new StringBuilder();

                sb.Append("Předmět:  Pozvánka na pohovor. Pozice: <strong>" + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + "</strong>;"); // get candidate position
                sb.Append("Vážený/ á  pane/paní : <strong>" + txtCandidateName.Text + "</strong>;"); // get candidate name
                sb.Append("Na základě předchozího telefonického rozhovoru bych Vás " +
                        " ráda pozvala na pracovní pohovor na pozici: <strong>" + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + "</strong>;"); // get candidate position
                sb.Append("Pohovor se bude konat v/ve : <strong> " + txtInterviewDate.Text + " v " + ddlToTime.SelectedValue + "</strong>;"); // get interview time 
                sb.Append("Kontaktní osoba: <strong>" + GetRecruitersName() + "</strong>;");
                sb.Append("Adresa: <strong>" + ddlWorkPlace.SelectedValue + "</strong>;");

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

        private void InviteRequestorForInterview(string candidate)
        {

            if (_misc.IsEmailNotificationTest("Interview_Invite", "Interview Invitation For: " + candidate, candidate) == false)
            {
                string mail_type = "Interview_Invite";
                string sender_ = "noreply@wistron.com ";

                string recipient = _ps.GetEmplEmailAdrress(_misc.GetRequestor(txtInterviewRequestID.Text, "EREC_REQUESTS"));
                string recipient_name = _ps.GetName(_misc.GetRequestor(txtInterviewRequestID.Text, "EREC_REQUESTS")); 
                string cc = "";
                string subject = "WCZ Recruitment Requestor: Interview for position " + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + " was arranged, please attend";

                string parameters = InterviewDetails();

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

        private void InviteManagerForInterview(string candidate)
        {

            if (_misc.IsEmailNotificationTest("Interview_Invite", "Interview Invitation For: " + candidate, candidate) == false)
            {
                string mail_type = "Interview_Invite";
                string sender_ = "noreply@wistron.com ";

                //string recipient = hiddenManagerID.Value;
                string recipient = "rhea_prokop@wistron.com";
                string recipient_name = _ps.GetName(recipient);
                string cc = "";
                string subject = "Dept Manager: Interview for position " + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + " was arranged, please attend";

                string parameters = InterviewDetails();

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

        private void InviteRecruitersForInterview(string candidate)
        {
            if (_misc.IsEmailNotificationTest("Interview_Invite", "Interview Invitation For: " + candidate, candidate) == false)
            {
                string mail_type = "Interview_Invite";
                string sender_ = "noreply@wistron.com ";

                string recipient = GetRecruitersEmail(); // recruiters email address
                string recipient_name = GetRecruitersName();// recruiters name
                string cc = "";
                string subject = "HR Recruiter: Interview for position " + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + " was arranged, please attend";

                string parameters = InterviewDetails();

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

        private void InviteOthersForInterview(string candidate)
        {
            if(txtOtherInterviewers.Text != "")
            {
                if (_misc.IsEmailNotificationTest("Interview_Invite", "Interview Invitation For: " + candidate, candidate) == false)
                {
                    string mail_type = "Interview_Invite";
                    string sender_ = "noreply@wistron.com ";

                    string recipient = GetOthersEmail(); // recruiters email address
                    string recipient_name = GetOthersName();// recruiters name
                    string cc = "";
                    string subject = "Interview Panel: Interview for position " + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + " was arranged, please attend";

                    string parameters = InterviewDetails();

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

        private string InterviewDetails()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Please be informed, that interview with <strong>" + txtCandidateName.Text +
                    "</strong> for position <strong>" + _cand.GetCandidateAppliedPosition(txtInterviewRequestID.Text) + "</strong> was arranged.;");
            sb.Append("Date: <strong>" + txtInterviewDate.Text + "</strong> ;");
            sb.Append("Time: <strong>" + ddlToTime.SelectedValue + "</strong>;");
            sb.Append("Where: <strong>" + ddlWorkPlace.SelectedValue + "</strong>;");
            sb.Append("Recruiter: <strong>" + txtRecruiter.Text + "</strong>;");
            sb.Append("Attachment: <strong><a href=" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + hiddenCandID.Value + ">" +
                "[Click To View Candidate Profile]</a></strong>;");

            return sb.ToString();
        }

        private string GetRecruitersName()
        {
            StringBuilder sb = new StringBuilder(); 
            string[] recruiters = txtRecruiter.Text.Split(',');

            foreach (string rec in recruiters)
            {
                sb.Append(_ps.GetName(rec.Trim()) + ",");
            }

            return sb.ToString().TrimEnd(',');
        }

        private string GetRecruitersEmail()
        {
            StringBuilder sb = new StringBuilder(); 
            string[] recruiters = txtRecruiter.Text.Split(',');

            foreach (string rec in recruiters)
            {
                sb.Append(_ps.GetEmplEmailAdrress(rec.Trim()) + ","); 
            }

            return sb.ToString().TrimEnd(',');
        }

        private string GetOthersEmail()
        {
            StringBuilder sb = new StringBuilder();
            string[] interviewers = txtOtherInterviewers.Text.Split(',');

            foreach (string interviewer in interviewers)
            {
                sb.Append(_ps.GetEmplEmailAdrress(interviewer.Trim()) + ",");
            }

            return sb.ToString().TrimEnd(','); 
             
        }

        private string GetOthersName()
        {
            StringBuilder sb = new StringBuilder();
            string[] interviewers = txtOtherInterviewers.Text.Split(',');

            foreach (string interviewer in interviewers)
            {
                sb.Append(_ps.GetName(interviewer.Trim()) + ",");
            }

            return sb.ToString().TrimEnd(',');
        }

        protected void GridAllRequest_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text = _ps.GetName(e.Row.Cells[2].Text) + " (" + e.Row.Cells[2].Text  + ")";
            }
        }
    }
}
