<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="InterviewResult.aspx.cs" Inherits="EHR.Main.InterviewResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
<div class="header">
  <div>
   <asp:Label ID="lblCandidateTitle" Text="Candidate Manager" runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right;" class="ui-buttonset">
 <asp:HyperLink runat="server" NavigateUrl="~/Main/Candidate.aspx"   ID="btnViewAll" Text="View All Candidates" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />

 <asp:HyperLink  runat="server" NavigateUrl="~/Main/AddCandidate.aspx" ID="btnAssign" Text="Create Profile" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
</div>
</div>

 <!--INSERT CANDIDATE PROFILE HERE -->
 

<asp:Panel runat="server" ID="Panel1" style="margin-top: 10px;">
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
    

	 <table style="width: 95%;" >
		 <tr><td><asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                    <table style="width: 100%; margin-top: 20px;" class="zebra"> 
                        <tbody>  
                    
                           <tr>
                                 <td >Interview Date: <span style="color: red; display: inline;">*</span></td>
                                 <td >
                                 <asp:TextBox runat="server" ID="txtInterviewDate" ReadOnly="true" >
                                 
                                 </asp:TextBox>
                                 
                                 <asp:Calendar SelectionMode="DayWeekMonth" 
                                      id="calInterviewDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server" 
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest"
                                      BorderWidth="2px"
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset"  CellPadding="1" CssClass="calendar"
                                      OnSelectionChanged="CalInterviewDate_Selection_Change">
                                      
                                
                              </asp:Calendar>
                                     <asp:ImageButton ID="imgInterviewDate" runat="server" OnClick="InterviewDate_Click" 
                                     ImageUrl="~/Content/css/light/images/calendar.gif"  CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                                   <asp:RequiredFieldValidator  runat="server" ID="reqtxtInterviewDate" ControlToValidate="txtInterviewDate"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                                 </td>
                                 <td > </td>
                                 <td>&nbsp;
                                 </td>                                      
                            </tr>                     
                            <tr>
                                 
                                 <td >Interviewed By: <span style="color: red; display: inline;">*</span></td>
                                 <td >
                                 <asp:TextBox runat="server" ID="txtInterviewedBy"></asp:TextBox>
                                 <asp:RequiredFieldValidator  runat="server" ID="reqddlInterviewedBy" ControlToValidate="txtInterviewedBy"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                                 </td>   
                                 <td>For Request ID: <span style="color: red; display: inline;">*</span></td>   
                                 
                                  <td><asp:DropDownList runat="server" ID="ddlRequestID" AutoPostBack="true">
                                 
                                 </asp:DropDownList>
                                   <asp:RequiredFieldValidator  runat="server" ID="reqddlRequestID" ControlToValidate="ddlRequestID"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>

                                 </td>                               
                            </tr>    
                            
                           <tr>
                                 <td >Possible Onboard Date: <span style="color: red; display: inline;">*</span></td>
                                 <td >
                                 <asp:TextBox runat="server" ID="txtOnBoardDate" >
                                 
                                 </asp:TextBox>
                                 
                                 <asp:Calendar SelectionMode="DayWeekMonth" 
                                      id="calPosiOnBoardDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server"
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest"
                                      BorderWidth="2px"
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset" CellPadding="1" CssClass="calendar"
                                      OnSelectionChanged="CalPosiOnBoardDate_Selection_Change">
                                      
                                
                              </asp:Calendar>
                                     <asp:ImageButton ID="btnCalPosiOnboard" runat="server" OnClick="ViewOnBoardCal_OnClick" 
                                     ImageUrl="~/Content/css/light/images/calendar.gif" CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                                   <asp:RequiredFieldValidator  runat="server" ID="reqtxtOnBoardDate" ControlToValidate="txtOnBoardDate"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                                 </td>
                                 <td >Salary Expectation: </td>
                                 <td>
                                 <asp:TextBox runat="server" ID="txtSalaryExp"></asp:TextBox>
                                 </td>                                      
                            </tr>     
                            <tr>
                            <td >For Department: <span style="color: red; display: inline;">*</span></td>
                              <td >  
                               <asp:DropDownList  AutoPostBack="true" runat="server" OnSelectedIndexChanged="DdlDepartment_SelectedIndexChanged"
                                 ID="ddlDepartment">
                                     </asp:DropDownList>
                                      <asp:RequiredFieldValidator  runat="server" ID="reqddlDepartment" ControlToValidate="ddlDepartment"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                         
                                     </td>
                                <td>
                                For Plant:  
                                </td>
                                <td>
                                <asp:DropDownList runat="server" ID="ddlPlant"  class="form-field" >
                            <asp:ListItem Value="">Select A Plant</asp:ListItem>
                            <asp:ListItem Value="F521">Plant 1</asp:ListItem> 
                            <asp:ListItem Value="F525">Plant 3</asp:ListItem>
                            <asp:ListItem Value="SU">Site Support</asp:ListItem>
                            </asp:DropDownList>
                                </tr>                              
                          
                                  
                            </tbody>
                   </table>
                   </ContentTemplate></asp:UpdatePanel>
     </td></tr> 
     <tr>
     <td>
     
                  <table style="width: 100%; margin-top: 20px;" class="zebra"> 
                           
                            <thead>
                                <tr>
                                    <th align="left" style="padding: 10px;">Interview Remarks</th>
                                </tr>
                            </thead> 
                             <tbody>
                            <tr><td style="padding: 20px;">
                           <asp:Textbox ID="txtRemarks"  class="form-field" runat="server" TextMode="MultiLine" Width="800px" CssClass="form-textarea" />
                            
                            </td></tr>        
                       </tbody>
                  </table>
     </td>
     </tr>      
     <tr>
     <td> 
                  <table style="width: 100%; margin-top: 20px;" class="zebra"> 
                            
                            <thead>
                                <tr>
                                    <th colspan="4" align="left" style="padding: 10px;">CANDIDATE SUITABILITY</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <th colspan="4"></th>
                                </tr>
                            </tfoot>
                            <tbody> 
                            <tr>
                                <td  valign="top">Suitable for position: <span style="color: red; display: inline;">*</span></td>
                                <td valign="middle">
                                <asp:ListBox runat="server" ID="listFromJobs" style="height: 250px;" SelectionMode="Multiple">
                                </asp:ListBox>  
                                </td>
                                <td>
                                   <asp:Button runat="server" ID="btnAddClick" Text="->" OnClick="BtnAddClick"
                                CssClass="ui-button ui-button-text-only ui-widget ui-state-default " 
                               style="width: 100px" CausesValidation="false" />&nbsp;&nbsp;&nbsp; 
                                 <asp:Button runat="server" ID="btnRemove" Text="<-" OnClick="BtnRemoveClick" CausesValidation="false"
                                 style="width: 100px" CssClass="ui-button ui-button-text-only ui-widget ui-state-default " 
                                 /><br /><br /> 
                                </td>
                                <td><asp:ListBox runat="server" ID="listToJobs" SelectionMode="Multiple" style="height: 250px;"></asp:ListBox>&nbsp;&nbsp;&nbsp;
                               <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="listToJobs"
            ErrorMessage="Dont leave blank"></asp:RequiredFieldValidator><br />
                                </td> 
                                </tr>
                       </tbody>
                  </table>
     </td>
     </tr>  
    <tr><td align="right" style="padding: 20px;">
     <asp:Button ID="btnSavedInterview" runat="server"  Text="Save Interview Info" OnClick="SavedInterviewInfo_Click"  
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />

    <asp:Button ID="btnSavedAndSendLetter" runat="server"  Text="Save & Send Acceptance Letter" OnClick="SaveAndSendLetter_Click"  
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />
   <asp:Button ID="btnRejectCandidate" runat="server"  Text="Reject This Candidate" OnClick="RejectCandidateInfo_Click"  
    CausesValidation="false" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />
 
    </td></tr> 
  </table>
</asp:Content>
