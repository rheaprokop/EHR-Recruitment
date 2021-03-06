﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Training.aspx.cs" Inherits="EHR.Training" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
        
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">
  <!--TABS STARTS HERE-->
    <script>
	$(function() {
		$( "#tabs" ).tabs();
	});
	</script>
	
	<script type = "text/javascript">
	    function ConfirmOnBoard() {
	        var count = document.getElementById("<%=hfCount.ClientID %>").value;
	        var gv = document.getElementById("<%=gridToday.ClientID%>");
	        var chk = gv.getElementsByTagName("input");
	        for (var i = 0; i < chk.length; i++) {
	            if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
	                count++;
	            }
	        }
	        if (count == 0) {
	            alert("No records to delete.");
	            return false;
	        }
	        else {
	            return confirm("You are confirming attendance for " + count + " records.");
	        }
	    }
</script>
    <div id="tabs" style="font-size: 80%; width: 1200px; height: 600px;">
	    <ul>
		    <li><a href="#tabs-1"><asp:Label runat="server" ID="lblTodayDate"></asp:Label></a></li>
		    <li><a href="#tabs-2"><asp:Label runat="server" ID="lblSecondDay"></asp:Label></a></li>
		    <li><a href="#tabs-3"><asp:Label runat="server" ID="lblThirdDay"></asp:Label></a></li>
		    <li><a href="#tabs-4"><asp:Label runat="server" ID="lblFourthDay"></asp:Label></a></li>
		    <li><a href="#tabs-5"><asp:Label runat="server" ID="lblOthers" Text="Other Records"></asp:Label></a></li>
	    </ul>
	    
	    <div id="tabs-1">
	         
		    <asp:GridView ID="gridToday" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
                 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"   
                 DataKeyNames = "CandidateID" PageSize = "10" GridLines="None">
                    
                     
                       <Columns>  
                            <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" Visible="false" Enabled="true" 
                             /> 
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server"
                             />
                        </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:hyperlinkfield DataNavigateUrlFields="CandidateID"  ControlStyle-Width="60px" 
   DataNavigateUrlFormatString="~/Main/Training.aspx?action=1&candidate={0}"   
   DataTextField="CandidateID" HeaderText=""  DataTextFormatString= "On Board" ></asp:hyperlinkfield>
           <asp:hyperlinkfield DataNavigateUrlFields="CandidateID"  ControlStyle-Width="100px" 
   DataNavigateUrlFormatString="~/Main/Training.aspx?action=2&candidate={0}"   
   DataTextField="CandidateID" HeaderText=""  DataTextFormatString= "Did Not Attend" ></asp:hyperlinkfield>   
                     <asp:BoundField ItemStyle-Width = "150px" DataField = "EMPLID"
                       HeaderText = "Employee ID"/>       
                    <asp:BoundField ItemStyle-Width = "150px" DataField = "CandidateID"
                       HeaderText = "Candidate ID"/> 
                    <asp:BoundField ItemStyle-Width = "350px" DataField = "FULLNAME"
                       HeaderText = "Candidate Name"/> 
                   <asp:BoundField ItemStyle-Width = "250px" DataField = "EMAIL"
                       HeaderText = "Email Address"/>    
                       
                       <asp:BoundField ItemStyle-Width = "150px" DataField = "OnBOARDTIME"
                       HeaderText = "On Board Time"/> 
                       
                     </Columns> 
                </asp:GridView> 
                <asp:HiddenField ID="hfCount" runat="server" Value = "0" /> 
                <asp:Button ID="btnDelete" runat="server" Text="Confirm Attendance"
                    CssClass="ui-button ui-button-text-only ui-widget ui-state-default" style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px; margin-top: 20px;"

                   OnClientClick = "return ConfirmOnBoard();" OnClick="btnAttendance_Click" />
	    </div> 
	    <div id="tabs-2">
		      <asp:GridView ID="gridSecondDay" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
                 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"   
                 DataKeyNames = "CandidateID" PageSize = "10" GridLines="None"> 
              <Columns>  
              <asp:BoundField ItemStyle-Width = "150px" DataField = "EMPLID"
                       HeaderText = "Employee ID"/> 
                    <asp:BoundField ItemStyle-Width = "150px" DataField = "CandidateID"
                       HeaderText = "CandidateID"/> 
                    <asp:BoundField ItemStyle-Width = "350px" DataField = "FULLNAME"
                       HeaderText = "Candidate Name"/> 
                   <asp:BoundField ItemStyle-Width = "250px" DataField = "EMAIL"
                       HeaderText = "Email Address"/>     
                   <asp:BoundField ItemStyle-Width = "150px" DataField = "OnBOARDTIME"
                       HeaderText = "On Board Time"/>  
                     </Columns>  
                  </asp:GridView>    
	    </div> 	   
	    <div id="tabs-3">
       <asp:GridView ID="gridThirdDay" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
                 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"   
                 DataKeyNames = "CandidateID" PageSize = "10" GridLines="None"> 
              <Columns>  
              <asp:BoundField ItemStyle-Width = "150px" DataField = "EMPLID"
                       HeaderText = "Employee ID"/> 
                    <asp:BoundField ItemStyle-Width = "150px" DataField = "CandidateID"
                       HeaderText = "CandidateID"/> 
                    <asp:BoundField ItemStyle-Width = "350px" DataField = "FULLNAME"
                       HeaderText = "Candidate Name"/> 
                   <asp:BoundField ItemStyle-Width = "250px" DataField = "EMAIL"
                       HeaderText = "Email Address"/>     
                   <asp:BoundField ItemStyle-Width = "150px" DataField = "OnBOARDTIME"
                       HeaderText = "On Board Time"/>  
                     </Columns>  
                  </asp:GridView>    	    
	    </div> 
	    <div id="tabs-4">
       <asp:GridView ID="gridFourthDay" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
                 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"   
                 DataKeyNames = "CandidateID" PageSize = "10" GridLines="None"> 
              <Columns>  
              <asp:BoundField ItemStyle-Width = "150px" DataField = "EMPLID"
                       HeaderText = "Employee ID"/> 
                    <asp:BoundField ItemStyle-Width = "150px" DataField = "CandidateID"
                       HeaderText = "CandidateID"/> 
                    <asp:BoundField ItemStyle-Width = "350px" DataField = "FULLNAME"
                       HeaderText = "Candidate Name"/> 
                   <asp:BoundField ItemStyle-Width = "250px" DataField = "EMAIL"
                       HeaderText = "Email Address"/>     
                   <asp:BoundField ItemStyle-Width = "150px" DataField = "OnBOARDTIME"
                       HeaderText = "On Board Time"/>  
                     </Columns>  
                  </asp:GridView>    	    
	    </div>	    
	 <div id="tabs-5">
        <asp:GridView ID="gridOthers" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
                 AllowPaging ="true"  OnPageIndexChanging = "OnPaging"   
                 DataKeyNames = "CandidateID" PageSize = "10" GridLines="None"> 
              <Columns>  
              <asp:BoundField ItemStyle-Width = "150px" DataField = "EMPLID"
                       HeaderText = "Employee ID"/> 
                    <asp:BoundField ItemStyle-Width = "150px" DataField = "CandidateID"
                       HeaderText = "CandidateID"/> 
                    <asp:BoundField ItemStyle-Width = "350px" DataField = "FULLNAME"
                       HeaderText = "Candidate Name"/> 
                   <asp:BoundField ItemStyle-Width = "250px" DataField = "EMAIL"
                       HeaderText = "Email Address"/>                           
                   <asp:BoundField ItemStyle-Width="200px" DataField="OnBOARDDATE" 
                        HeaderText = "On Board Date" /> 
                   <asp:BoundField ItemStyle-Width = "150px" DataField = "OnBOARDTIME"
                       HeaderText = "On Board Time"/>  
                    <asp:hyperlinkfield DataNavigateUrlFields="CandidateID"  
                        ControlStyle-Width="60px" 
                   DataNavigateUrlFormatString="~/Main/UpdateOnBoardTime.aspx?candidate={0}"   
                   DataTextField="CandidateID" HeaderText="Update OnBoarding" 
                   DataTextFormatString= "<img src='../../Content/css/light/front/edit.png' border=0  alt='Update Record' style='boder:0' />" >
                   </asp:hyperlinkfield>                       
                     </Columns>  
                  </asp:GridView>    
     </div>
</asp:Content>
