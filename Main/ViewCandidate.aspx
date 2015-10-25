<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ViewCandidate.aspx.cs" Inherits="EHR.Main.ViewCandidate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
             
             <script type="text/javascript" language="javascript">
                function ConfirmOnDelete()
                {
                  if (confirm("Are you sure?")==true)
                    return true;
                  else
                    return false;
                }
     
            </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
<asp:Panel runat="server" ID="pnlRequestManager">
<li><span style="padding-left: 30px;"><a href="AllRequests.aspx">Request Manager</span></a></li>
</asp:Panel>
<asp:Panel runat="server" ID="pnlCandidateManager">
<li class="selected"><span style="padding-left: 30px;"><a href="Candidate.aspx?action=view">Candidate Manager</span></a></li> 
</asp:Panel>
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
 <br />
  <table style="width: 85%; margin-left: 20px; " >   
 <tr><td>
            <table style="width: 100%; margin-top: 0px;" class="no-zebra" align="left"> 
                
                <thead> 
                    <tr>
                        <th colspan="4" align="left">CANDIDATE STATUS</th>
                    </tr>
                </thead> 
                <tbody>
                        <tr>
                         <td style="padding-left: 30px; font-weight: bold; width: 260px;">Candidate Status: </td>
                         <td  ><asp:Label runat="server" ID="lblCandidateStatus" style="display: inline" ></asp:Label> 
                         </td>
                         <td>&nbsp;</td>
                         <td>
                         &nbsp;
                          </td>               
                    </tr>   
                    <asp:Panel runat="server" ID="pnlHideAcceptance">
                        <tr>
                         <td style="padding-left: 30px; font-weight: bold; width: 260px;">Candidate Accepted Job: </td>
                         <td colspan="2">
                            <asp:RadioButtonList runat="server" ID="radioAcceptance" RepeatDirection="Horizontal" style="display: inline">
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:RadioButtonList> 
                            &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button runat="server" ID="btnCandidateAcceptance" Text="Submit" style="display: inline;"
                          CssClass="ui-button ui-button-text-only ui-widget ui-state-default"  CausesValidation="false" OnClick="BtnCandidateAcceptedAction_Click"  /></td>
                          
                          </td>               
                    </tr>             
                    </asp:Panel>                         
                </tbody>
                
           </table>
           
</td></tr>   
    <tr><td>
     
            <table style="width: 100%; margin-top: 20px;" class="zebra-view" align="left"> 
                
                <thead> 
                    <tr>
                        <th colspan="4" align="left">CANDIDATE INFORMATION</th>
                    </tr>
                </thead> 
                <tbody>
                        <tr>
                         <td>Candidate ID: </td>
                         <td><asp:Label runat="server" ID="lblCandidateID" ></asp:Label></td>
                         <td>Academic Title: </td>
                         <td>
                         <asp:Label runat="server" ID="lblAcademitTitle" ></asp:Label>
                          </td>               
                    </tr>                
                    <tr>
                         <td>First Name: </td>
                         <td><asp:Label runat="server" ID="lblFirstName" ></asp:Label></td>
                         <td>Last Name: </td>
                         <td><asp:Label runat="server" ID="lblLastName" ></asp:Label></td>                         
                    </tr>                  
                   
                     
                    </tbody>
           </table>
</td></tr> 

 <tr><td>   
         <!--Start of Candidate Address-->
         <table style="width: 100%; margin-top: -10px;" class="zebra-view" align="left">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">CANDIDATE ADDRESS</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
                
                <tbody>
  
                     <tr>
                         <td >Street: </td>
                         <td><asp:Label runat="server" ID="lblStreet" ></asp:Label></td>
                         <td >City </td>
                         <td><asp:Label runat="server" ID="lblCity" ></asp:Label></td>                         
                    </tr> 
 
                     <tr>
                         <td >Country of Citizenship: </td>
                         <td>
                        <asp:Label runat="server" ID="lblCountry" ></asp:Label>
                         </td>
                         <td >Postcode/zip code: </td>
                         <td><asp:Label runat="server" ID="lblPostCode" ></asp:Label></td>                         
                    </tr>  
                   <tr>
                         <td >Contact Number: </td>
                         <td> <asp:Label runat="server" ID="lblContactNumber"  style="display:inline;"></asp:Label></td>
                         <td >Email Address: </td>
                         <td><asp:Label runat="server" ID="lblEmailAddress" ></asp:Label></td>                         
                    </tr>                                       
              </tbody>
            </table>
         <!-- End of Candidate Address-->   
