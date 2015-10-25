using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Web.Mail;
using System.Text;
using EHR.Model;

namespace EHR.Model
{
    public class ObjectsMisc
    {
        QueryOleDB myQueries = new QueryOleDB();
        EmailBody myEmailBody = new EmailBody();
         
        public string CreateRequestID()
        {
            string YearNow = DateTime.Today.ToString("yy");
            string MonthNow = String.Format("{0:MM}", DateTime.Now).ToString();
            string RequestID;
            string LatestRequestID = myQueries.GetLastRequestID(); // GET THE LAST REQUEST ID ex. RQ1109001
           
            if (LatestRequestID != "")
            {
                string SufRequestID = LatestRequestID.Substring(6);    // GET THE LAST 3 DIGIT of Request ID (ex. 001)
                string latestYearMonth = LatestRequestID.Substring(2, 4); 
                int SuffixRequest = Convert.ToInt32(SufRequestID);
                int SuffixRequestVal = SuffixRequest + 1;
                string SuffixToString = Convert.ToString(SuffixRequestVal);
                string SuffixReqID = SuffixToString.PadLeft(3, '0');

                if (YearNow + MonthNow == latestYearMonth)
                {
                    RequestID = "RQ" + YearNow + MonthNow + SuffixReqID;
                }
                else
                {
                    RequestID = "RQ" + YearNow + MonthNow + "001";
                }
         
            }
            else
            {
                RequestID = "RQ" + YearNow + MonthNow + "001";
            }


            return RequestID;
             
        }

        private string YearMonth()
        {
            return DateTime.Now.ToString("yyMM");
        }

        public string CreateCandidateID()
        {
            string YearNow = DateTime.Today.ToString("yy");
            string MonthNow = String.Format("{0:MM}", DateTime.Now).ToString();
            string CandidateID;
            string LatestCandidateID = myQueries.GetLastCandidateID();// GET THE LAST CANDIDATE ID ex. CAN1109001

            if (LatestCandidateID != "")
            {
                string SufCandidateID = LatestCandidateID.Substring(6);    // GET THE LAST 3 DIGIT of Request ID (ex. 001)
                int SuffixCandidate = Convert.ToInt32(SufCandidateID);
                string latestYearMonth = LatestCandidateID.Substring(2, 4); 
                int SuffixCandidateVal = SuffixCandidate + 1;
                string SuffixToString = Convert.ToString(SuffixCandidateVal);
                string SuffixCanID = SuffixToString.PadLeft(3, '0');

                if (YearNow + MonthNow == latestYearMonth)
                {
                    CandidateID = "CD" + YearNow + MonthNow + SuffixCanID;
                }
                else
                {
                    CandidateID = "CD" + YearNow + MonthNow + "001";
                }
            }
            else
            {
                CandidateID = "CD" + YearNow + MonthNow + "001";
            }
            return CandidateID;
        }


        public string FirstEmplID() // For First Candidate
        {
            string EmployeeID;
            string YearNow = DateTime.Today.ToString("yy");
            string MonthNow = String.Format("{0:MM}", DateTime.Now).ToString();

            string LatestEmployeeID = myQueries.GetLastEmplID(); // GET THE LAST EMPLID

            if (LatestEmployeeID != "")
            {
                string SufEmpID = LatestEmployeeID.Substring(6);    // GET THE LAST 3 DIGIT of Request ID (ex. 001)
                int SuffixEmp = Convert.ToInt32(SufEmpID);
                int SuffixEmpVal = SuffixEmp + 1;
                string SuffixToString = Convert.ToString(SuffixEmpVal);
                string SuffixEmpID = SuffixToString.PadLeft(3, '0');
                string latestYearMonth = LatestEmployeeID.Substring(1, 4);
                if (YearNow + MonthNow == latestYearMonth)
                {
                    EmployeeID = "C" + YearNow + MonthNow + SuffixEmpID;
                }
                else
                {
                    EmployeeID = "C" + YearNow + MonthNow + "001";
                }

                
            }
            else
            {
                EmployeeID = "C" + YearNow + MonthNow + "001";
            }
            return EmployeeID;
        }

