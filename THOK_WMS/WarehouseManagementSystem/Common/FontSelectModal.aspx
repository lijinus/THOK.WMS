<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FontSelectModal.aspx.cs" Inherits="FontSelectModal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>字体设置</title>
    <link rel=stylesheet href="../css/css.css" type="text/css">
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <table width="600" border="0" cellpadding="0" cellspacing="0">
          <!--DWLayoutTable-->
          <tr>
            <td width="100" height="30" valign="top" style="vertical-align: middle; text-align: center"><!--DWLayoutEmptyCell-->
                <span style="font-size: 10pt">字体:</span>&nbsp;</td>
            <td colspan="2" valign="top" style="vertical-align: middle; text-align: left"><!--DWLayoutEmptyCell--><asp:DropDownList ID="ddlFont" runat="server">
                </asp:DropDownList></td>
            <td width="100" valign="top" style="vertical-align: middle; text-align: center"><!--DWLayoutEmptyCell-->
                <span style="font-size: 10pt">字体颜色:</span>&nbsp;</td>
            <td width="200" valign="top" style="vertical-align: middle; text-align: left"><!--DWLayoutEmptyCell--><asp:DropDownList ID="ddlFontColor" runat="server">
                </asp:DropDownList></td>
          </tr>
          <tr>
            <td height="30" valign="top" style="vertical-align: middle; text-align: center"><!--DWLayoutEmptyCell-->&nbsp;<span style="font-size: 10pt">字体大小:</span></td>
            <td colspan="2" valign="top" style="vertical-align: middle; text-align: left"><!--DWLayoutEmptyCell--><asp:DropDownList ID="ddlFontSize" runat="server">
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>32</asp:ListItem>
                <asp:ListItem>36</asp:ListItem>
                <asp:ListItem>48</asp:ListItem>
                <asp:ListItem>72</asp:ListItem>
                </asp:DropDownList></td>
            <td style="vertical-align: middle; text-align: center">
                <span style="font-size: 10pt">是否加粗:</span>&nbsp;</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlFontIncrease" runat="server">
                    <asp:ListItem Value="加粗">加粗</asp:ListItem>
                    <asp:ListItem Value="正常">正常</asp:ListItem>
                </asp:DropDownList>&nbsp;</td>
          </tr>
          <tr>
            <td height="30" colspan="2" valign="top"><!--DWLayoutEmptyCell-->&nbsp;</td>
            <td colspan="2" valign="top" style="vertical-align: middle"><!--DWLayoutEmptyCell-->&nbsp;<asp:Button ID="btnFontOK" runat="server" OnClick="btnFontOK_Click" Text="确定" CssClass="ButtonCss" />&nbsp;
                <asp:Button ID="btnFontRetrue" runat="server" OnClick="btnFontRetrue_Click" Text="返回" CssClass="ButtonCss" /></td>
            <td valign="top"><!--DWLayoutEmptyCell-->&nbsp;</td>
          </tr>
        </table>
    </form>
    
    <script language="javascript" type="text/javascript">
       var obj = window.dialogArguments;
       var ary=obj.split(",");
       var objSelFont=document.getElementById("ddlFont");
       for(var i=0;i<objSelFont.options.length;i++)
       {
           if(objSelFont.options(i).value==ary[0])
           {
              objSelFont.options.selectedIndex=i;
           }
       }
       
       var objSelFontColor=document.getElementById("ddlFontColor");
       for(var i=0;i<objSelFontColor.options.length;i++)
       {
           if(objSelFontColor.options(i).value==ary[1])
           {
              objSelFontColor.options.selectedIndex=i;
           }
       }

       var objSelFontSize=document.getElementById("ddlFontSize");
       for(var i=0;i<objSelFontSize.options.length;i++)
       {
           if(objSelFontSize.options(i).value==ary[2])
           {
              objSelFontSize.options.selectedIndex=i;
           }
       }
       
       var objSelFontIncrease=document.getElementById("ddlFontIncrease");
       for(var i=0;i<objSelFontIncrease.options.length;i++)
       {
           if(objSelFontIncrease.options(i).value==ary[3])
           {
              objSelFontIncrease.options.selectedIndex=i;
           }
       }       
    </script>
</body>
</html>
