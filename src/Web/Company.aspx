<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Company.aspx.vb" Inherits="Kondongpu.Company" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="lnr-apartment icon-gradient bg-primary"></i>
                </div>
                <div> Company<div class="page-title-subheading">รายการทะเบียนโรงงาน/บริษัท </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="main-card mb-3 card">
            <div class="card-header">
                <i class="header-icon lnr-apartment icon-gradient bg-success"></i>รายการทะเบียนโรงงาน/บริษัท
            <div class="btn-actions-pane-right">
                <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <=2 Then%>
                <a href="CompanyModify?m=c" class="btn btn-success pull-right"><i class="fa fa-plus-circle"></i>เพิ่มใหม่</a>
                <% End If %>
            </div>
            </div>
            <div class="card-body table-responsive">
                <table id="tbdata" class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center">รหัส</th>
                            <th class="text-left">ชื่อโรงงาน/บริษัท</th>
                            <th class="text-center">เจ้าของโควต้า</th>
                            <th class="text-center">เบอร์โทร</th>
                            <th class="text-center">ที่อยู่</th>
                            <th class="text-center">เลขผู้เสียภาษี</th>                         
                            <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <= 2 Then%>
                            <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                            <% End If %>
                        </tr>
                    </thead>
                    <tbody>
                        <% For Each row As DataRow In dtComp.Rows %>
                        <tr>
                            <td class="text-center"><% =String.Concat(row("Code")) %></td>
                            <td><a href="CompanyModify?m=c&cid=<% =String.Concat(row("UID")) %>"><% =String.Concat(row("CompanyName")) %> </a></td>
                            <td class="text-center"><% =String.Concat(row("OwnerName")) %></td>
                            <td class="text-center"><% =String.Concat(row("Telephone")) %></td>
                            <td class="text-left"><% =String.Concat(row("CompanyAddress")) %> </td>
                               <td class="text-center"><% =String.Concat(row("VATID")) %></td>
                            <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <= 2 Then%>
                            <td width="110" class="text-center">                               
                                <a href="CompanyModify?m=c&cid=<% =String.Concat(row("UID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดู/แก้ไข"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                                                              
                            </td>
                            <% End If %>
                        </tr>
                        <%  Next %>
                    </tbody>
                </table>
            </div>
        </div>


    </section>
</asp:Content>
