<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingGroupPage.aspx.cs" Inherits="Code_BasicInfo_Employee" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>员工信息</title>
    <script type="text/javascript" src="../../JScript/SelectDialog.js"></script>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <script type="text/javascript" src="../../JScript/ajax_validate.js?t=00"></script>
    <link href="../../css/css.css" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css?t=98" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           <Scripts>
              <asp:ScriptReference  Path="~/JScript/ajax_validate.js?t=122901"/>          
           </Scripts>
           <Services>
              <asp:ServiceReference Path="~/WebServices/Validate.asmx" />
           </Services> 
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="true">
          <ContentTemplate>

          <!--数据显示-->
          <asp:Panel ID="pnlList" runat="server" Height="100%" Width="100%">
             <!--工具栏-->
             <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
                  <table style="width:100%; height:20px;">
                     <tr>
                       <td style="height: 22px">
                           <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="SORTING_CODE">分拣线编码</asp:ListItem>
                               <asp:ListItem Value="SORTING_NAME">分拣线名称</asp:ListItem>
                              
                           </asp:DropDownList>
                           <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox"></asp:TextBox><asp:RadioButton GroupName="order" ID="rbASC" runat="server" Text="升" Checked="True" />
                           <asp:RadioButton GroupName="order" ID="rbDESC" runat="server" Text="降" />
                           <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                           <asp:Button ID="btnCreate" runat="server" Text="新增"　CssClass="ButtonCreate" OnClick="btnCreate_Click"/>
                           <asp:Button ID="btnDelete" runat="server" Text="删除"　CssClass="ButtonDel" OnClick="btnDelete_Click" OnClientClick="return DelConfirm('btnDelete')"/>
                           <asp:Button ID="btnExit" runat="server" Text="退出"  OnClick="btnExit_Click" CssClass="ButtonExit" />
                        </td>
                     </tr>
                  </table>
             </asp:Panel>
             <!--数据-->
              <asp:Panel ID="pnlMain" runat="server" Height="100%" Style="position: relative; overflow-x:auto; overflow-y:auto;" Width="100%">
                  <asp:GridView ID="gvMain" runat="server" Style="position: relative;table-layout:fixed;width:100%;"
                     OnRowEditing="gvMain_RowEditing" OnRowDataBound="gvMain_RowDataBound" CssClass="GridStyle" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False">
                      <RowStyle BackColor="AliceBlue" Height="28px" />
                      <HeaderStyle CssClass="GridHeader2" />
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                          <asp:TemplateField HeaderText="操作">
                              <HeaderStyle Width="65px" />
                          </asp:TemplateField>
                          <asp:BoundField DataField="SORTING_CODE" HeaderText="分拣线编码" >
                              <HeaderStyle Width="88px" />
                              <ItemStyle HorizontalAlign="Center" />
                              <FooterStyle Width="100px" />
                          </asp:BoundField>
                             <asp:BoundField DataField="SORTING_TYPE" HeaderText="分拣线类型">
                              <HeaderStyle Width="100px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="SORTING_NAME" HeaderText="分拣线名称" >
                              <HeaderStyle Width="100%" />
                          </asp:BoundField>
                          <asp:BoundField DataField="SORTINGSTATE" HeaderText="是否可用">
                              <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="UPDATE_DATE" HeaderText="更新时间">
                              <HeaderStyle Width="120px" />
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
              </asp:Panel>
              <!--分页导航-->
              <asp:Panel ID="pnlNavigator" runat="server" Height="31px" Style="left: 0px; position: relative; top: 0px" Width="100%">
                 <table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
                   <tr>
                     <td style="height: 29px">
                       <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
                     </td>
                   </tr>
                  </table>  
               </asp:Panel>
          </asp:Panel>  
           
          <!--编辑-->
              <asp:Panel ID="pnlEdit" runat="server" Height="480px" Width="100%" Visible="false">
                   <table class="OperationBar">
                      <tr>
                        <td>
                        <asp:Button ID="btnSave" Text=" 保 存" runat="server" OnClick="btnSave_Click" CssClass="ButtonSave" OnClientClick="return CheckBeforeSubmit()" />
                        <asp:Button ID="btnCancel" Text="取 消" runat="server" CssClass="ButtonCancel" OnClick="btnCancel_Click" />            
                        </td>
                      </tr>
                   </table>
               <div style="padding-left:15px; padding-top:15px;">
                   <table>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font>分拣线编码</td>
                         <td><asp:TextBox ID="txtSortingCode" runat="server" CssClass="myinput" Width="100px"></asp:TextBox>
                             <span id="validate_msg" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>此编码已经存在</span> 
                             <span id="msg_code" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>分拣线编码不能为空</span>
                         </td>
                      </tr>
                      <tr>
                         <td class="tdTitle"><font color="red">*</font>分拣线名称</td> 
                         <td><asp:TextBox ID="txtSortingName" runat="server"  CssClass="myinput" Width="180px"></asp:TextBox>
                         <span id="msg_name" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>分拣线名称不能为空</span>
                         </td>
                      </tr>
                      </tr> 
                        <%--  <tr>
                         <td class="tdTitle">分拣组编码</td>
                         <td><asp:TextBox ID="txtSortingGRoupCode" runat="server" CssClass="myinput" Width="100px" ></asp:TextBox></td>
                      </tr> --%>
                      <tr>
                         <td class="tdTitle">
                             分拣线类型</td>
                         <td>
                             &nbsp;<asp:DropDownList ID="txtSortingType" runat="server">
                                 <asp:ListItem  Value="0">未选择</asp:ListItem>
                                 <asp:ListItem  Value="1">1-电子标签</asp:ListItem>
                                 <asp:ListItem  Value="2">2-半自动</asp:ListItem>
                                 <asp:ListItem  Value="3">2-全自动</asp:ListItem>
                             </asp:DropDownList></td>
                        <tr>
                         <td class="tdTitle">
                             是否使用</td>
                         <td>
                             &nbsp;<asp:DropDownList ID="ddlISACTIVE" runat="server">
                                 <asp:ListItem Selected="True" Value="0">试用</asp:ListItem>
                                 <asp:ListItem Value="1">使用</asp:ListItem>
                                 <asp:ListItem Value="2">停用</asp:ListItem>
                             </asp:DropDownList></td>
                      </tr>
                      
                   </table>
                 </div>  
              </asp:Panel>  
           </ContentTemplate>
     </asp:UpdatePanel>
        <!--隐藏数据-->  
        <div>
           <asp:HiddenField ID="hdnXGQX" Value="1" runat="server" />
           <asp:HiddenField ID="hdnOpFlag" Value="0" runat="server" />
        </div>
    </form>
