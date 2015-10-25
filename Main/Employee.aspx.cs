using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model;
using System.Configuration;
namespace EHR.Recruitment
{
    public partial class Employee : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        ObjectsMisc myMisc = new ObjectsMisc();
        DBModel _db = new DBModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            string candidate = Request.QueryString["candidate"];
            LoadCandidteDetails(candidate);
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
            string OracleQry = "Select TO_CHAR(APP_DT,'yyyy/MM/dd') AS APP_DT, BIRTHNUMBER, CANDIDATEID, EMPLID, FIRSTNAME, LASTNAME, TITLE, STREET, CITY, COUNTRY, " +
                   "POSTCODE, CONTACT, EMAILADDRESS, CVCZ, CVEN, RECORDEDDATE, " +
                   "TO_CHAR(BIRTHDATE,'yyyy/MM/dd') AS BIRTHDATE, STATUS, SEX, GRADUATE, FIELD, LANGUAGE1, SKILL1, LANGUAGE2, SKILL2, " +
                   "EXPERIENCE, EXPERTISE, CURRENTEMPL, CURRENTPOSI, DRIVING, ISCAR, ISFORKLIFT, OFFERSENT, " +
                   "OFFERFORREQUESTID, HIRINGSTATUS, TO_CHAR(OFFERONBOARDDATE,'yyyy/MM/dd') AS OFFERONBOARDDATE, OFFERDEPT, HIGHESTEDUCLVL, ADDRESS2, NATIONALITY " +
                   "from EREC_CANDIDATES where CANDIDATEID = '" + id + "' ";

            OleDbCommand myCmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRd = myCmd.ExecuteReader();

            if (myRd.Read())
            {

                hiddenApp_dt.Value = Convert.ToString(myRd["APP_DT"]);
                lblEmployeeID.Text = Convert.ToString(myRd["EMPLID"]);
                lblCandidateTitle.Text = Convert.ToString(myRd["TITLE"]);
                lblFirstName.Text = Convert.ToString(myRd["FIRSTNAME"]);
                lblLastName.Text = Convert.ToString(myRd["LASTNAME"]);
                lblStreet.Text = Convert.ToString(myRd["STREET"]);
                lblCity.Text = Convert.ToString(myRd["CITY"]);
                lblCountry.Text = Convert.ToString(myRd["COUNTRY"]);
                lblPostCode.Text = Convert.ToString(myRd["POSTCODE"]);
                lblContactNumber.Text = Convert.ToString(myRd["CONTACT"]);
                //lblContactNumber.Text = "(+420) " + lblContactNumber.Text;
                lblEmailAddress.Text = Convert.ToString(myRd["EMAILADDRESS"]);
                string status = Convert.ToString(myRd["STATUS"]);
                hiddenStatus.Value = status;
                switch(status)
                {
                    case "S":
                        lblStatus.Text = "Single";

                        break;
                    case "M":
                        lblStatus.Text = "Married";
                        break;
                    case "L":
                        lblStatus.Text = "Married With Children";
                        break;
                    case "C":
                        lblStatus.Text = "Cohabited";
                        break;
                    case "E":
                        lblStatus.Text = "Separated";
                        break;
                    case "W":
                        lblStatus.Text = "Widowed";
                        break;
                }
                             

                lblBirthdate.Text = Convert.ToString(myRd["BIRTHDATE"]);
                string grad = Convert.ToString(myRd["GRADUATE"]);

                hiddenGrad.Value = grad;
                switch(grad)
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

               

                string highest = Convert.ToString(myRd["HIGHESTEDUCLVL"]);
                hiddenHighest.Value = highest;
                switch(highest)
                {
                    case "A":
                        lblHighest.Text  = "None Selected";
                        break;
                    case "B":
                        lblHighest.Text  = "Less Than HS Graduate";
                        break;
                    case "C":
                        lblHighest.Text  = "HS Graduate or Equivalent";
                        break;
                    case "D":
                        lblHighest.Text  = "Some College";
                        break;
                    case "E":
                        lblHighest.Text  = "Technical School";
                        break; 
                    case "F":
                        lblHighest.Text  = "2-Year College Degree";
                        break;
                    case "G":
                        lblHighest.Text  = "Bachelor Level Degree";
                        break;
                } 

                lblLanguage1.Text = Convert.ToString(myRd["LANGUAGE1"]);
                lblSkill1.Text = Convert.ToString(myRd["SKILL1"]);
                lblLanguage2.Text = Convert.ToString(myRd["LANGUAGE2"]);
                lblSkill2.Text = Convert.ToString(myRd["SKILL2"]);
                hiddenGender.Value = Convert.ToString(myRd["SEX"]);

                switch (hiddenGender.Value)
                {
                    case "M":
                        lblSex.Text = "Male";
                        break;
                    case "F":
                        lblSex.Text = "Female";
                        break;
                }
                                          
                lblOfferOnBoardDate.Text = Convert.ToString(myRd["OFFERONBOARDDATE"]);
                lblDept.Text = Convert.ToString(myRd["OFFERDEPT"]); 
                hiddenReqID.Value = Convert.ToString(myRd["OFFERFORREQUESTID"]);
                hiddenCandID.Value = Convert.ToString(myRd["CANDIDATEID"]);
                string hiringstats = Convert.ToString(myRd["HIRINGSTATUS"]);

                if (hiringstats == "In PS")
                {
                    btnSavePS.Enabled = false;
                }

                lblAddress2.Text = Convert.ToString(myRd["ADDRESS2"]);
                lblNationality.Text = Convert.ToString(myRd["NATIONALITY"]);
            }

