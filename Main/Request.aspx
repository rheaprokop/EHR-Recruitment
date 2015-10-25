<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="EHR.Recruitment.Request" EnableEventValidation ="false"%>
<%@ Register TagPrefix="req"  Src="ViewRequest.ascx" TagName="Request" %>
<%@ Register TagPrefix="req" Src="UpdateRequest.ascx" TagName="EditRequest" %>
<%@ MasterType VirtualPath="~/Views/Shared/Site.Master" %>
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
  <div>
   <asp:Label ID="lblCandidateTitle" Text="Manpower Requisition Form " runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right; margin-top: -10px;" class="ui-buttonset">
<asp:HyperLink runat="server" NavigateUrl="~/Views/Main/Main.aspx"   ID="btnMainPage" Text="Dashboard" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
<asp:HyperLink runat="server" NavigateUrl="~/Main/AllRequests.aspx"   ID="btnViewAllRequest" Text="View All Requests" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
<asp:HyperLink  runat="server" NavigateUrl="~/Main/Request.aspx?form=create" Enabled="false" ID="btnCreateRequest" Text="Create Request" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
<asp:HyperLink  runat="server" NavigateUrl="~/Main/MyRequests.aspx" ID="btnMyRequests" Text="My Requests" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />

</div>
</div> 
 </asp:Panel>   
  

<asp:Panel runat="server" ID="pnlCreate" style="margin-left: 20px; margin-right: 10px; margin-top: 10px;">

   <table style="width: 100%; "><tr><td>
            <table style="width: 100%; margin-top: 20px;" class="no-zebra">
                            <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">REQUESTOR INFO</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
            
                <tbody> 
                     <tr>
                         <td >Requestor: </td>
                         <td><asp:TextBox ID="txtRequestorName" runat="server"  Enabled="false" class="form-field"></asp:TextBox></td>
                         <td>Department Code: </td>
                         <td><asp:TextBox ID="txtRequestorDeptID" runat="server"  Enabled="false" class="form-field" ></asp:TextBox></td>                         
                    </tr>  
                    </tbody>
           </table>
     </td></tr>
     <tr><td align="right">   
                <table cellpadding="5" cellspacing="5" style="width:100%; border: solid 0px #cccccc;">
                <tr><td style="float: left;">
                    <asp:Panel runat="server" ID="pnlError" style="border: solid 1px #c0221f; background: #ffbde5; width: 260px; padding: 10px; margin: 0 auto; margin-bottom: 10px;" >
                    <asp:Label runat="server" ID="lblError" style="font-size: 11px;"></asp:Label><br />
                    </asp:Panel>
               </td><td style="float: right;">
                    <asp:DropDownList ID="ddlIsAgencySelected" runat="server"  
                  CssClass="dropdown"   style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px; padding: 10px; width: 300px;"
                     AutoPostBack="true" OnSelectedIndexChanged="ddlIsAgencySelected_SelectIndexChanged" >
                        <asp:ListItem Value="" Selected=True>Select Request Type</asp:ListItem>
                        <asp:ListItem Value="0">Non-Agency / Site Support</asp:ListItem>
                        <asp:ListItem Value="1">Agency Request</asp:ListItem>
                    </asp:DropDownList>
                    </td></tr></table>
       </td></tr>
</table>


<asp:Panel ID="PnlMainForm" runat="server" >   

