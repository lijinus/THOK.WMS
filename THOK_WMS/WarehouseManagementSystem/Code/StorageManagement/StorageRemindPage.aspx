<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageRemindPage.aspx.cs" Inherits="Code_StorageManagement_StorageRemindPage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>库存安全提醒</title>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
          <span style=" width:100%; text-align:center; font-size:17pt; height:35px; padding-top:5px;">库存安全存量警示表</span>
          <asp:GridView ID="gvRemind" runat="server" Style="position: relative;table-layout:fixed;width:96%;"
             OnRowDataBound="gvMain_RowDataBound" CssClass="GridStyle" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False">
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="PRODUCTCODE" HeaderText="产品代码" >
                      <HeaderStyle Width="70px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="MAX_LIMITED" HeaderText="库存上限" >
                      <HeaderStyle Width="70px" />
                      <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                  </asp:BoundField>
                  <asp:BoundField DataField="MIN_LIMITED" HeaderText="库存下限">
                      <HeaderStyle Width="70px" />
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QTY_JIAN" HeaderText="实际库存" DataFormatString="{0:N2}">
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField HeaderText="预警描述" >
                      <HeaderStyle Width="100%" />
                  </asp:BoundField>
              </Columns>
          </asp:GridView>
    </div>
    </form>
</body>
</html>
