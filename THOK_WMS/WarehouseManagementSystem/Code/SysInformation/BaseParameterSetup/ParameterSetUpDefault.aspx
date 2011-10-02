<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParameterSetUpDefault.aspx.cs" Inherits="Code_SysInformation_BaseParameterSetup_ParameterSetUpDefault" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" href="../../../css/css.css" type="text/css" />
    <style>
      .menuOn
      {
         height:24px;
         background-image:url(../../../images/4_08.jpg); 
         background-position-y:1px; 
         cursor:pointer; 
         background-repeat:no-repeat;
         border-bottom:solid 0px gray;
         text-align:center;
      }
      
      .menuOff
      {
         cursor:pointer; 
         background-repeat:no-repeat;
         border-bottom:solid 1px gray;
      }
    </style>
</head>
<body style="margin:0 0 0 0; overflow-y:hidden;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
          <ContentTemplate>       
    <div style="width:100%">
      <table style="width:100%;border-collapse:collapse;" cellpadding="0" cellspacing="0">
        <tr style="height:5px;"><td>&nbsp;</td></tr>
        <tr>
           <td>
             <table cellpadding="0" cellspacing="0" width="100%">
              <tr>
                  <td class="menuOff">&nbsp;</td>
                  <td id="td01" class="menuOn" onclick="OptionSwitch(1)" height="24px" width="112px;">格式设置</td>
                  <td width="80px" class="menuOff">&nbsp;</td>
                  <td id="td02" class="menuOff" onclick="OptionSwitch(2)"  height="24px" width="112px">用户参数设置</td>
                  <td width="80px" class="menuOff"">&nbsp;</td>
                  <td id="td03" class="menuOff" onclick="OptionSwitch(3)" height="24px" width="112px" >系统参数设置</td>
                  <td class="menuOff">&nbsp;</td>   
              </tr>          
             </table>       
           </td>
        </tr>
        <tr id="r01" style="display:block;">
          <td>
            <iframe id="frame" src="BusinessProcess.aspx" frameborder="0" width="100%" height="550px"></iframe>
          </td>
        </tr>
        <tr id="r02" style="display:none;">
          <td>
             <iframe id="Iframe1" src="SystemInf.aspx" frameborder="0" width="100%" height="550px"></iframe>
          </td>
        </tr>
        <tr id="r03" style="display:none;">
           <td>
             <iframe id="Iframe2" src="UserInfoParameterSetup.aspx" frameborder="0" width="100%" height="550px"></iframe>
           </td>
        </tr>
      </table>
    </div>
      </ContentTemplate>
   </asp:UpdatePanel>   
    
    </form>
</body>
</html>
<script>
if (window.navigator.userAgent.indexOf("Firefox")>=1)
{
   document.getElementById('frame').width=document.body.clientWidth;
   document.getElementById('Iframe1').width=document.body.clientWidth;
   document.getElementById('Iframe2').width=document.body.clientWidth;
}
  function OptionSwitch(index)
  {
     if(index==1)
     {
        document.getElementById('r01').style.display='block';
        document.getElementById('r02').style.display='none';
        document.getElementById('r03').style.display='none';
         document.getElementById('td01').className='menuOn';
         document.getElementById('td02').className='menuOff';
         document.getElementById('td03').className='menuOff';
     }
     else if(index==2)
     {
        document.getElementById('r01').style.display='none';
        document.getElementById('r02').style.display='block';
        document.getElementById('r03').style.display='none';

         document.getElementById('td02').className='menuOn';
         document.getElementById('td01').className='menuOff';
         document.getElementById('td03').className='menuOff';
     }
     else if(index==3)
     {
        document.getElementById('r01').style.display='none';
        document.getElementById('r02').style.display='none';
        document.getElementById('r03').style.display='block';     

         document.getElementById('td03').className='menuOn';
         document.getElementById('td02').className='menuOff';
         document.getElementById('td01').className='menuOff';
     }
  }
</script>
