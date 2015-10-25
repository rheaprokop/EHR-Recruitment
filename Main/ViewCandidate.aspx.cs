using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using System.Text;
using ehr_email;
using System.IO;
using System.Diagnostics;
using System.Net;
namespace EHR.Main
{
    public partial class ViewCandidate : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc myMisc = new ObjectsMisc();
        MiscModel _misc = new MiscModel();
        CandidateModel _cand = new CandidateModel();
        DBModel _db = new DBModel(); 
        RequestModel _rq = new RequestModel(); 
        PSModel _ps = new PSModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            GetCandidate(Request.QueryString["candidate"]);
            string candidateID = (string)(Session["candidate"]);
            HideAcceptanceDetails();
            GetCandidateStatus(candidateID);
            //DownloadCzech_Link(candidateID);
        }

        private void GetCandidate(string id)
        {
            string docPath = @"C:\EHR\Recruitment\docs\resumes\";
            string OracleQry = "Select FIRSTNAME, LASTNAME, TITLE, STREET, CITY, COUNTRY, " +
                               "POSTCODE, CONTACT, EMAILADDRESS, CVCZ, CVEN, RECORDEDDATE, " +
                               "TO_CHAR(BIRTHDATE,'dd.MM.YYYY') AS BIRTHDATE, HIRINGSTATUS , SEX, GRADUATE, FIELD, LANGUAGE1, SKILL1, LANGUAGE2, SKILL2, " +
                               "EXPERIENCE, EXPERTISE, CURRENTEMPL, CURRENTPOSI, DRIVING, ISCAR, ISFORKLIFT, OFFERSENT, BIRTHNUMBER, REMARKS, ISDRIVEOTHERS " +
                               "from EREC_CANDIDATES where CANDIDATEID = '" + id + "' ";


            myObjs.GetConn();
            OleDbCommand myCmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRd = myCmd.ExecuteReader(); 

            if (myRd.Read())
            {
                lblCandidateID.Text = id;
                Session["candidate"] = id;
                hiddenCandID.Value = id;
                lblCandidateStatus.Text = Convert.ToString(myRd["HIRINGSTATUS"]);
                lblAcademitTitle.Text = Convert.ToString(myRd["TITLE"]);
                lblFirstName.Text = Convert.ToString(myRd["FIRSTNAME"]);
                lblLastName.Text = Convert.ToString(myRd["LASTNAME"]);
                string Name = lblFirstName.Text + " " + lblLastName.Text;
                lblStreet.Text = Convert.ToString(myRd["STREET"]);
                lblCity.Text = Convert.ToString(myRd["CITY"]);
                
                lblCountry.Text =  GetCountryDesc(Convert.ToString(myRd["COUNTRY"]));
                lblPostCode.Text = Convert.ToString(myRd["POSTCODE"]);
                lblContactNumber.Text = Convert.ToString(myRd["CONTACT"]);
                lblContactNumber.Text = "(+420) " + lblContactNumber.Text;
                lblEmailAddress.Text = Convert.ToString(myRd["EMAILADDRESS"]);
                string status = Convert.ToString(myRd["HIRINGSTATUS"]);
                lblStatus.Text = status;
                lblCandidateTitle.Text = Name + " (Status: " + status + ")";
                switch (lblStatus.Text)
                {
                    case "C":
                        lblStatus.Text = "Cohabited";
                        break;
                    case "D":
                        lblStatus.Text = "Divorced";
                        break;
                    case "E":
                        lblStatus.Text = "Separated";
                        break;
                    case "M":
                        lblStatus.Text = "Married";
                        break;
                    case "S":
                        lblStatus.Text = "Single";
                        break;
                    case "W":
                        lblStatus.Text = "Widowed";
                        break;
                    case "L":
                        lblStatus.Text = "Married With Children";
                        break;
                }


                lblBirthdate.Text = Convert.ToString(myRd["BIRTHDATE"]);


                string grad = Convert.ToString(myRd["GRADUATE"]);
 
                switch (grad)
                {
                    case "A":
                        lblGraduate.Text = "Ph.D";
                        break;
                    case "B":
                        lblGraduate.Text = "Master";
                        break;
                    case "C":
                        lblGraduate.Text = "Bachelor";
                        break;
                    case "D":
                        lblGraduate.Text = "Technician";
                        break;
                    case "F":
                        lblGraduate.Text = "Junior High School";
                        break;
                    case "G":
                        lblGraduate.Text = "Elementary";
                        break;
                    case "E":
                        lblGraduate.Text = "Senior High School";
                        break;
                    case "H":
                        lblGraduate.Text = "Illiterate";
                        break;
                    case "I":
                        lblGraduate.Text = "Others";
                        break;
                }



                lblField.Text = Convert.ToString(myRd["FIELD"]);
                lblLanguage1.Text = Convert.ToString(myRd["LANGUAGE1"]);
                lblSkill1.Text = Convert.ToString(myRd["SKILL1"]);
                lblLanguage2.Text = Convert.ToString(myRd["LANGUAGE2"]);
                lblSkill2.Text = Convert.ToString(myRd["SKILL2"]);
                lblSex.Text = Convert.ToString(myRd["SEX"]);
                if (lblSex.Text == "M")
                {
                    lblSex.Text = "Male";
                }

                if (lblSex.Text == "F")
                {
                    lblSex.Text = "Female";
                }

                lblExperience.Text = Convert.ToString(myRd["EXPERIENCE"]);
                lblExpertise.Text = Convert.ToString(myRd["EXPERIENCE"]);
                lblCurrentEmpl.Text = Convert.ToString(myRd["CURRENTEMPL"]);
                lblCurrentPosi.Text = Convert.ToString(myRd["CURRENTPOSI"]);
                lblBirthNumber.Text = Convert.ToString(myRd["BIRTHNUMBER"]); 
 
                lblRemarks.Text = Convert.ToString(myRd["REMARKS"]);

                try
                {
                     byte[] dbbyteCZ;
                   dbbyteCZ = (byte[])myRd["CVCZ"];  

                     
                }
                catch (Exception ex)
                {
                    btnDownloadCzechCV.Visible = false;
                    lblNoCzechCV.Visible = true;
                }

                try
                {
                    byte[] dbbyteEn;
                    dbbyteEn = (byte[])myRd["CVEN"];
                }
                catch (Exception ex)
                {
                    btnEnglishCV.Visible = false;
                    lblNoEnglishCV.Visible = true;
                }
            }

        }

        private void HideAcceptanceDetails()
        {
            if (lblStatus.Text == "Acceptance Letter Sent")
            {
                //btnCandidateAcceptance.Visible = true;
                pnlHideAcceptance.Visible = true;
            }
            else
            {
                //btnCandidateAcceptance.Visible = false;
                pnlHideAcceptance.Visible = false;
                
            }
        }

        protected void BtnCandidateAcceptedAction_Click(object sender, EventArgs e)
        {
            string selected = radioAcceptance.SelectedValue;
            string candidateID = (string)(Session["candidate"]); 

            _db.GetConn();
            string UpdateCandProfile = "Update EREC_CANDIDATES Set HIRINGSTATUS='Accepted', " +
                        "OFFERACCEPTED = '" + radioAcceptance.SelectedValue + "' " +
                        "Where CANDIDATEID='" + candidateID + "'";
            OleDbCommand myCMDUpdate = new OleDbCommand(UpdateCandProfile, myObjs.MyConn);
            myCMDUpdate.ExecuteNonQuery();
            CreateEmployeeID(candidateID); //create an employee ID here

            
            NotifyHRTrainor(candidateID);

            myMisc.ShowMessage("Successfully updated candidate profile. Candidate: " + _cand.GetCandidateName(candidateID), this.Page);
            Response.AddHeader("REFRESH", "1;URL=http://" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + candidateID);
 
        }

        private void NotifyHRTrainor(string candidate)
        {
            if (_misc.IsEmailNotificationTest("Notify_HR_Trainor", "HR Trainor Notification: " + candidate, candidate) == false)
            {
                string mail_type = "Notify_HR_Trainor";
                string sender_ = "noreply@wistron.com ";

                string recipient = GetTrainorEmails(candidate);
                string recipient_name = GetTrainorsName(candidate);
                string cc = "";
                string subject = "WCZ Trainor - Recruitment Notification";

                StringBuilder sb = new StringBuilder();
                sb.Append("Candidate Name: <a href=" + _misc.GetWebAppRoot() + "/Main/ViewCandidate.aspx?candidate=" + candidate + ">" 
                                                + _cand.GetCandidateName(candidate) + "</a>;");
                sb.Append("<br /><a href=" + _misc.GetWebAppRoot() + "/Main/Training.aspx>Go To On-Board Table</a>");
                 
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
        private void CreateEmployeeID(string id)
        {
            string LastEmplid;
            string EmplID;

            string GetEmployee = "Select EMPLID from EREC_CANDIDATES WHERE EMPLID = (Select max(EMPLID) from EREC_CANDIDATES)";
            OleDbCommand mycmd = new OleDbCommand(GetEmployee, myObjs.MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();
            if (myrd.Read())
            {
                LastEmplid = Convert.ToString(myrd["EMPLID"]);
            }
            else
            {
                LastEmplid = "";
            }

            if (LastEmplid != "")
            {
                EmplID = myMisc.GenerateEmployeeID();
            }
            else
            {
                EmplID = myMisc.FirstEmplID();
            }

            string UpdateEmplID = "Update EREC_CANDIDATES set EMPLID='" + EmplID + "' where CANDIDATEID='" + id + "'";
            OleDbCommand mycmdUpdate = new OleDbCommand(UpdateEmplID, myObjs.MyConn);
            mycmdUpdate.ExecuteNonQuery();
        }
        
        private string GetTrainorEmails(string candidate)
        {
            StringBuilder sb = new StringBuilder();
            string[] interviewers = _rq.GetTrainor(_cand.GetCandidateRequestAcceptedFor(candidate)).Split(',');

            foreach (string interviewer in interviewers)
            {
                sb.Append(_ps.GetEmplEmailAdrress(interviewer.Trim()) + ",");
            }

            return sb.ToString().TrimEnd(','); 
             
        } 

        private string GetTrainorsName(string candidate)
        {
            StringBuilder sb = new StringBuilder();
            string[] interviewers = _rq.GetTrainor(_cand.GetCandidateRequestAcceptedFor(candidate)).Split(',');

            foreach (string interviewer in interviewers)
            {
                sb.Append(_ps.GetName(interviewer.Trim()) + ",");
            }

            return sb.ToString().TrimEnd(',');
        }

        private void GetCandidateStatus(string candidate)
        {
            switch (lblStatus.Text)
            {
                case "In File":
                    btnAddDetails.Enabled = false;
                    btnSendOffer.Enabled = false;
                    break;
                case "Accepted":
                    btnInvite.Enabled = false;
                    btnAddDetails.Enabled = false;
                    btnSendOffer.Enabled = false;
                    break;
                case "For Interview":  
                    btnSendOffer.Enabled = false;
                    break;
                case "Interviewed":
                    break;
                case "In PS":
                    btnInvite.Visible = false;
                    btnAddDetails.Visible = false;
                    btnSendOffer.Visible = false;
                    btnBackToProfile.Visible = false;
                    break;
                case "On Board":
                    btnInvite.Visible = false;
                    btnAddDetails.Visible = false;
                    btnSendOffer.Visible = false;
                    btnBackToProfile.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void DownloadCzech_Link(string candidate)
        {
            _db.GetConn();
            FileStream FS = null;
            byte[] dbbyte;
            string extension;
            string sql = "SELECT CVCZ, CVCZ_EXT FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                try
                {
                    dbbyte = (byte[])rd["CVCZ"];
                    extension = Convert.ToString(rd["CVCZ_EXT"]);


                    Response.Buffer = true;
                    Response.ContentType = GetContentType(extension);
                    Response.BinaryWrite(dbbyte);
                }
                catch (Exception ex)
                {
                }
                
            }
        }

        private void DownloadEnglish_Link(string candidate)
        {
            _db.GetConn();
            FileStream FS = null;
            byte[] dbbyte;
            string extension;
            string sql = "SELECT CVEN, CVEN_EXT FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                dbbyte = (byte[])rd["CVEN"];
                extension = Convert.ToString(rd["CVEN_EXT"]);

               
                Response.Buffer = true;
                Response.ContentType = GetContentType(extension);
                Response.BinaryWrite(dbbyte);

            }
        }

        protected void BtnDownloadCzech_Click(object sender, EventArgs e)
        {
            DownloadCzech_Link(hiddenCandID.Value);
             
        }

        protected void BtnDownloadEnglish_Click(object sender, EventArgs e)
        {
            DownloadEnglish_Link(hiddenCandID.Value);
        }

        static string GetContentType(string ext)
        {
            string contenttype;
            switch (ext)
            {
                case ".doc":
                    contenttype = "application/msword";
                    break;
                case ".docx":
                    contenttype = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".xls":
                    contenttype = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    contenttype = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                case ".pdf":
                    contenttype = "application/pdf";
                    break;
                default:
                    contenttype = "";
                    break;
            }

            return contenttype;
        }


        public string GetCountryDesc(string country_id)
        {
            _db.GetConn(); 
            string desc; 
            string sql = "select DESCRSHORT  from LIB_PS_COUNTRY WHERE COUNTRYID = '" + country_id + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();
            if (_rd.Read())
            {
                desc = Convert.ToString(_rd["DESCRSHORT"]);
            }
            else
            {
                desc = ""; 
            }

            return desc;
        }
    }
}
