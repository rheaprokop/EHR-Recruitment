<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Approval_old.aspx.cs" Inherits="EHR.Recruitment.Approval" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Top" ContentPlaceHolderID="TopContent" runat="server">
  
</asp:Content>
<asp:Content ID="Left" ContentPlaceHolderID="LeftContent" runat="server">
 
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
<asp:Panel ID="pnlMainPage" runat="server" CssClass="pnlWelcome" style="margin-top: 5px;">
     <div class="header">
  <div>
   <asp:Label ID="Label1" Text="Approval Manager" runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right;" class="ui-buttonset">
 <asp:HyperLink  runat="server" NavigateUrl="~/Recruitment/AllRequests.aspx" ID="btnViewRequests" Text="View All Requests" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />

<asp:HyperLink  runat="server" NavigateUrl="~/Recruitment/Approval.aspx?action=request" ID="btnFind" Text="Request Status" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
<asp:HyperLink  runat="server" NavigateUrl="~/Recruitment/Approval.aspx?action=assign" ID="btnAssign" Text="Assign PIC" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
<asp:HyperLink  runat="server" NavigateUrl="~/Recruitment/Approval.aspx?action=approver" ID="btnApprover" Text="Assign Approver" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />

<asp:HyperLink runat="server" NavigateUrl="~/Recruitment/Approval.aspx?action=view"   ID="btnViewAll" Text="View Process" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />

<!--<asp:HyperLink  runat="server" NavigateUrl="~/Recruitment/Approval.aspx?action=add"  ID="btnAdd" Text="Add Approver" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />-->
 </div>
         
   </div>
 </asp:Panel>  

