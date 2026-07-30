<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agreement.aspx.vb" Inherits="Kondongpu.Agreement" %>

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
                    รายละเอียดสัญญา
                            <div class="page-title-subheading"></div>
                </div>
            </div>
        </div>
    </div>

    <!-- Main content -->
    <section class="content">
        <div id="pnCancel" runat="server" class="alert alert-danger">
            <h4>สัญญานี้มีสถานะ <b>ยกเลิกสัญญา</b> แล้ว ท่านไม่สามารถดำเนินการใดๆได้อีก</h4>
        </div>

        <div class="row">
            <section class="col-lg-7 connectedSortable">
                <div class="main-card mb-3 card">
                     <div class="card-header">    ข้อมูลสัญญา
                        <asp:HiddenField ID="hdAgreementUID" runat="server" />
            <div class="btn-actions-pane-right">
              สถานะ : <asp:Label ID="lblStatus" runat="server" CssClass="badge badge-success" Text="ปกติ"></asp:Label>   
            </div>
            </div> 
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>เลขที่สัญญา</label>
                                    <asp:TextBox ID="txtAgreementNo" runat="server" CssClass="form-control text-center text-blue text-bold" BackColor="#CEE7FF"></asp:TextBox>      
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>ประเภทสัญญา</label><asp:HiddenField ID="hdAgreementTypeUID" runat="server" />
                                    <asp:Label ID="lblAgreementType" runat="server" CssClass="form-control text-center text-success" Text=""></asp:Label>                                   
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>วันที่ทำสัญญา</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAgreementDate" runat="server" CssClass="form-control text-center"
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
                                    <label>วันที่ครบกำหนด</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtDuedate" runat="server" CssClass="form-control text-center"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-language="th-th" data-provide="datepicker"
                                            onkeyup="chkstr(this,this.value)"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fa lnr-calendar-full"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>ผู้ให้กู้/ผู้ให้เช่าซื้อ/ผู้ขาย</label>
                                     <asp:DropDownList ID="ddlCreditor" runat="server" CssClass="form-control select2">
                                        <asp:ListItem Selected="True" Value="1">นายสมส่วน ลานอก</asp:ListItem>
                                        <asp:ListItem Value="2">นายธีรพงศ์ ลานอก</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>สถานที่ทำสัญญา/ทำขึ้นที่</label>
                                    <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control" Text="ฅนดงพุ"></asp:TextBox>
                                </div>
                            </div>
