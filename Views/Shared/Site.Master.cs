using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EHR.Model; 

namespace EHR.Views.Shared
{
    public partial class Site : System.Web.UI.MasterPage
    {
        PSModel _ps = new PSModel();
        AccountModel _accnt = new AccountModel();
        DBModel _db = new DBModel();
        GlobalAccountModel _account = new GlobalAccountModel(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //_accnt.ValidateUser(UserID, Page.Response); 
            lblEmployeeID.Text = UserID();
            lblFirstName.Text = _ps.GetName(UserID());
            int r = lblFirstName.Text.IndexOf(" "); 
                
            try
            {
                lblFirstName.Text = lblFirstName.Text.Substring(0, r);
            }
            catch (Exception ex)
            {
            }
            
        }

        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session.Abandon(); 
            HttpContext.Current.Response.Cookies.Remove("user");
            Response.Redirect("~/Account/Default.aspx");
        }

        public string UserID()
        {
            ///how to access to aspx page
            //////// string str = Master.UserID(); 
            string loggedUser = (string)(Session["emplid"]);
            string UserID =  _account.ReadCookie("user");
            return UserID;
        }
 
    }
}
