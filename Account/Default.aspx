<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EHR.Views.Account.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
     <title>E-Human Resources Applications System</title>
     <link href="../../Content/css/light/login.css" rel="stylesheet" type="text/css" />
     <link href="../../Content/css/light/Style.css" rel="stylesheet" type="text/css" />
     <link href="../../Content/css/jQuery/jquery.ui.all.css" rel="stylesheet" type="text/css" />
      
     <script src="../../Scripts/jQuery/jquery-1.6.2.js" type="text/javascript"></script>
     <script src="../../Scripts/jQuery/jquery.ui.core.js" type="text/javascript"></script>
     <script src="../../Scripts/jQuery/jquery.ui.widget.js" type="text/javascript"></script>
     <script src="../../Scripts/jQuery/jquery.ui.button.js" type="text/javascript"></script> 
     <script src="../../Scripts/jQuery/jquery.ui.tabs.js" type="text/javascript"></script>
     <script src="../../Scripts/jQuery/jquery.ui.datepicker.js" type="text/javascript"></script>
     <script src="../../Scripts/jQuery/jquery.ui.dialog.js" type="text/javascript"></script>
     <script src="../../Scripts/jQuery/jquery.ui.position.js" type="text/javascript"></script> 
     
      <style type="text/css" media="screen">
            .slides_container {
                width:570px;
                height:270px;
            }
            .slides_container div {
                width:570px;
                height:270px;
                display:block;
            }
        </style>
    
        <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
        <script src="slides.js" type="text/javascript"></script>
</head>
<body>
<form runat="server" id="frmLogin">
<table align="center" border="0" style="margin: 0px auto; width: 1050px;">
<tr>
<td>
<div style="float: left; margin: -10px auto;">
<span style="padding: 10px; font-family: Bebas Neue; font-size: 30px;">Welcome to E-Human Resources Application Systems</span>
<span style="display:block;"> <img src="../Content/images/slide2.jpg"></span>
<span style="display:block; font-family: Bebas Neue; font-size: 18px; color: #cccccc; margin-right: 30px;">Powered by: Wistron Czech Republic</span>

</div>
</td>
<td>
<div id="login" style="float: right; margin-top: 80px;">
<h1><img src="../../Content/css/light/images/logo_login.png" /></h1> 
 
	<p>
		<label>Username</label>
		<asp:TextBox runat="server" ID="txtEmployeeID" size="30" CssClass="input" 
            style="font-family: Bebas Neue; color: #ccc; padding: 5px;"></asp:TextBox>
	</p>
	<p>
		<label>Password</label>
		<asp:TextBox runat="server" ID="txtPassword" TextMode="Password" size="30"  
            CssClass="input" style="font-family: Bebas Neue; color: #ccc; padding: 5px;"></asp:TextBox>
	</p>
	<p><label><asp:CheckBox runat="server" ID="rememberMe" /> Remember Me</label>&nbsp;&nbsp;
	<label><asp:CheckBox runat="server" ID="savePwd" /> Save Password</label></p>
	<p class="submit">
		<asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_OnClick" 
	   Text="Logon" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: Bebas Neue; font-size: 16px; letter-spacing: 1px; float: left;" />
		
	</p>



<p id="nav">
<a href="#" title="Password Lost and Found">Lost your password?</a>
</p>


	<p id="backtoblog"><a href="mailto:wcz_apsupport@wistron.com" title="Are you lost?">&larr; Contact AP Support</a></p>
	</div>

</td>
</tr>
</table>

 	<script type="text/javascript">
 	    $(function() {
 	        // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!


 	        $("#pnlDialog").dialog({
 	            height: 230,
 	            width: 430,
 	            modal: true
 	        });
 	    });
	</script>
	<asp:Panel runat="server" ID="pnlDialog" style="display:none" CssClass="mws-jui-dialog">
	 
        <asp:Label runat="server" ID="lblErrorMsg"></asp:Label> 
                         
	</asp:Panel>
	 </form>
	  
	  
</body>
</html>
