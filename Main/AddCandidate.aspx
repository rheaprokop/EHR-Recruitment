<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AddCandidate.aspx.cs" Inherits="EHR.Recruitment.AddCandidate1" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
 
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
<li><span style="padding-left: 30px;"><a href="AllRequests.aspx">Request Manager</span></a></li>
<li class="selected"><span style="padding-left: 30px;"><a href="Candidate.aspx?action=view">Candidate Manager</span></a></li>
 </asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">
  <div class="header">
  <div>
   <asp:Label ID="lblCandidateTitle" Text="Candidate Manager" runat="server" CssClass="logotext"></asp:Label>
   </div>
 <div style=" float: right; margin-top: -15px;" class="ui-buttonset">
 <asp:HyperLink runat="server" NavigateUrl="~/Main/Candidate.aspx?action=view"   ID="btnViewAll" Text="View All Candidates" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left; padding-right: 20px; padding-left: 20px;" />
 
 <asp:HyperLink  Enabled="false" runat="server" NavigateUrl="~/Main/AddCandidate.aspx" ID="btnAssign" Text="Create Profile" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" style="font-family: BebasNeue; font-size: 22px; letter-spacing: 1px; float: left;  padding-right: 20px; padding-left: 20px;" />
</div>
</div>

 <asp:Panel runat="server" ID="pnlCreateCand" style="margin-left: 50px; margin-right: 10px; margin-top: 10px;">
    <table style="width: 90%;" ><tr><td>
            <table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    <tr>
                         <th colspan="4" align="left">CANDIDATE INFORMATION</th>
                                               
                    </tr>
                </thead>
                
                
                <tbody>
                    <tr>
                         <td class="form-label">Candidate's First Name: <span style="color: red; display: inline;">*</span></td>
                         <td align="left"><asp:TextBox ID="txtFirstName" runat="server"  style="width: 180px" MaxLength="25"></asp:TextBox>
                         <asp:RequiredFieldValidator  runat="server" ID="reqtxtFirstName" ControlToValidate="txtFirstName"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                           
                          
                         </td>
                         <td class="form-label">Last Name: <span style="color: red; display: inline;">*</span></td>
                         <td align="left"><asp:TextBox ID="txtLastName" runat="server"  style="width: 180px" MaxLength="25"></asp:TextBox>
                          <asp:RequiredFieldValidator  runat="server" ID="reqtxtLastName" ControlToValidate="txtLastName"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                        
                         </td>                         
                    </tr>                  
                    
                     
                    </tbody>
           </table>
     </td></tr> 
  <tr><td>   
         <!--Start of Candidate Address-->
         <table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">CANDIDATE ADDRESS</th>
                    </tr>
                </thead>
               
                <tbody>
  
                     <tr>
                         <td class="form-label">Street: <span style="color: red; display: inline;">*</span></td>
                         <td align="left"><asp:Textbox ID="txtStreet" runat="server"  style="width: 180px" MaxLength="70"/>
                         <asp:RequiredFieldValidator runat="server" ID="reqtxtStreet" ControlToValidate="txtStreet" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                         </td>
                         <td class="form-label">City: <span style="color: red; display: inline;">*</span></td>
                         <td align="left">
                         <asp:DropDownList runat="server" ID="ddlCity"  style="width: 180px">
                             <asp:ListItem Value="">Select One</asp:ListItem>
                            <asp:ListItem Value="BRNO">Brno</asp:ListItem>
                            <asp:ListItem Value="CESK">Ceske Budejovice</asp:ListItem>
                            <asp:ListItem Value="CHOM">Chomutov</asp:ListItem>
                            <asp:ListItem Value="DECI">Decin</asp:ListItem>
                            <asp:ListItem Value="FRYD">Frydek-Mistek</asp:ListItem>
                            <asp:ListItem Value="HAV">Havirov</asp:ListItem>
                            <asp:ListItem Value="HRAD">Hradec Kralove</asp:ListItem>
                            <asp:ListItem Value="JIHL">Jihlava</asp:ListItem>
                            <asp:ListItem Value="KARL">Karlovy Vary</asp:ListItem>
                            <asp:ListItem Value="KARV">Karvina</asp:ListItem>
                            <asp:ListItem Value="KLAD">Kladno</asp:ListItem>
                            <asp:ListItem Value="LIBE">Liberec</asp:ListItem>
                            <asp:ListItem Value="MLAD">Mlada Boleslav</asp:ListItem>
                            <asp:ListItem Value="MOST">Most</asp:ListItem>
                            <asp:ListItem Value="OLOM">Olomouc</asp:ListItem>
                            <asp:ListItem Value="OPAV">Opava</asp:ListItem>
                            <asp:ListItem Value="OSTR">Ostrava</asp:ListItem>
                            <asp:ListItem Value="PARD">Pardubice</asp:ListItem>
                            <asp:ListItem Value="PLZE">Plzen</asp:ListItem>
                            <asp:ListItem Value="PRA">Praha</asp:ListItem>
                            <asp:ListItem Value="PRER">Prerov</asp:ListItem>
                            <asp:ListItem Value="TEPL">Teplice</asp:ListItem>
                            <asp:ListItem Value="USTI">Usti nad Labem</asp:ListItem>
                            <asp:ListItem Value="ZLIN">Zlin</asp:ListItem>
                            <asp:ListItem Value="OTH">Other</asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator runat="server" ID="reqddlCity" ControlToValidate="ddlCity" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                         </td>                         
                    </tr> 
                    <tr>
                    
                     <td class="form-label">Address 2: <span style="color: red; display: inline;">*</span></td>
                         <td align="left">
                        <asp:Textbox runat="server" ID="txtAddress2" style="width: 180px"></asp:Textbox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAddress2" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                         </td>
                         <td class="form-label">Nationality: <span style="color: red; display: inline;">*</span></td>
                         <td align="left"><asp:Textbox ID="txtNationality" runat="server" style="width: 180px"  MaxLength="5" />
                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtNationality" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                         </td>  
                    </tr>
                     <tr>
                         <td class="form-label">Citizenship: <span style="color: red; display: inline;">*</span></td>
                         <td align="left">
                         <asp:DropDownList runat="server" ID="ddlCountry"  style="width: 180px">
                             <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="reqddlCountry" ControlToValidate="ddlCountry" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                         </td>
                         <td class="form-label">Postcode/zip code: <span style="color: red; display: inline;">*</span></td>
                         <td align="left"><asp:Textbox ID="txtPostCode" runat="server" style="width: 180px"  MaxLength="5" />
                         <asp:RequiredFieldValidator runat="server" ID="reqtxtPostCode" ControlToValidate="txtPostCode" 
                         ErrorMessage="Required!"  cssClass="reqErrorMsg"  style="display:inline;"></asp:RequiredFieldValidator>
                         </td>                         
                    </tr>  
                   <tr>
                         <td class="form-label">Contact Number: </td>
                         <td align="left">(+420) <asp:Textbox ID="txtContact" runat="server" Width="150px" MaxLength="9"/></td>
                         <td class="form-label">Email Address: <span style="color: red; display: inline;">*</span></td>
                         <td align="left"><asp:Textbox ID="txtEmailAddress" runat="server" Width="180px" />
                         <asp:RegularExpressionValidator ID="reqtxtEmailAddressInv" runat="server" ControlToValidate="txtEmailAddress" 
                        ErrorMessage="Invalid Email!" SetFocusOnError="True"   cssClass="reqErrorMsg" style="display:inline;"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                          <asp:RequiredFieldValidator  runat="server" ID="reqtxtEmailAddress" ControlToValidate="txtEmailAddress"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator></td>                         
                    </tr>                                       
              </tbody>
            </table>
         <!-- End of Candidate Address-->   
