<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="EHR.Views.Recruitment.Approval1" %>
<%@ MasterType VirtualPath="~/Views/Shared/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server"> 
      
   <table style="width: 100%;"><tr><td>
            <table style="width: 90%; margin-top: 20px;" class="zebra-view">
                <thead>
                <th colspan="2" style="text-align:left; height: 40px;">
                <h2 style="font-weight: normal;">&nbsp;&nbsp;EHR - Recruitment Form</h2> 
                </th>
                <th colspan="2">
                <span>Request Form ID :  
                <asp:TextBox ID="txtRequestID" runat="server"  Enabled="false" style="background: #dddddd; border: #dddddd; "> 
                         </asp:TextBox> </span>
              <span> Request Date: <asp:TextBox ID="txtRequestedDate" runat="server"  Enabled="false" 
                style="background: #dddddd; border: #dddddd; color: #000000; "></asp:TextBox> </span>
                </th>
                </thead>
                <tbody> 
                     <tr>
                         <td  >Requestor ID: </td>
                         <td ><asp:TextBox ID="txtRequestorEmplID" runat="server"  Enabled="false"> 
                         </asp:TextBox></td>
                         <td >Department Code: </td>
                         <td ><asp:TextBox ID="txtRequestorDeptID" runat="server"  Enabled="false"></asp:TextBox></td>                         
                    </tr>
                     <tr>
                         <td  >Requestor </td>
                         <td ><asp:TextBox ID="txtRequestor" runat="server"  Enabled="false"> 
                         </asp:TextBox></td>
                          <td  >Is it Agency Request? </td>
                         <td ><asp:TextBox ID="txtIsAgency" runat="server"  Enabled="false"> 
                         </asp:TextBox></td>                       
                    </tr>
                                      
                    </tbody>
           </table>
           
           
     </td></tr>
</table>

 
<div id="tabs">
	<div id="tabs-1">
	<asp:UpdatePanel ID="pnlRequestFor" runat="server">
    <ContentTemplate>
 <table style="width: 90%; margin-top: 10px;" class="zebra-view">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">REQUEST FOR</th>
                    </tr>
                </thead> 
                
                <tbody>
                    <tr>
                         <td >Site: </td>
                         <td  ><asp:Textbox ID="txtSite" runat="server" style="color:#000;" onfocus="this.blur();" onselectstart="event.returnValue=false;" Enabled="false"/></td>
                         <td >Plant: </td>
                         <td  > 
                           <asp:Textbox ID="txtPlant" runat="server" Enabled="false"/>
                         </td>                         
                    </tr>
                     <tr>
                         <td >Department: </td>
                         <td >
                          <asp:Textbox ID="txtDeptCode" runat="server" Enabled="false"/>
                         </td>
                         <td >No of Person(s): </td>
                         <td ><asp:Textbox ID="txtNoOfPerson" runat="server" Enabled="false"/>  
                         </td>                         
                    </tr>  
                     <tr> 
                         <td >
                         <asp:Label ID="lblIntendedBoardDate" Text="Intended Board Date" runat="server" style="font-weight: bold; color: #111111;"></asp:Label>
                         <asp:Label ID="lblLengthEmplDate" Text="Length Of Employment Date" runat="server" style="font-weight: bold; color: #111111;"></asp:Label>
                         </td>
                         <td >
                          
                         <asp:TextBox runat="server" id="txtOnBoardDate" Enabled="false"/>
                         
                        
                         <asp:TextBox runat="server" ID="txtLengthDate" Enabled="false"></asp:TextBox>
                       
                        </td> 
                          <td >&nbsp; </td>
                         <td >
                         &nbsp;
                         </td>                        
                    </tr>      
                    
