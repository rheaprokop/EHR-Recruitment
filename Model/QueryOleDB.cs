using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Mail; 

namespace EHR.Model
{
    public class QueryOleDB : ObjectsOleDB
    {
        
        //Query to get the NAME_A, FIRST_NAME_SRCH, DEPTID from PS.PS_SUB_WCZ_AT_VW_A
        public string GetFullName(string EmplID)
        {
            string OracleQry = "Select NAME_A From PS.PS_SUB_WCZ_AT_VW_A Where EMPLID = :EmplID";
            string EmplFullName;

            GetConn();
            OleDbCommand myCommand = new OleDbCommand(OracleQry, MyConn);
            
            myCommand.Parameters.AddWithValue("':EmplID'", EmplID);

            OleDbDataReader myReader = myCommand.ExecuteReader();
                
                    if (myReader.Read())
                    {
                        EmplFullName = Convert.ToString(myReader["NAME_A"]);
                    }
                    else
                    {
                        EmplFullName = "0";
                    }
               
            

            MyConn.Close();
            return EmplFullName;
            
        }

        public string GetEmplEmail(string EmplID)
        {
            string OracleQry = "Select EMAIL_ADDRESS_A From PS.PS_SUB_WCZ_AT_VW_A WHere EMPLID = '" + EmplID + "'";

            return OracleQry;
        }

        public string GetEmplEmailAddress(string EmplID)
        {
            GetConn();
            string OracleQry = "Select EMAIL_ADDRESS_A From PS.PS_SUB_WCZ_AT_VW_A WHere EMPLID = '" + EmplID + "'";
            string email;
            OleDbCommand myCommand = new OleDbCommand(OracleQry, MyConn);
            OleDbDataReader myRd = myCommand.ExecuteReader();
            if (myRd.Read())
            {
                 email = Convert.ToString(myRd["EMAIL_ADDRESS_A"]);
            }
            else
            {
                 email = "";
            }

            return email;
        }


        public void GetEmployeeName(string EmplID, out string EmplName, out string DeptID, out string FirstName)
        {
            string OracleQry = "Select NAME_A, FIRST_NAME_SRCH, DEPTID From PS.PS_SUB_WCZ_AT_VW_A" +
                               " Where EMPLID = '" + EmplID + "'";

            GetConn();
            OleDbCommand MyCommand = new OleDbCommand(OracleQry, MyConn);
             

            OleDbDataReader MyReader = MyCommand.ExecuteReader();

            if (MyReader.Read())
            {
                EmplName = Convert.ToString(MyReader["NAME_A"]);
                DeptID = Convert.ToString(MyReader["DEPTID"]);
                FirstName = Convert.ToString(MyReader["FIRST_NAME_SRCH"]);
            }
            else
            {
                EmplName = "";
                DeptID = "";
                FirstName = "";
            }

            MyConn.Close();
        }

        public string GetEmplFrmDept(string dept)
        {
            string OracleQry = "Select EMPLID, NAME_A From PS.PS_SUB_WCZ_AT_VW_A" +
                                " Where DEPTID = '" + dept + "'";

            return OracleQry;
        }

        public string GetJobBusiTitleFromCode(string JobCode, out string JobBusiTitle)
        {
            string OracleQry = "Select JOB_DESCR FROM PS.PS_SUB_WCZ_AT_VW_A Where JOBCODE = :JobCode";
            GetConn();
            OleDbCommand MyCommand = new OleDbCommand(OracleQry, MyConn);
            MyCommand.Parameters.AddWithValue("':JobCode'", JobCode);
            OleDbDataReader MyReader = MyCommand.ExecuteReader();
            if (MyReader.Read())
            {
                JobBusiTitle = Convert.ToString(MyReader["JOB_DESCR"]);
            }
            else
            {
                JobBusiTitle = "";
            }
            MyConn.Close();
            return JobBusiTitle;
        }


        //Query to get All Department except fro CSD
        public string GetDeptID()
        {
            /*string OracleQry = "Select DEPTID, DESCR From PS.PS_SUB_WCZORG_VW_A Where DEPTID Not Like 'C%' AND DEPTID Like 'MD%'" + 
                               " Order by DEPTID";*/

            string OracleQry = "Select DEPTID, DESCR From PS.PS_SUB_WCZORG_VW_A WHERE DEPTID LIKE '%MD%' Order by DEPTID";
                                         
            return OracleQry;


        }