<div id="tabs">
	<div id="tabs-1">
	<asp:UpdatePanel ID="pnlRequestFor" runat="server">
    <ContentTemplate>
 <table style="width: 100%; margin-top: 20px;" class="no-zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">REQUEST FOR</th>
                    </tr>
                </thead>
                
                <tbody>
                    <tr>       
                          <td class="form-label">Department: <span style="color: red; display: inline;">*</span></td>
                         <td ><asp:DropDownList ID="ddlDepartment" runat="server"  class="form-field"
                           OnSelectedIndexChanged="DdlDepartment_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlDepartment" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                          </td> 
                          
                         <td class="form-label">Plant: <span style="color: red; display: inline;">*</span></td>
                         <td> 
                            <asp:DropDownList Enabled="false" runat="server" ID="ddlPlant" class="form-field">
                            </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="ddlPlantReq" ControlToValidate="ddlPlant" 
                         ErrorMessage="Select A Plant"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>&nbsp;
                         
                         </td>                 
                    </tr>
                     <tr>
                        <td>Job Title:</td>                       
                        <td><asp:DropDownList runat="server" ID="ddlJobTitle"></asp:DropDownList></td>
                        <td colspan="2">&nbsp;*Job Title depends on Plant and Department Selected</td>
                    </tr>  
                     <tr> 
                         <td class="form-label">
                         <table width="100%" style="margin-left: -10px; border: 0px;" class="zebra">
                         <tr><td class="form-label">
                         <asp:Label ID="lblIntendedBoardDate" Text="Intended Board Date" runat="server" style="color: #111111" ></asp:Label>
                         <asp:Label ID="lblLengthEmplDate" Text="Length Of Employment Date" runat="server" style="color: #111111"></asp:Label>
                         </td>
                         <td> 
                         <asp:Label ID="lblIntendedBoardDateAskterisk" Text="*" runat="server" style="color: red;"></asp:Label>
                         <asp:Label ID="lblLengthEmplDateAskterisk" Text="*" runat="server" style="color: red; display: inline; width: 5px;"></asp:Label> 
                         </td></tr></table>
                         </td>
                         <td>
                     
                         <asp:TextBox runat="server" id="txtOnBoardDate" Enabled="false"  class="form-field" style="margin-right: 10px; width: 100px;"/>
                         <asp:Calendar SelectionMode="DayWeekMonth"
                                      id="calOnBoardDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server"
                                      DayHeaderStyle-Font-Bold="false" 
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest"

                                      BorderWidth="1px"
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset" CellPadding="1" CssClass="calendar"
                                      OnSelectionChanged="CalOnBoardDate_Selection_Change">                                
                              </asp:Calendar>
                             
                                     <asp:ImageButton ID="btnCalOnBoardDate" runat="server" OnClick="ViewOnBoardCal_OnClick" 
                                     ImageUrl="~/Content/css/light/images/calendar.gif"  CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                         <asp:RequiredFieldValidator runat="server" ID="reqtxtOnBoardDate" ControlToValidate="txtOnBoardDate"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                         <asp:TextBox runat="server"   class="form-field" ID="txtLengthDate" style="margin-left: -50px;"></asp:TextBox>
                           <asp:RequiredFieldValidator  runat="server" ID="reqLengthDate" ControlToValidate="txtLengthDate"
                          ErrorMessage="Required Length!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                        </td>      
                        <td class="form-label">No of Person(s): <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:Textbox  class="form-field" ID="txtNoOfPerson"  runat="server" style="width: 30px; margin-right: 10px;" />
                         <asp:CompareValidator  runat="server" ID="EnterNoOfPersons" ControlToValidate="txtNoOfPerson" 
            ErrorMessage="Enter A Number" Type="Integer" Operator="DataTypeCheck" cssClass="reqErrorMsg" style="display:inline;"></asp:CompareValidator>
           <asp:RequiredFieldValidator ID="reqtxtNoOfPerson"  ControlToValidate="txtNoOfPerson" ErrorMessage="Required!" runat="server" cssClass="reqErrorMsg"  style="display:inline;">
                         </asp:RequiredFieldValidator></td>                  
                    </tr>                                                        
              </tbody>
            </table>
        
            </ContentTemplate>
            </asp:UpdatePanel>    
        <!--END OF "REQUEST FOR TABLE"--> 
	</div>
	<div id="tabs-2"  name="tabs-2">
	<asp:UpdatePanel ID="pnlRequisitionType" runat="server">
    <ContentTemplate>
        
               <!--REQUISITION TYPE-->
                <table style="width: 100%; margin-top: 20px;" class="no-zebra">
                                <thead>
                                    <tr>
                                        <th colspan="4" align="left" style="padding: 10px;">REQUISITION TYPE</th>
                                    </tr>
                                </thead>
                                
                                <tbody>
                                    <tr>
                                         <td class="form-label">Type: <span style="color: red; display: inline;">*</span></td>
                                         <td><asp:DropDownList  runat="server" ID="ddlRequestType"
                                              AutoPostBack="true"   OnSelectedIndexChanged="ddlRequestType_SelectIndexChanged">
                                             <asp:ListItem Value="">Select Type</asp:ListItem>
                                             <asp:ListItem Value="Increase">Increase</asp:ListItem>
                                              <asp:ListItem Value="Replacement">Replacement</asp:ListItem>
                                             </asp:DropDownList>
                                            </td>
                                         <td class="form-label">
                                         <asp:Label ID="lblIncrease" Text="Increase *" runat="server" Visible="false" style="font-weight: bold; color: #111111;"></asp:Label></td>
                                         <td>
                                         <asp:DropDownList runat="server" ID="ddlIfIncrease"  Visible="false">
                                             <asp:ListItem Value="">Select Type</asp:ListItem>
                                             <asp:ListItem Value="Under Budget">Under Budget</asp:ListItem>
                                              <asp:ListItem Value="Over Budget">Over Budget</asp:ListItem>
                                             </asp:DropDownList> 
                                                           </td>                         
                                    </tr> 
                                    
                                   <tr>
                                         <td class="form-label">
                                         <asp:Label Text="Replacement" ID="lblReplacement" runat="server" Visible="false" 
                                         style="font-weight: bold; color: #111111;"></asp:Label></td>
                                         <td><asp:DropDownList runat="server"   ID="ddlReplacement" Visible="false">
                                             <asp:ListItem Value="">Select Type</asp:ListItem>
                                             <asp:ListItem Value="Successor">Successor</asp:ListItem>
                                              <asp:ListItem Value="Transfer">Transfer</asp:ListItem>
                                             </asp:DropDownList>
                                              </td>
                                         <td class="form-label">
                                         <asp:Label Text="Replacement To:" ID="lblReplacementTo" Visible="false" runat="server" style="font-weight: bold; color: #111111;"></asp:Label> </td>
                                         <td>
                                          <asp:TextBox ID="txtReplacementTo" runat="server" class="form-field"  Visible="false"></asp:TextBox>
                                         </td>                         
                                    </tr>          
                                                                                                  
                              </tbody>
                            </table>
                  <!--END OF "REQUISITION TYPE"-->
         </ContentTemplate>
            </asp:UpdatePanel>       
	</div>
	<div id="tabs-3"  name="tabs-3">
	<asp:UpdatePanel ID="pnlEmployeeType" runat="server">
    <ContentTemplate>
	<!--EMPLOYEE  TYPE-->
   <table style="width: 100%; margin-top: 20px;" class="no-zebra">
                        <thead>
                            <tr>
                                <th colspan="4" align="left" style="padding: 10px;">EMPLOYEE TYPE</th>
                            </tr>
                        </thead>
                        
                        <tbody>
                            <tr>
                                 <td class="form-label">Employee Category: <span style="color: red; display: inline;">*</span></td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlEmployeeCategory"  >
                                 <asp:ListItem Value="" Selected="true">Select Category</asp:ListItem>
                                 <asp:ListItem Value="IDL">IDL</asp:ListItem>
                                 <asp:ListItem Value="DL">DL</asp:ListItem></asp:DropDownList>
                               <asp:RequiredFieldValidator   runat="server" ID="reqddlEmployeeCategory" ControlToValidate="ddlEmployeeCategory"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                                 </td>
                                 <td class="form-label">Contract Type: <span style="color: red; display: inline;">*</span></td>
                                 <td>
                                     <asp:DropDownList runat="server" ID="ddlContractType"  >
                                     <asp:ListItem Value="" Selected="True">Select Type</asp:ListItem>
                                     <asp:ListItem Value="Part Time">Part Time</asp:ListItem>
                                     <asp:ListItem Value="Full Time">Full Time</asp:ListItem>
                                     <asp:ListItem Value="Contractual">Contractual</asp:ListItem> 
                                     </asp:DropDownList>
                                      <asp:RequiredFieldValidator   runat="server" ID="reqddlContractType" ControlToValidate="ddlContractType"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                                 </td>                         
                            </tr>
                                            
 
                              <tr>
                              <td class="form-label">Maximum Salary ( Kč ): </td>
                                 <td>
                                 <asp:Textbox   ID="txtMaximumSalary"  class="form-field" runat="server" />
                                  <asp:CompareValidator   runat="server" ID="reqMaximumSalary" ControlToValidate="txtMaximumSalary" 
            ErrorMessage="Enter A Number" Type="Currency" Operator="DataTypeCheck" cssClass="reqErrorMsg" style="display:inline;"></asp:CompareValidator>
                                 
                                </td>
                              <td class="form-label">&nbsp;</td>
                              <td>&nbsp;</td> 
                            </tr>  

                              <tr> <td style="height: 150px">Note Additional Manpower: <span style="color: red; display: inline;">*</span> </td>
                                 <td colspan="3">
                                 <asp:Textbox ID="txtNoteManpower"   class="form-field"  runat="server"  CssClass="form-textarea" TextMode="MultiLine" />
                                 <asp:RequiredFieldValidator   runat="server" ID="reqtxtNoteManpower" ControlToValidate="txtNoteManpower"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                                 </td> 
                                             
                            </tr>                                                                                                                                        
                      </tbody>
                    </table>
                    
                 <table style="width: 100%; margin-top: 20px;" class="no-zebra">
                        <thead>
                            <tr>
                                <th colspan="4" align="left" style="padding: 10px;">OTHER WORK INFORMATION</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th colspan="4"></th>
                            </tr>
                        </tfoot>
                        
                        <tbody>
                            <tr>
                            <td>Work Place: <span style="color: red; display: inline;">*</span></td>                    
                            <td><asp:DropDownList runat="server" ID="ddlWorkPlace"  >
                                 <asp:ListItem Value="" Selected="true"></asp:ListItem>
                                 <asp:ListItem>Budova D3, Vlastimila Pecha 1296/10, 627 00 Brno Slatina</asp:ListItem> 
                                 <asp:ListItem>K Letisti 1792/1 66451 Slapanice, Brno-Venkov</asp:ListItem>
                                 </asp:DropDownList> 
                          <asp:RequiredFieldValidator   runat="server" ID="reqddlWorkPlace" ControlToValidate="ddlWorkPlace"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                                 </td>
                            <td>Work Time : <span style="color: red; display: inline;">*</span></td>
                            <td><asp:DropDownList runat="server" ID="ddlWorkTime"  >
                                 <asp:ListItem Value="">Select One</asp:ListItem>
                                 <asp:ListItem Value="Fixed">Fixed</asp:ListItem>
                                 <asp:ListItem Value="Flexible">Flexible</asp:ListItem>
                                 </asp:DropDownList>
                                  <asp:RequiredFieldValidator runat="server"   ID="reqddlWorkTime" ControlToValidate="ddlWorkTime"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                       
                             </td>
                            </tr>
                          <tr style="height: 50px;">
                          
                           <td>Work Days :</td>
                            <td colspan="3"><asp:CheckBoxList ID="chkDays" runat="server" 
                               RepeatDirection="Horizontal" BorderStyle="None" style="border: 0px; width: 600px">
                                 <asp:ListItem>Monday</asp:ListItem>
                                 <asp:ListItem>Tuesday</asp:ListItem>
                                 <asp:ListItem>Wednesday</asp:ListItem> 
                                 <asp:ListItem>Thursday</asp:ListItem>
                                 <asp:ListItem>Friday</asp:ListItem>
                                 <asp:ListItem>Saturday</asp:ListItem>
                                 <asp:ListItem>Sunday</asp:ListItem>
                                 
                                 </asp:CheckBoxList>
                                 
                            </td> 
                           
                          </tr>
                          <tr> 
                          <td>Time Frame:</td>
                            <td>
                            From : <asp:DropDownList runat="server" ID="ddlFromTime" style="width: 100px;">
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
                                 <asp:ListItem Value="17:30">17:30</asp:ListItem>
                                 <asp:ListItem Value="18:00">18:00</asp:ListItem>
                                 <asp:ListItem Value="18:30">18:30</asp:ListItem>
                                 <asp:ListItem Value="19:00">19:00</asp:ListItem>
                                 <asp:ListItem Value="19:30">19:30</asp:ListItem>
                                 <asp:ListItem Value="20:00">20:00</asp:ListItem>
                                 </asp:DropDownList>&nbsp;&nbsp;
                            To :  <asp:DropDownList runat="server" ID="ddlToTime" style="width: 100px;">
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
                                 <asp:ListItem Value="17:30">17:30</asp:ListItem>
                                 <asp:ListItem Value="18:00">18:00</asp:ListItem>
                                 <asp:ListItem Value="18:30">18:30</asp:ListItem>
                                 <asp:ListItem Value="19:00">19:00</asp:ListItem>
                                 <asp:ListItem Value="19:30">19:30</asp:ListItem>
                                 <asp:ListItem Value="20:00">20:00</asp:ListItem> 
                                 </asp:DropDownList>  
                            
                             
                          </td>
                          <td><span style="margin-top: 20px; display: inline;">Work on Weekends?</span>
                           </td>
                           <td> <asp:DropDownList runat="server" ID="ddlWeekends" style="margin-top: 10px;">
                                 <asp:ListItem Value="">Select One</asp:ListItem>
                                 <asp:ListItem Value="No">No</asp:ListItem>
                                 <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                  <asp:ListItem Value="Yes">Maybe</asp:ListItem>
                                 </asp:DropDownList></td>
                          </tr>
                        <tr> 
                           <td class="form-label" style="height: 110px">Note for Working Time: <span style="color: red; display: inline;">*</span></td>
                          <td colspan="3"><asp:Textbox ID="txtNoteWorkTime"  class="form-field" runat="server"  CssClass="form-textarea" TextMode="MultiLine" /></td>
                      
                            </tr>  
                       
                      
                        </tbody></table>
            </ContentTemplate>
            </asp:UpdatePanel>                       
          <!--END OF "EMPLOYEE TYPE"-->
 	</div>
 	<div id="tabs-4"  name="tabs-4">
 	<!--QUALIFICATION REQUIRED-->
        <table style="width: 100%; margin-top: 20px;" class="no-zebra">
                        <thead>
                            <tr>
                                <th colspan="4" align="left" style="padding: 10px;">Qualification Required</th>
                            </tr>
                        </thead>
                        
                        <tbody>
                            <tr>
                                 <td class="form-label">Language Skill 1: </td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlLanguage1">
                                 <asp:ListItem Value="" Selected="True">Select Language</asp:ListItem>
                                 <asp:ListItem Value="English">English</asp:ListItem>
                                 <asp:ListItem Value="Czech">Czech</asp:ListItem> 
                                 <asp:ListItem Value="Chinese">Chinese</asp:ListItem> 
                                 <asp:ListItem Value="German">German</asp:ListItem>
                                 <asp:ListItem Value="French">French</asp:ListItem> 
                                 </asp:DropDownList>
                                 </td>
                                 <td class="form-label">Level: </td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlLanguageLvl1">
                                 <asp:ListItem Value="" Selected="True">Select Level</asp:ListItem>
                                 <asp:ListItem Value="Beginner">Beginner</asp:ListItem>
                                 <asp:ListItem Value="Intermediate">Intermediate</asp:ListItem>
                                   <asp:ListItem Value="Advanced">Advanced</asp:ListItem>
                                 </asp:DropDownList>
                                 </td>                         
                            </tr>
                            <tr>
                                 <td class="form-label">Language Skill 2: </td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlLanguage2">
                                 <asp:ListItem Value="" Selected="True">Select Language</asp:ListItem>
                                 <asp:ListItem Value="English">English</asp:ListItem>
                                 <asp:ListItem Value="Czech">Czech</asp:ListItem> 
                                 <asp:ListItem Value="Chinese">Chinese</asp:ListItem> 
                                 <asp:ListItem Value="German">German</asp:ListItem>
                                 <asp:ListItem Value="French">French</asp:ListItem> 
                                 </asp:DropDownList>
                                 </td>
                                 <td class="form-label">Level: </td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlLanguageLvl2">
                                 <asp:ListItem Value="" Selected="True">Select Level</asp:ListItem>
                                 <asp:ListItem Value="Beginner">Beginner</asp:ListItem>
                                 <asp:ListItem Value="Intermediate">Intermediate</asp:ListItem>
                                 <asp:ListItem Value="Advanced">Advanced</asp:ListItem>
                                 </asp:DropDownList>
                                 </td>                         
                            </tr> 
                            <asp:Panel runat="server" ID="pnlEducationInfo">
                             <tr>
                                 <td class="form-label">Education: <span style="color: red; display: inline;">*</span></td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlEducation"  >
                                 <asp:ListItem Value="" Selected="true">Select One</asp:ListItem>
                                 <asp:ListItem Value="Primary">Primary School</asp:ListItem>
                                 <asp:ListItem Value="Secondary">Secondary School</asp:ListItem>
                                 <asp:ListItem Value="University">University</asp:ListItem>                                 
                                 </asp:DropDownList>
                                 <asp:RequiredFieldValidator runat="server"   ID="reqddlEducation" ControlToValidate="ddlEducation"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                                 </td>
                                 <td class="form-label">Major In: </td>
                                 <td><asp:Textbox ID="txtMajorIn"  class="form-field" runat="server" /></td>                         
                            </tr>  
                            <tr>
                             <td class="form-label">Required Work Exp.: </td>
                            <td  ><asp:Textbox ID="txtRequiredWorkExp"  class="form-field" runat="server" /></td>  
                             <td class="form-label">&nbsp;</td>
                            <td  >&nbsp;</td>  
                            </tr>
                            </asp:Panel>  
                           <tr>
                                 <td  style="height: 150px" class="form-label">Other Remarks :</td>
                                  
                                 <td colspan="3"><asp:Textbox ID="txtRemarks"  class="form-field" runat="server" TextMode="MultiLine" CssClass="form-textarea" /></td>
             
                                             
                            </tr>    
                            <tr>
                      <td class="form-label"  style="height: 150px" ><asp:Label runat="server" ID="lblNoteExperience" Text="Note For Work Experience:" style="color: #111111"></asp:Label><asp:Label runat="server" ID="lblOtherRequirements" Text="Other Requirements:"   CssClass="form-label" style="font-weight: bold; color: #111111"></asp:Label></td>
                                  
                  <td colspan="3"><asp:Textbox ID="txtNoteExperience"  class="form-field" runat="server" TextMode="MultiLine" CssClass="form-textarea" /><asp:Textbox ID="txtOtherRequirements" runat="server" TextMode="MultiLine" CssClass="form-textarea" /></td>

                            </tr>                                                                                                                          
                      </tbody>
                    </table>
                    <table style="width: 100%; margin-top: 20px;"><tr><td  align="right">
                    <asp:Panel runat="server" ID="pnlErrorBelow" style="border: solid 1px #c0221f; background: #ffbde5; width: 260px; padding: 10px; margin: 0 auto; margin-bottom: 10px;" >
                    <asp:Label runat="server" ID="lblValidation" style="font-size: 11px;"></asp:Label>
                    </asp:Panel>
                    
                    
         <asp:Button ID="btnSave" runat="server" OnClick="BtnRecruitment_Save" Text="Save" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;" />
      
     	 <asp:Button ID="btnSaveAnother" runat="server" OnClick="IsSaveAnother" Text="Save & Add Another" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;" />
    </td></tr></table>
          <!--END OF "QUALIFICATION REQUIRED"-->

 	</div>

 	</div>
 	
 	</asp:Panel>
</asp:Panel>

<asp:Panel runat="server" ID="pnlViewingRequest">
<req:Request runat="server" ID="RequestView"></req:Request>
</asp:Panel> 

<asp:Panel runat="server" ID="pnlEditRequest">
<req:EditRequest runat="server" ID="RequestEdit" />
</asp:Panel>
</asp:Content>
