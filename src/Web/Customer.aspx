<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Customer.aspx.vb" Inherits="Kondongpu.Customer" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-users icon-gradient bg-primary"></i>
                </div>
                <div> Customer<div class="page-title-subheading">รายการทะเบียนข้อมูลลูกค้า </div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="main-card mb-3 card">
            <div class="card-header">
                <i class="header-icon fa fa-users icon-gradient bg-success"></i>รายการทะเบียนข้อมูลลูกค้า
            <div class="btn-actions-pane-right">
                <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <=2 Then%>
                <a href="CustomerModify?m=c" class="btn btn-success pull-right"><i class="fa fa-plus-circle"></i>เพิ่มลูกค้าใหม่</a>
                <% End If %>
            </div>
            </div>
            <div class="card-body table-responsive">
                <table id="tbdata" class="table table-bordered">
                    <thead>
                        <tr>
                            <th class="text-center">รหัสลูกค้า</th>
                            <th class="text-left">ชื่อลูกค้า</th>
                            <th class="text-center">ชื่อเล่น</th>
                            <th class="text-center">อายุ</th>
                            <th class="text-center">ที่อยู่</th>
                            <th class="text-center">เลขบัตรประชาชน</th>                         
                            <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <= 2 Then%>
                            <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                            <% End If %>
                        </tr>
                    </thead>
                    <tbody>
                        <% For Each row As DataRow In dtCus.Rows %>
                        <tr>
                            <td class="text-center"><% =String.Concat(row("CustomerID")) %></td>
                            <td><a href="CustomerModify?m=c&cid=<% =String.Concat(row("UID")) %>"><% =String.Concat(row("CustomerName")) %> </a></td>
                            <td class="text-center"><% =String.Concat(row("NickName")) %></td>
                            <td class="text-center"><% =String.Concat(row("Age")) %></td>
                            <td class="text-left"><% =String.Concat(row("CustomerAddress")) %> </td>
                               <td class="text-center"><% =String.Concat(row("CardID")) %></td>
                            <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <= 2 Then%>
                            <td width="110" class="text-center">                               
                                <a href="CustomerModify?m=c&cid=<% =String.Concat(row("UID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไขข้อมูลลูกค้า"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                <a href="LoanNew?m=l&cid=<% =String.Concat(row("UID")) %>" class="btn btn-success" data-toggle="tooltip" data-placement="top" data-original-title="บันทึกให้กู้"><i class="fa fa-coins" aria-hidden="true"></i></a>                                                   
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