        public string GenerateEmployeeID()
        {
            string EmployeeID;
            string YearNow = DateTime.Today.ToString("yy");
            string MonthNow = String.Format("{0:MM}", DateTime.Now).ToString();
            string LatestEmployeeID = myQueries.GetLastEmplFromCand();

            if (LatestEmployeeID != "")
            {
                string SufEmpID = LatestEmployeeID.Substring(6);    // GET THE LAST 3 DIGIT of Request ID (ex. 001)
                int SuffixEmp = Convert.ToInt32(SufEmpID);
                int SuffixEmpVal = SuffixEmp + 1;
                string SuffixToString = Convert.ToString(SuffixEmpVal);
                string SuffixEmpID = SuffixToString.PadLeft(3, '0');
                string latestYearMonth = LatestEmployeeID.Substring(1, 4);

                if (YearNow + MonthNow == latestYearMonth)
                {
                    EmployeeID = "C" + YearNow + MonthNow + SuffixEmpID;
                }
                else
                {
                    EmployeeID = "C" + YearNow + MonthNow + "001";
                }
                
            }
            else
            {
                EmployeeID = "C" + YearNow + MonthNow + "001";
            }
            return EmployeeID;

        }


        public string ConvertDateFormat(DateTime d)
        {   
            //valid format is 12/25/2011;
             
            string month = d.Month.ToString();
            string day =  d.Day.ToString();
            string year = d.Year.ToString();

            string newDate = month + "/" + day + "/" + year;
            return newDate;
        }

        

        //Get First Step 

        private string FirstStepFlow(string IsAgency)
        {
            string OracleQry = "Select FlowID from ApprovalFlow Where IsAgency = :IsAgency And Status = '1' And RowNum = 1" + 
                            " Order by FlowID Asc";
            
            string flowStart;
            myQueries.GetConn();
            using (OleDbCommand myCommand = new OleDbCommand(OracleQry, myQueries.MyConn))
            {
                myCommand.Parameters.AddWithValue("':IsAgency'", IsAgency);

                using (OleDbDataReader myReader = myCommand.ExecuteReader())
                {
                    myReader.Read();
                    flowStart = Convert.ToString(myReader["FlowID"]);
                    return flowStart;
                }
            }
        }

