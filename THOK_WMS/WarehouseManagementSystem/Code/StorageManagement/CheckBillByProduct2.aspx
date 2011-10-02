<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckBillByProduct2.aspx.cs" Inherits="Code_StorageManagement_CheckBillByProduct" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>货位盘点</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <style>
    .SideBar
    {
       padding-top:1px;
       vertical-align:top; 
       padding-right:1px;
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
<body style="margin-right:0px; background-color:Red;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            </ContentTemplate>
       </asp:UpdatePanel>
   
<table width="100%" style="display:<%=div01display%>" cellpadding="0" cellspacing="1">
   <tr><td style=""> &nbsp; &nbsp; 请将要盘点的产品打<span style="color:red"><strong>√</strong></span></td></tr>
   <tr>
     <td style="height: 465px">
        <table style="width:100%; background-color:AliceBlue;" cellpadding="0" cellspacing="0">
           <tr>
              <td class="SideBar" width="250px" style="height: 461px">
                <div style="overflow-x:hidden; overflow-y:scroll; width:100%; height:440px;">
                    <asp:DataGrid ID="dgProduct" runat="server" AutoGenerateColumns="False" BackColor="LightGray"
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" ForeColor="Black"
                        GridLines="None" OnItemDataBound="dgProduct_ItemDataBound" CellSpacing="1">
                        <FooterStyle BackColor="#CCCCCC" />
                        <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                        <HeaderStyle BackColor="SkyBlue" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="70px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="120px" />
                            </asp:BoundColumn>
                        </Columns>
                        <ItemStyle BackColor="White" Height="24px" />
                    </asp:DataGrid>
                 </div>
                 <div>
                     <asp:RadioButton ID="rbCode" runat="server" GroupName="product" Checked="True" Text="产品代码" />
                     <asp:RadioButton ID="rbName" runat="server" GroupName="product" Text="产品名称"/><br />
                     <asp:TextBox ID="keywords" runat="server" onKeyUp="AutoSearch();" onKeyDown="AutoSearch();" autocomplete="off"></asp:TextBox>
                     <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click"></asp:LinkButton>
                 </div>
              </td>
              
              <td style="vertical-align:top; padding-left:1px; height: 461px;"> 
              
                <div style="height:24px;vertical-align:middle; width:100% ">
                   <img src="../../images/ico_home.gif" border="0" /></div>
                   
                <div style="width:100%; height:390px; overflow-x:auto; overflow-y:auto;">
                <asp:DataGrid ID="dgCell" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgCell_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify">
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
              </td>
           </tr>
        </table>
     </td>
   </tr>
   <tr>
     <td style="height:38px; line-height:16pt;">
         <asp:Button ID="btnPrevious1" runat="server" Text="上一步" Enabled="False" />
         <asp:Button ID="btnNext1" runat="server" Text="下一步" OnClick="btnNext1_Click" />
     </td>
       
   </tr>
</table>

<table width="100%" style="display:<%=div02display%>" cellpadding="0" cellspacing="1">
   <tr><td style="height:30px; line-height:16pt;">已选择盘点的货位</td></tr>
   <tr>
     <td>
        <table style="width:100%; background-color:AliceBlue;" cellpadding="0" cellspacing="0">
           <tr> 
              <td style="vertical-align:top; padding-left:1px;"> 
                <div style="width:100%; height:420px; overflow-x:auto; overflow-y:auto;">
                <asp:DataGrid ID="dgSelectedCell" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgCell_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify">
                        <Columns>
                            <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CURRENTPRODUCT" HeaderText="产品代码">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="产品名称" DataField="C_PRODUCTNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称"></asp:BoundColumn>
                            <asp:BoundColumn DataField="QUANTITY" HeaderText="库存量">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                       <ItemStyle BackColor="AliceBlue" Height="26px" />
                       <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                       <AlternatingItemStyle BackColor="White" />
                   </asp:DataGrid> 
                </div>
              </td>
           </tr>
        </table>
     </td>
   </tr>
   <tr>
     <td style="height:38px; line-height:16pt;">
         <asp:Button ID="btnPrevious2" runat="server" Text="上一步" />
         <asp:Button ID="btnSave" runat="server" Text="生成盘点单" OnClick="btnSave_Click" />
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
 document.onkeydown=function()   
  {   
      AutoSearch(); 
  } 
  function AutoSearch()
  {
       document.getElementById('lnkSearch').click();
  }
</script>     

</form>
  
</body>
</html>