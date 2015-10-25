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
    public partial class CandidateInfo : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            string candidate = Request.QueryString["candidate"];
            LoadCandidteDetails(candidate);
            IsSession();
        }

        private void IsSession()
        {
            string UserID = (string)(Session["UserID"]);
            if (UserID == "")
            {
                Response.Redirect("~/Account/Default.aspx?s=0");
            }
        }

        private void GetAction(string a, string candidateID)
        {
            switch (a)
            {
                case "view":
                    LoadCandidteDetails(candidateID);
                    break;
            }
        }

        private void LoadCandidteDetails(string id)
        {
            myObjs.GetConn();
            string OracleQry = "Select CANDIDATEID, FIRSTNAME, LASTNAME, TITLE, STREET, CITY, COUNTRY, " +
                   "POSTCODE, CONTACT, EMAILADDRESS, CVCZ, CVEN, RECORDEDDATE, " +
                   "BIRTHDATE, STATUS, SEX, GRADUATE, FIELD, LANGUAGE1, SKILL1, LANGUAGE2, SKILL2, " +
                   "EXPERIENCE, EXPERTISE, CURRENTEMPL, CURRENTPOSI, DRIVING, ISCAR, ISFORKLIFT, OFFERSENT, OFFERFORREQUESTID, HIRINGSTATUS " +
                   "from CANDIDATES where CANDIDATEID = '" + id + "' ";

            OleDbCommand myCmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRd = myCmd.ExecuteReader();
            
            if (myRd.Read())
            {
                lblCandidateID.Text             = id;
                lblCandidateTitle.Text          = Convert.ToString(myRd["TITLE"]);
                lblFirstName.Text = Convert.ToString(myRd["FIRSTNAME"]);
                lblLastName.Text = Convert.ToString(myRd["LASTNAME"]);
                lblStreet.Text = Convert.ToString(myRd["STREET"]);
                lblCity.Text = Convert.ToString(myRd["CITY"]);
                lblCountry.Text = Convert.ToString(myRd["COUNTRY"]);
                lblPostCode.Text = Convert.ToString(myRd["POSTCODE"]);
                lblContactNumber.Text = Convert.ToString(myRd["CONTACT"]);
                lblContactNumber.Text = "(+420) " + lblContactNumber.Text;
                lblEmailAddress.Text = Convert.ToString(myRd["EMAILADDRESS"]);
                lblStatus.Text = Convert.ToString(myRd["STATUS"]);
                lblBirthdate.Text = Convert.ToString(myRd["BIRTHDATE"]);
                string grad = Convert.ToString(myRd["GRADUATE"]);

                switch (grad)
                {
                    case "A":
                        lblGraduate.Text = "None Selected";
                        break;
                    case "B":
                        lblGraduate.Text = "Less Than HS Graduate";
                        break;
                    case "C":
                        lblGraduate.Text = "HS Graduate or Equivalent";
                        break;
                    case "D":
                        lblGraduate.Text = "Some College";
                        break;
                    case "E":
                        lblGraduate.Text = "Technical School";
                        break;
                    case "F":
                        lblGraduate.Text = "2-Year College Degree";
                        break;
                    case "G":
                        lblGraduate.Text = "Bachelor Level Degree";
                        break;
                }
                                    

                lblField.Text = Convert.ToString(myRd["FIELD"]);
                lblLanguage1.Text = Convert.ToString(myRd["LANGUAGE1"]);
                lblSkill1.Text = Convert.ToString(myRd["SKILL1"]); 
                lblLanguage2.Text = Convert.ToString(myRd["LANGUAGE2"]);
                lblSkill2.Text = Convert.ToString(myRd["SKILL2"]);
                lblSex.Text = Convert.ToString(myRd["SEX"]);
                lblExperience.Text = Convert.ToString(myRd["EXPERIENCE"]);
                lblExpertise.Text = Convert.ToString(myRd["EXPERIENCE"]);
                lblCurrentEmpl.Text = Convert.ToString(myRd["CURRENTEMPL"]);
                lblCurrentPosi.Text = Convert.ToString(myRd["CURRENTPOSI"]);
                lblDrivingLicense.Text = Convert.ToString(myRd["DRIVING"]);
                lblIsCar.Text = Convert.ToString(myRd["ISCAR"]);
                lblIsForkLift.Text = Convert.ToString(myRd["ISFORKLIFT"]);
                lblIsCVCZ.Text = Convert.ToString(myRd["CVCZ"]);
                lblIsCVEN.Text = Convert.ToString(myRd["CVEN"]);
                hiddenReqID.Value = Convert.ToString(myRd["OFFERFORREQUESTID"]);
                hiddenCandID.Value = Convert.ToString(myRd["CANDIDATEID"]);
                string hiringstats = Convert.ToString(myRd["HIRINGSTATUS"]);

                if (hiringstats == "In PS")
                {
                    btnSavePS.Enabled = false;
                }
            }

            myObjs.MyConn.Close();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            myObjs.GetConn();
            string UpdatePS = "Update CANDIDATES set HIRINGSTATUS = 'In PS' Where CANDIDATEID='" + lblCandidateID.Text + "'";
            OleDbCommand myCMDUpdate = new OleDbCommand(UpdatePS, myObjs.MyConn);
            myCMDUpdate.ExecuteNonQuery();

            string CountHired = "Select REQUIREDPERSON, HIRED From REQUESTS Where REQUESTID='" + hiddenReqID.Value + "'";
            int hired;
            int required;
            
            OleDbCommand myHired = new OleDbCommand(CountHired, myObjs.MyConn);
            OleDbDataReader myRD = myHired.ExecuteReader();
            if (myRD.Read())
            {

                required = Convert.ToInt32(myRD["REQUIREDPERSON"]);
                hired = Convert.ToInt32(myRD["HIRED"]); 

            }
            else
            {
                hired = 0;
                required = 0;
            }
            hired = hired + 1;

            if (hired != 0)
            {
                string UpdateStats = "UPDATE REQUESTS Set HIRED = '" + hired + "', RESULT = 'RECRUITMENT' where REQUESTID='" + hiddenReqID.Value + "'";
                OleDbCommand myCMD = new OleDbCommand(UpdateStats, myObjs.MyConn);
                myCMD.ExecuteNonQuery();
                 
            }
            
            if(hired == required)
            {
                string UpdateStats = "UPDATE REQUESTS Set RESULT = 'CLOSED' where REQUESTID='" + hiddenReqID.Value + "'";
                OleDbCommand myCMD = new OleDbCommand(UpdateStats, myObjs.MyConn);
                myCMD.ExecuteNonQuery();
            }

            Response.Redirect("~Recruitment/PSEncoder.aspx");

        }


        private void SaveEmplData()
        {

        }
    }
}