</td></tr>
<tr><td>
 <asp:UpdatePanel ID="pnlCreateCandDesign" runat="server"  >
 <ContentTemplate>
        <table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">PERSONAL INFORMATION</th>
                    </tr>
                </thead>
               
                
                <tbody>
  
                     <tr>
                         <td class="form-label">Birthdate: <span style="color: red; display: inline;">*</span></td>
                         <td class="calendar">
                        <asp:DropDownList runat="server" ID="ddlDay" Width="100" >
                        <asp:ListItem Value="">Day</asp:ListItem>
                        <asp:ListItem Value="1">01</asp:ListItem>
                        <asp:ListItem Value="2">02</asp:ListItem>
                        <asp:ListItem Value="3">03</asp:ListItem>
                        <asp:ListItem Value="4">04</asp:ListItem>
                        <asp:ListItem Value="5">05</asp:ListItem>
                        <asp:ListItem Value="6">06</asp:ListItem>
                        <asp:ListItem Value="7">07</asp:ListItem>
                        <asp:ListItem Value="8">08</asp:ListItem>
                        <asp:ListItem Value="9">09</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="26">26</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="ddlMonth"  Width="100" >
                        <asp:ListItem Value="">Mon</asp:ListItem>
                        <asp:ListItem Value="01">01</asp:ListItem>
                        <asp:ListItem Value="02">02</asp:ListItem>
                        <asp:ListItem Value="03">03</asp:ListItem>
                        <asp:ListItem Value="04">04</asp:ListItem>
                        <asp:ListItem Value="05">05</asp:ListItem>
                        <asp:ListItem Value="06">06</asp:ListItem>
                        <asp:ListItem Value="07">07</asp:ListItem>
                        <asp:ListItem Value="08">08</asp:ListItem>
                        <asp:ListItem Value="09">09</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem></asp:DropDownList>&nbsp;&nbsp;
                        <asp:Textbox ID="txtYear" runat="server"  Width="80" />  
                           </td>
                         <td>Sex: <span style="color: red; display: inline;">*</span> </td>
                         <td>
                         <asp:DropDownList runat="server" ID="ddlSex" Width="180px">
                         <asp:ListItem Value="">Select One</asp:ListItem>
                         <asp:ListItem Value="M">Male</asp:ListItem>
                         <asp:ListItem Value="F">Female</asp:ListItem>
                         </asp:DropDownList>
                          <asp:RequiredFieldValidator  runat="server" ID="reqddlSex" ControlToValidate="ddlSex"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>
                         </td>                         
                    </tr> 
                      <tr>
                         <td class="form-label">Status: </td>
                         <td>
                         <asp:DropDownList runat="server" ID="ddlStatus">
                         <asp:ListItem Value="">Select One</asp:ListItem>
                         <asp:ListItem Value="S">Single</asp:ListItem>
                         <asp:ListItem Value="M">Married</asp:ListItem>
                         <asp:ListItem Value="L">Married With Children</asp:ListItem> 
                         <asp:ListItem Value="C">Cohabited</asp:ListItem>
                         <asp:ListItem Value="E">Separated</asp:ListItem> 
                         <asp:ListItem Value="W">Widowed</asp:ListItem> 
                         </asp:DropDownList>
                         </td>
                         <td colspan="2">  <asp:RequiredFieldValidator  runat="server" ID="reqddlDay" ControlToValidate="ddlDay"
                          ErrorMessage="Day is required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>&nbsp;  
                           <asp:RequiredFieldValidator  runat="server" ID="reqddlMonth" ControlToValidate="ddlMonth"
                          ErrorMessage="Month is required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator> &nbsp;
                          <asp:RequiredFieldValidator  runat="server" ID="reqtxtYear" ControlToValidate="txtYear"
                          ErrorMessage="Year is required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>  
                          <asp:RangeValidator runat="server" ControlToValidate="txtYear"  MinimumValue="1950" MaximumValue="2050"
                          Type="Integer"     ErrorMessage="Year must be greater than 1950."

                           ></asp:RangeValidator>
     
                      </td>
                                            
                    </tr> 
                    <tr>
                    <td class="form-label">Birth Number: </td>
                         <td>
                         <asp:Textbox ID="txtBirthNumber" runat="server" width="180px"   />
                         </td>  
                         <td colspan="2"></td>
                    </tr>                   
           
           </table>
           </ContentTemplate></asp:UpdatePanel>
