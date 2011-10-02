<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseCellEditPage.aspx.cs" Inherits="Code_BasicInfo_WarehouseCellEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>货架货位</title>
    <base target="_self" />
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <script type="text/javascript" src="../../JScript/SelectDialog.js?t=00"></script>
    <link href="../../css/FieldsetCss.css" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function RefreshParent(path)
        {
           alert('货位删除成功！');
           window.parent.document.getElementById('hdnRemovePath').value=path;
           window.parent.document.getElementById('btnRemoveNode').click();
        }
        
        function UpdateParent()
        {
           alert('货位修改成功！');
           window.parent.document.getElementById('btnUpdateSelected').click();
        }
        
        function ReloadParent()
        {
           alert('货位添加成功！');
           window.parent.document.getElementById('btnReload').click();
        }
        function openwin()
        {
　　     window.open ("BatchAssignedProduct.aspx","", "height=410px, width=600px,top=200px,left=300px, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
　　    }
    </script>
</head>
<body style="margin-left:20px;">
    <form id="form1" runat="server">
         <fieldset style="width: 509px">
                  <legend>货位</legend>   
                   <table>
                      <tr><td colspan="4"><asp:TextBox ID="txtCELLID" runat="server"  CssClass="HiddenControl"></asp:TextBox>
                          <input class="ButtonCreate" name="btnBack" onclick="openwin()" type="button" value="批量分配指定卷烟" /></td></tr>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font>货架编码</td>
                         <td  ><asp:TextBox ID="txtShelfCode" runat="server" CssClass="TextBox" onfocus="CannotEdit(this)"></asp:TextBox>
                         </td>
                         <td class="tdTitle"></td>
                         <td><asp:TextBox ID="txtAreaType" runat="server" CssClass="TextBox" ></asp:TextBox></td>
                      </tr>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font>货位编码</td> 
                         <td  ><asp:TextBox ID="txtCellCode" runat="server"  CssClass="TextBox" ></asp:TextBox>
                         </td>
                         <td class="tdTitle"><font color="red">*</font>货位名称</td> 
                         <td><asp:TextBox ID="txtCellName" runat="server"  CssClass="TextBox" ></asp:TextBox>
                         </td>
                      </tr>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font>货位限量</td> 
                         <td  ><asp:TextBox ID="txtMaxQty" runat="server"  CssClass="TextBoxRight" onblur="IsNumber(this,'货位限量')" >0</asp:TextBox>
                         </td>
                         <td class="tdTitle">
                             货位层位置</td> 
                         <td>
                             &nbsp;<asp:DropDownList ID="ddlLayer" runat="server">
                             </asp:DropDownList></td>
                      </tr> 
                      <tr>
                         <td class="tdTitle"><asp:TextBox ID="txtAssignedProductCode" runat="server"  CssClass="HiddenControl" Height="0"></asp:TextBox>
                             指定卷烟</td> 
                         <td  ><asp:TextBox ID="txtAssignedProductName" runat="server"  CssClass="TextBox"></asp:TextBox>
                         <input id="Button1" type="button" value="..." onclick="SelectDialog2('txtAssignedProductCode,txtAssignedProductName','WMS_PRODUCT','PRODUCTCODE,PRODUCTNAME');" class="ButtonBrowse"/>
                         
                         </td>
                         <td class="tdTitle">
                             是否启用</td> 
                         <td><asp:DropDownList ID="ddlActive" runat="server">
                                 <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                                 <asp:ListItem Value="0">未启用</asp:ListItem>
                             </asp:DropDownList></td>
                      </tr> 
                      <tr>
                        <td class="tdTitle">
                            <font color="red">*</font>货位单位</td>
                        <td >
                            <asp:TextBox ID="txtUnitCode" runat="server" CssClass="TextBox" Width="71px" ></asp:TextBox><asp:TextBox ID="txtUnitName" runat="server" CssClass="TextBox" Width="54px" onfocus="CannotEdit(this)"></asp:TextBox>
                            <input id="Button2" type="button" value="..." onclick="SelectDialog2('txtUnitCode,txtUnitName','WMS_UNIT','UNITCODE,UNITNAME');" class="ButtonBrowse"/></td>
                        <td class="tdTitle">
                            虚拟货位</td>
                        <td><asp:DropDownList ID="ddlVirtual" runat="server">
                            <asp:ListItem Selected="True" Value="0">不是</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList></td>
                      </tr> 
                      
                      <tr>
                        <td class="tdTitle">
                            货位设备标识</td>
                        <td >
                            <asp:TextBox ID="txtPalletID" runat="server" CssClass="TextBox" ></asp:TextBox></td>
                        <td class="tdTitle">
                            电子标签组</td>
                        <td>
                            <asp:TextBox ID="txtEGroup" runat="server" CssClass="TextBox" >-1</asp:TextBox></td>
                      </tr>  
                      <tr>
                         <td class="tdTitle">
                            电子标签端口</td>
                         <td >
                             <asp:TextBox ID="txtECom" runat="server" CssClass="TextBox" >-1</asp:TextBox></td>
                         <td class="tdTitle">电子标签地址</td>
                         <td>
                             <asp:TextBox ID="txtEAddress" runat="server" CssClass="TextBox" >-1</asp:TextBox></td>
                      </tr>
    
                      <tr><td colspan="4"  style="height:35px; text-align:center;">
                          &nbsp;<asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button" OnClick="btnSave_Click" OnClientClick="return CheckBeforeSubmit()"/>
                          &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="button"
                              Enabled="False" OnClick="btnDelete_Click" Text="删除" OnClientClick="return DeleteConfirm()" /><asp:Button ID="btnContinue" runat="server" CssClass="button" Enabled="False" OnClick="btnContinue_Click"
                              Text="继续增加" Visible="False" /><asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="button" OnClick="btnCancel_Click" Visible="False"/></td></tr>                                                                                                                                                                      
                    </table>  
              </fieldset>
    </form>
<script type="text/javascript">
function CheckBeforeSubmit()
{
    var cellcode=document.getElementById('txtCellCode').value;
    var cellname=document.getElementById('txtCellName').value;//document.getElementById('txtTitle').value.trim();
    var maxqty=document.getElementById('txtMaxQty').value;
    var unitcode=document.getElementById('txtUnitCode').value;
     var productname=document.getElementById('txtAssignedProductName').value;
      var areatype=document.getElementById('txtAreaType').value;
     if(productname=="")
     {
        document.getElementById('txtAssignedProductCode').value="";
     }
    if(cellcode=="")
    {
       alert('货位编码不能为空！');
       return false;
    }
    if(cellname=="")
    {
       alert('货位名称不能为空！');
       return false;
    }
    
    if(maxqty=="")
    {
       alert('货位限量不能为空！');
       return false;
    }
    
    if(unitcode=="")
    {
       alert('货位单位不能为空！');
       return false;
    }
    if(areatype=="")
    {
        alert('库区类别不能为空！');
        return false;
    }
}

function clear(id)
{
   alert(id)
   document.getElementById(id).value="";
}
</script>
</body>
</html>
