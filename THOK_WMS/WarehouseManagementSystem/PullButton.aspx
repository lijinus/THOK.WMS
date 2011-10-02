<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PullButton.aspx.cs" Inherits="PullButton" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
  <meta   content="Microsoft   Visual   Studio   .NET   7.0"   name="GENERATOR"/>   
  <meta   content="Visual   Basic   7.0"   name="CODE_LANGUAGE"/>   
  <meta   content="JavaScript"   name="vs_defaultClientScript"/>   
  <meta   content="http://schemas.microsoft.com/intellisense/ie5"   name="vs_targetSchema"/>   
  <script type="text/javascript"   language="javascript"   id="clientEventHandlersJS">   
  <!--
  function   adjust2()   {   
      if(window.parent.main.cols=="0,8,100%"){   
      window.IMG1.src="images/leftmenu/left.jpg";   
      window.parent.main.cols="183,8,100%";   
      window.IMG1.alt="隐藏导航栏" ;   
      }   
      else{   
      window.IMG1.src="images/leftmenu/right.jpg";   
      window.parent.main.cols="0,8,100%";   
      window.IMG1.alt="显示导航栏";   
      }   
  }    
  function   adjust()  
  {   
    var Menu=window.parent.document.getElementById('Layer3');
    var Pull=window.parent.document.getElementById('Layer4');
    var Nav=window.parent.document.getElementById('Layer5');
    var Main = window.parent.document.getElementById('Layer6');
//    var QueryTemp=Main.document.getElementById('WQHWT');
    var QueryTemp=window.parent.frames['mainFrame'].document.getElementById('QueryTemplatePage');
    if(Menu.style.width!="0px")
       {
//            window.IMG1.src="images/leftmenu/right.jpg";   
            document.getElementById('IMG1').src='images/leftmenu/right.jpg';
            document.getElementById('IMG1').alt="显示导航栏" ;  
            Menu.style.width="0px";
            Pull.style.left="0px";//"width:10px; height:660px; top:100px; left:0px;";
            Nav.style.width="1008px";//"width:1014px; height:20px; top:100px; left:10px;";
            Nav.style.left="8px";
            Main.style.width="1008px";// height:640px; top:120px; left:10px;";
            Main.style.left="8px";
          
            if(QueryTemp!=null)
            {
            var Tablea=window.parent.frames['mainFrame'].document.getElementById('pnlTable');
            var Tableb=window.parent.frames['mainFrame'].document.getElementById('PlaceHolder1');
            Tablea.style.width="985px";            
            Tableb.style.width="985px";
            }
            
        }
    	else
		{
            document.getElementById('IMG1').src="images/leftmenu/left.jpg";   
            document.getElementById('IMG1').alt="隐藏导航栏" ;  
            Nav.style.width="836px";//"width:1014px; height:20px; top:100px; left:10px;";
            Nav.style.left="178px";
            Main.style.width="836px";// height:640px; top:120px; left:10px;";
            Main.style.left="178px";
            Menu.style.width="170px";
            Pull.style.left="170px";//"width:10px; height:660px; top:100px; left:0px;";
            if(QueryTemp!=null)
            {
            var Tablea=window.parent.frames['mainFrame'].document.getElementById('pnlTable');
            var Tableb=window.parent.frames['mainFrame'].document.getElementById('PlaceHolder1');            
            Tablea.style.width="815px";            
            Tableb.style.width="815px";         
            }
		}
  }   
    
  //-->   
  </script>   
  </head>  
  <body style="left:0px;top:0px; margin:0 0 0 0;">  
      <table height="100%" width="8px" border="0" cellpadding="0" cellspacing="0" style="background-image:url(images/leftmenu/shu_bg.jpg);">
          <!--DWLayoutTable-->
          <tr>
            <td height="35%" valign="top" style="width:8px"><img src="images/leftmenu/shu_top.jpg"/></td>
          </tr>
          <tr>
             <td nowrap style="width: 8px"><a href="#"><img id="IMG1" alt="隐藏导航栏" onclick="return adjust2()" src="images/leftmenu/left.jpg" border="0" name="IMG1"/></a></td>
          </tr>
          <tr>
            <td height="45%" valign="top" style="width: 8px"><!--DWLayoutEmptyCell-->&nbsp;</td>
          </tr>
      </table>
  </body> 
</html>

