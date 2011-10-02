<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyPwdSuccess.aspx.cs" Inherits="Code_SysInfomation_PwdModify_ModifyPwdSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>密码修改成功</title> 
    <link rel="stylesheet" href="../../../css/css.css" type="text/css" />
   <script type="text/javascript">
    var iTimeLeave =5;
    function getLeaveTime()
    {
      if(iTimeLeave > 0)
       {
          document.getElementById("txtTimeLeave").value = iTimeLeave;
          iTimeLeave = iTimeLeave - 1; 
          setTimeout("getLeaveTime()",1000);
       }
       else
       {         
          location.href="../../../MainPage.aspx";      
       }
    }
    function click_back()
    {
         location.href="../../../MainPage.aspx"; 
         return false;
    }
    function click_reset()
    {
         location.href="PwdModify.aspx"; 
         return false;
    }
    </script>
    
</head>
<body onload="getLeaveTime()" style="width: 100%; height: 100%; background-color: transparent;">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table  style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; background-color: <%=getColorValue(Session["grid_EvenRowColor"].ToString())%>;"  cellpadding="0" cellspacing="0">
            <tr >
                <td style="border-top-width: 1px; border-left-width: 1px; border-bottom-width: 1px; width: 426px; height: 17px; border-right-width: 1px" >
                    <br />
                    <br />
                    密码修改成功,<input id="txtTimeLeave" 
                        type="text" value="10" style="width: 15px; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; background-color: transparent;" />s后自动返回<br />
                    <br />
                </td>
            </tr>
           <tr style="" >
           <td style="HEIGHT: 17px; border-top-width: 1px; border-left-width: 1px; width: 426px; border-right-width: 1px; border-bottom-width: 1px;">
               <asp:LinkButton ID="lbnBack" OnClientClick="javascript:return click_back()" runat="server">返回</asp:LinkButton>
               &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
               <asp:LinkButton ID="lbnReset" OnClientClick="javascript:return click_reset()" runat="server">重新设置</asp:LinkButton></td>
           </tr>
        </table>
       </div>
    </form>
</body>
</html>