<asp:UpdatePanel runat="server" ID="pnlCreateApprovalFlow">
<ContentTemplate>
<table style="width: 100%;">
<tr><td style="align: left;">
            <table style="width: 85%; margin-top: 20px;" class="zebra">
                   <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">ADD APPROVER</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
                <tbody> 
                    <!--Assign PIC or detect from form-->
                    <tr>
                    <td class="form-label" style="padding-left: 15px;">Assignment</td>
                    <td class="form-selected" colspan="3">
                    <asp:DropDownList runat="server" ID="ddlAssignment" style="width: 200px;" 
                     OnSelectedIndexChanged="DdlAssignment_SelectedIndexChanged" AutoPostBack="true">
                           <asp:ListItem Value="">--Select One--</asp:ListItem>
                           <asp:ListItem Value="1">Assign A PIC</asp:ListItem>
                           
                           <asp:ListItem Value="2">Base on Request Form</asp:ListItem>
                     </asp:DropDownList> 
                     <asp:RequiredFieldValidator runat="server" ID="reqDdlAssignment" ControlToValidate="ddlAssignment"
                        ErrorMessage="Required" style="display: inline;"></asp:RequiredFieldValidator>
                    </td>                   
                   </tr>
                <asp:Panel runat="server" ID="pnlPICAssignAPic">
                        <tr>
                        <td class="form-label" style="padding-left: 15px;">Department ID :</td>
                        <td class="form-selected">
                        <asp:DropDownList runat="server" ID="ddlDepartment" style="width: 150px;"
                         OnSelectedIndexChanged="DlDepartment_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td class="form-label" style="padding-left: -15px;">Assign A Person:</td>
                        <td class="form-field">
                        
                        <asp:DropDownList runat="server" ID="ddlAssignAPIC" style="width: 200px;"></asp:DropDownList>    
                       <asp:RequiredFieldValidator runat="server" ID="reqddlAssignAPIC" ControlToValidate="ddlAssignAPIC"
                        ErrorMessage="Required" style="display: inline;"></asp:RequiredFieldValidator>

                        </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlBaseOnRequest">
                        <tr>
                        <td class="form-label" style="padding-left: 15px;">Request Type</td>
                        <td class="form-selected">
                        <asp:DropDownList runat="server" ID="ddlRequestFrom" style="width: 200px;">
                        <asp:ListItem Value="">--- Select One ---</asp:ListItem>
                        <asp:ListItem Value="0">Requestor's Department</asp:ListItem>
                        <asp:ListItem Value="1">Requestor's Plant</asp:ListItem>
                        <asp:ListItem Value="2">Request's Department</asp:ListItem>
                        <asp:ListItem Value="3">Request's Plant</asp:ListItem>
                        
                        </asp:DropDownList>
                        </td>
                        <td class="form-label" style="padding-left: -15px;">&nbsp;</td>
                        <td class="form-field"> &nbsp;
                        </td>
                        </tr>
                    </asp:Panel>
                        <tr>
                        <td class="form-label" style="padding-left: 15px;">Flow Name:</td>
                        <td class="form-selected">
                         <asp:TextBox runat="server" ID="txtFlowName"></asp:TextBox>
                          <asp:RequiredFieldValidator runat="server" ID="reqtxtFlowName" ControlToValidate="txtFlowName"
                        ErrorMessage="Required" style="display: inline;"></asp:RequiredFieldValidator>
                        </td>
                        <td class="form-label" style="padding-left: -15px;">Is Part of Agency Flow:</td>
                        <td class="form-field">
                        
                        <asp:DropDownList runat="server" ID="ddlIsAgency" style="width: 200px;">
                        <asp:ListItem Value="">--- Select One ---</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>    
                        <asp:RequiredFieldValidator runat="server" ID="reqddlIsAgency" ControlToValidate="ddlIsAgency"
                        ErrorMessage="Required" style="display: inline;"></asp:RequiredFieldValidator>
                        </td>
                        </tr>    
                        
                        <tr><td colspan="4">
                        <div  class="ui-buttonset"  style="width: 800px;">
                          <asp:Button ID="btnCancel" runat="server" OnClick="IsCancel" Text="Cancel" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; padding-right: 20px; padding-left: 20px;padding-bottom: 5px; " />
 	                        <asp:Button ID="btnSaveAnother" runat="server" OnClick="IsSaveAnother" Text="Save & Add Another" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; padding-right: 20px; padding-left: 20px; padding-bottom: 5px;" />
	                        <asp:Button ID="btnSave" runat="server" OnClick="IsSave" Text="Save" CssClass="ui-button ui-button-text-only ui-widget ui-state-default " style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px; padding-bottom: 5px;" />
                        </div>
                        </td></tr>            
                </tbody>
                </table>

</td></tr>
</table>     
</ContentTemplate></asp:UpdatePanel>
<asp:Panel runat="server" ID="pnlViewApprovalFlow" style="margin-left: 100px;">
<br /><br />
<asp:UpdatePanel runat="server" ID="pnlApprovalFlow">
<ContentTemplate> 

<span class="table-title">NON-AGENCY APPROVAL FLOW</span>
<asp:GridView ID="gridApprovalIsNotAgency" CellPadding="0" CellSpacing="0" GridLines="None"  
      runat="server" CssClass="grid-zebra" 
      OnRowDataBound="GridApprovalIsNotAgency_RowDataBound"   >
    <Columns>
        
    </Columns>
    
    </asp:GridView>
</ContentTemplate></asp:UpdatePanel>  
    
<br /><br /> 
<asp:UpdatePanel runat="server" ID="pnlApprovalFlowTwo">
<ContentTemplate>    
<span class="table-title">AGENCY APPROVAL FLOW</span>
<asp:GridView ID="gridApprovalIsAgency" CellPadding="0" CellSpacing="0" GridLines="None"  
      runat="server" CssClass="grid-zebra"    
      OnRowDataBound="GridApprovalIsAgency_RowDataBound">
    <Columns>
     </Columns>
    </asp:GridView>  
  </ContentTemplate></asp:UpdatePanel>   


</asp:Panel>

<!-- FOR FIND STATUS OF REQUEST --> 

