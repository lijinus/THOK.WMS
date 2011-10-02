<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RealTimeStock.aspx.cs" Inherits="Code_Statistic_RealTimeStock" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>实时库存查询</title>
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
                  <%--<asp:DropDownList ID="ddlWAREHOUSE" runat="server" >
                     <asp:ListItem Selected="True" Value="WH_CODE">配送中心</asp:ListItem>
                  </asp:DropDownList>--%>
                </td>
                
                <td>
                  <%--<asp:DropDownList ID="ddlAREACODE" runat="server">
                     <asp:ListItem Selected="True" Value="AREACODE">主库区</asp:ListItem>
                     <asp:ListItem Value="AREACODE">零烟区</asp:ListItem>
                     <asp:ListItem Value="AREACODE">备货区</asp:ListItem>
                     <asp:ListItem Value="AREACODE">暂存区</asp:ListItem>
                     <asp:ListItem Value="AREACODE">损烟区</asp:ListItem>
                  </asp:DropDownList>--%>
                </td>
                <td class="tdTitle">仓库库区</td>
                <td >
                    <table  cellpadding="0" cellspacing="0">
                       <tr>
                             <td style="height: 24px"><asp:TextBox ID="txtAreaName" runat="server" CssClass="TextBox" TabIndex="1" ></asp:TextBox></td>
                           <td style="height: 24px"><asp:TextBox ID="txtAreaCode" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                           <td style="height: 24px"><input id="Button2" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtAreaCode,txtAreaName','WMS_WH_AREA','AREACODE,AREANAME','AREANAME','1');" /></td>
                       </tr>
                    </table>
                </td>         
                <td>
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
    <div style=" height:500px; width:100%; overflow:auto;">
     <asp:GridView ID="gvStock" runat="server" CssClass="GridStyle" style=" table-layout:fixed;" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False" Width="100%" ><%--OnRowDataBound="gvStorage_RowDataBound"--%>
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="CURRENTPRODUCT" HeaderText="产品代码" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUANTITY" HeaderText="产品数量" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITCODE" HeaderText="单位编码" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITNAME" HeaderText="单位名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="AREANAME" HeaderText="库区" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
              </Columns>
          </asp:GridView>
    </div>
    <div class="edit_detail_header">
         <table cellpadding="0"  cellspacing="0">
            <tr>
               <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                   <asp:Label ID="lblNumber" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
            </table>
       </div>
    <table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
       <tr>
         <td>
           <%--<NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>--%>
         </td>
       </tr>
      </table>  
      </ContentTemplate>
     </asp:UpdatePanel>
</form>
</body>
</html>
