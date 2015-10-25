<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="HireCandidate.aspx.cs" Inherits="EHR.Main.HireCandidate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server"> 

<div class="header">
  <table><thead>
  <tr>
       <th colspan="4" align="left">
   <asp:Label ID="lblCandidateTitle" Text="CANDIDATE PROFILE"  
   runat="server" CssClass="logotext"></asp:Label>
   
   </th></tr>
   </thead></table> 
    <asp:Panel runat="server" ID="pnlCandidateButton">
    <div style="float: right; margin-top: -60px;" class="ui-buttonset">
     <asp:HyperLink runat="server" NavigateUrl="~/Main/Candidate.aspx?action=view"   ID="btnViewAll" Text="View All Candidates" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
     <asp:HyperLink  runat="server" NavigateUrl="~/Main/AddCandidate.aspx" ID="btnAssign" Text="Create Profile" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
     </div> 
     </asp:Panel>
</div>
 <!--INSERT CANDIDATE PROFILE HERE -->
 <asp:Panel runat="server" ID="pnlButtons" style="margin-top: 10px;">
       <table style="width: 100%; " > 
        <tr>
        
        <td align="right">
        
        <asp:HiddenField runat="server" ID="hiddenCandID" />
        <div class="ui-buttonset">
                
 
                    <asp:Button ID="btnInvite" runat="server"  Text="INVITE FOR INTERVIEW"  PostBackUrl="~/Main/InviteCandidate.aspx" CausesValidation="false"
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />
            
                    <asp:Button ID="btnAddDetails" runat="server"  Text="ADD INTERVIEW DETAILS" PostBackUrl="~/Main/InterviewResult.aspx"  CausesValidation="false"
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />
                                     
                    <asp:Button ID="btnViewPastInterviews" runat="server"  Text="PAST INTERVIEWS"  PostBackUrl="~/Main/InterviewDetails.aspx"  CausesValidation="false"
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />
                     
                     <asp:Button ID="btnSendOffer" runat="server"  Text="SEND OFFER LETTER" PostBackUrl="~/Main/HireCandidate.aspx"   CausesValidation="false"
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />  
                    
                     <asp:Button ID="btnBackToProfile" runat="server"  Text="Back To Candidate Profile" PostBackUrl="~/Main/ViewCandidate.aspx" CausesValidation="false"
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />

    </div>
    </td></tr> 
    </table>
    </asp:Panel>
                        
            
<table style="width: "80%;">
<tbody> 
<tr><td>
     
            <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">CANDIDATE ACCEPTANCE INFO</th>
                    </tr>
                </thead> 
             
                     <tr>
                         
                        <td class="form-label" style="width: 200px;" >Candidate Name: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:TextBox ID="txtCandidateName" runat="server"  CssClass="form-field" Enabled="false" style="width: 200px;"></asp:TextBox> 
                         </td>
                          <td class="form-label" style="width: 200px;" > For Request ID: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:DropDownList runat="server" ID="ddlForRequest"></asp:DropDownList>
                         <asp:RequiredFieldValidator  runat="server" ID="reqddlForRequest" ControlToValidate="ddlForRequest"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                         </td> 
                     </tr>  
                     <tr>
                     <td class="form-label" style="width: 200px;" >On Board Date: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:TextBox  ID="txtOnBoardDate" runat="server" CssClass="form-field"  Enabled="false" style="width: 200px;"></asp:TextBox> 
                         <asp:Calendar SelectionMode="DayWeekMonth"
                                      id="calOnBoardDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server" OnSelectionChanged="CalendarOnBoardDate_Selection_Change"
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest"
                                      BorderWidth="2px" 
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset" CellPadding="1" CssClass="calendar">
                                      
                                
                              </asp:Calendar>
                                     <asp:ImageButton ID="btnOnBoardDate" runat="server"  OnClick="OnBoardDate_Click"
                                     ImageUrl="~/Content/css/light/images/calendar.gif" CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                        <asp:RequiredFieldValidator  runat="server" ID="reqtxtOnBoardDate" ControlToValidate="txtOnBoardDate"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                        
                         </td>
                         <td class="form-label" style="width: 200px;" > For Department: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:DropDownList runat="server" ID="ddlForDepartment" style="width: 100px;">
                         </asp:DropDownList>
                         </td> 
                        
                     </tr>   
                     <tr>
                     <td>
                      Time:  <span style="color: red; display: inline;">*</span>
                      </td>
                     <td><asp:DropDownList runat="server" ID="ddlToTime"
                          AutoPostBack="true"
                          style="width: 100px;">
                                 <asp:ListItem Value="">Select</asp:ListItem> 
                                  <asp:ListItem Value="06:00">06:00</asp:ListItem>
                                 <asp:ListItem Value="06:30">06:30</asp:ListItem>
                                 <asp:ListItem Value="07:00">07:00</asp:ListItem>
                                 <asp:ListItem Value="07:30">07:30</asp:ListItem>
                                 <asp:ListItem Value="08:00">08:00</asp:ListItem>
                                 <asp:ListItem Value="08:30">08:30</asp:ListItem>
                                 <asp:ListItem Value="09:00">09:00</asp:ListItem>
                                  <asp:ListItem Value="09:30">09:30</asp:ListItem>
                                 <asp:ListItem Value="10:00">10:00</asp:ListItem>
                                 <asp:ListItem Value="10:30">10:30</asp:ListItem>
                                 <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                 <asp:ListItem Value="11:30">11:30</asp:ListItem>
                                 <asp:ListItem Value="12:00">12:00</asp:ListItem>
                                 <asp:ListItem Value="12:30">12:30</asp:ListItem>
                                 <asp:ListItem Value="13:00">13:00</asp:ListItem>
                                 <asp:ListItem Value="13:30">13:30</asp:ListItem>
                                 <asp:ListItem Value="14:00">14:00</asp:ListItem>
                                 <asp:ListItem Value="14:30">14:30</asp:ListItem>
                                 <asp:ListItem Value="15:00">15:00</asp:ListItem>
                                 <asp:ListItem Value="15:30">15:30</asp:ListItem>
                                 <asp:ListItem Value="16:00">16:00</asp:ListItem>
                                 <asp:ListItem Value="16:30">16:30</asp:ListItem>
                                 <asp:ListItem Value="17:00">17:00</asp:ListItem> 
                                 </asp:DropDownList>
                                 <asp:RequiredFieldValidator  runat="server" ID="reqddlToTime" ControlToValidate="ddlToTime"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                     </td>
                      <td>Address: <span style="color: red; display: inline;">*</span></td><td> <asp:DropDownList runat="server" ID="ddlWorkPlace"  >
                                 <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                 <asp:ListItem>Budova D3, Vlastimila Pecha 1296/10, 627 00 Brno Slatina</asp:ListItem> 
                                 <asp:ListItem>K Letisti 1792/1 66451 Slapanice, Brno-Venkov</asp:ListItem>
                                 </asp:DropDownList>
                          <asp:RequiredFieldValidator  runat="server" ID="reqddlWorkPlace" ControlToValidate="ddlWorkPlace"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                  </td> 
                     </tr> 
                      
                    </table> 
