<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryBillQueryPage.aspx.cs" Inherits="Code_Statistic_DeliveryBillQueryPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>出库单查询</title>
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
        <Triggers>
           <asp:PostBackTrigger ControlID="btnDeliveryBillExcel" />
        </Triggers>
        <Triggers>
           <asp:PostBackTrigger ControlID="btnAllotExcel" />
        </Triggers>
           <ContentTemplate>    
      <table style="width:100%;font-size:12px; " cellpadding="0" cellspacing="0">
        <tr>
            <td>
             <!--工具栏-->
             <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
                  <table style="width:100%; height:20px;">
                     <tr>
                       <td style="height: 22px">
                           <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="BILLNO">出库单号</asp:ListItem>
                               <asp:ListItem Value="OPERATEPERSON">审核人</asp:ListItem>
                               <asp:ListItem Value="Memo">备注</asp:ListItem>
                           </asp:DropDownList>
                           <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox"></asp:TextBox><asp:RadioButton GroupName="order" ID="rbASC" runat="server" Text="升" Checked="True" />
                           <asp:RadioButton GroupName="order" ID="rbDESC" runat="server" Text="降" />
                           <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                           <asp:Button ID="btnQuery2" runat="server" Text="高级查询"  OnClientClick="return ShowQueryDialog();" CssClass="ButtonQuery" OnClick="btnQuery2_Click"/>
                           <asp:Button ID="btnExit" runat="server" Text="退出"  OnClick="btnExit_Click" OnClientClick="Exit();" CssClass="ButtonExit" />
                        </td>
                        <td align="right">
                           <asp:Button ID="btnDeliveryBillExcel" runat="server" Text="导出Excel" CssClass="ButtonDown" OnClick="btnDeliveryBillExcel_Click" />
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
                       <asp:BoundField DataField="BillNo" HeaderText="出库单号" >
                           <HeaderStyle Width="90px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:BoundField>
                       <asp:BoundField DataField="WH_NAME" HeaderText="仓库名称">
                           <HeaderStyle Width="100px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="BILLDATE" HeaderText="制单日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                           <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                           <HeaderStyle Width="80px" />
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
         
          <div Style=" text-align:left;" class="edit_pager"> <!--分页导航-->
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
                       出库单<asp:Label ID="lblBillNo" runat="server" ForeColor="Red"></asp:Label>明细</td>
                   <td style="width:100px; "><span style="cursor:pointer; text-align:center;" onclick="javascript:divAllotment.style.display='block';divDetail.style.display='none';">&nbsp;&nbsp;浏览分配明细</span></td>
                </tr>
             </table>
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
                           <asp:BoundColumn DataField="OUTPUTQUANTITY" HeaderText="实际出库量">
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
           </div> 
           <%--<div Style=" text-align:right;" class="edit_detail_pager"><!--明细分页-->
             <NetPager:AspNetPager ID="pager2" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;" Width="450px"></NetPager:AspNetPager>
           </div>--%>
      </div>
      <div id="divAllotment" style="display:none;">
           <div class="edit_detail_header">
             <table cellpadding="0"  cellspacing="0" width="100%">
                <tr>
                   <td style="padding-left:1em; padding-top:5px; height:30px;">出库分配明细</td>
                   <td align="right"><asp:Button ID="btnAllotExcel" runat="server" Text="分配明细导出" CssClass="ButtonDown" OnClick="btnAllotExcel_Click" /></td>
                   <td style="width:100px; height: 30px;"><span style="cursor:pointer; text-align:center;" onclick="javascript:divAllotment.style.display='none';divDetail.style.display='block';">&nbsp;&nbsp;返回出库明细</span></td>
                </tr>
             </table>
          </div>
          <div  class="edit_detail_body">
               <asp:DataGrid ID="dgAllotment" runat="server" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None" AutoGenerateColumns="False"  Width="100%" OnItemDataBound="dgAllotment_ItemDataBound">
                       <AlternatingItemStyle BackColor="AliceBlue" />
                       <ItemStyle BackColor="White" Height="26px" />
                       <HeaderStyle CssClass="GridHeader2" />
                       <Columns>
                           <asp:BoundColumn DataField="TASKID" HeaderText="拼盘号">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="45px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="90px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="90px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="80px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="70px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                               <HeaderStyle Width="60px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="QUANTITY" HeaderText="分配数量">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Right" />
                               <HeaderStyle Width="70px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="STATUS" HeaderText="作业状态">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="70px" />
                           </asp:BoundColumn>
                       </Columns>
                   </asp:DataGrid>  
          </div>
          <div Style=" text-align:right;" class="edit_detail_pager"></div>
      </div>
      <div style="font-size:0px; position:absolute; bottom:0px; right:0px;">
               <asp:Button ID="btnReload" runat="server" Text="" CssClass="HiddenControl" OnClick="btnReload_Click" />
               <asp:HiddenField ID="hdnOpFlag" runat="server"  Value="2"/>
               <asp:HiddenField ID="hdnRowIndex" runat="server"  Value="0"/>
               <asp:HiddenField ID="hdnScrollTop" runat="server"  Value="0"/>
               <asp:HiddenField ID="hdnDetailRowIndex" runat="server"  Value="0"/>
      </div>

<script>
 
function selectRow(objGridName,index)
{

  if(document.getElementById('hdnOpFlag').value=="0")
  {
     return;
  }
  var table=document.getElementById(objGridName);
  for(var i=1;i<table.rows.length;i++)       //table.rows[1].cells.length
  {
     table.rows[i].cells[0].innerHTML="";
  }
  table.rows[index+1].cells[0].innerHTML="<img src=../../images/arrow01.gif />";
  if(objGridName=="gvMain")
  {
      document.getElementById('hdnRowIndex').value=index;
      document.getElementById('hdnScrollTop').value=document.getElementById('gvMain').parentElement.parentElement.scrollTop;
      
      document.getElementById('btnReload').click();
  }
}

function ShowQueryDialog()
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    var TableName='WMS_OUT_BILLMASTER';
    var returnvalue=window.showModalDialog("../../Common/QueryDialog.aspx?TableName="+TableName+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=320px;dialogHeight=400px");
    if(returnvalue==null)
    {
       return false;
    }
}

function Exit()
{
   window.open("../../MainPage.aspx","_self");
}
</script> 

               </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>