using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Data;
using System.Data.OleDb;
using System.Resources; 
using EHR.Views.Account;

// IsActiveDirectoryLogin
// IsDatabaseLogin
// HashedPwd
// IsValidLogin
// GetDBDeptID
// GetAPDeptID
// UserIsExpired
// IfLockUser
// LockUser


namespace EHR.Model
{
    public class AccountModel   
    {
         DBModel _db = new DBModel();
        ChangePassword _pwd = new ChangePassword();
        
        //Get employee active directory login  
        public bool IsActiveDirectoryLogin(string EmplID, string Password)
        {
            string _path = "LDAP://10.82.33.21/dc=wcz,dc=wistron";
            string _filterAttribute;

            string domainAndUsername = @"WCZ\" + EmplID;
            DirectoryEntry entry = new DirectoryEntry(_path,
                                                       domainAndUsername,
                                                         Password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + EmplID + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    //error msg here
                }
                // Update the new path to the user in the directory
                _path = result.Path;

                _filterAttribute = (String)result.Properties["cn"][0];

                return true;
            }
            catch (Exception ex)
            {
                string Message = "Error:" + ex.Message;
                return false;
            }
        }


        //Login through Database .. return 1 or 0;
        public bool IsDatabaseLogin(string EmplID, string pwd)
        {
             _db.GetConn();
             
            string newPwd = HashedPwd(EmplID, pwd); 
            string sql = "SELECT COUNT(USERID) FROM LIB_USERS WHERE USERID = '" + EmplID + "'" +
                         "AND PASSWORD = '" + newPwd + "'";
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count >= 1)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _db.conn.Close();
            }


        }

        //salt pass
        //get user's encrypted password
        private string HashedPwd(string emplid, string pwd)
        {
            _db.GetConn();
            string sql = "SELECT PASSWORD FROM  LIB_USERS WHERE USERID ='" + emplid + "'";
            string password;
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
                if (rd.Read())
                {
                    string dbpass = Convert.ToString(rd["PASSWORD"]);
                    string hashedMD5 = _pwd.getMD5Hash(pwd);
                    if (dbpass == hashedMD5)
                    {
                        password = hashedMD5;
                    }
                    else
                    {
                        password = "";
                    }


                }
                else
                {
                    password = "";
                }
            }
            catch (Exception ex)
            {
                password = "";
            }
            finally
            {
                _db.conn.Close();
            }

            return password;

        }
        

        //call this to validate empty pass and user
        public bool IsValidLogin(string EmplID, string Password)
        {
            return !(string.IsNullOrEmpty(EmplID) && string.IsNullOrEmpty(Password));
        }
                

        

        // if user is lock
        public bool IfLockUser(string userid)
        {
            string sql = "SELECT LOCKFLAG FROM LIB_USERS WHERE USERID = '" + userid  + "'";
            _db.GetConn();
            string lockflag;
            OleDbDataReader rd = _db.GetDataReader(sql);
            if (rd.Read() == true)
            {
                lockflag = Convert.ToString(rd["LOCKFLAG"]);
            }
            else
            {
                lockflag = "";
            }
            _db.conn.Close();
            if (lockflag == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
             
        }

        public void LockUser(string userid)
        {            
            string sql = "UPDATE LOCKFLAG SET LOCKFLAG='0' WHERE USERID = '" + userid + "'";
            try
            {
                _db.GetExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _db.conn.Close();
            }
        }


        public bool IsAPUser(string userid)
        {

            string sql = "SELECT COUNT(USERID) FROM LIB_APUSER WHERE USERID = '" + userid + "'";
            _db.GetConn();
            /*OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            int count = Convert.ToInt32(cmd.ExecuteScalar());*/
            int count = _db.GetExecuteScalar(sql);
            if (count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

          
        }

        //compare dept

        public bool IsSameDept(string userid)
        {
            if (IsAPUser(userid) == true)
            {
                string sql = "select unique(DEPTID) from LIB_APUser where userid='" + userid + "' " +
                             "union select deptid from ehr.LIB_USERs where userid='" + userid + "'";
                DataSet ds = _db.GetTableSet(sql, "LIB_APUser");
                int intCount;
                intCount = ds.Tables["LIB_APUser"].Rows.Count;
                if (intCount == 1)
                {
                    return true;
                    _db.conn.Close();
                }
                else
                {
                    //do locking codes here
                    return false;
                }

            }
            else
            {
                return false;
            }
            
        }


        public string GetUserSessID()
        {
            string UserID = (string)(HttpContext.Current.Session["UserID"]);
            return UserID;

        }

        public bool IsValidUserLog(string s)
        {
            if (s == null)
            {
                return false;
            }
            {
                return true;
            }
        }

        public void ValidateUser(string s, HttpResponse response)
        {
            if (IsValidUserLog(s) == false)
            {
                response.Redirect("../Account/");
            }
        }

         
    }
    
}
