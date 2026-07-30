<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CustomerModify.aspx.vb" Inherits="Kondongpu.CustomerModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-user-female icon-gradient bg-green"></i>
                </div>
                <div>
                    ข้อมูลลูกค้า
					<div class="page-title-subheading">จัดการรายละเอียดข้อมูลลูกค้า </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <section class="col-lg-8 connectedSortable">
                <div class="justify-content-center">
                    <div class="col-lg-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <i class="header-icon pe-7s-user-female icon-gradient bg-success"></i>ข้อมูลลูกค้า<asp:HiddenField ID="hdCustomerUID" runat="server" />
                                <div class="btn-actions-pane-right actions-icon-btn">
                                    <asp:Label ID="lblNewCode" runat="server" CssClass="small"></asp:Label>
                                </div>
                            </div>

                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="p-2">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>รหัสลูกค้า</label>
                                                        <asp:TextBox ID="txtCustomerID" runat="server" CssClass="form-control text-blue text-bold text-center" ReadOnly="true" Enabled="false" BackColor="White"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>เลขบัตรประชาชน</label>
                                                        <asp:TextBox ID="txtCardID"  MaxLength="13" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>ทะเบียนชาวไร่</label>
                                                        <asp:TextBox ID="txtFamerCode" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>คำนำหน้า</label>
                                                        <asp:DropDownList ID="ddlPrefix" runat="server"  CssClass="form-control select2"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label>ชื่อ</label>
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label>นามสกุล</label>
                                                        <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>ชื่อเล่น</label>
                                                        <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>อายุ</label>
                                                        <asp:TextBox ID="txtAge" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>เพศ</label>
                                                        <asp:RadioButtonList ID="optSex" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="M">ชาย</asp:ListItem>
                                                            <asp:ListItem Value="F">หญิง</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label>บ้านเลขที่</label>
                                                        <asp:TextBox ID="txtAddressNo" runat="server" CssClass="form-control" placeholder="เลขที่ตั้ง/บ้านเลขที่"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>หมู่</label>
                                                        <asp:TextBox ID="txtMoo" runat="server" CssClass="form-control" placeholder="หมู่"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label>หมู่บ้าน/ถนน</label>
                                                        <asp:TextBox ID="txtRoad" runat="server" CssClass="form-control" placeholder="ถนน/ซอย"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>แขวง/ตำบล</label>
                                                        <asp:TextBox ID="txtSubDistrict" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>เขต/อำเภอ</label>
                                                        <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>จังหวัด</label><br />
                                                        <asp:DropDownList CssClass="form-control select2" ID="ddlProvince" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>รหัสไปรษณีย์</label>
                                                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>เบอร์โทร</label>
                                                        <asp:TextBox ID="txtTel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>E-mail</label>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control special-letter"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Line ID</label>
                                                        <asp:TextBox ID="txtLineID" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>เลขที่บัญชีเงินฝาก</label>
                                                        <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                  <div class="col-md-4">
          <div class="form-group">
            <label>ชื่อบัญชี</label>
              <asp:TextBox ID="txtAccountName" runat="server" cssclass="form-control"></asp:TextBox>
          </div>
        </div>
                                                  <div class="col-md-4">
          <div class="form-group">
            <label>ธนาคาร</label>
              <asp:DropDownList ID="ddlBank" runat="server" cssclass="form-control select2"></asp:DropDownList>
          </div>
        </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-4 col-xl-4">
                                                    <div class="form-group">
                                                        <label>ลูกค้าโปรแกรมสินเชื่อ</label>
                                                        <div class="button r" id="button-1">
                                                            <input id="chkLoan" type="checkbox" class="checkbox" runat="server" checked="checked">
                                                            <div class="knobs"></div>
                                                            <div class="layer"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-4 col-xl-4">
                                                    <div class="form-group">
                                                        <label>ลูกค้าโปรแกรมอ้อย</label>
                                                        <div class="button r" id="button-2">
                                                            <input id="chkCane" type="checkbox" class="checkbox" runat="server">
                                                            <div class="knobs"></div>
                                                            <div class="layer"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-4 col-xl-2">
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
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>


                </div>
            </section>

            <section class="col-lg-4 connectedSortable">
                <div id="pnCar" runat="server" class="box box-solid"> 
                            <div class="box-header">
                         <i class="header-icon pe-7s-car icon-gradient bg-mixed-hopes"></i>
                        ข้อมูลทะเบียนรถ
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                </div>
                    </div>
                    <div class="box-body">
                        <asp:UpdatePanel ID="UpdatePanelCarAdd" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <label>หมายเลขทะเบียนรถ</label>
                                            <asp:TextBox ID="txtCarRegis" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="cmdAddCar" runat="server" Text="เพิ่ม"   CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="cmdAddCar" />
                                <asp:AsyncPostBackTrigger ControlID="grdCar" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UpdatePanelCarList" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdCar" CssClass="table table-hover" runat="server" CellPadding="2" GridLines="None" AutoGenerateColumns="False" Font-Bold="False">
                                            <RowStyle BackColor="#F7F7F7" />
                                            <Columns>
                                                <asp:BoundField DataField="nRow" HeaderText="No.">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RegisNumber" HeaderText="ทะเบียนรถ">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="ลบ">
                                                    <ItemTemplate>
                                                         <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/delete.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />                                                         
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle HorizontalAlign="Center"
                                                CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle CssClass="th" Font-Bold="True" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>

                                        <asp:PostBackTrigger ControlID="cmdAddCar" />
                                        <asp:AsyncPostBackTrigger ControlID="grdCar" EventName="RowCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>


                <div id="pnDocument" runat="server" class="box box-solid">                    
                         <div class="box-header">
                         <i class="header-icon pe-7s-ticket icon-gradient bg-mixed-hopes"></i>
                        เอกสารรับฝาก
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>ชื่อเอกสาร</label>
                                    <asp:DropDownList ID="ddlDocument" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>    
                              <div class="col-md-5">
                                <div class="form-group">
                                    <label>เลขที่อ้างอิง</label>
                                     <asp:TextBox ID="txtDocDesc" runat="server" CssClass="form-control" placeholder="เลขทะเบียนรถ/เลขที่ฉโนด"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label></label>
                                    <br />
                                    <asp:Button ID="cmdAddDoc" CssClass="btn btn-success" runat="server" Text="เพิ่ม" />
                                </div>

                            </div>
                        </div>
                          <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanelDocument" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdDocument" CssClass="table table-hover" runat="server" CellPadding="0" GridLines="None"  AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="nRow" HeaderText="No.">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DocumentName" HeaderText="รายการเอกสาร" />
                                            <asp:TemplateField HeaderText="ลบ">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgDel" runat="server" ImageUrl="images/delete.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UID") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle HorizontalAlign="Center"
                                            CssClass="dc_pagination dc_paginationC dc_paginationC11" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle CssClass="th" Font-Bold="True"
                                            VerticalAlign="Middle" HorizontalAlign="Left" />


                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="cmdAddDoc" />
                                    <asp:AsyncPostBackTrigger ControlID="grdDocument" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

   </div>
                    </div>

                </div>

            </section>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="justify-content-center">
                    <div class="row justify-content-center">
                        <asp:Button ID="cmdSave" runat="server" Width="100" CssClass="btn btn-primary" Text="บันทึก" />
                        <asp:Button ID="cmdCancel" runat="server" Width="100" CssClass="btn btn-secondary" Text="ยกเลิก" />
                        <asp:Button ID="cmdDelete" runat="server" Width="100" CssClass="btn btn-danger" Text="ลบ" />
                        <a href="Customer.aspx?m=c" class="btn btn-secondary">กลับหน้ารายการลูกค้า</a>
                    </div>

                    <br />
                </div>
            </div>
        </div>

        <!-- Modal HTML > -->
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
    </section>
</asp:Content>
