<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Candidate.aspx.cs" Inherits="EHR.Recruitment.Candidate" %>
 
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
    function ConfirmDelete()
    {
       var count = document.getElementById("<%=hfCount.ClientID %>").value;
       var gv = document.getElementById("<%=gridCandidates.ClientID%>");
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
  <div class="header">
  <div>
   <asp:Label ID="lblCandidateTitle" Text="Candidate Manager" runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right; margin-top: -10px;" class="ui-buttonset">
 <asp:HyperLink runat="server" NavigateUrl="~/Main/Candidate.aspx"  Enabled="false"   ID="btnViewAll" Text="View All Candidates" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
   <asp:HyperLink  runat="server" NavigateUrl="~/Main/AddCandidate.aspx" ID="btnAssign" Text="Create Profile" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
</div>
</div>
  <asp:Panel ID="pnlViewCandidates" runat="server">
 
            <table style="width: 95%; margin-top: 20px; margin-left: 15px;" class="no-zebra">
                 <thead>
                    <tr>
                        <th colspan="3" align="left" style="padding: 10px; font-weight: normal;">FIND A CANDIDATE</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="3"></th>
                    </tr>
                </tfoot>
            
                <tbody> 
                     <tr>
                         <td class="form-label" >Enter Keyword: </td>
                         <td>
                         <asp:TextBox ID="txtCandidateID" runat="server"  Enabled="true" style="width: 200px;" CssClass="form-field"></asp:TextBox> &nbsp;&nbsp;<asp:Button ID="btnFindCandidate" runat="server"  Text="Find Candidate" CssClass="ui-button ui-button-text-only ui-widget ui-state-default " style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; display: inline; width: 150px;" OnClick="FindCandidate_Click" />

                          </td> 
                     </tr>   
                    
                     </tbody></table> 
  
 <span style="display: block; margin-top: 30px; margin-bottom: 30px;">
<asp:DropDownList runat="server" ID="ddlCandidateFilter" 
  AutoPostBack="true"  OnSelectedIndexChanged="DdlCandidateFilter_SelectedIndexChanged"
  style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; padding: 10px; width: 250px;">
<asp:ListItem Value="">Filter Candidates</asp:ListItem>
<asp:ListItem Value="1">In File</asp:ListItem>
<asp:ListItem Value="2">For Interview</asp:ListItem>
<asp:ListItem Value="3">Interviewed</asp:ListItem> 
<asp:ListItem Value="4">Acceptance Letter Sent</asp:ListItem>
<asp:ListItem Value="5">Accepted</asp:ListItem>
<asp:ListItem Value="6">On Board</asp:ListItem>
<asp:ListItem Value="7">In PS</asp:ListItem>
<asp:ListItem Value="8">All</asp:ListItem>
</asp:DropDownList>
 </span>

<asp:GridView ID="gridCandidates" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"  Width="100%" 
 DataKeyNames = "CandidateID" PageSize = "10" PagerSettings-LastPageImageUrl="~/Content/css/light/front/last.png" PagerSettings-NextPageImageUrl="~/Content/css/light/front/next.png" GridLines="None">
    
     
       <Columns>  
            <asp:TemplateField>
        <HeaderTemplate>
            <asp:CheckBox ID="chkAll" runat="server" Enabled="true" Visible = "false"
             />Delete
        </HeaderTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="chk" runat="server"
             />
        </ItemTemplate> 
    </asp:TemplateField>
    
       <asp:hyperlinkfield DataNavigateUrlFields="CANDIDATEID"
   DataNavigateUrlFormatString="~/Main/ViewCandidate.aspx?candidate={0}" ControlStyle-BorderStyle="none"  
   DataTextField="CANDIDATEID" HeaderText="View"   DataTextFormatString= "<img src='../../Content/css/light/front/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>

       <asp:hyperlinkfield DataNavigateUrlFields="CANDIDATEID"
   DataNavigateUrlFormatString="~/Main/Update.aspx?id={0}" ControlStyle-BorderStyle="none"  
   DataTextField="CANDIDATEID" HeaderText="Update"   DataTextFormatString= "<img src='../../Content/css/light/front/edit.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
      <asp:BoundField ItemStyle-Width = "20%" DataField = "CANDIDATEID" 
       HeaderText = "Candidate ID"/> 
       <asp:BoundField ItemStyle-Width = "20%" DataField = "FULLNAME" 
       HeaderText = "Candidate Name"/> 
       <asp:BoundField ItemStyle-Width = "20%" DataField = "EMAIL" 
       HeaderText = "Email Address"/> 
       <asp:BoundField ItemStyle-Width = "20%" DataField = "HIRINGSTATUS" 
       HeaderText = "Hiring Status"/> 
     </Columns> 
</asp:GridView> 
<br /><br />
<asp:HiddenField ID="hfCount" runat="server" Value = "0" />
 <asp:Button ID="btnDelete" runat="server" Text="Delete Checked Records"
     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;"

   OnClientClick = "return ConfirmDelete();" OnClick="btnDelete_Click" />
        
  
</asp:Panel>
   
    
</asp:Content>
