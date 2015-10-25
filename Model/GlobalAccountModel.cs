using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Security.Cryptography;
using System.Globalization;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using EHR.Views.Account;

namespace EHR.Model
{
    public class GlobalAccountModel
    {
        DBModel _db = new DBModel();
        MiscModel _misc = new MiscModel();
        EHR.Views.Account.ChangePassword _pwd = new EHR.Views.Account.ChangePassword(); 

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValidUser(string u, string p)
        {
            
            if (GetUserType(u) == 1) //user is using DB TYPE
            { 
                if (GetDBLogin(u, p) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (GetUserType(u) == 0) //user is using AD Type (Default)
            {
                if (IsActiveDirectoryLogin(u, p) == true)
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

        public int GetUserType(string userid)
        {
            
            int usertype;
            string sql = "SELECT USERTYPE FROM LIB_USERS WHERE USERID = '" + userid + "'";
            _db.GetConn();
            OleDbCommand cmd = new OleDbCommand(sql, _db.conn);
            OleDbDataReader _rd = cmd.ExecuteReader();
            if(_rd.Read())
            {
                usertype = Convert.ToInt32(_rd["USERTYPE"]);
            }
            else
            {
                usertype = 0; 
            }

            return usertype;
        } 
         

        public bool GetDBLogin(string u, string p)
        {
            
            string newPwd = HashedPwd(u, p);
            string sql = "SELECT COUNT(USERID) FROM LIB_USERS WHERE USERID = '" + u + "'" +
                         "AND PASSWORD = '" + newPwd + "'";
            try
            {
                _db.GetConn();
                OleDbCommand cmd = new OleDbCommand(sql, _db.conn); 
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
  
        public int NoOfLogTrials(string email)
        {
            int tried;
            _db.GetConn();
            string sql = "SELECT tries FROM {1}" +
                            "where EMAIL_ADDRESS = '{0}'";
            using (OleDbDataReader rd = _db.GetDataReader(string.Format(sql, email, "DBUSERS")))
            {

                if (rd.Read())
                {
                    tried = Convert.ToInt32(rd["tries"]);

                }
                else
                {
                    tried = 0;
                }
            }
            return tried;
        }

        public void LockUserAccount(string email)
        {
            int noOfTrials = NoOfLogTrials(email);
            int count = noOfTrials + 1;
            string table = "DBUSERS";
            _db.GetConn();
            string updateTrials = "UPDATE DBUSERS SET " +
                    "tries = '" + count + "' where EMAIL_ADDRESS = '{0}'";
            _db.GetExecuteNonQuery(string.Format(updateTrials, email, table));

            int noTrials = NoOfLogTrials(email);

            if (noTrials >= 3)
            {
                string lockSql = "UPDATE {1} SET " +
                        "STATUS = '3' where EMAIL_ADDRESS = '{0}'";
                _db.GetExecuteNonQuery(string.Format(lockSql, email, table));

            }
        }

        public void ResetTries(string userid)
        {
            _db.GetConn();
            string clrTriesSql = "UPDATE {0} SET " +
                        "[tries] = '0' where [userid] = '{1}'";
            _db.GetExecuteNonQuery(string.Format(clrTriesSql, "DBUSERS", userid));

        }

        public int GetUserStatus(string u)
        {
            int status;
            _db.GetConn();
            string sql = "SELECT status FROM {0} where email_address = '{1}'";
            using (OleDbDataReader _rd = _db.GetDataReader(string.Format(sql, "DBUSERS", u)))
            {
                if (_rd.Read())
                {
                    status = Convert.ToInt32(_rd["status"]);
                }
                else
                {
                    status = 0;
                }
            }
            return status;
        }

        public string GetUserID(string u)
        {
            string userid, table = "DBUSERS";
            _db.GetConn();
            string sql = "SELECT userid FROM {1} " +
                           "WHERE [email_address] = '{0}'";
            using (OleDbDataReader rd = _db.GetDataReader(string.Format(sql, u, table)))
            {
                if (rd.Read())
                {
                    userid = Convert.ToString(rd["userid"]);
                }
                else
                {
                    userid = "";
                }
            }
            return userid;

        }

        public string GetEmailAddress(string id)
        {
            string email;
            _db.GetConn();
            string sql = "SELECT email_address FROM {0} " +
                           "WHERE [userid] = '{1}'";
            using (OleDbDataReader rd = _db.GetDataReader(string.Format(sql, "DBUSERS", id)))
            {
                if (rd.Read())
                {
                    email = Convert.ToString(rd["email_address"]);
                }
                else
                {
                    email = "";
                }
            }
            return email;
        }

        public void GetUserFirstAndLastName(string id, out string fname, out string lname)
        {
            _db.GetConn();
            string table = "DBUSERS";
            string sqlAccount = "SELECT firstname, lastname FROM {1} " +
                           "WHERE [userid] = '{0}'";
            using (OleDbDataReader rd = _db.GetDataReader(string.Format(sqlAccount, id, table)))
            {

                if (rd.Read())
                {
                    fname = Convert.ToString(rd["firstname"]);
                    lname = Convert.ToString(rd["lastname"]);
                }
                else
                {
                    fname = "";
                    lname = "";
                }
            }

        }

        public string ShowFullName(string username)
        {
            string fname, lname;
            GetUserFirstAndLastName(GetUserID(username), out fname, out lname);
            string fullname = fname + " " + lname;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fullname);
        }

        public void ResetPassTrials(string userid)
        {
            _db.GetConn(); 
            string updateTries = "UPDATE DBUSERS SET tries = '0' " +
                                "WHERE USERID = '" + userid + "'";
            _db.GetExecuteNonQuery(string.Format(updateTries, userid));
        }

        

        public void GetAllStatus(DropDownList ddl)
        {
            _db.GetConn();
            string sql = "SELECT statusID, statusDesc FROM {0} ORDER BY statusID ASC";
            DataTable _dt = _db.GetTable(string.Format(sql, "DBUSERS"));

            ddl.DataSource = _dt;
            ddl.DataValueField = "statusID";
            ddl.DataTextField = "statusDesc";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", "")); 
        }
 
        public string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rngRandom = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rngRandom.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string CreateHash(string str, string salt)
        {
            string SaltAndPwd = String.Concat(str, salt);
            string HashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(SaltAndPwd, "SHA1");
            return HashedPwd;
        }
 

        public void SaveActivities(string activity_owner_id, string strActivities)
        {
            string sql = "INSERT INTO {0} ([user_id], [activity], [dbname], [activitydate]) " +
                         "VALUES ({1}, '{2}', 'e-recruitment', '{3}')";
            try
            {
                _db.GetConn();
                //_db.GetExecuteNonQuery(string.Format(sql, _vars.Activities(), activity_owner_id, strActivities, DateTime.Now));
            }
            catch (Exception ex)
            {

            }
            finally
            { 
            }
        }

        public void WriteCookie(string name, string value, bool remember)
        {
            HttpCookie MyCookie = new HttpCookie(name);
            MyCookie.Value = value;

            if (remember == true)
            {
                MyCookie.Expires = DateTime.Now.AddYears(1);
            }
            HttpContext.Current.Response.Cookies.Add(MyCookie);

        }


        public string ReadCookie(string name)
        { 
            HttpCookie myCookie = new HttpCookie(name);
            myCookie = HttpContext.Current.Request.Cookies[name];

            // Read the cookie information and display it.
            if (myCookie != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[name].Value;
                return cookie;
            }
            else
            {
                return "";
            }
        }

         
    }
}