</td></tr>
<tr><td>

        <table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">EDUCATION INFORMATION</th>
                    </tr>
                </thead> 
                <tbody>
   <tr>
                       <td>Academic Title: </td>
                         <td>
                         
                         <asp:DropDownList runat="server" ID="ddlTitle"  width="180px" >
                         <asp:ListItem Value="">Select One</asp:ListItem>
                         <asp:ListItem Value="Bakalář (Bc.)">Bakalář (Bc.)</asp:ListItem> 
                         <asp:ListItem Value="Inženýr (Ing.)">Inženýr (Ing.)</asp:ListItem>
                         <asp:ListItem Value="Magistr (Mgr.)">Magistr (Mgr.)</asp:ListItem>
                         <asp:ListItem Value="Doktor (Ph.D.)">Doktor (Ph.D.)</asp:ListItem>
                         </asp:DropDownList>
                         </td>   
                         <td class="form-label">Highest Educ Level: <span style="color: red; display: inline;">*</span> </td>
                         <td>
                           <asp:DropDownList runat="server" ID="ddlHighestEducLevel"  width="180px" >
                        
                         <asp:ListItem Value="">Select One</asp:ListItem>
                         <asp:ListItem Value="B">Less Than HS Graduate</asp:ListItem>
                         <asp:ListItem Value="C">HS Graduate or Equivalent</asp:ListItem>
                         <asp:ListItem Value="D">Some College</asp:ListItem>
                         <asp:ListItem Value="E">Technical School</asp:ListItem>
                         <asp:ListItem Value="F">2-Year College Degree</asp:ListItem>
                         <asp:ListItem Value="G">Bachelor Level Degree</asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator  runat="server" ID="reqddlHighestEducLevel" ControlToValidate="ddlHighestEducLevel"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>  
                         </td>                         
                    </tr>  
                     <tr>
                         <td class="form-label">Degree: <span style="color: red; display: inline;">*</span></td>
                         <td>
                        <asp:DropDownList runat="server" ID="ddlGraduate"  width="180px" >
                                 <asp:ListItem Value="">Select One</asp:ListItem>
                               <asp:ListItem Value="A">Ph.D</asp:ListItem>
                               <asp:ListItem Value="B">Master</asp:ListItem>                                  
                               <asp:ListItem Value="C">Bachelor</asp:ListItem>
                               <asp:ListItem Value="D">Technician</asp:ListItem>
                              <asp:ListItem Value="F">Junior High School</asp:ListItem>
                              <asp:ListItem Value="G">Elementary</asp:ListItem>
                              <asp:ListItem Value="E">Senior High School</asp:ListItem>
                              <asp:ListItem Value="H">Illiterate</asp:ListItem>
                              <asp:ListItem Value="I">Others</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator  runat="server" ID="reqddlGraduate" ControlToValidate="ddlGraduate"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>  
                       
                         </td>
                         <td class="form-label">Field: </td>
                         <td>
                         <asp:Textbox ID="txtField" runat="server" style="size: 30;"  width="180px"   />
                         </td>                         
                    </tr>  
                                       
 <tr>
                                 <td class="form-label">Language Skill 1: <span style="color: red; display: inline;">*</span></td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlLanguage1"  width="180px" >
                                 <asp:ListItem Value="" Selected="True">None</asp:ListItem>
                                 <asp:ListItem Value="EN">English</asp:ListItem> 
                                 <asp:ListItem Value="CZ">Czech</asp:ListItem> 
                                 <asp:ListItem Value="KO">Korean</asp:ListItem>
                                 <asp:ListItem Value="SP">Spanish</asp:ListItem> 
                                 <asp:ListItem Value="VI">Vietnam</asp:ListItem>
                                 <asp:ListItem Value="GE">German</asp:ListItem> 
                                 <asp:ListItem Value="JA">Japanese</asp:ListItem> 
                                 <asp:ListItem Value="OT">Other</asp:ListItem> 
                                 </asp:DropDownList>
                                 <asp:RequiredFieldValidator  runat="server" ID="reqddlLanguage1" ControlToValidate="ddlLanguage1"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>  
                                 </td>
                                 <td class="form-label">Level: <span style="color: red; display: inline;">*</span></td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlSkill1"  width="180px" >
                                 <asp:ListItem Value="" Selected="True">Select Level</asp:ListItem>
                                 <asp:ListItem Value="Beginner">Beginner</asp:ListItem>
                                 <asp:ListItem Value="Intermediate">Intermediate</asp:ListItem>
                                   <asp:ListItem Value="Advanced">Advanced</asp:ListItem>
                                 </asp:DropDownList>
                                 <asp:RequiredFieldValidator  runat="server" ID="reqddlSkill1" ControlToValidate="ddlSkill1"
                          ErrorMessage="Required!"  cssClass="reqErrorMsg" style="display: inline;"></asp:RequiredFieldValidator>  
                                 </td>                         
                            </tr>                   
 <tr>
                                 <td class="form-label">Language Skill 2: </td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlLanguage2"  width="180px" >
                                 <asp:ListItem Value="" Selected="True">None</asp:ListItem>
                                 <asp:ListItem Value="EN">English</asp:ListItem> 
                                 <asp:ListItem Value="CZ">Czech</asp:ListItem> 
                                 <asp:ListItem Value="KO">Korean</asp:ListItem>
                                 <asp:ListItem Value="SP">Spanish</asp:ListItem> 
                                 <asp:ListItem Value="VI">Vietnam</asp:ListItem>
                                 <asp:ListItem Value="GE">German</asp:ListItem> 
                                 <asp:ListItem Value="JA">Japanese</asp:ListItem> 
                                 <asp:ListItem Value="OT">Other</asp:ListItem> 
                                 </asp:DropDownList>
                                 </td>
                                 <td class="form-label">Level: </td>
                                 <td>
                                 <asp:DropDownList runat="server" ID="ddlSkill2"  width="180px" >
                                 <asp:ListItem Value="" Selected="True">Select Level</asp:ListItem>
                                 <asp:ListItem Value="Beginner">Beginner</asp:ListItem>
                                 <asp:ListItem Value="Intermediate">Intermediate</asp:ListItem>
                                   <asp:ListItem Value="Advanced">Advanced</asp:ListItem>
                                 </asp:DropDownList>
                                 </td>                         
                            </tr>
                    
                    
                    
                    </table>
