<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="UpdateOnBoardTime.aspx.cs" Inherits="EHR.Main.UpdateOnBoardTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">
<asp:UpdatePanel runat="server" ID="pnlHire"><ContentTemplate>                  
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
                         <asp:HiddenField runat="server" ID="hiddenCandidateID" /> 
                         </td>
                          <td class="form-label" style="width: 200px;" > For Request ID: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:TextBox runat="server" ID="txtRequestID" ReadOnly="true"></asp:TextBox>
                          
                         </td> 
                     </tr>  
                     <tr>
                     <td class="form-label" style="width: 200px;" >New On Board Date: <span style="color: red; display: inline;">*</span></td>
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
                         <asp:TextBox runat="server" ID="txtDeptID" ReadOnly="true"></asp:TextBox>
                         </td> 
                        
                     </tr>   
                     <tr>
                     <td>
                      Set New Time:  <span style="color: red; display: inline;">*</span>
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
                      <td>Address: <span style="color: red; display: inline;">*</span></td><td> 
                         <asp:TextBox runat="server" ID="txtWorkPlace" ReadOnly="true"></asp:TextBox>
                          
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
                         <asp:TextBox runat="server" ID="txtjobposition" ReadOnly="true"></asp:TextBox> 
                             </td>
                        <td class="form-label" style="width: 200px;" > </td>
                         <td>
                          
                         </td>
                     </tr>  
                     <tr>
                         <td class="form-label" style="width: 200px;" >Offer Salary ( Kč ): <span style="color: red; display: inline;">*</span> </td>
                         <td>
                         <asp:TextBox ID="txtSalary" runat="server" CssClass="form-field"   style="width: 200px;" Text="--- Hidden ---"></asp:TextBox> 
                            
                         </td>
                        <td class="form-label" style="width: 200px;" >Salary After Probation  ( Kč ): <span style="color: red; display: inline;">*</span> </td>
                         <td>
                         <asp:TextBox ID="txtSalaryAfter" runat="server"  CssClass="form-field"  style="width: 200px;" Text="--- Hidden ---"></asp:TextBox> 
                            </td>
                     </tr>  
                         
                    </table> 
</td></tr> 

<tr><td align="right">
<br />
     <asp:Button ID="btnUpdateOnBoardTime" runat="server"  Text="UPDATE ON BOARD DATE" OnClick="BtnOnBoardTime_Click"     
      CssClass="ui-button ui-button-text-only ui-widget ui-state-default " 
      style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; padding-right: 20px; 
                padding-left: 20px; padding-bottom: 10px; display: inline;" />
                
     <asp:Button ID="btnBackToOnBoard" runat="server"  Text="BACK TO PROFILE"  OnClick = "BtnBackToOnBoard_Click"
      CssClass="ui-button ui-button-text-only ui-widget ui-state-default "  CausesValidation="false"
      style="font-family: BebasNeue; font-size: 20px; letter-spacing: 1px; padding-right: 20px; 
                padding-left: 20px; padding-bottom: 10px; display: inline;" />
                           
                
<br />

</td></tr>
</tbody>
</table></ContentTemplate></asp:UpdatePanel>  
</asp:Content>
