<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionTimeOut.aspx.cs" Inherits="SessionTimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
    alert('对不起,操作时限已过,请重新登入！');
    window.parent.opener=null;
    window.parent.close();
    window.open('LoginPage.aspx','_Parent','height=700, width=1100, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=no, resizable=yes,location=yes, status=yes');
//    window.parent.opener=null;
//    window.parent.close();
//    window.open('LoginPage.aspx','_Parent','height=700, width=1100, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=no, resizable=yes,location=yes, status=yes');
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
