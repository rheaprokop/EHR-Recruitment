using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using ehr_email;

namespace EHR.Model
{
    public class ApprovalModel
    {
        DBModel _db = new DBModel(); 
        MiscModel _misc = new MiscModel();
        PSModel _ps = new PSModel(); 
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
                        case "HR_STA_NDE":
                            //get dept manager of new dept
                           appemplid = GetNewDeptManager(deptid);
                            
                            break;
                        case "HR_STA_NPM":
                            //get dept manager of new dept
                            string newPM;
                            
                             string appemplidGet = GetNewPlantManager(deptid);
                             string newPMsql = "SELECT EMPLID FROM PS.PS_SUB_WCZ_AT_VW_A WHERE JOB_DESCR = 'Managing Director'";
                             OleDbCommand cmd = new OleDbCommand(newPMsql, _db.conn);

                             OleDbDataReader _rdnew = cmd.ExecuteReader(); 
                             if (_rdnew.Read())
                             {
                                 newPM = Convert.ToString(_rdnew["EMPLID"]);
                             }
                             else
                             {
                                 newPM = "";
                             }
                             if (newPM == appemplidGet)
                             {
                                 appemplid = "";
                             }
                             else
                             {
                                 appemplid = appemplidGet;
                             }
                            //appemplid = "";
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
                

