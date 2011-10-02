<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleSet.aspx.cs" Inherits="Code_SysInfomation_RoleManage_RoleSet" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
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
<script type="text/javascript">
function Exit()
{
window.parent.location='../../../MainPage.aspx'
}
</script>
 <link href="../../../css/css.css" type="text/css" rel="Stylesheet" />
</head>
<body style=" margin:0px 0 0 0;" >
    <form id="form1" runat="server">
    <div style="padding-top:5px;">
        &nbsp;
        <asp:Label ID="lbTitle" runat="server" Font-Bold="True" Font-Size="10pt" Height="24px"
            Width="284px">用户组权限设置</asp:Label>&nbsp;
    </div>
        
    <div>
        <table>
         <tr style="font-size:10pt; font-weight:bold; color:Black;">
           <td>&nbsp;</td>
           <td>
               <asp:LinkButton ID="lnkBtnExpand" runat="server" OnClick="lnkBtnExpand_Click" BorderStyle="None" ForeColor="ActiveCaption" Width="45px">展开</asp:LinkButton></td>
           <td><asp:LinkButton ID="lnkBtnCollapse" runat="server" OnClick="lnkBtnCollapse_Click" ForeColor="ActiveCaption" Width="45px">折叠</asp:LinkButton></td>
         </tr>
        </table>
        <asp:Panel ID="plTree" runat="server" Height="443px" Width="100%" ScrollBars="Auto">
           <yyc:smarttreeview id="sTreeModule" runat="server" allowcascadecheckbox="True"
                showlines="True" Font-Size="10pt" ExpandDepth="1">
               <LeafNodeStyle ForeColor="MidnightBlue" />
           </yyc:smarttreeview>
       </asp:Panel>
    </div>
    <div id="divOp" style="position:absolute;top:5px; left:280px; display:block; width:100px;">
      <table>
       <tr style="font-size:10pt; font-weight:bold; color:Black;">
         <td style="width:55px; height: 17px;"><asp:LinkButton ID="lnkBtnSave" runat="server" OnClick="lnkBtnSave_Click" Visible="False" ForeColor="ActiveCaption">保存</asp:LinkButton></td>
         <td style="width:55px; height: 17px;"><asp:LinkButton ID="lnkBtnCancle" runat="server" ForeColor="ActiveCaption" OnClientClick="Exit();">退出</asp:LinkButton></td>
       </tr>
      </table>
    </div>
    </form>
</body>
</html>
