<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupUserList.aspx.cs" Inherits="Code_SysInfomation_RoleManage_GroupUserList" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../../css/css.css" type="text/css" rel="Stylesheet" />
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
</style>    
</head>
<body style="margin:0 0 0 0;">
    <form id="form1" runat="server">
    <div>
      <table>
        <tr>
          <td style="height: 13px"><asp:Label ID="Label1" runat="server" Text="用户组成员" Height="21px" Width="167px" Font-Bold="True" Font-Size="10pt"></asp:Label></td>
        </tr>
        <tr>
          <td>
              <asp:DataGrid ID="dgGroupUser" runat="server" Font-Size="10pt"  AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CellPadding="4" OnDeleteCommand="dgGroupUser_DeleteCommand" AllowPaging="True" OnPageIndexChanged="dgGroupUser_PageIndexChanged" OnItemDataBound="dgGroupUser_ItemDataBound" PageSize="8">
                  <Columns>
                      <asp:BoundColumn DataField="UserID" HeaderText="ID"></asp:BoundColumn>
                      <asp:BoundColumn DataField="UserName" HeaderText="用户名">
                          <HeaderStyle Width="85px" />
                      </asp:BoundColumn>
                      <asp:BoundColumn DataField="GroupName" HeaderText="用户组">
                          <HeaderStyle Width="85px" />
                      </asp:BoundColumn>
                      <asp:ButtonColumn CommandName="Delete" HeaderText="操作" Text="删除">
                          <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                              Font-Underline="False" ForeColor="#000066" HorizontalAlign="Center" VerticalAlign="Middle" />
                          <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                              Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="35pt" />
                      </asp:ButtonColumn>
                  </Columns>
                  <HeaderStyle Height="25px" BackColor="WhiteSmoke" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                  <PagerStyle Mode="NumericPages" />

              </asp:DataGrid>
          </td>
        </tr>
      </table>           
    </div>
    </form>
</body>
</html>
