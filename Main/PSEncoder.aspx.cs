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
    public partial class PSEncoder : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlErrorReq.Visible = false; 
            if (!IsPostBack)
            {
                PopulateNewEmp();
            }
        }

        private void PopulateNewEmp()
        {
            //string sql = "select UNIQUE(EREC_CANDIDATES.EMPLID), EREC_CANDIDATES.CANDIDATEID,  " +
            //         "INITCAP(EREC_CANDIDATES.LASTNAME)|| ', '|| INITCAP(EREC_CANDIDATES.FIRSTNAME) FULLNAME, " +
            //         "EREC_CANDIDATES.OFFERDEPT as DEPARTMENT, PS.PS_JOB.JOB_CODE CODE, " +
            //         //"PS.PS_JOB.JOB_BUSI_TITLE AS POSITION,  " +
            //         "TO_CHAR(EREC_CANDIDATES.OFFERONBOARDDATE,'MM/dd/yyyy') as BOARDTIME   " +
            //         "from EREC_CANDIDATES " +
            //         "JOIN PS.PS_JOB ON EREC_CANDIDATES.OFFERPOSITION = PS.PS_JOB.JOB_CODE " +
            //         "Where EREC_CANDIDATES.HIRINGSTATUS = 'On Board' ORDER BY EREC_CANDIDATES.EMPLID ASC "; 

            string sql = "select UNIQUE(EREC_CANDIDATES.EMPLID), EREC_CANDIDATES.CANDIDATEID,  " +
                        "INITCAP(EREC_CANDIDATES.LASTNAME)|| ', '|| INITCAP(EREC_CANDIDATES.FIRSTNAME) FULLNAME,  " +
                        "EREC_CANDIDATES.OFFERDEPT as DEPARTMENT, EREC_CANDIDATES.OFFERPOSITION AS POSITION,  " +
                        "TO_CHAR(EREC_CANDIDATES.OFFERONBOARDDATE,'MM/dd/yyyy') as BOARDTIME " +
                        "from EREC_CANDIDATES  Where EREC_CANDIDATES.HIRINGSTATUS = 'On Board' " +
                        "ORDER BY EREC_CANDIDATES.EMPLID ASC";


            DataTable myDT = myObjs.GetTable(sql);
            if (myDT.Rows.Count == 0)
            {
                pnlErrorReq.Visible = true;
                lblReqError.Text = "No New Employee To Encode to PeopleSoft.";
            }
            else
            {
                gridNewEmployees.DataSource = myDT;
                gridNewEmployees.DataBind();

                 
            }
        }
    }
}
