using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

using EHR.Model;
using System.IO; 

namespace EHR.Recruitment
{
    public partial class AddCandidate1 : System.Web.UI.Page
    {
        ObjectsMisc MyMisc = new ObjectsMisc();
        ObjectsOleDB MyObjs = new ObjectsOleDB();
        QueryOleDB MyQry = new QueryOleDB();
        MiscModel _misc = new MiscModel();
        DBModel _db = new DBModel(); 
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                chkVehicle.Visible = false;
                lblVehicleChk.Visible = false;
                _misc.PopulateCountry(ddlCountry);
            }
        }
         

        protected void IsSave(object sender, EventArgs e)
        {
            SaveData(); 
        }


        private void SaveData()
        {

            string CandidateID = MyMisc.CreateCandidateID();
            DateTime RecordedDateTime = DateTime.Now;
             

            DateTime app_dt = DateTime.Now;
            string app_date = String.Format("{0:MM/dd/yyyy}", app_dt);
            string birthdate = ddlMonth.SelectedValue + "/" + ddlDay.SelectedValue + "/" + txtYear.Text;

            ////czech upload
            string filePathCZ = fileCzechCV.PostedFile.FileName;
            string filenameCZ = Path.GetFileName(filePathCZ);
            string extensionCZ = System.IO.Path.GetExtension(this.fileCzechCV.PostedFile.FileName);

            Stream fsCZ = fileCzechCV.PostedFile.InputStream;
            BinaryReader brCZ = new BinaryReader(fsCZ);
            Byte[] byteCz = brCZ.ReadBytes((Int32)fsCZ.Length);

            ///// english upload 

            string filePathEn = fileEnglishCV.PostedFile.FileName;
            string filenameEN = Path.GetFileName(filePathEn);
            string extensionEN = System.IO.Path.GetExtension(this.fileEnglishCV.PostedFile.FileName);

            Stream fsEN = fileEnglishCV.PostedFile.InputStream;
            BinaryReader brEN = new BinaryReader(fsEN);
            Byte[] bytesEn = brEN.ReadBytes((Int32)fsEN.Length);

            chkVehicle.Items[0].Value = "";
            chkVehicle.Items[1].Value = "";
            chkVehicle.Items[2].Value = "";
            if (chkVehicle.Items[0].Selected)
            {
                chkVehicle.Items[0].Value = "Car";
            }

            if (chkVehicle.Items[1].Selected)
            {
                chkVehicle.Items[1].Value = "ForkLift";
            }
            if (chkVehicle.Items[2].Selected)
            {
                chkVehicle.Items[2].Value = "Others";
            }

            string OracleQry = "Insert Into EREC_CANDIDATES (CANDIDATEID, ISINTERVIEWED, FIRSTNAME, LASTNAME, TITLE, " +
                                "STREET, CITY, COUNTRY, POSTCODE, CONTACT, EMAILADDRESS, BIRTHDATE, " +
                                "CVCZ, CVCZ_EXT, CVEN, CVEN_EXT, SEX, STATUS, GRADUATE, FIELD, LANGUAGE1, " +
                                "SKILL1, LANGUAGE2, SKILL2, EXPERIENCE, EXPERTISE, CURRENTEMPL, CURRENTPOSI, " +
                                "DRIVING, ISCAR, ISFORKLIFT, HIRINGSTATUS, APP_DT, HIGHESTEDUCLVL, BIRTHNUMBER, " +
                                "DELETED, REMARKS, ISDRIVEOTHERS, ADDRESS2, NATIONALITY)" +
                                "VALUES (" +
                                "'" + CandidateID + "', 'No',  '" + txtFirstName.Text + "', '" + txtLastName.Text + "', " +
                                "'" + ddlTitle.SelectedValue.ToString() + "', '" + txtStreet.Text + "', '" + ddlCity.SelectedValue.ToString() + "', " +
                                "'" + ddlCountry.SelectedValue.ToString() + "', '" + txtPostCode.Text + "', " +
                                "'" + txtContact.Text + "', '" + txtEmailAddress.Text  + "', to_date('" + birthdate + "','MM/dd/yyyy'), " +
                                "'" + byteCz + "', '" + extensionCZ + "', '" + bytesEn + "', '" + extensionEN + "', " +
                                "'" + ddlSex.SelectedValue.ToString() + "', '" + ddlStatus.SelectedValue.ToString() + "', '" + ddlGraduate.SelectedValue.ToString() + "', " +
                                "'" + txtField.Text + "', '" + ddlLanguage1.SelectedValue.ToString() + "', " +
                                "'" + ddlSkill1.SelectedValue.ToString() + "', '" + ddlLanguage2.SelectedValue.ToString() + "', " +
                                "'" + ddlSkill2.SelectedValue.ToString() + "', '" + ddlExperience.SelectedValue.ToString() + "', '" + txtExpertise.Text + "', " +
                                "'" + txtCurrentEmpl.Text + "', '" + txtCurrentPosi.Text + "', " +
                                "'" + ddlDrivingLicense.SelectedValue.ToString() + "', '" + chkVehicle.Items[0].Value + "', " +
                                "'" + chkVehicle.Items[1].Value + "', 'In File',  to_date('" + app_date + "','MM/dd/yyyy'), " +
                                "'" + ddlHighestEducLevel.SelectedValue.ToString() + "', '" + txtBirthNumber.Text + "', '0', " +
                                "'" + txtRemarks.Text + "', '" + chkVehicle.Items[2].Value + "', " +
                                "'" + txtAddress2.Text + "', '" + txtNationality.Text + "')";
            MyObjs.GetConn();
            OleDbCommand mycmd = new OleDbCommand(OracleQry, MyObjs.MyConn);
             
            if (Page.IsValid)
            {
                try
                {
                    mycmd.ExecuteNonQuery();
                    //insert activity 
                    MyMisc.ShowMessage("Successfully Added Candidate: " + txtFirstName.Text + " " + txtLastName.Text, this.Page);
                    //Response.AddHeader("REFRESH", "3;URL=http://" + _misc.GetWebAppRoot() + "/Main/Candidate.aspx");
                }
                catch (Exception ex)
                {
                    lblCandidateTitle.Text = ex.Message;
                }
          
            }
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
       
         
    }
}
