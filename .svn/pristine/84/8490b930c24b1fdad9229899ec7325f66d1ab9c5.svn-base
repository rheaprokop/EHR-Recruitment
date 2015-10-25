<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CandidateInfo.aspx.cs" Inherits="EHR.Recruitment.CandidateInfo" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Top" ContentPlaceHolderID="TopContent" runat="server">
 
</asp:Content>
<asp:Content ID="Left" ContentPlaceHolderID="LeftContent" runat="server"> 

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="mainContent" runat="server">
<asp:Panel ID="pnlCreateCandDesign" runat="server" style="margin-left: 20px; margin-right: 10px; margin-top: -10px;"> 
  <div class="header">
    <div>
   <asp:Label ID="lblTitle" Text="Candidate Profile" runat="server" CssClass="logotext"></asp:Label>
   </div>
  </div>
  
  <asp:Panel runat="server" ID="pnlButtons" style="margin-top: 10px;">
       <table style="width: 95%; " > 
        <tr>
        
        <td align="right">
        <asp:HiddenField runat="server" ID="hiddenCandID" />
        <asp:HiddenField runat="server" ID="hiddenReqID" />
 
     </td></tr> 
    </table>
    </asp:Panel>
 <table style="width: 85%; " >   
    <tr><td>
            <table style="width: 100%; margin-top: 20px;" class="zebra-view" align="center"> 
                <tbody>
                <thead> 
                    <tr>
                        <th colspan="4" style="padding: 10px;" align="left">CANDIDATE INFORMATION</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
                        <tr>
                         <td>Candidate ID: </td>
                         <td><asp:Label runat="server" ID="lblCandidateID" CssClass="candidate-label"></asp:Label></td>
                         <td>Academic Title: </td>
                         <td>
                         <asp:Label runat="server" ID="lblCandidateTitle" CssClass="candidate-label"></asp:Label>
                          </td>               
                    </tr>                
                    <tr>
                         <td>First Name: </td>
                         <td><asp:Label runat="server" ID="lblFirstName" CssClass="candidate-label"></asp:Label></td>
                         <td>Last Name: </td>
                         <td><asp:Label runat="server" ID="lblLastName" CssClass="candidate-label"></asp:Label></td>                         
                    </tr>                  
                   
                     
                    </tbody>
           </table>
</td></tr> 

 <tr><td>   
         <!--Start of Candidate Address-->
         <table style="width: 100%; margin-top: 20px;" class="zebra-view">
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
                         <td><asp:Label runat="server" ID="lblStreet" CssClass="candidate-label"></asp:Label></td>
                         <td >City </td>
                         <td><asp:Label runat="server" ID="lblCity" CssClass="candidate-label"></asp:Label></td>                         
                    </tr> 
 
                     <tr>
                         <td >Country of Citizenship: </td>
                         <td>
                        <asp:Label runat="server" ID="lblCountry" CssClass="candidate-label"></asp:Label>
                         </td>
                         <td >Postcode/zip code: </td>
                         <td><asp:Label runat="server" ID="lblPostCode" CssClass="candidate-label"></asp:Label></td>                         
                    </tr>  
                   <tr>
                         <td >Contact Number: </td>
                         <td> <asp:Label runat="server" ID="lblContactNumber" CssClass="candidate-label" style="display:inline;"></asp:Label></td>
                         <td >Email Address: </td>
                         <td><asp:Label runat="server" ID="lblEmailAddress" CssClass="candidate-label"></asp:Label></td>                         
                    </tr>                                       
              </tbody>
            </table>
         <!-- End of Candidate Address-->   
</td></tr>
<tr><td>  
        <table style="width: 100%; margin-top: 20px;" class="zebra-view">
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
                         <asp:Label runat="server" ID="lblBirthdate" CssClass="candidate-label"></asp:Label> 
                             
                         </td>
                         <td >Sex </td>
                         <td>
                         <asp:Label runat="server" ID="lblSex" CssClass="candidate-label"></asp:Label>
                         </td>                         
                    </tr> 
                      <tr>
                         <td >Status: </td>
                         <td>
                         <asp:Label runat="server" ID="lblStatus" CssClass="candidate-label"></asp:Label>
                         </td>
                         <td >  </td>
                         <td>&nbsp;</td>                         
                    </tr>                    
           
           </table> 
