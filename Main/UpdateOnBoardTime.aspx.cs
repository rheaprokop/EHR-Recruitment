using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model; 

namespace EHR.Main
{
    public partial class UpdateOnBoardTime : System.Web.UI.Page
    {
        QueryOleDB myQry = new QueryOleDB();
        CandidateModel _cand = new CandidateModel();
        DBModel _db = new DBModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            string candidate = Request.QueryString["candidate"];
            calOnBoardDate.Visible = false;

            LoadCandidateData(candidate); 
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

        private void LoadCandidateData(string candidate)
        {
            _db.GetConn();
            txtCandidateName.Text = myQry.GetCandidateName(candidate);
            txtRequestID.Text = _cand.GetCandidateRequestAcceptedFor(candidate);
            txtDeptID.Text = _cand.GetOfferDept(candidate);
            txtjobposition.Text = _cand.GetOfferJobPosition(candidate);
            string sql = "SELECT TO_CHAR(OFFERONBOARDDATE,'DD.MM.YYY') as OnBOARDDATE, " +
                "TO_CHAR(OFFERONBOARDDATE,'HH24:MI') as ONBOARDTIME, " + 
                "OFFERADDRESS FROM EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                hiddenCandidateID.Value = candidate;
                //txtOnBoardDate.Text = Convert.ToString(rd["OnBOARDDATE"]);
                txtWorkPlace.Text = Convert.ToString(rd["OFFERADDRESS"]);
                //ddlToTime.SelectedValue = Convert.ToString(rd["ONBOARDTIME"]);
            }
            else
            {
                txtOnBoardDate.Text = ""; 
            }
        }

        protected void BtnBackToOnBoard_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Main/Training.aspx");
        }
        protected void BtnOnBoardTime_Click(object sender, EventArgs e)
        {
            _db.GetConn();
            DateTime MyDate = DateTime.Now;
            string bd = txtOnBoardDate.Text + " " + ddlToTime.SelectedValue.ToString(); // 25/12/2012 21:02:44
            string onboard = txtOnBoardDate.Text = txtOnBoardDate.Text + " " + ddlToTime.SelectedValue;

            try
            {
                string sql = "UPDATE EREC_CANDIDATES SET OFFERONBOARDDATE = to_date('" + bd + "', 'dd/mm/yyyy hh24:mi:ss') " +
                    "WHERE CANDIDATEID = '" + hiddenCandidateID.Value + "'";
                OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
                cmd.ExecuteNonQuery();
                Response.Redirect("~/Main/Training.aspx");
            }

            catch (Exception ex)
            {
            }
            finally
            {
                 
            
            }
           
        }
    }
}
