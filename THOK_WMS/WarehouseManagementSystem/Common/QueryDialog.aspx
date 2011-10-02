<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryDialog.aspx.cs" Inherits="Common_QueryDialog" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>高级查询对话框&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
    <base target="_self" />
    <link href="../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../JScript/Calendar.js"></script>
    <script src="../JScript/SelectDialog.js?time=098"></script>
</head>
<body style="margin:0 0 0 0px;">
    <form id="form1" runat="server">
    <div style="background-image:url(../images/main-02.gif); height:31px;"></div>
    <asp:Table ID="tblQuery" runat="server" Width="100%" CellPadding="0" CellSpacing="0">
    </asp:Table>
    <div style="text-align:center; padding-top:10px;">
        <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click" CssClass="ButtonCss" />
        &nbsp; &nbsp;<asp:Button ID="btnClose" runat="server" Text="关闭" OnClick="btnClose_Click" CssClass="ButtonCss"/>
    </div>
  <div>
      <asp:HiddenField ID="hideTableName" runat="server" />
  </div> 
<script>
function ReturnValue(fieldValue){
   if (window.document.all)//IE判断window.showModalDialog!=null
   {
     window.returnValue=fieldValue;
     window.opener=null;
     window.close();

   }
   else
   {
      alert('请使用IE');
//    returnValue=fieldValue;
//    window.close();
//       var aryValue=new Array();
//       aryValue=fieldValue.split('|');
//       var aryTarget=new Array();
//       aryTarget=document.getElementById('hideTargetControls').value.split(',');
//       for(var i=0;i<aryTarget.length;i++)
//       {
//          var e=parent.opener.document.getElementById(aryTarget[i]);
//          if(e!=null)
//          {
//            e.value=aryValue[i];
//          }
//       }  
//       window.close();      
   }
}
function window_onload()
{
    if(window.document.all)//IE
    {
    //对于IE直接读数据
    //        var txtInput=window.document.getElementById("txtInput");
    //        txtInput.value=window.dialogArguments;
    }
    else//FireFox
    {
    //获取参数
    //window.dialogArguments=window.opener.myArguments;
    //var txtInput=window.document.getElementById("txtInput");
    //txtInput.value=window.dialogArguments;
    }
}

function window_onunload() {
//对于Firefox需要进行返回值的额外逻辑处理
    if(!window.document.all)//FireFox
    {
       window.opener.myAction.returnAction(window.returnValue)
    }
}

</script>   
    </form>
</body>
</html>
