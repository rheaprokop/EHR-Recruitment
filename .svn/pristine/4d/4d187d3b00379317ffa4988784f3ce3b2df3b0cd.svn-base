<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="EHR.Recruitment.Forms" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Top" ContentPlaceHolderID="TopContent" runat="server"> 
  
</asp:Content>
<asp:Content ID="Left" ContentPlaceHolderID="LeftContent" runat="server"> 

<asp:Panel runat="server" ID="pnlRequestManager">
<li><span style="padding-left: 30px;"><a href="AllRequests.aspx">Request Manager</span></a></li>
</asp:Panel>
<asp:Panel runat="server" ID="pnlCandidateManager">
<li><span style="padding-left: 30px;"><a href="Candidate.aspx?action=view">Candidate Manager</span></a></li> 
</asp:Panel>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
<asp:Panel ID="pnlMainPage" runat="server" CssClass="pnlWelcome" style="margin-top: 5px;">
 <div class="header"> 

<asp:Label ID="lblCandidateTitle" Text="Viewing Requests" runat="server" CssClass="logotext"></asp:Label>
    </div> 
 </asp:Panel> 
 
<table style="margin-top: 5px; width: 1200px;">

<tr>
<td align="left"> 

<asp:DropDownList runat="server" ID="ddlReportRequest" OnSelectedIndexChanged="DdlReportRequest_SelectedIndexChanged"
  AutoPostBack="true" style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; padding: 10px; width: 250px;"
 
>
<asp:ListItem Value="">Filter Requests</asp:ListItem>
<asp:ListItem Value="1">By Request Date</asp:ListItem>
<asp:ListItem Value="2">By Request ID</asp:ListItem>

</asp:DropDownList>
 
</td>
<td align="right">
&nbsp;&nbsp;
<asp:TextBox runat="server" ID="txtSearchRequest" Text="Enter Employee ID, Requestor, Requestor ID"
style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; width: 320px; color: #cccccc;"
></asp:TextBox>

<asp:Button ID="btnGo" runat="server" OnClick="BtnGoSearch_Click" Text="Go"  CausesValidation="false"
                        CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
                        style="font-family: BebasNeue; font-size: 10px; letter-spacing: 1px; width: 60px; display: inline;" 
                        />
</td>
</tr>
<tr>
<td colspan="2">
<asp:UpdatePanel ID="pnlView" runat="server">
<ContentTemplate> 
 <asp:Panel runat="server" ID="pnlErrorReq" style="border: solid 1px #c0221f; background: #ffbde5; width: 260px; padding: 10px; margin: 0 auto; margin-bottom: 10px;" >
<asp:Label runat="server" ID="lblReqError" style="font-size: 11px;"></asp:Label><br />
</asp:Panel>
     <asp:GridView ID="gridAllRequest" CellPadding="0" CellSpacing="0" GridLines="None"  
     AllowPaging="false"  runat="server" CssClass="grid-zebra" style="width: 1200px;"  
     PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
       <Columns>  
          
   <asp:hyperlinkfield DataNavigateUrlFields="RequestID" 
   DataNavigateUrlFormatString="~/Recruitment/Request.aspx?form=view&flow=0&id={0}" ControlStyle-BorderStyle="none"  
   DataTextField="RequestID" HeaderText="View"   DataTextFormatString= "<img src='../App_Themes/front/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
      
    </Columns>
    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/App_Themes/front/next.png" 
        PreviousPageImageUrl="~/App_Themes/front/previous.png"  FirstPageImageUrl="~/App_Themes/front/first.png"   />
    
</asp:GridView> 

</ContentTemplate> 
</asp:UpdatePanel>
</td></tr>
</table>
<asp:Panel ID="Panel1" runat="server" CssClass="pnlWelcome" style="margin-top: 20px;">
    <div class="header"> 
        <asp:Label ID="Label1" Text="Viewing Candidates" runat="server" CssClass="logotext"></asp:Label>
    </div> 
 </asp:Panel> 
<table style="margin-top: 5px; width: 1200px;">

<tr>
<td align="left"> 
<asp:DropDownList runat="server" ID="ddlCandidates" OnSelectedIndexChanged="DdlCandidates_SelectedIndexChanged"
  AutoPostBack="true" style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; padding: 10px; width: 250px;"
 
>
<asp:ListItem Value="">Filter Candidates</asp:ListItem>
<asp:ListItem Value="1">By Candidate ID</asp:ListItem>
<asp:ListItem Value="2">By Status</asp:ListItem>

</asp:DropDownList>
 
</td>
<td align="right">
&nbsp;&nbsp;
<asp:TextBox runat="server" ID="txtCandidate" Text="Enter Candidate ID, Name"
style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; width: 320px; color: #cccccc;"
></asp:TextBox>

<asp:Button ID="btnCandidate" runat="server" OnClick="BtnGoCandidateSearch_Click" Text="Go"  CausesValidation="false"
                        CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
                        style="font-family: BebasNeue; font-size: 10px; letter-spacing: 1px; width: 60px; display: inline;" 
                        />
</td>
</tr>
<tr>
<td colspan="2">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate> 
     <asp:Panel runat="server" ID="pnlError" style="border: solid 1px #c0221f; background: #ffbde5; width: 260px; padding: 10px; margin: 0 auto; margin-bottom: 10px;" >
<asp:Label runat="server" ID="lbl_CanError" style="font-size: 11px;"></asp:Label><br />
</asp:Panel> 
     <asp:GridView ID="gridCandidates" CellPadding="0" CellSpacing="0" GridLines="None"  
     AllowPaging="false"  runat="server" CssClass="grid-zebra" style="width: 1100px;"  
     PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
       <Columns>  
          
   <asp:hyperlinkfield DataNavigateUrlFields="CANDIDATEID" 
   DataNavigateUrlFormatString="~/Recruitment/ViewCandidate.aspx?action=view&candidate={0}" ControlStyle-BorderStyle="none"  
   DataTextField="CANDIDATEID" HeaderText="View"   DataTextFormatString= "<img src='../App_Themes/front/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
      
    </Columns>
    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/App_Themes/front/next.png" 
        PreviousPageImageUrl="~/App_Themes/front/previous.png"  FirstPageImageUrl="~/App_Themes/front/first.png"   />
    
</asp:GridView> 

</ContentTemplate> 
</asp:UpdatePanel>
</td></tr>
</table>  
</asp:Content>