<asp:UpdatePanel runat="server" ID="pnlRequestStatus">
<ContentTemplate>
<table style="width: 100%;"><tr><td align="center">
            <table style="margin: 0 auto; width: 85%; margin-top: 20px; margin-left: 15px;" class="zebra">
                            <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">REQUEST STATUS</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
            
                <tbody> 
                     <tr>
                         <td class="form-label" >Enter Request Form ID: </td>
                         <td>
                         <asp:TextBox ID="txtRequestFrmID" runat="server"  Enabled="true" style="width: 200px;"></asp:TextBox> <asp:Button ID="btnRequestFrmID" runat="server"  OnClick="FindFormForRequest" Text="Find My Form" CssClass="ui-button ui-button-text-only ui-widget ui-state-default " style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; padding-right: 20px; padding-left: 20px; display: inline;" />

                          </td>
                         <td colspan="2">
                         
                         </td>
                     </tr>  
                     
                     </tbody></table>
                     
</td></tr>
 <tr><td align="left">
  <table style="width: 95%; margin-top: -10px;" border="0" >
  <tr><td>
 
    
     <asp:GridView ID="gridRequestStatus" CellPadding="0" CellSpacing="0" GridLines="None"   OnPageIndexChanging="GridRequestStatus_PageIndexChanging" 
     AllowPaging="True"  runat="server" CssClass="grid-zebra" style="width: 1100px;" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
       <Columns>  
        
             <asp:hyperlinkfield DataNavigateUrlFields="RequestID" ControlStyle-BorderStyle="none"  
   DataNavigateUrlFormatString="~/Recruitment/Approval.aspx?action=request&status=show&RequestID={0}"  
   DataTextField="RequestID" HeaderText=""  DataTextFormatString= "<img src='../App_Themes/front/view.png' alt='View Record'  border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
    </Columns>
             
    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/App_Themes/front/next.png" 
        PreviousPageImageUrl="~/App_Themes/front/previous.png"  FirstPageImageUrl="~/App_Themes/front/first.png"   />
    
</asp:GridView>

<asp:GridView ID="gridShowStatus" CellPadding="0" CellSpacing="0" GridLines="None"  
 runat="server" CssClass="grid-zebra" style="width: 800px;" AutoGenerateColumns="True" 
 OnRowDataBound="GridShowStatus_RowDataBound" >
  <Columns>  
   </Columns>
   <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="~/App_Themes/front/next.png" 
        PreviousPageImageUrl="~/App_Themes/front/previous.png"  FirstPageImageUrl="~/App_Themes/front/first.png"   />
</asp:GridView>
    </td></tr></table>
 </td></tr>
</table>      

           
</ContentTemplate>
</asp:UpdatePanel>


<!-- FOR ASSIGNING PIC --> 


 
     
     
<asp:UpdatePanel runat="server" ID="pnlAssignPIC">
<ContentTemplate>
<table style="width: 100%;">
<tr><td style="align: left;">
 
	        <!--start tab 1-->
	        <table align="left" style="width: 70%; border: 0;"><tr><td style="align: left;">
                <asp:Label runat="server" ID="lblMessage" CssClass="errorMsgOK"></asp:Label>
                             <table style="margin: 0 auto; width:  100%; margin-top: 20px; margin-left: 15px;" class="zebra">
 
                  <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">ASSIGN PIC</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
                                <tbody> 
                  
                                     <tr>
                                        <td class="form-label" style="padding-left: 15px;">HR Recruiter</td>
                                        <td class="form-selected">
                                        <asp:DropDownList runat="server" ID="ddlHRRecruiter" style="width: 200px;" > 
                                       
                                        </asp:DropDownList>
                                        </td>
                                        <td class="form-label" style="padding-left: -15px;">&nbsp;</td>
                                         
                                        </tr>
                                     <tr>
                                        <td class="form-label" style="padding-left: 15px;">HR Trainer</td>
                                        <td class="form-selected">
                                        <asp:DropDownList runat="server" ID="ddlHRTrainer" style="width: 200px;" > 
                                        
                                        </asp:DropDownList>
                                        </td>
                                        <td class="form-label" style="padding-left: -15px;">&nbsp;</td>
                                         
                                        </tr>        
                                     <tr>
                                        <td class="form-label" style="padding-left: 15px;">PS Encoder</td>
                                        <td class="form-selected">
                                        <asp:DropDownList runat="server" ID="ddlPSEncoder" style="width: 200px;" > 
                                         
                                        </asp:DropDownList>
                                        </td>
                                        <td class="form-label" style="padding-left: -15px;">&nbsp;</td>
                                        
                                        </tr>                                         
                                     </tbody></table>
                </td></tr>
                <tr><td style="float: right">
                <br />
                <asp:Button ID="btnSavePIC" runat="server" Text="Save PIC" OnClick="IsSavePIC"  
                   CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
                   style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; margin-left: 10px;" />
                </td></tr>
                </table> 
                </td></tr> 
                </table>   
	      
     
    </td></tr></table>
