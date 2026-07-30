<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CompanyModify.aspx.vb" Inherits="Kondongpu.CompanyModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="lnr-apartment icon-gradient bg-green"></i>
                </div>
                <div>
                    ข้อมูลโรงงาน/บริษัท
					<div class="page-title-subheading">จัดการรายละเอียดข้อมูลโรงงาน/บริษัท </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Main content -->
    <section class="content">
        <div class="row">
           
                    <div class="col-lg-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <i class="header-icon lnr-apartment icon-gradient bg-success"></i>ข้อมูลโรงงาน/บริษัท<asp:HiddenField ID="hdCompanyUID" runat="server" />
                                <div class="btn-actions-pane-right actions-icon-btn">                                   
                                </div>
                            </div>

                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="p-2">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>รหัสโรงงาน/โควต้า</label>
                                                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control text-blue text-bold text-center"></asp:TextBox>
                                                    </div>
                                                </div>  
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>ชื่อโรงงาน</label>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div> 
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>ชื่อเรียกสั้นๆ</label>
                                                        <asp:TextBox ID="txtAliasName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>ชื่อเจ้าของโควต้า</label>
                                                        <asp:TextBox ID="txtOwnerName" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                              
                                            </div>
                                            <div class="row">

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>บ้านเลขที่</label>
                                                        <asp:TextBox ID="txtAddressNo" runat="server" CssClass="form-control" placeholder="เลขที่ตั้ง/บ้านเลขที่"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <label>หมู่</label>
                                                        <asp:TextBox ID="txtMoo" runat="server" CssClass="form-control" placeholder="หมู่"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>หมู่บ้าน/ถนน</label>
                                                        <asp:TextBox ID="txtRoad" runat="server" CssClass="form-control" placeholder="ถนน/ซอย"></asp:TextBox>
                                                    </div>
                                                </div>
                                           
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
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>รหัสไปรษณีย์</label>
                                                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>เลขผู้เสียภาษี</label>
                                                        <asp:TextBox ID="txtTaxId"  MaxLength="13" runat="server" CssClass="form-control text-center"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>เบอร์โทร</label>
                                                        <asp:TextBox ID="txtTel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>  
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Fax</label>
                                                        <asp:TextBox ID="txtFax" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>E-mail</label>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control special-letter"></asp:TextBox>
                                                    </div>
                                                </div>                                             
                                                 <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Website</label>
                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>                                              
    
                                            </div>
                                            <div class="row">                                              
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

        <div class="row">
            <div class="col-lg-12">
                <div class="justify-content-center">
                    <div class="row justify-content-center">
                        <asp:Button ID="cmdSave" runat="server" Width="100" CssClass="btn btn-primary" Text="บันทึก" />
                        <asp:Button ID="cmdCancel" runat="server" Width="100" CssClass="btn btn-secondary" Text="ยกเลิก" />
                        <asp:Button ID="cmdDelete" runat="server" Width="100" CssClass="btn btn-danger" Text="ลบ" />
                        <a href="Company.aspx?m=c" class="btn btn-secondary">กลับหน้ารายการโรงงาน/บริษัท</a>
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
