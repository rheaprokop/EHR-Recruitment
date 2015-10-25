using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using ehr_email;
using System.IO;

namespace EHR.Model
{
    public class MiscModel
    {

        DBModel _db = new DBModel();
        PSModel _ps = new PSModel(); 

        public void GetMsgBox(string msgno, Panel pnl, Label lbl)
        {
            pnl.Visible = true; 
            lbl.Text = GetMsgType(msgno); 
        }

        public string GetWebAppRoot()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
                return HttpContext.Current.Request.Url.Host;
            else
                return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
        }



        public string GetMsgType(string msgno)
        {
            string Msg;
            switch (msgno)
            {
                case "1":
                    Msg = "The username or password you entered is invalid.";
                    break;
                case "2":
                    Msg = "You did not enter any username or password. ";
                    break;
                case "3":
                    Msg = "You don't have access. Please contact IT Support.";
                    break;
                case "4":
                    Msg = "You don't have any access to any application. Please contact EHR Admin.";
                    break;
                case "5":
                    Msg = "User has been added.";
                    break;
                case "6":
                    Msg = "Your account has been locked. Please contact the administrator.";
                    break;
                case "7":
                    Msg = "Please enter Employee ID / Username.";
                    break;
                case "8":
                    Msg = "Please enter Password.";
                    break;
                case "9":
                    Msg = "Your account has expired.";
                    break; 
                case "10":
                    Msg = "Employee not found.";
                    break;
                case "11":
                    Msg = "Are you sure you want to submit?";
                    break;
                case "12":
                    Msg = "Invalid format of Salary/Bonus amount!";
                    break;
                case "13":
                    Msg = "Nothing was changed!";
                    break;
                case "14":
                    Msg = "Your request has been sent.";
                    break;
                case "15":
                    Msg = "Bonus Amount is missing!";
                    break;
                case "16":
                    Msg = "Department does not match!";
                    break;
                case "17":
                    Msg = "Changed department is the same as current!";
                    break;
                case "18":
                    Msg = "Changed position is the same as current!";
                    break;
                case "19":
                    Msg = "Successfully Approve Transaction";
                    break;
                case "20":
                    Msg = "Successfully Rejected Transaction";
                    break;
                case "21":
                    Msg = "Group has been added.";
                    break;
                case "22":
                    Msg = "Application has been added.";
                    break;
                case "23":
                    Msg = "User successfully edited.";
                    break;
                case "24":
                    Msg = "You don't have permission to access this page.";
                    break;
                case "25":
                    Msg = "User already exist!";
                    break;
                case "26":
                    Msg = "Effective date is missing!";
                    break;
                case "27":
                    Msg = "Employee position not found! Please contact HR Administrator.";
                    break;
                case "28":
                    Msg = "Successfully Close Request";
                    break;
                case "29":
                    Msg = "This mail has been deleted.";
                    break;
                case "30":
                    Msg = "Password has been changed.";
                    break;
                case "31":
                    Msg = "Please select a new position!";
                    break;
                case "32":
                    Msg = "You don't have signature available. You cannot proceed without one.";
                    break;
                case "33":
                    Msg = "Type already exist!";
                    break;
                case "34":
                    Msg = "Reason already exist!";
                    break;
                case "35":
                    Msg = "Severence already exist!";
                    break;
                case "36":
                    Msg = "Type has been added.";
                    break;
                case "37":
                    Msg = "Reason has been added.";
                    break;
                case "38":
                    Msg = "Severence has been added.";
                    break;
                case "39":
                    Msg = "New Group has been added.";
                    break;
                case "40":
                    Msg = "Group already exist!";
                    break;
                case "41":
                    Msg = "Evaluation successfully saved!";
                    break;
                default:
                    Msg = "";
                    break;
            }

            return Msg;
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

        public bool IsEmailNotificationTest(string m_type, string subject, string reqID)
        {
            string sqlMode = "SELECT TEST_MODE FROM EHR_CONFIG WHERE APPLICATION = 'E-Recruitment'";
            OleDbDataReader rd = _db.GetDataReader(sqlMode);
            string test_mode;
            if (rd.Read())
            {
                test_mode = Convert.ToString(rd["TEST_MODE"]);
            }
            else
            {
                test_mode = "";
            }

            if (test_mode == "1") // 1 = is for testing mode
            {
                string sql = "SELECT TEST_EMAIL_TO, TEST_EMAIL_FROM FROM EHR_CONFIG";
                OleDbDataReader _rd = _db.GetDataReader(sql);
                string test_email_to;
                string test_email_fr;
                if (_rd.Read())
                {
                    test_email_to = Convert.ToString(_rd["TEST_EMAIL_TO"]);
                    test_email_fr = Convert.ToString(_rd["TEST_EMAIL_FROM"]);
                }
                else
                {
                    test_email_to = "";
                    test_email_fr = "";
                }

                if (test_email_to != "" && test_email_fr != "")
                {
                    string mail_type = m_type;
                    string sender_ = "noreply@wistron.com ";
                    //string recipient = "rhea_prokop@wistron.com";
                    //string recipient_name = "Rhea Prokop";
                    string parameters;
                    string recipient = test_email_to;
                    string recipient_name = "Receiver";
                    string cc = "";
                    if (m_type == "Notification")
                    {
                         parameters = "Approval Link: <a href='http://ehr.qas.wcz.wistron/Views/Status/RQ04.aspx?id='" + reqID + "'>Click To Approve</a>";

                    }
                    else
                    {
                         parameters = "";
                    }


                    Cls_Email.sendmail(mail_type, sender_, recipient, cc, subject, parameters, "", recipient_name);

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

        public string GetRequestor(string requestid, string table)
        {
            _db.GetConn();
            string reqID;
            string sql = "SELECT REQUESTOR FROM " + table + " WHERE REQUESTID = '" + requestid + "'";
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

        public void PopulateCountry(DropDownList ddlCountry)
        {
            string sql = "select COUNTRYID, DESCRSHORT  from LIB_PS_COUNTRY";
            DataTable _dt = _db.GetTable(sql);

            ddlCountry.DataSource = _dt;
            ddlCountry.DataTextField = "DESCRSHORT";
            ddlCountry.DataValueField = "COUNTRYID";
            ddlCountry.DataBind();

            ddlCountry.SelectedValue = "232";
        }
    }
}

public class EncryptDecryptQueryString
{
    private byte[] key = { };
    private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
    public string Decrypt(string stringToDecrypt, string sEncryptionKey)
    {
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,
              des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string Encrypt(string stringToEncrypt, string SEncryptionKey)
    {
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,
              des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

     

}


