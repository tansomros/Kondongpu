<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PriceModify.aspx.vb" Inherits="Kondongpu.PriceModify" %>

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
                    <i class="pe-7s-note icon-gradient bg-success"></i>
                </div>
                <div>
                    กำหนดราคาซื้อ
                            <div class="page-title-subheading">กำหนดรายละเอียดราคาซื้อ</div>
                </div>
            </div>
        </div>
    </div>

    <!-- Main content -->
    <section class="content">
          <div class="row">
            <section class="col-lg-12 connectedSortable">
                <div class="main-card mb-3 card">
                     <div class="card-header">ตั้งราคา
                        <asp:HiddenField ID="hdUID" runat="server" />
            <div class="btn-actions-pane-right"> 
            </div>
            </div> 
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>ปี</label>
                                          <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                            </div>                          
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>วันที่เริ่ม</label>
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
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>วันที่สิ้นสุด</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtEnddate" runat="server" CssClass="form-control text-center"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-language="th-th" data-provide="datepicker"
                                            onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>ประเภทอ้อย</label>
                                     <asp:DropDownList ID="ddlCane" runat="server" CssClass="form-control select2">                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
  <div class="col-md-6">
                                <div class="form-group">
                                    <label>โรงงาน</label><br />
                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true"  CssClass="form-control btn btn-primary select2"></asp:DropDownList>
                                </div>
                            </div>

                               <div class="col-md-6">
       <div class="form-group">
           <label>ที่อยู่</label>
           <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="35" CssClass="form-control"></asp:TextBox>
       </div>
   </div>


</div>             

   <div  class="row">
     <div class="col-md-4">
                                <div class="form-group">
                                    <label>ราคา</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>    
         <div class="col-md-4">
      <div class="form-group">
          <label>Active</label>
          <div class="button r" id="button-3">
              <input id="chkStatus" type="checkbox" class="checkbox" runat="server" checked="checked">
              <div class="knobs"></div>
              <div class="layer"></div>
          </div>
      </div>
  </div>

                        </div>
                                  
                    </div>
                    <div class="card-footer"> 
                    </div>
                </div> 
                </section>
        </div>    

        <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="บันทึก" Width="100px" />             
                <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete" Width="120px" />
                 <asp:Button ID="cmdBack" CssClass="btn btn-secondary" runat="server" Text="กลับหน้ารายการ" Width="120px" />
            </div>
        </div>

    </section>
</asp:Content>
