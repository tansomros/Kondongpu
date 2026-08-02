<%@ Page Title="บัญชีเงินกู้" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Card.aspx.vb" Inherits="Kondongpu.Card" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-portfolio icon-gradient bg-primary"></i>
                </div>
                <div>เลือกบัญชีลูกค้า</div>
            </div>
        </div>
    </div>     
    <section class="content">        
        <div class="main-card mb-3 card">
            <div class="card-header">
                <i class="header-icon lnr-list icon-gradient bg-success"></i>รายการสมุดบัญชีเงินกู้
            <div class="btn-actions-pane-right">   
              สถานะ :    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" Width="160" AutoPostBack="True">
                     <asp:ListItem Value="0" Selected="True">ทั้งหมด</asp:ListItem>
                    <asp:ListItem Value="A">ปกติ</asp:ListItem>
                    <asp:ListItem Value="T">ยกยอดไป</asp:ListItem>
                    <asp:ListItem Value="X">ปิดบัญชี</asp:ListItem>
                            </asp:DropDownList> 
        
            </div>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table id="tbdata" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center" style="width: 120px">เลขที่บัญชี</th>                                
                                <th class="text-center">ชื่อลูกค้า</th>                                                                
                                <th class="text-center">วงเงินเครดิต</th>                              
                                <th class="text-center">วงเงินคงเหลือ</th>
                                <th class="text-center">ยอดกู้</th>
                                <th class="text-center">ยอดคงค้าง</th>
                                <th  width="100" class="text-center">สถานะ</th>
                                <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtAcc.Rows %>
                            <tr>
                                <td class="text-center"><% =String.Concat(row("AccNo")) %></td>                                 
                                <td><% =String.Concat(row("CustomerName")) %></td>     
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("CreditAmount")).ToString("#,##0") %></td>
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("CreditBalance")).ToString("#,##0") %></td>
                                <td class="text-right text-bold text-success"><% =Kondongpu.DBNull2Dbl(row("LoanAmount")).ToString("#,##0") %></td>
                                <td class="text-right text-bold text-danger"><% =Kondongpu.DBNull2Dbl(row("DebtBalance")).ToString("#,##0")  %></td>
                                <td class="text-center"><% =String.Concat(row("CardStatusName")) %></td>
                                <td class="text-center" style="width:220px">    
                                      <% If String.Concat(row("CardStatus")) = "A" Then %>
                                    <a href="LoanNew?m=l&cid=<% =String.Concat(row("UID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="บันทึกให้กู้"><i class="fa fa-plus-circle" aria-hidden="true"></i></a>  
                                     <a href="LoanPay?m=l&cid=<% =String.Concat(row("UID")) %>" class="btn btn-warning" data-toggle="tooltip" data-placement="top" data-original-title="ชำระเงินกู้"><i class="fa fa-coins" aria-hidden="true"></i></a> 
                                    <a href="LoanCard?m=l&cid=<% =String.Concat(row("UID")) %>" class="btn btn-danger" data-toggle="tooltip" data-placement="top" data-original-title="ปิดบัญชี"><i class="fa fa-lock" aria-hidden="true"></i></a>  
                                    <% End If %>
                                     <a href="ReportViewer.aspx?rpt=card&code=<% =String.Concat(row("AccNo")) %>&id=<% =String.Concat(row("UID")) %>" target="_blank" class="btn btn-success" data-toggle="tooltip" data-placement="top" data-original-title="พิมพ์การ์ดลูกหนี้"><i class="fa fa-print" aria-hidden="true"></i></a>                                      
                                </td>
                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>


    </section>
</asp:Content>
