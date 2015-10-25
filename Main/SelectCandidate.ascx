<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCandidate.ascx.cs" Inherits="EHR.Recruitment.SelectCandidate" %>
<table style="width: 100%; align: center;">
<tr><td style="align: left;">
            <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">CANDIDATE HIRING INFO</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th colspan="4"></th>
                    </tr>
                </tfoot>
            
                <tbody> 
                     <tr>
                         <td class="form-label" style="width: 200px;" >Request Form Number: </td>
                         <td>
                         <asp:TextBox ID="txtRequestNo" runat="server" CssClass="form-field" Enabled="false" style="width: 200px;"></asp:TextBox> 
                         </td>
                         <td class="form-label"  style="width: 200px;">  </td> 
                        <td>
                          &nbsp;
                        </td>
                     </tr>  
                     <tr>
                         <td class="form-label">Number of Candidates Required: </td>
                         <td>
                         <asp:TextBox ID="txtCandidateRequired" runat="server" CssClass="form-field"  Enabled="false" style="width: 200px;"></asp:TextBox> 
                         </td>
                         <td class="form-label" >Number of Candidates Left: </td> 
                        <td>
                         <asp:TextBox ID="TextBox2" runat="server"  Enabled="false" CssClass="form-field"  style="width: 200px;"></asp:TextBox> 
                        </td>
                     </tr>                       
                     </tbody></table> 
</td></tr>
<tr><td style="align: left;">
            <table style="width: 100%; margin-top: 20px; margin-left: 15px;" class="zebra">
                 <thead>
                    <tr>
                        <th colspan="4" align="left" style="padding: 10px; font-weight: normal;">SELECT A CANDIDATE</th>
                    </tr>
                </thead> 
            
                <tbody> 
                     <tr>
                         <td class="form-label" style="width: 200px;" >Enter a Candidate Details: </td>
                         <td   >
                         <asp:TextBox ID="txtCandidateKeyword" CssClass="form-field" runat="server"  Enabled="true"  value="i.e. Candidate ID, Name, Skills..."></asp:TextBox> 
                        </td>
                        <td colspan="2"> 
                       
                         <asp:Button ID="btnCandidateSearch" runat="server" Text="Search Candidate"
        CssClass="ui-button ui-button-text-only ui-widget ui-state-default ui-corner" OnClick="SearchCandidate_Click"
        style="font-family: BebasNeue; font-size: 16px; letter-spacing: 1px; float: left; "  
         />
                         </td>
                         
                     </tr>             
                     </tbody></table> 
</td></tr>
<tr><td>
<div class="header" style="margin-top: 50px;">
  <div>
   <asp:Label ID="lblCandidateTitle" Text="Select A Candidate" runat="server" CssClass="logotext"></asp:Label>
   </div>
   </div>
  <asp:Panel runat="server" ID="pnlError" style="border: solid 1px #c0221f; background: #ffbde5; width: 800px; padding: 20px; margin: 0 auto; margin-top: 30px;" >
                    <asp:Label runat="server" ID="lblError" style="font-size: 14px;"></asp:Label><br />
                    </asp:Panel>
<asp:GridView ID="gridCandidates" runat="server"   DataKeyNames="CandidateID" 
    AllowPaging="True" EnableViewState="False" CellPadding="0" CellSpacing="0" GridLines="None"   
     CssClass="grid-zebra" style="width: 1100px;"  PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-FirstPageImageUrl="first.png">
            <Columns>
                 
       <asp:hyperlinkfield DataNavigateUrlFields="CandidateID"    
   DataNavigateUrlFormatString="~/Recruitment/ViewCandidate.aspx?action=view&candidate={0}"  
   DataTextField="CandidateID" HeaderText=""  DataTextFormatString= "<img src='../App_Themes/front/view.png'  border=0 alt='View Record' style='boder:0' />" ></asp:hyperlinkfield>
 
  </Columns>
        </asp:GridView> 
</td></tr>
</table>
