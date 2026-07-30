<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cane.aspx.vb" Inherits="Kondongpu.Cane" %>
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
                            <i class="lnr-leaf icon-gradient bg-primary"></i>
                        </div>
                        <div>ประเภทอ้อย
                            <div class="page-title-subheading">จัดการ เพิ่ม/แก้ไข ประเภทอ้อย </div>
                        </div>
                    </div>
                </div>
            </div> 

<section class="content">  

      <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-leaf icon-gradient bg-success">
            </i>อ้อย
            <div class="btn-actions-pane-right">               
                <asp:linkButton ID="cmdNew" runat="server"  CssClass="btn btn-primary pull-right" Width="100"><i class="fa fa-plus-circle"></i>เพิ่มใหม่</asp:linkButton>       
            </div>
        </div>     
              <div class="card-body">
<div class="row table-responsive">
<asp:GridView ID="grdData" CssClass="table table-hover" 
                             runat="server" CellPadding="2" 
                                                        GridLines="None" 
                      AutoGenerateColumns="False"  
                             Font-Bold="False" AllowPaging="True">
                        <RowStyle BackColor="#F7F7F7" />
                        <columns>
                            <asp:BoundField DataField="UID" HeaderText="Id" >
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CaneName" HeaderText="Name" >
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
    </div>        </div>
            <!-- /.box-body    <a href="LocationDetail?m=v&id=< % =String.Concat(row("UID")) %>" class="font-icon-button"><i class="fa fa-file-pdf" aria-hidden="true"></i> ดูรายละเอียด</a> -->
          </div>



   <div id="modal-window" class="modal fade modal-window"  role="dialog" data-backdrop="static" tabindex="-1" style="display: none;" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
               <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h6 class="modal-title">จัดการอ้อย</h6>
          </div>
          <div class="modal-body">                  
      <div class="row">
   <div class="col-md-2">
          <div class="form-group">
            <label>ID</label>
              <asp:TextBox ID="txtUID" runat="server" cssclass="form-control text-center"  ReadOnly="True"></asp:TextBox>
          </div>
        </div>         

             <div class="col-md-10">
          <div class="form-group">
            <label>Name</label>
              <asp:TextBox ID="txtName" runat="server" cssclass="form-control" placeholder="ชื่อ"></asp:TextBox>
          </div>
        </div>
                  <div class="col-md-12">
          <div class="form-group">
            <label>Remark</label>
              <asp:TextBox ID="txtRemark" runat="server" cssclass="form-control" placeholder=""></asp:TextBox>
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
