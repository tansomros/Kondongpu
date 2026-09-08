<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentViewer.aspx.vb" Inherits="Kondongpu.DocumentViewer" %>
<%@ Register Assembly="DevExpress.XtraReports.v23.2.Web.WebForms, Version=23.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section class="content"> 
<table width="100%" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td align="center">
        <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server"></dx:ASPxWebDocumentViewer>
      </td>
  </tr>   
</table> 
    </section>     
</asp:Content>
