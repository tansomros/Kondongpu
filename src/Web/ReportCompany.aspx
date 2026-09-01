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
                    <div class="col-lg-6 col-md-2 col-xl-2">
                        <div class="form-group">
                            <label>Start Date</label>
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
                    <div class="col-lg-6 col-md-2 col-xl-2">
                        <div class="form-group">
                            <label>End Date</label>
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
                            <label><asp:Label ID="lblComp" runat="server" Text="โรงงาน"></asp:Label></label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-12 col-xl-3">
                        <br />
                        <asp:LinkButton ID="cmdView" runat="server" CssClass="btn btn-warning" Width="120px"><i class="fa fa-desktop"></i>ดูรายงาน</asp:LinkButton>
                        <asp:LinkButton ID="cmdExport" runat="server" CssClass="btn btn-success" Width="120px"><i class="fa fa-file-text"></i>Print</asp:LinkButton>

                    </div>
                </div>

            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="main-card mb-3 card">
                    <div class="card-header">
                        รายการข้อมูลที่พบตามเงื่อนไข
            <div class="btn-actions-pane-right">
            </div>
                    </div>
                    <div class="card-body table-responsive">
                        <table id="tbreport" class="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th class="text-center">No</th>
                                    <th class="text-center">วันที่</th>
                                    <th class="text-center">เลขที่บิล</th>
                                    <th class="text-center">ทะเบียนรถ</th>
                                    <th class="text-center">เกรดอ้อย</th>
                                    <th class="text-center">ราคา</th>
                                    <th class="text-center">นน.สุทธิ</th>
                                    <th class="text-center">จำนวนเงิน</th>
                                    <th class="text-center">ค่าน้ำมัน</th>
                                    <th class="text-center">เป็นเงินสุทธิ</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% For Each row As DataRow In dtRptC.Rows %>
                                <tr>
                                    <td class="text-center"><% =String.Concat(row("nRow")) %></td>
                                    <td class="text-center"><% =Format(row("SendDate"), "dd/MM/yyyy") %></td>
                                    <td class="text-center"><% =String.Concat(row("BillNumber")) %></td>
                                    <td class="text-center"><% =String.Concat(row("CarRegisNumber")) %></td>
                                    <td class="text-center"><% =String.Concat(row("CaneTypeUID")) %></td>
                                    <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("UnitPrice")).ToString("#,##0.##") %></td>
                                    <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("Weight")).ToString("#,##0.##") %></td>
                                    <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("NetPrice")).ToString("#,##0.##") %></td>
                                    <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("GasPrice")).ToString("#,##0.##") %></td>
                                    <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("NetBalance")).ToString("#,##0.##") %></td>
                                </tr>
                                <%  Next %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdView" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <script type="text/javascript">
            function initReportDataTable() {
                var table = $('#tbreport');
                if (table.length && table.find('tbody tr').length > 0) {
                    if ($.fn.DataTable.isDataTable('#tbreport')) {
                        $('#tbreport').DataTable().destroy();
                    }
                    $('#tbreport').DataTable({
                        "order": [[0, "asc"]],
                        "pageLength": 25,
                        "language": {
                            "search": "ค้นหา:",
                            "lengthMenu": "แสดง _MENU_ รายการ",
                            "info": "แสดง _START_ ถึง _END_ จาก _TOTAL_ รายการ",
                            "infoEmpty": "ไม่พบรายการ",
                            "infoFiltered": "(กรองจากทั้งหมด _MAX_ รายการ)",
                            "zeroRecords": "ไม่พบข้อมูลที่ค้นหา",
                            "paginate": {
                                "first": "หน้าแรก",
                                "last": "หน้าสุดท้าย",
                                "next": "ถัดไป",
                                "previous": "ก่อนหน้า"
                            }
                        }
                    });
                }
            }

            $(function () {
                initReportDataTable();
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                initReportDataTable();
            });
        </script>

    </section>
</asp:Content>
