<%@ Page Title="Home" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.vb" Inherits="Kondongpu.Home" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="assets/styles.css" rel="stylesheet" />

    <script>
        window.Promise ||
            document.write(
                '<script src="assets/polyfill.min.js"><\/script>'
            )
        window.Promise ||
            document.write(
                '<script src="assets/classList.min.js"><\/script>'
            )
        window.Promise ||
            document.write(
                '<script src="assets/findindex_polyfill_mdn.js"><\/script>'
            )
    </script>


    <script src="assets/apexcharts.js"></script>


    <script>
        // Replace Math.random() with a pseudo-random number generator to get reproducible results in e2e tests
        // Based on https://gist.github.com/blixt/f17b47c62508be59987b
        var _seed = 42;
        Math.random = function () {
            _seed = _seed * 16807 % 2147483647;
            return (_seed - 1) / 2147483646;
        };
    </script>
    <script>
        var colors = [
            '#008FFB',
            '#FEB019',
            '#00E396',
            '#FF4560',
            '#775DD0',
            '#546E7A',
            '#26a69a',
            '#D10CE8'
        ]
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-light icon-gradient bg-mean-fruit"></i>
                </div>
                <div>
                    Dashboard
                                    <div class="page-title-subheading">ฅนดงพุ</div>
                </div>
            </div>
        </div>
    </div>

    <!-- Main content -->
    <section class="content">
         <div class="alert alert-primary text-center">
                         <b></b>
             <br />
            
                        </div> 

        <div class="row">
        <section class="col-lg-6 connectedSortable">
              <div class="box box-solid">
        <div class="box-header with-border">
          <h2 class="box-title">รายการแบบฟอร์ม</h2>   
          <div class="box-tools pull-right">            
          </div>                       
        </div>
        <div class="box-body">
                  <table id="tbdata" class="table table-bordered table-hover">
                        <thead>
                            <tr>  
                                <th class="text-center" style="width: 30px">No.</th> 
                                <th class="text-left">แบบฟอร์ม</th>
                                <th class="text-center">ดาวน์โหลด</th> 
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtFm.Rows %>
                            <tr> 
                                <td class="text-center"><% =String.Concat(row("nRow")) %></td>
                                <td class="text-left"><a href="<% =String.Concat(row("Link")) %>" target="_blank" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดาวน์โหลด"><% =String.Concat(row("Descriptions")) %></a></td>
                                <td class="text-center" style="width: 30px">                                   
                                    <a href="<% =String.Concat(row("Link")) %>"  target="_blank" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดาวน์โหลด"><i class="fa fa-cloud-download-alt" aria-hidden="true"></i></a>
                                </td>
                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                                
    </div>
        <div class="box-footer">
       
        </div>
      </div> 
</section>
            <section class="col-lg-6 connectedSortable">
                <div class="main-card mb-3 card">
                    <div class="card-header">
                        <i class="header-icon lnr-calendar-full icon-gradient bg-plum-plate"></i>ปฏิทินกิจกรรม
                        <div class="btn-actions-pane-right">
                            <div role="group" class="btn-group-sm nav btn-group">
                                <a data-toggle="tab" href="#tab-eg1-0" class="btn-shadow btn btn-primary active">แสดงแบบปฏิทิน</a>
                                <a data-toggle="tab" href="#tab-eg1-1" class="btn-shadow btn btn-primary">แสดงแบบลิสต์รายการ</a>
                            </div>
                        </div>
                    </div>
                    <div class="card-body no-padding">
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab-eg1-0" role="tabpanel">
                               <div id="calendar2"></div>
                            </div>
                            <div class="tab-pane" id="tab-eg1-1" role="tabpanel">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                       <asp:GridView ID="grdEvent"
                                        runat="server" CellPadding="0" ForeColor="#333333"
                                        GridLines="None"
                                        AutoGenerateColumns="False" Width="100%" CssClass="table table-hover" AllowPaging="True">
                                        <RowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="EventDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันที่">
                                            <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Descriptions" HeaderText="ชื่อรายการ">
                                            <HeaderStyle CssClass="text-left" HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle CssClass="dc_pagination dc_paginationC dc_paginationC11" HorizontalAlign="Center" />
                                        <SelectedRowStyle  Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Middle" />
                                    </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="grdEvent" EventName="PageIndexChanging" />
                                    </Triggers>
                                </asp:UpdatePanel>
                           
                            </div>                       
                        </div>
                    </div>                    
                </div>
            </section>
        </div>
    </section>
</asp:Content>
