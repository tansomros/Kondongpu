<%@ Page Title="User Account" MetaDescription="จัดการข้อมูลผู้ใช้งาน" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserModify.aspx.vb" Inherits="Kondongpu.UserModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="app-page-title">
    <div class="page-title-wrapper">
        <div class="page-title-heading">
            <div class="page-title-icon">
                <i class="pe-7s-users icon-gradient bg-success"></i>
            </div>
            <div>
                <%: Title %>
                    <div class="page-title-subheading">
                        <%: MetaDescription %>
                    </div>
            </div>
        </div>
    </div>
</div>
<section class="content">  

<div class="row">    
<section class="col-lg-7 connectedSortable">
  <div class="main-card mb-3 card">
    <div class="card-header"><i class="header-icon lnr-user icon-gradient bg-primary">
            </i>ข้อมูลทั่วไป
            <div class="btn-actions-pane-right">
            </div>
        </div>
        <div class="card-body">

      <div class="row">
            <div class="col-md-2">
          <div class="form-group">
            <label>User ID.</label>
              <asp:Label ID="lblUserID" runat="server" cssclass="form-control text-center"></asp:Label>
          </div>
        </div>        
          <div class="col-md-10">
          <div class="form-group">
            <label>Display Name</label>
            <asp:TextBox ID="txtDisplayname" runat="server" cssclass="form-control" placeholder="ชื่อ-นามสกุล"></asp:TextBox>
          </div>
        </div>
      
      </div>
      <div class="row">          
           <div class="col-md-6">
          <div class="form-group">
            <label>เบอร์โทร</label>
            <asp:TextBox ID="txtTel" runat="server" cssclass="form-control" placeholder="เบอร์โทร"></asp:TextBox>
          </div>
        </div>
        <div class="col-md-6">
          <div class="form-group">
            <label>E-mail</label>
            <asp:TextBox ID="txtEmail" runat="server" cssclass="form-control special-letter" placeholder="อีเมล"></asp:TextBox>
          </div>
        </div>
      </div>               
            
  <div class="row">
            <div class="col-md-6">
                        <div class="form-group">
                            <label>Active</label>  
        <div class="button r" id="button-1"> 
              <input id="chkStatus" type="checkbox" class="checkbox" runat="server" >          
          <div class="knobs"></div>
          <div class="layer"></div>
        </div>
                            </div>
                    </div>
   </div>
      </div>
    </div>   

</section>
    <section class="col-lg-5 connectedSortable"> 
  <div class="main-card mb-3 card">
        <div class="card-header"><i class="header-icon lnr-screen icon-gradient bg-primary">
            </i>รหัสผู้ใช้งาน
            <div class="btn-actions-pane-right">
            </div>
        </div>
        <div class="card-body">
      <div class="row">
        <div class="col-md-6">
          <div class="form-group">
            <label>Username</label>
            <asp:TextBox ID="txtUsername" runat="server" cssclass="form-control text-center" placeholder="ชื่อ Login"></asp:TextBox>
          </div>

        </div>
        <div class="col-md-6">
          <div class="form-group">
            <label>Password</label>
            <asp:TextBox ID="txtPassword" runat="server" cssclass="form-control text-center" placeholder="รหัสผ่าน"></asp:TextBox>
          </div>
      </div>
      </div>
</div>        
          </div> 
         <div class="main-card mb-3 card">
   <div class="card-header"><i class="header-icon lnr-lock icon-gradient bg-primary">
            </i>สิทธิ์การใช้งาน
            <div class="btn-actions-pane-right"> 
            </div>
        </div>
        <div class="card-body">
            <asp:RadioButtonList ID="chkGroup" runat="server">
                <asp:ListItem Value="1">Administrator</asp:ListItem>
                <asp:ListItem Value="2">Manager</asp:ListItem>
                <asp:ListItem Value="3">Officer</asp:ListItem>
                <asp:ListItem Value="4">User</asp:ListItem>
            </asp:RadioButtonList>
       
          </div>
    </div>
</section>
</div>  

  <div class="row">       
        <div class="col-md-12 text-center">
          <asp:Button ID="cmdSave" CssClass="btn btn-primary" runat="server" Text="Save" Width="120px" />
              <asp:Button ID="cmdClear" CssClass="btn btn-secondary" runat="server" Text="Clear" Width="120px" />
            <asp:Button ID="cmdDelete" CssClass="btn btn-danger" runat="server" Text="Delete" Width="120px" />
        </div>
      </div>

        </section>
</asp:Content>