</td></tr>
<tr><td>

<table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">PROFESSIONAL EXPERIENCE</th>
                    </tr>
                </thead> 
                <tbody>
                   <tr>
                         <td class="form-label">Professional Experience: </td>
                         <td>
                            <asp:DropDownList runat="server" ID="ddlExperience"   width="180px">
                                 <asp:ListItem Value="" Selected="True">Select One</asp:ListItem>
                                 <asp:ListItem Value="Less than 6 months">Less than 6 months</asp:ListItem>
                                 <asp:ListItem Value="1 Year">1 Year</asp:ListItem>  
                                 <asp:ListItem Value="2 Years">2 Years</asp:ListItem>  
                                 <asp:ListItem Value="3 Years">3 Years</asp:ListItem>  
                                 <asp:ListItem Value="4 Years">4 Years</asp:ListItem>  
                                 <asp:ListItem Value="5 Years">5 Years</asp:ListItem>  
                                 <asp:ListItem Value="6 Years">6 Years</asp:ListItem>  
                                 <asp:ListItem Value="7 Years">7 Years</asp:ListItem>  
                                 <asp:ListItem Value="8 Years">8 Years</asp:ListItem>  
                                 <asp:ListItem Value="9 Years">9 Years</asp:ListItem>  
                                 <asp:ListItem Value="10 Years">10 Years</asp:ListItem>  
                                 <asp:ListItem Value="More than 10 Years">More than 10 Years</asp:ListItem>  
                                  </asp:DropDownList>
                       
                         </td>
                         <td class="form-label">Field of expertise: </td>
                         <td>
                         <asp:Textbox ID="txtExpertise" runat="server"   width="180px"   />
                         </td>                         
                    </tr> 
                     <tr>
                         <td class="form-label">Current Employer: </td>
                         <td>
                        <asp:Textbox ID="txtCurrentEmpl" runat="server"   width="180px"   />
                       
                         </td>
                         <td class="form-label">Current Position: </td>
                         <td>
                         <asp:Textbox ID="txtCurrentPosi" runat="server"   width="180px"   />
                         </td>                         
                    </tr> 
 </table>
