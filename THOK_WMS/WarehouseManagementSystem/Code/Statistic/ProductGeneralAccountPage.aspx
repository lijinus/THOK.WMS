<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGeneralAccountPage.aspx.cs" Inherits="Code_Statistic_ProductGeneralAccountPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>产品总账查询</title>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <script src="../../JScript/Calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="OperationBar">
       <tr><td>
         <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                    &nbsp; 日期&nbsp;
                </td>
                <td><asp:TextBox ID="txtStartDate" runat="server" Width="70px"></asp:TextBox><input id="Button1" type="button" value="" onclick="setday(txtStartDate)" class="ButtonDate" /></td>
                <td>
                    &nbsp;至&nbsp;
                </td>
                <td><asp:TextBox ID="txtEndDate" runat="server" Width="70px"></asp:TextBox><input id="Button2" type="button" value="" onclick="setday(txtEndDate)" class="ButtonDate" /></td>
                <td><asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
                </td>
                <td></td>
              </tr>
         </table>
       </td></tr>
    </table>
    <div style=" height:500px; width:100%; overflow:auto;">
     <asp:GridView ID="gvStorage" runat="server" CssClass="GridStyle" style=" table-layout:fixed;" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvStorage_RowDataBound">
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:TemplateField>
                      <HeaderStyle Width="70px" />
                  </asp:TemplateField>
                  <asp:BoundField DataField="SETTLEDATE" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                      <HeaderStyle Width="80px" HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTCODE" HeaderText="产品代码" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BEGINNING" HeaderText="期初量(件)" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BEGINNING_TIAO" HeaderText="期初量(条)" DataFormatString="{0:N2}" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENTRYAMOUNT" HeaderText="入库量(件)" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENTRY_TIAO" HeaderText="入库量(条)" DataFormatString="{0:N2}" HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="DELIVERYAMOUNT" HeaderText="出库量(件)" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="DELIVERY_TIAO" HeaderText="出库量(条)" DataFormatString="{0:N2}" HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PROFITAMOUNT" HeaderText="盘盈量(件)" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PROFIT_TIAO" HeaderText="盘盈量(条)" DataFormatString="{0:N2}" HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LOSSAMOUNT" HeaderText="盘亏量(件)" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LOSS_TIAO" HeaderText="盘亏量(条)" DataFormatString="{0:N2}" HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENDING" HeaderText="日结量(件)" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENDING_TIAO" HeaderText="日结量(条)" DataFormatString="{0:N2}" HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
              </Columns>
          </asp:GridView>
    </div>
    <table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
       <tr>
         <td>
           <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
         </td>
       </tr>
      </table>  
    </form>
</body>
</html>
