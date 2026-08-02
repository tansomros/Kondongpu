<%@ Page Title="บันทึกให้กู้" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BillDetail.aspx.vb" Inherits="Kondongpu.BillDetail" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openModalSend(sender, title, message) {
            $("#spnHeader").text(title);
            $("#spnBodyMsg").text(message);
            $('#modal-window-send').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
        function openModalCancel(sender, title, message) {
            $("#spnHeaderCancel").text(title);
            $("#spnBodyMsgCancel").text(message);
            $('#modal-window-cancel').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
        function openModalChangeType(sender, title, message) {
            $("#spnHeaderChangeType").text(title);
            $("#spnBodyMsgChangeType").text(message);
            $('#modal-window-changetype').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
        function openModalOverview(sender, title, message) {
            $("#spnHeaderOverview").text(title);
            $("#spnBodyMsgOverview").text(message);
            $('#modal-window-overview').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
        function openModalAlert(sender, title, message) {
            $("#spnHeaderAlert").text(title);
            $("#spnBodyMsgAlert").text(message);
            $('#modal-window-alert').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-cash icon-gradient bg-success"></i>
                </div>
                <div>
                    ออกใบเสร็จรับเงิน<div class="page-title-subheading"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <section class="col-lg-12 connectedSortable">
                <div class="main-card mb-3 card">
                    <div class="card-header">บันทึกรายการออกใบเสร็จรับเงิน</div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-2 col-xl-2">
                                <div class="form-group">
                                    <label>เลขที่ใบเสร็จ</label><asp:HiddenField ID="hdBillUID" runat="server" />
                                    <asp:Label ID="lblBillNumber" runat="server" BackColor="#CEE7FF" CssClass="form-control text-center text-primary text-bold"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-2 col-xl-2">
                                <div class="form-group">
                                    <label>วันที่ออกใบเสร็จ</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="form-control text-center"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-language="th-th" data-provide="datepicker"
                                            onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 col-md-4 col-xl-4">
                                <div class="form-group">
                                    <label>ลูกค้า</label>
                                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-12 col-md-4 col-xl-4">
                                <div class="form-group">
                                    <label>โรงงาน</label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Height="50" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
        <div class="row">
            <section class="col-lg-6 connectedSortable">
                <div id="pnAddItem" runat="server" class="main-card mb-3 card">
                    <div class="card-header">
                        รายการบิลรับอ้อย
                        <asp:HiddenField ID="hdRUID" runat="server" />
                        <asp:HiddenField ID="hdDUID" runat="server" />
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>เลขที่บิล</label>
                                    <asp:TextBox ID="txtBillReference" runat="server" CssClass="form-control text-center text-bold" BackColor="#FFFFFF"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>วันที่ส่ง</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSendDate" runat="server" CssClass="form-control text-center"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-language="th-th" data-provide="datepicker"
                                            onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-4 col-xl-4">
                                <div class="form-group">
                                    <label>ทะเบียนรถ</label>
                                    <asp:DropDownList ID="ddlCar" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-4 col-xl-4">
                                <div class="form-group">
                                    <label>ประเภทอ้อย</label>
                                    <asp:DropDownList ID="ddlCane" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>ราคา/ตัน</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>น้ำหนัก</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">ตัน</span>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>เป็นเงิน</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtTotalPrice" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>หักค่าน้ำมัน</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtGasPrice" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-1 col-xl-1 pt-4">
                                <asp:Button ID="cmdAdd" CssClass="btn btn-success" runat="server" Text="เพิ่ม" Width="60px" />
                            </div>
                        </div>
                        
                        <div class="row">

                            <div class="table-responsive">
                                <asp:GridView ID="grdReceipt" CssClass="table table-hover" 
                             runat="server" CellPadding="2" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False"  
                             Font-Bold="False" AllowPaging="True">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField DataField="UID" HeaderText="Id" >
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AccountName" HeaderText="Name" >
                            <HeaderStyle HorizontalAlign="Left" /> 
                            </asp:BoundField>                           
                            <asp:BoundField DataField="Remark" HeaderText="Remark">
                            <HeaderStyle HorizontalAlign="Left" />  
                            </asp:BoundField>                         
                             <asp:TemplateField>
              <itemtemplate>
                    <asp:linkButton ID="imgEdit" runat="server"  Text="แก้ไข" CssClass="btn btn-primary" Width="60"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />  
                   <asp:linkButton ID="imgDel" runat="server"  Text="ลบ" CssClass="btn btn-danger" Width="60"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />  
                                 </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />          
            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerStyle CssClass="pagination-kdp" HorizontalAlign="Center" />
                     </asp:GridView>   

                                
                            </div>
                        </div>
                    </div>
                </div>

            </section>

            <section class="col-lg-6 connectedSortable">
                <div id="pnDeduct" runat="server" class="main-card mb-3 card">
                    <div class="card-header">
                        รายการหัก
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-4 col-xl-4">
                                <div class="form-group">
                                    <label>รายการ</label>
                                    <asp:DropDownList ID="ddlAccount" runat="server" CssClass="form-control select2" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>เลขที่บิล</label>
                                    <asp:TextBox ID="txtDeductRef" runat="server" CssClass="form-control text-center text-bold" BackColor="#FFFFFF"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>จำนวน</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDeductAmount" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-2 col-xl-2 pt-4">
                                <asp:Button ID="cmdAddDeduct" CssClass="btn btn-success" runat="server" Text="เพิ่ม" Width="60px" />
                            </div>
                        </div>
                        <div class="row">

                            <div class="table-responsive">
                                <asp:GridView ID="grdDeduct" CssClass="table table-hover" 
                             runat="server" CellPadding="2" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False"  
                             Font-Bold="False" AllowPaging="True">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField DataField="UID" HeaderText="Id" >
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AccountName" HeaderText="Name" >
                            <HeaderStyle HorizontalAlign="Left" /> 
                            </asp:BoundField>                           
                            <asp:BoundField DataField="Remark" HeaderText="Remark">
                            <HeaderStyle HorizontalAlign="Left" />  
                            </asp:BoundField>                         
                             <asp:TemplateField>
              <itemtemplate>
                    <asp:linkButton ID="imgEdit" runat="server"  Text="แก้ไข" CssClass="btn btn-primary" Width="60"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />  
                   <asp:linkButton ID="imgDel" runat="server"  Text="ลบ" CssClass="btn btn-danger" Width="60"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />  
                                 </itemtemplate>
              <itemstyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />          
            </asp:TemplateField>
                        </columns>
                        <footerstyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                     
                        <pagerstyle HorizontalAlign="Center" 
                             CssClass="dc_pagination dc_paginationC dc_paginationC11" />                     
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <headerstyle CssClass="th" Font-Bold="True" />                     
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerStyle CssClass="pagination-kdp" HorizontalAlign="Center" />
                     </asp:GridView>   

                                
                            </div>
                        </div>

                    </div>
                </div>
            </section>

        </div>
        <div class="row">

            <div class="col-lg-6 col-md-3 col-xl-3">
                <div class="form-group">
                    <label>น้ำหนักรวม</label>
                    <div class="input-group input-group-lg">
                        <asp:Label ID="lblTotalWeight" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="20px"></asp:Label>
                        <div class="input-group-append">
                            <span class="input-group-text">฿</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-3 col-xl-3">
                <div class="form-group">
                    <label>รวมรับ</label>
                    <div class="input-group input-group-lg">
                        <asp:Label ID="lblTotalNetPrice" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="20px"></asp:Label>
                        <div class="input-group-append">
                            <span class="input-group-text">฿</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-3 col-xl-3">
                <div class="form-group">
                    <label>รวมหัก</label>
                    <div class="input-group input-group-lg">
                        <asp:Label ID="lblTotalDeduct" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="20px"></asp:Label>
                        <div class="input-group-append">
                            <span class="input-group-text">฿</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-3 col-xl-3">
                <div class="form-group">
                    <label>คงเหลือ</label>
                    <div class="input-group input-group-lg">
                        <asp:Label ID="lblBalance" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="20px"></asp:Label>
                        <div class="input-group-append">
                            <span class="input-group-text">฿</span>
                        </div>
                    </div>
                </div>
            </div>



        </div>


        <div class="row justify-content-center">
            <hr />
            <div class="col-md-12 text-center">
                <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="บันทึก" Width="100px" />
                <asp:Button ID="cmdClear" CssClass="btn btn-secondary" runat="server" Text="Clear" Width="100px" />
                <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete" Width="120px" />
                <asp:Button ID="cmdPrint" CssClass="btn btn-success" runat="server" Text="พิมพ์ใบเสร็จ" Width="120px" />
            </div>
        </div>



        <!-- Modal HTML -->
        <div id="modal-window" class="modal fade modal-window" role="dialog" data-backdrop="static" tabindex="-1" style="display: none; z-index: 9999;" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header-window">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h6 class="modal-title-window">&nbsp;<span id="spnTitle2"></span></h6>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <span id="spnMsg2"></span>
                                    <br />
                                    <img id="img1" src="" style="width: 100%; display: inline-block;" />
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <button class="btn btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modal-window-cancel" class="modal fade" role="dialog" data-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <div class="modal-title"><span id="spnHeaderCancel"></span></div>
                    </div>
                    <div class="modal-body">
                        <p><span id="spnBodyMsgCancel"></span>&nbsp;โปรดระบุเหตุผลในการยกเลิก </p>
                        <asp:TextBox ID="txtCancelRemark" runat="server" CssClass="form-control" placeholder="ระบุเหตุผล"></asp:TextBox>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="cmdConfirmCancel" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="100" />
                        <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ปิด</button>
                    </div>
                </div>
            </div>
        </div>
        <!--- End Modal --->
    </section>
</asp:Content>