</td></tr> 
<tr><td>
            <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">JOB & SALARY INFORMATION</th>
                    </tr>
                </thead> 
                                  <tr>
                         <td class="form-label" style="width: 200px;" >Job Position: <span style="color: red; display: inline;">*</span> </td>
                         <td>
                         <asp:DropDownList runat="server" ID="ddlJobPosition"></asp:DropDownList>
                         
                         <asp:RequiredFieldValidator  runat="server" ID="reqddlJobPosition" ControlToValidate="ddlJobPosition" 
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                       
                         </td>
                        <td class="form-label" style="width: 200px;" > </td>
                         <td>
                          
                         </td>
                     </tr>  
                     <tr>
                         <td class="form-label" style="width: 200px;" >Offer Salary ( Kč ): <span style="color: red; display: inline;">*</span> </td>
                         <td>
                         <asp:TextBox ID="txtSalary" runat="server" CssClass="form-field"   style="width: 200px;"></asp:TextBox> 
                          <asp:RequiredFieldValidator  runat="server" ID="reqtxtSalary" ControlToValidate="txtSalary"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                         
                         </td>
                        <td class="form-label" style="width: 200px;" >Salary After Probation  ( Kč ): <span style="color: red; display: inline;">*</span> </td>
                         <td>
                         <asp:TextBox ID="txtSalaryAfter" runat="server"  CssClass="form-field"  style="width: 200px;"></asp:TextBox> 
                         <asp:RequiredFieldValidator  runat="server" ID="reqtxtSalaryAfter" ControlToValidate="txtSalaryAfter"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                         </td>
                     </tr>  
                  <tr><td colspan="4">
                                          <asp:CompareValidator runat="server" id="cmpNumbers" controltovalidate="txtSalaryAfter" controltocompare="txtSalary" operator="GreaterThanEqual" type="Integer" errormessage="The Offer Salary should be smaller than the Salary After Probation!"  cssClass="reqErrorMsg"/>

                     </td></tr>                
                    </table> 
                    
</td></tr> 

<tr><td align="right">
<br />

    <asp:HyperLink runat="server" ID="linkToOutlook" 
     CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
     style="float:left; font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; padding-right: 20px; padding-top: 10px; padding-left: 20px; padding-bottom: 10px; display: inline;"
     Text="USE OUTLOOK TO SEND EMAIL"></asp:HyperLink>
       <asp:Button ID="btnSendLetter" runat="server"  Text="SEND EMAIL TO CANDIDATE"  OnClick="SendEmailToCandidate_Click"   
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default " style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; padding-right: 20px; padding-left: 20px; padding-bottom: 10px; display: inline;" />
<br />

</td></tr>
</tbody>
</table> 
</asp:Content>
