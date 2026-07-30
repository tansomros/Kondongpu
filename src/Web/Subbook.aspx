<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Subbook.aspx.vb" Inherits="Kondongpu.Subbook" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-ribbon icon-gradient bg-success"></i>
                </div>
                <div>
                    <asp:Label ID="lblTitle" runat="server" Text="รายการ Subbook"></asp:Label>
                </div>
            </div>
        </div>
    </div>
     
    <section class="content">        
        <div class="box box-solid">
             <div class="box-header">
              <i class="fa fa-filter"></i>
              <h3 class="box-title">ค้นหา</h3>   
                  <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-lg-6 col-md-3 col-xl-3">
                        <div class="form-group">
                            <label>Start Date</label>
                            <br />
                            <div class="input-group">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control text-center"
                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                    data-date-language="th-th" data-provide="datepicker"
                                    onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                <div class="input-group-append">
                                    <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-lg-6 col-md-3 col-xl-3">
                        <div class="form-group">
                            <label>End Date</label>
                            <br />
                            <div class="input-group">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control text-center"
                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                    data-date-language="th-th" data-provide="datepicker"
                                    onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                <div class="input-group-append">
                                    <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>     
                     <div class="col-md-3 text-left"> 
                         <label>&nbsp;</label>
                            <br />
                        <asp:LinkButton ID="cmdView" runat="server" CssClass="btn btn-success" Width="120px"><i class="fa fa-search"></i>ค้นหา</asp:LinkButton> 
                    </div>
                </div>
            </div>
        </div>
        <div class="main-card mb-3 card">
            <div class="card-header">
                <i class="header-icon lnr-list icon-gradient bg-success"></i>Subbook List
            <div class="btn-actions-pane-right">
                
            </div>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table id="tbdata" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center" style="width: 120px">Subbook No.</th>
                                <th class="text-center">รายการ</th>
                                <th class="text-center">ลงรับ</th>
                                <th class="text-center">ลงจ่าย</th>                             
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtREQ.Rows %>
                            <tr>
                                <td class="text-center"><% =String.Concat(row("Code")) %></td>
                                <td><% =String.Concat(row("Descriptions")) %></td>
                                <td class="text-right"><% =KDP.DBNull2Dbl(row("RevAmount")).ToString("#,##0.##")  %></td>
                                <td class="text-right"><% =KDP.DBNull2Dbl(row("PayAmount")).ToString("#,##0.##")  %></td>                         
                               

                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                </div>

            </div>
            <div class="d-block text-right float-right card-footer">
                <table align="right">
                    <tr>
                        <td>รวม</td>
                         <td><asp:Label ID="lblTotalRev" runat="server" Text="0.00" CssClass="form-control text-bold text-right text-success" Width="120px"  BackColor="#E0F3FF"></asp:Label></td>
                         <td><asp:Label ID="lblTotalPay" runat="server" Text="0.00" CssClass="form-control text-bold text-right text-danger" Width="120px" BackColor="#E0F3FF"></asp:Label></td>
                    </tr>
                </table>               
                   
                    
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-12 text-center"> 
                <asp:Button ID="cmdPrint" CssClass="btn btn-success" runat="server" Text="พิมพ์" Width="120px" />
            </div>
        </div>
    </section>
</asp:Content>