</ContentTemplate>
</asp:UpdatePanel>

<!--PANELS DETAIL-->
<asp:Panel runat="server" ID="pnlViewManager">  
</asp:Panel>

 
<asp:UpdatePanel runat="server" ID="pnlApprover">
<ContentTemplate>  
 <table style="width: 70%; margin-top: 20px;"><tr><td style="align: center;">
                <asp:Label runat="server" ID="Label2" CssClass="errorMsgOK"></asp:Label>
                             <table style="margin: 0 auto; width:  100%; margin-top: 20px; margin-left: 15px;" class="zebra">
 
                             
                  <thead>
                    <tr>
                        <th colspan="3" align="left" style="padding: 10px; font-weight: normal;">ASSIGN APPROVER</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="3"></th>
                    </tr>
                </tfoot>
                                <tbody> 
                  
                                     <tr>
                                        <td class="form-label" style="padding-left: 15px; width: 150px;">HR Reviewer</td>
                                        <td class="form-selected" style="width: 150px;">
                                        <asp:DropDownList runat="server" ID="ddlHRReviewer" style="width: 200px;" > 
                                       
                                        </asp:DropDownList>
                                        </td>
                                        <td class="form-label" style="padding-left: -15px;">&nbsp;</td>
                                         
                                        </tr>
                                     <tr>
                                        <td class="form-label" style="padding-left: 15px;">HR Manager</td>
                                        <td class="form-selected">
                                        <asp:DropDownList runat="server" ID="ddlHRManager" style="width: 200px;" > 
                                        
                                        </asp:DropDownList>
                                        </td>
                                        <td class="form-label" style="padding-left: -15px;">&nbsp;</td>
                                         
                                        </tr>    
                                        <asp:UpdatePanel runat="server" ID="pnlGenManager"><ContentTemplate>   
                                    <tr>
                                        <td class="form-label" style="padding-left: 15px;">WCZ Gen Manager</td>
                                        <td class="form-selected">
                                         <asp:TextBox runat="server" ID="txtGenManager" style="width: 150px"></asp:TextBox>
                                        </td>
                                        <td class="form-label" style="padding-left: -15px;"> 
                                        <asp:Button ID="btnGetManangerName" runat="server" OnClick="BtnGetManagerName_Click" Text="Get Name"  CausesValidation="false"
                        CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
                        style="font-family: BebasNeue; font-size: 10px; letter-spacing: 1px; width: 60px; display: inline;" 
                        />
                          <asp:Label runat="server" ID="lblName" style=" display: inline;"></asp:Label></td>
                                        </tr>      
                                        </ContentTemplate></asp:UpdatePanel>                              
                                     </tbody></table>
                </td></tr>
                <tr><td style="float: right">
                <br />
                <asp:Button ID="Button1" runat="server" Text="Save Approver" OnClick="IsSaveApprover_Click"  
                   CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
                   style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; margin-left: 10px;" />
                </td></tr>
                </table> 
                </td></tr> 
                </table>   	     
</ContentTemplate></asp:UpdatePanel>
</asp:Content>
