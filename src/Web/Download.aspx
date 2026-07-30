<%@ Page Title="รายการเอกสารดาวน์โหลด" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Download.aspx.vb" Inherits="Kondongpu.Download" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
      <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-cloud-download icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>ดาวน์โหลดแบบฟอร์ม
                                    <div class="page-title-subheading"></div>
                                </div>
                            </div>
                        </div>
                    </div>      

<section class="content">                        
     <div class="box box-success">
        <div class="box-header with-border">
          <h2 class="box-title">รายการแบบฟอร์ม</h2>   
          <div class="box-tools pull-right">            
          </div>                       
        </div>
        <div class="box-body">
                  <table id="tbdata" class="table table-bordered table-hover">
                        <thead>
                            <tr>  
                                <th class="text-center" style="width: 30px">No.</th> 
                                <th class="text-left">แบบฟอร์ม</th>
                                <th class="text-center">ดาวน์โหลด</th> 
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each row As DataRow In dtFrm.Rows %>
                            <tr> 
                                <td class="text-center"><% =String.Concat(row("nRow")) %></td>
                                <td class="text-left"><a href="<% =String.Concat(row("Link")) %>" target="_blank" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดาวน์โหลด"><% =String.Concat(row("Descriptions")) %></a></td>
                                <td class="text-center" style="width: 30px">                                   
                                    <a href="<% =String.Concat(row("Link")) %>"  target="_blank" class="text-primary" data-toggle="tooltip" data-placement="top" data-original-title="ดาวน์โหลด"><i class="fa fa-cloud-download-alt" aria-hidden="true"></i></a>
                                </td>
                            </tr>
                            <%  Next %>
                        </tbody>
                    </table>
                                
    </div>
        <div class="box-footer">
       
        </div>
      </div> 
  </section>                  

</asp:Content>
