using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using EHR.Model;
using System.Web.Security;

namespace EHR.Views.Account
{
    public partial class Default : System.Web.UI.Page
    {
        GlobalAccountModel _accnt = new GlobalAccountModel();
        DBModel _db = new DBModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {           
           
            pnlDialog.Visible = false;
            if (Request.QueryString["ltype"] == "out")
            { 
                SignOut();
            }

        }

        protected void BtnSubmit_OnClick(object sender, EventArgs e)
        {
             _accnt.Username = txtEmployeeID.Text.Trim();
             _accnt.Password = txtPassword.Text.Trim();

            if (_accnt.IsValidUser(txtEmployeeID.Text, txtPassword.Text) == true)
            {
               LogUser(_accnt.Username, _accnt.Password);
                Response.Redirect("~/Main/Main.aspx");
            }
            else
            { 
                lblErrorMsg.Text = "Invalid Login";
            }

        }


        private void LogUser(string u, string p)
        {

            //check if emailaddress exists in database
            if (_accnt.IsValidLogin(u, p) == true)
            {
                bool isCheckedRemember = rememberMe.Checked;
               // Session["emplid"] = u.Trim();
                _accnt.WriteCookie("user", u, isCheckedRemember);
                
             //   _accnt.ResetPassTrials(_accnt.GetUserID(u));

                Response.Redirect("~/Main/Main.aspx");
            }
            else
            {
                if (_accnt.NoOfLogTrials(u) >= 3)
                { 
                    lblErrorMsg.Text = "Your account is locked.";
                }
                else
                { 
                    lblErrorMsg.Text = "Invalid Password.";
                    _accnt.LockUserAccount(u);
                }
            }
        }



        private void SignOut()
        { 
             var cookie = new HttpCookie("user", "");
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Set(cookie);
            FormsAuthentication.SignOut();
            
        }
    }
}