            myObjs.MyConn.Close();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            myObjs.GetConn();
            string UpdatePS = "Update EREC_CANDIDATES set HIRINGSTATUS = 'In PS' Where CANDIDATEID='" + hiddenCandID.Value + "'";
            OleDbCommand myCMDUpdate = new OleDbCommand(UpdatePS, myObjs.MyConn);
            myCMDUpdate.ExecuteNonQuery();

            string CountHired = "Select REQUIREDPERSON, HIRED From EREC_REQUESTS Where REQUESTID='" + hiddenReqID.Value + "'";
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
                string UpdateStats = "UPDATE EREC_REQUESTS Set HIRED = '" + hired + "', RESULT = 'RECRUITMENT' where REQUESTID='" + hiddenReqID.Value + "'";
                OleDbCommand myCMD = new OleDbCommand(UpdateStats, myObjs.MyConn);
                myCMD.ExecuteNonQuery();

            }

            if (hired == required)
            {
                string UpdateStats = "UPDATE EREC_REQUESTS Set RESULT = 'CLOSED' where REQUESTID='" + hiddenReqID.Value + "'";
                OleDbCommand myCMD = new OleDbCommand(UpdateStats, myObjs.MyConn);
                myCMD.ExecuteNonQuery();
            }
            //SaveEmplData();
            
            SaveToPSTable();

            Response.Redirect("~/Main/PSEncoder.aspx");

        }

        private string SaveApplcatnData()
        {
            string Name = lblFirstName.Text + " " + lblLastName.Text;
            string nationalnum;
            if (txtUIN_SGP.Text == "")
            {
                nationalnum = "N/A";
            }
            else
            {
                nationalnum = txtUIN_SGP.Text;
            }
             
            string OraclQry = "Insert into PS_APPLCATN_DATA_A (" +
                   "EMPLID, LOCATION, APP_DT, DESIRED_START_DT, " +
                   "DEPTID, NAME, ADDRESS1, COUNTRY, STATE, CITY, ZIP, PHONE, SEX, MAR_STATUS, " +
                   "BIRTHDATE, UIN_SGP,    " +
                   "LANGUAGE_SKILL, DEGREE, " +
                   "AREA_CODE_A, MAR_STATUS_DT, BLOOD_TYPE, REFERRAL_CODE_A, GENERAL_LVL_A, YR_EARNED, MONTH_GRADUATED_A, " +
                   "SCHOOL_CODE, DESCR, MAJOR_CODE, DESCR_A, ADDRESS2, NATIONALITY_A" +
                   ") VALUES (" +
                   "'" + lblEmployeeID.Text + "', '" + ddlLocation.SelectedValue.ToString() + "',  to_date('" + hiddenApp_dt.Value + "','yyyy/MM/dd'), to_date('" + lblOfferOnBoardDate.Text + "','yyyy/MM/dd'), " +
                   "'" + lblDept.Text + "',  '" + Name + "', '" + lblStreet.Text + "', '" + lblCountry.Text + "', 'none', " +
                   " '" + lblCity.Text + "', '" + lblPostCode.Text + "', '" + lblContactNumber.Text + "', '" + hiddenGender.Value + "', '" + hiddenStatus.Value + "', " +
                   "to_date('" + lblBirthdate.Text + "','yyyy/MM/dd'), " + nationalnum + ", " +
                   "'" + lblLanguage1.Text + "', '" + hiddenGrad.Value + "', " +
                   "'" + lblPostCode.Text + "', '', 'NA', '9', '0', '0000', '0000', " +
                   "'0', 'NA', 'NA', 'NA', '" + lblAddress2.Text + "', '" + lblNationality.Text + "'" +
                   ")";

            return OraclQry;
        }

        //private void SaveEmplData()
        //{ 
        //    OleDbCommand myCmd = new OleDbCommand(SaveApplcatnData(), myObjs.MyConn);
        //    try
        //    {
        //      //  myCmd.ExecuteNonQuery();
        //    }
        //    catch (OleDbException ex)
        //    {
        //        myMisc.ShowMessage(ex.Message, this.Page);
        //    }
        //}


        private void SaveToPSTable()
        {
            try
            {

                string conv2Str = ConfigurationManager.ConnectionStrings["connConv2"].ToString();
                OleDbConnection connConv2 = new OleDbConnection();
                connConv2.ConnectionString = conv2Str;
                connConv2.Open();
                OleDbCommand cmdConv2 = new OleDbCommand(SaveApplcatnData(), connConv2);
                cmdConv2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
    }
}
