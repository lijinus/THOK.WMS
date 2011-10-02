<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemInf.aspx.cs" Inherits="Code_SysInfomation_SystemInf_Default" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="../../../css/css.css?T=09" type="text/css" />
    <style type="text/css">
    A
    {
	    color:black;
	    text-decoration:none;
    }

    A:hover
    {
	    color:#FE6103;
	    text-decoration:none; /*underline;*/
    }
    A:Active
    {
	    text-decoration:none;
    }    
    td { 
      font-size: 12px;
      color: #000000;
      line-height: 150%;
      }
    .sec1 { 
      background-color: #EEEEEE;
      cursor: hand;
      color: #000000;
      border-left: 1px solid #FFFFFF;
      border-top: 1px solid #FFFFFF;
      border-right: 1px solid gray;
      border-bottom: 1px solid #FFFFFF
      }
    .sec2 { 
      background-color: #D4D0C8;
      cursor: hand;
      color: #000000;
      border-left: 1px solid #FFFFFF; 
      border-top: 1px solid #FFFFFF; 
      border-right: 1px solid gray; 
      font-weight: bold; 
      }
    .main_tab {
      background-color: #D4D0C8;
      color: #000000;
      border-left:1px solid #FFFFFF;
      border-right: 1px solid gray;
      border-bottom: 1px solid gray; 
      }
      
    </style>
    
    <script type="text/javascript" src="../../../JScript/InfHintDiv.js"></script>
    <script language="javascript" type="text/javascript">
        function LostFocus(obj)
        {
            obj.blur();
        }
        
        function HasChanged(obj)
        {
            var objValue=obj.value;
            if(objValue==1)
            {
               document.getElementById("txtSys_BufferCache").readOnly=false;
            }
            else
            {
               document.getElementById("txtSys_BufferCache").readOnly=true;
            }
        }
        function DoResult()
        {
            alert('[提示]',strMessage,'确认')
        }
    </script>
</head>
<body style="margin:10 20 10 20;">
    <form id="form1" runat="server">

       
        <table width="100%" cellpadding="3" cellspacing="0" style="left: 0px; position: relative; top: 0px">
            <tr >
                <td colspan="2" style=" font-weight: <% =HeadFont[3]%>; font-size: <%=HeadFont[2] %>pt; color: <% =HeadFont[1]%>; font-family: <%=HeadFont[0] %>; ">
                    <asp:Label ID="labSystemSet" runat="server" Text="系统设置"></asp:Label></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labPageCount" runat="server" Text="每页记录数："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:TextBox ID="txtSys_PageCount" runat="server" Width="150px"></asp:TextBox></td>
            </tr>
            
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="Label1" runat="server" Text="分页是否显示数字页码："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                    <asp:DropDownList ID="ddl_ShowPageIndex" runat="server">
                        <asp:ListItem Selected="True" Value="true">是</asp:ListItem>
                        <asp:ListItem Value="false">否</asp:ListItem>
                    </asp:DropDownList>
                       </td>
            </tr>
            
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; height: 21px;">
                    <asp:Label ID="labPrintForm" runat="server" Text="打印形式："></asp:Label></td>
                <td colspan="1" style="width: 510px; height: 21px;">
                        <asp:DropDownList ID="ddlPrintForm" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>

            <tr>
                <td colspan="2" style=" font-weight: <% =HeadFont[3]%>; font-size: <%=HeadFont[2] %>pt; color: <% =HeadFont[1]%>; font-family: <%=HeadFont[0] %>; ">
                    <asp:Label ID="labGridSet" runat="server" Text="网格设置"></asp:Label></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; height: 21px;">
                    <asp:Label ID="labColumnTitleFont" runat="server" Text="标题文本："></asp:Label></td>
                <td colspan="1" style="width: 510px; height: 21px;">
                        <asp:TextBox ID="txtGrid_ColumnTitleFont" runat="server" Width="150px"></asp:TextBox><asp:Button ID="btnGrid_ColumnTitleFont" runat="server" Text="..." Width="24px" OnClick="btnGrid_ColumnTitleFont_Click" /></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labContentFont" runat="server" Text="内容文本："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:TextBox ID="txtGrid_ContentFont" runat="server" Width="150px"></asp:TextBox><asp:Button ID="btnGrid_ContentFont" runat="server" Text="..." Width="24px" OnClick="btnGrid_ContentFont_Click" /></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labSelectMode" runat="server" Text="记录的选中模式："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_SelectMode" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labDisplayRowColor" runat="server" Text="单数行颜色："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_DisplayRowColor" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labDisplayRowColorEven" runat="server" Text="双数行颜色："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_DisplayRowColorEven" runat="server" Width="150px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; height: 21px;">
                    <asp:Label ID="labColumnTextAlign" runat="server" Text="标题列的文本对齐方式："></asp:Label></td>
                <td colspan="3" style="width: 510px; height: 21px;">
                        <asp:DropDownList ID="ddlGrid_ColumnTextAlign" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labContentTextAlign" runat="server" Text="内容列的文本对齐方式："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_ContentTextAlign" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; height: 21px;">
                    <asp:Label ID="labMoneyColumnAlign" runat="server" Text="内容列的货币对齐方式："></asp:Label></td>
                <td colspan="1" style="width: 510px; height: 21px;">
                        <asp:DropDownList ID="ddlGrid_MoneyColumnAlign" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; height: 21px;">
                    <asp:Label ID="labNumberColumnAlign" runat="server" Text="内容列的数值对齐方式："></asp:Label></td>
                <td colspan="1" style="width: 510px; height: 21px;">
                        <asp:DropDownList ID="ddlGrid_NumberColumnAlign" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labIsRefreshBeforeAdd" runat="server" Text="新增操作后是否刷新网格："></asp:Label></td>
                <td colspan="3" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_IsRefreshBeforeAdd" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labIsRefreshBeforeUpdate" runat="server" Text="修改操作后是否刷新网格："></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_IsRefreshBeforeUpdate" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr style="height: 21px;">
                <td style="font-weight: <% =TableFont[3]%>; font-size: <%=TableFont[2] %>pt; color: <% =TableFont[1]%>; font-family: <%=TableFont[0] %>; ">
                    <asp:Label ID="labIsRefreshBeforeDelete" runat="server" Text="删除操作后是否刷新网格：" ></asp:Label></td>
                <td colspan="1" style="width: 510px">
                        <asp:DropDownList ID="ddlGrid_IsRefreshBeforeDelete" runat="server" Width="80px">
                        </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2" style="">
                    &nbsp;<asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonCss" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="退出" CssClass="ButtonCss" OnClientClick="return Exit();" OnClick="btnClear_Click" />
                    <asp:Button ID="btnReSet" runat="server" Text="恢复初值" CssClass="ButtonCss" OnClick="btnReSet_Click"/></td>
            </tr>
        </table>
    </form>
</body>
</html>
<script>
  function Exit()
  {
     window.parent.location="../../../MainPage.aspx";
     return false;
  }
</script>