</td></tr>
<tr><td>  
        <table style="width: 100%; margin-top:  -10px;" class="zebra-view" align="left">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">PERSONAL INFORMATION</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
                
                <tbody>
  
                     <tr>
                         <td >Birthdate: </td>
                          <td >
                         <asp:Label runat="server" ID="lblBirthdate" ></asp:Label> 
                             
                         </td>
                         <td >Sex </td>
                         <td>
                         <asp:Label runat="server" ID="lblSex" ></asp:Label>
                         </td>                         
                    </tr> 
                      <tr>
                         <td >Status: </td>
                         <td>
                         <asp:Label runat="server" ID="lblStatus" ></asp:Label>
                         </td>
                         <td >Birth Number: </td>
                         <td>
                         <asp:Label runat="server" ID="lblBirthNumber" ></asp:Label>
                         </td>
                    </tr>                    
                </tbody>
           </table> 
</td></tr>
<tr><td>

        <table style="width: 100%; margin-top:  -10px;" class="zebra-view" align="left">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">EDUCATION INFORMATION</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
                <tbody>
  
                     <tr>
                         <td>Graduate: </td>
                         <td>
                         <asp:Label runat="server" ID="lblGraduate" ></asp:Label> 
                         </td>
                         <td >Field: </td>
                         <td>
                        <asp:Label runat="server" ID="lblField" ></asp:Label>
                         </td>                         
                    </tr> 
                    <tr>
                                 <td >Language Skill 1: </td>
                                 <td>
                                <asp:Label runat="server" ID="lblLanguage1" ></asp:Label>
                                 </td>
                                 <td >Level: </td>
                                 <td>
                                 <asp:Label runat="server" ID="lblSkill1" ></asp:Label>
                                 </td>                         
                            </tr>                   
 <tr>
                                 <td >Language Skill 2: </td>
                                 <td>
                                 <asp:Label runat="server" ID="lblLanguage2" ></asp:Label>
                                 </td>
                                 <td >Level: </td>
                                 <td>
                                 <asp:Label runat="server" ID="lblSkill2" ></asp:Label>
                                 </td>                         
                            </tr>
                    
                </tbody> 
                    
                    </table>
</td></tr>
<tr><td>
<table style="width: 100%; margin-top:  -10px;" class="zebra-view" align="left">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">PROFESSIONAL EXPERIENCE</th>
                    </tr>
                </thead> 
                <tbody>
                   <tr>
                         <td >Professional Experience: </td>
                         <td>
                            <asp:Label runat="server" ID="lblExperience" ></asp:Label>
                       
                         </td>
                         <td >Field of expertise: </td>
                         <td>
                         <asp:Label runat="server" ID="lblExpertise" ></asp:Label>
                         </td>                         
                    </tr> 
                     <tr>
                         <td >Current Employer: </td>
                         <td>
                        <asp:Label runat="server" ID="lblCurrentEmpl" ></asp:Label>
                       
                         </td>
                         <td >Current Position: </td>
                         <td>
                         <asp:Label runat="server" ID="lblCurrentPosi" ></asp:Label>
                         </td>                         
                    </tr> 
                </tbody>
 </table>
</td></tr>
<tr><td>
        <table style="width: 100%; margin-top:  -10px;" class="zebra-view" align="left">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">REMARKS</th>
                    </tr>
                </thead>  
                    <tr>
                          
                           <td >Remarks: </td>
                           <td  class="form-check"> 
                                 <asp:Label runat="server" ID="lblRemarks"  Text="Remarks"></asp:Label>
                                 
                           </td>  
                      </tr>
                    </table>
</td></tr>
<tr><td>
<!--Start of Candidate CV-->
         <table style="width: 100%; margin-top:  -10px;" class="zebra-view" align="left">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">Upload Documents</th>
                    </tr>
                </thead> 
                
                <tbody> 
                     <tr>
                       <td> Czech CV: </td>
                         <td>
                            <asp:Button runat="server" ID="btnDownloadCzechCV" Text="Click To Download" OnClick="BtnDownloadCzech_Click" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" />
                            <asp:Label runat="server" ID="lblNoCzechCV" Visible="false" Text="No file uploaded"></asp:Label>
                        </td>
                         <td >English CV: </td>
                         <td >
                            <asp:Button runat="server" ID="btnEnglishCV" Text="Click To Download" OnClick="BtnDownloadEnglish_Click" CausesValidation="false" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" />
                            <asp:Label runat="server" ID="lblNoEnglishCV" Visible="false" Text="No file uploaded"></asp:Label>
                         </td>
                                           
                    </tr>  
                     
                    </tbody>
        </table>                                  

</td></tr>  
</table> 
</asp:Content>
