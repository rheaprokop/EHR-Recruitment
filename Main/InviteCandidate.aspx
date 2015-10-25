<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="InviteCandidate.aspx.cs" Inherits="EHR.Main.InviteCandidate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
<script type="text/javascript">

    $("#ctl00_mainContent_frmCandidate_btnLookUp").click(function() {
    $("#ctl00_mainContent_dialog").dialog("close");
        return false;
    });

    var confirmed = false;
    function confirmDialog(obj) {
        if (!confirmed) {

            $("#ctl00_mainContent_dialog").dialog({

                resizable: false,
                height: 700,
                width: 1000,
                modal: true,
                title: "Requisition Box",
                open: function(type, data) {
                    $(this).parent().appendTo("form");
                },
                buttons: {
                     
                    "Cancel": function() {
                        $(this).dialog("close");
                    }
                }
            });
        }

        return confirmed;
    }

   
	</script>   
	
 
<asp:Panel ID="dialog" runat="server"  style="display:none;">
   
<asp:UpdatePanel ID="pnlView" runat="server">
<ContentTemplate>  
   <asp:GridView ID="gridAllRequest" CellPadding="0" CellSpacing="0" GridLines="None" OnRowDataBound="GridAllRequest_DataBound"
     AllowPaging="false"  runat="server" CssClass="grid-zebra" style="width: 900px;"   AutoGenerateColumns="true"
     PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="../Content/css/light/front/first.png">
   <Columns>   
   <asp:hyperlinkfield DataNavigateUrlFields="RequestID"  
   DataNavigateUrlFormatString="../Main/InviteCandidate.aspx?action=send&request={0}"  
   DataTextField="RequestID" HeaderText=""  
   DataTextFormatString= "<img src='../Content/css/light/front/interviewform.png'  border=0  />" >
   </asp:hyperlinkfield> 
    </Columns>
    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
        NextPageImageUrl="../Content/css/light/front/next.png" 
        PreviousPageImageUrl="../Content/css/light/front/previous.png"  
        FirstPageImageUrl="../Content/css/light/front/first.png"   />
 </asp:GridView> 

</ContentTemplate> 
</asp:UpdatePanel> 
</asp:Panel>
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


<table style="width: 90%;">
<tbody> 
<tr><td>

            <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">CANDIDATE INTERVIEW INFO</th>
                    </tr>
                </thead> 
                
                      <tr>
                         <td class="form-label" style="width: 200px;" > For Request ID: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate> 
                         <asp:TextBox ID="txtInterviewRequestID" runat="server"  CssClass="form-field" Enabled="false" style="width: 200px;"></asp:TextBox> 
                         <asp:ImageButton ID="btnLookUp" runat="server" OnClientClick="return confirmDialog(this);" ImageUrl="../../Content/css/light/front/lookup.png" style="width: 30px; height: 30px;" />
                         <asp:RequiredFieldValidator  runat="server" ID="reqtxtInterviewRequestID" ControlToValidate="txtInterviewRequestID"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                            </ContentTemplate></asp:UpdatePanel>
                         </td>
                         <td class="form-label"  style="width: 200px;"> </td> 
                        <td>
                            &nbsp;
                        </td>
                     </tr>
                     
                     <tr>
                         <td class="form-label" style="width: 200px;" >Interview Date: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:TextBox ID="txtInterviewDate" runat="server" CssClass="form-field"  Enabled="false" style="width: 200px;"></asp:TextBox> 
                         <asp:Calendar SelectionMode="DayWeekMonth"
                                      id="calInterviewDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server"  
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest" 
                                      BorderWidth="2px" 
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset" CellPadding="1" CssClass="calendar"
                                      OnSelectionChanged="InterviewDate_SelectionChanged">
                                      
                                
                              </asp:Calendar>
                                     <asp:ImageButton ID="btnCalPosiOnboard" runat="server" OnClick="InterviewDate_OnClick" 
                                     ImageUrl="~/Content/css/light/images/calendar.gif" CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                                      
                          <asp:RequiredFieldValidator  runat="server" ID="reqtxtInterviewDate" ControlToValidate="txtInterviewDate"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                       
                       
                         </td>
                        <td class="form-label" style="width: 200px;" >Candidate Name: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:TextBox ID="txtCandidateName" runat="server"  CssClass="form-field" Enabled="false" style="width: 200px;"></asp:TextBox> 
                          <asp:HiddenField runat="server" ID="hiddenCandidateID" />
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
                                <asp:ListItem>Vlastimila Pecha 1296/10, 627 00 Brno Slatina</asp:ListItem> 
                                 <asp:ListItem>K Letisti 1792/1 66451 Slapanice, Brno-Venkov</asp:ListItem>
                                 </asp:DropDownList>
                          <asp:RequiredFieldValidator  runat="server" ID="reqddlWorkPlace" ControlToValidate="ddlWorkPlace"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                  </td> 
                     </tr>   
                    </table> 
</td></tr> 
 <tr><td >
            <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">WILL BE INTERVIEW BY</th>
                    </tr>
                </thead> 
            
                <tbody>  
                     <tr>
                         <td class="form-label" style="width: 200px;" >HR Recruiter: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:TextBox runat="server" ID="txtRecruiter"></asp:TextBox>
                         <asp:Label runat="server" ID="lblRecruiter"></asp:Label>
                         </td>
                         <td class="form-label"  style="width: 200px;">  Dept. Manager:<span style="color: red; display: inline;">*</span></td> 
                        <td><asp:HiddenField ID="hiddenManagerID" runat="server" />
                          <asp:TextBox runat="server" ID="txtManagerName"  Enabled="false"></asp:TextBox>
                          <asp:RequiredFieldValidator  runat="server" ID="reqtxtManagerName" ControlToValidate="txtManagerName"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                        </td>
                     </tr>  
                                                  
                    </table> 
                 
</td></tr> 
<tr><td align="right">
<br />
 <asp:Button ID="btnSend" runat="server" Text="Send Invitation To Candidate" OnClick="SendInterviewInv_Click" 
   CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
   style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px;" /> 
 <asp:Button ID="Button1" runat="server" Text="Save Invitation Only" OnClick="SaveInvitationOnly_Click" 
   CssClass="ui-button ui-button-text-only ui-widget ui-state-default" 
   style="font-family: BebasNeue; font-size: 18px; letter-spacing: 1px;" /> 
</td></tr>
<tr><td> 

        <table style="width: 50%;  margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th align="left" style="padding: 10px; font-weight: normal;">ADD PEOPLE TO BE INCLUDED IN THE INTERVIEW</th>
                    </tr>
                </thead> 
                <tbody> 
                     <tr>
                         
                        <td class="form-label"  > 
                         
                        <asp:TextBox TextMode="MultiLine" Rows="5" Columns="300"  ID="txtOtherInterviewers" CssClass="form-field" runat="server" placeholder="Enter Employee ID(s) separte with comma (,)" style="width: 500px;"></asp:TextBox>   
                        <asp:PlaceHolder runat="server" ID="placeInterviewer"></asp:PlaceHolder>
                        </td>  
                     </tr>  
                                                                        
                </tbody>
            </table>
       
</td></tr>
</tbody>
</table>
     
</asp:Content>
