<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Price.aspx.vb" Inherits="Kondongpu.Price" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="pe-7s-calculator icon-gradient bg-success"></i>
                </div>
                <div>รายการกำหนดราคา
                </div>
            </div>
        </div>
    </div>
     
    <section class="content">        
        <div class="box box-solid">
             <div class="box-header">
              <i class="fa fa-filter"></i>
              <h3 class="box-title">ค้นหา</h3>   
                  <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                </div>
            </div>
            <div class="box-body">
                <div class="row">
                 <div class="col-lg-6 col-md-1 col-xl-1">
                     <div class="form-group">
                         <label>ปี</label>                         
                         <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" AutoPostBack="True">
                         </asp:DropDownList>
                     </div>
                 </div>
                    <div class="col-lg-6 col-md-2 col-xl-2">
                        <div class="form-group">
                            <label>Start Date</label>
                            <br />
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
                    <div class="col-lg-6 col-md-2 col-xl-2">
                        <div class="form-group">
                            <label>End Date</label>
                            <br />
                            <div class="input-group">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control text-center"
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
                            <label>ประเภทอ้อย</label> 
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control select2" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="col-lg-6 col-md-3 col-xl-3">
     <div class="form-group">
         <label>โรงงาน</label> 
         <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" AutoPostBack="True">
         </asp:DropDownList>
     </div>
 </div>
                    <div class="col-lg-6 col-md-1 col-xl-1">
                        <div class="form-group">
                            <label>สถานะ</label> 
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="-">ทั้งหมด</asp:ListItem>
                                <asp:ListItem Value="A">Active</asp:ListItem>
                                <asp:ListItem Value="D">Deactive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-12 text-center"> 
                        <asp:LinkButton ID="cmdView" runat="server" CssClass="btn btn-success" Width="120px"><i class="fa fa-search"></i>ค้นหา</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="main-card mb-3 card">
            <div class="card-header">
                <i class="header-icon lnr-list icon-gradient bg-success"></i>Price List
            <div class="btn-actions-pane-right">
                <% If Convert.ToInt32(Request.Cookies("ROLE_ID").Value) = 1 Then%>
                <a href="Price?m=new" class="btn btn-success pull-right"><i class="fa fa-plus-circle"></i>กำหนดราคาใหม่</a>
                <% End If %>
            </div>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table id="tbprice" class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th class="text-center" style="width: 60px">ปี</th>
                                <th class="text-center" style="width: 120px">รหัสโรงงาน</th>
                                <th class="text-center">ชื่อโรงงาน</th>
                                <th class="text-center">จังหวัด</th>
                                <th class="text-center">อ้อย</th>
                                <th class="text-center">วันที่เริ่ม</th>
                                <th class="text-center">วันที่สิ้นสุด</th>
                                <th class="text-center">ราคา</th>
                                <th  width="100" class="text-center">สถานะ</th>                              
                                <th class="sorting_asc_disabled sorting_desc_disabled text-center"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtPrice.Rows %>
                            <tr>
                                <td><% =String.Concat(row("PYear")) %></td>
                                <td class="text-center"> <a href="PriceDetail?id=<% =String.Concat(row("UID")) %>" data-toggle="tooltip" data-placement="top" data-original-title="ดูรายละเอียด"><% =String.Concat(row("CompanyCode")) %></a></td>
                                <td><% =String.Concat(row("CompanyName")) %></td>
                                <td><% =String.Concat(row("ProvinceName")) %></td>
                                <td><% =String.Concat(row("CaneName")) %></td>
                                <td class="text-center"><% =String.Concat(row("StartDTT")) %></td>
                                <td class="text-center"><% =String.Concat(row("EndDTT")) %></td>
                                <td class="text-center"><% =String.Concat(row("UnitPrice")) %></td>
                                <td class="text-center">
                                     <% If String.Concat(row("StatusFlag")) = "A" Then%>
   <asp:Image ID="imgStatus" runat="server" ImageUrl="images/icon-ok.png" />
 <% End If %>
                                </td>                   
                                <td class="text-center" style="width: 50px">                                   
                                    <a href="PriceDetail?id=<% =String.Concat(row("UID")) %>" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดูรายละเอียด"><i class="fa fa-edit" aria-hidden="true"></i></a>
                                </td>

                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>


    </section>
</asp:Content>
