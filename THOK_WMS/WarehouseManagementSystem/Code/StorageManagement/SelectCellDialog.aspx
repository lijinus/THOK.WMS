<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCellDialog.aspx.cs" Inherits="Code_StorageManagement_SelectCellDialog" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>货位选择</title>
    <base target="_self" />
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <style>
    .SideBar
    {
       
       padding-top:5px;
       vertical-align:top; 
       width:184px; 
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
function ReturnValue(fieldValue){
   if (window.document.all)//IE判断window.showModalDialog!=null
   {
     window.returnValue=fieldValue;
     window.opener=null;
     window.close();
   }
   else
   {
//    returnValue=fieldValue;
//    window.close();
       var aryValue=new Array();
       aryValue=fieldValue.split('|');
       var aryTarget=new Array();
       aryTarget=document.getElementById('hideTargetControls').value.split(',');
       for(var i=0;i<aryTarget.length;i++)
       {
          var e=parent.opener.document.getElementById(aryTarget[i]);
          if(e!=null)
          {
            e.value=aryValue[i];
          }
       }  
       window.close();      
   }
}
function window_onload()
{
    if(window.document.all)//IE
    {
    //对于IE直接读数据
    //        var txtInput=window.document.getElementById("txtInput");
    //        txtInput.value=window.dialogArguments;
    }
    else//FireFox
    {
    //获取参数
    //window.dialogArguments=window.opener.myArguments;
    //var txtInput=window.document.getElementById("txtInput");
    //txtInput.value=window.dialogArguments;
    }
}

function window_onunload() {
//对于Firefox需要进行返回值的额外逻辑处理
    if(!window.document.all)//FireFox
    {
       window.opener.myAction.returnAction(window.returnValue)
    }
}

</script>  
</head>
<body style="background-image:url(../);">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
               </ContentTemplate>
        </asp:UpdatePanel>  
  
    <table style="width:100%; background-color:WHITE;" cellpadding="0" cellspacing="0">
       <tr>
          <td style=" background-color:#e4e4e4;" class="SideBar"><!--仓库架构-->
            <div style="overflow-x:hidden; overflow-y:auto; width:184px; height:400px;">
              <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged">
                  <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
              </yyc:smarttreeview>
            </div>
          </td>
          
          <td style="vertical-align:top; padding-left:10px;"> <!--编辑区-->
            <div style="height:24px;vertical-align:middle; width:100% ">
               <img src="../../images/ico_home.gif" border="0" />当前选中的节点：
               <asp:Label ID="lblCurrentNode" runat="server" ForeColor="#404040"></asp:Label>
            </div>
            <div style="width:100%; height:380px; overflow-x:auto; overflow-y:auto;">
                <asp:DataGrid ID="dgCell" runat="server" AutoGenerateColumns="False" OnItemDataBound="dgCell_ItemDataBound" Width="100%">
                    <AlternatingItemStyle BackColor="AliceBlue" />
                    <ItemStyle BackColor="White" Height="26px" />
                    <HeaderStyle CssClass="GridHeader2" />
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
                </asp:DataGrid>
            </div>
          </td>
       </tr>
    </table>
    

 <div>
     <asp:HiddenField ID="hideScrollTop" runat="server" />
     <asp:HiddenField ID="hdnWarehouseCode" runat="server" />
     <asp:HiddenField ID="hdnAreaCode" runat="server" />
     <asp:HiddenField ID="hdnShelfCode" runat="server" />
     <asp:HiddenField ID="hdnDepth" runat="server" Value="-1" />
     <asp:HiddenField ID="hdnRemovePath" runat="server" Value="/" />
 </div>  

    </form>
<script>

function KeepSilent()
{
  return;
}

function postBackByObject()
{
//   document.getElementById('hideScrollTop').value=document.getElementById('tvWarehouse').parentElement.scrollTop;
}
//document.getElementById('tvWarehouse').parentElement.scrollTop=document.getElementById('hideScrollTop').value;
</script>    
</body>
</html>