        public void ApprovalFlow(string deptID, string IsAgency, string RequestID)
        {
            myQueries.GetConn();
            

            if (IsAgency == "0") // if request is NOT for agency
            {

                //IF SITE SUPPORT

                string OraGetSS = "Select ISSITESUPPORT From REQUESTS Where REQUESTID = '" + RequestID + "'";
                OleDbCommand myCMDSS = new OleDbCommand(OraGetSS, myQueries.MyConn);
                OleDbDataReader myRDSS = myCMDSS.ExecuteReader();
                myRDSS.Read();
                string ISSITESUPPORT = Convert.ToString(myRDSS["ISSITESUPPORT"]);

                if (ISSITESUPPORT == "YES") // ex: MD0L00
                {

                    //count steps if NOT AGENCY 
                    string OracleSteps = "Select count(FlowID) as NoOfSteps From ApprovalFlow Where IsAgency = :IsAgency And Status = 1 And FlowID != 4";
                    using (OleDbCommand myCmd = new OleDbCommand(OracleSteps, myQueries.MyConn))
                    {
                        myCmd.Parameters.AddWithValue("':IsAgency'", IsAgency);
                        int NoOfSteps = Convert.ToInt32(myCmd.ExecuteScalar());


                        for (int i = 1; i <= NoOfSteps; i++)
                        {
                            //get no of steps 
                            string OracleNoSteps = "select * from " +
                            "(select rownum  rown, FlowID, Flow, Assignment, PIC, FRMPIC  from ApprovalFlow WHERE isAGENCY = '0' And Status = '1'  And FlowID != 4 Order By FlowID ASC) " +
                            "where rown = '" + i + "'";

                            myQueries.GetConn();
                            using (OleDbCommand myCmdNoOfSteps = new OleDbCommand(OracleNoSteps, myQueries.MyConn))
                            {
                                using (OleDbDataReader myReadNoOfSteps = myCmdNoOfSteps.ExecuteReader())
                                {
                                    if (myReadNoOfSteps.Read())
                                    {
                                        string assignment = Convert.ToString(myReadNoOfSteps["ASSIGNMENT"]);

                                        string FlowIDStep = Convert.ToString(myReadNoOfSteps["FlowID"]);

                                        string pic;

                                        if (assignment == "1")     // if pic is declared
                                        {
                                            pic = Convert.ToString(myReadNoOfSteps["PIC"]);
                                        }
                                        else //if the pic is based from form
                                        {

                                            string FrmPIC = Convert.ToString(myReadNoOfSteps["FRMPIC"]);
                                            string OracleFrmPIC;

                                            switch (FrmPIC)
                                            {
                                                ///the content are from base data
                                                case "1":  // Requestor's Department Manager (just get requestor's manager ID in PS)
                                                    OracleFrmPIC = "Select REQUESTORDEPTID from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                    OleDbCommand myCmd1 = new OleDbCommand(OracleFrmPIC, myQueries.MyConn);
                                                    OleDbDataReader myRead1 = myCmd1.ExecuteReader();
                                                    if (myRead1.Read())
                                                    {
                                                        //DEPT ID OF REQUESTOR
                                                        string deptid = Convert.ToString(myRead1["REQUESTORDEPTID"]);

                                                        //GET MANAGER ID OF /*deptid*/
                                                        string ManagerQry = "Select MANAGER_ID from PS.PS_SUB_WCZORG_VW_A Where DEPTID = '" + deptid + "'";
                                                        try
                                                        {
                                                        OleDbCommand cmdManagerQry = new OleDbCommand(ManagerQry, myQueries.MyConn);
                                                        OleDbDataReader rdManagerQry = cmdManagerQry.ExecuteReader();

                                                            if (rdManagerQry.Read())
                                                            {
                                                                pic = Convert.ToString(rdManagerQry["MANAGER_ID"]);
                                                            }
                                                            else
                                                            {
                                                                pic = "";
                                                            }
                                                        }
                                                        catch(OleDbException ex)
                                                        {
                                                            pic = "";
                                                        }
                                                       
                                                        //pic = "C0709033"; // For Testing
                                                    }
                                                    else
                                                    {
                                                        pic = "";
                                                    }
                                                    break;
                                                case "2":  //Request's Department Manager (get manager id of dept in ps_sub_wczorg_vw_a table)
                                                    OracleFrmPIC = "Select DEPTCODE from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                    pic = "C1109001"; // temporary for qas
                                                    break;
                                                case "3":  //Request's Plant Manager (Except Site Support)
                                                    OracleFrmPIC = "Select PLANT from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                    pic = "C1109001";  // temporary for qas
                                                    break;
                                                default:
                                                    OracleFrmPIC = "";
                                                    pic = "";
                                                    break;
                                            }

                                        }

                                        string OracleInsertStep = "Insert into REQAPPROVALLOGS (APPROVALID, REQUESTID, FLOWID, APPROVALSTATUS, PIC)" +
                                              "Values (ApprovalLog_Seq.NextVal, '" + RequestID + "', '" + FlowIDStep + "', '0', '" + pic + "')";


                                        myQueries.GetExecuteNonQuery(OracleInsertStep);
                                    }
                                }
                            }

                        }
                        myQueries.MyConn.Close();
                    }
                }

                else
                { //if not site support
                    //count steps if NOT AGENCY 
                    string OracleSteps = "Select count(FlowID) as NoOfSteps From ApprovalFlow Where IsAgency = :IsAgency And Status = 1";
                    using (OleDbCommand myCmd = new OleDbCommand(OracleSteps, myQueries.MyConn))
                    {
                        myCmd.Parameters.AddWithValue("':IsAgency'", IsAgency);
                        int NoOfSteps = Convert.ToInt32(myCmd.ExecuteScalar());


                        for (int i = 1; i <= NoOfSteps; i++)
                        {
                            //get no of steps 
                            string OracleNoSteps = "select * from " +
                            "(select rownum  rown, FlowID, Flow, Assignment, PIC, FRMPIC  from ApprovalFlow WHERE isAGENCY = '0' And Status = '1'  Order By FlowID ASC) " +
                            "where rown = '" + i + "'";

                            myQueries.GetConn();
                            using (OleDbCommand myCmdNoOfSteps = new OleDbCommand(OracleNoSteps, myQueries.MyConn))
                            {
                                using (OleDbDataReader myReadNoOfSteps = myCmdNoOfSteps.ExecuteReader())
                                {
                                    if (myReadNoOfSteps.Read())
                                    {
                                        string assignment = Convert.ToString(myReadNoOfSteps["ASSIGNMENT"]);

                                        string FlowIDStep = Convert.ToString(myReadNoOfSteps["FlowID"]);

                                        string pic;

                                        if (assignment == "1")     // if pic is declared
                                        {
                                            pic = Convert.ToString(myReadNoOfSteps["PIC"]);
                                        }
                                        else //if the pic is based from form
                                        {

                                            string FrmPIC = Convert.ToString(myReadNoOfSteps["FRMPIC"]);
                                            string OracleFrmPIC;

                                            switch (FrmPIC)
                                            {
                                                ///the content are from base data
                                                case "1":  // Requestor's Department Manager
                                                    OracleFrmPIC = "Select REQUESTORDEPTID from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                    OleDbCommand myCmd1 = new OleDbCommand(OracleFrmPIC, myQueries.MyConn);
                                                    OleDbDataReader myRead1 = myCmd1.ExecuteReader();
                                                    if (myRead1.Read())
                                                    {
                                                        string deptid = Convert.ToString(myRead1["REQUESTORDEPTID"]);

                                                        //GET MANAGER ID OF /*deptid*/
                                                        string ManagerQry = "Select MANAGER_ID from PS.PS_SUB_WCZORG_VW_A Where DEPTID = '" + deptid + "'";
                                                        try
                                                        {
                                                        OleDbCommand cmdManagerQry = new OleDbCommand(ManagerQry, myQueries.MyConn);
                                                        OleDbDataReader rdManagerQry = cmdManagerQry.ExecuteReader();
                                                            if (rdManagerQry.Read())
                                                            {
                                                                pic = Convert.ToString(rdManagerQry["MANAGER_ID"]);
                                                            }
                                                            else
                                                            {
                                                                pic = "";
                                                            }
                                                        }
                                                        catch (OleDbException ex)
                                                        {
                                                            pic = "";
                                                        }
                                                        
                                                       // pic = "C0709033";  // Use On Testing
                                                    }
                                                    else
                                                    {
                                                        pic = "";
                                                    }
                                                    break;
                                                case "2":  //Request's Department Manager
                                                    OracleFrmPIC = "Select DEPTCODE from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                    pic = "C0709033";
                                                    break;
                                                case "3":  //Request's Plant Manager (Except Site Support)
                                                    OracleFrmPIC = "Select PLANT from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                    pic = "C1109001";
                                                    break; 
                                                default:
                                                    OracleFrmPIC = "";
                                                    pic = "";
                                                    break;
                                            }

                                        }

                                        string OracleInsertStep = "Insert into REQAPPROVALLOGS (APPROVALID, REQUESTID, FLOWID, APPROVALSTATUS, PIC)" +
                                              "Values (ApprovalLog_Seq.NextVal, '" + RequestID + "', '" + FlowIDStep + "', '0', '" + pic + "')";


                                        myQueries.GetExecuteNonQuery(OracleInsertStep);
                                    }
                                }
                            }

                        }
                        myQueries.MyConn.Close();
                    }

                }//if not site support

                

            }
            else if (IsAgency == "1") // if request is for agency 
            {
                //count steps if NOT AGENCY 
                string OracleStepsA = "Select count(FlowID) as NoOfSteps From APPROVALFLOWFORAGENCY Where IsAgency = '1' And Status = 1";
                using (OleDbCommand myCmdA = new OleDbCommand(OracleStepsA, myQueries.MyConn))
                {
                    
                    int NoOfStepsA = Convert.ToInt32(myCmdA.ExecuteScalar());


                    for (int i = 1; i <= NoOfStepsA; i++)
                    {
                        //get no of steps 
                        string OracleNoStepsA = "select * from " +
                        "(select rownum  rown, FlowID, Flow, Assignment, PIC, FRMPIC  from APPROVALFLOWFORAGENCY WHERE isAGENCY = '1' And Status = '1'  Order By FlowID ASC) " +
                        "where rown = '" + i + "'";

                        myQueries.GetConn();
                        using (OleDbCommand myCmdNoOfStepsA = new OleDbCommand(OracleNoStepsA, myQueries.MyConn))
                        {
                            using (OleDbDataReader myReadNoOfStepsA = myCmdNoOfStepsA.ExecuteReader())
                            {
                                if (myReadNoOfStepsA.Read())
                                {
                                    string assignment = Convert.ToString(myReadNoOfStepsA["ASSIGNMENT"]);

                                    string FlowIDStepA = Convert.ToString(myReadNoOfStepsA["FlowID"]);

                                    string picA;

                                    if (assignment == "1")     // if pic is declared
                                    {
                                        picA = Convert.ToString(myReadNoOfStepsA["PIC"]);
                                    }
                                    else //if the pic is based from form
                                    {

                                        string FrmPIC = Convert.ToString(myReadNoOfStepsA["FRMPIC"]);
                                        string OracleFrmPIC;

                                        switch (FrmPIC)
                                        {
                                            ///the content are from base data
                                            case "1":  // Requestor's Department Manager
                                                OracleFrmPIC = "Select REQUESTORDEPTID from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                OleDbCommand myCmd1 = new OleDbCommand(OracleFrmPIC, myQueries.MyConn);
                                                OleDbDataReader myRead1 = myCmd1.ExecuteReader();
                                                if (myRead1.Read())
                                                {
                                                    string deptid = Convert.ToString(myRead1["REQUESTORDEPTID"]);
                                                    //GET MANAGER ID OF /*deptid*/
                                                    string ManagerQry = "Select MANAGER_ID from PS.PS_SUB_WCZORG_VW_A Where DEPTID = '" + deptid + "'";
                                                    try
                                                    {
                                                    OleDbCommand cmdManagerQry = new OleDbCommand(ManagerQry, myQueries.MyConn);
                                                    OleDbDataReader rdManagerQry = cmdManagerQry.ExecuteReader();
                                                        if (rdManagerQry.Read())
                                                        {
                                                            picA = Convert.ToString(rdManagerQry["MANAGER_ID"]);
                                                        }
                                                        else
                                                        {
                                                            picA = "";
                                                        }
                                                    }
                                                    catch (OleDbException ex)
                                                    {
                                                        picA = "";
                                                    }
                                                    
                                                    
                                                    //picA = "C0709033";
                                                }
                                                else
                                                {
                                                    picA = "";
                                                }
                                                break;
                                            case "2":  //Request's Department Manager
                                                OracleFrmPIC = "Select DEPTCODE from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                picA = "C0709033";
                                                break;
                                            case "3":  //Request's Plant Manager
                                                OracleFrmPIC = "Select PLANT from REQUESTS WHERE REQUESTID='" + RequestID + "'";
                                                picA = "C1109001";
                                                break;
                                            default:
                                                OracleFrmPIC = "";
                                                picA = "";
                                                break;
                                        }

                                    }

                                    string OracleInsertStepA = "Insert into REQAPPROVALLOGS (APPROVALID, REQUESTID, FLOWID, APPROVALSTATUS, PIC)" +
                                          "Values (ApprovalLog_Seq.NextVal, '" + RequestID + "', '" + FlowIDStepA + "', '0', '" + picA + "')";


                                    OleDbCommand MyStepsA = new OleDbCommand(OracleInsertStepA, myQueries.MyConn);
                                    MyStepsA.ExecuteNonQuery();
                                }
                            }
                        }

                    }
                    myQueries.MyConn.Close();
                }
            } 
        }

