<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingOrderStatePage.aspx.cs" Inherits="Code_Sorting_SortingOrderStatePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <table class="OperationBar" style="width:100%; height:5px;">
             <tr>
             </tr>
          </table> 
          <table style="width:100%; height:20px;" cellpadding="0" cellspacing="1" >
                    <tr>
                     <td style=" width:64px;">
                      <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="SORTING_NAME">分拣线名称</asp:ListItem>
                               <asp:ListItem Value="DELIVER_LINE_NAME">送货线路名称</asp:ListItem>
                               <asp:ListItem Value="SORT_DATE">分拣日期</asp:ListItem>
                               <asp:ListItem Value="EMPLOYEENAME">送货人名称</asp:ListItem>
                           </asp:DropDownList>
                     </td>
                     <td style="width: 150px"> <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox" Width="151px"></asp:TextBox></td>
                     <td style=" height:20px;"><input id="Button1" type="button" value="" onclick="setday(txtKeyWords)" class="ButtonDate" />
                     <asp:Button ID="btnQuery" runat="server" Text="查询"  CssClass="ButtonQuery" OnClick="btnQuery_Click"/>&nbsp;
                     </td>
                     <td class="tdTitle" style="width: 20px; height: 20px" align="left">
                          送货人名称</td>
                       <td style="height: 20px; width: 100px;"  align="left">
                            <table  cellpadding="0" cellspacing="0">
                              <tr><td style="height: 20px"><asp:TextBox ID="txtEmployeeName" runat="server" CssClass="myinput" TabIndex="1" Height="20px" ></asp:TextBox></td>
                                  <td style="height: 20px"><asp:TextBox ID="txtEmployeeCode" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                                  <td style="height: 20px"><asp:TextBox ID="txtEmployeeTel" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                                  <td style="height: 20px"><input id="Button2" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtEmployeeCode,txtEmployeeName,txtEmployeeTel','BI_EMPLOYEE','EMPLOYEECODE,EMPLOYEENAME,TEL');" /></td>
                                  <td style="height: 20px"><asp:Button ID="btnCreate" runat="server" Text="确认" CssClass="ButtonAudit" OnClick="btnCreate_Click"  /></td>
                                  <td style="height: 20px"><asp:Button ID="BtnClose" runat="server" Text="取消" CssClass="ButtonAudit2" OnClick="btnClose_Click"  /> </td>
                              </tr>
                            </table>
                       </td>
                       <td>
                        
                       </td>
                      <td align="right">
                     <asp:Button ID="btnExit" runat="server" Text="退出"  OnClientClick="Exit();" CssClass="ButtonExit" OnClick="btnExit_Click" />&nbsp;
                     </td>
                    </tr>
                  </table>
    <div style=" height:80%;" class="edit_detail_body">
           <asp:GridView ID="dgDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" CellSpacing="1" GridLines="None" Width="100%" OnRowDataBound="dgDetail_RowDataBound">
                   <Columns>
                       <asp:TemplateField>
                           <HeaderStyle Width="65px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:TemplateField>
                       <asp:BoundField DataField="SORTING_CODE" HeaderText="分拣线编码" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                       <asp:BoundField DataField="SORTING_NAME" HeaderText="分拣线名称" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                        <asp:BoundField DataField="SORT_DATE" HeaderText="分拣日期" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                          <asp:BoundField DataField="DELIVER_LINE_CODE" HeaderText="送货线路编码" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                        <asp:BoundField DataField="DELIVER_LINE_NAME" HeaderText="送货线路名称" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                        <asp:BoundField DataField="SORT_QUANTITY" HeaderText="分拣总数量" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                        <asp:BoundField DataField="SORT_ORDER_NUM" HeaderText="分拣订单数" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                        <asp:BoundField DataField="SORT_BEGIN_DATE" HeaderText="分拣开始时间" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                        <asp:BoundField DataField="SORT_END_DATE" HeaderText="分拣结束时间" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                       <asp:BoundField DataField="SORT_COST_TIME" HeaderText="分拣用时" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField >
                       <asp:BoundField DataField="EMPLOYEENAME" HeaderText="送货人" >
                           <HeaderStyle Width="160px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField >
                        <asp:TemplateField HeaderText="查询明细">
                      <HeaderStyle Width="70px" />
                     </asp:TemplateField>
                   </Columns>
                   <RowStyle BackColor="AliceBlue" Height="26px" />
                   <PagerStyle Font-Italic="False" Font-Size="10pt" Height="21px" />
                   <HeaderStyle BackColor="Gainsboro" Font-Size="10pt" ForeColor="DimGray" CssClass="GridHeader2" />
                   <AlternatingRowStyle BackColor="White" />
               </asp:GridView>
                <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px"></asp:Label>&nbsp;
    </div>
     <div>
           <asp:HiddenField ID="hdnOpFlag" runat="server" Value="0" />
       </div>
    </form>
</body>
</html>
