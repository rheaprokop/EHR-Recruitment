using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;


namespace EHR.Model
{
    public class EmailBody : QueryOleDB
    {
        //ObjectsMisc myMisc = new ObjectsMisc();
        
        public string EmailTo(string EmplID)
        {
            string OracleQry = "Select NAME_A From PS.PS_SUB_WCZ_AT_VW_A Where EMPLID = :EmplID";
            string FullName; 

            OleDbCommand myCommand = new OleDbCommand(OracleQry, MyConn);
            myCommand.Parameters.AddWithValue("':EmplID'", EmplID);

            OleDbDataReader myReader = myCommand.ExecuteReader();
                
                    if (myReader.Read())
                    {
                        FullName = Convert.ToString(myReader["NAME_A"]);
                    }
                    else
                    {
                        FullName = "";
                    }
                    return FullName;
                
           
        }

        public string Approver(string requestID, string FlowID, string EmplID)
        {
             
            string txtEmail;
           
            string OracleQry = "Select Requestor From Requests where RequestID = '" + requestID + "'";
            GetConn();
            OleDbCommand myCommand = new OleDbCommand(OracleQry, MyConn);
            OleDbDataReader myRead = myCommand.ExecuteReader();
            
               
               if (myRead.Read())
                {
                        string Requestor = Convert.ToString(myRead["Requestor"]);
                        string linkBox = "Recruitment/Request.aspx?form=view&flow=" + FlowID + "&id=" + requestID + "&action=1";

                        txtEmail = "<br />Dear " + GetFullName(EmplID) + ", <br /><br />Please be informed that <a href='http://ehr.qas.wcz.wistron/" + linkBox + "'>" +
                                   GetFullName(Requestor) + "</a> " +
                                   " with Request ID: " + requestID + " has applied for a Request of new Employee<br /><br />" +
                                   "<a href='http://ehr.qas.wcz.wistron/" + linkBox + "'>Visit Recruitment Approval Page</a>";
                        GetConn();
                        string InsertToInbox = "Insert into INBOXAPP (INBOXID, EMPLID, LINKURL, FLOWID, REQUESTID) " +
                                        "VALUES(INBOX_SEQ.NextVal, " +
                                       "'" + EmplID + "', '" + linkBox + "', '" +  FlowID + "', '" + requestID + "')";
                        OleDbCommand cmd = new OleDbCommand(InsertToInbox, MyConn);
                        cmd.ExecuteNonQuery();
                        
                }
               else
                {
                        txtEmail = "no content";
                }

               


            return txtEmail;  
        }

        public string RejectRequestor(string RequestID)
        {
            GetConn();
            string Requestor = "Select REQUESTOR From REQUESTS Where REQUESTID='" + RequestID + "'";
            OleDbCommand myCmd = new OleDbCommand(Requestor, MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            myRD.Read();

            string UpdateStatusReq = "Update REQUESTS set RESULT='Rejected' Where REQUESTID='" + RequestID + "'";
            OleDbCommand myStatRD = new OleDbCommand(UpdateStatusReq, MyConn);
            myStatRD.ExecuteNonQuery();

            string EmplID = Convert.ToString(myRD["REQUESTOR"]);

            string txtEmail = "<br />Dear " + GetFullName(EmplID) + ", <br /><br />Please be informed that <a href='http://ehr.qas.wcz.wistron/Recruitment/Request.aspx?form=view&id=Request.aspx?form=view&id=" + RequestID + "'>your request</a> " +
                       " with Request ID: " + RequestID + " has been rejected.<br /><br />" +
                       " " ;
            return txtEmail;
            MyConn.Close();
        }


        public string EmailApprovedRequest(string RequestID)
        {
            GetConn();
            string Requestor = "Select REQUESTOR From REQUESTS Where REQUESTID='" + RequestID + "'";
            OleDbCommand myCmd = new OleDbCommand(Requestor, MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            myRD.Read();
              
            string EmplID = Convert.ToString(myRD["REQUESTOR"]);

            string txtEmail = "<br />Dear " + GetFullName(EmplID) + ", <br /><br />Please be informed that <a href='http://ehr.qas.wcz.wistron/Recruitment/Request.aspx?form=view&id=" + RequestID + "'>your request</a> " +
                       " with Request ID: " + RequestID + " has been approved.<br /><br />";
            return txtEmail;
            MyConn.Close();
        }


        public string EmailToRecruiter(string RequestID)
        {
            GetConn();
            string Recruiter = "Select REQUESTOR, HRRECRUITER From REQUESTS Where  REQUESTID='" + RequestID + "'";
            OleDbCommand myCmd = new OleDbCommand(Recruiter, MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            myRD.Read();

            string UpdateStatusReq = "Update REQUESTS set RESULT='Approved' Where REQUESTID='" + RequestID + "'";
            OleDbCommand myStatRD = new OleDbCommand(UpdateStatusReq, MyConn);
            myStatRD.ExecuteNonQuery(); 

            string RecruiterPIC = Convert.ToString(myRD["HRRECRUITER"]);
            string EmplID = Convert.ToString(myRD["REQUESTOR"]);
            string txtEmail = "<br />Dear " + GetFullName(RecruiterPIC) + " <br /><br />Please be informed that form " + RequestID + " for new employees applied by " +
                              GetFullName(EmplID) + " was approved. <br /><br />" +
                               "You may now process the recruiting of candidate.<br /><br />" +
                               "<a href=http://ehr.qas.wcz.wistron/Recruitment/Request.aspx?form=view&flow=0&id=" + RequestID + ">View Request Details</a>";
            return txtEmail;
            MyConn.Close();
        }


        public string EmailToDeptAssistant(string RequestID)
        {

            GetConn();
            string Recruiter = "Select REQUESTOR, DEPTASSISTANT From REQUESTS Where  REQUESTID='" + RequestID + "'";
            OleDbCommand myCmd = new OleDbCommand(Recruiter, MyConn);
            OleDbDataReader myRD = myCmd.ExecuteReader();
            myRD.Read();

            string DEPTASSISTANT = Convert.ToString(myRD["DEPTASSISTANT"]);
            string EmplID = Convert.ToString(myRD["REQUESTOR"]);
            string txtEmail = "<br />Dear " + GetFullName(DEPTASSISTANT) + " <br /><br />Please be informed that you have been assign by " +
                              GetFullName(EmplID) + "  as Dept Assistant for form: " + RequestID +
                               "<br />You will be informed about recruitment process through emails.<br /><br />" +
                               "<a href=http://ehr.qas.wcz.wistron/Recruitment/Request.aspx?form=view&flow=0&id=" + RequestID + ">View Request Details</a>";
            return txtEmail;
            MyConn.Close();
        }


      

         
    }
}
