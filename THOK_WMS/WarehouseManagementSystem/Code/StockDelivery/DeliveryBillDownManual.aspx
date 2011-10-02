<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryBillDownManual.aspx.cs" Inherits="Code_StockDelivery_DeliveryBillDownManual" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>手动下载出库单</title>
    <base target="_self" />
    <link href="../../css/css.css?t=0" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <script>
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
                        <td >
                            <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative; left: 0px; top: 0px;" Width="100%">
                                 <table style="width:100%; height:20px;">
                                     <tr>
                                         <td>
                                             日期:
                                             <asp:TextBox ID="txtStartDate" runat="server" Width="100px"></asp:TextBox>
                                             <input id="Button1" type="button" value="" onclick="setday(txtStartDate)" class="ButtonDate" />
                                             &nbsp;到:
                                             <asp:TextBox ID="txtEndDate" runat="server" Width="100px"></asp:TextBox>
                                             <input id="Button2" type="button" value="" onclick="setday(txtEndDate)" class="ButtonDate" />
                                             <%--<asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>--%>
                                             <asp:Button ID="btnSelectDown" runat="server" CssClass="ButtonDown" Text="日期内数据下载" OnClick="btnSelectDown_Click" />
                                             
                                         </td>
                                         <td style="height: 22px" align="right">
                                             <asp:Button ID="btnDown" runat="server" CssClass="ButtonDown" Text="选择下载" OnClick="btnDown_Click" />
                                             <%--<asp:Button ID="btnUnite" runat="server" CssClass="ButtonDown" Text="合单" OnClick="btnUnite_Click" />--%>
                                             <asp:Button ID="btnBack" runat="server" Text=" 返回" CssClass="ButtonBack" Visible="true" OnClick="btnBack_Click" />
                                         </td>
                                     </tr>
                                 </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div style="height:300px; width:100%; overflow:auto;" ><%--class="edit_body" --%>
                                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" CellSpacing="1" GridLines="None" Width="100%" OnRowDataBound="gvMaster_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderStyle Width="15px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                             <HeaderStyle Width="25px" />
                                             <%--<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                   Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BILLNO" HeaderText="出库单号">
                                            <HeaderStyle Width="80px" />
                                            <%--<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                   Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BILLDATE" HeaderText="制单日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                                            <HeaderStyle Width="80px" />
                                            <%--<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                   Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BILLTYPE" HeaderText="出库单类型">
                                            <HeaderStyle Width="100px" />
                                            <%--<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                   Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundField>                
                                        <asp:BoundField DataField="QUANTITY_SUM" HeaderText="总数量">
                                            
                                            <%--<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                   Font-Underline="False" HorizontalAlign="Center" />
                                                   <HeaderStyle Width="70px" />
                                        </asp:BoundField>

                                        <%--<asp:BoundField DataField="MEMO" HeaderText="县名称">
                                            <HeaderStyle Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>--%>
                                    </Columns>
                                    <RowStyle BackColor="AliceBlue" Height="26px" />
                                    <PagerStyle Font-Italic="False" Font-Size="10pt" Height="21px" />
                                    <HeaderStyle BackColor="Gainsboro" Font-Size="10pt" ForeColor="DimGray" CssClass="GridHeader2"/>
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                               
                            </div>
                            <%--<div Style=" text-align:left;" class="edit_pager"><!--分页导航-->
                                <NetPager:AspNetPager ID="pager" Width="450" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
                            </div>--%>
                        </td>
                    </tr>
                </table>
                <div class="edit_detail_header">
                    <table cellpadding="0"  cellspacing="0" width="100%">
                        <tr>
                           <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                           出库单<asp:Label ID="lblBillNo" runat="server" ForeColor="Red"></asp:Label>明细</td>
                        </tr>
                    </table>
                </div>
                <div style="height:240px; width:100%; overflow:inherit;"  ><%--class="edit_detail_body"--%>
                    <asp:DataGrid ID="dgDetail" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None" EnableViewState="False" HorizontalAlign="Justify" OnItemDataBound="dgDetail_ItemDataBound">
                        
                        <Columns>
                            <asp:TemplateColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <HeaderStyle Width="5%" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                 <HeaderStyle Width="5%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="商品编码">
                                <HeaderStyle Width="35%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT_NAME" HeaderText="商品名称">
                                <HeaderStyle Width="35%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                                <HeaderStyle Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="AliceBlue" Height="26px" />
                        <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                    </asp:DataGrid>
                    <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px"></asp:Label>
                </div>
               <%-- <div class="edit_detail_pager"><!--明细分页-->
                    <NetPager:AspNetPager ID="pager2" Width="450px" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
                </div>--%>
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
