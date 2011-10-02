<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseShelfEditPage.aspx.cs" Inherits="Code_BasicInfo_WarehouseShelfEdit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>库区货架</title>
    <base target="_self" />
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/FieldsetCss.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../../JScript/SelectDialog.js?t=00"></script>
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <script>
        function RefreshParent(path)
        {
           alert('货架删除成功！');
           window.parent.document.getElementById('hdnRemovePath').value=path;
           window.parent.document.getElementById('btnRemoveNode').click();
        }
        function UpdateParent()
        {
           alert('货架修改成功！');
           window.parent.document.getElementById('btnUpdateSelected').click();
        }
        function ReloadParent()
        {
           alert('货架添加成功！');
           window.parent.document.getElementById('btnReload').click();
        }
         function openwin()
        {
　　     window.open ("BatchAssignedProduct.aspx","", "height=410, width=600,top=200px,left=300px, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
　　    }
    </script>
</head>
<body style="margin-left:20px;">
    <form id="form1" runat="server">
            <fieldset style="width: 509px">
                <legend>货架</legend>   
                   <table>
                      <tr><td colspan="4">
                      <asp:TextBox ID="txtShelfID" runat="server" CssClass="HiddenControl"></asp:TextBox>
                          <input class="ButtonCreate" name="btnBack" onclick="openwin()" type="button" value="批量分配指定卷烟" /></td>
                       </tr>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font> 仓库编码</td>
                         <td>
                             <asp:TextBox ID="txtWhCode" runat="server" CssClass="TextBox" Width="140px" onfocus="CannotEdit(this)"></asp:TextBox></td>
                         <td class="tdTitle"><font color="red">*</font>库区编码</td>
                         <td><asp:TextBox ID="txtAreaCode" runat="server" CssClass="TextBox" Width="140px" onfocus="CannotEdit(this)"></asp:TextBox></td>
                         
                      </tr>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font>货架编码</td> 
                         <td><asp:TextBox ID="txtShelfCode" runat="server"  CssClass="TextBox" Width="140px"></asp:TextBox>
                         </td>
                         <td class="tdTitle"><font color="red">*</font>货架名称</td> 
                         <td><asp:TextBox ID="txtShelfName" runat="server"  CssClass="TextBox" Width="140px"></asp:TextBox>
                         </td>
                      </tr>

                      <tr>
                         <td class="tdTitle">
                             图形化X轴</td> 
                         <td><asp:TextBox ID="txtImgX" runat="server"  CssClass="TextBoxRight" Width="140px" onblur="IsNumber(this,'图形化X轴')">0.00</asp:TextBox>
                         </td>
                         <td class="tdTitle">
                             图形化Y轴</td> 
                         <td><asp:TextBox ID="txtImgY" runat="server"  CssClass="TextBoxRight" Width="140px" onblur="IsNumber(this,'图形化Y轴')">0.00</asp:TextBox>
                         </td>
                      </tr>  
                      
                      <tr>
                         <td class="tdTitle">货架行数</td> 
                         <td><asp:TextBox ID="txtCellRows" runat="server"  CssClass="TextBoxRight" Width="140px" onblur="IsNumber(this,'货架行数')">3</asp:TextBox>
                         </td>
                         <td class="tdTitle">货架列数</td> 
                         <td><asp:TextBox ID="txtCellCols" runat="server"  CssClass="TextBoxRight" Width="140px" onblur="IsNumber(this,'货架列数')"></asp:TextBox>
                         </td>
                      </tr>  
                      <tr>

                      </tr>    
                      <tr>
                         <td class="tdTitle">是否启用</td> 
                         <td style="text-align: left"><asp:DropDownList ID="ddlActive" runat="server">
                                 <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                                 <asp:ListItem Value="0">未启用</asp:ListItem>
                             </asp:DropDownList></td>
                             <td class="tdTitle"><font color="red">*</font>库区类别</td>
                             <td><asp:TextBox ID="txtAreaType" runat="server"  CssClass="TextBoxRight" Width="140px" onfocus="CannotEdit(this)"></asp:TextBox>
                         </td>
                     
                      </tr>    
                      <tr>
                         <td class="tdTitle">备注</td> 
                         <td colspan="3" style="text-align: left"><asp:TextBox ID="txtMemo" runat="server"  CssClass="MultilineTextBox" Width="376px" Rows="10" TextMode="MultiLine"></asp:TextBox>
                         </td>
                      </tr> 
                     
                      <tr><td colspan="4"  style="height:35px; text-align:center;">
                          &nbsp;<asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button" OnClick="btnSave_Click" OnClientClick="return CheckBeforeSubmit()"/>
                          &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnDelete" runat="server" CssClass="button"
                              Enabled="False" OnClick="btnDelete_Click" Text="删除" OnClientClick="return DeleteConfirm()" /><asp:Button ID="btnContinue" runat="server" Text="继续增加" CssClass="button" OnClick="btnContinue_Click" Enabled="False" Visible="False"/><asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="button" OnClick="btnCancel_Click" Visible="False"/></td></tr>                                                                                
                    </table>  
              </fieldset>
    </form>
<script>
function CheckBeforeSubmit()
{
    var shelfcode=document.getElementById('txtShelfCode').value;
    var shelfname=document.getElementById('txtShelfName').value;//document.getElementById('txtTitle').value.trim();
    var cols=document.getElementById('txtCellCols').value;
    var rows=document.getElementById('txtCellRows').value;
    var areatype=document.getElementById('txtAreaType').value;

    if(shelfcode=="")
    {
       alert('货架编码不能为空！');
       return false;
    }
    if(shelfname=="")
    {
       alert('货架名称不能为空！');
       return false;
    }
    
    if(cols=="")
    {
       alert('货架列数不能为空！');
       return false;
    }
    
    if(rows=="")
    {
       alert('货架行数不能为空！');
       return false;
    }
    if(areatype=="")
    {
        alert('库区类别不能为空！');
        return false;
    }
    
}
</script>    
</body>
</html>