<asp:UpdatePanel ID="pnlRequisitionType" runat="server">
    <ContentTemplate>
        
               <!--REQUISITION TYPE-->
                
                                    <tr>
                                         <td >Request Type: </td>
                                         <td ><asp:Textbox ID="txtRequestType" runat="server" Enabled="false"/>
                                       </td>
                                         <td >
                                         <asp:Label ID="lblIncrease" Text="Increase:" runat="server" style="font-weight: bold; color: #111111;"></asp:Label></td>
                                         <td >
                                          <asp:Textbox ID="txtIncrease" runat="server" Enabled="false"/>
                                    </td>                         
                                    </tr> 
                                    <asp:Panel runat="server" ID="pnlReplacement">
                                   <tr>
                                         <td ><asp:Label Text="Replacement:" ID="lblReplacement" runat="server" style="font-weight: bold; color: #111111;"></asp:Label></td>
                                         <td > <asp:Textbox ID="txtReplacement" runat="server" Enabled="false"/>
                                             </td>
                                         <td >
                                         <asp:Label Text="Replacement To:" ID="lblReplacementTo" runat="server" style="font-weight: bold; color: #111111;"></asp:Label> </td>
                                         <td >
                                          <asp:TextBox ID="txtReplacementTo" runat="server" Enabled="false"></asp:TextBox>
                                         </td>                         
                                    </tr>          
                                      </asp:Panel>                                                            
                             
                  <!--END OF "REQUISITION TYPE"-->
         </ContentTemplate>
            </asp:UpdatePanel>                                                                          
              </tbody>
            </table>
        
            </ContentTemplate>
            </asp:UpdatePanel>    
        <!--END OF "REQUEST FOR TABLE"--> 
	</div>
	<div id="tabs-2"  name="tabs-2">
	   
	</div>
	<div id="tabs-3"  name="tabs-3">
	<asp:UpdatePanel ID="pnlEmployeeType" runat="server">
    <ContentTemplate>
	<!--EMPLOYEE  TYPE-->
   <table style="width: 90%; margin-top: 20px;" class="zebra-view">
                        <thead>
                            <tr>
                                <th colspan="4" align="left" style="padding: 10px;">EMPLOYEE TYPE</th>
                            </tr>
                        </thead>
                        
                        
                        <tbody>
                            <tr>
                                 <td >Employee Category: </td>
                                 <td >
                                  <asp:Textbox ID="txtEmployeeCategory" runat="server" Enabled="false" style="color: #000000; font-size: 100%"/>
                                 </td>
                                 <td >Contract Type: </td>
                                 <td >
                                      <asp:Textbox ID="txtContractType" runat="server" Enabled="false"/>
                                 </td>                         
                            </tr>
                            <asp:Panel runat="server" ID="pnlJobsInfo" >
                            <tr>
                                 <td >Job Family: </td>
                                 <td >
                                 
                                <asp:Textbox ID="txtJobFamily" runat="server" Enabled="false"/>
                                   </td>
                                <td >Job Description: </td>
                                 <td > <asp:Textbox ID="txtJobDescription" runat="server" Enabled="false"/>
                             </td>                       
                            </tr>       
                            </asp:Panel>                     
 
                              <tr>
                              <td >Job Title</td>
                              <td ><asp:Textbox ID="txtJobBusiTitle" runat="server" Enabled="false"/></td>
                                 <td >Maximum Salary ( Kč ): </td>
                                 <td >
                                 <asp:Textbox ID="txtMaximumSalary" runat="server" Enabled="false" />
                                       
                                </td>
                                                  
                            </tr>  

                              <tr> <td >Note About Additional Manpower</td>
                                 <td colspan="3" >
                                 <asp:Textbox ID="txtNoteManpower" runat="server"  CssClass="form-textarea" TextMode="MultiLine" Enabled="false" style=" border: 0px; color: #015664; font-size: 17px; width: 300px; padding: 10px; height: 50px; "/>
                                                               

                           </td> 
                                             
                            </tr>                                                                                                                                        
                      </tbody>
                    </table>
                    
                 <table style="width: 90%; margin-top: 20px;" class="zebra-view">
                        <thead>
                            <tr>
                                <th colspan="4" align="left" style="padding: 10px;">OTHER WORK INFORMATION</th>
                            </tr>
                        </thead>
                         
                        
                        <tbody>
                            <tr>
                            <td >Work Place:</td>                    
                            <td >
                             <asp:Textbox ID="txtWorkPlace" runat="server" CssClass="form-textarea"  TextMode="MultiLine" Enabled="false" style="resize: none; border: 0px; color: #015664; font-size: 17px; width: 290px; padding: 10px; height: 70px; "/>
                                 </td>
                            <td >Work Time</td>
                            <td >
                            <asp:Textbox ID="txtWorkTime" runat="server" Enabled="false" />
                                 </td>
                            </tr>
                          <tr>
                          
                           <td >Work Days </td>
                                 <td colspan="3" > 
                                        <asp:Textbox ID="txtMonday" runat="server" Enabled="false" style="width: 60px;" />
                                        <asp:Textbox ID="txtTuesday" runat="server" Enabled="false"  style="width: 60px;"/>
                                        <asp:Textbox ID="txtWednesday" runat="server" Enabled="false"  style="width: 90px;"/>
                                        <asp:Textbox ID="txtThursday" runat="server" Enabled="false"  style="width: 70px;"/>
                                        <asp:Textbox ID="txtFriday" runat="server" Enabled="false"  style="width: 50px;"/>
                                        <asp:Textbox ID="txtSaturday" runat="server" Enabled="false"  style="width: 70px;" />
                                        <asp:Textbox ID="txtSunday" runat="server" Enabled="false"  style="width: 60px;"/>
                                       
                                 </td> 
                          </tr>
                        <tr>
                            <td  > Work on Weekends?</td>
                            <td  ><asp:Textbox ID="txtWeekend" runat="server" Enabled="false" /> </td>
                            <td>Time Frame</td>
                            <td>
                             From: <asp:Textbox ID="txtFromTime" runat="server" Enabled="false"  style="width: 60px;"/>
                             To: <asp:Textbox ID="txtToTime" runat="server" Enabled="false"  style="width: 60px;"/>         
                            </td>
                            </tr>  
                       <tr>
                        <td >Note for Working Time: </td>
                        <td colspan="3"  >
                        <asp:Textbox ID="txtNoteWorkTime" runat="server"  CssClass="form-textarea" TextMode="MultiLine"
                          Enabled="false" style=" border: 0px; color: #015664; font-size: 17px; width: 600px; padding: 10px; height: 50px; "/></td>
                        
                      
                        </tbody></table>
            </ContentTemplate>
            </asp:UpdatePanel>                       
          <!--END OF "EMPLOYEE TYPE"-->
 	</div> 
 	<!--QUALIFICATION REQUIRED-->
        <table style="width: 90%; margin-top: 20px;" class="zebra-view">
                        <thead>
                            <tr>
                                <th colspan="4" align="left" style="padding: 10px;">Qualification Required</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th colspan="4"></th>
                            </tr>
                        </tfoot>
                        
                        <tbody>
                            <tr>
                                 <td >Language Skill 1: </td>
                                 <td  >
                                 <asp:Textbox ID="txtLanguage1" runat="server" Enabled="false" />
                                 </td>
                                 <td >Level: </td>
                                 <td  >
                                <asp:Textbox ID="txtLevel1" runat="server" Enabled="false" />
                                 </td>                         
                            </tr>
                            <tr>
                                 <td >Language Skill 2: </td>
                                 <td  >
                                 <asp:Textbox ID="txtLanguage2" runat="server" Enabled="false" />
                                 </td>
                                 <td >Level: </td>
                                 <td  >
                                 <asp:Textbox ID="txtLevel2" runat="server" Enabled="false" />
                                 </td>                         
                            </tr> 
                            <asp:Panel runat="server" ID="pnlEducationInfo">
                             <tr>
                                 <td >Education: </td>
                                 <td  >
                                 <asp:Textbox ID="txtEducation" runat="server" Enabled="false" />
                                   </td>
                                 <td >Major In: </td>
                                 <td  ><asp:Textbox ID="txtMajorIn" runat="server" Enabled="false" /></td>                         
                            </tr>  
                            <tr>
                             <td >Required Work Exp.: </td>
                            <td   ><asp:Textbox ID="txtRequiredWork" runat="server" Enabled="false" /></td>  
                             <td >&nbsp; </td>
                            <td  >&nbsp;</td>  
                            </tr>
                            </asp:Panel>  
                           <tr>
                                 <td >Other Remarks :</td>
                                  
                                 <td colspan="3"><asp:Textbox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-textarea"  Enabled="false" style=" border: 0px; color: #015664; font-size: 17px; width: 600px; padding: 10px; height: 50px; "/></td>
             
                                             
                            </tr>    
                            <tr>
                      <td ><asp:Label runat="server" ID="lblNoteExperience" Text="Note For Work Experience:" style="font-weight: bold; color: #111111"></asp:Label><asp:Label runat="server" ID="lblOtherRequirements" Text="Other Requirements:"   Css style="font-weight: bold; color: #111111"></asp:Label></td>
                                  
                  <td colspan="3"><asp:Textbox ID="txtNoteExperience" runat="server" TextMode="MultiLine" CssClass="form-textarea"  Enabled="false" style=" border: 0px; color: #015664; font-size: 17px; width: 600px; padding: 10px; height: 50px; "/><asp:Textbox ID="txtOtherRequirements" runat="server" TextMode="MultiLine" CssClass="form-textarea"  Enabled="false" style=" border: 0px; color: #015664; font-size: 17px; width: 300px; padding: 10px; height: 50px; "/></td>

                            </tr>                                                                                                                          
                      </tbody>
                    </table> 
                    <asp:HiddenField runat="server" ID="hiddenIsAgencyVal" />
