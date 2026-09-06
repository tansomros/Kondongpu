<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportCondition.aspx.vb" Inherits="Kondongpu.ReportCondition" %>
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
   <% If Request("rpt") = "C6" Then %>
                                    <th class="text-center">เลขที่บิล</th>                          
                                    <th class="text-left"> ชื่อลูกค้า</th>                          
                                    <th class="text-center">ชื่อเล่น</th>
                                    <th class="text-left">รายการ</th>    
                                    <th class="text-right">จำนวนเงิน</th>
                    <% Else %>                               
                                    <th class="text-right">รวม</th>
                                    <th class="text-right">รวมหัก</th>
                                    <th class="text-right">จำนวนจ่ายรวม</th>
       <% End If %>    
                                 
                                </tr>
                            </thead>
                            <tbody>
                                <% For Each row As DataRow In dtRptB.Rows %>
                                <tr>
                                    <td class="text-center"><% =String.Concat(row("nRow")) %></td>
                                    <td class="text-center"><% =Format(row("BillDate"), "dd/MM/yyyy") %></td>
   <% If Request("rpt") = "C6" Then %>
                                    <td class="text-center"><% =String.Concat(row("BillNumber")) %></td>    
                                       <td class="text-left"><% =String.Concat(row("CustomerName")) %></td>   
                                       <td class="text-center"><% =String.Concat(row("NickName")) %></td>   
                                       <td class="text-left"><% =String.Concat(row("AccountName")) %></td>   
                                    <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("NetPrice")).ToString("#,##0.##") %></td>                            
                                         
                           <% Else %> 
                                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("SumPrice")).ToString("#,##0.##") %></td>
                                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("SumDeduct")).ToString("#,##0.##") %></td>
                                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("NetBalance")).ToString("#,##0.##") %></td>
                                         <% End If %>    
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