        //Query TO Get Job details
        public string GetJobDetails(string DeptID)
        {
            string OracleQry = "Select Distinct JOBCODE, JOB_DESCR FROM PS.PS_SUB_WCZ_AT_VW_A " +
                               " Where DEPTID= '" + DeptID + "'";

            return OracleQry;
        }



              

        //Query to Get Request ID
       public string GetLastRequestID()
         {
             string OracleQry = "Select REQUESTID from EREC_REQUESTS WHERE REQUESTID = (Select max(REQUESTID) from EREC_REQUESTS)";
             string RequestID;
             GetConn();
             OleDbCommand MyCommand = new OleDbCommand(OracleQry, MyConn);
             OleDbDataReader MyReader = MyCommand.ExecuteReader();

             if (MyReader.Read())
             {
                 RequestID = Convert.ToString(MyReader["REQUESTID"]);

             }
             else
             {
                RequestID = "";
             }

             MyConn.Close();
             return RequestID;
         }


       public string GetLastEmplID()
       {
           string EmplID;
           GetConn();
           string LastEmpl = "Select EMPLID from PS.PS_SUB_WCZ_AT_VW_A " +
                             "WHERE EMPLID = (Select max(EMPLID) " +
                             "from  PS.PS_SUB_WCZ_AT_VW_A WHERE EMPLID LIKE 'C%')";

           OleDbCommand MyCommand = new OleDbCommand(LastEmpl, MyConn);
           OleDbDataReader MyReader = MyCommand.ExecuteReader();

           if (MyReader.Read())
           {
               EmplID = Convert.ToString(MyReader["EMPLID"]);

           }
           else
           {
               EmplID = "";
           }

           MyConn.Close();
           return EmplID;
       }


       public string GetLastEmplFromCand()
       {
           string EmplID;
           GetConn();
           string LastEmplFromCan = "Select EMPLID from EREC_CANDIDATES " +
                            "WHERE EMPLID = (Select max(EMPLID) " +
                            "from  EREC_CANDIDATES  WHERE EMPLID LIKE 'C%')";

           OleDbCommand MyCommand = new OleDbCommand(LastEmplFromCan, MyConn);
           OleDbDataReader MyReader = MyCommand.ExecuteReader();

           if (MyReader.Read())
           {
               EmplID = Convert.ToString(MyReader["EMPLID"]);

           }
           else
           {
               EmplID = "";
           }

           MyConn.Close();
           return EmplID;
       }

         public string GetLastCandidateID()
         {
             string OracleQry = "Select CANDIDATEID from EREC_CANDIDATES WHERE CANDIDATEID = (Select max(CANDIDATEID) from EREC_CANDIDATES)";
             string CandidateID;
             GetConn();
             OleDbCommand MyCommand = new OleDbCommand(OracleQry, MyConn);
             OleDbDataReader MyReader = MyCommand.ExecuteReader();

             if (MyReader.Read())
             {
                 CandidateID = Convert.ToString(MyReader["CANDIDATEID"]);

             }
             else
             {
                 CandidateID = "";
             }

             MyConn.Close();
             return CandidateID;
         }

         public string GetFlow(string id)
         {
             string OracleQry = "Select Flow from ApprovalFlow Where FlowID = :id";
             string flow;

             GetConn();
             OleDbCommand myCmd = new OleDbCommand(OracleQry, MyConn);
             myCmd.Parameters.AddWithValue("':id'", id);
             OleDbDataReader myRead = myCmd.ExecuteReader();
            
               if (myRead.Read())
                 {
                         flow = Convert.ToString(myRead["Flow"]);
                 }
               else
                 {
                         flow = "";
                 }
                   return flow;
         }

         public string LoadHRData()
         {
             string OracleQry = "Select EMPLID, NAME_A From PS.PS_SUB_WCZ_AT_VW_A WHERE DEPTID = 'MD0L20' ";
             return OracleQry;
         }

         public string GetRequestor(string requestID)
         {
             GetConn();
             string OracleQry = "Select REQUESTOR From EREC_REQUESTS Where REQUESTID = '" + requestID + "'";
             OleDbCommand myCMD = new OleDbCommand(OracleQry, MyConn);
             OleDbDataReader myRD = myCMD.ExecuteReader();
             
             string requestor;
             if (myRD.Read())
             {
                 requestor = Convert.ToString(myRD["REQUESTOR"]);
             }
             else
             {
                 requestor = "";
             }
             return requestor;
         }


