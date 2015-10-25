using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using EHR.Model;
using ehr_email;
using System.Text; 

namespace EHR.Model
{ 
    public class GlobalApprovalModel
    {
        DBModel _db = new DBModel();
        PSModel _ps = new PSModel();
        MiscModel _misc = new MiscModel();
        GlobalAgentModel _agent = new GlobalAgentModel(); 

        //
        /// <summary>
        /// 
        /// Approval flow Process
        /// 
        /// 1. Query All Approvers and Update First Approver to "W" 
        ///     (GetApproval(string requestID, string employeeID, string appflowid, string deptid)) 
        ///  
        /// 
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="employeeID"></param>
        /// <param name="appflowid"></param>
        /// <param name="deptid"></param>

        //insert this method to get approval flow
        public void GetApproval(string requestID, string employeeID, string appflowid, string deptid)
        {
            _db.GetConn();
            string emplid_deptid = _ps.GetEmplDepartment(employeeID);
            string getPlantSQL = "SELECT PLANT_ID_A FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = '" + emplid_deptid + "'";
            OleDbCommand cmdPlant = new OleDbCommand(getPlantSQL, _db.conn);
            OleDbDataReader rdPlant = cmdPlant.ExecuteReader();
            string plant_id;
            if (rdPlant.Read())
            {
                plant_id = Convert.ToString(rdPlant["PLANT_ID_A"]);
            }
            else
            {
                plant_id = "SU";
            }


            //string appflowid = "AF002"; 
            // Get's all approver for certain approval flow
            string sql = "select distinct(b.appseq) APPSEQ, a.approverid APPID, a.approvervalue APPROVERVALUE, " +
                                     "a.approverdesc APPROVERDESC, a.appemplid APPEMPLID, a.category CATEGORY, a.source SOURCE " +
                                     " from ps.ps_approver a, ps.approvaldtl b where a.approvervalue=b.appvalue and " +
                                     "(a.category ='" + plant_id + "' or a.source='PS') and b.appflowid='" + appflowid + "' order by b.appseq asc";


            OleDbDataReader rd = _db.GetDataReader(sql);

            while (rd.Read())
            {

                string appseq = Convert.ToString(rd["APPSEQ"]);
                string appvalue = Convert.ToString(rd["APPROVERVALUE"]).ToUpper();
                string appdesc = Convert.ToString(rd["APPROVERDESC"]);
                string source = Convert.ToString(rd["SOURCE"]);
                string category = Convert.ToString(rd["CATEGORY"]);
                string approverid = Convert.ToString(rd["APPID"]);


                string appemplid;
                if (source == "PS")
                {
                    switch (appvalue)
                    {
                        case "SUP":
                            //get supervisor of this id
                            appemplid = GetSupervisorID(employeeID, "SUP");
                            break;
                        case "MAN":
                            //get manager of this id 
                            //appemplid = GetSupervisorID(employeeID, "MAN");
                            appemplid = GetMan(employeeID, "MAN");
                            break;
                        case "PM":
                            //get PM of this id   
                            appemplid = GetSupervisorID(employeeID, "PM");
                            break;
                        case "GM":
                            //get GM of this id  
                            appemplid = GetSupervisorID(employeeID, "GM");
                            break;  
                        default:
                            appemplid = "";
                            break;
                    }

                }

                else
                {
                    appemplid = Convert.ToString(rd["APPEMPLID"]);
                }

                string sqlAppv;

                if (appemplid == "")
                {
                    // insert all approvers to the approver logs
                    sqlAppv = "Insert into APPROVALTRANSACTION " +
                           "(REQID, APPFLOWID, APPSEQ, APPROVERVALUE, APPROVERDESC, APPEMPLID, RESULT) " +
                        //"(:reqid, :appflowid, :appseq, :appvalue, :appdesc, :appemplid)";
                           "VALUES ('" + requestID + "', '" + appflowid + "', '" + appseq + "', " +
                           " '" + appvalue + "', '" + appdesc + "', '" + appemplid + "', '')";
                }

                else
                {
                    // insert all approvers to the approver logs
                    sqlAppv = "Insert into APPROVALTRANSACTION " +
                           "(REQID, APPFLOWID, APPSEQ, APPROVERVALUE, APPROVERDESC, APPEMPLID, RESULT) " +
                        //"(:reqid, :appflowid, :appseq, :appvalue, :appdesc, :appemplid)";
                           "VALUES ('" + requestID + "', '" + appflowid + "', '" + appseq + "', " +
                           " '" + appvalue + "', '" + appdesc + "', '" + appemplid + "', 'O')";
                }


                try
                {
                    OleDbCommand cmdAppv = new OleDbCommand(sqlAppv, _db.conn);
                    cmdAppv.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                
            }

            //Set First Approver to Waiting
            IsNextApprover(requestID); 
        }
        
        //Get the next person to approve
        public void IsNextApprover(string RequestID)
        {
            if (CountOpen(RequestID) != 0)
            {
                string sql;
                try
                { 
                    //if this approver has proxy .. update the approval transaction table and 
                    //replace the employee with the agent
                    if(_agent.HasProxy(GetApprover(RequestID)))
                    {
                        //get current approver
                        string approver = GetApprover(RequestID);// _agent.GetAgent(GetApprover(RequestID));
                        //get current agent 
                        string agent = _agent.GetAgent(GetApprover(RequestID)); 
                        //get current emplid that requires agent
                        string appemplid = _agent.GetAgentsEmplid(approver, agent); 
                        //replace approver with it's agent.   
                        // string appemplids = approver.Replace(appemplid, agent);
                        string appemplids = approver + "," + agent;
                        
                        //update approvaltransaction table with new agent

                        sql = "update approvaltransaction set result = 'W', appemplid = '" + appemplids + "' " +
                               "where appseq = (Select min(appseq) from approvaltransaction where result='O' " +
                               "and  REQID = '" + RequestID + "') AND REQID = '" + RequestID + "'"; 
                    }
                    else
                    {
                         sql = "update approvaltransaction set result = 'W' " +
                                 "where appseq = (Select min(appseq) from approvaltransaction where result='O' " +
                                 "and  REQID = '" + RequestID + "') AND REQID = '" + RequestID + "'"; 
                       
                        
                    }

                    OleDbCommand _cmd = new OleDbCommand(sql, _db.conn);
                    _cmd.ExecuteNonQuery();
                    SendEmailToNextApprover(RequestID);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                NotifyRequestor(RequestID, _misc.GetWebAppRoot(), "A");
            }
        }

        //Count Open or Waiting Approvers
        public int CountOpen(string requestiD)
        {
            string count = "Select count(result) from approvaltransaction " +
                        "where  REQID = '" + requestiD + "' and RESULT = 'O' and ISNOTIFYONLY IS NULL ";

            int countWaiting = _db.GetExecuteScalar(count);

            return countWaiting;
        }

        public int CountWaiting(string requestID)
        {
            string count = "Select count(result) from approvaltransaction " +
            "where  REQID = '" + requestID + "' and RESULT = 'W' and ISNOTIFYONLY IS NULL ";

            int countWaiting = _db.GetExecuteScalar(count);

            return countWaiting;
        }

        public void ApprovedAction(string reqid, string userid, string remarks)
        {
            string sql = "Update APPROVALTRANSACTION SET RESULT = 'A', ACTUALAPPROVER = '" + userid + "', " +
                "DATESIGNED = SYSDATE, REMARKS = '" + remarks + "' WHERE REQID = '" + reqid +"' AND RESULT = 'W'";
            try
            {
                _db.GetExecuteNonQuery(sql);
                IsNextApprover(reqid);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
                     
        }

        public void RejectedAction(string reqid, string userid, string remarks)
        {
            string sql = "UPDATE APPROVALTRANSACTION SET RESULT = 'R', ACTUALAPPROVER = '" + userid + "', " +
                "DATESIGNED = SYSDATE, REMARKS = '" + remarks + "' WHERE REQID = '" + reqid + "' AND RESULT IN ('W', 'O')";

            try
            {
                _db.GetConn();
                OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
                cmd.ExecuteNonQuery();
                NotifyRequestor(reqid, _misc.GetWebAppRoot(), "R");
            }
            catch
            {
            }
        }

        private void SendEmailToNextApprover(string reqid)
        {
            try
            {
                string sql = "SELECT APPEMPLID, APPROVERDESC FROM approvaltransaction WHERE result = 'W' AND REQID = '" + reqid + "'";
                OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
                OleDbDataReader _rd = cmd.ExecuteReader();

                string MyUrl = _misc.GetWebAppRoot();

                if (_rd.HasRows)
                {
                    _rd.Read();
                    string sentto = Convert.ToString(_rd["appemplid"]);
                    string approverDesc = Convert.ToString(_rd["APPROVERDESC"]);
                    string[] emailto = sentto.Split(',');
                    // string email_add; 

                    StringBuilder sb = new StringBuilder();

                    foreach (string email in emailto)
                    {
                        sb.Append(_ps.GetEmplEmailAdrress(email.Trim()) + ",");
                    }

                    string email_addresses = sb.ToString();

                    

                    if (_misc.IsEmailNotificationTest("Notification", "You have request to approve: " + reqid, reqid) == false)
                    {
                        string mail_type = "Notification";
                        string sender_ = "noreply@wistron.com ";

                        string recipient = email_addresses;
                        string recipient_name = approverDesc;
                        string cc = "";
                        string subject = "EHR: Recruitment - Approval Notification";
                        string parameters = "Request Link: <a href='http://" + MyUrl + "/Main/Approval.aspx?appv=" + reqid + "&a=681559B44B2CC43C82AA343C925331D3'>" + reqid + "</a>";

                        Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                    }

                }
                 
            }
            catch (Exception ex)
            {
            }
        }

        private void NotifyRequestor(string reqid, string MyUrl, string action)
        {
            string requestor = _misc.GetRequestor(reqid, "EREC_REQUESTS");
            string requestorName = _ps.GetName(requestor);
            string email_type, subject; 
            if (action == "A")
            {
                email_type = "Notify_HR_Rec_Requestor";
                subject = "Approved Request: " + reqid;
            }
            else if (action == "R")
            {
                email_type = "Notify_RejectToApprover";
                subject = "Rejected Request: " + reqid;
            }
            else
            {
                email_type = "Notify_Error";
                subject = "Error Mail on request: " + reqid;
            }

            try
            {
                if (_misc.IsEmailNotificationTest(email_type, "Request No: " + reqid, reqid) == false)
                {
                    string mail_type = email_type;
                    string sender_ = "noreply@wistron.com ";

                    string recipient = _ps.GetEmplEmailAdrress(requestor);
                    string recipient_name = requestorName;
                    string cc = ""; 
                    string parameters = "Request Link: <a href='http://" + MyUrl + "/Main/Approval.aspx?appv=" + reqid + "&a=681559B44B2CC43C82AA343C925331D3'>" + reqid + "</a>";

                    Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                }
            }
            catch (Exception ex)
            {
            }
        }

        //get the manager id of person 
        public string GetSupervisorID(string emplid, string position)
        {
            string supervisor;

            //get the parent of employee based on employee ID and ps.wcz_org view's position 
            string sql = "SELECT emplid FROM ps.wcz_org " +
               "where position = '" + position + "'  " +
               "START WITH emplid IN (SELECT emplid  " +
               "FROM ps.wcz_org WHERE emplid = '" + emplid + "')  " +
               "CONNECT BY prior supervisor_id =emplid order by level desc";

            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                supervisor = Convert.ToString(rd["emplid"]);
            }
            else
            {
                supervisor = "";
            }
            return supervisor;

        }

        //get the manager id of person within the department section
        public string GetMan(string emplid, string position)
        {
            string supervisor = GetSupervisorID(emplid, position);
            string man;

            int Treelevel;
            string sqlTree = "select TREE_LEVEL_NUM from ps.ps_sub_wczorg_vw_a where manager_id = '{0}'";
            OleDbCommand cmd = new OleDbCommand(sqlTree, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();  
 
            if (_rd.Read())
            {
                Treelevel = Convert.ToInt32(_rd["TREE_LEVEL_NUM"]);
            }
            else
            {
                Treelevel = 0;
            }

            if (Treelevel == 8)
            {
                man = GetSupervisorID(supervisor, position);
            }
            else
            {
                man = GetSupervisorID(emplid, position);
            }
            return man;
        }

        public string GetRequestor(string reqid)
        {
            _db.GetConn();
            string reqID;
            string sql = "SELECT REQUESTOR FROM EREC_REQUESTS WHERE REQUESTID = '" + reqid + "'";
            OleDbDataReader _rd = _db.GetDataReader(sql);
            if (_rd.Read())
            {
                reqID = Convert.ToString(_rd["REQUESTOR"]);

            }
            else
            {
                reqID = "";
            }
            return reqID;
        }

        //false if not current approver
        public bool IsCurrentLoggedApprover(string reqid, string user)
        {
            string sql = "SELECT RESULT FROM APPROVALTRANSACTION WHERE " +
              "REQID = '" + reqid + "' AND APPEMPLID LIKE '%" + user + "%' AND RESULT = 'W' and ISNOTIFYONLY IS NULL";

            OleDbDataReader _rd = _db.GetDataReader(sql);
            if (_rd.Read())
            {
                string result = Convert.ToString(_rd["RESULT"]);
                if (result == "W")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //disabled approval controls if user is not an approver or is listed in approver database
       
        public bool IsApprover(string reqid, string user)
        {
            string sql = "SELECT COUNT(APPEMPLID) FROM APPROVALTRANSACTION WHERE APPEMPLID " +
                "LIKE '%" + user + "%' AND REQID = '" + reqid + "' and ISNOTIFYONLY IS NULL";

            int count = _db.GetExecuteScalar(sql);

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetApprover(string RequestID)
        {
            string appemplid; 
            string sqlApprover = "SELECT appemplid from approvaltransaction where " + 
                            "appseq = (Select min(appseq) from approvaltransaction where result='O' " +
                           "and  REQID = '" + RequestID + "') AND REQID = '" + RequestID + "'";
            OleDbCommand cmdApprover = new OleDbCommand(sqlApprover, _db.conn);
            OleDbDataReader rdApprover = cmdApprover.ExecuteReader();
            if (rdApprover.Read())
            {
                appemplid = Convert.ToString(rdApprover["appemplid"]);
            }
            else
            {
                appemplid = "";
            }
            return appemplid;
        }
    }
}
