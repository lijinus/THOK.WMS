<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Navigation.aspx.cs" Inherits="Admin_Navigation" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
</head>
<body style="height:30px; margin:0 0 0 0;">
    <form id="form1" runat="server" >
    <table width="100%"  border="0" cellpadding="0" cellspacing="0" height="30px" style="">
      <!--DWLayoutTable-->
      <tr>
        <td style="background-image:url(images/leftmenu/nav_left.gif); width:204px;">
            <img id="IMG1" alt="隐藏导航栏" onclick="return adjust()" src="images/leftmenu/open.gif" border="0" name="IMG1" align="absmiddle"/>
            <span id="span1" style="font-size:12px; color:DarkSlateGray; cursor:pointer;" onclick="adjust()">隐藏导航栏</span>
            <img src="images/leftmenu/logout.gif" border="0" align="absmiddle" />
            <span onclick="Logout()" style="font-size:12px; color:DarkSlateGray; cursor:pointer;" >注销</span>
        </td>
        <td valign="top" style="height:30px; padding-left:8px; padding-top:8px; background-image:url(images/leftmenu/nav_mid.gif); "><!--DWLayoutEmptyCell-->
          <asp:Label ID="labNavigation" runat="server" Text="快速通道:" Width="493px" Font-Size="10pt" ForeColor="Black" Font-Bold="False" Height="27px"></asp:Label>
        </td>
      </tr>
    </table>
    </form>
    <script>
  function   adjust(){   
      if(window.parent.main.cols=="0,100%"){   
      document.getElementById('IMG1').src="images/leftmenu/open.gif";   
      window.parent.main.cols="204,100%";   
      document.getElementById('IMG1').alt="隐藏导航栏" ; 
      document.getElementById('span1').innerText='隐藏导航栏';  
      }   
      else{   
      document.getElementById('IMG1').src="images/leftmenu/close.gif";   
      window.parent.main.cols="0,100%";   
      document.getElementById('IMG1').alt="显示导航栏"; 
      document.getElementById('span1').innerText='显示导航栏';    
      }   
  }
  
function Logout()
{
   window.opener=null;
   window.parent.close();
   window.open('LoginPage.aspx?Logout=true','_Parent','height=690, width=1014, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=yes, resizable=yes,location=yes, status=yes');
}
    </script>
</body>
</html>