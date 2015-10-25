using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EHR.Model;
using ehr_email;

namespace EHR
{
    public partial class Training : System.Web.UI.Page
    {
        ObjectsOleDB myObjs = new ObjectsOleDB();
        QueryOleDB myQry = new QueryOleDB();
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel();
        MiscModel _misc = new MiscModel();
        CandidateModel _cand = new CandidateModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            GetDateHeader();
            
            if (Page.IsPostBack == false)
            {
                GetAction();
            }

            if(IsPostBack)
                GetData();
                LoadReports(GetTodayDate(), gridToday);
                LoadReports(GetSecondDay(), gridSecondDay);
                LoadReports(GetThirdDay(), gridThirdDay);
                LoadReports(GetFourthDay(), gridFourthDay);
                LoadReports(GetOthers(), gridOthers); 
                 
        }

        private void LoadReports(string Qry, GridView grid)
        {
            DataTable myDT = myObjs.GetTable(Qry);
            grid.DataSource = myDT;
            grid.DataBind();


        }  

        private void GetDateHeader()
        {
            DateTime date = DateTime.Today;
            lblTodayDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", date).ToUpper();
            lblSecondDay.Text = String.Format("{0:dddd, MMMM d, yyyy}", date.AddDays(1)).ToUpper();
            lblThirdDay.Text = String.Format("{0:dddd, MMMM d, yyyy}", date.AddDays(2)).ToUpper();
            lblFourthDay.Text = String.Format("{0:dddd, MMMM d, yyyy}", date.AddDays(3)).ToUpper(); 
        }

        private void GetAction()
        {
            string a = Request.QueryString["action"];
            string id = Request.QueryString["candidate"];
            switch (a)
            {
                case "1":
                    Candidate_OnBoard(id);
                   // DeductFromRequest(GetRequestID(id)); 
                    Response.Redirect("~/Main/Training.aspx?action=view");
                    break;
                case "2":
                    myObjs.GetConn();
                    DidNotAttend(id);
                    DidNotComeOnBoard(id);
                    break;
                

            }
        }

        private void Candidate_OnBoard(string id)
        {
            string OracleQry = "Update EREC_CANDIDATES set HIRINGSTATUS = 'On Board' where CANDIDATEID = '" + id + "'";
            myObjs.GetConn();
            OleDbCommand Cmd = new OleDbCommand(OracleQry, myObjs.MyConn);
            Cmd.ExecuteNonQuery();

            NotifyPIC(id, "PS_ENCODER", "Notify_PS_Encoder", "HR PS Encoder");
             
        }

        private string GetRequestID(string candidate)
        {
            string requestID;
            string Oracle = "SELECT OFFERFORREQUESTID From EREC_CANDIDATES WHERE CANDIDATEID = '" + candidate + "'";
            OleDbCommand Cmd = new OleDbCommand(Oracle, myObjs.MyConn);
            OleDbDataReader myRD = Cmd.ExecuteReader();
            if (myRD.Read())
            {
                requestID = Convert.ToString(myRD["OFFERFORREQUESTID"]);
            }
            else
            {
                requestID = "No RequestID Found";
            }

            return requestID;
        }

