<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParameterPage.aspx.cs" Inherits="Code_BasicInfo_ParameterPage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>业务参数</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <link href="../../css/FieldsetCss.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
     
               <table class="OperationBar">
                  <tr>
                    <td>
                    <asp:Button ID="btnSave" Text=" 保 存" runat="server"  CssClass="ButtonSave"  OnClick="btnSave_Click" Enabled="False" />
                    <asp:Button ID="btnCancel" Text="取 消" runat="server" CssClass="ButtonCancel" OnClick="btnCancel_Click"  />            
                    </td>
                  </tr>
               </table>
               <table style="width:510px;">
                  <tr>
                    <td>
                       <div style="padding-left:15px; padding-top:15px;">
                        <fieldset style="width: 509px">
                           <legend>营销系统连接参数</legend>  
                           <table>
                                <tr style="display:none;">
                                    <td class="tdTitle" colspan="4">
                                       <asp:TextBox ID="txtID" runat="server" CssClass="HiddenControl"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td class="tdTitle">数据库类型</td>
	                                <td>
                                        &nbsp;<asp:DropDownList ID="ddlDBTYPE_1" runat="server">
                                            <asp:ListItem>SQLSERVER</asp:ListItem>
                                            <asp:ListItem>ORACLE</asp:ListItem>
                                            <asp:ListItem>ODBC</asp:ListItem>
                                            <asp:ListItem>OLEDB</asp:ListItem>
                                        </asp:DropDownList>
	                                </td>
                                 
                                    <td class="tdTitle">服务器名称</td>
	                                <td><asp:TextBox ID="txtSERVERNAME_1" runat="server" CssClass="myinput"></asp:TextBox></td>                         
                                  </tr>

                                  <tr>
                                    <td class="tdTitle">用户名</td>
	                                <td><asp:TextBox ID="txtUSERID_1" runat="server" CssClass="myinput"></asp:TextBox></td>  
                                    <td class="tdTitle">数据库名称</td>
	                                <td><asp:TextBox ID="txtDBNAME_1" runat="server" CssClass="myinput"></asp:TextBox></td>
                                  </tr>

                                  <tr>
                                    <td class="tdTitle">数据库密码</td>
	                                <td><asp:TextBox ID="txtPWD_1" runat="server" CssClass="myinput" TextMode="Password"></asp:TextBox></td>
	                                <td colspan="2" style="text-align:right;"><asp:Button ID="btnTestConnection1" runat="server" Text="测试连接" CssClass="button2" OnClick="btnTestConnection1_Click" /></td>
                                  </tr>
                           </table>
                        </fieldset>
                        
                        <fieldset style="width: 509px">
                           <legend>中烟系统连接参数</legend>  
                           <table>
                                <tr>
                                    <td class="tdTitle">
                                        数据库类型</td>
	                                <td>
                                        &nbsp;<asp:DropDownList ID="ddlDBTYPE_2" runat="server">
                                            <asp:ListItem>SQLSERVER</asp:ListItem>
                                            <asp:ListItem>ORACLE</asp:ListItem>
                                            <asp:ListItem>ODBC</asp:ListItem>
                                            <asp:ListItem>OLEDB</asp:ListItem>
                                        </asp:DropDownList>
	                                </td>
                                     <td class="tdTitle">
                                        服务器名称</td>
	                                <td><asp:TextBox ID="txtSERVERNAME_2" runat="server" CssClass="myinput"></asp:TextBox></td>
                                                           
                                  </tr>
                                  <tr>
                                    <td class="tdTitle">用户名</td>
	                                <td><asp:TextBox ID="txtUSERID_2" runat="server" CssClass="myinput"></asp:TextBox></td>
                                    <td class="tdTitle"> 数据库名称</td>
	                                <td><asp:TextBox ID="txtDBNAME_2" runat="server" CssClass="myinput"></asp:TextBox></td>                 
                                  </tr>

                                  <tr>
                                    <td class="tdTitle">数据库密码</td>
	                                <td><asp:TextBox ID="txtPWD_2" runat="server" CssClass="myinput" TextMode="Password"></asp:TextBox></td>
	                                <td colspan="2" style="text-align:right;"><asp:Button ID="btnTestConnection2" CssClass="button2" runat="server" Text="测试连接" OnClick="btnTestConnection2_Click" /></td>
                                  </tr>
                           </table>
                        </fieldset>
                        
                        <fieldset style="width: 509px">
                           <legend>其他系统连接参数</legend>  
                           <table>
                             <tr>
                                <td class="tdTitle">
                                    数据库类型</td>
	                            <td>
                                    &nbsp;<asp:DropDownList ID="ddlDBTYPE_3" runat="server">
                                            <asp:ListItem>SQLSERVER</asp:ListItem>
                                            <asp:ListItem>ORACLE</asp:ListItem>
                                            <asp:ListItem>ODBC</asp:ListItem>
                                            <asp:ListItem>OLEDB</asp:ListItem>
                                        </asp:DropDownList>
	                            </td>
                                 <td class="tdTitle">
                                    服务器名称</td>
	                            <td><asp:TextBox ID="txtSERVERNAME_3" runat="server" CssClass="myinput"></asp:TextBox></td>                    
                              </tr>

                              <tr>
                                <td class="tdTitle">用户名</td>
	                            <td><asp:TextBox ID="txtUSERID_3" runat="server" CssClass="myinput"></asp:TextBox></td>  
                                <td class="tdTitle"> 数据库名称</td>
	                            <td><asp:TextBox ID="txtDBNAME_3" runat="server" CssClass="myinput"></asp:TextBox></td>
                              </tr>
                              <tr>
                                <td class="tdTitle">
                                    数据库密码</td>
	                            <td><asp:TextBox ID="txtPWD_3" runat="server" CssClass="myinput" TextMode="Password"></asp:TextBox></td>
	                            <td colspan="2" style="text-align:right;"> <asp:Button ID="btnTestConnection3" runat="server" Text="测试连接" CssClass="button2" OnClick="btnTestConnection3_Click" /></td>
                              </tr>
                           </table>
                        </fieldset>
                        
                        <fieldset style="width: 509px; display:none;">
                           <legend>货位图形化参数</legend>  
                           <table>
                              <tr>
                                <td class="tdTitle">
                                    货位图形X轴</td>
	                            <td><asp:TextBox ID="txtCELL_IMG_X" runat="server" CssClass="myinput_right" onblur="IsNumber(this,'')"></asp:TextBox></td>
                                 <td class="tdTitle">
                                    货位图形Y轴</td>
	                            <td><asp:TextBox ID="txtCELL_IMG_Y" runat="server" CssClass="myinput_right" onblur="IsNumber(this,'')"></asp:TextBox></td>                     
                              </tr>
                              <tr>
                                <td class="tdTitle">
                                    货位图形Z轴</td>
	                            <td><asp:TextBox ID="txtSPACE_Z" runat="server" CssClass="myinput_right" onblur="IsNumber(this,'')"></asp:TextBox></td>
                              </tr>
                           </table>
                        </fieldset>
                      </div>                         
                    </td>
                  </tr>
               </table>

              <div>
                  <asp:HiddenField ID="hdnXGQX" Value="0" runat="server" />
              </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
  
</body>
</html>