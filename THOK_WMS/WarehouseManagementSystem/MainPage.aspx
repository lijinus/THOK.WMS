<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"> 
<style type="text/css">
</style>
    <title>主页</title>
    <link href="Css/css.css?t=88" type="text/css" rel="stylesheet" />
    <style type="text/css">
      .ButtonMessage
      {
        font-family: "Tahoma", "宋体"; 
        font-size:9pt;  
    /*  border: 0px #ff0000 solid; 
        background-color:Transparent;
         BORDER-BOTTOM: #ffffff 0px solid;  
        BORDER-LEFT: #93bee2 0px solid;  
        BORDER-RIGHT: #93bee2 0px solid;  
        BORDER-TOP: #93bee2 0px solid; */
        background-image:url(images/op/light.gif); 
        background-repeat:no-repeat;
        CURSOR: hand; 
        padding-left:13px;
        font-style: normal ; 
        height:24px;
      }
    </style>
<script type="text/javascript" language="javascript">
  function SetNewColor(source)
  {
            _oldColor=source.style.backgroundColor;
            source.style.backgroundColor='#C0E4EE';
          
  }
  function SetOldColor(source)
  {
         source.style.backgroundColor=_oldColor;
  }
</script>

<script language="JavaScript" type="text/javascript"><!--//建立一个弹出窗口var oPopup = window.createPopup();//得到这个弹出窗口的bodyvar oPopupBody = oPopup.document.body;//开始显示的坐标（默认是最右下脚）flyMove.expand = 0;flyMove.flyY = 10;flyMove.flyX = 10;//渐进显示的定时器var g_idFlyPopup = -1;//显示弹出窗口的定时器var TimeoutFlag=-1;//显示弹出窗口的方法function richDialog(){    //在弹出窗口中写入文字和数据//    oPopup.document.body.innerHTML = oDialog.innerHTML; //    oPopupBody.style.fontSize = document.body.currentStyle.fontSize;////    oPopupBody.style.backgroundImage='url(bg.jpg)';//    oPopupBody.style.cursor="pointer";//    oPopupBody.style.color = "infotext";//    oPopupBody.style.borderWidth='1px';//    oPopupBody.style.borderStyle='window-inset';    //oPopupBody.style.borderColor='activeborder';    //下面代码会立即显示弹出窗口    //oPopup.show(100, 50, 400, 300);    oPopup = oDialog;     flyInit();    g_idFlyPopup = window.setInterval(flyMove,10);}function flyMove(){    flyMove.expand += 2;    flyMove.flyY -= 2;    oPopup.show(flyMove.flyX-flyMove.expand, flyMove.flyY, 200, flyMove.expand);    var oPopupBody = oPopup.document.body;    if (oPopupBody.clientWidth >= oPopupBody.scrollWidth || oPopupBody.clientHeight >= oPopupBody.scrollHeight)    {        //清除渐进显示的定时器        window.clearInterval(g_idFlyPopup);        g_idFlyPopup = -1;        //清除调用弹出窗口的定时器        window.clearTimeout(TimeoutFlag);        TimeoutFlag=-1;        //注册6秒后关闭弹出窗口的定时器         window.setTimeout( 'closePopup()', 10000);    }}//关闭弹出窗口function closePopup(){    if( null != oPopup )    {        //oPopup.hide();        flyMove.expand = 0;        flyMove.flyY = window.screen.height;        flyMove.flyX = window.screen.width;    }}//初始化弹出窗口的坐标，将其定位到最右下角function flyInit(){    flyMove.expand = 0;    flyMove.flyY = window.screen.height;    flyMove.flyX = window.screen.width-10;    oPopupBody = oPopup.document.body;}//设定1秒后调用richDialog方法（用于显示弹出窗口）//--></script>

</head>
<body bgcolor="#F8FCFF" style="margin-top:30px;">
    <form id="form1" runat="server">
    </form>
        <DIV ID="oDialog"  style="position:absolute; bottom:0; right:1px;display:none;">
            <table style="width:202px;" cellpadding="0" cellspacing="0">                <tr style="background-image:url(images/desk/msg_bg.gif); height:29px;">                  <td style="font-size:12px; font-weight:bolder; color:#054371;  " >                    <table style="width:100%">                       <tr>                          <td>&nbsp; 系统预警</td>                          <td width="15"><span style="color: #ff3366; cursor:pointer;" onclick=" HideMessage()">[X]</span></td>                       </tr>                    </table>                  </td>                </tr>                <tr>                  <td valign="middle" align="center"  style="border:solid 1px #a1bae6; padding:5 5 5 5;">                       <asp:Table ID="tblRemind" runat="server" style="" CellSpacing="5" Font-Size="12px"></asp:Table>                  </td>                                 </tr>                                          </table>        </div>
        <asp:Panel ID="pnlRemind" runat="server" style="position:absolute; right:0; bottom:0;" Visible="false">
           <span style="font-size:13px; cursor:pointer;  width:68px; height:23px; padding:5 3 3 28; background-image:url(images/desk/msg_button.gif);" onclick="ShowMessage()">
             消息</span>
        </asp:Panel>
</body>
</html>
<script>
   if(document.getElementById('pnlRemind')!=null)
   {
//      ShowMessage();
//      window.setInterval(ShowMessage(),10)
     document.getElementById('pnlRemind').style.display='none';
     showMsg('oDialog');
   }
   
function ShowMessage(){//   //TimeoutFlag=window.setTimeout( richDialog, 1000 );//   document.getElementById('oDialog').style.display='block';//   document.getElementById('pnlRemind').style.display='none';//   window.setTimeout( 'HideMessage()', 10000);        document.getElementById('pnlRemind').style.display='none';
     showMsg('oDialog');}function HideMessage(){   document.getElementById('oDialog').style.display='none';   document.getElementById('pnlRemind').style.display='block';}


//设置透明度
function setOpacity(obj, value){
    if(document.all){
        if(value == 100){
            obj.style.filter = "";
        }else{
           //alert(value);
            obj.style.filter = "alpha(opacity=" + value + ")";    
        }
    }else{
        obj.style.opacity =value / 100 ;
       
    }
}
//用setTimeout循环减少透明度
function changeOpacity(obj, startValue, endValue, step, speed){
    if(startValue >= endValue){
         //document.body.removeChild(obj);
         document.getElementById('oDialog').style.display='block';
        return;
    }
    if(!obj)
    {
      return;
    }
    if(startValue>=100)
    {
      //document.body.removeChild(obj);
      document.getElementById('oDialog').style.display='block';
      return;
    }
   // alert(startValue);
    setOpacity(obj, startValue);
    setTimeout(function(){changeOpacity(obj, startValue+step, endValue, step, speed);}, speed);
}
//设置隐藏速度和id
function showMsg(id){
    var msg =document.getElementById(id);
    var step = 5, speed = 80;
//    if(msg.style.display=="none")
//    {
//      msg.style.display="";
//    }
    msg.style.display='block';
    changeOpacity(msg, 0, 100, step, speed);
}


</script>