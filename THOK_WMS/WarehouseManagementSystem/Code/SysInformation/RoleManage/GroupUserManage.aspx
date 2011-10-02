<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupUserManage.aspx.cs" Inherits="Code_SysInfomation_RoleManage_GroupUserManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <base target="_self" />
    <script type="text/javascript">
    function okFunc(url)
    {
//      var topFrame =parent.frames['mainFrame'];
//      if(topFrame!=null)
//         parent.frames['mainFrame'].location=url;
//      else location.href=url;
    }
    </script>
</head>
<body style=" margin:15px 5px 5px 5px;">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Height="24px" Text="Label"
            Width="192px" Font-Size="10pt"></asp:Label>
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
            Text="保 存" />
        </div>
    <div>
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="270px" Width="280px">
         <asp:DataGrid ID="dgUser" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="5" Font-Size="10pt" OnItemDataBound="dgUser_ItemDataBound">
            <Columns>
                <asp:BoundColumn DataField="UserName" HeaderText="用户名"></asp:BoundColumn>
                <asp:BoundColumn DataField="GroupName" HeaderText="所属用户组"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" HeaderText="用户组设置">
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="UserID"></asp:BoundColumn>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" Mode="NumericPages" />
            <ItemStyle ForeColor="#000066" />
            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" Wrap="False" />
        </asp:DataGrid>
        </asp:Panel>
    </div>
    </form>
</body>
</html>