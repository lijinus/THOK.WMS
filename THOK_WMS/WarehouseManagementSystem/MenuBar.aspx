<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuBar.aspx.cs" Inherits="Navigation" %>
<%@ Register TagPrefix="THOKUC" TagName="LeftMenu" Src="~/WebUserControl/THOKMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="css/leftmenu.css?t=009" type="text/css" rel="Stylesheet"/>
    <script type="text/javascript" src="JQuery/jquery-1.4.1.min.js"></script>
    
<style>
.sub{}
.main{}
</style>
<script type="text/javascript">
// 
$(document).ready(function(){
   $(".main").toggle(function(){
     $(this).next(".sub").animate({height: 'toggle', opacity: 'toggle'}, "slow");
   },function(){
		 $(this).next(".sub").animate({height: 'toggle', opacity: 'toggle'}, "slow");
   });
});
</script>
</head>
<body style=" margin:0 0 0 0;" onload="startclock();" bgcolor="#F8FCFF">
    <form id="form1" runat="server">
    <div style="background-image:url(images/leftmenu/left-bg.jpg);">
    <THOKUC:LeftMenu runat="server" ID="leftmenu" />
    </div>
    </form>
    <script type="text/javascript">
        function Logout()
        {
           window.opener=null;
           window.parent.close();
           window.open('LoginPage.aspx?Logout=true','_Parent','height=690, width=1014, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=yes, resizable=yes,location=yes, status=yes');
        }
        
        function Exit()
        {
           window.opener=null;
           window.parent.open('ResetPage.aspx','_self');
        }
    </script>
</body>
</html>
