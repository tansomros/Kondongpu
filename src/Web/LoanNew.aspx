<%@ Page Title="บันทึกให้กู้" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LoanNew.aspx.vb" Inherits="Kondongpu.LoanNew" %>
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
                    <i class="pe-7s-credit icon-gradient bg-success"></i>
                </div>
                <div>
                    บันทึกให้กู้ใหม่
                            <div class="page-title-subheading"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- Main content -->
    <section class="content">   
        <div class="row">
            <section class="col-lg-6 connectedSortable">
                <div class="main-card mb-3 card">
                    <div class="card-header">ข้อมูลบัญชีลูกหนี้</div>
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
                                    <label>วงเงิน/เครดิต</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtCredit" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="20px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>                        
                             <div class="col-lg-12 col-md-6 col-xl-6">
                                <div class="form-group">
                                    <label>คงเหลือ</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtCreditBalance" runat="server" ReadOnly="true" CssClass="form-control text-center text-success text-bold" BackColor="White" Font-Size="20px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div> 
                        </div>
                        
                    </div>
                
                </div>
            </section>
            <section class="col-lg-6 connectedSortable">
           <div class="main-card mb-3 card">
                    <div class="card-header">
                        ข้อมูลเงินกู้
                        <asp:HiddenField ID="hdCardUID" runat="server" />
                        <asp:HiddenField ID="hdCardDetailUID" runat="server" />
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>เลขที่รายการ</label>
                                    <asp:TextBox ID="txtSubbookNo" runat="server" CssClass="form-control text-center text-bold" BackColor="#FFFFFF"></asp:TextBox>
                                </div>
                            </div>                         
                            <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>วันที่ให้กู้</label>
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
                               <div class="col-lg-6 col-md-3 col-xl-2">
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
                    
                          
                            
</div>                           
   <div class="row">
     <div class="col-lg-6 col-md-3 col-xl-3">
                                <div class="form-group">
                                    <label>จำนวนเงินต้น</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control text-center text-blue text-bold"  BackColor="#CEE7FF" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-3 col-xl-2">
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
                             <div class="col-lg-6 col-md-3 col-xl-2">
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

         <div class="col-lg-6 col-md-3 col-xl-2">
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
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Height="50" TextMode="MultiLine"></asp:TextBox>                                   
                                </div>
                            </div>
                     </div>
                        </div>                  
                </div>
                <div class="main-card mb-3 card">
                    <div class="card-header">
                        วิธีจ่ายเงิน
                    </div>
                    <div class="card-body">
               
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-xl-3">
                                <div class="form-group">
                                    <div class="text-bold text-blue"><label>จ่ายโดย</label></div>
                                    <asp:RadioButtonList ID="optPayType" runat="server" RepeatDirection="Horizontal">
                                          <asp:ListItem Selected="True" Value="C">เงินสด</asp:ListItem>
                                        <asp:ListItem Value="T">เงินโอน</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-xl-4">
                                <div class="form-group">
                                    <label>จากบัญชี</label>
                                      <asp:DropDownList ID="ddlBank" runat="server"   CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-xl-3">
                                <div class="form-group"> 
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>              
                                            <label>สลิปโอนเงิน 
                                                <button class="btn-icon btn-icon-only btn-link no-border ico-info small" type="button" data-title="Note" data-toggle="popover-custom-bg" data-bg-class="text-white small bg-primary" data-content="ไฟล์นามสกุล .jpg, .jpeg, .gif, .png เท่านั้น ,ขนาดไฟล์ไม่เกิน 1024 Kb. เพิ่มได้ไม่เกิน 4 รูป" data-original-title="" title=""><i class="fa fa-info-circle btn-icon-wrapper"> </i></button>                                                
                                              </label>  
                                             <div class="file-upload"> 
                                                 <asp:FileUpload  ID="FileUploadA" runat="server" AllowMultiple="true" /> 
                                                  <i class="fa fa-camera"></i>
                                             </div>                                    
  </ContentTemplate>
                                <Triggers> 
                                    <asp:PostBackTrigger ControlID="cmdSave" />
                                </Triggers>
                            </asp:UpdatePanel>  
            
                                </div>
                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <asp:ImageButton ID="imgSlip" runat="server" Height="50px" Visible="false" />                                    
                                </div>
                            </div>
                        </div>  
                    </div>
                </div>
                 

            </section>
        </div>    

        <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="บันทึก" Width="100px" />
                <asp:Button ID="cmdClear" CssClass="btn btn-secondary" runat="server" Text="Clear" Width="100px" />
                <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete" Width="120px" />
                <asp:Button ID="cmdPrint" CssClass="btn btn-success" runat="server" Text="พิมพ์" Width="100px" />
            </div>
        </div>
        <hr />
         <div class="row">
            <section class="col-lg-12 connectedSortable">
                 <div class="main-card mb-3 card">
                    <div class="card-header">
                        รายการย้อนหลัง
                    </div>
                    <div class="card-body">
                         <div class="table-responsive">
                    <table id="tbdata" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center" style="width: 120px">เลขที่รายการ</th>                                
                                <th class="text-center">วันที่</th>                                                                
                                <th class="text-center">เงินต้น</th>                              
                                <th class="text-center">อัตราดอกเบี้ย</th>
                                <th class="text-center">ยอดดอกเบี้ย</th>
                                <th class="text-center">รวม</th> 
                                <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtLoan.Rows %>
                            <tr>
                                <td class="text-center"><% =String.Concat(row("Code")) %></td>                                 
                                <td class="text-center"><% =String.Concat(row("LoanDate")) %></td>     
                                <td class="text-right"><% =Kondongpu.DBNull2Dbl(row("Loan_Amount")).ToString("#,##0.##") %></td>
                                <td class="text-center"><% =Kondongpu.DBNull2Dbl(row("Int_Rate")).ToString("#,###.#0") %></td>
                                <td class="text-right text-bold text-success"><% =Kondongpu.DBNull2Dbl(row("Int_Total")).ToString("#,##0.##") %></td>
                                <td class="text-right text-bold text-danger"><% =Kondongpu.DBNull2Dbl(row("Loan_Total")).ToString("#,##0.##")  %></td>
                                <td class="text-center" style="width:50px">                                   
                                    <a href="LoanNew?m=l&id=<% =String.Concat(row("UID")) %>&cid=<% =String.Concat(row("CardUID")) %>"  data-toggle="tooltip" data-placement="top" data-original-title="ดู/ลบ"><i class="fa fa-search text-primary" aria-hidden="true"></i></a>  
                                     <a href="ReportViewer.aspx?rpt=pay&code=<% =String.Concat(row("Code")) %>&id=<% =String.Concat(row("UID")) %>" target="_blank" data-toggle="tooltip" data-placement="top" data-original-title="พิมพ์"><i class="fa fa-print text-success" aria-hidden="true"></i></a>  

                                </td>
                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                </div>
                    </div>
                </div>
 </section>
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
                                <p><span id="spnBodyMsgCancel"></span> &nbsp;โปรดระบุเหตุผลในการยกเลิก </p>
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
