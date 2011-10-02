<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckBillByChanged.aspx.cs" Inherits="Code_StorageManagement_CheckBillByChanged" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>异动盘点</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">

    </script>
    <style>
    .SideBar
    {
       
       padding-top:5px;
       vertical-align:top; 
       width:214px; 
       padding-right:4px;
    }
    .topic
    {
       padding-top:10px;
    }
    .topic2
    {
       text-align:center; 
       padding-top:3px;
       height:25px; 
       width:72px; 
       background-image:url(../../images/topic.gif);
       background-repeat:no-repeat;
    }
    </style>
<script type="text/javascript">

</script>  
</head>
<body style="margin-left:15px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<table style="width:100%;">
  <tr>
    <td align="center">
    
 
        <table width="98%" style="display:<%=div01display%>" cellpadding="0" cellspacing="0">
           <tr>
             <td style="padding: 1 1 1 1; " class="edit_detail_body">
                <table style="width:100%; background-color:AliceBlue;" cellpadding="1" cellspacing="1">
                   <tr>
                      <td style="vertical-align:top; padding-left:10px;"> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>  

                        <div style="width:100%; height:390px; overflow-x:auto; overflow-y:auto;">
                        <asp:DataGrid ID="dgCell" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None" EnableViewState="False" HorizontalAlign="Justify">
                                <Columns>
                                    <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CURRENTPRODUCT" HeaderText="产品代码">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="产品名称" DataField="C_PRODUCTNAME"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="55px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                                        <HeaderStyle Width="55px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="QUANTITY" HeaderText="库存量">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <HeaderStyle Width="55px" />
                                    </asp:BoundColumn>
                                </Columns>
                               <ItemStyle BackColor="AliceBlue" Height="26px" />
                               <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                               <AlternatingItemStyle BackColor="White" />
                           </asp:DataGrid> 
                        </div>
                        
                          <!--分页导航-->
                          <div Style=" text-align:right;">
                             <table id="paging" cellpadding="0" cellspacing="0" style="">
                               <tr>
                                 <td>
                                   <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
                                 </td>
                               </tr>
                              </table>  
                           </div>
                    </ContentTemplate>
               </asp:UpdatePanel>
                                         
                           
                      </td>

                      
                   </tr>
                </table>
             </td>
           </tr>
           <tr class="edit_detail_pager">
             <td style="height:38px; line-height:16pt;">
                 <asp:Button ID="btnPrevious1" runat="server" Text="上一步" Enabled="False" />
                 <asp:Button ID="btnNext1" runat="server" Text="下一步" OnClick="btnNext1_Click" />
             </td>
               
           </tr>
        </table>

        <table width="98%" style="display:<%=div02display%>" cellpadding="0" cellspacing="0">
           <tr><td style="height:30px; line-height:16pt;">已选择盘点的货位</td></tr>
           <tr>
             <td class="edit_body">
                <table style="width:100%; background-color:AliceBlue;" cellpadding="0" cellspacing="0">
                   <tr> 
                      <td style="vertical-align:top; padding-left:1px;"> 
                        <div style="width:100%; height:420px; overflow-x:auto; overflow-y:auto;">
                        <asp:DataGrid ID="dgSelectedCell" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgCell_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="序号">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="25px" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="90px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="90px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CURRENTPRODUCT" HeaderText="产品代码">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="90px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="产品名称" DataField="C_PRODUCTNAME"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <HeaderStyle Width="60px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                                        <HeaderStyle Width="60px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="QUANTITY" HeaderText="库存量">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                    </asp:BoundColumn>
                                </Columns>
                               <ItemStyle BackColor="AliceBlue" Height="26px" />
                               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" CssClass="GridHeader2" />
                               <AlternatingItemStyle BackColor="White" />
                           </asp:DataGrid> 
                        </div>
                      </td>
                   </tr>
                </table>
             </td>
           </tr>
           <tr  class="edit_detail_pager">
             <td style="height:38px; line-height:16pt;">
                 <asp:Button ID="btnPrevious2" runat="server" Text="上一步" />
                 <asp:Button ID="btnSave" runat="server" Text="生成盘点单" OnClick="btnSave_Click" />
             </td>
               
           </tr>
        </table>

    </td>
  </tr>
</table>

 <div style="position:absolute; padding-bottom:0px;">
     <asp:HiddenField ID="hideScrollTop" runat="server" />
     <asp:HiddenField ID="hdnWarehouseCode" runat="server" />
     <asp:HiddenField ID="hdnAreaCode" runat="server" />
     <asp:HiddenField ID="hdnShelfCode" runat="server" />
     <asp:HiddenField ID="hdnDepth" runat="server" Value="-1" />
 </div>  


<script>
function undo()
{
   return;
}
</script>              

</form>
  
</body>
</html>