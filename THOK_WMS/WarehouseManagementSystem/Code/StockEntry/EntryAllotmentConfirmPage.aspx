<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryAllotmentConfirmPage.aspx.cs" Inherits="Code_StockEntry_EntryAllotmentConfirmPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Expires" CONTENT="0"> 
    <meta http-equiv="Cache-Control" CONTENT="no-cache"> 
    <meta http-equiv="Pragma" CONTENT="no-cache">
    <title>入库单分配确认</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <style>
      .tabSelected
      {
        background-color:white;
        width:100px;
        text-align:center;
        cursor:pointer;
      }
      .tab
      {
         width:100px;
          background-color:AliceBlue;
          text-align:center;
           cursor:pointer;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>    
      <table style="width:100%;font-size:12px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
             <!--工具栏-->
             <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
                <table  class="OperationBar2" cellpadding="2" cellspacing="0" >
                   <tr>
                     <td style=" width:100%;">
                       <table >
                           <tr>
                             <td class="tdTitle">
                                 &nbsp; 制单日期</td>
                             <td><asp:TextBox ID="txtStartDate" runat="server" Width="75px"></asp:TextBox><input id="Button1" type="button" value="" onclick="setday(txtStartDate)" class="ButtonDate" />
                              至 <asp:TextBox ID="txtEndDate" runat="server" Width="75px"></asp:TextBox><input id="Button2" type="button" value="" onclick="setday(txtEndDate)" class="ButtonDate"/></td>
                             <td class="tdTitle">
                                 &nbsp; 入库单号</td>
                             <td><asp:TextBox ID="txtBillNo" runat="server" Width="86px"></asp:TextBox></td>
                           </tr>
                           <tr>
                             <td class="tdTitle">备注信息</td>
                             <td><asp:TextBox ID="txtMemo" runat="server" Width="211px"></asp:TextBox></td>
                             <td class="tdTitle">入库类别</td>
                             <td><asp:DropDownList ID="ddlBillType" runat="server">
                                     <asp:ListItem Selected="True" Value="1">所有入库单</asp:ListItem>
                                 </asp:DropDownList></td>
                           </tr>    
                       </table>
                     </td>
                     <td><asp:Button ID="btnQuery" runat="server" Text="查询"  CssClass="ButtonQuery" OnClick="btnQuery_Click"/></td>
                     <td>
                         <table cellpadding="0" cellspacing="0">
                            <tr>
                              <td><asp:Button ID="btnCancelAllotment" runat="server" Text="取消分配" CssClass="ButtonDel" OnClick="btnCancelAllotment_Click"/></td>
                              <td><asp:Button ID="btnConfirm" runat="server" Text="确认分配" CssClass="ButtonAudit" OnClick="btnConfirm_Click"/></td>
                              <td><asp:Button ID="btnReverseConfirm" runat="server" Text="取消确认" CssClass="ButtonAudit2" OnClick="btnReverseConfirm_Click"/></td>
                              <td><asp:Button ID="btnExit" runat="server" Text="退出"  OnClientClick="Exit();" CssClass="ButtonExit" /></td>
                            </tr>
                         </table>
                     </td>
                   </tr>
                </table>
             </asp:Panel>
            </td>
        </tr>
        <tr>
          <td>
           <div style="height:240px;" class="edit_body">
               <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" CellSpacing="1" GridLines="None" Width="100%" OnRowDataBound="gvMain_RowDataBound">
                   <Columns>
                       <asp:TemplateField>
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                       </asp:TemplateField>
                       <asp:TemplateField>
                           <HeaderStyle Width="15px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="45px" />
                       </asp:TemplateField>
                       <asp:BoundField DataField="BillNo" HeaderText="入库单号" >
                           <HeaderStyle Width="100px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="BILLDATE" HeaderText="制单日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                           <HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="OPERATEPERSON" HeaderText="操作员" >
                           <HeaderStyle Width="70px" />
                           <ItemStyle Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="VALIDATEPERSON" HeaderText="审核人" >
                           <HeaderStyle Width="70px" />
                           <ItemStyle Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="VALIDATEDATE" HeaderText="审核日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                           <HeaderStyle Width="80px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="STATUS" HeaderText="处理状态" >
                           <HeaderStyle Width="80px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="TOTALQUANTITY" HeaderText="总数量">
                           <ItemStyle HorizontalAlign="Right" Width="70px" />
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
          <!--分页导航-->
            <div Style=" text-align:left; background-color:whitesmoke;" class="edit_pager">
             <NetPager:AspNetPager ID="pager" Width="450" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager> 
            </div>
          </td>
        </tr>
      </table>
      <div class="edit_detail_header">
           <table cellpadding="0"  cellspacing="0" style=" width:100%;">
                <tr>
                   <td style="padding-left:1em; padding-top:5px; height:30px;">入库单<asp:Label ID="lblBillNo" runat="server" ForeColor="Red"></asp:Label>分配结果</td>
                   <td></td>
                   <td style="">&nbsp;</td>
                </tr>
           </table>
       </div>        
<asp:Panel ID="pnlDetail" runat="server" style="display:none;" Width="100%">
               
               <div style="width:100%; overflow:auto;">
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
                           <asp:BoundColumn DataField="INPUTQUANTITY" HeaderText="实际入库量">
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
                   <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
               </div>    <            
               <div Style=" text-align:right;">
               <!--明细分页-->
                 <table id="Table1" cellpadding="0" cellspacing="0" style="">
                   <tr>
                     <td>
                     </td>
                     <td>
                       <NetPager:AspNetPager ID="pager2" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
                     </td>
                   </tr>
                  </table>  
             </div>  
 </asp:Panel> 
 
<asp:Panel ID="pnlAllotment" runat="server" style="display:block;"  Width="100%"> 
              <div  class="edit_detail_body">
                   <asp:DataGrid ID="dgAllotment" runat="server" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None" AutoGenerateColumns="False"  Width="100%" OnItemDataBound="dgAllotment_ItemDataBound">
                       <AlternatingItemStyle BackColor="AliceBlue" />
                       <ItemStyle BackColor="White" Height="26px" />
                       <HeaderStyle CssClass="GridHeader2" />
                       <Columns>
                           <asp:TemplateColumn HeaderText="序号">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="30px" />
                           </asp:TemplateColumn>
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
              <div Style=" text-align:right;" class="edit_detail_pager"><!--分配结果分页-->
                  <NetPager:AspNetPager ID="pager3" Width="450" runat="server" OnPageChanging="pager3_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
             </div>
</asp:Panel>   
      
      
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