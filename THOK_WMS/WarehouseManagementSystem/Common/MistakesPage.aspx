<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MistakesPage.aspx.cs" Inherits="MistakesPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
    <!--
    .STYLE1 {color: #FFFFFF}
    body,td,th {
	    font-size: 13px;
	    color: #FFFFFF;
    }
    -->
    </style>
</head>
<body style="text-align: center; vertical-align: middle;">
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <br />
    <table width="515" height="371" border="0" align="center" cellpadding="0" cellspacing="0" id="__01">

      <tr>
        <td rowspan="4" style="width: 6px">&nbsp;</td>
        <td valign="top" background="../images/mistakes_bg1.jpg" style="height: 324px; width: 395px;"><table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td style="height: 55px; width: 122px;">&nbsp;</td>
            <td style="height: 55px;">&nbsp;</td>
            </tr>
          <tr>
            <td align="right" valign="top" style="vertical-align: middle; text-align: right; height: 35px; width: 122px;"><span class="STYLE1">模块：</span></td>
            <td style="vertical-align: middle; text-align: left; height: 35px;">
                <asp:Label ID="labModuleName" runat="server"></asp:Label></td>
            </tr>
          <tr>
            <td align="right" valign="top" style="vertical-align: middle; text-align: right; height: 35px; width: 122px;"><span class="STYLE1">方法：</span></td>
            <td style="vertical-align: middle; text-align: left; height: 35px;">
                <asp:Label ID="labFunctionName" runat="server"></asp:Label></td>
            </tr>
          <tr>
            <td align="right" valign="top" style="vertical-align: middle; text-align: right; height: 35px; width: 122px;"><span class="STYLE1">类型：</span></td>
            <td style="vertical-align: middle; text-align: left; height: 35px;">
                <asp:Label ID="labExceptionalType" runat="server"></asp:Label></td>
            </tr>
          <tr>
            <td height="30" align="right" valign="top" style="vertical-align: middle; text-align: right; width: 122px;"><span class="STYLE1">内容：</span></td>
            <td rowspan="2" valign="top" style="text-align: left; padding-top: 5px;">
                <asp:Label ID="labExceptionalDescription" runat="server"></asp:Label></td>
            </tr>
          <tr>
            <td style="height: 75px; width: 122px;">&nbsp;</td>
            </tr>
        </table></td>
        <td rowspan="3" valign="bottom" style="width: 114px"><img src="../images/fail.gif" alt="" width="100" height="104" border="0" usemap="#Map" />
          <map name="Map" id="Map">
            <area shape="rect" coords="11,15,101,92" href="#" onclick="javascript:history.back();"/>
          </map></td>
      </tr>
      <tr>
        <td style="height: 47px; width: 395px;">&nbsp;</td>
      </tr>
    </table>
    </form>
</body>
</html>