        public void GetNextApprovalStep(string requestID)
        {
            if (IsFinishedApproval(requestID) != 0) // IF THERE IS STILL APPROVER LEFT FOR THIS CERTAIN REQUEST ID
            {
                string FlowID;
                string OracleStatus = "Select FlowID from ReqApprovallogs where RequestID = '" + requestID + "'" +
                        " And ApprovalStatus = '0' And Rownum = '1' Order by FlowID Asc";

                myQueries.GetConn();

                OleDbCommand myCommand = new OleDbCommand(OracleStatus, myQueries.MyConn);
                myCommand.Parameters.AddWithValue("':requestID'", requestID);
                OleDbDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read())
                {
                    FlowID = Convert.ToString(myReader["FlowID"]);
                    string pic = GetApprover(FlowID, requestID);
                    string TxtSubject = "";
                    if (IsAgencyFromRequest(requestID) == "0")
                    {
                        //GET APPROVAL FLOW NON AGENCY
                         TxtSubject = "Select FLOW From APPROVALFLOW Where FLOWID = '" + FlowID + "'";

                    }
                    if (IsAgencyFromRequest(requestID) == "1")
                    {
                        //GET APPROVAL FLOW NON AGENCY
                         TxtSubject = "Select FLOW From APPROVALFLOWFORAGENCY Where FLOWID = '" + FlowID + "'";
                    }
                    

                    OleDbCommand mysub = new OleDbCommand(TxtSubject, myQueries.MyConn);
                    OleDbDataReader mysubread = mysub.ExecuteReader();
                    mysubread.Read();


                    string subject = Convert.ToString(mysubread["FLOW"]);

                    //GET EMAIL ADD OF THE PIC
                    OleDbCommand MyCmdEmail = new OleDbCommand(myQueries.GetEmplEmail(pic), myQueries.MyConn);
                    OleDbDataReader MyReadEmail = MyCmdEmail.ExecuteReader();

                    if (MyReadEmail.Read())
                    {
                        string EmailAdd = Convert.ToString(MyReadEmail["Email_Address_A"]);
                        string Body = myEmailBody.Approver(requestID, FlowID, pic);

                        SendEmail("wczaptest@wistron.com", pic, requestID, Body, subject);
                        
                    }

                }

            }

