<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManage.aspx.cs" Inherits="Code_SysInfomation_RoleManage_RoleManage" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="../../../css/css.css" type="text/css" rel="Stylesheet" />
</head>
<body style="margin:10 0 0 0;">
    <form id="form1" runat="server">
    <div>
      <table cellpadding="0" cellspacing="0" >
        <tr>
          <td style=" vertical-align:top; width: 300px; padding:0pt 5pt 5pt 5pt;"><!--GroupList-->
              <table id="tblGroupList" cellpadding="0" cellspacing="0" style=" width:298px; height:220px;">
                <tr>
                 <td style=" vertical-align:top;width: 100%;">
                     <fieldset style="width:298px; height:220px">
                       <asp:GridView ID="gvGroupList" runat="server" AllowPaging="True" AutoGenerateColumns="False" Font-Size="10pt" OnPageIndexChanging="gvGroupList_PageIndexChanging" OnRowDataBound="gvGroupList_RowDataBound" PageSize="5">
                         <Columns>
                             <asp:BoundField DataField="GroupID" HeaderText="ID">
                                 <HeaderStyle Width="0px" />
                             </asp:BoundField>
                             <asp:BoundField DataField="GroupName" HeaderText="用户组名称">
                                 <ItemStyle Width="85px" />
                             </asp:BoundField>
                             <asp:CommandField CancelText="" DeleteText="" EditText="" HeaderText="操作" InsertText=""
                                 NewText="" SelectText="" ShowSelectButton="True" UpdateText="">
                                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px" />
                             </asp:CommandField>
                         </Columns>
                         <RowStyle Height="23px" />
                         <HeaderStyle BackColor="WhiteSmoke" Height="23px" />
                           <PagerSettings Mode="NextPreviousFirstLast" />
                     </asp:GridView>
                     </fieldset>
                  </td>
                </tr>
              </table>
              <table style="height:8px;">
                <tr><td></td></tr>
              </table>
              <table id="tblGroupUserList"  cellpadding="0" cellspacing="0" style=" width:100%;"> <!--Users in Group-->
                <tr>
                  <td style="">
                   <fieldset style="height:263px; width:298px;">
                      <iframe  frameborder="0" style=" background-color:Transparent; width:98%; height:263px; border:solid,0pt,gray;" id="iframeGroupUserList" src="GroupUserList.aspx"></iframe>
                   </fieldset>
                  </td>
                </tr>
              </table>     
          </td>
          
          <td style=" vertical-align:top; width: 100%;"><!--RoleSet-->
            <table cellpadding="0" cellspacing="0" width=100%>
               <tr>
                 <td style="vertical-align:top; height: 492px;">
                   <fieldset style=" width:98%; height:492px; padding:0 0 0 0;">
                     <iframe id="iframeRoleSet" src="RoleSet.aspx" height="492px" style="width:100%;" frameborder="0" scrolling="no"></iframe>
                   </fieldset>
                 </td>
               </tr>
            </table>
          </td>
        </tr>
      </table>  
    </div>
    <div id="divHiden" style="display:none;">
    </div>
    </form>
  <script language="javascript" type="text/javascript">
     function RoleSet(GroupID,GroupName)
     {
         var date=new Date();
         var t=date.getMinutes()+date.getSeconds()+date.getMilliseconds();
         var iframeRoleSet=document.getElementById("iframeRoleSet");
         iframeRoleSet.src="RoleSet.aspx?GroupID="+GroupID+"&GroupName="+GroupName+"&time="+t;
     }
     
     function UserSet(GroupID,GroupName)
     { 
         var now=new Date();
         var temp=now.getMilliseconds();
         window.showModalDialog ('GroupUserManage.aspx?GroupID='+GroupID+'&GroupName='+GroupName+'&temp='+temp,temp,'top=0;left=0;toolbar=no;menubar=yes;scrollbars=no;resizable=yes;location=no;status=no;dialogWidth=300px;dialogHeight=370px');
     }
     
     function ShowGroupUserList(GroupID,GroupName)
     {
          var d=new Date();
          var t=d.getMinutes()+d.getMilliseconds();
          document.getElementById("iframeGroupUserList").src="GroupUserList.aspx?GroupName="+GroupName+"&GroupID="+GroupID+"&t="+t;
          RoleSet(GroupID,GroupName);
     }
  </script>  
</body>
</html>