</td></tr>
<tr><td>
<table style="width: 100%; margin-top: 20px;" class="no-zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">OTHER INFORMATION</th>
                    </tr>
                </thead>  
                    <tr>
                          
                           <td class="form-label">Driving License: </td>
                           <td  class="form-check"> 
                                 <asp:DropDownList runat="server" ID="ddlDrivingLicense"
                                  AutoPostBack="true" OnSelectedIndexChanged="DdlDrivingLicense_OnChange"
                                  width="180px">
                                 <asp:ListItem Value="">Select One</asp:ListItem>
                                 <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                 <asp:ListItem Value="No">No</asp:ListItem>
                                 </asp:DropDownList>
                                 
                           </td> 
                           <td ><asp:Label runat="server" ID="lblVehicleChk" Text="Which Vehicle:" ></asp:Label></td>
                           <td >
                           
                                 <asp:CheckBoxList ID="chkVehicle" style="border: 0px;"
                                 RepeatDirection="Horizontal" CssClass="no-zebra"
                                  runat="server" >
                                 <asp:ListItem>Car</asp:ListItem>
                                 <asp:ListItem>Forklift</asp:ListItem>
                                  <asp:ListItem>Others</asp:ListItem>
                                 
                                 </asp:CheckBoxList>
                                 </td> 
                          </tr>       
                    </table>
