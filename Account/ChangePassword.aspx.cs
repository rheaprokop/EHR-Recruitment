using System;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EHR.Model;
using System.Text.RegularExpressions;

namespace EHR.Views.Account
{
   
    public partial class ChangePassword : System.Web.UI.Page
    {
        public DBModel dbcon = new DBModel();
        public string strSQL;
        public MiscModel msg = new MiscModel();
        protected void Page_Load(object sender, EventArgs e)
        {    
            dbcon.GetConn();
            //Session["UserID"] = "P104006";
            try
            {
                EmpIDTextbox.Text = Session["UserID"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool checkpass(string userid)
        {
            string pwd = getMD5Hash(OldPasswordTextbox.Text);
            strSQL = "select * from lib_users where userid ='" + userid + "' and password='" + pwd + "'";
            if (dbcon.GetTable(strSQL).Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getMD5Hash(string strToHash)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Obj = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);

            bytesToHash = md5Obj.ComputeHash(bytesToHash);

            string strResult = "";

            foreach (byte b in bytesToHash)
            {
                strResult += b.ToString("x2");
            }

            return strResult;
        }

        bool password_policy_check(string pwd)
        {
            Regex myRegex = new Regex("(?=^.{6,255}$)((?=.*\\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*");
            if (myRegex.IsMatch(pwd))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void save_pwdhistory(string userid)
        {
            string pwd = getMD5Hash(NewPasswordTextbox.Text);
            strSQL = "select * from lib_pwdhistory where USERID='" + userid + "'";
            if (dbcon.GetTable(strSQL).Rows.Count == 6)
            {
                strSQL="update lib_pwdhistory set password='" + pwd + "', date_change=sysdate where userid='" + userid + "' and date_change=(select min(date_change)from lib_pwdhistory where userid='" + userid + "' )";
                dbcon.GetExecuteNonQuery(strSQL);
            }
            else
            {
                strSQL="insert into lib_pwdhistory (userid,password,date_change) values ('" + userid + "','" + pwd + "', sysdate )";
                dbcon.GetExecuteNonQuery(strSQL);
            }
        }

        bool check_pwdhistory()
        {
            string pwd = getMD5Hash(NewPasswordTextbox.Text);
            strSQL = "select * from lib_pwdhistory where userid='" + EmpIDTextbox.Text + "' and password='" + pwd + "'";
            if (dbcon.GetTable(strSQL).Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void SubmitButton_Click(object sender,EventArgs e)
        {
            if (checkpass(EmpIDTextbox.Text) == true)
            {
                if (check_pwdhistory() == true)
                {
                    try
                    {
                        string pwd = getMD5Hash(NewPasswordTextbox.Text);
                        strSQL = "update lib_users set password='" + pwd + "' where userid='" + EmpIDTextbox.Text + "'";
                        dbcon.GetExecuteNonQuery(strSQL);
                        save_pwdhistory(EmpIDTextbox.Text);
                        //lblMsg.Text = "Successfully updated!";
                        msg.GetMsgBox("30", pnlDialog, lblMsg);
                        Response.Redirect("~/Views/Home/");
                    }
                    catch (Exception ex)
                    {
                        //lblMsg.Text = ex.Message;
                        throw ex;
                    }
                }
                else
                {
                    //lblMsg.Text = "Password did not pass password history policy!";
                    //replaced by custom validator
                }
            }
            else
            {
                //lblMsg.Text = "Wrong Old Password!";
                //replaced by custom validator
            }
        }

        protected void IsOldPwdValid(object sender, ServerValidateEventArgs e)
        {
            if (checkpass(EmpIDTextbox.Text) == true)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected void IsPwdHistoryValid(object sender, ServerValidateEventArgs e)
        {
            if (check_pwdhistory() == true)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

    }
}
