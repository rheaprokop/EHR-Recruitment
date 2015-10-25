<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="InterviewDetails.aspx.cs" Inherits="EHR.Main.InterviewDetails" %>
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
   <asp:Label ID="lblCandidateTitle" Text="INTERVIEW HISTORY"  
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
           <asp:Button ID="btnBackToProfile" runat="server"  Text="Back To Candidate Profile"  CausesValidation="false"
           CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;" />

    </div>
    </td></tr> 
    </table>
    </asp:Panel> 
    
    <strong>Candidate is qualified to the following jobs:</strong>
 
    <br />  <br />
  <asp:GridView ID="gridCandidatesAppJobs" runat="server" AutoGenerateColumns = "false"  CssClass="no-zebra" 
 AllowPaging ="true"    Width="40%" ShowHeader="false"  
 DataKeyNames = "CANDIDATEID" PageSize = "10" PagerSettings-LastPageImageUrl="~/Content/css/light/front/last.png" PagerSettings-NextPageImageUrl="~/Content/css/light/front/next.png" GridLines="None">
    
     
  <Columns> 
         <asp:BoundField ItemStyle-Width = "50%" DataField = "POSITION" HeaderText="Candidate is qualified to the following jobs:"    />
                <asp:BoundField ItemStyle-Width = "20%" DataField = "CANDIDATEID"  Visible="false"
       HeaderText = "CANDIDATE"/>
       
     </Columns> 
</asp:GridView> 

<br />  <br />
    <strong>Interview Details History:</strong>
 
    <br /> 

	 <table style="width: 95%;" >
		 <tr><td> 
		 
		 <asp:Repeater runat="server" ID="rptInterviewDetails"><ItemTemplate>
                    <table style="width: 100%; margin-top: 20px;" class="zebra"> 
                        <tbody>  
                    
                           <tr>
                               
                                 <td >Candidate Status: </td>
                                 <td>
                                 <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HIRINGSTATUS")%>' ID="TextBox1" ReadOnly="true" >
                                 
                                 </asp:TextBox>
                                 </td>        
                                   <td >Interview Date: </td>
                                 <td >
                                 <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INTERVIEWDATE")%>' ID="txtInterviewDate" ReadOnly="true" >
                                 
                                 </asp:TextBox> 
                                 </td>                              
                            </tr>                     
                            <tr>
                                 
                                 <td >Interviewed By: </td>
                                 <td >
                                 <asp:TextBox runat="server" ID="txtInterviewedBy" Text='<%# DataBinder.Eval(Container.DataItem, "INTERVIEWEDBY")%>' ReadOnly="true"></asp:TextBox>
                               
                                 </td>   
                                 <td>For Request ID: </td>   
                                 
                                  <td><asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "REQUESTID")%>' ID="txtForRequestID" ReadOnly="true"></asp:TextBox>
                               
                                 </td>                               
                            </tr>    
                            
                           <tr>
                                 <td >Possible Onboard Date: </td>
                                 <td >
                                 <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POSIONBOARDDATE")%>' ID="txtOnBoardDate" ReadOnly="true"> 
                                 </asp:TextBox> 
                                  </td>
                                 <td >Salary Expectation: </td>
                                 <td>
                                 <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SALARYEXP")%>' ID="txtSalaryExp" ReadOnly="true"></asp:TextBox>
                                 </td>                                      
                            </tr>     
                            <tr>
                            <td >For Department: </td>
                              <td >  
                               <asp:TextBox runat="server" ID="txtForDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "SUITABLEFORDEPT")%>' ReadOnly="true"></asp:TextBox>
                                     </td>
                                <td>
                                For Plant:  
                                </td>
                                <td>
                                <asp:TextBox runat="server" ID="txtForPlant" Text='<%# DataBinder.Eval(Container.DataItem, "PLANT")%>' ReadOnly="true"></asp:TextBox>
                                </tr>                              
                            <tr>
                                 <td >Interview Remarks </td>
                                 <td colspan="3">
                                 <asp:TextBox TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "REMARKS")%>'  runat="server" ID="txtForRemarks" ReadOnly="true" Rows="3" Columns="100"> 
                                 </asp:TextBox> 
                                  </td>
                                                                     
                            </tr>                         
                                  
                            </tbody>
                   </table>
              </ItemTemplate></asp:Repeater>
                    
     </td></tr>   
  </table>
</asp:Content>
