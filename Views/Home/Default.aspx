﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EHR.Views.Home.Default" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="top" ContentPlaceHolderID="TopContent" runat="server">
</asp:Content>
<asp:Content ID="left" ContentPlaceHolderID="LeftContent" runat="server">
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="mainContent" runat="server"> 
  <br />
    <table style="width: 350px">
     <tr><td colspan="3"><span class="logotext" style="display: block">WHAT WOULD YOU LIKE TO DO</span></td></tr>
    <tr>
    <td><asp:ImageButton runat="server" ID="imgCreateReq" ImageUrl="~/Content/css/light/images/status.png" PostBackUrl="~/Views/Status/RQ01.aspx" />&nbsp;</td>
    
    <td><asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Content/css/light/images/recruit.png" PostBackUrl="~/Main/Main.aspx" />&nbsp;</td>
    <td><asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/Content/css/light/images/terminate.png" PostBackUrl="~/Views/Termination/Home.aspx" />&nbsp;</td>
     
     </tr>
  </table>
</asp:Content>
