<%@ Page Title="การ์ดลูกหนี้" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LoanCard.aspx.vb" Inherits="Kondongpu.LoanCard" %>
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
        function openModalTransfer(sender, title, message) {
            $("#spnHeaderTransfer").text(title);
            $("#spnBodyMsgTransfer").text(message);
            $('#modal-window-transfer').modal('show');
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
                    <i class="pe-7s-id icon-gradient bg-success"></i>
                </div>
                <div>การ์ดลูกหนี้
                            <div class="page-title-subheading">ข้อมูลบัญชีลูกหนี้ สามารถทำการปิดบัญชี/ยกยอดบัญชี้ ได้ในหน้านี้</div>
                </div>
            </div>
        </div>
    </div>
    <!-- Main content -->
    <section class="content">   
        <div class="row">
            <section class="col-lg-6 connectedSortable">
                <div class="main-card mb-3 card">          
                      <div class="card-header">ข้อมูลบัญชีลูกหนี้
            <div class="btn-actions-pane-right">
              สถานะ : <asp:Label ID="lblStatus" runat="server" Text="ปกติ"></asp:Label>   
            </div>
            </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>เลขที่บัญชี</label><asp:HiddenField ID="hdCustomerUID" runat="server" />
                                    <asp:TextBox ID="txtAccNo" runat="server"  BackColor="#FFFFFF" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-6 col-xl-6">
                                <div class="form-group">
                                    <label>ชื่อ-นามสกุล</label>
                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                          <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>ชื่อเล่น</label>
                                    <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>  
                            <div class="col-lg-12 col-md-6 col-xl-6">
                                <div class="form-group">
                                    <label>เลขบัตรประชาชน</label>
                                    <asp:TextBox ID="txtCardID" MaxLength="13" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>                          
   <div class="col-lg-12 col-md-6 col-xl-6">
                                <div class="form-group">
                                    <label>เบอร์โทร</label>
                                    <asp:TextBox ID="txtTel" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ที่อยู่</label>
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="35" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            </div>
  <div class="row">

     <div class="col-lg-12 col-md-6 col-xl-6">
                                <div class="form-group">
                                    <label>ยอดกู้ (เงินต้น+ดอกเบี้ย)</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtCredit" runat="server" ReadOnly="true" CssClass="form-control text-center text-blue text-bold" BackColor="White" Font-Size="20px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>                        
                             <div class="col-lg-12 col-md-6 col-xl-6">
                                <div class="form-group">
                                    <label>ยอดค้างชำระ</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtCreditBalance" runat="server" ReadOnly="true" CssClass="form-control text-center text-danger text-bold" BackColor="White" Font-Size="20px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div> 
                        </div>   
                         <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                  <asp:Button ID="cmdClose" CssClass="btn btn-danger" runat="server" Text="บันทึกปิดบัญชี" />
            </div>
        </div> 
                    </div>
                </div>
            </section>
            <section class="col-lg-6 connectedSortable">
           <div class="main-card mb-3 card">
                    <div class="card-header">
                        บันทึกปิดบัญชี/ยกยอด
                        <asp:HiddenField ID="hdCardUID" runat="server" />
                        <asp:HiddenField ID="hdCardDetailUID" runat="server" />
                    </div>
                    <div class="card-body">
                        <div class="row">                                               
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>วันที่</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSubmitDate" runat="server" CssClass="form-control text-center"
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
                                    <label>วันที่ครบกำหนด</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control text-center"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-language="th-th" data-provide="datepicker"
                                            onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i> </span>
                                        </div>
                                    </div>
                                </div>
                            </div>  
                               <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>จน.วัน</label>
                                    <div class="input-group">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDay" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmdCalDay" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="input-group-append">
                                            <span class="input-group-text btn btn-success"><asp:LinkButton ID="cmdCalDay" runat="server" CssClass="text-white"><i class="fa lnr-clock"></i></asp:LinkButton></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
  <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>จำนวนยอดยกไป</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control text-center text-blue text-bold"  BackColor="#CEE7FF" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
</div>                           
   <div class="row">
   
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>อัตราดอกเบี้ย</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control text-center text-danger text-bold"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>ดอกเบี้ย(บาท)</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control text-center text-success text-bold" BackColor="White"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
        <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>รวมเป็น</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" CssClass="form-control text-center text-success text-bold" BackColor="White"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

         <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <div class="input-group">
                                        <asp:Button ID="cmdCalculate" runat="server" Text="คำนวน" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>                        
                 <div class="row">
     <div class="col-lg-12 col-md-12">
                                <div class="form-group">
                                    <label>Remark</label>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Height="100" TextMode="MultiLine"></asp:TextBox>                                   
                                </div>
                            </div>
                     </div>
                         <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                  <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="บันทึกยกยอด" Width="120px" />
            </div>
        </div> 

                        </div>                  
                </div>
            </section>
        </div>    

        <div class="row justify-content-center">
            <div class="col-md-12 text-center">       
                <asp:Button ID="cmdPrint" CssClass="btn btn-success" runat="server" Text="พิมพ์การ์ดลูกหนี้" />
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
                                <p><span id="spnBodyMsgCancel"></span> &nbsp;</p>                    

                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="cmdConfirmCancel" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="100" />
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ยกเลิก</button>
                            </div>
                        </div>
                    </div>
                </div>
          <div id="modal-window-transfer" class="modal fade" role="dialog" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <div class="modal-title"><span id="spnHeaderTransfer"></span></div>
                            </div>
                            <div class="modal-body">
                                <p><span id="spnBodyMsgTransfer"></span> &nbsp;</p>                    

                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="cmdConfirmTransfer" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="100" />
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ยกเลิก</button>
                            </div>
                        </div>
                    </div>
                </div>
                   <!--- End Modal --->
    </section>
</asp:Content>