            else // If there are still more approver left for the request id
            {
                
                 
                string OraStatus = "Update REQUESTS set RESULT = 'RECRUITMENT' " + 
                            "Where REQUESTID ='" + requestID + "'";
                myQueries.GetExecuteScalar(OraStatus);
                myQueries.GetConn();
                string OracleGetRec = "Select HRRECRUITER From REQUESTS " + 
                                "Where REQUESTID ='" + requestID + "'";
                OleDbCommand myCmd = new OleDbCommand(OracleGetRec, myQueries.MyConn);
                OleDbDataReader myRD = myCmd.ExecuteReader();
                myRD.Read();
                string recruiter = Convert.ToString(myRD["HRRECRUITER"]);

                

                //send email to requestor that request has been approved and is the process of recruitment 
                myEmailBody.EmailApprovedRequest(requestID);
                //send notification to hr recruiter
                OleDbCommand MyCmdEmail = new OleDbCommand(myQueries.GetEmplEmail(recruiter), myQueries.MyConn);
                OleDbDataReader MyReadEmail = MyCmdEmail.ExecuteReader();
                if (MyReadEmail.Read())
                {
                    string EmailAdd = Convert.ToString(MyReadEmail["Email_Address_A"]);
                    string subject = "Recruitment Process: " + requestID;
                    string Body = myEmailBody.EmailToRecruiter(requestID);
                    //insert alert here
                    string txtAlert = "You have a request to approve: Request ID:" + requestID;
                   
                    SendEmail(EmailAdd, recruiter, requestID, Body, subject);
                }
            }

                    
                
           
            myQueries.MyConn.Close();

        }