<asp:Panel runat="server" ID="pnlApprovalActions">
<table style="width: 90%;"  align="center" cellpadding="5" cellspacing="5" class="no-zebra" >
<tr><td>Approver's Remark:</td></tr>
<tr><td align="center">
<asp:TextBox runat="server" ID="txtApproversRemarks" TextMode="MultiLine" Rows="8" Columns="120" style="padding: 20px; margin-top: 10px;"></asp:TextBox>
</td></tr>
<tr>  
<td align="right">
<asp:RadioButtonList runat="server" ID="radioSignOffButtons" style="display: inline; border-bottom: 0px;"
 RepeatDirection="Horizontal" Width="100px">
<asp:ListItem Value="A" Selected>Approve&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
<asp:ListItem Value="R">Rejected&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>  

</asp:RadioButtonList>

<asp:Button ID="btnSignOff" runat="server" OnClientClick="return confirmDialogLogs(this);" 
 OnClick="BtnSignOff_Click" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" 
Text="Sign Off" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  margin-left: 10px; display: inline" />
</td>
</tr>  
</table>
</asp:Panel>

<asp:Panel runat="server" ID="pnlApproverStatusMsg" Visible="false">
<table style="width: 90%;"  align="center" cellpadding="5" cellspacing="5" class="no-zebra" >
<tr><td><span style="paddin: 10px; color: Red"> This request/transaction number has been either approved or you are not an approver of this page. </span></td></tr>
</table>
</asp:Panel>