</td></tr>
<tr><td>

        <table style="width: 100%; margin-top: 20px;" class="zebra-view">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">EDUCATION INFORMATION</th>
                    </tr>
                </thead> 
                <tbody>
  
                     <tr>
                         <td >Graduate: </td>
                         <td>
                         <asp:Label runat="server" ID="lblGraduate" CssClass="candidate-label"></asp:Label>
                       
                         </td>
                         <td >Field: </td>
                         <td>
                        <asp:Label runat="server" ID="lblField" CssClass="candidate-label"></asp:Label>
                         </td>                         
                    </tr> 
 <tr>
                                 <td >Language Skill 1: </td>
                                 <td>
                                <asp:Label runat="server" ID="lblLanguage1" CssClass="candidate-label"></asp:Label>
                                 </td>
                                 <td >Level: </td>
                                 <td>
                                 <asp:Label runat="server" ID="lblSkill1" CssClass="candidate-label"></asp:Label>
                                 </td>                         
                            </tr>                   
 <tr>
                                 <td >Language Skill 2: </td>
                                 <td>
                                 <asp:Label runat="server" ID="lblLanguage2" CssClass="candidate-label"></asp:Label>
                                 </td>
                                 <td >Level: </td>
                                 <td>
                                 <asp:Label runat="server" ID="lblSkill2" CssClass="candidate-label"></asp:Label>
                                 </td>                         
                            </tr>
                    
                    
                    
                    </table>
</td></tr>
<tr><td>

<table style="width: 100%; margin-top: 20px;" class="zebra-view">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">PROFESSIONAL EXPERIENCE</th>
                    </tr>
                </thead> 
                <tbody>
                   <tr>
                         <td >Professional Experience: </td>
                         <td>
                            <asp:Label runat="server" ID="lblExperience" CssClass="candidate-label"></asp:Label>
                       
                         </td>
                         <td >Field of expertise: </td>
                         <td>
                         <asp:Label runat="server" ID="lblExpertise" CssClass="candidate-label"></asp:Label>
                         </td>                         
                    </tr> 
                     <tr>
                         <td >Current Employer: </td>
                         <td>
                        <asp:Label runat="server" ID="lblCurrentEmpl" CssClass="candidate-label"></asp:Label>
                       
                         </td>
                         <td >Current Position: </td>
                         <td>
                         <asp:Label runat="server" ID="lblCurrentPosi" CssClass="candidate-label"></asp:Label>
                         </td>                         
                    </tr> 
 </table>
</td></tr>
<tr><td>
<table style="width: 100%; margin-top: 20px;" class="zebra-view">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">OTHER INFORMATION</th>
                    </tr>
                </thead>  
                    <tr>
                          
                           <td >Driving License: </td>
                           <td  class="form-check"> 
                                 <asp:Label runat="server" ID="lblDrivingLicense" CssClass="candidate-label"></asp:Label>
                                 
                           </td> 
                           <td >Which Vehicle: </td>
                           <td>
                            <asp:Label runat="server" ID="lblIsCar" style="display: inline;"></asp:Label>&nbsp;&nbsp;
                            <asp:Label runat="server" ID="lblIsForkLift" style="display: inline;"></asp:Label>
                          </tr>       
                    </table>
</td></tr>
<tr><td>
<!--Start of Candidate CV-->
         <table style="width: 100%; margin-top: 20px;" class="zebra-view">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">Upload Documents</th>
                    </tr>
                </thead> 
                
                <tbody> 
                     <tr>
                       <td > Czech CV: </td>
                         <td>
                         <asp:Label runat="server" ID="lblIsCVCZ" CssClass="candidate-label"></asp:Label>
                       </td>
                         <td > English CV: </td>
                         <td ><asp:Label runat="server" ID="lblIsCVEN" CssClass="candidate-label"></asp:Label>
                         </td>
                                              
                    </tr>  
        </table>                                  

</td></tr> 
 <tr><td align="right">  
                    <asp:Button ID="btnSavePS" runat="server"  Text="SAVE PROFILE TO PEOPLESOFT"  OnClick="BtnSave_Click"
                     CssClass="ui-button ui-button-text-only ui-widget ui-state-default " style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; display: inline;" />
 
  </td></tr>
</table>
</asp:Panel>
</asp:Content>