        public string GetApprover(string FlowID, string requestID)
        {
            //get the next approver
            string OracleQry = "Select PIC from ReqApprovallogs WHERE FlowID = '" + FlowID +"'" +
                               "and REQUESTID='" + requestID + "' And APPROVALSTATUS = '0'";
            myQueries.GetConn();
            OleDbCommand myCMD = new OleDbCommand(OracleQry, myQueries.MyConn);
            OleDbDataReader myRD = myCMD.ExecuteReader();

            myRD.Read();
             string PIC = Convert.ToString(myRD["PIC"]);
             return PIC;
            

           
        }

        private int IsFinishedApproval(string RequestID)
        {
            string OracleStatus = "Select count(FlowID) from ReqApprovallogs where RequestID = '" + RequestID + "'" +
                    " And ApprovalStatus = '0' And Rownum = '1' Order by FlowID Asc";
            myQueries.GetConn();

            OleDbCommand myCMD = new OleDbCommand(OracleStatus, myQueries.MyConn);
            int approval = Convert.ToInt32(myCMD.ExecuteScalar());
            return approval; 

        }

        public void SendEmail(string EmailAdd, string EmplID, string RequestID, string txt, string subject)
        {
            try
            {
                string FullName = EmplID;
                MailMessage objMail = new MailMessage();
                objMail.From = "EHR <Rhea_Prokop@wistron.com>";
                objMail.To = "wczaptest@wistron.com";
                objMail.Subject = "EHR Notification for:" + RequestID + "-" + subject;
                objMail.BodyFormat = MailFormat.Html;
                objMail.Body = txt;
                SmtpMail.SmtpServer = "wczmail.wistron.com";
                SmtpMail.Send(objMail);
            }
            catch (Exception ex)
            {
                string FullName = EmplID;
                MailMessage objMail = new MailMessage();
                objMail.From = "EHR <Rhea_Prokop@wistron.com>";
                objMail.To = "wczaptest@wistron.com";
                objMail.Subject = "EHR Notification for:" + RequestID + "-" + subject;
                objMail.BodyFormat = MailFormat.Html;
                objMail.Body = "Error Msg for Approval: " + ex;
                SmtpMail.SmtpServer = "wczmail.wistron.com";
                SmtpMail.Send(objMail);
            }
        }