</div>     
                
                        <div id="pnArg2" class="row" runat="server">
                                  <div class="col-md-6">
                                <div class="form-group">
                                    <label>สินค้า</label>
                                    <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                  <div class="col-md-6">
                                <div class="form-group">
                                    <label>เพื่อจุดประสงค์</label>
                                    <asp:TextBox ID="txtObjective" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                               </div>    

   <div id="pnArg1" class="row" runat="server">
     <div class="col-md-4">
                                <div class="form-group">
                                    <label>จำนวนวงเงิน</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control text-center text-blue text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>อัตราดอกเบี้ย</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control text-center text-danger text-bold" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">%</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-5" id="pnBalance"  runat="server">
                                <div class="form-group">
                                    <label>คงเหลือ</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtBalance" runat="server" ReadOnly="true" CssClass="form-control text-center text-success text-bold" BackColor="White" Font-Size="16px"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">฿</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                                  
                    </div>
                    <div class="card-footer"> 
                    </div>
                </div>

                <div class="main-card mb-3 card">
                    <div class="card-header">ข้อมูลลูกค้า                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>เลือกลูกค้า</label><br />
                                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true"  CssClass="form-control btn btn-primary select2"></asp:DropDownList>
                                </div>
                            </div>
    </div> 
                        <asp:UpdatePanel ID="UpdatePanelCustomer" runat="server">
                            <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>รหัสลูกค้า</label><asp:HiddenField ID="hdCustomerUID" runat="server" />
                                    <asp:TextBox ID="txtCustomerID" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>ชื่อ-นามสกุล</label>
                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                          <div class="col-md-3">
                                <div class="form-group">
                                    <label>ชื่อเล่น</label>
                                    <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>  
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>เลขบัตรประชาชน</label>
                                    <asp:TextBox ID="txtCardID" MaxLength="13" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>                          
   <div class="col-md-6">
                                <div class="form-group">
                                    <label>เบอร์โทร</label>
                                    <asp:TextBox ID="txtTel" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                        <div class="row">

                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ที่อยู่</label>
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCustomer" EventName="SelectedIndexChanged" />
                            </Triggers>

                        </asp:UpdatePanel>   
                    </div>
                    <div class="box-footer clearfix">
                    </div>
                </div>
             

                </section>
            <section class="col-lg-5 connectedSortable">
                <div class="main-card mb-3 card">
                    <div class="card-header">หลักค้ำประกัน</div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-10">
                                <div class="form-group">
                                    <label>รายการหลักค้ำประกัน</label>
                                    <asp:TextBox ID="txtDescritions" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                   <br />
                                    <asp:Button ID="cmdAdd" CssClass="btn btn-success" runat="server" Text="เพิ่ม" Width="60px" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="grdGuarantee" CssClass="table table-hover"   runat="server" CellPadding="0"      GridLines="None"   AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="nRow" HeaderText="No.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="Descriptions" HeaderText="รายการหลักค้ำประกัน">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                   
                                    <asp:TemplateField HeaderText="ลบ">
                                        <ItemTemplate>
                                           <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/delete.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle HorizontalAlign="Center"  CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle CssClass="th" Font-Bold="True"   VerticalAlign="Middle" HorizontalAlign="Left" />
                            </asp:GridView>

                        </div>


                    </div>

                </div>
                <div class="main-card mb-3 card">
                    <div class="card-header">
                        บุคคลค้ำประกัน
                    </div>
                    <div class="card-body">
               
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="text-bold text-blue"><label>1.ชื่อผู้ค้ำประกัน</label></div>
                                    <asp:TextBox ID="txtG1Name" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>อายุ</label>
                                    <asp:TextBox ID="txtG1Age" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label>เบอร์โทร</label><br />
                                    <asp:TextBox ID="txtG1Tel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ที่อยู่</label>
                                    <asp:TextBox ID="txtG1Address" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="text-bold text-blue"> <label>2.ชื่อผู้ค้ำประกัน</label></div>
                                    <asp:TextBox ID="txtG2Name" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>อายุ</label>
                                    <asp:TextBox ID="txtG2Age" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label>เบอร์โทร</label><br />
                                    <asp:TextBox ID="txtG2Tel" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>ที่อยู่</label>
                                    <asp:TextBox ID="txtG2Address" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- Modal HTML -->
                <div id="modal-window-send" class="modal fade" role="dialog" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <div class="modal-title"><span id="spnHeader"></span></div>
                            </div>
                            <div class="modal-body">
                                <h4>
                                    <p><span id="spnBodyMsg"></span>.</p>

                                </h4>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="cmdConfirm" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="100" />
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ปิด</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="modal-window-changetype" class="modal fade" role="dialog" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <div class="modal-title"><span id="spnHeaderChangeType"></span></div>
                            </div>
                            <div class="modal-body">
                                <p><span id="spnBodyMsgChangeType"></span>ประเภทสัญญา </p>
                                <asp:DropDownList ID="ddlAType" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="cmdSaveChangeType" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="100" />
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ปิด</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="modal-window-overview" class="modal fade" role="dialog" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <div class="modal-title"><span id="spnHeaderOverview"></span></div>
                            </div>
                            <div class="modal-body">
                                <p><span id="spnBodyMsgOverview"></span></p>
                                <asp:GridView ID="grdOverview" CssClass="table table-hover" runat="server" CellPadding="2" GridLines="None" AutoGenerateColumns="False" Font-Bold="False">
                                    <Columns>
                                        <asp:BoundField DataField="CWhen" HeaderText="Date/time">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="Remark"></asp:BoundField>
                                        <asp:BoundField DataField="StatusName" HeaderText="Status"></asp:BoundField>
                                        <asp:BoundField DataField="DisplayName" HeaderText="User"></asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center"
                                        CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                                    <HeaderStyle Font-Bold="True" />
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ปิด</button>
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
                                <p><span id="spnBodyMsgCancel"></span>โปรดระบุเหตุผลในการยกเลิก </p>
                                <asp:TextBox ID="txtCancelRemark" runat="server" CssClass="form-control" placeholder="ระบุเหตุผล"></asp:TextBox>

                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="cmdConfirmCancel" runat="server" class="btn btn-primary" Text="ยืนยัน" Width="100" />
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ปิด</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="modal-window-alert" class="modal fade" role="dialog" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <div class="modal-title"><span id="spnHeaderAlert"></span></div>
                            </div>
                            <div class="modal-body">
                                <p><span id="spnBodyMsgAlert"></span></p>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary pull-right" data-dismiss="modal" width="100">ปิด</button>
                            </div>
                        </div>
                    </div>
                </div>

                <!--- End Modal --->
            </section>
        </div>    

        <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="บันทึก" Width="100px" />
                <asp:Button ID="cmdCancel" CssClass="btn btn-secondary" runat="server" Text="ยกเลิกสัญญา" Width="120px" />
                <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete" Width="120px" />
                <asp:Button ID="cmdPrintContract" CssClass="btn btn-success" runat="server" Text="พิมพ์สัญญา" Width="100px" />
                 <asp:Button ID="cmdPrintGuarantee" CssClass="btn btn-success" runat="server" Text="พิมพ์สัญญาค้ำประกัน"  />
                 <asp:Button ID="cmdPrintDebt" CssClass="btn btn-success" runat="server" Text="พิมพ์หนังสือรับสภาพหนี้" />
            </div>
        </div>

    </section>
</asp:Content>
