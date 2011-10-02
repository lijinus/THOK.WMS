<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryBillDownUnite.aspx.cs" Inherits="Code_StockDelivery_DeliveryBillDownUnite" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>分拣线下载出库单</title>
    <base target="_self" />
    <link href="../../css/css.css?t=0" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <script>
        function selectRow(objGridName,index){
           if(document.getElementById('hdnOpFlag').value=="0"){
              return;
           }
           var table=document.getElementById(objGridName);
           for(var i=1;i<table.rows.length;i++){
             table.rows[i].cells[0].innerHTML="";
           }
           table.rows[index+1].cells[0].innerHTML="<img src=../../images/arrow01.gif />";
           if(objGridName=="gvUnite"){
             document.getElementById('hdnRowIndex').value=index;
             document.getElementById('hdnScrollTop').value=document.getElementById('gvUnite').parentElement.parentElement.scrollTop;
             document.getElementById('btnReload').click();
           }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table class="OperationBar">
       <tr><td>
         <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                    日期&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="70px">
                    </asp:TextBox>
                </td>
                <td>
                    <input id="Button1" type="button" value="" onclick="setday(txtDate)" class="ButtonDate" />
                </td>
                <td>
                    &nbsp;批次号&nbsp;
                    <asp:DropDownList ID="ddlBatch" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td>
                    <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="ButtonQuery" OnClick="btnSelect_Click" />
                </td>
                <td>
                    &nbsp;仓库&nbsp;
                    <asp:DropDownList ID="ddlWarehouse" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;单据类型&nbsp;
                    <%--<asp:DropDownList ID="ddlBillType" runat="server">
                    </asp:DropDownList>--%>
                </td>
                <td>
                    <asp:TextBox ID="txtBillTypeName" runat="server" Width="70px"/>
                 </td>
                <td>
                    <asp:TextBox ID="txtBillTypeCode" runat="server" Width="0" Height="0" BorderWidth="0" />
                </td>
                <td>
                    <input id="Button2" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtBillTypeCode,txtBillTypeName','WMS_BILLTYPE','TYPECODE,TYPENAME','BUSINESS','2');" />
                    <%--OnClick="btnQuery_Click"--%>
                </td>
                <td>
                    <asp:Button ID="btnUnite" runat="server" Text="合单" CssClass="ButtonDown" OnClick="btnUnite_Click"/>
                </td>
              </tr>
         </table>
       </td></tr>
    </table>
    <div style=" height:500px; width:100%; overflow:auto;">
     <asp:GridView ID="gvUnite" runat="server" CssClass="GridStyle" style=" table-layout:fixed;" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="false" Width="100%"><%--OnRowDataBound="gvUnite_RowDataBound"--%>
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                 <%-- <asp:TemplateField HeaderText="查询明细">
                      <HeaderStyle Width="70px" />
                  </asp:TemplateField>--%>
                  <asp:BoundField DataField="ORDERDATE" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                      <HeaderStyle Width="80px" HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BATCHNO" HeaderText="批次号" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="CIGARETTECODE" HeaderText="产品代码" >
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="CIGARETTENAME" HeaderText="产品名称" >
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUANTITY" HeaderText="数量" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  
              </Columns>
          </asp:GridView>
    </div>
    <%--<table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
       <tr>
         <td>
           <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
         </td>
       </tr>
      </table>  --%>
      </ContentTemplate>
      </asp:UpdatePanel> 
    </form>
</body>
</html>
