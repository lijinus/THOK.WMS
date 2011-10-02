<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterAndDeliveryBillQuery.aspx.cs" Inherits="Code_Statistic_EnterAndDeliveryBillQuery" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>产品单据查询</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>  
    <div>
    <table style="width:100%;font-size:12px; " cellpadding="0" cellspacing="0">
       <tr>
         <td>
            <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
               <table style="width:100%; height:20px;">
                  <tr>
                    <td style="height: 22px">
                       <asp:TextBox ID="txtProductCode" runat="server" CssClass="TextBox" Width="140px"></asp:TextBox>
                       <input id="Button2" class="ButtonBrowse2" onclick="SelectDialog2('txtProductCode','WMS_PRODUCT','PRODUCTCODE','PRODUCTCODE',txtProductCode.value);" type="button" value="" tabindex="1" />
                       <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                    </td>
                  </tr>
               </table>            
            </asp:Panel>
         </td>
       </tr>
       <tr>
         <td>
           <div style="height:210px;" class="edit_body">
              <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" CellSpacing="1" GridLines="None" Width="100%" OnRowDataBound="gvMain_RowDataBound">
                  <Columns>
                     <asp:TemplateField>
                        <HeaderStyle Width="15px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </asp:TemplateField>
                     <asp:BoundField DataField="BillNo" HeaderText="单据号" >
                        <HeaderStyle Width="120px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </asp:BoundField>
                     <%--<asp:BoundField DataField="WH_NAME" HeaderText="仓库名称">
                        <HeaderStyle Width="100px" />
                     </asp:BoundField>--%>
                     <asp:BoundField DataField="BILLDATE" HeaderText="制单日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                        <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </asp:BoundField>
                     <asp:BoundField DataField="OPERATEPERSON" HeaderText="操作员" >
                        <HeaderStyle Width="65px" />
                        <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="STATUS" HeaderText="处理状态" >
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </asp:BoundField>
                     <asp:BoundField DataField="VALIDATEPERSON" HeaderText="审核人">
                        <HeaderStyle Width="65px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                     </asp:BoundField>
                     <asp:BoundField DataField="VALIDATEDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审核日期"
                         HtmlEncode="False">
                         <HeaderStyle Width="100px" />
                         <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="TYPENAME"  HeaderText="审核日期"
                         HtmlEncode="False">
                         <HeaderStyle Width="100px" />
                         <ItemStyle HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="MEMO" HeaderText="备注" >
                     </asp:BoundField>
                  </Columns>
                  <RowStyle BackColor="AliceBlue" Height="26px" />
                  <PagerStyle Font-Italic="False" Font-Size="10pt" Height="21px" />
                  <HeaderStyle BackColor="Gainsboro" Font-Size="10pt" ForeColor="DimGray" CssClass="GridHeader2" />
                  <AlternatingRowStyle BackColor="White" />
               </asp:GridView>
           </div>
           
           
          <div Style=" text-align:left;" class="edit_pager"><!--分页导航-->
             <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;" Width="450px"></NetPager:AspNetPager>
          </div>
         </td>
       </tr>
    </table>
    <div id="divDetail">      
           <div class="edit_detail_header">
             <table cellpadding="0"  cellspacing="0" width="100%">
                <tr>
                   <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                       单据<asp:Label ID="lblBillNo" runat="server" ForeColor="Red"></asp:Label>明细</td>
                   <%--<td style="width:100px; "><span style="cursor:pointer; text-align:center;" onclick="javascript:divAllotment.style.display='block';divDetail.style.display='none';">&nbsp;&nbsp;浏览分配明细</span></td>--%>
                </tr>
             </table>
          </div> 
    </div>
    
    <div  class="edit_detail_body">
                   <asp:DataGrid ID="dgDetail" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgDetail_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify">
                       <Columns>
                           <asp:TemplateColumn HeaderText="序号">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="30px" />
                           </asp:TemplateColumn>
                           <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                               <HeaderStyle Width="20px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                               <HeaderStyle Width="90px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                               <HeaderStyle Width="150px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                               <HeaderStyle Width="70px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                               <HeaderStyle Width="60px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                               <HeaderStyle Width="60px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Right" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PUTQUANTITY" HeaderText="实际出入库量">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Right" />
                               <HeaderStyle Width="80px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRICE" HeaderText="单价">
                               <HeaderStyle Width="60px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Right" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="MEMO" HeaderText="备注"></asp:BoundColumn>
                       </Columns>
                       <ItemStyle BackColor="AliceBlue" Height="26px" />
                       <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                       <AlternatingItemStyle BackColor="White" />
                   </asp:DataGrid>
                   <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px" ></asp:Label>
                   <%-- <div Style=" text-align:right;" class="edit_detail_pager"><!--明细分页-->
                     <NetPager:AspNetPager ID="pager2" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;" Width="450px"></NetPager:AspNetPager>
                   </div>--%>
               </div>
               
               <div style="font-size:0px; position:absolute; bottom:0px; right:0px;">
               <asp:Button ID="btnReload" runat="server" Text="" CssClass="HiddenControl" OnClick="btnReload_Click" />
               <asp:HiddenField ID="hdnOpFlag" runat="server"  Value="2"/>
               <asp:HiddenField ID="hdnRowIndex" runat="server"  Value="0"/>
               <asp:HiddenField ID="hdnDetailRowIndex" runat="server"  Value="0"/>
      </div>
      </ContentTemplate>
      </asp:UpdatePanel>
    </form>
</body>
</html>
