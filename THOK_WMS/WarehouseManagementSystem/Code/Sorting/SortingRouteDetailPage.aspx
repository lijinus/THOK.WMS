<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingRouteDetailPage.aspx.cs" Inherits="Code_Sorting_SortingRouteDetailPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>分拣线数量汇总</title>
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
          <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative; left: 0px; top: 0px;" Width="99%">
       <table class="" style="height:30px;" >
       <tr ><td>
         <table cellpadding="0" cellspacing="0" >
              <tr align="left">
                 <td class="tdTitle" style="height: 29px">分拣线编码</td>
                       <td style="height: 25px; width: 100px;" >
                            <table  cellpadding="0" cellspacing="0">
                              <tr><td style="height: 24px"><asp:TextBox ID="txtSortingName" runat="server" CssClass="TextBox" TabIndex="1" Width="120px" ></asp:TextBox></td>
                                  <td ><asp:TextBox ID="txtSortingCode" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                                  <td style="height: 24px"><input id="Button2" class="ButtonBrowse2" type="button" onclick="SelectDialog2('txtSortingCode,txtSortingName','DWV_DPS_SORTING','SORTING_CODE,SORTING_NAME','ISACTIVE','1');" /></td>
                              </tr>
                            </table>
                       </td> 
                       <td class="tdTitle" style="height: 29px">订单日期</td>
                      <td style="width: 150px; height: 20px;"><asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox" Width="120px"></asp:TextBox>
                     <input id="Button1" type="button" onclick="setday(txtKeyWords)" class="ButtonDate" /></td>
                     <td style="width: 45px; height: 29px;"><asp:Button ID="btnQuery" runat="server" Text="查询"  CssClass="ButtonQuery" OnClick="btnQuery_Click"/>&nbsp;
                     </td>
                     <td style="width:60px;"></td>
                <td align="left" style="height: 29px">
                   <asp:Button ID="btnExcel" runat="server" CssClass="ButtonDown" Text="导出Excel" OnClick="btnExcel_Click" />
                </td>
                <td align="left" style="height: 29px">
                   <asp:Button ID="btnReturn" runat="server" CssClass="ButtonBack" Text="返回" OnClick="btnReturn_Click" />
                </td>
              </tr>
              <tr>
              </tr>
         </table>
       </td></tr>
    </table>
    </asp:Panel>
        <asp:GridView ID="dgDetail" runat="server" CssClass="GridStyle" style=" table-layout:fixed;" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False" Width="100%"  ><%--OnSelectedIndexChanged="gvStock_SelectedIndexChanged"--%>
        <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="PRODUCTCODE" HeaderText="产品代码" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                      <HeaderStyle Width="130px" />
                       <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QTY_JIAN" HeaderText="数量(件)" >
                      <HeaderStyle Width="130px" />
                       <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QTY_TIAO" HeaderText="数量(条)" >
                      <HeaderStyle Width="130px" />
                       <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                   <asp:BoundField DataField="SORTING_CODE" HeaderText="分拣线编码" >
                      <HeaderStyle Width="130px" />
                       <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SORTING_NAME" HeaderText="分拣线名称" >
                      <HeaderStyle Width="130px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>  <%--
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
