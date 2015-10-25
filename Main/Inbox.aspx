<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="EHR.Recruitment.Inbox" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
<asp:Panel runat="server" ID="pnlInbox">

<li><span style="padding-left: 30px;" class="selected"><a href="Dashboard.aspx">Dashboard</span></a></li>
</asp:Panel>
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">
<asp:Panel ID="pnlMainPage" runat="server" CssClass="pnlWelcome" style="margin-top: 5px;">
 <div class="header"> 

<asp:Label ID="lblCandidateTitle" Text="Viewing Request For Approval" runat="server" CssClass="logotext"></asp:Label>
    </div> 
 </asp:Panel>  
  <asp:GridView ID="gridInbox" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
 AllowPaging ="true" OnRowDataBound="GridInbox_RowDataBound" OnPageIndexChanging = "OnPaging"
 DataKeyNames = "inboxid" PageSize = "10" GridLines="None" Width="1200px"
  PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
 
       <Columns>  
            <asp:TemplateField>
        <HeaderTemplate>
            <asp:CheckBox ID="chkAll" runat="server" Enabled="true" Visible="false"
             />
        </HeaderTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="chk" runat="server" 
             />
        </ItemTemplate> 
    </asp:TemplateField>
        <asp:hyperlinkfield DataNavigateUrlFields="requestid, flowid"   ItemStyle-Width = "50px"
   DataNavigateUrlFormatString="~/Recruitment/Request.aspx?form=view&flow={1}&id={0}" ControlStyle-BorderStyle="none"  
   DataTextField="inboxid" HeaderText=""   DataTextFormatString= "<img src='../App_Themes/front/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
    
   <asp:BoundField ItemStyle-Width = "250px" DataField = "requestid"
           HeaderText = "Request ID"/>   
     
   <asp:BoundField ItemStyle-Width = "300px" DataField = "REQUESTOR"
           HeaderText = "Requestor"/>    
           
     <asp:BoundField ItemStyle-Width = "300px" DataField = "NAME_A"
           HeaderText = "NAME"/>    
                  
 <asp:BoundField ItemStyle-Width = "250px" DataField = "APPROVED"
           HeaderText = "status"/>             
  <asp:BoundField ItemStyle-Width = "250px" DataField = "APP"
           HeaderText = "APPROVAL DATE"/>       
   <asp:BoundField ItemStyle-Width = "250px" DataField = "ACTION"
           HeaderText = "LAST ACTION"/>             
                  
           </Columns> 
     
      <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/App_Themes/front/next.png" 
        PreviousPageImageUrl="~/App_Themes/front/previous.png"  FirstPageImageUrl="~/App_Themes/front/first.png"   />
</asp:GridView> 
 <asp:HiddenField ID="hfCount" runat="server" Value = "0" /><br />
<asp:Button ID="btnDelete" runat="server" Text="Delete Checked Records"
   OnClientClick = "return ConfirmDelete();" 
    CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;"
     />
        
</asp:Content>