        public void SendJobOfferEmail(string toEmail, string toName, string position, string txt)
        {
            try
            {
                MailMessage objMail = new MailMessage();
                objMail.From = "EHR <Rhea_Prokop@wistron.com>";
                objMail.To = toEmail;
                objMail.Subject = "Job Acceptance Letter: " + position;
                objMail.BodyFormat = MailFormat.Html;
                objMail.Body = txt;
                SmtpMail.SmtpServer = "wczmail.wistron.com";
                SmtpMail.Send(objMail);
            }
            catch (Exception ex)
            {
                string Message = "<br /><br />Error:" + ex.Message;
                MailMessage objMail = new MailMessage();
                objMail.From = "EHR <Rhea_Prokop@wistron.com>";
                objMail.To = "EHR <Rhea_Prokop@wistron.com>";
                objMail.Subject = "Error Sending: Job Acceptance Letter: " + position;
                objMail.BodyFormat = MailFormat.Html;
                objMail.Body = "Error Sending Email: " + txt + Message;
                SmtpMail.SmtpServer = "wczmail.wistron.com";
                SmtpMail.Send(objMail);
            }
        }

        public void SendAcceptanceInfo(string toEmail, string toName, string requestID, string txt)
        {

            MailMessage objMail = new MailMessage();
            objMail.From = "EHR <Rhea_Prokop@wistron.com>";
            objMail.To = toName + " <" + toEmail + ">";
            objMail.Subject = "Candidate for " + requestID + " was acceppted.";
            objMail.BodyFormat = MailFormat.Html;
            objMail.Body = txt;
            SmtpMail.SmtpServer = "wczmail.wistron.com";
            SmtpMail.Send(objMail);
        }

        public void SendExternalEmail(string requestID, string candidateID)
        {
            string position;
            string recruiter;
            string candidateName;
            string hremail;
            myQueries.GetConn();
            //Get Request Info
            string OracleReqInfo = "Select JOBBUSITITLE  From REQUESTS Where REQUESTID='" + requestID + "'";
            OleDbCommand myCMD = new OleDbCommand(OracleReqInfo, myQueries.MyConn);
            OleDbDataReader myRD = myCMD.ExecuteReader();
            myRD.Read();

            position = Convert.ToString(myRD["JOBBUSITITLE"]); 

            // Get Candidate Info
            string OracleCandInfo = "Select FIRSTNAME, LASTNAME, EMAILADDRESS From CANDIDATES Where CANDIDATEID = '" + candidateID + "'";
            OleDbCommand myCMDCand = new OleDbCommand(OracleCandInfo, myQueries.MyConn);
            OleDbDataReader myRDCand = myCMDCand.ExecuteReader();
            myRDCand.Read();
            
            string Firstname = Convert.ToString(myRDCand["FIRSTNAME"]);
            string Lastname = Convert.ToString(myRDCand["LASTNAME"]);
            string emailaddcand = Convert.ToString(myRDCand["EMAILADDRESS"]);
            candidateName = Firstname + " " + Lastname;
            myQueries.GetConn();
            //Get InterviewInfo 
            string OracleInv = "Select TO_CHAR(INTERVIEWDATE, 'dd.mm.yyyy hh:mi A.M.') as INTERVIEW, HRRECRUITER from INTERVIEWINVITE " +
                "Where INTERVIEWINVID = (Select max(INTERVIEWINVID) from INTERVIEWINVITE)  AND CANDIDATEID = '" + candidateID + "' AND REQUESTID = '" + requestID + "'";
            OleDbCommand myCMDInv = new OleDbCommand(OracleInv, myQueries.MyConn);
            OleDbDataReader myRDInv = myCMDInv.ExecuteReader();
            myRDInv.Read();

            string InterviewDate = Convert.ToString(myRDInv["INTERVIEW"]);
            string recruiterid = Convert.ToString(myRDInv["HRRECRUITER"]);
            recruiter = myQueries.GetFullName(recruiterid);
            myQueries.GetConn();
            string OraGetEmail = myQueries.GetEmplEmail(recruiterid);
            OleDbCommand myComEma = new OleDbCommand(OraGetEmail, myQueries.MyConn);
            OleDbDataReader myRDEma = myComEma.ExecuteReader();
            myRDEma.Read();
            hremail = Convert.ToString(myRDEma["EMAIL_ADDRESS_A"]);

 
           


        }

