<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Inbox.aspx.cs" Inherits="EHR.Views.Shared.Inbox" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server">

<asp:Panel ID="pnlMainPage" runat="server" CssClass="pnlWelcome" style="margin-top: 5px;">
 <div class="header"> 

<asp:Label ID="lblCandidateTitle" Text="Viewing Request For Approval" runat="server" CssClass="logotext"></asp:Label>
    </div> 
 </asp:Panel>  
 
 
 <!--TABS STARTS HERE-->
    	<script>
    	    $(function() {
    	        $("#tabs").tabs();
    	    });

    	    $(function() {
    	        $("#tabs-pic").tabs();
    	    });
	</script>
    <div id="tabs" style="font-size: 80%; width: 100%; height: 900px; margin-top: 50px;">
	    <ul>
		     
		    <li><a href="#tabs-1">For Approval</a></li>
		    <li><a href="#tabs-2">Approved Forms</a></li>
		     <li><a href="#tabs-3">Rejected Forms</a></li> 
	    </ul>
	    
	    <div id="tabs-1">
	    <asp:UpdatePanel  runat="server"><ContentTemplate>
	        <table><tr><td><asp:Label runat="server" ID="lblWaiting" Visible="false" 
	        style="float: left; margin-top: 20px; color: Red; display: block;"></asp:Label></td></tr>
	        <tr><td><asp:GridView ID="gridWaiting" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
             AllowPaging ="true"  OnPageIndexChanging = "OnPagingWaiting"  OnRowDataBound="GridWaiting_RowDataBound"
             DataKeyNames = "REQID" PageSize = "10" GridLines="None" Width="1000px" style="align:left; display:block;"
              PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
             
                   <Columns>  
                         <asp:hyperlinkfield DataNavigateUrlFields="REQID"   ItemStyle-Width = "50px"
               DataNavigateUrlFormatString="~/Views/Shared/Inbox.aspx?id={0}" ControlStyle-BorderStyle="none"  
               DataTextField="REQID" HeaderText=""   
               DataTextFormatString= "<img src='/Content/css/light/images/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
                             <asp:BoundField ItemStyle-Width = "250px" DataField = "REQID"
                       HeaderText = "Request ID"/>    
               <asp:BoundField ItemStyle-Width = "300px" DataField = "REQUESTOR"
                       HeaderText = "Requestor"/>    
               <asp:BoundField ItemStyle-Width = "300px" DataField = "NAME_A"
                       HeaderText = "Requestor Name"/>                          
               <asp:BoundField ItemStyle-Width = "250px" DataField = "DEPTID"
                       HeaderText = "Dept ID"/>      
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQDATE"
                       HeaderText = "Requested Date"/>    
                
                       </Columns> 
                 
                  <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
                    NextPageImageUrl="~/Content/css/light/images/next.png" 
                    PreviousPageImageUrl="~/Content/css/light/images/previous.png"  FirstPageImageUrl="~/Content/css/light/images/first.png"   />
            </asp:GridView>   </td></tr>
	        
	        </table>
	         
        </ContentTemplate></asp:UpdatePanel>
		    
	    </div> 
	    <div id="tabs-2">
	    <asp:UpdatePanel runat="server"><ContentTemplate>
	    <asp:Label runat="server" ID="lblApproved" Visible="false" style="margin-top: 50px; color: Red;"></asp:Label>
	         <asp:GridView ID="gridApproved" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
             AllowPaging ="true"  OnPageIndexChanging = "OnPagingApproved" 
             
             DataKeyNames = "TRANSID" PageSize = "10" GridLines="None" Width="1000px"
              PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
             
                   <Columns>  
                        <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAllApproved" runat="server" Enabled="true"  
                         AutoPostBack="true" OnSelectedIndexChanged="chkAllApproved_OnSelectedChanged"
                         />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkApproved" runat="server" 
                         />
                    </ItemTemplate> 
                </asp:TemplateField>
                   <asp:hyperlinkfield DataNavigateUrlFields="REQID"   ItemStyle-Width = "50px"
               DataNavigateUrlFormatString="~/Views/Shared/Inbox.aspx?id={0}" ControlStyle-BorderStyle="none"  
               DataTextField="REQID" HeaderText=""   
               DataTextFormatString= "<img src='/Content/css/light/images/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
                
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQID"
                       HeaderText = "Request ID"/>    
               <asp:BoundField ItemStyle-Width = "250px" DataField = "TRANSID"
                       HeaderText = "Trans ID"/>                        
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQSTRID"
                       HeaderText = "Requestor"/>    
               <asp:BoundField ItemStyle-Width = "250px" DataField = "DEPTID"
                       HeaderText = "Dept ID"/>    
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQDATE"
                       HeaderText = "Requested Date"/>    
                
                       </Columns> 
                 
                  <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
                    NextPageImageUrl="~/Content/css/light/images/next.png" 
                    PreviousPageImageUrl="~/Content/css/light/images/previous.png"  FirstPageImageUrl="~/Content/css/light/images/first.png"   />
            </asp:GridView> 
             <asp:HiddenField ID="hfCountApproved" runat="server" Value = "0" /><br />
            <asp:Button ID="btnApprove_Click" runat="server" Text="Delete Checked Records"
              OnClick="btnApproved_DeleteApproved"
                class="mws-button red" style="font-family: Bebas Neue; font-size: 12px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;"
                 />
		    </ContentTemplate></asp:UpdatePanel>
	    </div> 	 
        <div id="tabs-3">
        <asp:UpdatePanel runat="server"><ContentTemplate>
        <asp:Label runat="server" ID="lblRejected" Visible="false" style="margin-top: 50px; color: Red;"></asp:Label>
	         <asp:GridView ID="gridRejected" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra" 
             AllowPaging ="true"  OnPageIndexChanging = "OnPagingRejected" 
             DataKeyNames = "TRANSID" PageSize = "10" GridLines="None" Width="1000px"
              PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
             
                   <Columns>  
                        <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAllRejected" runat="server" Enabled="true"  
                         />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRejected" runat="server" 
                         />
                    </ItemTemplate> 
                </asp:TemplateField>
                    <asp:hyperlinkfield DataNavigateUrlFields="REQID"   ItemStyle-Width = "50px"
               DataNavigateUrlFormatString="~/Views/Shared/Inbox.aspx?id={0}" ControlStyle-BorderStyle="none"  
               DataTextField="REQID" HeaderText=""   
               DataTextFormatString= "<img src='/Content/css/light/images/view.png' border=0  alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
                
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQID"
                       HeaderText = "Request ID"/>  
               <asp:BoundField ItemStyle-Width = "250px" DataField = "TRANSID"
                       HeaderText = "Trans ID"/>                           
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQSTRID"
                       HeaderText = "Requestor"/>    
               <asp:BoundField ItemStyle-Width = "250px" DataField = "DEPTID"
                       HeaderText = "Dept ID"/>    
               <asp:BoundField ItemStyle-Width = "250px" DataField = "EMPLOYEE"
                       HeaderText = "Employee"/>   
               <asp:BoundField ItemStyle-Width = "250px" DataField = "REQDATE"
                       HeaderText = "Requested Date"/>     
                       </Columns>  
                  <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" 
                    NextPageImageUrl="~/Content/css/light/images/next.png" 
                    PreviousPageImageUrl="~/Content/css/light/images/previous.png"  FirstPageImageUrl="~/Content/css/light/images/first.png"   />
            </asp:GridView> 
             <asp:HiddenField ID="hfCountRejected" runat="server" Value = "0" /><br />
            <asp:Button ID="btnReject_Click" runat="server" Text="Delete Checked Records"
                OnClick="btnRejected_DeleteRejected"
                class="mws-button green" style="font-family: Bebas Neue; font-size: 12px; letter-spacing: 1px;  padding-right: 20px; padding-left: 20px;"
                 />
		    </ContentTemplate></asp:UpdatePanel>
	    </div>
	      
     </div>
   
 
     
       
<Script type="text/javascript">
    $(function() {
    $("#ctl00_mainContent_pnlDialog").dialog({
            height: 230,
            width: 430,
            modal: true
        });
    });
</script>
	<asp:Panel runat="server" ID="pnlDialog"  CssClass="mws-jui-dialog" style="display:none">
        <asp:Label runat="server" ID="lblErrorMsg"></asp:Label> 
                         <br /><br />
       <a href="../Shared/Inbox.aspx"><span style="float: right" class="mws-button green">
        
        Close Box</span></a>
        </asp:Panel>

<asp:Panel runat="server" ID="pnlPIC">
<asp:GridView ID="gridPrintPIC" runat="server" AutoGenerateColumns = "false"  CssClass="grid-zebra"  
             DataKeyNames = "REQUESTID" PageSize = "10" GridLines="None" Width="1000px"
              PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" 
              PagerSettings-FirstPageImageUrl="first.png">
 </asp:GridView>
</asp:Panel>
  
</asp:Content>
