<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="MyRequests.aspx.cs" Inherits="EHR.Recruitment.MyRequests" %>
<%@ MasterType VirtualPath="~/Views/Shared/Site.Master" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
<script type = "text/javascript">
    function ConfirmDelete()
    {
       var count = document.getElementById("<%=hfCount.ClientID %>").value;
       var gv = document.getElementById("<%=gridMyRequests.ClientID%>");
       var chk = gv.getElementsByTagName("input");
       for(var i=0;i<chk.length;i++)
       {
            if(chk[i].checked && chk[i].id.indexOf("chkAll") == -1)
            {
                count++;
            }
       }
       if(count == 0)
       {
            alert("No records to delete.");
            return false;
       }
       else
       {
            return confirm("Do you want to delete " + count + " records.");
       }
    }
</script>
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
  
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
<asp:Panel runat="server" ID="pnlRequestManager">
<li><span style="padding-left: 30px;"><a href="AllRequests.aspx">Request Manager</span></a></li>
</asp:Panel>
<asp:Panel runat="server" ID="pnlCandidateManager">
<li><span style="padding-left: 30px;"><a href="Candidate.aspx?action=view">Candidate Manager</span></a></li> 
</asp:Panel>
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">
<asp:Panel ID="pnlMainPage" runat="server" CssClass="pnlWelcome" style="margin-top: 5px;">
  <div class="header">
  <div>
   <asp:Label ID="lblCandidateTitle" Text="My Requests" runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right; margin-top: -10px;" class="ui-buttonset">
 <asp:HyperLink runat="server" NavigateUrl="~/Main/Main.aspx"   ID="btnMainPage" Text="Dashboard" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
 <asp:HyperLink runat="server" NavigateUrl="~/Main/AllRequests.aspx"   ID="btnViewAll" Text="View All Request" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />

<asp:HyperLink  runat="server" NavigateUrl="~/Main/Request.aspx?form=create" ID="btnAssign" Text="Create Request" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
<asp:HyperLink  runat="server" NavigateUrl="~/Main/MyRequests.aspx" Enabled="false" ID="btnMyRequests" Text="My Requests" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />

</div>
</div> 
 </asp:Panel>  
 
 
<table style="border: 0px; width: 1200px;">
<tr><td>                
        <asp:Panel runat="server" ID="pnlFindRequest" > 
        <table style="width: 95%; border: 0px;" align="left">
        <tr>
        <td  align="left">
                    <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                         <thead>
                            <tr>
                                <th colspan="2" align="left" style="padding: 10px; font-weight: normal;">FIND A REQUEST</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th colspan="2"></th>
                            </tr>
                        </tfoot>
                    
                        <tbody> 
                             <tr>
                                 <td class="form-label" >Enter Request Form ID: </td>
                                 <td>
                                 <asp:TextBox ID="txtRequestFrmID" runat="server"  Enabled="true" style="width: 200px; margin-left: 10px" CssClass="form-field" >
                                 </asp:TextBox> <asp:Button ID="btnRequestFrmID" runat="server"  OnClick="IsFindRequest" Text="Find Request" CssClass="ui-button ui-button-text-only ui-widget ui-state-default " style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px;display: inline;" />
                                  </td>
                                  
                             </tr>  
                             
                             
                             </tbody></table> 
        </td></tr>
        </table>
         
        </asp:Panel>
 </td></tr>
 
 <tr><td>
<asp:GridView ID="gridMyRequests" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra"  
 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"  Width="1200px"   OnRowDataBound="GridMyRequests_DataBound"
 DataKeyNames = "RequestID" PageSize = "10" GridLines="None">
    
     
       <Columns>  
            <asp:TemplateField>
        <HeaderTemplate>
            <asp:CheckBox ID="chkAll" runat="server" Enabled="true" Visible ="false"
             />Delete
        </HeaderTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="chk" runat="server"
             />
        </ItemTemplate> 
    </asp:TemplateField>
    
    <asp:hyperlinkfield DataNavigateUrlFields="RequestID"  
   DataNavigateUrlFormatString="~/Main/ViewRequest.aspx?id={0}" ControlStyle-BorderStyle="none"  
   DataTextField="RequestID" HeaderText="View"   DataTextFormatString= "<img src='../../Content/css/light/front/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
       <asp:BoundField ItemStyle-Width = "150px" DataField = "RequestID"
       HeaderText = "Request ID"/> 
   
    <asp:BoundField ItemStyle-Width = "150px" DataField = "Name"
       HeaderText = "Requestor"/> 
 
    <asp:BoundField ItemStyle-Width = "150px" DataField = "DeptCode"
       HeaderText = "Department"/> 
    <asp:BoundField ItemStyle-Width = "150px" DataField = "POSITION"
       HeaderText = "Position"/>        
    <asp:BoundField ItemStyle-Width = "100px" DataField = "HIRED"
       HeaderText = "Hired"/> 
    <asp:BoundField ItemStyle-Width = "100px" DataField = "REQUIREDPERSON"
       HeaderText = "Required"/>   
  <asp:BoundField ItemStyle-Width = "200px" DataField = "RESULT"
       HeaderText = "Status"/>              
     </Columns> 
   <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/Content/css/light/front/next.png" 
        PreviousPageImageUrl="~/Content/css/light/front/previous.png"  FirstPageImageUrl="~/Content/css/light/front/first.png"   />
     
</asp:GridView> 
<asp:HiddenField ID="hfCount" runat="server" Value = "0" /><br />
<asp:Button ID="btnDelete" runat="server" Text="Delete Checked Records"
   OnClientClick = "return ConfirmDelete();" OnClick="btnDelete_Click"
    CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;"
     />
 </td></tr></table>    
</asp:Content>