</td></tr>

<tr><td>
<table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    
                </thead>  
                    <tr>
                          
                           <td class="form-label">Remarks: </td>
                           <td> 
                                 <asp:Textbox ID="txtRemarks"  class="form-field" runat="server" TextMode="MultiLine" CssClass="form-textarea" />
                                 
                           </td>  
                          </tr>       
                    </table>
</td></tr>
<tr><td>
<!--Start of Candidate CV-->
         <table style="width: 100%; margin-top: 20px;" class="zebra">
                <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px;">Upload Documents</th>
                    </tr>
                </thead> 
                
                <tbody> 
                     <tr>
                       <td class="form-label">Upload Czech CV: </td>
                         <td>
                         <asp:FileUpload ID="fileCzechCV" runat="server" />
                       </td>
                         <td class="form-label">Upload English CV: </td>
                         <td >
                         <asp:FileUpload ID="fileEnglishCV" runat="server" />
                         </td>
                                              
                    </tr>  
        </table>                                  

</td></tr>
 
<tr><td>       
 </td></tr>
 <tr><td>
 <asp:Button ID="btnSaveAndAdd"  runat="server" text="Save & Add Another" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: right;" />
 <asp:Button ID="btnSave" OnClick="IsSave"  runat="server" text="Save Candidate" CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner-all" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: right;" />

  </td></tr>
</table></asp:Panel>

</asp:Content>
