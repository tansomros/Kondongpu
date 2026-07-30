<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AgreementNew.aspx.vb" Inherits="Kondongpu.AgreementNew" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="app-page-title">
        <div class="page-title-wrapper">
            <div class="page-title-heading">
                <div class="page-title-icon">
                    <i class="lnr-highlight icon-gradient bg-success"></i>
                </div>
                <div>เขียนสัญญาใหม่</div>
            </div>
        </div>
    </div>
     
    <section class="content">   
         <div class="row">
                <div class="col-md-4">          
          <div class="small-box bg-success">
            <div class="inner">
              <a href="Agreement.aspx?m=ag&t=1"><h3>สัญญาเงินกู้</h3></a>
              <p></p>
            </div>
            <div class="icon">
              <i class="lnr-gift"></i>
            </div>
            <a href="Agreement.aspx?m=ag&t=1" class="small-box-footer">Create <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-md-4">          
          <div class="small-box bg-primary">
            <div class="inner">
              <a href="Agreement.aspx?m=ag&t=2"><h3>สัญญาเช่า-ซื้อ</h3></a>
              <p></p>
            </div>
            <div class="icon">
              <i class="lnr-car"></i>
            </div>
            <a href="Agreement.aspx?m=ag&t=2" class="small-box-footer">Create <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>        
        <div class="col-md-4">          
          <div class="small-box bg-purple">
            <div class="inner">
              <a href="Agreement.aspx?m=ag&t=3"><h3>สัญญาซื้อขาย<sup style="font-size: 20px"></sup></h3></a>
              <p></p>
            </div>
            <div class="icon">
              <i class="lnr-cart"></i>
            </div>
            <a href="Agreement.aspx?m=ag&t=3" class="small-box-footer">Create <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
     </div>
    </section>
</asp:Content>
