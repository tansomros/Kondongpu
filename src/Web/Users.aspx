<%@ Page Title="User Account" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Users.aspx.vb" Inherits="Kondongpu.Users" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="pe-7s-users icon-gradient bg-success"></i>
            </div>
            <div>
                <%: Title %>
                    <div class="page-title-subheading">
                        <%: MetaDescription %>
                    </div>
            </div>
        </div>
    </div>
</div>
     
<section class="content">  
     <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-users icon-gradient bg-success">
            </i>ผู้ใช้งานทั้งหมด
            <div class="btn-actions-pane-right">
                <a href="UserModify?m=u&s=u" class="btn btn-success btn-sm pull-right"><i class="fa fa-plus-circle"></i> เพิ่มใหม่</a>
            </div>
        </div>
        <div class="card-body">              
               <div class="row">
                <div class="col-md-12 table-responsive">

                    <div class="card-body table-responsive">
                        <table id="tbdata" class="table table-bordered">
                            <thead>
                                <tr>
                                    <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) > 2 Then%>
                                    <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                                    <% End If %>
                                    <th class="text-center">Username</th>
                                    <th class="text-left">ชื่อ</th>
                                    <th class="text-center">สิทธิ์</th>
                                     <th class="text-center">Last Login</th>
                                    <th class="text-center">Active</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% For Each row As DataRow In dtUser.Rows %>
                                <tr>
                                    <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) > 2 Then%>
                                    <td width="50" class="text-center">
                                        <a href="UserModify?m=u&s=u&uid=<% =String.Concat(row("UserID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไข"><i class="fa fa-edit" aria-hidden="true"></i></a>                                                    
                                    </td>
                                    <% End If %>
                                    <td class="text-left"><% =String.Concat(row("Username")) %></td>
                                    <td><a href="UserModify?m=u&s=u&uid=<% =String.Concat(row("UserID")) %>"><% =String.Concat(row("DisplayName")) %> </a></td>
                                
                                    <td class="text-center"><% =String.Concat(row("RoleName")) %></td>   
                                      <td class="text-center"><% =String.Concat(row("LastLoginDate2")) %></td>  
                                    <td class="text-center">
                                       <% If String.Concat(row("StatusFlag")) = "A" Then%>
                                         <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
                                       <% End If %>
                                    </td> 
                                </tr>
                                <%  Next %>
                            </tbody>
                        </table>
                    </div>
                </div>
          </div>
             
 </div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 
     </section>
</asp:Content>
