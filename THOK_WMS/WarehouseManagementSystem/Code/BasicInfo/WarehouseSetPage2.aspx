<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseSetPage2.aspx.cs" Inherits="Code_BasicInfo_WarehouseSetPage2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
    <script type="text/javascript">
        function loadEventCookie()
        {
            var strCook = document.cookie;
            document.title=strCook;
            if(strCook.indexOf("!~")!=0){
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS+2,intE);
                document.getElementById("div_tree").scrollTop = strPos;
            }
        }
        function setDivPosition()
        {
            var intY = document.getElementById("div_tree").scrollTop;
            document.title = intY;
            document.cookie = "yPos=!~" + intY + "~!";
        }
        
    </script>
</head>
<body style="background-image:url(../);" onload="loadEventCookie()">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
               </ContentTemplate>
               <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btnReload" EventName="Click" />
                  <asp:AsyncPostBackTrigger ControlID="btnRemoveNode" EventName="Click" />  
                  <asp:AsyncPostBackTrigger ControlID="btnUpdateSelected" EventName="Click" /> 
               </Triggers>
        </asp:UpdatePanel>  
  
    <table style="width:100%; background-color:WHITE;" cellpadding="0" cellspacing="0">
       <tr>
         <td colspan="2">
           <table class="OperationBar" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <asp:Button ID="btnNewWarehouse" Text="新增仓库" runat="server" CssClass="ButtonCreate"  OnClientClick="return OpenEditWarehouse()" />  
                    <asp:Button ID="btnNewArea" runat="server" Text="增加库区" CssClass="ButtonCreate" OnClientClick="return OpenEditArea()" Enabled="False"/>
                    <asp:Button ID="btnNewShelf" runat="server" Text="增加货架" CssClass="ButtonCreate" OnClientClick="return OpenEditShelf()" Enabled="False"/>
                    <asp:Button ID="btnNewCell" runat="server" Text="增加货位" CssClass="ButtonCreate" OnClientClick="return OpenEditCell()" Enabled="False" />
                 <asp:Button ID="btnCancel" Text="退出" runat="server" CssClass="ButtonExit" OnClick="btnCancel_Click"  />            
                 </td>
              </tr>
           </table>
         </td>
       </tr>
       <tr>
          <td style="" class="SideBar"><!--仓库架构-->
            <div id="div_tree" style="overflow-x:hidden; overflow-y:auto; width:280px; height:530px;"  onscroll="setDivPosition()" onmouseup="loadEventCookie()" onmouseover="loadEventCookie()" >
              <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged">
                  <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
              </yyc:smarttreeview>
            </div>
          </td>
          
          <td style="vertical-align:top; padding-left:10px;"> <!--编辑区-->
            <div style="height:24px;vertical-align:middle; width:100% ">
               <img src="../../images/ico_home.gif" border="0" />当前选中的节点：
               <asp:Label ID="lblCurrentNode" runat="server" ForeColor="#404040"></asp:Label>
                <%--<asp:Button ID="btnBatch" Text="批量为仓库分配制定卷烟" runat="server" CssClass="ButtonCreate"  />--%></div>
            <div style="width:100%; height:500px; overflow-x:auto; overflow-y:auto;">
               <iframe id="frame" runat="server" src="" style="width:100%; height:480px;" bordercolor="white" frameborder="no"></iframe>
            </div>
          </td>
       </tr>
    </table>
    

 <div>
     <asp:HiddenField ID="hideScrollTop" runat="server" />
     <asp:HiddenField ID="hdnWarehouseCode" runat="server" />
     <asp:HiddenField ID="hdnAreaCode" runat="server" />
     <asp:HiddenField ID="hdnShelfCode" runat="server" />
     <asp:HiddenField ID="hdnAreaType" runat="server" />
     <asp:HiddenField ID="hdnDepth" runat="server" Value="-1" />
     <asp:HiddenField ID="hdnRemovePath" runat="server" Value="/" />
 </div>  
 <asp:Button ID="btnReload" runat="server" CssClass="HiddenControl" Text="" OnClick="btnReload_Click" />
 <asp:Button ID="btnRemoveNode" runat="server" CssClass="HiddenControl" Text="" OnClick="btnRemoveNode_Click"/>
 <asp:Button ID="btnUpdateSelected" runat="server" CssClass="HiddenControl" Text="" OnClick="btnUpdateSelected_Click"/>  



    </form>
<script>

function OpenEditWarehouse(strWH_CODE)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="0";
    if(strWH_CODE==null)
    {  
          document.getElementById('frame').src="WarehouseEditPage.aspx?time="+time; 
          return false;
    }                                 
}

function OpenEditArea(strAreaID)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="1";
    var whcode=document.getElementById('hdnWarehouseCode').value;
    if(strAreaID==null)
    {
       document.getElementById('frame').src="WarehouseAreaEditPage.aspx?WHCODE="+whcode+"&time="+time;
       return false;
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
        var areatype=document.getElementById('hdnAreaType').value;
        document.getElementById('frame').src="WarehouseShelfEditPage.aspx?WHCODE="+whcode+"&AREACODE="+areacode+"&AREATYPE="+areatype+"&time="+time;
        return false;
    }
}

function OpenEditCell(strCellID)
{
    var date=new Date();
    var time=date.getMilliseconds()+date.getSeconds();
    document.getElementById('hdnDepth').value="3";
    var areatype=document.getElementById('hdnAreaType').value;
    if(strCellID==null)
    {
        var shelfcode=document.getElementById('hdnShelfCode').value;
        document.getElementById('frame').src="WarehouseCellEditPage.aspx?SHELFCODE="+shelfcode+"&AREATYPE="+areatype+"&time="+time;
        return false;
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

function postBackByObject()
{
//   document.getElementById('hideScrollTop').value=document.getElementById('tvWarehouse').parentElement.scrollTop;
}
//document.getElementById('tvWarehouse').parentElement.scrollTop=document.getElementById('hideScrollTop').value;



</script>    
</body>
</html>