        public void SendEmailToInterviewer(string to, string PIC, string candidateID, string requestID )
        {
            string candidateName;
            string position; 
            string ToName;
            string recruiter;

            myQueries.GetConn();

            ToName = myQueries.GetFullName(to);
            myQueries.GetConn();
            //Get Request Info
            string OracleReqInfo = "Select JOBBUSITITLE  From REQUESTS Where REQUESTID='" + requestID + "'";
            OleDbCommand myCMD = new OleDbCommand(OracleReqInfo, myQueries.MyConn);
            OleDbDataReader myRD = myCMD.ExecuteReader();
            myRD.Read();

            position = Convert.ToString(myRD["JOBBUSITITLE"]);


            // Get Candidate Info
            string OracleCandInfo = "Select FIRSTNAME, LASTNAME, EMAILADDRESS From CANDIDATES Where CANDIDATEID = '" + candidateID + "'";
            OleDbCommand myCMDCand = new OleDbCommand(OracleCandInfo, myQueries.MyConn);
            OleDbDataReader myRDCand = myCMDCand.ExecuteReader();
            myRDCand.Read();
            string Firstname = Convert.ToString(myRDCand["FIRSTNAME"]);
            string Lastname = Convert.ToString(myRDCand["LASTNAME"]);
            string emailaddcand = Convert.ToString(myRDCand["EMAILADDRESS"]);
            candidateName = Firstname + " " + Lastname;

            string OracleInv = "Select TO_CHAR(INTERVIEWDATE, 'dd.mm.yyyy hh:mi A.M.') as INTERVIEW, HRRECRUITER from INTERVIEWINVITE " +
                "Where INTERVIEWINVID = (Select max(INTERVIEWINVID) from INTERVIEWINVITE)  AND CANDIDATEID = '" + candidateID + "' AND REQUESTID = '" + requestID + "' ORDER BY INTERVIEWINVID DESC";
            OleDbCommand myCMDInv = new OleDbCommand(OracleInv, myQueries.MyConn);
            OleDbDataReader myRDInv = myCMDInv.ExecuteReader();
            myRDInv.Read();

            string InterviewDate = Convert.ToString(myRDInv["INTERVIEW"]);
            string recruiterid = Convert.ToString(myRDInv["HRRECRUITER"]);
            recruiter = myQueries.GetFullName(recruiterid);

            

           
        }

        private string IsAgencyFromRequest(string requestID)
        {
            myQueries.GetConn();
            string s;
            string OracleReq = "Select IsAgency From Requests where REQUESTID='" + requestID + "'";
            OleDbCommand myReq = new OleDbCommand(OracleReq, myQueries.MyConn);
            OleDbDataReader myRD = myReq.ExecuteReader();
            if (myRD.Read())
            {
                s = Convert.ToString(myRD["IsAgency"]);

            }
            else
            {
                s = "not found";
            }
            return s;
        }

        public void EmailForChanges(string requestID, string txt, string email)
        {
            myQueries.GetConn(); 

            MailMessage objMail = new MailMessage();
            objMail.From = "EHR <Rhea_Prokop@wistron.com>";
            objMail.To = myQueries.GetRequestor(requestID) + " <" + email + ">";
            objMail.Subject = "Required changes for Request: " + requestID;
            objMail.BodyFormat = MailFormat.Html;
            objMail.Body = txt;
            SmtpMail.SmtpServer = "wczmail.wistron.com";
            SmtpMail.Send(objMail);

        }


        public void ShowMessage(string msg, Page PageInstance)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert('");
            sb.Append(msg);
            sb.Append("');");
            sb.Append("</script>");
            PageInstance.ClientScript.RegisterStartupScript(this.GetType(),
                            "script", sb.ToString());
        } 
    }
}