        private void DeductFromRequest(string requestID)
        {
            int hired;

            string OraQry = "Select HIRED From EREC_REQUESTS Where REQUESTID = '" + requestID + "' ";
            OleDbCommand myCmd = new OleDbCommand(OraQry, myObjs.MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            if (myRD.Read())
            {
                hired = Convert.ToInt32(myRD["HIRED"]);
            }
            else
            {
                hired = 0;
            }

            if (hired == 0)
            {
                hired = 0;
            }
            else
            {
                hired = hired + 1;
            }

            string OraUpdate = "Update EREC_REQUESTS SET HIRED = '" + hired + "' where " +
                            "REQUESTID = '" + requestID + "'";
            OleDbCommand myCMDUp = new OleDbCommand(OraUpdate, myObjs.MyConn);
            myCMDUp.ExecuteNonQuery();

        }

        string GetTodayDate()
        {
            string sql = "select EMPLID, CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  EMAILADDRESS AS EMAIL, TO_CHAR(OFFERONBOARDDATE,'HH24:MI') as OnBOARDTIME from EREC_CANDIDATES where TO_CHAR(OFFERONBOARDDATE,'MM/dd/yyyy') = TO_CHAR(sysdate,'MM/dd/yyyy') AND HIRINGSTATUS = 'Accepted'";
            return sql;
        }

        string GetSecondDay()
        {
            string sql = "select EMPLID, CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  EMAILADDRESS AS EMAIL, TO_CHAR(OFFERONBOARDDATE,'HH24:MI') as OnBOARDTIME from EREC_CANDIDATES where TO_CHAR(OFFERONBOARDDATE,'MM/dd/yyyy') = TO_CHAR(sysdate + 1,'MM/dd/yyyy') AND HIRINGSTATUS = 'Accepted'";
            return sql;
        }

        string GetThirdDay()
        {
            string sql = "select EMPLID, CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  EMAILADDRESS AS EMAIL, TO_CHAR(OFFERONBOARDDATE,'HH24:MI') as OnBOARDTIME from EREC_CANDIDATES where TO_CHAR(OFFERONBOARDDATE,'MM/dd/yyyy') = TO_CHAR(sysdate + 2,'MM/dd/yyyy') AND HIRINGSTATUS = 'Accepted'";
            return sql;
        }

        string GetFourthDay()
        {
            string sql = "select EMPLID, CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  EMAILADDRESS AS EMAIL, TO_CHAR(OFFERONBOARDDATE,'HH24:MI') as OnBOARDTIME from EREC_CANDIDATES where TO_CHAR(OFFERONBOARDDATE,'MM/dd/yyyy') = TO_CHAR(sysdate + 3,'MM/dd/yyyy') AND HIRINGSTATUS = 'Accepted'";
            return sql;
        }

        string GetOthers()
        {
            string sql = "select EMPLID, CANDIDATEID,  LASTNAME|| ', '|| FIRSTNAME FULLNAME,  " +
                "EMAILADDRESS AS EMAIL, TO_CHAR(OFFERONBOARDDATE,'DD.MM.YYYY') as OnBOARDDATE, " +
                "TO_CHAR(OFFERONBOARDDATE,'HH24:MI') as OnBOARDTIME FROM EREC_CANDIDATES WHERE " +
                "HIRINGSTATUS = 'Accepted'  AND TO_CHAR(OFFERONBOARDDATE,'DD.MM.YYYY') < SYSDATE " +
                "OR TO_CHAR(OFFERONBOARDDATE,'DD.MM.YYYY')  > sysdate + 4";
            return sql; 
        } 

        private void DidNotAttend(string id)
        {
            string sql = "Update EREC_CANDIDATES set HIRINGSTATUS = 'Did Not Attend' Where CANDIDATEID='" + id + "'";
            OleDbCommand myCMD = new OleDbCommand(sql, myObjs.MyConn);
            myCMD.ExecuteNonQuery();
        }

        // FOR TODAY'S GRID CHECKBOXESS
        private void GetData()
        {
            ArrayList arr;
            if (ViewState["SelectedRecords"] != null)
                arr = (ArrayList)ViewState["SelectedRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)gridToday.HeaderRow
                                .Cells[0].FindControl("chkAll");
            for (int i = 0; i < gridToday.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(gridToday.DataKeys[i].Value))
                    {
                        arr.Add(gridToday.DataKeys[i].Value);
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)gridToday.Rows[i]
                                       .Cells[0].FindControl("chk");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(gridToday.DataKeys[i].Value))
                        {
                            arr.Add(gridToday.DataKeys[i].Value);
                        }
                    }
                    else
                    {
                        if (arr.Contains(gridToday.DataKeys[i].Value))
                        {
                            arr.Remove(gridToday.DataKeys[i].Value);
                        }
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
        }

        private void SetData()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)gridToday.HeaderRow
                                    .Cells[0].FindControl("chkAll");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            for (int i = 0; i < gridToday.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gridToday.Rows[i]
                                .Cells[0].FindControl("chk");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(gridToday.DataKeys[i].Value);
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
            hfCount.Value = (arr.Count - currentCount).ToString();

            NotifyPicMult("PS Encoder", "Notify_PS_Encoder_Mult", "PS Encoder", hfCount.Value);
            
        }

