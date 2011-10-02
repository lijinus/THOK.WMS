<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseSetPage.aspx.cs" Inherits="Code_BasicInfo_Warehouse" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>仓库资料设置</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <style>
    .SideBar
    {
       background-image: url(../../images/bar_bg.gif);
       background-position:right;
       background-repeat:no-repeat;
       background-position-y:-10px;
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
</head>
<body style="background-image:url(../);">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
  
    <table style="width:100%; background-color:WHITE;" cellpadding="0" cellspacing="0">
       <tr>
         <td colspan="2">
           <table class="OperationBar" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <asp:Button ID="btnNewWarehouse" Text="新增仓库" runat="server" CssClass="ButtonCreate"  OnClientClick="return OpenEditWarehouse()" />  
                    <asp:Button ID="btnNewArea" runat="server" Text="增加库区" CssClass="ButtonCreate" OnClientClick="return OpenEditArea()" Enabled="False"/>
                    <asp:Button ID="btnNewShelf" runat="server" Text="增加货架" CssClass="ButtonCreate" OnClientClick="return OpenEditShelf()" Enabled="False"/>
                    <asp:Button ID="btnNewCell" runat="server" Text="增加货位" CssClass="ButtonCreate" OnClientClick="return OpenEditCell()" Enabled="False"/>
                 <asp:Button ID="btnCancel" Text="退出" runat="server" CssClass="ButtonExit" OnClick="btnCancel_Click"  />            
                 </td>
              </tr>
           </table>
         </td>
       </tr>
       <tr>
          <td style="" class="SideBar"><!--仓库架构-->
            <div style="overflow-x:hidden; overflow-y:auto; width:213px; height:510px;">
              <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged">
                  <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
              </yyc:smarttreeview>
            </div>
          </td>
          
          <td style="vertical-align:top; padding-left:10px;"> <!--编辑区-->
            <div style="height:24px;vertical-align:middle; width:100% ">
               <img src="../../images/ico_home.gif" border="0" />当前选中的节点：
               <asp:Label ID="lblCurrentNode" runat="server" ForeColor="#C00000"></asp:Label>
            </div>
            
            <div class="topic">仓库</div>
            <div style="height:70px; overflow-x:auto; overflow-y:auto;"><!--仓库-->
                <asp:DataGrid ID="dgHouse" runat="server" AutoGenerateColumns="False" BackColor="#768CA6" CellPadding="1" CellSpacing="1" GridLines="None" OnItemDataBound="dgHouse_ItemDataBound" Width="100%" OnDeleteCommand="dgHouse_DeleteCommand">
                    <HeaderStyle BackColor="#E9F3FC" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:ButtonColumn CommandName="Delete">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="70px" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="WH_ID" HeaderText="WH_ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="WH_CODE" HeaderText="仓库编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="WH_NAME" HeaderText="仓库名称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SHORTNAME" HeaderText="简称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="WH_TYPE" HeaderText="仓库类型">
                            <HeaderStyle Width="60px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UNITNAME" HeaderText="默认单位">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="WH_AREA" HeaderText="面积">
                            <HeaderStyle Width="60px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CAPACITY" HeaderText="容量">
                            <HeaderStyle Width="60px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ISACTIVE" HeaderText="是否启用">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle BackColor="White" Height="18px" ForeColor="#254E80" />
                </asp:DataGrid>
            </div>
            
            <div class="topic">库区</div>
            <div style="height:80px; overflow-x:auto; overflow-y:auto;"><!--库区--> 
                <asp:DataGrid ID="dgArea" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="#768CA6" CellPadding="1" CellSpacing="1" GridLines="None" OnItemDataBound="dgArea_ItemDataBound" OnDeleteCommand="dgArea_DeleteCommand">
                    <HeaderStyle BackColor="#E9F3FC" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:ButtonColumn CommandName="Delete" HeaderText="操作">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="70px" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="AREA_ID" HeaderText="AREA_ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="WH_CODE" HeaderText="仓库编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AREACODE" HeaderText="库区编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AREANAME" HeaderText="库区名称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SHORTNAME" HeaderText="库区简称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ISACTIVE" HeaderText="是否启用">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle BackColor="White" Height="18px" ForeColor="#254E80" />
                </asp:DataGrid>
            </div>
            
            <div class="topic">货架</div>
            <div style="height:120px; overflow-x:auto; overflow-y:auto;"><!--货架-->
                <asp:DataGrid ID="dgShelf" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="#768CA6" CellPadding="1" CellSpacing="1" GridLines="None" OnItemDataBound="dgShelf_ItemDataBound" OnDeleteCommand="dgShelf_DeleteCommand">
                    <HeaderStyle BackColor="#E9F3FC" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:ButtonColumn CommandName="Delete">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="70px" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="SHELF_ID" HeaderText="SHELF_ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="AREACODE" HeaderText="库区编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SHELFCODE" HeaderText="货架编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SHELFNAME" HeaderText="货架名称"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CELLROWS" HeaderText="货架行数">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Width="55px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CELLCOLS" HeaderText="货架列数">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Width="55px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ISACTIVE" HeaderText="是否启用">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle BackColor="White" Height="18px" ForeColor="#254E80" />
                </asp:DataGrid>
            </div>
            <table id="table1" cellpadding="0" cellspacing="0" style="width:500px;">
               <tr>
                 <td>
                   <NetPager:AspNetPager ID="pager1" runat="server"  ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" OnPageChanging="pager1_PageChanging" ></NetPager:AspNetPager>
                 </td>
               </tr>
            </table> 
            
            
             <div class="topic">货位</div>
            <div style="height:130px; overflow-x:auto; overflow-y:auto;"><!--货位-->
                <asp:DataGrid ID="dgCell" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="#768CA6" CellPadding="1" CellSpacing="1" GridLines="None" OnItemDataBound="dgCell_ItemDataBound" OnDeleteCommand="dgCell_DeleteCommand">
                    <HeaderStyle BackColor="#E9F3FC" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:ButtonColumn CommandName="Delete">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="70px" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="CELL_ID" HeaderText="CELL_ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SHELFCODE" HeaderText="货架编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                            <HeaderStyle Width="70px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                            <HeaderStyle Width="70px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MAX_QUANTITY" HeaderText="最大存量">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Width="50px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="A_PRODUCTNAME" HeaderText=" 指定产品"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UNITNAME" HeaderText="货位单位">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="60px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ISACTIVE" HeaderText="是否启用">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle BackColor="White" Height="18px" ForeColor="#254E80" />
                </asp:DataGrid>
            </div>
            <table id="paging2" cellpadding="0" cellspacing="0" style="width:500px;">
               <tr>
                 <td>
                   <NetPager:AspNetPager ID="pager2" runat="server"  ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" OnPageChanging="pager2_PageChanging"></NetPager:AspNetPager>
                 </td>
               </tr>
            </table> 
          </td>
       </tr>
    </table>
    
 <div>
     <asp:HiddenField ID="hdnWarehouseCode" runat="server" />
     <asp:HiddenField ID="hdnAreaCode" runat="server" />
     <asp:HiddenField ID="hdnShelfCode" runat="server" />
     <asp:HiddenField ID="hdnDepth" runat="server" Value="-1" />
 </div>  
 
              </ContentTemplate>
        </asp:UpdatePanel>  
<asp:Button ID="btnReload" runat="server" CssClass="HiddenControl" Text="" OnClick="btnReload_Click" />
    </form>
<script>
function OpenEditWarehouse(strWH_CODE)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="0";
    if(strWH_CODE==null)
    {
        window.showModalDialog("WarehouseEditPage.aspx?time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=430px");
        
        document.getElementById('btnReload').click();   
        
    }  
    else
    {
        window.showModalDialog("WarehouseEditPage.aspx?WH_CODE="+strWH_CODE+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=430px");
    }                               
}

function OpenEditArea(strAreaID)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="1";
    if(strAreaID==null)
    {
        var whcode=document.getElementById('hdnWarehouseCode').value;
        window.showModalDialog("WarehouseAreaEditPage.aspx?WHCODE="+whcode+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=400px");
//        alert();
        document.getElementById('btnReload').click();
    }
    else
    {  
        window.showModalDialog("WarehouseAreaEditPage.aspx?AREA_ID="+strAreaID+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=400px");
//        document.getElementById('btnReload').click(); 
    }
}

function OpenEditShelf(strShelfID)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="2";
    if(strShelfID==null)
    {
        var whcode=document.getElementById('hdnWarehouseCode').value;
        if(whcode=="")
        {
           alert("请选择仓库！")
           return;
        }
        var areacode=document.getElementById('hdnAreaCode').value;
        if(areacode=="")
        {
           alert("请选择库区！")
           return;
        }
        window.showModalDialog("WarehouseShelfEditPage.aspx?WHCODE="+whcode+"&AREACODE="+areacode+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=460px");
                                                 
        document.getElementById('btnReload').click(); 
    }
    else
    {
        window.showModalDialog("WarehouseShelfEditPage.aspx?SHELF_ID="+strShelfID+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=460px");
                                                 
       
    }
}

function OpenEditCell(strCellID)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="3";
    if(strCellID==null)
    {
        var shelfcode=document.getElementById('hdnShelfCode').value;
        window.showModalDialog("WarehouseCellEditPage.aspx?SHELFCODE="+shelfcode+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=380px");  
        document.getElementById('btnReload').click(); 
    }
    else
    {
       window.showModalDialog("WarehouseCellEditPage.aspx?CELL_ID="+strCellID+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=555px;dialogHeight=380px");  
       //document.getElementById('btnReload').click(); 
    }
}

function DeleteConfirm()
{
      if(confirm('确定要删除选择的数据？','删除提示'))
      {
         return true;
      }
      else
      {
         return false;
      }
}

function KeepSilent()
{
  return;
}

</script>    
</body>
</html>
