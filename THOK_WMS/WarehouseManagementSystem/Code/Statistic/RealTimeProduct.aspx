<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RealTimeProduct.aspx.cs" Inherits="Code_Statistic_RealTimeProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../css/css.css?t=9" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
           <Scripts>
              <asp:ScriptReference  Path="~/JScript/ajax_validate.js?t=122901"/>          
           </Scripts>
           <Services>
              <asp:ServiceReference Path="~/WebServices/Validate.asmx" />
           </Services>          
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
         <Triggers>
       <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
         <ContentTemplate>
             <table class="OperationBar">
       <tr><td>
         <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                 
                </td>
                
                <td>
                </td>
                <td style="width: 76px" >
                    <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="CURRENTPRODUCT">卷烟编码</asp:ListItem>
                               <asp:ListItem Value="C_PRODUCTNAME">卷烟名称</asp:ListItem>
                           </asp:DropDownList>
                </td>
                <td>
                   <asp:TextBox ID="txtAreaName" runat="server" CssClass="TextBox" TabIndex="1" ></asp:TextBox>
                   <asp:Button ID="btnQuery" runat="server" CssClass="ButtonQuery" Text="查询" OnClick="btnQuery_Click" />
                </td>
                <td align="right">
                   <asp:Button ID="btnExcel" runat="server" CssClass="ButtonDown" Text="导出Excel" OnClick="btnExcel_Click" />
                </td>
                <td>
                   <asp:Button ID="btnReturn" runat="server" CssClass="ButtonBack" Text="返回" OnClick="btnReturn_Click" />
                </td>
              </tr>
              <tr>
              </tr>
         </table>
       </td></tr>
    </table>
        <asp:GridView ID="gvStock" runat="server" CssClass="GridStyle" style=" table-layout:fixed;" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False" Width="100%" ><%--OnRowDataBound="gvStorage_RowDataBound"--%>
        <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="CURRENTPRODUCT" HeaderText="产品代码" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="C_PRODUCTNAME" HeaderText="产品名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUY_JIAN" HeaderText="库存数量(件)" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUY_TIAO" HeaderText="库存数量(条)" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUY_ZHI" HeaderText="库存数量(总支)" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField><%-- 
                  <asp:BoundField DataField="UNITNAME" HeaderText="单位名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>--%>
              </Columns>
        </asp:GridView>
        </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
