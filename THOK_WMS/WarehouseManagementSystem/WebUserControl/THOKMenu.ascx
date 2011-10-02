<%@ Control Language="C#" AutoEventWireup="true" CodeFile="THOKMenu.ascx.cs" Inherits="Webparts_THOKMenu" %>

<asp:Panel ID="plMenu" runat="server">
</asp:Panel>

<script type="text/javascript" language="javascript">
   function Display(obj)
   {
         var menu=document.getElementById('leftmenu_div'+obj);
         var title=document.getElementById('leftmenu_table'+obj);
       if(menu.style.display=="block")
       {
          menu.style.display="none";
          title.style.backgroundImage='url(images/leftmenu/button.jpg)';
       }
       else
       {
          menu.style.display="block";
          title.style.backgroundImage='url(images/leftmenu/button2.jpg)';
       }
       
      for(var i=0;i<10;i++)
      {
         if(i!=obj)
         {
             
             var menu2=document.getElementById('leftmenu_div'+i);
             if(menu2==null)
             {
                 break;
             }
             else
             {
                  menu2.style.display="none";
                  document.getElementById('leftmenu_table'+i).style.backgroundImage='url(images/leftmenu/button.jpg)';
             }
          }
      }                   
   }
</script>