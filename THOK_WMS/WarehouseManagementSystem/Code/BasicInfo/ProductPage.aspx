<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPage.aspx.cs" Inherits="Code_BasicInfo_ProductPage" %>

<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>产品信息</title>
    <script type="text/javascript" src="../../JScript/SelectDialog.js"></script>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
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
        <Triggers>
        <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
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
                               <asp:ListItem Value="PRODUCTCODE">产品编码</asp:ListItem>
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
                        <asp:Button ID="btnDown" runat="server" Text="下载" CssClass="ButtonDown" OnClick="btnDown_Click" /> 
                        <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="ButtonDown" OnClick="btnExcel_Click"/>
                        
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
                              <HeaderStyle Width="90px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                              <HeaderStyle Width="150px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="SHORTNAME" HeaderText="产品简称" >
                              <HeaderStyle Width="150px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="BARCODE" HeaderText="条形码" >
                              <HeaderStyle Width="100px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="ABCODE" HeaderText="助记码" >
                              <HeaderStyle Width="80px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="UNITNAME" HeaderText="单位名称" >
                              <HeaderStyle Width="80px" />
                          </asp:BoundField>
                          <%--<asp:BoundField DataField="JIANTIAORATE" HeaderText="件条比率" >
                              <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="TIAOBAORATE" HeaderText="条包比率" >
                              <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="BAOZHIRATE" HeaderText="包支比率" >
                              <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>--%>
                          <asp:BoundField DataField="JIANCODE" HeaderText="件单位" >
                              <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="TIAOCODE" HeaderText="条单位" >
                              <HeaderStyle Width="80px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MAXCELLPIECE" HeaderText="货位最高存储数量" >
                              <HeaderStyle Width="150px" />
                              <ItemStyle HorizontalAlign="Center" />
                          </asp:BoundField>
                          <asp:BoundField DataField="MEMO" HeaderText="备注" >
                              <HeaderStyle Width="200PX" />
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
                        <td class="tdTitle"><font color="red">*</font>产品代码</td>
	                    <td><asp:TextBox ID="txtPRODUCTCODE" runat="server" CssClass="myinput"></asp:TextBox>
                             <span id="validate_msg" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>此编码已经存在</span> 
                             <span id="msg_code" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>产品编码不能为空</span>
	                    </td>
                      </tr>

                      <tr>
                        <td class="tdTitle"><font color="red">*</font>产品名称</td>
	                    <td><asp:TextBox ID="txtPRODUCTNAME" runat="server" CssClass="myinput"></asp:TextBox>
	                       <span id="msg_name" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>产品名称不能为空</span>
	                    </td>
                      </tr>
                      
                      <tr>
                        <td class="tdTitle">产品简称</td>
	                    <td><asp:TextBox ID="txtSHORTNAME" runat="server" CssClass="myinput"></asp:TextBox></td>
                      </tr>
                      
                      <tr>
                        <td class="tdTitle">类别编码</td>
	                    <td>
	                      <table cellpadding="0" cellspacing="0">
	                        <tr>
	                          <td><asp:TextBox ID="txtPRODUCTCLASS" runat="server" CssClass="myinput" Width="113px"></asp:TextBox></td>
	                          <td><input id="Button3" type="button" class="ButtonClear2" onclick="Clear(txtPRODUCTCLASS,txtCLASSNAME)"/></td>
	                          <td><input id="Button1" onclick="SelectDialog2('txtPRODUCTCLASS,txtCLASSNAME','WMS_PRODUCTCLASS','CLASSCODE,CLASSNAME')" class="ButtonBrowse2" type="button" /></td>
	                        </tr>
	                      </table>
	                     </td>
                       </tr>
                      
                      <tr>
                        <td class="tdTitle">类别名称</td>
	                    <td><asp:TextBox ID="txtCLASSNAME" runat="server" CssClass="myinput" onfocus="this.blur();"></asp:TextBox></td>
                      </tr>

                      <tr>
                        <td class="tdTitle">供应商编码</td>
                        <td>
                          <table cellpadding="0" cellspacing="0">
	                        <tr>
	                          <td><asp:TextBox ID="txtSUPPLIERCODE" runat="server" CssClass="myinput" Width="113px"></asp:TextBox></td>
	                          <td><input id="Button6" type="button" class="ButtonClear2" onclick="Clear(txtSUPPLIERCODE,txtSUPPLIERNAME)"/></td>
	                          <td><input id="Button5" onclick="SelectDialog2('txtSUPPLIERCODE,txtSUPPLIERNAME','BI_SUPPLIER','SUPPLIERCODE,SUPPLIERNAME')" class="ButtonBrowse2" type="button" /></td>
	                        </tr>
	                      </table>
                        </td>
                      </tr>
                      <tr>
                        <td class="tdTitle">供应商名称</td>
	                    <td><asp:TextBox ID="txtSUPPLIERNAME" runat="server" CssClass="myinput" onfocus="this.blur()"></asp:TextBox></td>
                      </tr>                      
                      <tr>
                        <td class="tdTitle">条形码</td>
	                    <td><asp:TextBox ID="txtBARCODE" runat="server" CssClass="myinput"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">助记码</td>
	                    <td><asp:TextBox ID="txtABCODE" runat="server" CssClass="myinput"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">单位编码</td>
	                    <td>
	                      <table cellpadding="0" cellspacing="0">
	                         <tr>
	                           <td><asp:TextBox ID="txtUNITCODE" runat="server" CssClass="myinput" Width="113px"></asp:TextBox></td>
	                           <td><input id="Button4" type="button" class="ButtonClear2" onclick="Clear(txtUNITCODE,txtUNITNAME)" /></td>
	                           <td><input id="Button2" onclick="SelectDialog2('txtUNITCODE,txtUNITNAME','WMS_UNIT','UNITCODE,UNITNAME')" class="ButtonBrowse2" type="button" /></td>
	                         </tr>
	                      </table>
	                    
                            
	                    </td>
                      </tr>
                      <%--<tr>
                        <td class="tdTitle">件条比率</td>
	                    <td><asp:TextBox ID="txtJIANTIAORATE" runat="server" CssClass="myinput_right" ></asp:TextBox>
                            <span id="msg_rate1" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>请填写数字</span>
	                    </td>
                      </tr>
                      <tr>
                        <td class="tdTitle">条包比率</td>
	                    <td><asp:TextBox ID="txtTIAOBAORATE" runat="server" CssClass="myinput_right" ></asp:TextBox>
	                       <span id="msg_rate2" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>请填写数字</span>
	                    </td>
                      </tr>
                      <tr>
                        <td class="tdTitle">包支比率</td>
	                    <td><asp:TextBox ID="txtBAOZHIRATE" runat="server" CssClass="myinput_right" ></asp:TextBox>
	                        <span id="msg_rate3" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>请填写数字</span>
	                    </td>
                      </tr>
                      <tr>--%>
                        <td class="tdTitle">单位名称</td>
	                    <td><asp:TextBox ID="txtUNITNAME" runat="server" CssClass="myinput" onfocus="this.blur();"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td class="tdTitle">件单位</td>
	                    <td>
	                      <table cellpadding="0" cellspacing="0">
	                        <tr>
	                          <td><asp:TextBox ID="txtJIANCODE" runat="server" CssClass="myinput" Width="113px"></asp:TextBox></td>
	                          <td><input id="Button9" type="button" class="ButtonClear2" onclick="Clear(txtJIANCODE)"/></td>
	                          <td><input id="Button7" onclick="SelectDialog2('txtJIANCODE','WMS_UNIT','UNITCODE')" class="ButtonBrowse2" type="button" /></td>
	                        </tr>
	                      </table>
	                     </td>
                       </tr>
                      
                      
                      <tr>
                        <td class="tdTitle">条单位</td>
	                    <td>
	                      <table cellpadding="0" cellspacing="0">
	                        <tr>
	                          <td><asp:TextBox ID="txtTIAOCODE" runat="server" CssClass="myinput" Width="113px"></asp:TextBox></td>
	                          <td><input id="Button10" type="button" class="ButtonClear2" onclick="Clear(txtTIAOCODE)"/></td>
	                          <td><input id="Button8" onclick="SelectDialog2('txtTIAOCODE','WMS_UNIT','UNITCODE')" class="ButtonBrowse2" type="button" /></td>
	                        </tr>
	                      </table>
	                     </td>
                       </tr>
                       
                       <tr>
                         <td class="tdTitle">储位存量上限</td>
                         <td><asp:TextBox ID="txtMAXCELLPIECE" runat="server" CssClass="myinput_right" ></asp:TextBox>
	                        <span id="Span1" style="display:none; color:Red; position:absolute;"><img src="../../images/warnning.ico"/>请填写数字</span>
	                    </td>
                       </tr>
                      
                      <tr>
                        <td class="tdTitle">备注</td>
	                    <td><asp:TextBox ID="txtMEMO" runat="server" Columns="20" Rows="5" TextMode="MultiLine" Width="150px"></asp:TextBox></td>
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
    var name=document.getElementById('txtPRODUCTNAME').value.trim();
    var code=document.getElementById('txtPRODUCTCODE').value.trim();
    var r1=document.getElementById('txtJIANTIAORATE');
    var r2=document.getElementById('txtTIAOBAORATE');
    var r3=document.getElementById('txtBAOZHIRATE');
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
    
    if(r1.value=="")/////
    {
        r1.value="50";
    }
    else if(isNaN(r1.value))
    {
       msg_rate1.style.display='block';
       flag=false;
    }
    else
    {
       msg_rate1.style.display='none';
    }
    
    if(r2.value=="")/////
    {
        r2.value="20";
    }
    else if(isNaN(r2.value))
    {
       msg_rate2.style.display='block';
       flag=false;
    }
    else
    {
       msg_rate2.style.display='none';
    }
    
    if(r3.value=="")////
    {
        r3.value="10";
    }
    else if(isNaN(r3.value))
    {
       msg_rate3.style.display='block';
       flag=false;
    }
    else
    {
       msg_rate3.style.display='none';
    }
    
    
    return flag;
}


function Clear(obj1,obj2)
{
  obj1.value="";
  obj2.value="";
}

function Open()
{
  document.execCommand("saveas");
}

</script>
</body>
</html>