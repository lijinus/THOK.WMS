<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseEditPage.aspx.cs" Inherits="Code_BasicInfo_WarehouseEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>仓库</title>
    <base target="_self" />
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
   <script type="text/javascript" src="../../JScript/SelectDialog.js?t=00"></script>
    <%--<script type="text/javascript" src="../../JScript/SelectDialog.js"></script>--%>
    <link href="../../css/FieldsetCss.css?0=t" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <script>
        function RefreshParent(path)
        {
           alert('仓库删除成功！');
           window.parent.document.getElementById('hdnRemovePath').value=path;
           window.parent.document.getElementById('btnRemoveNode').click();
        }


        function UpdateParent()
        {
           alert('仓库修改成功！');
           window.parent.document.getElementById('btnUpdateSelected').click();
        }
        
        function ReloadParent()
        {
           alert('仓库添加成功！');
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
        <legend>&nbsp;仓库</legend>
    <table>
      <tr>
         <td colspan="4"><asp:TextBox ID="txtWHID" runat="server" CssClass="HiddenControl" ></asp:TextBox>
             <input class="ButtonCreate" name="btnBack" onclick="openwin()" type="button" value="批量分配指定卷烟" />
             </td>
      </tr>
      <tr>
         <td class="tdTitle"><font color="red">*</font>仓库编码</td>
         <td><asp:TextBox ID="txtWhCode" runat="server" CssClass="TextBox" Width="140px"></asp:TextBox>
         </td>
         <td class="tdTitle"><font color="red">*</font>仓库名称</td> 
         <td><asp:TextBox ID="txtWhName" runat="server"  CssClass="TextBox" Width="140px"></asp:TextBox>
         </td>
      </tr>

      <tr>
         <td class="tdTitle">仓库简称</td> 
         <td><asp:TextBox ID="txtShortName" runat="server"  CssClass="TextBox" Width="140px"></asp:TextBox>
         </td>
         <td class="tdTitle">
             仓库类型</td> 
         <td> 
             <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                 <asp:ListItem Selected="True" Value="0">主库区</asp:ListItem>
                 <asp:ListItem Value="1">暂存区</asp:ListItem>
             </asp:DropDownList>
         </td>
     </tr>

       
      <tr>
          <td class="tdTitle">仓库面积</td> 
         <td><asp:TextBox ID="txtArea" runat="server"  CssClass="TextBoxRight" Width="140px"></asp:TextBox><span
                 style="font-size: 13pt; font-family: 宋体">㎡</span>
         </td>
          <td class="tdTitle">仓库容量</td> 
         <td><asp:TextBox ID="txtCapacity" runat="server"  CssClass="TextBoxRight" Width="140px"></asp:TextBox><span
                 style="font-size: 9pt">件</span>
         </td>
      </tr>
        
      <tr>
         <td class="tdTitle">默认单位</td> 
         <td>
            <asp:TextBox ID="txtDEFAULTUNIT" runat="server" CssClass="myinput" Width="48px"></asp:TextBox>
            <asp:TextBox ID="txtUNITNAME" runat="server" CssClass="myinput" Width="52px"></asp:TextBox>
                            <input id="Button2" onclick="SelectDialog2('txtDEFAULTUNIT,txtUNITNAME','WMS_UNIT','UNITCODE,UNITNAME')" class="ButtonBrowse" type="button" value="..." />
         </td>                                 
         <td class="tdTitle">是否启用</td> 
         <td><asp:DropDownList ID="ddlActive" runat="server">
                 <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                 <asp:ListItem Value="0">未启用</asp:ListItem>
             </asp:DropDownList></td>
     
      </tr>   
      <tr>
         <td class="tdTitle">备注</td> 
         <td colspan="3"><asp:TextBox ID="txtMemo" runat="server" CssClass="MultilineTextbox" Width="376px" Rows="10" TextMode="MultiLine" MaxLength="246"></asp:TextBox>
         </td>
      </tr> 
              <tr>
	            <td style="height:35px; text-align:center;" colspan="4">
                          &nbsp;&nbsp;
                          <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保存"  OnClick="btnSave_Click" OnClientClick="return CheckBeforeSubmit()" />&nbsp;
                    &nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="button" Enabled="False"
                        OnClick="btnDelete_Click" Text="删除" OnClientClick="return DeleteConfirm()"/>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="取消" OnClick="btnCancel_Click" Visible="False"  />
              </tr> 
    </table>
   </fieldset> 
    </form>
<script>
    function CheckBeforeSubmit()
    {
        var code=document.getElementById('txtWhCode').value;
        var name=document.getElementById('txtWhName').value;//document.getElementById('txtTitle').value.trim();


        if(code=="")
        {
           alert('仓库编码不能为空！');
           return false;
        }
        if(name=="")
        {
           alert('仓库名称不能为空！');
           return false;
        }
    }    
</script>
</body>
</html>
