<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AgreementList.aspx.vb" Inherits="Kondongpu.AgreementList" %>

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
                <div>สัญญา</div>
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
                  <div class="col-lg-6 col-md-3 col-xl-3">
                        <div class="form-group">
                            <label>ประเภท</label>
                            <br />
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-3 col-xl-3">
                        <div class="form-group">
                            <label>สถานะ</label>
                            <br />
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-12 text-center"> 
                        <asp:LinkButton ID="cmdView" runat="server" CssClass="btn btn-success" Width="120px"><i class="fa fa-search"></i>ค้นหา</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="main-card mb-3 card">
            <div class="card-header">
                <i class="header-icon lnr-list icon-gradient bg-success"></i>รายการสัญญา
            <div class="btn-actions-pane-right">
                <a href="AgreementNew.aspx?m=ag&t=new" class="btn btn-success pull-right"><i class="fa fa-plus-circle"></i>ทำสัญญาใหม่</a>            
            </div>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table id="tbdata" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center" style="width: 120px">เลขที่สัญญา</th>
                                <th class="text-center">ลงวันที่</th>
                                <th class="text-center">ชื่อลูกค้า</th>                                                                
                                <th class="text-center">วงเงิน</th>
                                <th class="text-center">ยอดจ่าย</th>
                                <th class="text-center">คงเหลือ</th>
                                <th class="text-center">ดอกเบี้ย(%)</th>
                                <th class="text-center">ประเภทสัญญา</th> 
                                <th  width="100" class="text-center">สถานะ</th>
                                <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtAgr.Rows %>
                            <tr>
                                <td class="text-center"> <a href="Agreement?m=ag&id=<% =String.Concat(row("UID")) %>" data-toggle="tooltip" data-placement="top" data-original-title="ดูรายละเอียด"><% =String.Concat(row("Code")) %></a></td>
                                <td class="text-center"><% =Format(row("AgreementDate"), "dd/MM/yyyy") %></td>
                                <td><% =String.Concat(row("CustomerName")) %></td>                                
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("Amount")).ToString("#,##0.##") %></td>
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("PayAmount")).ToString("#,##0.##") %></td>                                
                                <td class="text-right"><% =(Kondongpu.DBNull2Dbl(row("Amount")) - Kondongpu.DBNull2Dbl(row("PayAmount"))).ToString("#,##0.##")  %></td>
                                <td class="text-center"><% =String.Concat(row("Interest")) %></td>
                                <td class="text-center"><% =String.Concat(row("AgreementTypeName")) %></td>
                                <td class="text-center"><% =String.Concat(row("AgreementStatusName")) %></td>
                                <td class="text-center" style="width: 50px">   
                                    <% If String.Concat(row("AgreementStatus")) = "1" Then %>
                                    <a href="Agreement?m=ag&id=<% =String.Concat(row("UID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="แก้ไข"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                    <% Else %>
                                     <a href="Agreement?m=ag&id=<% =String.Concat(row("UID")) %>" class="btn btn-secondary" data-toggle="tooltip" data-placement="top" data-original-title="ดูรายละเอียด"><i class="fa fa-search" aria-hidden="true"></i></a>
                                    <% End If %>
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
