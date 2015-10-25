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
    public partial class InterviewDetails : System.Web.UI.Page
    {
        DBModel _db = new DBModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string candidateID = (string)(Session["candidate"]);
                PopulateCandidateJobs(candidateID);
                PopulateInterviewDetails(candidateID);
                btnBackToProfile.PostBackUrl = "~/Main/ViewCandidate.aspx?candidate=" + candidateID;
            }
        }


        private void PopulateCandidateJobs(string candidate)
        {
            string sql = "SELECT DISTINCT(POSITION) POSITION, CANDIDATEID FROM EREC_CANDIDATEJOBS WHERE CANDIDATEID = '" + candidate + "'";
            DataTable _dt = _db.GetTable(sql);
            gridCandidatesAppJobs.DataSource = _dt;
            gridCandidatesAppJobs.DataBind(); 

        }

        private void PopulateInterviewDetails(string candidate)
        {
            string sql = "SELECT EREC_CANDIDATEINTDETAILS.REQUESTID, EREC_CANDIDATEINTDETAILS.POSIONBOARDDATE, EREC_CANDIDATEINTDETAILS.SALARYEXP, " +
                 "EREC_CANDIDATEINTDETAILS.SUITABLEFORDEPT, EREC_CANDIDATEINTDETAILS.INTERVIEWEDBY, " +
                 "EREC_CANDIDATEINTDETAILS.INTERVIEWDATE, EREC_CANDIDATEINTDETAILS.REMARKS, " +
                 "EREC_CANDIDATES.HIRINGSTATUS, PLANT FROM EREC_CANDIDATEINTDETAILS  INNER JOIN EREC_CANDIDATES " +
                 "ON EREC_CANDIDATES.CANDIDATEID = EREC_CANDIDATEINTDETAILS.CANDIDATEID " +
                 "WHERE EREC_CANDIDATEINTDETAILS.CANDIDATEID = '" + candidate + "' ORDER BY EREC_CANDIDATEINTDETAILS.INTERVIEWDATE DESC";

            DataTable _dt = _db.GetTable(sql);
            rptInterviewDetails.DataSource = _dt;
            rptInterviewDetails.DataBind();
        }


    }
}
