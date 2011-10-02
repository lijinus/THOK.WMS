<%@ Control Language="C#" AutoEventWireup="true" CodeFile="THOKTreeMenu.ascx.cs" Inherits="Admin_WebUserControl_THOKTreeMenu" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" style="overflow-y:auto;">
  <tr  class="left_bg" style="position:relative; top:expression(this.offsetParent.scrollTop);z-index:999;">
    <td height="20" class="left_link"><asp:LinkButton ID="lnkExpand" runat="server" OnClick="lnkExpand_Click">展开</asp:LinkButton> | 
        <asp:LinkButton ID="lnkCollapse" runat="server" OnClick="lnkCollapse_Click">收缩</asp:LinkButton> | <A href="MenuBar.aspx" class="webfiletopfont">刷新</A><br>欢迎您：<%=Session["G_User"]%></td>
  </tr>
  <tr>
    <td  style="vertical-align:top;">
        <yyc:smarttreeview id="sTreeMenu" runat="server" showlines="True" Font-Size="12px" ForeColor="black">
           <LeafNodeStyle ForeColor="MidnightBlue" />
            <Nodes>
                <asp:TreeNode Expanded="True" ImageUrl="~/images/leftmenu/earth.gif" SelectAction="None"
                    Text="管理系统" Value="管理系统"></asp:TreeNode>
            </Nodes>
            <HoverNodeStyle ForeColor="#FF8000" />
            <LeafNodeStyle ForeColor="#00486A" />
        </yyc:smarttreeview>    
    </td>
  </tr>
</table>