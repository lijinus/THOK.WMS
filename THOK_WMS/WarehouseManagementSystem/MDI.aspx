<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MDI.aspx.cs" Inherits="Admin_MDI" %>
<html xmlns="http://www.w3.org/1999/xhtml" onunload="GoReset();">
<head id="Head1" runat="server">
    <title>仓储管理系统</title>
<script type="text/javascript" language="javascript">
   window.moveTo(0,0);
   window.resizeTo(screen.availWidth,screen.availHeight);
    function GotoReset()
    {
       window.open("ResetPage.aspx",'_self');
    }
    function avoid(obj)
    {
       alert(obj.src);
    }
</script>
</head>
<frameset onunload="GotoReset();" rows="71,30,*" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="TopTitlePage.aspx" name="topFrame" scrolling="No" noresize="noresize" frameborder="0" id="topFrame" title="topFrame" />
  <frame id="Navigation" name="Navigation" frameborder=0 scrolling=no noresize=noresize src="Navigation.aspx" />
  <frameset id="main" rows="*" cols="204,100%" framespacing="0" frameborder="no" border="0">
    <frame src=MenuBar.aspx name="leftFrame" scrolling="auto"  noresize="noresize" frameborder="0" id="leftFrame" title="leftFrame"/>
    <frame name="mainFrame" id="mainFrame" frameborder=0 scrolling=yes title="" src="MainPage.aspx"/>
  </frameset>
</frameset>
</html>
