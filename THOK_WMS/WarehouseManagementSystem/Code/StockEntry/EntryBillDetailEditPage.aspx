<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryBillDetailEditPage.aspx.cs" Inherits="Code_StockEntry_EntryBillDetailEdit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>入库单明细&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <script src="../../Jscript/Search.js?time=2011" type="text/javascript"></script>
</head>
<body style=" margin-left:30px; margin-top:20px; margin-bottom:10px; height:100%;">
    <form id="form1" runat="server" >
      <TABLE cellpadding="0" cellspacing="1" >
          <tr style="display:none;">
            <td class="tdTitle">ＩＤ</td>
	        <td ><asp:TextBox ID="txtID" runat="server" CssClass="myinput" Width="140px"></asp:TextBox></td>
          </tr>
          <tr style="">
            <td class="tdTitle" style="height: 25px"><font color="red">*</font>入库单号</td>
	        <td style="height: 25px" ><asp:TextBox ID="txtBillNo" runat="server" CssClass="myinput" Enabled="False" Width="140px" onfocus="this.blur();"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="tdTitle"><font color="red">*</font>产品代码</td>
	        <td >
	          <table cellpadding="0" cellspacing="0">
	            <tr>
	              <td style="height: 24px"><asp:TextBox ID="txtProductCode" runat="server" CssClass="myinput" Width="140px"></asp:TextBox></td>
	             <%-- <td><asp:TextBox ID="txtProductCode" runat="server" CssClass="myinput" Width="140px" onkeyup="searchSuggest();" autocomplete="off"></asp:TextBox>--%>
	              <td style="height: 24px"><input id="Button2" class="ButtonBrowse2" onclick="SelectDialog2('txtProductCode,txtProductName,txtUnitCode,txtUnitName','WMS_PRODUCT','PRODUCTCODE,PRODUCTNAME,UNITCODE,UNITNAME','PRODUCTCODE',txtProductCode.value);" type="button" value="" tabindex="1" /></td>
	            </tr>
	          </table>
	          <%--<div id="search_suggest" style="display:none">   --%>   
	        </td>
          </tr>
          <tr>
            <td class="tdTitle">产品名称</td>
	        <td ><asp:TextBox ID="txtProductName" runat="server" CssClass="myinput" Width="140px" ReadOnly="True" TabIndex="13"></asp:TextBox>
	        
	        </td>
          </tr>
          <tr style="">
            <td class="tdTitle"><font color="red">*</font>单位编码</td>
	        <td >
	          <table cellpadding="0" cellspacing="0">
	            <tr>
	              <td style="height: 24px"><asp:TextBox ID="txtUnitCode" runat="server" CssClass="myinput" Width="140px"  onfocus="this.blur();" TabIndex="14"></asp:TextBox></td>
	            <%-- <td><input id="Button1" class="ButtonBrowse2"  onclick="SelectDialog2('txUnitCode,txtUnitName','WMS_UNIT','UNITCODE,UNITNAME');"
                    type="button" value="" /></td>--%>
	            </tr>
	          </table>
	        </td>
          </tr>
          <tr style="">
            <td class="tdTitle" style="height: 25px">单位名称</td>
	        <td style="height: 25px" ><asp:TextBox ID="txtUnitName" runat="server" CssClass="myinput" Width="140px"   onfocus="this.blur();" TabIndex="2"></asp:TextBox></td>
          </tr>
          <tr style="">
            <td class="tdTitle">数量</td>
	        <td ><asp:TextBox ID="txtQuantity" runat="server" CssClass="myinput" Width="140px" onblur="Count();changeInputQty();" style="text-align:right;" TabIndex="3">0.00</asp:TextBox></td>
          </tr>
          <tr style="">
            <td class="tdTitle">实际入库量</td>
	        <td ><asp:TextBox ID="txtInputQuantity" runat="server" CssClass="myinput" Width="140px" onblur="Count();" style="text-align:right;" TabIndex="2">0.00</asp:TextBox></td>
          </tr>
          <tr>
            <td class="tdTitle">单价</td>
	        <td ><asp:TextBox ID="txtPrice" runat="server" CssClass="myinput" Width="140px" onblur="IsNumber(this,'单价');Count();"   style="text-align:right;" TabIndex="4" >0.00</asp:TextBox></td>
          </tr>
          <tr style="">
            <td class="tdTitle">总金额</td>
	        <td ><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="myinput" Width="140px" style="text-align:right;" onfocus="CannotEdit(this)" TabIndex="5">0.00</asp:TextBox></td>
          </tr>
          <tr style="">
            <td class="tdTitle">备注</td>
	        <td ><asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine"  CssClass="MulitiLineTextBox" Height="61px" Width="141px" TabIndex="6"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="tdTitle" style="height:34px;"></td>
	        <td style="height: 34px" >
	            <%--<asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Enabled="False"></asp:LinkButton>--%>
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonCss" OnClick="btnSave_Click" Enabled="False" tabindex="7"/>
                <%--<input type ="submit" id="btnSave1" runat="server" value="保存" class="ButtonCss" onclick="Save();" tabindex="7" >/--%>
                <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="ButtonCss" OnClientClick="javascript:window.close();" /></td>
          </tr> 
        </TABLE>
    </form>
<script>
//  function Save()
//  {
//     if(IsNumber(document.getElementById('txtQuantity'),'数量') && IsNumber(document.getElementById('txtInputQuantity'),'数量'))
//     {
//        document.getElementById('btnSave').click();
//        javascript:window.close();
//     }
//  }
  function Count()
  {
     var qty=document.getElementById('txtQuantity').value;
     var price=document.getElementById('txtPrice').value;
     document.getElementById('txtTotalAmount').value=parseFloat(qty*price).toFixed(3);
  }
  function changeInputQty()
  {
     document.getElementById('txtInputQuantity').value=document.getElementById('txtQuantity').value;
     document.getElementById('txtInputQuantity').focus();
  }
</script>    
</body>
</html>
