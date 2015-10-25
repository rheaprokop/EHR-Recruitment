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
    public partial class SelectCandidate : System.Web.UI.UserControl
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();


        protected void Page_Load(object sender, EventArgs e)
        {
            GetRequestCandInfo();
            string RequestID = Request.QueryString["id"];
            Session["RequestID"] = RequestID;


            txtCandidateKeyword.Attributes.Add("onfocus", "this.value=''");
            if (!IsPostBack)
            {
                pnlError.Visible = false;
            }
        }

        private void GetRequestCandInfo()
        {
            myObjs.GetConn();
            string RequestID = Request.QueryString["id"];
            
            string OracleCandInfo = "Select REQUIREDPERSON From REQUESTS Where RequestID = '" + RequestID + "'";
            OleDbCommand myCMD = new OleDbCommand(OracleCandInfo, myObjs.MyConn);
            OleDbDataReader myRD = myCMD.ExecuteReader();
            if (myRD.Read())
            {
                txtRequestNo.Text = RequestID;
                txtCandidateRequired.Text = Convert.ToString(myRD["REQUIREDPERSON"]);
            }
            myObjs.MyConn.Close();
        }

        protected void SearchCandidate_Click(object sender, EventArgs e)
        {
            
            //Interviewed Candidate
            string OracleQry = "Select CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME, HIRINGSTATUS " +
                               "From CANDIDATES " +
                               "Where CANDIDATEID LIKE '%" + txtCandidateKeyword.Text + "%' OR " +
                               "FIRSTNAME LIKE '%" + txtCandidateKeyword.Text + "%' OR " +
                               "LASTNAME LIKE '%" + txtCandidateKeyword.Text + "%' ";

            OleDbCommand myCMD = new OleDbCommand(OracleQry, myObjs.MyConn); 
            
            DataTable myDT = myObjs.GetTable(OracleQry);
            gridCandidates.DataSource = myDT;
            gridCandidates.DataBind();

            if (myDT.Rows.Count == 0)
            {
                pnlError.Visible = true;
                lblError.Text = "No Candiate Record found from your search. Try another Keyword";
            }
            else
            {
                pnlError.Visible = false;
            }
        }
    }
}