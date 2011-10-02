<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PwdModify.aspx.cs" Inherits="Code_SysInfomation_PwdModify_PwdModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>密码修改界面</title>
    <link rel="stylesheet" href="../../../css/css.css" type="text/css" />
    <style>
       .title
       {
          text-align:right;
          width:120px;
       }
       .tableRow
       {
          
       }
    </style>
    <script type="text/javascript">
    function AckPassword()
    {
       var txtNewPwd = document.getElementById("txtNewPwd").value;
       var txtAckPwd = document.getElementById("txtAckPwd").value;

       if( document.getElementById("txtNewPwd").value==document.getElementById("txtAckPwd").value) // txtNewP==txtAckPwd)
       {
          return true;      
       }
       else 
       {
         document.getElementById("labMessage").innerText = "密码不一致";
         return false;
       }      
    }
    function CancelModify()
    {
       document.getElementById("txtNewPwd").value = "";
       document.getElementById("txtAckPwd").value = "";
       document.getElementById("txtOldPwd").value = "";
       return false;
    }
    
    function Exit()
    {
       location="../../../MainPage.aspx";
       return false;
    }
    </script>
</head>
<body style="text-align: left; width: 100%; height: 100%; background-color: transparent;">
    <form id="form1" runat="server" defaultfocus="txtOldPwd">  
        <div style="height:120px;"></div>
        <table id="ModifyPwd" border="0" cellpadding="3" cellspacing="0"  align="center">
            <tr>
              <td colspan="2"  style=" text-align:center; color:#1a70ad;height:55px; font-size:13pt;">
                  <span style=""> <strong>::: 用户密码修改 :::</strong></span>
              </td>
            </tr>
            <tr class="tableRow">
                <td class="title" >
                    用 户 名:</td>
                <td style="width: 260px;">
                <asp:TextBox ID="txtUserName" runat="server" ReadOnly="True" CssClass="DisabledTextBox" Height="20"></asp:TextBox></td>
            </tr>
            <tr class="tableRow">
                <td class="title"> 旧 密 码:</td>
                <td>
                   <asp:TextBox ID="txtOldPwd" runat="server"  TextMode="Password" CssClass="TextBox" Height="20"></asp:TextBox></td>
            </tr>
            <tr class="tableRow">
                 <td class="title">
                     新 密 码:</td>
                 <td>
                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"  CssClass="TextBox" Height="20"></asp:TextBox></td>
            </tr>
             <tr class="tableRow">
                <td class="title"> 确认密码:</td>
                <td>
                   <asp:TextBox ID="txtAckPwd" runat="server" TextMode="Password" CssClass="TextBox" Height="20"></asp:TextBox></td>
            </tr>
            <tr class="tableRow">
                  <td colspan="2" style=" text-align :center; height: 38px; padding-top:2em;"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                         <asp:Button ID="Button1" runat="server" OnClientClick="return AckPassword();" OnClick="lbtnSave_Click"  CssClass="ButtonCss" Text=" 保 存 "/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="Button2" runat="server" OnClientClick="return Exit()"  OnClick="lbtnCancel_Click" CssClass="ButtonCss"  Text=" 取消"/><br />
                         <asp:Label ID="labMessage" runat="server" ForeColor="Red" Width="112px" ></asp:Label>  
                  </td> 
            </tr>

        </table>
    </form>
</body>
</html>

