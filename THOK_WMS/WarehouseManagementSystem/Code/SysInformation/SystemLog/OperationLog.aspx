<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperationLog.aspx.cs" Inherits="Code_SysInformation_SystemLog_OperationLog" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户信息</title>
    <script type="text/javascript" src="../../../JScript/setday9.js"></script>
    <script type="text/javascript" src="../../../JScript/Check.js?t=00"></script>
    <link href="../../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../../css/op.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
          <ContentTemplate>

          <!--数据显示-->
          <asp:Panel ID="pnlList" runat="server" Height="480px" Width="100%">
             <!--工具栏-->
             <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
                  <table style="width:100%; height:20px;">
                     <tr>
                       <td style="height: 22px">
                           <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="LoginUser">登录用户</asp:ListItem>
                               <asp:ListItem Value="LoginModule">登录模块</asp:ListItem>
                           </asp:DropDownList>
                           <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox"  Width="120"></asp:TextBox>
                           时间从
                           <asp:TextBox ID="txtDateStart" runat="server" CssClass="TextBox" Width="70"></asp:TextBox>
                           <input type="button" class="ButtonDate" onclick="setday(document.getElementById('txtDateStart'))"/>
                           至
                           <asp:TextBox ID="txtDateEnd" runat="server" CssClass="TextBox"  Width="70"></asp:TextBox>
                           <input type="button" class="ButtonDate" onclick="setday(document.getElementById('txtDateEnd'))"/>
                           <asp:RadioButton GroupName="order" ID="rbASC" runat="server" Text="升" Checked="True" />
                           <asp:RadioButton GroupName="order" ID="rbDESC" runat="server" Text="降" />
                           <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                           <asp:Button ID="btnDelete" runat="server" Text="删除"　CssClass="ButtonDel" OnClick="btnDelete_Click" OnClientClick="return DelConfirm('btnDelete')" Enabled="False"/>
                           <asp:Button ID="btnDeleteAll" runat="server" Text="清空" CssClass="ButtonClearAll" OnClientClick="return ClearConfirm();" Enabled="False" OnClick="btnDeleteAll_Click" />
                           <asp:Button ID="btnExit" runat="server" Text="退出"  OnClick="btnExit_Click" CssClass="ButtonExit" />
                        </td>
                     </tr>
                  </table>
             </asp:Panel>
             <!--数据-->
              <asp:Panel ID="pnlMain" runat="server" Height="480px" Style="position: relative; overflow-x:hidden; overflow-y:auto;" Width="100%">
                  <asp:GridView ID="gvMain" runat="server" Style="position: relative;table-layout:fixed;width:100%;"
                     OnRowDataBound="gvMain_RowDataBound" CssClass="GridStyle" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False">
                      <RowStyle BackColor="WhiteSmoke" Height="28px" />
                      <HeaderStyle CssClass="GridHeader" />
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                          <asp:TemplateField HeaderText="操作">
                              <HeaderStyle Width="30px" />
                          </asp:TemplateField>
                          <asp:BoundField DataField="LoginUser" HeaderText="登录用户" >
                              <HeaderStyle Width="120px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="LoginTime" HeaderText="登录时间" DataFormatString="{0:yyyy-MM-dd}" >
                              <HeaderStyle Width="120px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="LoginModule" HeaderText="登录模块" >
                              <HeaderStyle Width="150px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ExecuteOperator" HeaderText="执行操作" >
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
              </asp:Panel>
              <!--分页导航-->
              <asp:Panel ID="pnlNavigator" runat="server" Height="31px" Style="left: 0px; position: relative; top: 0px" Width="100%">
                 <table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
                   <tr>
                     <td>
                       <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
                     </td>
                   </tr>
                  </table>  
               </asp:Panel>
          </asp:Panel>  
            
           </ContentTemplate>
     </asp:UpdatePanel>
        <!--隐藏数据-->  
        <div>
        </div>
    </form>
<script>

//删除确认
function DelConfirm(btnID)
{
     var table=document.getElementById('gvMain');
     var hasChecked=false;
     for(var i=1;i<table.rows.length;i++)
     {
        var cell=table.rows[i].cells[0];
        var chk=cell.getElementsByTagName("INPUT");
        if(chk[0].checked)
        {
            hasChecked=true;
            break;
        }
     }
     
     if(!hasChecked)
     {
        alert('请选择要删除的数据！');
        return false;
     }
      if(confirm('确定要删除选择的数据？','删除提示'))
      {
         var btn=document.getElementById(btnID);
         btn.click();
         //window.location.reload();
      }
      else
      {
         return false;
      }
}

function ClearConfirm()
{
   if(confirm('确定要清空所有日志记录','清空提示'))
   {
     
   }
   else
   {
     return false;
   }
}
</script>
</body>
</html>