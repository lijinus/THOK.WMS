<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageDetailQueryPage.aspx.cs" Inherits="Code_Statistic_StorageDetailQueryPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>库存明细账</title>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <script src="../../JScript/Calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" height:490px; width:880px; overflow:auto; background-color:WhiteSmoke;">
     <asp:GridView ID="gvStorage" runat="server" CssClass="GridStyle"  CellPadding="3" CellSpacing="1" 
                   BorderWidth="0px" AutoGenerateColumns="False" Width="880px" OnRowDataBound="gvStorage_RowDataBound">
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="BILLDATE" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False">
                      <HeaderStyle Width="80px" HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BILLNO" HeaderText="单据编号">
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BUSINESSTYPE" HeaderText="单据业务">
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTCODE" HeaderText="产品代码" >
                      <HeaderStyle Width="100px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                  <HeaderStyle Width="120px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUANTITY" HeaderText="账面数量" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QTY_JIAN" HeaderText="数量(自然件)" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="120px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QTY_TIAO" DataFormatString="{0:N2}" HeaderText="数量(条)" HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="100px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITNAME" HeaderText="单据单位">
                  <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" /> 
                      </asp:BoundField>
              </Columns>
          </asp:GridView>
    </div>
    <table id="paging" cellpadding="0" cellspacing="0" style="width:880px;">
       <tr>
         <td>
           <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
         </td>
       </tr>
      </table>  
    </form>
</body>
</html>
