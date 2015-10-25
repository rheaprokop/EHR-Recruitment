<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="EHR.Views.Account.ChangePassword"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
    <label>
        Username<br />
        <asp:TextBox runat="server" ID="EmpIDTextbox" size="30" CssClass="input" Style="font-family: Bebas Neue;
            color: #ccc; padding: 5px;" ReadOnly="True"></asp:TextBox></label><br />
    <label>
        Old Password<br />
        <asp:TextBox runat="server" ID="OldPasswordTextbox" TextMode="Password" size="30"
            CssClass="input" Style="font-family: Bebas Neue; color: #ccc; padding: 5px;"></asp:TextBox></label>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Wrong Old Password"
        ControlToValidate="OldPasswordTextbox" OnServerValidate="IsOldPwdValid" Display="None"></asp:CustomValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="CustomValidator1">
    </asp:ValidatorCalloutExtender>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your old password!"
        Display="None" ControlToValidate="OldPasswordTextbox"></asp:RequiredFieldValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator1">
    </asp:ValidatorCalloutExtender>
    <br />
    <label>
        New Password<br />
        <asp:TextBox runat="server" ID="NewPasswordTextbox" TextMode="Password" size="30"
            CssClass="input" Style="font-family: Bebas Neue; color: #ccc; padding: 5px;"></asp:TextBox></label>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Password policy check failed!"
        ValidationExpression="(?=^.{6,255}$)((?=.*\\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*"
        ControlToValidate="NewPasswordTextbox" Display="None"></asp:RegularExpressionValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RegularExpressionValidator1">
    </asp:ValidatorCalloutExtender>
    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Password did not passed password history policy!"
        Display="None" ControlToValidate="NewPasswordTextbox" OnServerValidate="IsPwdHistoryValid"></asp:CustomValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="CustomValidator2">
    </asp:ValidatorCalloutExtender>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter new password!"
        Display="None" ControlToValidate="NewPasswordTextbox"></asp:RequiredFieldValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator2">
    </asp:ValidatorCalloutExtender>
    <br />
    <label>
        Confirm Password<br />
        <asp:TextBox runat="server" ID="ConfirmPasswordTextbox" TextMode="Password" size="30"
            CssClass="input" Style="font-family: Bebas Neue; color: #ccc; padding: 5px;"></asp:TextBox></label>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Confirm Password doesn't match!"
        ControlToCompare="NewPasswordTextbox" ControlToValidate="ConfirmPasswordTextbox"
        Display="None"></asp:CompareValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CompareValidator1">
    </asp:ValidatorCalloutExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter confirm password!"
        Display="None" ControlToValidate="ConfirmPasswordTextbox"></asp:RequiredFieldValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator3">
    </asp:ValidatorCalloutExtender><br />
    <p class="submit">
        <asp:Button ID="SubmitButton" runat="server" Text="Change Password" CssClass="ui-button ui-button-text-only ui-widget ui-state-default"
            Style="font-family: Bebas Neue; font-size: 16px; letter-spacing: 1px; float: left;"
            OnClick="SubmitButton_Click" /><br />
    </p>
    <script type="text/javascript">
 	    $(function() {
 	        // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!

 	        $("#ctl00_mainContent_pnlDialog").dialog({
 	            height: 230,
 	            width: 430,
 	            modal: true
 	        });
 	    });
    </script>

    <asp:Panel runat="server" ID="pnlDialog" Style="display: none" CssClass="mws-jui-dialog"
        Visible="false">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
    </asp:Panel>
</asp:Content>
