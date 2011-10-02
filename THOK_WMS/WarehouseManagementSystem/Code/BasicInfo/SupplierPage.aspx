<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierPage.aspx.cs" Inherits="Code_BasicInfo_SupplierPage" %>

<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>供应商信息</title>
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
                               <asp:ListItem Selected="True" Value="SUPPLIERNAME">供应商名称</asp:ListItem>
                               <asp:ListItem Value="SUPPLIERCODE">供应商编号</asp:ListItem>
                               <asp:ListItem Value="CONTACTPERSON">联系人</asp:ListItem>
                               <asp:ListItem Value="Memo">备注</asp:ListItem>
                           </asp:DropDownList>
                           <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox"></asp:TextBox><asp:RadioButton GroupName="order" ID="rbASC" runat="server" Text="升" Checked="True" />
                           <asp:RadioButton GroupName="order" ID="rbDESC" runat="server" Text="降" />
                           <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                           <asp:Button ID="btnCreate" runat="server" Text="新增"　CssClass="ButtonCreate" OnClick="btnCreate_Click" Enabled="False"/>
                           <asp:Button ID="btnDelete" runat="server" Text="删除"　CssClass="ButtonDel" OnClick="btnDelete_Click" OnClientClick="return DelConfirm('btnDelete')" Enabled="False"/>
                           <asp:Button ID="btnExit" runat="server" Text="退出"  OnClick="btnExit_Click" CssClass="ButtonExit" />
                           
                        </td>
                        <td style="height:22px" align="right">
                            <asp:Button ID="btnDown" runat="server" Text="下载供应商" CssClass="ButtonDown" OnClick="btnDown_Click" /> 
                            <%--<asp:Button ID="btnDownCustomer" runat="server" Text="下载客户" CssClass="ButtonDown" OnClick="btnDownCustomer_Click" /> --%>
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
                              <HeaderStyle Width="60px" />
                          </asp:TemplateField>
                          <asp:BoundField DataField="SUPPLIERCODE" HeaderText="供应商编码" >
                              <HeaderStyle Width="80px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="SUPPLIERNAME" HeaderText="供应商名称" >
                              <HeaderStyle Width="200px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="CONTECTPERSON" HeaderText="联系人">
                              <HeaderStyle Width="90px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="TEL" HeaderText="联系电话">
                              <HeaderStyle Width="120px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ADDRESS" HeaderText="地址">
                              <HeaderStyle Width="150px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ZIP" HeaderText="邮编">
                              <HeaderStyle Width="70px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ISACTIVE" HeaderText="是否启用">
                              <HeaderStyle Width="70px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="BANKACCOUNT" HeaderText="银行帐号">
                              <HeaderStyle Width="150px" />
                           </asp:BoundField>   
                           <asp:BoundField DataField="BANKNAME" HeaderText="银行名称">
                              <HeaderStyle Width="150px" />
                          </asp:BoundField>
                           <asp:BoundField DataField="TAXNO" HeaderText="税号">
                              <HeaderStyle Width="150px" />
                          </asp:BoundField>
                           <asp:BoundField DataField="CREDITGRADE" HeaderText="信用等级">
                              <HeaderStyle Width="80px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MEMO" HeaderText="备注" >
                              <HeaderStyle Width="150px" />
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
                      <tr>
                        <td class="tdTitle"><font color="red">*</font>供应商编码</td>
	                    <td><asp:TextBox ID="txtSUPPLIERCODE" runat="server" CssClass="myinput" Width="215px"></asp:TextBox>
                             <span id="validate_msg" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>此编码已经存在</span> 
                             <span id="msg_code" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>供应商编码不能为空</span>
	                    
	                    </td>
                      </tr>
                      <tr>
                        <td class="tdTitle"><font color="red">*</font>供应商名称</td>
	                    <td><asp:TextBox ID="txtSUPPLIERNAME" runat="server" CssClass="myinput" Width="215px"></asp:TextBox>
	                    <span id="msg_name" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>供应商名称不能为空</span>
	                    </td>
                      </tr>
                      <tr>
                        <td class="tdTitle">联系电话</td>
	                    <td><asp:TextBox ID="txtTEL" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">传真</td>
	                    <td><asp:TextBox ID="txtFAX" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">联系人</td>
	                    <td><asp:TextBox ID="txtCONTECTPERSON" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">地址</td>
	                    <td><asp:TextBox ID="txtADDRESS" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">邮编</td>
	                    <td><asp:TextBox ID="txtZIP" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">银行帐号</td>
	                    <td><asp:TextBox ID="txtBANKACCOUNT" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">银行名称</td>
	                    <td><asp:TextBox ID="txtBANKNAME" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">税号</td>
	                    <td><asp:TextBox ID="txtTAXNO" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">信用等级</td>
	                    <td><asp:TextBox ID="txtCREDITGRADE" runat="server" CssClass="myinput" Width="215px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">是否启用</td>
	                    <td>
                            <asp:DropDownList ID="ddlActive" runat="server">
                                <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                                <asp:ListItem Value="0">未启用</asp:ListItem>
                            </asp:DropDownList></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">备注</td>
	                    <td><asp:TextBox ID="txtMEMO" runat="server" Columns="20" Rows="5" TextMode="MultiLine" Width="215px"></asp:TextBox></td>
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
    var name=document.getElementById('txtSUPPLIERNAME').value.trim();
    var code=document.getElementById('txtSUPPLIERCODE').value.trim();
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
</script>
</body>
</html>
