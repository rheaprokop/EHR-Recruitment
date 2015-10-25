<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="EHR.Recruitment.Upload" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">
  <div class="header">
  <div>
   <asp:Label ID="lblCandidateTitle" Text="Upload Candidate Profile" runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right;" class="ui-buttonset">
 <asp:HyperLink runat="server" NavigateUrl="~/Recruitment/Candidate.aspx?action=view"   ID="btnViewAll" Text="View All Candidates" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
  <asp:HyperLink  runat="server" NavigateUrl="~/Recruitment/AddCandidate.aspx" ID="btnAssign" Text="Create Profile" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
</div>
</div>
<asp:Panel runat="server" ID="pnlError" style="display:none; border: solid 1px #c0221f; background: #27cf00; width: 400px; padding: 20px; margin: 0 auto; margin-top: 30px;" >
                    <asp:Label runat="server" ID="lblError" style="font-size: 14px;"></asp:Label><br />
                    </asp:Panel>
                    
 
 
<table style="width: 70%; ">
<tr><td align="left">
            <table style="width:98%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">FIND A REQUEST</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
            
                <tbody> 
                     <tr>
                         <td class="form-label" >Select File to Upload: </td>
                         <td>
                         </td>
                         <td colspan="2">
                             <asp:FileUpload ID="fileUploadCan" runat="server" />
                         </td>
                     </tr>   
                     </tbody>
         
         </table> 
</td></tr>
<tr><td align="right">         
<asp:Button ID="btnSubmit" runat="server" OnClick="IsUpload_Click" Text="Upload File" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;" />
</td></tr>
</table>
  
<table style="width: 1000px">

<tr><td >           
</td></tr> 

</table>
</asp:Content>