         public void InsertActivityDesc(string text, string EmplID)
         {
             //insert into activities
             GetConn();
             DateTime MyDate = DateTime.Now;
             string s = String.Format("{0:MM/dd/yyyy}", MyDate);  
             string OraActivity = "Insert into ACTIVITIES(ACTIVITYID, ACTIVITYDATE, ACTIVITYDESC, EMPLID) " +
                    "VALUES (Activity_Seq.NextVal, to_date('" + s + "','MM/dd/yyyy'), '" + text + "', '" + EmplID + "')";
             OleDbCommand myCMD = new OleDbCommand(OraActivity, MyConn);
             myCMD.ExecuteNonQuery();
             MyConn.Close();
         }

         public void InsertAlerts(string text, string UserID)
         {
             //insert into activities
             GetConn();
             DateTime MyDate = DateTime.Now;
             string s = String.Format("{0:MM/dd/yyyy}", MyDate);
             string OraActivity = "Insert into ALERTS(ALERTID, ALERTDATE, ALERTDESC, ALERTOEMPLID) " +
                    "VALUES (Alert_Seq.NextVal, to_date('" + s + "','MM/dd/yyyy'), '" + UserID + "')";
             OleDbCommand myCMD = new OleDbCommand(OraActivity, MyConn);
             myCMD.ExecuteNonQuery();
             MyConn.Close();
         }

         public string GetCandidateName(string candidateID)
         {
             GetConn();
             string name;
             string OraCand = "Select INITCAP(FIRSTNAME)|| ' '|| INITCAP(LASTNAME) FULLNAME from EREC_CANDIDATES " +
                    "Where CANDIDATEID = '" + candidateID + "'";
             OleDbCommand myCMD = new OleDbCommand(OraCand, MyConn);
             OleDbDataReader myRD = myCMD.ExecuteReader();
             if (myRD.Read())
             {
                 name = Convert.ToString(myRD["FULLNAME"]);
             }
             else
             {
                 name = "Can't find name.";
             }

             return name;
         }

         public void EmailBodyGeneral(string toEmail, string subject, string txt)
         {
             try
             {
                 MailMessage objMail = new MailMessage();
                 objMail.From = "EHR <Rhea_Prokop@wistron.com>";
                 objMail.To = toEmail;
                 objMail.Subject = subject;
                 objMail.BodyFormat = MailFormat.Html;
                 objMail.Body = txt;
                 SmtpMail.SmtpServer = "wczmail.wistron.com";
                 SmtpMail.Send(objMail);
             }
             catch (Exception e)
             {
                 MailMessage objMail = new MailMessage();
                 objMail.From = "EHR <Rhea_Prokop@wistron.com>";
                 objMail.To = "EHR <Rhea_Prokop@wistron.com>";
                 objMail.Subject = subject;
                 objMail.BodyFormat = MailFormat.Html;
                 objMail.Body = "Email not deliver. <br />Did not deliver to: " + toEmail + e.Message;
                 SmtpMail.SmtpServer = "wczmail.wistron.com";
                 SmtpMail.Send(objMail);
             }

         }

         public string RoleAccess(string Emplid)
         {
             GetConn();
             string Role;
             string OracleQry = "Select ROLE From DBUSERS Where EMPLID = '" + Emplid + "'";
             OleDbCommand mycmd = new OleDbCommand(OracleQry, MyConn);
             OleDbDataReader myrd = mycmd.ExecuteReader();

             if (myrd.Read())
             {
                 Role = Convert.ToString(myrd["ROLE"]);
             }
             else
             {
                 Role = "";
             }

             return Role;
         }

         public string GetNameByEmail(string EmailAdd)
         {
             GetConn();
             string name_a;
             string OracleQry = "Select NAME_A From ps.ps_sub_wcz_at_vw_a where EMAIL_ADDRESS_A = '" + EmailAdd + "'";
             OleDbCommand myCmd = new OleDbCommand(OracleQry, MyConn);
             OleDbDataReader myrd = myCmd.ExecuteReader();
             if (myrd.Read())
             {
                 name_a = Convert.ToString(myrd["NAME_A"]);
             }
             else
             {
                 name_a = "";
             }
             return name_a;

         }


