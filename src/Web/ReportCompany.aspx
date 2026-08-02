<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportCompany.aspx.vb" Inherits="Kondongpu.ReportCompany" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-monitor icon-gradient bg-primary"></i>
                </div>
                <div>
                    <asp:Label ID="lblReportTitle" runat="server" Text="รายงานสัญญา"></asp:Label>
                    <div class="page-title-subheading">ฅนดงพุ</div>
                </div>
            </div>
        </div>
    </div>

    <section class="content">

        <div class="box box-solid">
            <div class="box-body" style="background-color: #14539a; color: white">
                <div class="row">
                        <div class="col-lg-6 col-md-4 col-xl-3">
                        <div class="form-group">
                            <label>ประเภท</label>
                            <br />
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-4 col-xl-3">
                        <div class="form-group">
                            <label>สถานะ</label>
                            <br />
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div> 
                    <div class="col-lg-6 col-md-4 col-xl-3">
                        <div class="form-group">
                            <label>คำค้นหา</label><br />
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" PlaceHolder="ชื่อลูกค้า / เลขที่สัญญา"></asp:TextBox>
                        </div>
                    </div>
                    
                     <div class="col-lg-6 col-md-12 col-xl-3">
                        <br />
                        <asp:LinkButton ID="cmdView" runat="server" CssClass="btn btn-warning" Width="120px"><i class="fa fa-desktop"></i>ดูรายงาน</asp:LinkButton>
                        <asp:LinkButton ID="cmdExport" runat="server" CssClass="btn btn-success" Width="120px"><i class="fa fa-file-excel"></i>Export</asp:LinkButton>

                    </div>
                </div>

            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>        
        <div id="pnData" runat="server" class="main-card mb-3 card">
            <div class="card-header">
                รายการสัญญาที่พบตามเงื่อนไข
            <div class="btn-actions-pane-right">
            </div>
            </div>
            <div class="card-body table-responsive">
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
                            <% For Each row As DataRow In dtL.Rows %>
                            <tr>
                                <td class="text-center"><% =String.Concat(row("Code")) %></td>
                                <td class="text-center"><% =Format(row("AgreementDate"), "dd/MM/yyyy") %></td>
                                <td><% =String.Concat(row("CustomerName")) %></td>                                
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("Amount")).ToString("#,##0.##") %></td>
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("PayAmount")).ToString("#,##0.##") %></td>                                
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("Amount") - row("PayAmount")).ToString("#,##0.##")  %></td>
                                <td class="text-center"><% =String.Concat(row("Interest")) %></td>
                                <td class="text-center"><% =String.Concat(row("AgreementTypeName")) %></td>
                                <td class="text-center"><% =String.Concat(row("AgreementStatusName")) %></td>
                             
                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
            </div>
        </div>   
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdView" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </section>
</asp:Content>
