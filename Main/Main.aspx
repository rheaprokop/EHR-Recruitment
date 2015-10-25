<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="EHR.Views.Recruitment.Main" %>
<%@ MasterType VirtualPath="~/Views/Shared/Site.Master" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Top" ContentPlaceHolderID="TopContent" runat="server"> 
</asp:Content>
<asp:Content ID="Left" ContentPlaceHolderID="LeftContent" runat="server"> 
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">
 

<table style="width: 100%;" cellspacing="0" cellpadding="0" >
<tr> 
<td>
<asp:Panel runat="server" ID="pnlFullAdmin"> 
  <br />
<table style="width: 350px; float: left;">
    <tr><td colspan="3"><span class="logotext" style="display: block">WHAT WOULD YOU LIKE TO DO</span></td></tr>
     <tr> 
    <td><asp:ImageButton runat="server" ID="imgCreateReq" ImageUrl="~/Content/css/light/front/createrequest.png" PostBackUrl="~/Main/Request.aspx?form=create" /></td>
    <td><asp:ImageButton runat="server" ID="imgViewReq" ImageUrl="~/Content/css/light/front/viewrequest.jpg" PostBackUrl="~/Main/AllRequests.aspx" /></td>
    <td><asp:ImageButton runat="server" ID="imgFindReq" ImageUrl="~/Content/css/light/front/myrequests.png" PostBackUrl="~/Main/MyRequests.aspx" /> </td>
    </tr>
    <asp:Panel runat="server" ID="pnlHRPanel" Visible="false">
    <tr>
    <td><asp:ImageButton runat="server" ID="imgCreatCand" ImageUrl="~/Content/css/light/front/createcandidate.png" PostBackUrl="~/Main/AddCandidate.aspx"/></td>
    <td><asp:ImageButton runat="server" ID="imgFindCand" ImageUrl="~/Content/css/light/front/findcandidate.png"  PostBackUrl="~/Main/Candidate.aspx?action=find" /></td>
    <td><asp:ImageButton runat="server" ID="imgAssignPIC" ImageUrl="~/Content/css/light/front/assignpic.png" PostBackUrl="~/Main/AssignAgent.aspx" /></td>
    </tr>
    <tr>
    <td><asp:ImageButton runat="server" ID="imgOnBoard" ImageUrl="~/Content/css/light/front/onboard_box.png" PostBackUrl="~/Main/Training.aspx"/></td>
    <td><asp:ImageButton runat="server" ID="imgPSEncoder" ImageUrl="~/Content/css/light/front/ps_encoder.png"  PostBackUrl="~/Main/PSEncoder.aspx" /></td>
    <td><asp:ImageButton runat="server" ID="imgStatusReq" ImageUrl="~/Content/css/light/front/requeststatus.png"   PostBackUrl="~/Main/MyRequests.aspx" /> </td>
     </tr>     
     </asp:Panel>
    </table> 
</asp:Panel> 
</td>
<td>
    &nbsp;

</td>
</tr> 
</table>
<table style="width: 95%; margin-top: 20px;" >
   <tr><th colspan="4" style="text-align: left; font-weight: normal; height: 10px;" class="logotext">VIEWING RECENT REQUESTS</th></tr>
  <tr><td>
    <asp:GridView ID="gridRecentRequest" CellPadding="0" CellSpacing="0" GridLines="None" 
     AllowPaging ="true"  OnPageIndexChanging = "OnPaging"
      runat="server" CssClass="grid-zebra" style="width: 1100px;" OnRowDataBound="GridRecentRequest_RowDataBound"  
     PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="~/Content/css/light/front/first.png">
       <Columns>
        
    </Columns>
             
    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/Content/css/light/front/next.png" 
        PreviousPageImageUrl="~/Content/css/light/front/previous.png"  FirstPageImageUrl="~/Content/css/light/front/first.png"   />
    
</asp:GridView>  
     
</td></tr></table>
 
</asp:Content>
