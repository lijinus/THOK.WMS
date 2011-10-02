<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingOrderDownManual.aspx.cs" Inherits="Code_StockDelivery_DeliveryBillDownManual" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>手动下载分拣订单</title>
     <base target="_self" />
    <link href="../../css/css.css?t=0" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <script type="text/javascript">
        function selectRow(objGridName,index){
           if(document.getElementById('hdnOpFlag').value=="0"){
              return;
           }
           var table=document.getElementById(objGridName);
           for(var i=1;i<table.rows.length;i++){
             table.rows[i].cells[0].innerHTML="";
           }
           table.rows[index+1].cells[0].innerHTML="<img src=../../images/arrow01.gif />";
           if(objGridName=="gvMaster"){
             document.getElementById('hdnRowIndex').value=index;
             document.getElementById('hdnScrollTop').value=document.getElementById('gvMaster').parentElement.parentElement.scrollTop;
             document.getElementById('btnReload').click();
           }
        }
    </script>
    <script type="text/javascript">
        function Back(){
           var date = Date();
           var time=date.getMilliseconds();
           window.open("DeliveryBillPage.aspx?t="+time,"_self");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width:100%;font-size:12px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative; left: 0px; top: 0px;" Width="99%">
                                 <table style="width:98%; height:20px;">
                                     <tr>
                                         <td "height: 22px">
                                             日期:
                                             <asp:TextBox ID="txtStartDate" runat="server" Width="70px"></asp:TextBox>
                                             <input id="Button1" type="button" value="" onclick="setday(txtStartDate)" class="ButtonDate" />
                                             &nbsp;到:
                                             <asp:TextBox ID="txtEndDate" runat="server" Width="70px"></asp:TextBox>
                                             <input id="Button2" type="button" value="" onclick="setday(txtEndDate)" class="ButtonDate" />
                                             <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                                             <asp:Button ID="btnSelectDown" runat="server" CssClass="ButtonDown" Text="日期内数据下载" OnClick="btnSelectDown_Click" />
                                             
                                         </td>
                                         <td style="height: 22px" align="right">
                                            <%--<asp:Button ID="Button3" runat="server" CssClass="ButtonDown" Text="全部下载" OnClick="btnFullDown_Click" />--%>
                                             <asp:Button ID="btnDown" runat="server" CssClass="ButtonDown" Text="选择下载" OnClick="btnDown_Click" />
                                             <asp:Button ID="btnBack" runat="server" Text=" 返回" CssClass="ButtonBack" Visible="true" OnClick="btnBack_Click" OnClientClick="Back();" />
                                         </td>
                                     </tr>
                                 </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div style="height:210px;" class="edit_body">
                                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" CellSpacing="1" GridLines="None" Width="99%" OnRowDataBound="gvMaster_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderStyle Width="15px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                             <HeaderStyle Width="25px" />
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ORDER_ID" HeaderText="订单单号">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORDER_DATE" HeaderText="订单日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORDER_TYPE" HeaderText="订单类型">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CUST_CODE" HeaderText="客户编号" >
                                         <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                           </asp:BoundField>
                                        <asp:BoundField DataField="CUST_NAME" HeaderText="客户名称">
                                        <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUANTITY_SUM" HeaderText="总数量">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="DETAIL_NUM" HeaderText="明细数">
                                        <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField> 
                                        <asp:BoundField DataField="DELIVER_ORDER" HeaderText="送货路线">
                                        <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField> 
                                    </Columns>
                                    <RowStyle BackColor="AliceBlue" Height="26px" />
                                    <PagerStyle Font-Italic="False" Font-Size="10pt" Height="21px" />
                                    <HeaderStyle BackColor="Gainsboro" Font-Size="10pt" ForeColor="DimGray" CssClass="GridHeader2"/>
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                               
                            </div><%-- 
                            <div style=" text-align:left;" class="edit_pager"><!--分页导航-->
                                <NetPager:AspNetPager ID="pager" Width="450" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
                            </div>--%>
                        </td>
                    </tr>
                </table>
                <div class="edit_detail_header">
                    <table cellpadding="0"  cellspacing="0">
                        <tr>
                           <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                           分拣订单号<asp:Label ID="lblBillNo" runat="server" ForeColor="Red"></asp:Label>明细</td>
                        </tr>
                    </table>
                </div>
                <div style="height:240px;" class="edit_detail_body">
                    <asp:DataGrid ID="dgDetail" runat="server" Width="99%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None" EnableViewState="False" HorizontalAlign="Justify" OnItemDataBound="dgDetail_ItemDataBound">
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="AliceBlue" Height="26px" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <HeaderStyle Width="25px" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ORDER_DETAIL_ID" HeaderText="订单明细号" Visible="False">
                                 <HeaderStyle Width="20px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BRAND_CODE" HeaderText="商品编码">
                                <HeaderStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BRAND_NAME" HeaderText="商品名称">
                                <HeaderStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BRAND_UNIT_NAME" HeaderText="单位">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="商品名称">
                                <HeaderStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CUST_CODE" HeaderText="客户编码">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRICE" HeaderText="单价">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>                           
                           <%-- <asp:BoundColumn DataField="MEMO" HeaderText="备注"></asp:BoundColumn>--%> 
                        </Columns>
                        <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                    </asp:DataGrid>
                    <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px"></asp:Label>
                </div><%-- 
                <div class="edit_detail_pager"><!--明细分页-->
                    <NetPager:AspNetPager ID="pager2" Width="450px" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
                </div>
                
                --%>
                <div style="font-size:0px; position:absolute; bottom:0px; right:0px;">
                    <asp:Button ID="btnReload" runat="server" Text="" CssClass="HiddenControl" OnClick="btnReload_Click"/>
                    <asp:HiddenField ID="hdnOpFlag" runat="server"  Value="2"/>
                    <asp:HiddenField ID="hdnRowIndex" runat="server"  Value="0"/>
                    <asp:HiddenField ID="hdnScrollTop" runat="server"  Value="0"/>
                    <asp:HiddenField ID="hdnDetailRowIndex" runat="server"  Value="0"/>
                </div>
            </ContentTemplate> 
        </asp:UpdatePanel>
    </form>
</body>
</html>
