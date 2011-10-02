<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryManualAllotPage.aspx.cs" Inherits="Code_StockEntry_EntryManualAllotPage" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>入库分配作业_手动分配</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
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
    </style>
    
 <script   language="javascript">   
  var   beginMoving=false;   
  var   sourceObj=null;   
  var   objectObj=null;   
  function   MouseDownToMove(obj){   
  obj.style.zIndex=1;   
  obj.mouseDownY=event.clientY;   
  obj.mouseDownX=event.clientX;   
  beginMoving=true;   
  obj.setCapture();   
  sourceObj=obj;   
  objectObj=null;   
  }   
    
  function   MouseMoveToMove(obj){   
          if(!beginMoving)   return   false;   
  obj.style.top   =   (event.clientY-obj.mouseDownY);   
  obj.style.left   =   (event.clientX-obj.mouseDownX);   
  }   
  function   MouseUpToMove(obj){   
  if(!beginMoving)   return   false;   
  obj.releaseCapture();   
  obj.style.top=0;   
  obj.style.left=0;   
  obj.style.zIndex=0;   
  beginMoving=false;   
  window.setTimeout("swapFun()",10);   
  }   
    
  function   MouseOverFun(obj)   
  {   
     objectObj=obj;   
  }  
    
  function   swapFun()   
  {   
      if(sourceObj!=null&&   objectObj!=null) 
      {  
//    sourceObj.swapNode(objectObj);  
//    alert("cell:"+objectObj.name);
//    alert("detail:"+sourceObj.name);
         document.getElementById('hdnCellRow').value=objectObj.name; //cellrowindex
         document.getElementById('hdnDetailRow').value=sourceObj.name; //detailrowindex
         document.getElementById('btnAllot').click(); 
      }
//      sourceObj=null;   
//      objectObj=null;   
  } 
  
function Exit()
{
  window.open("../../MainPage.aspx","_self");return false;
}

function Back()
{
  window.open("EntryAllotPage.aspx","_self");return false;
}  
  </script> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
       
           </ContentTemplate>
        </asp:UpdatePanel>          
    
        <div style="width:100%; height:5px; background-image:url(../../images/option_bg01.gif);overflow: hidden ;"></div>
        <table cellpadding="1" cellspacing="1" style="width:100%; background-image:url(../../images/option_bg01.gif);">
          <tr style="height:27px; z-index:2;">
           <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_no.gif); background-repeat:no-repeat; cursor:hand;"><a href="EntryAutoAllotPage.aspx">自动分配</a></td>
           <td style="text-align:center; height:27px; width:102px; background-image:url(../../images/option_selected.gif); background-repeat:no-repeat;">手动分配</td>
           <td style="border-bottom:solid 1px gray;"></td>
          </tr>
        </table>
        <table style="width:100%; height:28PX;">
          <tr>
            <td>
           <asp:Button ID="btnSaveAllotment" runat="server" Text="保存分配结果"  CssClass="ButtonSave"  Enabled="False" OnClick="btnSaveAllotment_Click"/>
           <asp:Button ID="btnReAllot" runat="server" Text="重新分配"  CssClass="ButtonDel" OnClick="btnReAllot_Click" />
            <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="ButtonBack" OnClientClick="return Back()"/>
            <asp:Button ID="btnExit" runat="server" Text="退出" CssClass="ButtonExit" OnClientClick="return Exit()" />
            </td>
          </tr>
        </table>
        <table style="height:160px; width:100%; overflow:auto;" cellpadding="0" cellspacing="0">
          <tr>
             <td valign="top">                
                 <asp:DataGrid ID="dgDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None" 
                     OnItemDataBound="dgDetail_ItemDataBound" Width="100%" OnPageIndexChanged="dgDetail_PageIndexChanged" PageSize="5" AllowPaging="True">
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
                             <HeaderStyle Width="90px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="INPUTQUANTITY" HeaderText="未分配数量">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                             <HeaderStyle Width="70px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="ALLOTEDQTY" HeaderText="已分配数量">
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
      <table style="width:100%;  border:solid 1px #cdd5e2;">
         <tr>
            <td style=" width:213px;background-color:#f8f8f8;">
                <div style="overflow-x:hidden; overflow-y:scroll; width:213px; height:520px;">
                  <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged" >
                      <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
                  </yyc:smarttreeview>
                </div>
            </td>
            <td>
              <asp:Panel ID="pnlNullCell" runat="server"  Width="100%" Height="520px" style=" overflow:auto; padding:10 10 10 5;"></asp:Panel>
            </td>
         </tr>
      </table>
      
      <div style="">
         <asp:Button ID="btnAllot" runat="server" Text="" OnClick="btnAllot_Click"  CssClass="HiddenControl"/>
          <asp:HiddenField ID="hdnDetailRow" runat="server" />
          <asp:HiddenField ID="hdnCellRow" runat="server" />
      </div>

    </form>
     
</body>
</html>