<script>

//删除确认
function DelConfirm(btnID)
{
     var table=document.getElementById('gvMain');
     var hasChecked=false;
     for(var i=1;i<table.rows.length;i++)
     {
        var cell=table.rows[i].cells[0];
        var chk=cell.getElementsByTagName("INPUT");
        if(chk[0].checked)
        {
            hasChecked=true;
            break;
        }
     }
     
     if(!hasChecked)
     {
        alert('请选择要删除的数据！');
        return false;
     }
      if(confirm('确定要删除选择的数据？','删除提示'))
      {
         var btn=document.getElementById(btnID);
         btn.click();
         //window.location.reload();
      }
      else
      {
         return false;
      }
}


function CheckBeforeSubmit()
{
    var name=document.getElementById('txtSortingName').value.trim();
    var code=document.getElementById('txtSortingCode').value.trim();
    var flag=true;
    if(name=="")
    {
       msg_name.style.display='block';
       flag=false;
    }
    else
    {
       msg_name.style.display='none';
    }
    if(code=="")
    {
       msg_code.style.display='block';
       flag=false;
    }
    else
    {
       msg_code.style.display='none';
    }
    return flag;
}
function Clear(obj1,obj2)
{
   obj1.value="";
   obj2.value="";
}

</script>
</body>
</html>
