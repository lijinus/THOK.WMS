<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverStockedProductPage.aspx.cs" Inherits="Code_Statistic_OverStockedProductPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>积压产品清单</title>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <table class="OperationBar">
       <tr><td>
         <table cellpadding="0" cellspacing="0">
              <tr>
                <td>仓库
                    <asp:DropDownList ID="ddlWarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp; &nbsp; 库区
                    <asp:DropDownList ID="ddlArea" runat="server">
                    </asp:DropDownList>
                </td>
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
                  <asp:BoundField DataField="CURRENTPRODUCT" HeaderText="产品代码" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="C_PRODUCTNAME" HeaderText="产品名称" >
                      
                  </asp:BoundField>
                  <asp:BoundField DataField="CELLCODE" HeaderText="货位编码">
                      <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle Width="120px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="CELLNAME" HeaderText="货位名称">
                      <HeaderStyle Width="90px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUANTITY" HeaderText="库存数量" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITCODE" HeaderText="单位编码" >
                      <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle Width="70px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITNAME" HeaderText="单位名称" >
                      <HeaderStyle Width="70px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="INPUTDATE" HeaderText="入库日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle Width="80px" Wrap="False" />
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