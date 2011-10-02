<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryAutoAllotPage.aspx.cs" Inherits="Code_StockDelivery_DeliveryAutoAllotPage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>出库分配作业_自动分配</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; height:5px; background-image:url(../../images/option_bg01.gif);overflow: hidden ;"></div>
    <table cellpadding="1" cellspacing="1" style="width:100%; background-image:url(../../images/option_bg01.gif);">
      <tr style="height:27px; z-index:2;">
       <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_selected.gif); background-repeat:no-repeat; cursor:hand;">自动分配</td>
       <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_no.gif); background-repeat:no-repeat;"><a href="DeliveryManualAllotPage.aspx">手动分配</a></td>
       <td style="border-bottom:solid 1px gray;"></td>
      </tr>
    </table>
<div style="height:25px;padding-top:15px; padding-bottom:5px;"><!--执行自动分配，系统将自动为出库单中的货品分配货位-->
    &nbsp;
  <%--  <asp:RadioButton ID="rbFIFO" runat="server" GroupName="rule" Checked="True" Text="先进先出" Enabled="false" />
    <asp:RadioButton ID="rbProx" runat="server" GroupName="rule" Text="就近原则" Enabled="false" />--%>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnAllot" runat="server" Text="进行分配" OnClick="btnAllot_Click"  CssClass="ButtonAllot"/>
   <asp:Button ID="btnSaveAllotment" runat="server" Text="保存分配结果"  CssClass="ButtonSave"  Enabled="False" OnClick="btnSaveAllotment_Click"/>
    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="ButtonBack" OnClientClick="return Back()"/>
    <asp:Button ID="btnExit" runat="server" Text="退出" CssClass="ButtonExit" OnClientClick="return Exit()" />
</div><asp:ListBox runat="server" ID="lbError" Width="100%" Height="100px" Visible="false" ForeColor="red" Font-Size="Medium"  ></asp:ListBox>
  <div style="Width:100%; height:200px; overflow:auto;">
                 <asp:DataGrid ID="dgDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None" Width="100%" OnItemDataBound="dgDetail_ItemDataBound">
                     <Columns>
                         <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                             <HeaderStyle Width="20px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BILLNO" HeaderText="出库单号">
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
                             <HeaderStyle  />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
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
               </div>
      <div style="height:10px;"></div>
      
      <asp:Panel ID="pnlResult" runat="server"  Width="100%" Height="100%" Visible="false" style="overflow-y:auto;">
          <asp:DataGrid ID="dgResult" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None" Width="100%" OnItemDataBound="dgResult_ItemDataBound">
              <Columns>
                  <asp:BoundColumn DataField="DETAILID" HeaderText="明细ID" Visible="False">
                      <HeaderStyle Width="20px" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="BILLNO" HeaderText="出库单号">
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
                  <asp:BoundColumn DataField="QUANTITY" HeaderText="分配量">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" HorizontalAlign="Center" />
                  </asp:BoundColumn>
                  <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                      <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" HorizontalAlign="Center" />
                  </asp:BoundColumn>
              </Columns>
              <ItemStyle BackColor="White" Height="26px" />
              <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:DataGrid>
          
             <asp:Panel ID="pnlAllotedCell" runat="server"  Width="100%" Height="2px"></asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlMoveDetail" runat="server"  Width="100%" Height="100%" Visible="false" style="overflow-y:auto;">
        <div style="height:319px;" class="edit_detail_body" >
        <asp:DataGrid id="dgMoveDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None" Width="100%" OnItemDataBound="dgMoveDetail_ItemDataBound" >
                       <Columns>
                           <asp:BoundColumn DataField="ID" HeaderText="明细ID" Visible="False">
                      <HeaderStyle Width="20px" />
                  </asp:BoundColumn>
                           <asp:BoundColumn DataField="BILLNO" HeaderText="移库单号">
                               <HeaderStyle Width="120px"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="OUT_CELLCODE" HeaderText="货位编码(出)">
                                <HeaderStyle Width="100px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="OUT_CELLNAME" HeaderText="货位名称(出)">
                               <HeaderStyle Width="70px"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="IN_CELLCODE" HeaderText="货位编码(入)">
                               <HeaderStyle Width="100px"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="IN_CELLNAME" HeaderText="货位名称(入)">
                               <HeaderStyle Width="70px"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                               <HeaderStyle Width="80px"  />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                                    <HeaderStyle Width="50px"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                               <HeaderStyle Width="60px"  />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                               <HeaderStyle Width="60px"  />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                               <HeaderStyle Width="40px"  />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Right"  />
                           </asp:BoundColumn>
                       </Columns>
                       <ItemStyle BackColor="ActiveCaptionText" Height="26px"  />
                       <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px"  />
                       <AlternatingItemStyle BackColor="White"  />
                   </asp:DataGrid>
                    <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px" ></asp:Label>
                   </div></asp:Panel>
        <asp:DataGrid ID="dgAllotMaster" runat="server">
        </asp:DataGrid>
        <asp:DataGrid ID="dgAllotDetail" runat="server">
        </asp:DataGrid>
    </form>
<script>
function Exit()
{
  window.open("../../MainPage.aspx","_self");return false;
}

function Back()
{
  window.open("DeliveryAllotPage.aspx","_self");return false;
}
</script>    
</body>
</html>