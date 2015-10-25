using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EHR.Model;

namespace EHR.Main
{
    public partial class Update : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        ObjectsMisc myMisc = new ObjectsMisc();
        DBModel _db = new DBModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                string candidate = Request.QueryString["id"];
                GetCandidate(candidate);
                PopulateCountry(ddlCountry); 
            }
        }

        private void GetCandidate(string id)
        {
            string docPath = @"C:\EHR\Recruitment\docs\resumes\";
            string OracleQry = "Select FIRSTNAME, LASTNAME, TITLE, STREET, CITY, COUNTRY, " +
                               "POSTCODE, CONTACT, EMAILADDRESS, " +
                               "BIRTHDATE, STATUS, SEX, GRADUATE, FIELD, LANGUAGE1, SKILL1, LANGUAGE2, SKILL2, " +
                               "EXPERIENCE, EXPERTISE, CURRENTEMPL, CURRENTPOSI, DRIVING, ISCAR, ISFORKLIFT, OFFERSENT, HIGHESTEDUCLVL, BIRTHNUMBER, " +
                               "CVCZ, CVEN from EREC_CANDIDATES where CANDIDATEID = '" + id + "' ";


            myObjs.GetConn();
            OleDbCommand myCmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            OleDbDataReader myRd = myCmd.ExecuteReader();

            if (myRd.Read())
            {
                txtFirstName.Text = Convert.ToString(myRd["FIRSTNAME"]);
                txtLastName.Text = Convert.ToString(myRd["LASTNAME"]);
                txtStreet.Text = Convert.ToString(myRd["STREET"]);
                ddlCity.SelectedValue = Convert.ToString(myRd["CITY"]);
                ddlCountry.SelectedValue = Convert.ToString(myRd["COUNTRY"]);
                txtPostCode.Text = Convert.ToString(myRd["POSTCODE"]);
                txtContact.Text = Convert.ToString(myRd["CONTACT"]);
                txtEmailAddress.Text = Convert.ToString(myRd["EMAILADDRESS"]);
                ddlTitle.SelectedValue = Convert.ToString(myRd["TITLE"]);
                string bdate = Convert.ToString(myRd["BIRTHDATE"]).ToString();

                DateTime myDateTime = DateTime.Parse(bdate);
                ddlDay.SelectedValue = myDateTime.Day.ToString();
                ddlMonth.SelectedValue = myDateTime.Month.ToString();
                txtYear.Text = myDateTime.Year.ToString();

                ddlStatus.SelectedValue = Convert.ToString(myRd["STATUS"]);
                ddlSex.SelectedValue = Convert.ToString(myRd["SEX"]);
                ddlGraduate.SelectedValue = Convert.ToString(myRd["SEX"]);
                txtField.Text = Convert.ToString(myRd["FIELD"]);
                ddlLanguage1.SelectedValue = Convert.ToString(myRd["LANGUAGE1"]);
                ddlSkill1.SelectedValue = Convert.ToString(myRd["SKILL1"]);
                ddlLanguage2.SelectedValue = Convert.ToString(myRd["LANGUAGE2"]);
                ddlSkill2.SelectedValue = Convert.ToString(myRd["SKILL2"]);
                ddlExperience.SelectedValue = Convert.ToString(myRd["EXPERIENCE"]);
                txtExpertise.Text = Convert.ToString(myRd["EXPERTISE"]);
                txtCurrentEmpl.Text = Convert.ToString(myRd["CURRENTEMPL"]);
                txtCurrentPosi.Text = Convert.ToString(myRd["CURRENTPOSI"]);
                ddlDrivingLicense.SelectedValue = Convert.ToString(myRd["DRIVING"]);

                if (ddlDrivingLicense.SelectedValue == "Yes")
                {
                    string ISCAR = Convert.ToString(myRd["ISCAR"]);
                    if (ISCAR != "")
                    {
                        chkVehicle.Items[0].Selected = true;
                    }
                    string ISFORKLIFT = Convert.ToString(myRd["ISFORKLIFT"]);

                    if (ISCAR != "")
                    {
                        chkVehicle.Items[1].Selected = true;
                    }
                }


                ddlHighestEducLevel.SelectedValue = Convert.ToString(myRd["HIGHESTEDUCLVL"]);
                txtBirthNumber.Text = Convert.ToString(myRd["BIRTHNUMBER"]);
                //GET RESUMES FILENAME
                string CVcz = Convert.ToString(myRd["CVCZ"]);
                string CVen = Convert.ToString(myRd["CVEN"]);

                if (CVcz == "")
                {
                    linkIsCVCZ.Text = "No Resume Uploaded";
                }
                
                if (CVen == "")
                {
                    linkIsCVEN.Text = "No Resume Uploaded";
                }
               
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string candidate = Request.QueryString["id"];
            string OracleQry = " Update EREC_CANDIDATES set FIRSTNAME = :firstname, " +
                "LASTNAME = :lastname, TITLE = :title, STREET = :street, CITY = :city, " +
                "COUNTRY = :country, POSTCODE = :postcode, CONTACT = :contact, EMAILADDRESS = :email, " +
                "BIRTHDATE = to_date(:birthdate,'MM/DD/YYYY'), STATUS = :status, SEX = :sex, GRADUATE = :graduate, FIELD = :field, " +
                "LANGUAGE1 = :language1, SKILL1 = :skill1, LANGUAGE2 = :language2, SKILL2 = :skill2, " +
                "EXPERIENCE = :experience, EXPERTISE = :expertise, CURRENTEMPL = :currentempl, CURRENTPOSI = :currentposi, " +
                "DRIVING = :driving, ISCAR = :iscar, ISFORKLIFT = :isforklift, " +
                "HIGHESTEDUCLVL = :highesteduc, BIRTHNUMBER = :birthnumber " +
                "Where CANDIDATEID = '" + candidate + "'";

            myObjs.GetConn();
            OleDbCommand mycmd = new OleDbCommand(OracleQry, myObjs.MyConn);

            mycmd.Parameters.AddWithValue("':firstname'", txtFirstName.Text);
            mycmd.Parameters.AddWithValue("':lastname'", txtLastName.Text);
            mycmd.Parameters.AddWithValue("':title'", ddlTitle.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':street'", txtStreet.Text);
            mycmd.Parameters.AddWithValue("':city'", ddlCity.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':country'", ddlCountry.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':postcode'", txtPostCode.Text);
            mycmd.Parameters.AddWithValue("':contact'", txtContact.Text);
            mycmd.Parameters.AddWithValue("':email'", txtEmailAddress.Text);
            mycmd.Parameters.AddWithValue("':birthdate'", ddlMonth.SelectedValue + "/" + ddlDay.SelectedValue + "/" + txtYear.Text); // 12/25/2011
            mycmd.Parameters.AddWithValue("':status'", ddlStatus.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':sex'", ddlSex.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':graduate'", ddlGraduate.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':field'", txtField.Text);
            mycmd.Parameters.AddWithValue("':language1'", ddlLanguage1.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':skill1'", ddlSkill1.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':language2'", ddlLanguage2.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':skill2'", ddlSkill2.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':experience'", ddlExperience.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':expertise'", txtExpertise.Text);
            mycmd.Parameters.AddWithValue("':currentempl'", txtCurrentEmpl.Text);
            mycmd.Parameters.AddWithValue("':currentposi'", txtCurrentPosi.Text);
            mycmd.Parameters.AddWithValue("':driving'", ddlDrivingLicense.SelectedValue.ToString());


            chkVehicle.Items[0].Value = "";
            chkVehicle.Items[0].Value = "";

            if (chkVehicle.Items[0].Selected)
            {
                chkVehicle.Items[0].Value = "Car";
            }

            if (chkVehicle.Items[1].Selected)
            {
                chkVehicle.Items[1].Value = "ForkLift";
            }

            mycmd.Parameters.AddWithValue("':iscar'", chkVehicle.Items[0].Value);
            mycmd.Parameters.AddWithValue("':isforklift'", chkVehicle.Items[1].Value);

            mycmd.Parameters.AddWithValue("':highesteduc'", ddlHighestEducLevel.SelectedValue.ToString());
            mycmd.Parameters.AddWithValue("':birthnumber'", txtBirthNumber.Text);

            //UPDATE UPLOAD NEW FILE IF NOT EMPTY 

            string filenameCZ;
            string filenameEN;

            if (fileCzechCV.HasFile)
            {

                string extCZ = System.IO.Path.GetExtension(this.fileCzechCV.PostedFile.FileName);
                filenameCZ = candidate + "_cz" + extCZ;

                //delete existing file then save new one

                fileCzechCV.SaveAs(@"C:\EHR\Recruitment\docs\resumes\" + filenameCZ);

                string UpdateCVCZ = "Update EREC_CANDIDATES Set CVCZ = '" + filenameCZ + "' " +
                    "Where CANDIDATEID = '" + candidate + "'";
                OleDbCommand mycmdCZ = new OleDbCommand(UpdateCVCZ, myObjs.MyConn);
                mycmdCZ.ExecuteNonQuery();
            }

            if (fileEnglishCV.HasFile)
            {
                string extEN = System.IO.Path.GetExtension(this.fileEnglishCV.PostedFile.FileName);
                filenameEN = candidate + "_en" + extEN;
                fileEnglishCV.SaveAs(@"C:\EHR\Recruitment\docs\resumes\" + filenameEN);
                string UpdateCVEN = "Update CANDIDATES Set CVEN = '" + filenameEN + "' " +
                "Where CANDIDATEID = '" + candidate + "'";
                OleDbCommand mycmdEN = new OleDbCommand(UpdateCVEN, myObjs.MyConn);
                mycmdEN.ExecuteNonQuery();
            }


            if (Page.IsValid)
            {
                mycmd.ExecuteNonQuery();
                //insert activity
                string txtActivity = "Updated Candidate Profile: " + candidate;
                string UserID = (string)(Session["UserID"]);
                myQry.InsertActivityDesc(txtActivity, UserID);


                Response.Redirect("~/Main/Candidate.aspx?action=view&s=0");

            }
            myObjs.MyConn.Close();


        }



        protected void DdlDrivingLicense_OnChange(object sender, EventArgs e)
        {
            string _filter = ddlDrivingLicense.SelectedValue;

            if (_filter == "Yes")
            {
                lblVehicleChk.Visible = true;
                chkVehicle.Visible = true;
            }
            else
            {
                lblVehicleChk.Visible = false;
                chkVehicle.Visible = false;
            }

        }

        public void PopulateCountry(DropDownList ddlCountry)
        {
            string sql = "select COUNTRYID, DESCRSHORT  from LIB_PS_COUNTRY";
            DataTable _dt = _db.GetTable(sql);

            ddlCountry.DataSource = _dt;
            ddlCountry.DataTextField = "DESCRSHORT";
            ddlCountry.DataValueField = "COUNTRYID";
            ddlCountry.DataBind();


             
           ddlCountry.SelectedValue = "232";
             
        }

       
    }
}