        protected void btnAttendance_Click(object sender, EventArgs e)
        {
            int count = 0;
            SetData();
            gridToday.AllowPaging = false;
            gridToday.DataBind();
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            count = arr.Count;
            for (int i = 0; i < gridToday.Rows.Count; i++)
            {
                if (arr.Contains(gridToday.DataKeys[i].Value))
                {
                    DeleteRecordFromPage(gridToday.DataKeys[i].Value.ToString());
                    arr.Remove(gridToday.DataKeys[i].Value);
                }
            }
            ViewState["SelectedRecords"] = arr;
            hfCount.Value = "0";
            gridToday.AllowPaging = true;
            LoadReports(GetTodayDate(), gridToday);

           
            ShowMessage(count);
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gridToday.PageIndex = e.NewPageIndex;
            gridToday.DataBind();
            SetData();
        }

        private void DeleteRecordFromPage(string candidateID)
        {
            myObjs.GetConn();
            

            Candidate_OnBoard(candidateID);
        }

        private void ShowMessage(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert('");
            sb.Append(count.ToString());
            sb.Append(" employee(s) onboard.');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(),
                            "script", sb.ToString());
        }

        private void DidNotComeOnBoard(string candidateID)
        {
            myObjs.GetConn();
            string GetRequestQry = "Select INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME, EMPLID, " +
                "OFFERFORREQUESTID, OFFERPOSITION, OFFERONBOARDDATE, OFFERDEPT " +
                "From EREC_CANDIDATES Where CANDIDATEID = '" + candidateID + "'";

            //get requestid
            OleDbCommand myCMD = new OleDbCommand(GetRequestQry, myObjs.MyConn);
            OleDbDataReader myRD = myCMD.ExecuteReader();
            if (myRD.Read())
            {
                string fullname = Convert.ToString(myRD["FULLNAME"]);
                string requestID = Convert.ToString(myRD["OFFERFORREQUESTID"]);
                string position = Convert.ToString(myRD["OFFERPOSITION"]);
                string onboarddate = Convert.ToString(myRD["OFFERONBOARDDATE"]);
                string emplid = Convert.ToString(myRD["EMPLID"]);
                string deptid = Convert.ToString(myRD["OFFERDEPT"]);

                //string txtBody = "Please be informed that CANDIDATE: " + candidateID + " DID NOT attend training.<br />" +
                //    "Please see information below: <br />" +
                //    "Name: " + fullname + "<br />" +
                //    "Employee ID: " + emplid + "<br />" +
                //    "Department: " + deptid + "<br />" +
                //    "Position: " + myQry.GetJob_Code(position) + "<br /><br />" +
                //    "EHR";

                //mail requestor 
                string subjectForRequestor = "(Requestor) On Board Information for : " + emplid;
                //MailToPIC(myQry.GetEmplEmailAddress(myQry.GetRequestor(requestID)), txtBody, subjectForRequestor);

            }

        }

        public void NotifyPicMult(string approverval, string emailtype, string to, string count)
        {
            string appemplid;
            string sql = "SELECT APPEMPLID FROM APPROVALTRANSACTION WHERE APPROVERVALUE = '" + approverval + "'";
            _db.GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rdAppemplid = cmd.ExecuteReader();
            if (_rdAppemplid.Read())
            {
                appemplid = Convert.ToString(_rdAppemplid["APPEMPLID"]);
                string[] emailto = appemplid.Split(',');

                StringBuilder sb = new StringBuilder();

                foreach (string email in emailto)
                {
                    sb.Append(_ps.GetEmplEmailAdrress(email.Trim()) + ",");
                }
                string email_addresses = sb.ToString();

                if (_misc.IsEmailNotificationTest(emailtype, "" + "", "") == false)
                {
                    string mail_type = emailtype;
                    string sender_ = "noreply@wistron.com ";

                    string recipient = email_addresses;
                    string recipient_name = to;
                    string cc = "";
                    string subject = "WCZ Recruitment - PS Encoding: EMPL ";
                    string parameters = "No of New Employee(s): " + count;

                    Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                }
            }
            else
            {
                appemplid = "";
            }

        }

        public void NotifyPIC(string candid, string approverval, string emailtype, string to)
        {
            string appemplid;
            string sql = "SELECT APPEMPLID FROM APPROVALTRANSACTION WHERE APPROVERVALUE = '" + approverval + "'";
            _db.GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rdAppemplid = cmd.ExecuteReader();
            if (_rdAppemplid.Read())
            {
                appemplid = Convert.ToString(_rdAppemplid["APPEMPLID"]);
                string[] emailto = appemplid.Split(',');

                StringBuilder sb = new StringBuilder();

                foreach (string email in emailto)
                {
                    sb.Append(_ps.GetEmplEmailAdrress(email.Trim()) + ",");
                }
                string email_addresses = sb.ToString();

                if (_misc.IsEmailNotificationTest(emailtype, "Candidate Name: " + candid, candid) == false)
                {
                    string mail_type = emailtype;
                    string sender_ = "noreply@wistron.com ";

                    string recipient = email_addresses;
                    string recipient_name = to;
                    string cc = "";
                    string subject = "WCZ Recruitment - PS Encoding: EMPL " + _cand.GetCandidateEmplid(candid);
                    string parameters = "Candidate Name: " + _cand.GetCandidateName(candid) + ";" +
                                        "Candidate Employee ID: " + _cand.GetCandidateEmplid(candid) + ";" +
                                        "Candidate Position: " + _cand.GetOfferJobPosition(candid) + ";" +
                                        "<br /><br /><a href=" + _misc.GetWebAppRoot() + "/Main/PSEncoder.aspx>Go To Employee Table</a>";

                    Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                }
            }
            else
            {
                appemplid = "";
            }

        }
  

       // private void EmailToPIC(string candidateID) // requestor, dept manager and ps encoder
       // {
            //myObjs.GetConn();
            //string GetRequestQry = "Select INITCAP(LASTNAME)|| ', '|| INITCAP(FIRSTNAME) FULLNAME, EMPLID, " + 
            //    "OFFERFORREQUESTID, OFFERPOSITION, OFFERONBOARDDATE, OFFERDEPT " +
            //    "From EREC_CANDIDATES Where CANDIDATEID = '" + candidateID + "'";

            ////get requestid
            //OleDbCommand myCMD = new OleDbCommand(GetRequestQry, myObjs.MyConn);
            //OleDbDataReader myRD = myCMD.ExecuteReader();
            //if (myRD.Read())
            //{
            //    string fullname = Convert.ToString(myRD["FULLNAME"]);
            //    string requestID = Convert.ToString(myRD["OFFERFORREQUESTID"]);
            //    string position = Convert.ToString(myRD["OFFERPOSITION"]);
            //    string onboarddate = Convert.ToString(myRD["OFFERONBOARDDATE"]);
            //    string emplid = Convert.ToString(myRD["EMPLID"]);
            //    string deptid = Convert.ToString(myRD["OFFERDEPT"]);

            //    string txtBody = "Please be informed that CANDIDATE: " + candidateID + " has attended the training.<br />" +
            //        "Please see information below: <br />" +
            //        "Name: " + fullname + "<br />" +
            //        "Employee ID: " + emplid + "<br />" +
            //        "Department: " + deptid + "<br />" +
            //        "Position: " + myQry.GetJob_Code(position) + "<br /><br />" +
            //        "EHR";
                
            //    //mail requestor 
            //    string subjectForRequestor = "(Requestor) On Board Information for : " + emplid;
            //    MailToPIC(myQry.GetEmplEmailAddress(myQry.GetRequestor(requestID)), txtBody, subjectForRequestor);

            //    //mail to ps encoder
            //    string psencoder;
            //    string PSQry = "Select PSENCODER From REQUESTS where REQUESTID = '" + requestID + "'";
            //    OleDbCommand mycmd = new OleDbCommand(PSQry, myObjs.MyConn);
            //    OleDbDataReader myrd = mycmd.ExecuteReader();
            //    if (myrd.Read())
            //    {
            //        psencoder = Convert.ToString(myrd["PSENCODER"]);
            //    }
            //    else
            //    {
            //        psencoder = "";
            //    }
            //    string subjectForEncoder = "(PS Encoder) On Board Information for : " + emplid;
            //    MailToPIC(myQry.GetEmplEmailAddress(psencoder), txtBody, subjectForEncoder);

            //}
       // }

       
    }
}
