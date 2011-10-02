<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryAutoAllotPage.aspx.cs" Inherits="Code_StockEntry_EntryAutoAllotPage" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>入库分配作业_自动分配</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../../JScript/Calendar.js"></script>
    <script type="text/javascript" src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <style type="text/css">
    .ButtonAllot
    {
        font-family: "Tahoma", "宋体"; 
        font-size:9pt;  
        border: 0px #ff0000 solid; 
        background-color:Transparent;
        background-image:url(../../images/op/tools.png); 
        background-repeat:no-repeat;
        CURSOR: hand; 
        padding-left:10px;
        font-style: normal ; 
        height:20px;
    }
    .cell
    {
      height:90px; width:77px;  text-align:center; vertical-align:middle; background-color:whitesmoke; 
      border:1px solid gray;
    }
    .switch_up
    {
        height:10px;
        width:11px;
        cursor:hand;
        background-image:url(../../images/switch_up.gif); 
        background-repeat:no-repeat;
    }
    .switch_down
    {
        height:10px;
        width:11px;
        cursor:hand;
        background-image:url(../../images/switch_down.gif); 
        background-repeat:no-repeat;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; height:5px; background-image:url(../../images/option_bg01.gif);overflow: hidden ;"></div>
    <table cellpadding="1" cellspacing="1" style="width:100%; background-image:url(../../images/option_bg01.gif);">
      <tr style="height:27px; z-index:2;">
       <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_selected.gif); background-repeat:no-repeat; cursor:hand;">自动分配</td>
       <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_no.gif); background-repeat:no-repeat;"><a href="EntryManualAllotPage.aspx">手动分配</a></td>
       <td style="border-bottom:solid 1px gray;"></td>
      </tr>
    </table>
<div style="height:25px;padding-top:15px; padding-bottom:5px;"><!--执行自动分配，系统将自动为入库单中的货品分配货位-->
    &nbsp;
    <asp:RadioButton ID="rbRowFirst" runat="server" GroupName="rule" Checked="True" Text="行优先" />
    <asp:RadioButton ID="rbColFirst" runat="server" GroupName="rule" Text="列优先" />
<%--    <asp:RadioButton ID="rbCellEqually" runat="server" GroupName="rule" Text="均匀存放" />
    <asp:RadioButton ID="rbAreaEqually" runat="server" GroupName="rule" Text="分区存放" />--%>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnAllot" runat="server" Text="进行分配" OnClick="btnAllot_Click"  CssClass="ButtonAllot"/>
   <asp:Button ID="btnSaveAllotment" runat="server" Text="保存分配结果"  CssClass="ButtonSave"  Enabled="False" OnClick="btnSaveAllotment_Click"/>
    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="ButtonBack" OnClientClick="return Back()"/>
    <asp:Button ID="btnExit" runat="server" Text="退出" CssClass="ButtonExit" OnClientClick="return Exit()" />
</div>        <asp:ListBox runat="server" ID="lbError" Width="100%" Height="100px" Visible="false" ForeColor="red" Font-Size="Medium"></asp:ListBox>
  <div style="Width:100%;  overflow:auto;">
          <div class="edit_detail_header" style="padding-top:8px; padding-left:5px;">
             <table style="width:100%;">
                <tr><td style="height: 16px">入库明细</td>
                     <td style="text-align:right; height: 16px;">
                         <asp:Button ID="btnSwitchDetail" runat="server" CssClass="switch_up" BorderWidth="0px" Height="10px" OnClick="btnSwitchDetail_Click" Width="11px" />
                     </td>
                </tr>
             </table>
          </div>
          <asp:Panel ID="pnlDetail" runat="server"  Width="100%" Height="200px"  style="overflow-y:auto;">
          <%--<asp:Panel ID="pnlDetail" runat="server" Width="100%">--%>
                 <asp:DataGrid ID="dgDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" 
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None" Width="100%" OnItemDataBound="dgDetail_ItemDataBound">
                     <Columns>
                         <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                             <HeaderStyle Width="20px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BILLNO" HeaderText="入库单号">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                             <HeaderStyle Width="100px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                             <HeaderStyle Width="70px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="INPUTQUANTITY" HeaderText="实际入库数量">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                             <HeaderStyle Width="80px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                             <HeaderStyle Width="60px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                             <HeaderStyle Width="60px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                     </Columns>
                     <ItemStyle BackColor="White" Height="26px" />
                     <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" />
                 </asp:DataGrid>
         </asp:Panel>    
                 
 </div>
      <div style="height:10px;"></div>
    
      <asp:Panel ID="pnlResult1" runat="server"  Width="100%"  Visible="false" style="overflow-y:auto;">
       <div class="edit_detail_header" style="padding-top:8px; padding-left:5px;">
             <table style="width:100%;">
                <tr><td>分配结果</td>
                     <td style="text-align:right;"></td>
                </tr>
             </table>
          </div>
          </asp:Panel>
      <asp:Panel ID="pnlResult" runat="server"  Width="100%" Height="400px"  Visible="false" style="overflow-y:auto;"><asp:DataGrid id="dgResult" runat="server" CssClass="GridStyle" Width="100%" OnItemDataBound="dgResult_ItemDataBound" GridLines="None" EnableViewState="False" CellSpacing="1" CellPadding="3" AutoGenerateColumns="False" OnPreRender="dgResult_PreRender">
              <Columns>
                  <asp:BoundColumn DataField="DETAILID" HeaderText="明细ID" Visible="False">
                      <HeaderStyle Width="20px" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="BILLNO" HeaderText="入库单号">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                      <HeaderStyle Width="100px" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                      <HeaderStyle Width="70px" />
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                      <HeaderStyle Width="150px" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                      <HeaderStyle Width="80px" />
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                      <HeaderStyle Width="60px" />
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" HorizontalAlign="Center" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" HorizontalAlign="Center" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="ALLOTQUANTITY" HeaderText="分配量">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                  </asp:BoundColumn>
              </Columns>
              <ItemStyle BackColor="White" Height="26px" />
              <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:DataGrid> <asp:Panel id="pnlAllotedCell" runat="server" Width="100%"></asp:Panel> </asp:Panel>
    </form>
<script type="text/javascript">
function Exit()
{
  window.open("../../MainPage.aspx","_self");return false;
}

function Back()
{
  window.open("EntryAllotPage.aspx","_self");return false;
}

function ShowGrid(grid)
{
  var g=document.getElementById(grid);
  if(g.style.display=='block' || g.style.display=='')
  {
     g.style.display='none';
  }
  else
  {
    g.style.display='block';
  }
}
</script>    
</body>
</html>