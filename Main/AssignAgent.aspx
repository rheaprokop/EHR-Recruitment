<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AssignAgent.aspx.cs" Inherits="EHR.Main.AssignAgent" %>
<%@ MasterType VirtualPath="~/Views/Shared/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="mainContent" runat="server">

<table style="width: 80%; margin-top: 20px;" class="no-zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">SET AN AGENT</th>
                    </tr>
                </thead>
                
                <tbody>
                    <tr>
                         <td >Employee Name: <span style="color: red; display: inline;">*</span></td>
                         <td>
                         <asp:Textbox ID="txtEmplName" runat="server" Text="WCZ" Enabled="false" class="form-field" />
                         
                         </td>
                         <td class="form-label">Employee Dept ID: <span style="color: red; display: inline;">*</span></td>
                         <td> 
                             <asp:Textbox ID="txtEmployeeDeptID" runat="server" Text="WCZ" Enabled="false" style="margin-right: 10px; width: 130px;" class="form-field" />
                         </td>                         
                    </tr> 
                     <tr> 
                         <td class="form-label">
                        <asp:Label ID="lblEndDate" Text="End Date" runat="server" style="color: #111111" ></asp:Label>
                          </td>
                         <td> 
                         <asp:TextBox runat="server" id="txtStartDate" Enabled="false"  class="form-field" style="margin-right: 10px; width: 130px;"/>
                         <asp:Calendar SelectionMode="DayWeekMonth"
                                      id="calStartDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server"
                                      DayHeaderStyle-Font-Bold="false" 
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest"
                                      BorderWidth="1px"
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset" CellPadding="1" CssClass="calendar"
                                      OnSelectionChanged="CalStartDate_OnChanged">
                                      
                                
                              </asp:Calendar>
                             
                                     <asp:ImageButton ID="btnCalOnBoardDate" runat="server" OnClick="ViewOnBoardCal_OnClick" 
                                     ImageUrl="~/Content/css/light/images/calendar.gif"  CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                      
                        
                        </td>      
                        <td>End Date</td>   
                           <td> 
                         <asp:TextBox runat="server" id="txtEndBox" Enabled="false"  class="form-field" style="margin-right: 10px; width: 130px;"/>
                         <asp:Calendar SelectionMode="DayWeekMonth"
                                      id="calEndDate" 
                                      style="overflow: auto; position:absolute;top:100;left:250;display:block;z-index:0;"
                                      runat="server"
                                      DayHeaderStyle-Font-Bold="false" 
                                      FirstDayOfWeek="Monday" TodayDayStyle-BackColor="#CCCCCC" DayNameFormat="Shortest"

                                      BorderWidth="1px"
                                      BackColor="White"  
                                      ForeColor="Black" 
                                      Font-Names="Verdana" BorderColor="#999999"
                                      BorderStyle="Outset" CellPadding="1" CssClass="calendar"
                                      OnSelectionChanged="CalEndDate_Changed"> 
                        </asp:Calendar>
                             
                                     <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ViewCalEndDate_Click" 
                                     ImageUrl="~/Content/css/light/images/calendar.gif"  CausesValidation="false"
                                      style="width:16px; height:15px;"/>
                      
                        
                        </td>                
                    </tr>       
                    <tr>
                         <td >Agent Dept ID: <span style="color: red; display: inline;">*</span></td>
                         <td><asp:DropDownList runat="server" ID="ddlAgentDeptID" AutoPostBack="true" OnSelectedIndexChanged="DdlAgentDeptID_SelectedIndexChanged" style="width: 130px; padding: 5px;"></asp:DropDownList></td>
                         <td class="form-label">Set An Agent: <span style="color: red; display: inline;">*</span></td>
                         <td> 
                             <asp:DropDownlist runat="server" ID="ddlAgent" style="width: 150px; padding: 5px;"></asp:DropDownlist>
                         </td>                         
                    </tr>  
                    <tr>
                    <td >Remarks: <span style="color: red; display: inline;">*</span></td>
                    <td colspan = "3" style="padding: 10px;">
                    <asp:Textbox ID="txtRemarks"  class="form-field" runat="server" TextMode="MultiLine" CssClass="form-textarea" />
                    </td></tr>  
                    <tr><td colspan="4" align="right" style="padding-right: 100px">
                    <asp:Button runat="server" ID="btnSetAgent" Text="Set An Agent" OnClick="BtnInsertAgent_Click" CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;" /></td></tr>                                              
              </tbody>
            </table>
            
<asp:GridView ID="gridApprover" runat="server" AutoGenerateColumns = "false" HeaderStyle-Font-Bold="false" HeaderStyle-HorizontalAlign="Left"
 style = "margin-top: 10px; width: 100%;" CssClass="no-zebra" DataKeyNames = "REQID" GridLines="None"> 
 <Columns> 
</Columns>
</asp:GridView>
</asp:Content>