<table style="width: 90%; margin-top: 10px;"  align="center" cellpadding="0" cellspacing="0" >
<tr>  
<td align="right">

<asp:GridView ID="gridApprover" runat="server" AutoGenerateColumns = "false" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left"
 OnRowDataBound = "GridApprover_RowDataBound" style = "margin-top: 10px; width: 100%;" CssClass="no-zebra" DataKeyNames = "REQID" GridLines="None"> 
         
 <Columns> 
   <asp:BoundField ItemStyle-Width = "20%" DataField = "APPROVERDESC" 
       HeaderText = "Description"/> 
   <asp:BoundField ItemStyle-Width = "20%" DataField = "APPEMPLID"
       HeaderText = "Empl ID(s)"/>   
   <asp:BoundField ItemStyle-Width = "15%" DataField = "ACTUALAPPROVER"
       HeaderText = "Actual Approver"/>     
  <asp:BoundField ItemStyle-Width = "10%" DataField = "RESULT"
       HeaderText = "Status"/>    
  <asp:BoundField ItemStyle-Width = "15%" DataField = "DATESIGNED"
       HeaderText = "Date Signed"/>      
  <asp:BoundField ItemStyle-Width = "20%" DataField = "REMARKS"
       HeaderText = "Remarks"/>           
 </Columns>
 </asp:GridView> 
</td></tr></table> 

 
 
</asp:Content>
