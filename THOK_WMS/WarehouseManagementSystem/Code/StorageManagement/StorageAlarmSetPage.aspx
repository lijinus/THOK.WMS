<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageAlarmSetPage.aspx.cs" Inherits="Code_StorageManagement_StorageAlarmSetPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>库存安全预警设置</title>
    <script type="text/javascript" src="../../JScript/SelectDialog.js"></script>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
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
          <asp:Panel ID="pnlList" runat="server" Height="480px" Width="100%">
             <!--工具栏-->
             <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
                  <table style="width:100%; height:20px;">
                     <tr>
                       <td style="height: 22px">
                           <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="PRODUCTNAME">产品名称</asp:ListItem>
                               <asp:ListItem Value="PRODUCTCODE">产品代码</asp:ListItem>
                               <asp:ListItem Value="Memo">备注</asp:ListItem>
                           </asp:DropDownList>
                           <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox"></asp:TextBox><asp:RadioButton GroupName="order" ID="rbASC" runat="server" Text="升" Checked="True" />
                           <asp:RadioButton GroupName="order" ID="rbDESC" runat="server" Text="降" />
                           <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                           <asp:Button ID="btnCreate" runat="server" Text="新增"　CssClass="ButtonCreate" OnClick="btnCreate_Click" Enabled="False"/>
                           <asp:Button ID="btnDelete" runat="server" Text="删除"　CssClass="ButtonDel" OnClick="btnDelete_Click" OnClientClick="return DelConfirm('btnDelete')" Enabled="False"/>
                           <asp:Button ID="btnExit" runat="server" Text="退出"  OnClick="btnExit_Click" CssClass="ButtonExit" />
                        </td>
                     </tr>
                  </table>
             </asp:Panel>
             <!--数据-->
              <asp:Panel ID="pnlMain" runat="server" Height="480px" Style="position: relative; overflow-x:auto; overflow-y:auto;" Width="100%">
                  <asp:GridView ID="gvMain" runat="server" Style="position: relative;table-layout:fixed;width:100%;"
                     OnRowEditing="gvMain_RowEditing" OnRowDataBound="gvMain_RowDataBound" CssClass="GridStyle" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False">
                      <RowStyle BackColor="AliceBlue" Height="28px" />
                      <HeaderStyle CssClass="GridHeader2" />
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                          <asp:TemplateField HeaderText="操作">
                              <HeaderStyle Width="60px" />
                          </asp:TemplateField>
                          <asp:BoundField DataField="PRODUCTCODE" HeaderText="产品代码" >
                              <HeaderStyle Width="70px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                              <HeaderStyle Width="130px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MAX_LIMITED" HeaderText="库存上限" >
                              <HeaderStyle Width="70px" />
                              <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MIN_LIMITED" HeaderText="库存下限">
                              <HeaderStyle Width="70px" />
                              <ItemStyle HorizontalAlign="Right" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MEMO" HeaderText="备注" >
                              <HeaderStyle Width="100%" />
                          </asp:BoundField>
                      </Columns>
                  </asp:GridView>
              </asp:Panel>
              <!--分页导航-->
              <asp:Panel ID="pnlNavigator" runat="server" Height="31px" Style="left: 0px; position: relative; top: 0px" Width="100%">
                 <table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
                   <tr>
                     <td>
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
                     <TABLE>
                      <tr style="display:none;">
                        <td class="tdTitle">ID</td>
	                    <td><asp:TextBox ID="txtID" runat="server" CssClass="myinput"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle"><font color="red">*</font>产品代码</td>
	                    <td>
                           <table cellpadding="0" cellspacing="0">
                                <tr>
                                   <td><asp:TextBox ID="txtPRODUCTCODE" runat="server" CssClass="myinput" onfocus="this.blur()" Width="112px" onpropertychange="UniqueValidate('WMS_ALARM','PRODUCTCODE',this.value,'1=1')"></asp:TextBox></td>
                                   <td><input id="Button2" type="button" class="ButtonClear2" onclick="Clear(txtPRODUCTCODE,txtPRODUCTNAME)" /></td>
                                   <td><input id="Button1" onclick="SelectDialog2('txtPRODUCTCODE,txtPRODUCTNAME','WMS_PRODUCT','PRODUCTCODE,PRODUCTNAME');" class="ButtonBrowse2" type="button" /></td>
                                  <td style="width:160px;">
                                      <span id="validate_msg" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>此产品已经设置预警</span> 
                                     <span id="msg_code" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>产品代码不能为空</span>
                                  </td>
                               </tr>
                           </table>
                         </td>
                      </tr>
                      <tr>
                        <td class="tdTitle"><font color="red">*</font>产品名称</td>
	                    <td><asp:TextBox ID="txtPRODUCTNAME" runat="server" CssClass="myinput" onfocus="this.blur()"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle"><font color="red">*</font>库存上限</td>
	                    <td>
                            <asp:TextBox ID="txtMAX" runat="server" CssClass="myinput_right" onblur="IsNumber(this,'')"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="txtMIN"><font color="red">*</font>库存下限</td>
	                    <td>
                            <asp:TextBox ID="txtMIN" runat="server" CssClass="myinput_right" onblur="IsNumber(this,'')"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">备注</td>
	                    <td><asp:TextBox ID="txtMEMO" runat="server" Height="78px" TextMode="MultiLine" Width="150px"></asp:TextBox></td>
                      </tr>
                    </TABLE>          
                   </div> 
              </asp:Panel>  
           </ContentTemplate>
     </asp:UpdatePanel>
        <!--隐藏数据-->  
        <div>
           <asp:HiddenField ID="hdnXGQX" Value="0" runat="server" />
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
    var code=document.getElementById('txtPRODUCTCODE').value;
    
    if( code=="")
    {
       msg_code.style.display='block';
       return false;
    }
//    else
//    {    
//        UniqueValidate('WMS_ALARM','PRODUCTCODE',code,'1=1');
//        if(validate_msg.style.display=='block')
//        {
//          return false;
//        }
//    }

//    var max=document.getElementById('txtMAX').value;
//    var min=document.getElementById('txtMIN').value
}

function Clear(obj1,obj2)
{
   obj1.value="";
   obj2.value="";
}

</script>
</body>
</html>