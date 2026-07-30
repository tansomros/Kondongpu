<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BankAccount.aspx.vb" Inherits="Kondongpu.BankAccount" %>
<%@ Import Namespace="System.Data" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function openModalMasterData(sender,uid) {	
            $('#modal-window').modal('show');
			return false;
        } 

    </script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-wallet icon-gradient bg-primary"></i>
                        </div>
                        <div>Bank Account
                            <div class="page-title-subheading">จัดการ เพิ่ม/แก้ไข บัญชีเงินฝากสำหรับโอน </div>
                        </div>
                    </div>
                </div>
            </div> 

<section class="content">  

      <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon fa fa-wallet icon-gradient bg-success">
            </i>บัญชีเงินฝาก
            <div class="btn-actions-pane-right">
                <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) <=2 Then%>
                <asp:linkButton ID="cmdNew" runat="server"  CssClass="btn btn-primary pull-right" Width="100"><i class="fa fa-plus-circle"></i>เพิ่มใหม่</asp:linkButton>        
                <% End If %>
            </div>
        </div>     
              <div class="card-body">
<div class="row table-responsive">
<asp:GridView ID="grdData" CssClass="table table-hover"  
                             runat="server" CellPadding="2" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False"  
                             Font-Bold="False">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField DataField="AccountNo" HeaderText="เลขที่บัญชี" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="AccountName" HeaderText="ชื่อบัญชี" >
                            <HeaderStyle HorizontalAlign="Left" />  
                            </asp:BoundField>
                            <asp:BoundField DataField="BankName" HeaderText="ธนาคาร" >
                            <HeaderStyle HorizontalAlign="Left" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="Branch" HeaderText="สาขา" />                                                  
                             <asp:TemplateField>
              <itemtemplate>
                    <asp:linkButton ID="imgEdit" runat="server"  Text="แก้ไข" CssClass="btn btn-primary" Width="60"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AccountNo") %>' />  
                   <asp:linkButton ID="imgDel" runat="server"  Text="ลบ" CssClass="btn btn-danger" Width="60"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AccountNo") %>' />  
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
                     </asp:GridView>                                 
    </div>        </div>
          </div>



   <div id="modal-window" class="modal fade modal-window"  role="dialog" data-backdrop="static" tabindex="-1" style="display: none;" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h6 class="modal-title">จัดการบัญชีเงินฝาก</h6>
          </div>
          <div class="modal-body">                  
      <div class="row">
   <div class="col-md-6">
          <div class="form-group">
            <label>เลขที่บัญชี</label>
              <asp:TextBox ID="txtAccountNo" runat="server" cssclass="form-control text-center"></asp:TextBox>
          </div>
        </div> 
             <div class="col-md-6">
          <div class="form-group">
            <label>ชื่อบัญชี</label>
              <asp:TextBox ID="txtAccountName" runat="server" cssclass="form-control" placeholder="ชื่อ"></asp:TextBox>
          </div>
        </div>
     </div>
 <div class="row">
       <div class="col-md-6">
          <div class="form-group">
            <label>ธนาคาร</label>
              <asp:DropDownList ID="ddlBank" runat="server" cssclass="form-control select2"></asp:DropDownList>
          </div>
        </div>
            <div class="col-md-6">
          <div class="form-group">
            <label>สาขา</label>
              <asp:TextBox ID="txtBranch" runat="server" cssclass="form-control" placeholder="คำอธิบาย"></asp:TextBox>
          </div>
        </div>
     </div> 
  <div class="row">
   <div class="col-md-12 text-center">
               <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-primary" Width="100" Text="บันทึก"></asp:Button>
          <asp:Button ID="cmdDelete" runat="server" CssClass="btn btn-danger" Width="100" Text="ลบ"></asp:Button>
  <asp:Button ID="cmdClear" runat="server" CssClass="btn btn-secondary" Width="100" Text="ยกเลิก"></asp:Button> 
       </div>
      </div>

</div>
            <div class="box-footer clearfix">
           
            </div>
          </div>
 </div>
 </div>
     
                           
</section>   
</asp:Content>
