<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Jobs.aspx.cs" Inherits="EHR.Recruitment.Jobs" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">

<!--PANEL REPORTS FOR JOBS-->
<asp:Panel runat="server" ID="pnlReportJobs">

<table style="margin-top: 5px; width: 1200px;"> 
<tr>
<td align="left"> 

<asp:DropDownList runat="server" ID="ddlPnlJobs" OnSelectedIndexChanged="DdlPnlJobs_SelectedIndexChanged"
  AutoPostBack="true" style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; padding: 10px; width: 250px;"
 
> 

</asp:DropDownList>
 
</td>
<td align="right">
&nbsp;&nbsp;
<asp:TextBox runat="server" ID="txtSearch" Text="Enter Candidate ID, Enter Job"
style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; width: 320px; color: #cccccc;"
></asp:TextBox>

<asp:Button ID="btnGoSearch" runat="server" OnClick="BtnGoSearch_Click" Text="Go"  CausesValidation="false"
                        CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
                        style="font-family: BebasNeue; font-size: 10px; letter-spacing: 1px; width: 60px; display: inline;" 
                        />
</td>
</tr>
<tr>
<td colspan="2">
<asp:UpdatePanel ID="pnlJobs" runat="server">
<ContentTemplate> 
 <asp:Panel runat="server" ID="pnlError" style="border: solid 1px #c0221f; background: #ffbde5; width: 260px; padding: 10px; margin: 0 auto; margin-bottom: 10px;" >
<asp:Label runat="server" ID="lblError" style="font-size: 11px;"></asp:Label><br />
</asp:Panel>
     <asp:GridView ID="gridJobs" CellPadding="0" CellSpacing="0" GridLines="None"  
     AllowPaging="false"  runat="server" CssClass="grid-zebra" style="width: 1100px;"  
     PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
       <Columns>  
          
   <asp:hyperlinkfield DataNavigateUrlFields="CandidateJobsID" 
   DataNavigateUrlFormatString="~/Recruitment/Request.aspx?form=view&flow=0&id={0}" ControlStyle-BorderStyle="none"  
   DataTextField="CandidateJobsID" HeaderText="View"   DataTextFormatString= "<img src='../App_Themes/front/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
      
    </Columns>
    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/App_Themes/front/next.png" 
        PreviousPageImageUrl="~/App_Themes/front/previous.png"  FirstPageImageUrl="~/App_Themes/front/first.png"   />
    
</asp:GridView> 

</ContentTemplate> 
</asp:UpdatePanel>
</td></tr>
</table>
</asp:Panel> <!--PANEL ENDS FOR REPORT JOBS -->
</asp:Content>
