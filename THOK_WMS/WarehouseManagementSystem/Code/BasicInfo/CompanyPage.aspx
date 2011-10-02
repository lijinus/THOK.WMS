<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyPage.aspx.cs" Inherits="Code_BasicInfo_Company" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>公司信息</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
     
               <table class="OperationBar">
                  <tr>
                    <td style="width: 100%">
                    <asp:Button ID="btnSave" Text=" 保 存" runat="server"  CssClass="ButtonSave"  OnClick="btnSave_Click" Enabled="False" />
                    <asp:Button ID="btnCancel" Text="取 消" runat="server" CssClass="ButtonCancel" OnClick="btnCancel_Click"  />            
                    </td>
                  </tr>
               </table>
               
              <div style="padding-left:15px; padding-top:15px;">
                <table  cellpadding="1" cellspacing="3">
                  <tr>
                     <td class="tdTitle"><font color="red">*</font>机构编号</td>
                     <td style="width:200PX;"><asp:TextBox ID="txtComCode" runat="server" CssClass="myinput" Width="166px"></asp:TextBox>
                     </td>
                     <td class="tdTitle"><font color="red">*</font>机构名称</td> 
                     <td ><asp:TextBox ID="txtComName" runat="server"  Width="166px" CssClass="myinput" OnTextChanged="txtComName_TextChanged"></asp:TextBox>
                     </td>
                  </tr>
                  <tr>

                  </tr>
                  
                  <tr>
                     <td class="tdTitle"><font color="red">*</font>国家局统一码</td>
                     <td ><asp:TextBox ID="txtUnifiedCode" runat="server" CssClass="myinput" Width="166px"></asp:TextBox>
                     </td>
                  
                     <td class="tdTitle"><font color="red">*</font>机构类型</td>
                     <td >
                         <asp:DropDownList ID="ddlComType" runat="server">
                             <asp:ListItem Selected="True">选择机构类型</asp:ListItem>
                         </asp:DropDownList></td>

                         
                  </tr>
                  
                  <tr>

                  </tr>
                  <tr>
                     <td class="tdTitle"><font color="red">*</font>上级编码</td>
                     <td ><asp:TextBox ID="txtUP_CODE" runat="server" CssClass="myinput" Width="166px"></asp:TextBox>
                     </td>
                  
                     <td class="tdTitle">仓库面积</td>
                     <td ><asp:TextBox ID="txtSTORE_ROOM_AREA" runat="server" CssClass="myinput_right" Width="166px" onblur="IsNumber(this,'分拣线数')"></asp:TextBox>
                         </td>

                         
                  </tr>
                  
                  <tr>

                  </tr>
                  <tr>
                     <td class="tdTitle">仓库容量</td>
                     <td ><asp:TextBox ID="txtCapacity" runat="server" CssClass="myinput_right" Width="166px" onblur="IsNumber(this,'仓库容量')"></asp:TextBox>
                         件
                     </td>
                     
                     <td class="tdTitle">分拣线数</td>
                     <td ><asp:TextBox ID="txtSortLine" runat="server" CssClass="myinput_right" Width="166px" onblur="IsNumber(this,'分拣线数')"></asp:TextBox>
                         条</td>
                  </tr>
                  
                  <tr>

                     
                  </tr>
                  
                  
                  <tr>
                  <td class="tdTitle">仓库个数</td>
                     <td ><asp:TextBox ID="txtSTORE_ROOM_NUM" runat="server" CssClass="myinput_right" Width="166px" onblur="IsNumber(this,'分拣线数')"></asp:TextBox>
                         </td>
                     <td class="tdTitle">更新时间</td>
                     <td ><asp:TextBox ID="txtUpdatedTime" runat="server"  CssClass="myinput" Width="166px" onfocus="CannotEdit(this)"></asp:TextBox></td>
                  </tr>
               </table>
              </div>      
              <div>
                  <asp:HiddenField ID="hdnXGQX" Value="0" runat="server" />
              </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
  
</body>
</html>
