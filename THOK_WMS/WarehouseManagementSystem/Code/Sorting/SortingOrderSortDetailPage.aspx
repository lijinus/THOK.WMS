<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingOrderSortDetailPage.aspx.cs" Inherits="Code_Sorting_SortingOrderSortDetailPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>分拣情况表</title>
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
      <ContentTemplate>
        <asp:GridView ID="dgDetail" runat="server" CssClass="GridStyle" style="table-layout:fixed;" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False" Width="100%" >
        <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="CUST_CODE" HeaderText="客户编码" >
                      <HeaderStyle Width="80px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  
                  <asp:BoundField DataField="CUST_NAME" HeaderText="客户名称" >
                      <HeaderStyle Width="280px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="QUANTITY_SUM" HeaderText="数量" >
                      <HeaderStyle Width="80px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SORT_BEGIN_TIME" HeaderText="分拣开始时间" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SORT_END_TIME" HeaderText="分拣结束时间" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField><%--
                  <asp:BoundField DataField="QUY_ZHI" HeaderText="分拣用时" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField> 
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