                OleDbCommand cmdAppv = new OleDbCommand(sqlAppv, _db.conn); 
                cmdAppv.ExecuteNonQuery();

               
            }
            IsNextApprover(requestID);
        }


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

        public string GetMan(string emplid, string position)
        {
            string supervisor = GetSupervisorID(emplid, position);
            string man;

            int Treelevel;
            string sqlTree = "select TREE_LEVEL_NUM from ps.ps_sub_wczorg_vw_a where manager_id = '" + supervisor + "'";
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

        public string GetNewDeptManager(string newDept)
        {
            string supervisor;
            string newMan = "SELECT MANAGER_ID FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = '" + newDept + "'";
            OleDbCommand cmd = new OleDbCommand(newMan, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                supervisor = Convert.ToString(rd["MANAGER_ID"]);
            }
            else
            {
                supervisor = "";
            }

            string newSupervisor = GetMan(supervisor, "MAN");
            return newSupervisor;

        }

        public string GetNewPlantManager(string newDept)
        {
            string supervisor;
            if (newDept.Substring(0, 3) == "MD1")
            {
                newDept = "MD1";
            }
            else if (newDept.Substring(0, 3) == "MD0")
            {
                newDept = "MD0";
            }
            else if (newDept.Substring(0, 3) == "MD3")
            {
                newDept = "MD3";
            }
            else
            {
                newDept = "";
            }
            string supervisorSql; 
            switch (newDept)
            {
                case "MD0":
                    supervisorSql = "SELECT MANAGER_ID FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = 'MD0000'";
                    break;
                case "MD1":
                    supervisorSql = "SELECT MANAGER_ID FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = 'MD1000'"; 
                    break;
                case "MD3":
                    supervisorSql = "SELECT MANAGER_ID FROM PS.PS_SUB_WCZORG_VW_A WHERE DEPTID = 'MD3000'";
                    break;
                default:
                    supervisorSql = "";
                    break;
            }

            OleDbCommand cmd = new OleDbCommand(supervisorSql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                supervisor = Convert.ToString(rd["MANAGER_ID"]);
            }
            else
            {
                supervisor = "";
            }
            return supervisor;
        }


        public void IsNextApprover(string transNo)
        {
            //get current approver
            _db.GetConn();
            string sqlCurrent = "SELECT  MIN(APPSEQ) APPSEQ FROM APPROVALTRANSACTION WHERE REQID = '" + transNo + "' " +
                                "AND RESULT = 'O'";

            OleDbCommand cmd = new OleDbCommand(sqlCurrent, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                int currentAppv ;
                try
                {
                     currentAppv = Convert.ToInt32(rd["APPSEQ"]);
                }
                catch (Exception ex)
                {
                    currentAppv = 0;
                }
                
                string sqlNext;
                if (currentAppv == 0)
                {  
                    string sqlLast = "SELECT REQUESTORID, REQUESTID FROM STATUS_CHANGE WHERE TRANSID = '" + transNo + "'";
                    OleDbDataReader _ord = _db.GetDataReader(sqlLast);
                    string requestorID;
                    string requestID;

                    if (_ord.Read())
                    {
                        requestorID = Convert.ToString(_ord["REQUESTORID"]);
                        requestID = Convert.ToString(_ord["REQUESTID"]);

                        if (_misc.IsEmailNotificationTest("Notify_Approved", "Approval Notification: " + requestorID, requestID) == false)
                        {
                             

                            string mail_type = "Notify_Approved";
                            string sender_ = "noreply@wistron.com ";

                            string recipient = _ps.GetEmplEmailAdrress(requestorID);
                            string recipient_name = _ps.GetEmplFirstName(requestorID);
                            string cc = "martin_husar@wistron.com; rhea_prokop@wistron.com";
                            string subject = "EHR: Status Change - Approval Notification";
                            string parameters = "Request Link: <a href='http://ehr.wcz.wistron/Views/Status/RQ03.aspx?id=" + requestID + "'>" + requestID + "</a>";

                            Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);
                        }
                        string salary_flag;
                        string print_salary_flag = "";
                        string payroll;
                        string hr;
                        string pic_status;
                        string sqlSalaryFlag = "SELECT SALARY_FLAG, PRINT_SALARY_FLAG, PAYROLL_PIC, HR_PIC FROM STATUS_CHANGE WHERE REQUESTID='" + requestID + "'";
                        OleDbDataReader _rdSalFlag = _db.GetDataReader(sqlSalaryFlag);
                        if (_rdSalFlag.Read())
                        {
                            salary_flag = Convert.ToString(_rdSalFlag["SALARY_FLAG"]);
                            print_salary_flag = Convert.ToString(_rdSalFlag["PRINT_SALARY_FLAG"]);
                            payroll = Convert.ToString(_rdSalFlag["PAYROLL_PIC"]);
                            hr = Convert.ToString(_rdSalFlag["HR_PIC"]);
                             
                             
                        }
                        else
                        {
                            salary_flag = "";
                            hr = "";
                            payroll = ""; 
                        }

                        if (salary_flag == "1" && print_salary_flag != "9")
                        {
                            string sqlNextStatus = "UPDATE STATUS_CHANGE SET STATUS = 'Payroll Process' WHERE TRANSID = '" + transNo + "'";
                            _db.GetExecuteNonQuery(sqlNextStatus);
                        } 

                        if (salary_flag == "0" || (salary_flag == "1" && print_salary_flag == "9") )
                        {
                            string sqlNextStatus = "UPDATE STATUS_CHANGE SET STATUS = 'HR Process' WHERE TRANSID = '" + transNo + "'";
                            _db.GetExecuteNonQuery(sqlNextStatus);
                        } 
                          
                    }
                    else
                    {
                        requestID = "";
                        requestorID = "";
                    }
                }
                else
                {
                    //update next approver to waiting
                     sqlNext = "UPDATE APPROVALTRANSACTION SET RESULT = 'W' WHERE APPSEQ = '" + currentAppv + "' " +
                                "AND REQID = '" + transNo + "'";
                     _db.GetExecuteNonQuery(sqlNext);
                     string sqlIsStatus = "UPDATE STATUS_CHANGE SET STATUS = 'Approval Process' WHERE TRANSID = '" + transNo + "'";

                     _db.GetExecuteNonQuery(sqlIsStatus);

                } 
            } 
        }


        //current approver's emplid
        public string GetCurrentApprover(string transno)
        {
            string sqlCurrent = "SELECT  APPEMPLID  FROM APPROVALTRANSACTION WHERE REQID = '" + transno + "' " +
                      "AND RESULT = 'W'";
            _db.GetConn();
            OleDbCommand cmd = new OleDbCommand(sqlCurrent, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();

            string currentAppv;
            if (rd.Read())
            {
                currentAppv = Convert.ToString(rd["APPEMPLID"]);
            }
            else
            {
                currentAppv = "";
            }

            return currentAppv;
            
        }

        public string GetApproverValue(string transno, string sessid)
        {
            string sqlCurrAppv = "SELECT APPSEQ, APPROVERVALUE FROM APPROVALTRANSACTION " +
                                "WHERE APPEMPLID = '" + sessid + "' AND " +
                                "REQID = '" + transno + "' AND RESULT = 'W'";
            OleDbDataReader _rd = _db.GetDataReader(sqlCurrAppv);

            string currAppValue; 
            if (_rd.Read())
            {
                currAppValue = Convert.ToString(_rd["APPROVERVALUE"]); 
            }
            else
            {
                currAppValue = ""; 
            }
            return currAppValue;
        }


        public bool HasApproverSignature(string appemplid)
        { 
            // Create the path and file name to check for duplicates.
            //string savePath = "D:\\Projects-EHR\\Content\\signatures\\"; // localhost
            string savePath = "d:\\EHR\\Content\\signatures\\";// production
            string emplid = appemplid + ".JPG";
            string pathToCheck = savePath + emplid;
            if (System.IO.File.Exists(pathToCheck) == true)
            {
                return true;
            }
            else
            {
                return false; //FALSE
            }
        }
    }
}