         public string GetTrainer(string requestID) // email to trainer after candidate acceptance
         {
             GetConn();
             string trainer;
             string TrainerSql = "Select HRTRAINER From EREC_REQUESTS Where  REQUESTID='" + requestID + "'";
             OleDbCommand myCmd = new OleDbCommand(TrainerSql, MyConn);
             OleDbDataReader myRD = myCmd.ExecuteReader();
             if (myRD.Read())
             {
                 trainer = Convert.ToString(myRD["HRTRAINER"]);
             }
             else
             {
                 trainer = "";
             }
             return trainer;
         }


        public string InformationToTrainer(string candidateID)
        {
            string txtEmail;
            GetConn();
            string OracleQry = "Select TO_CHAR(OFFERONBOARDDATE, 'dd.MM.yyyy hh:mm AM') as ONBOARD, OFFERFORREQUESTID From EREC_CANDIDATES Where CANDIDATEID ='" + candidateID + "' "; 
            OleDbCommand mycmd = new OleDbCommand(OracleQry, MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();
            if (myrd.Read())
            {
                string requestID = Convert.ToString(myrd["OFFERFORREQUESTID"]);
                string ONBOARD = Convert.ToString(myrd["ONBOARD"]);

              txtEmail = "Dear  " + GetFullName(GetTrainer(requestID)) + ", <br /> " +
                        "As HR Trainer, please be informed that candidate:  " + GetCandidateName(candidateID) + "  has been accepted.<br />" +
                        "And will be On Board to be trained on: " + ONBOARD + " <br /> <br /> " +
                            "EHR";
            }
            else
            {
                txtEmail = "";
            }
            return txtEmail;
        }

        public string GetManager(string requestID) // email to trainer after candidate acceptance
        {
             GetConn();
             string manager;
            //GET MANAGER ID OF /*deptid*/
             string OracleFrmPIC = "Select REQUESTORDEPTID from EREC_REQUESTS WHERE REQUESTID='" + requestID + "'";
             OleDbCommand myCmd1 = new OleDbCommand(OracleFrmPIC, MyConn);
             OleDbDataReader myRead1 = myCmd1.ExecuteReader();
             if (myRead1.Read())
             {
                 //DEPT ID OF REQUESTOR
                 string deptid = Convert.ToString(myRead1["REQUESTORDEPTID"]);
                 string ManagerQry = "Select MANAGER_ID from PS.PS_SUB_WCZORG_VW_A Where DEPTID = '" + deptid + "'";
                 try
                 {
                     OleDbCommand cmdManagerQry = new OleDbCommand(ManagerQry, MyConn);
                     OleDbDataReader rdManagerQry = cmdManagerQry.ExecuteReader();

                     if (rdManagerQry.Read())
                     {
                         manager = Convert.ToString(rdManagerQry["MANAGER_ID"]);
                     }
                     else
                     {
                         manager = "";
                     }
                 }
                 catch (OleDbException ex)
                 {
                     manager = "";
                 }
             }
             else
             {
                 manager = "";
             }
             return manager;
        }

        public string InformationToManager(string candidateID, string salary, string salaryAfter)
        {
            GetConn(); 
            string txtEmail;
            string OracleQry = "Select TO_CHAR(OFFERONBOARDDATE, 'dd.MM.yyyy hh:mm AM') as ONBOARD, OFFERFORREQUESTID From EREC_CANDIDATES Where CANDIDATEID ='" + candidateID + "' "; 
            OleDbCommand mycmd = new OleDbCommand(OracleQry, MyConn);
            OleDbDataReader myrd = mycmd.ExecuteReader();
            if (myrd.Read())
            {
                string requestID = Convert.ToString(myrd["OFFERFORREQUESTID"]);
                string ONBOARD = Convert.ToString(myrd["ONBOARD"]);
                txtEmail = "Dear  " + GetFullName(GetManager(requestID)) + ", <br /> " +
                  "As Dept Manager, please be informed that candidate:  " + GetCandidateName(candidateID) + "  has been accepted.<br />" +
                  "And will be On Board to be trained on: " + ONBOARD + " <br />Salary: '" + salary + "'; <br />Salary After Probation: '" + salaryAfter+ "'" +
                      " <br />  <br /> EHR";
            }
            else
            {
                txtEmail = "";
            }
            return txtEmail;
        }
    }
}
