<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryManualAllotPage.aspx.cs" Inherits="Code_StockDelivery_DeliveryManualAllotPage" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>出库分配作业_手动分配</title>
    <base target="_self" />
    <link href="../../css/css.css?o" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <style>
      .cell
      {
        height:90px; width:87px; background-color:#fcfcfc;border:1px solid #595a5c;
        text-align:center; vertical-align:middle;  word-break:keep-all; word-wrap : 
      }
      .panel
      {
        height:100px; width:90px;
        over-float:hidden; float:left; 
      }
      .button2{
            font-size:12px;
            border:1px #1E7ACE solid;
            background:#D0F0FF;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
       
        
    
        <div style="width:100%; height:5px; background-image:url(../../images/option_bg01.gif);overflow: hidden ;"></div>
        <table cellpadding="1" cellspacing="1" style="width:100%; background-image:url(../../images/option_bg01.gif);">
          <tr style="height:27px; z-index:2;">
           <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_no.gif); background-repeat:no-repeat; cursor:hand;"><a href="DeliveryAutoAllotPage.aspx">自动分配</a></td>
           <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_selected.gif); background-repeat:no-repeat;">手动分配</td>
           <td style="border-bottom:solid 1px gray;"></td>
          </tr>
        </table>
        <table style="width:100%; height:28PX;">
          <tr>
            <td>
           <asp:Button ID="btnSaveAllotment" runat="server" Text="保存分配结果"  CssClass="ButtonSave" OnClick="btnSaveAllotment_Click"/>
           <asp:Button ID="btnReAllot" runat="server" Text="重新分配"  CssClass="ButtonDel" OnClick="btnReAllot_Click" />
            <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="ButtonBack" OnClientClick="return Back()"/>
            <asp:Button ID="btnExit" runat="server" Text="退出" CssClass="ButtonExit" OnClientClick="return Exit()" />
            </td>
          </tr>
        </table>
        <table style="height:160px; width:100%; overflow:auto; background-color:White;" cellpadding="0" cellspacing="0">
          <tr>
             <td valign="top">                
                 <asp:DataGrid ID="dgDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None" 
                     OnItemDataBound="dgDetail_ItemDataBound" Width="100%" OnPageIndexChanged="dgDetail_PageIndexChanged" PageSize="5" AllowPaging="True" OnEditCommand="dgDetail_EditCommand">
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
                             <HeaderStyle Width="90px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                             <HeaderStyle Width="70px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="ALLOTEDQTY" HeaderText="已分配数量" Visible="False">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Right" />
                             <HeaderStyle Width="70px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                             <HeaderStyle Width="70px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                             <HeaderStyle Width="70px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:ButtonColumn ButtonType="PushButton" CommandName="Edit" HeaderText="分配" Text="分配">
                             <HeaderStyle Width="45px" />
                         </asp:ButtonColumn>
                     </Columns>
                     <ItemStyle BackColor="White" Height="25px" />
                     <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                     <AlternatingItemStyle Height="25px" />
                     <PagerStyle NextPageText="" PrevPageText="" Visible="False" />
                 </asp:DataGrid>
             </td>
          </tr>
        </table>
          <!--分页导航-->
          <div Style=" text-align:right; width:100%; background-color:WhiteSmoke; border-bottom:solid 1px Gainsboro;">
             <table id="paging" cellpadding="0" cellspacing="0" style=" padding-bottom:3px; height:20px;">
               <tr>
                 <td>
                   <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
                 </td>
               </tr>
              </table>  
           </div>
           
        <!--分配结果--> 
        <div  style="height:100%; width:100%; overflow:auto;">
            <asp:DataGrid ID="dgAllotment" runat="server" AutoGenerateColumns="False" OnItemDataBound="dgAllotment_ItemDataBound" Width="100%" OnDeleteCommand="dgAllotment_DeleteCommand" OnPreRender="dgAllotment_PreRender">
                <ItemStyle Height="26px" />
                <HeaderStyle CssClass="GridHeader2" />
                <Columns>
                    <asp:BoundColumn DataField="BILLNO" HeaderText="出库单号">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle Width="90px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="140px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="100px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="QUANTITY" HeaderText="分配数量">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle Width="60px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                        <HeaderStyle Width="60px" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Delete" HeaderText="删除" Text="删除">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <HeaderStyle Width="30px" />
                    </asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>  
             
      
      <div style="">
          <asp:HiddenField ID="hdnDetailRow" runat="server" />
          <asp:HiddenField ID="hdnCellRow" runat="server" />
      </div>
      
     <table cellpadding="2" cellspacing="18" style=" border:solid 1px gray; background-color:AliceBlue; position:absolute; top:560px; left:200px; display:none;">
        <tr>
          <td style="background-color:White;">
            <div style="overflow-x:hidden; overflow-y:auto; height:360px; width:290px;">
              <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged" >
                  <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
              </yyc:smarttreeview>
            </div>
          </td>
        </tr>
     </table>
 <script   language="javascript">   
function Exit()
{
  window.open("../../MainPage.aspx","_self");return false;
}

function Back()
{
  window.open("DeliveryAllotPage.aspx","_self");return false;
}  

function allot()
{
  alert();
  return false;
}
  </script> 
  
  
             </ContentTemplate>
        </asp:UpdatePanel>  
    </form>
     
</body>
</html>