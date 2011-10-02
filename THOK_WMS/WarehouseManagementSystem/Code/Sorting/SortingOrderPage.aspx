<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingOrderPage.aspx.cs" Inherits="Code_StockEntry_EntryBillPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

    <title>入库单</title>
    <base target="_self" />
    <link href="../../css/css.css?t=0" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../../JScript/Calendar.js"></script>
    <script type="text/javascript" src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>    
           
      <table style="width:100%;font-size:12px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
             <!--工具栏-->
             <asp:Panel ID="pnlListToolbar" runat="server"  Height="30px" Style="position: relative" Width="100%">
                  <table style="width:100%; height:20px;">
                     <tr>
                       <td style="height: 22px">
                           <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="ORDER_ID">订单号</asp:ListItem>
                               <asp:ListItem Value="ORDER_DATE">订单时间</asp:ListItem>
                           </asp:DropDownList>
                           <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox"></asp:TextBox>
                           <input id="Button1" type="button" onclick="setday(txtKeyWords)" class="ButtonDate" />
                          
                           <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" CssClass="ButtonQuery"/>
                          
                           <%--<asp:Button ID="btnCreate" runat="server" Text="新增"　CssClass="ButtonCreate" OnClick="btnCreate_Click" Enabled="False"/>--%>
                           <%--<asp:Button ID="btnCreate2" runat="server" Text="备货区补货" CssClass="ButtonCreate"  OnClick="btnCreate2_Click"/>--%>
                          <%-- <asp:Button ID="btnDelete" runat="server" Text="删除"　CssClass="ButtonDel" OnClick="btnDelete_Click" OnClientClick="return DelConfirm('btnDelete')" Enabled="False"/>
                           <asp:RadioButton GroupName="order" ID="rbASC" runat="server" Text="升" Checked="True" />
                           <asp:RadioButton GroupName="order" ID="rbDESC" runat="server" Text="降" />
                          --%>
                           <asp:Button ID="btnExit" runat="server" Text="退出"  OnClick="btnExit_Click" OnClientClick="Exit();" CssClass="ButtonExit" />
                        </td>
                        <td style="height:22px" align="right">
                          <%--<asp:Button ID="Button2" runat="server" Text="清除当天信息" CssClass="ButtonDel" OnClick="btnDeleteDown_Click" /> --%>
                           <asp:Button ID="btnManualDown" runat="server" Text="下载" CssClass="ButtonDown" OnClick="btnManualDown_Click" /> 
                           
                           <%--<asp:Button ID="btnLineReturn" runat="server" Text="分拣线退库" CssClass="ButtonDown" OnClick="btnLineReturn_Click" />--%>
                           <%--<asp:Button ID="btnAutoDown" runat="server" Text="自动下载" CssClass="ButtonDown" OnClick="btnAutoDown_Click" /> --%>
                           <%--<asp:Button ID="btnDown" runat="server" Text="下载营销系统单据" CssClass="ButtonDown" OnClick="btnDown_Click" /> --%>
                        </td>
                     </tr>
                  </table>
             </asp:Panel>
            </td>
        </tr>
        <tr>
          <td>
           <div style="height:210px;" class="edit_body">
               <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="GridStyle" CellSpacing="1" GridLines="None" Width="100%" OnRowDataBound="gvMain_RowDataBound">
                   <Columns>
                       <asp:TemplateField>
                           <HeaderStyle Width="15px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                       </asp:TemplateField>
                       <asp:BoundField DataField="ORDER_ID" HeaderText="订单单号">
                            <HeaderStyle Width="80px" />
                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORDER_DATE" HeaderText="订单日期" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" >
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORDER_TYPE" HeaderText="订单类型">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CUST_CODE" HeaderText="客户编号" >
                         <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                           </asp:BoundField>
                        <asp:BoundField DataField="CUST_NAME" HeaderText="客户名称">
                        <HeaderStyle Width="180px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QUANTITY_SUM" HeaderText="总数量">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>                                        
                        <asp:BoundField DataField="DETAIL_NUM" HeaderText="明细数">
                        <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="DELIVER_LINE_NAME" HeaderText="送货路线">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField> 
                   </Columns>
                   <RowStyle BackColor="AliceBlue" Height="26px" />
                   <PagerStyle Font-Italic="False" Font-Size="10pt" Height="21px" />
                   <HeaderStyle BackColor="Gainsboro" Font-Size="10pt" ForeColor="DimGray" CssClass="GridHeader2" />
                   <AlternatingRowStyle BackColor="White" />
               </asp:GridView>
           </div>
          
           <div Style=" text-align:left;" class="edit_pager"><!--分页导航-->
              <NetPager:AspNetPager ID="pager" Width="450" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
           </div>
          </td>
        </tr>
     </table>
     
   <div class="edit_detail_header">
        <table cellpadding="0"  cellspacing="0" style="width:100%;" >
            <tr>
               <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                   订单号<asp:Label ID="lblBillNo" runat="server" ForeColor="Red"></asp:Label>明细</td>
            </tr>
        </table>
   </div>
   <div style="height:240px;" class="edit_detail_body">
       <asp:DataGrid ID="dgDetail" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgDetail_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify">
           <Columns>
               <asp:TemplateColumn HeaderText="序号">
                   <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                       Font-Underline="False" HorizontalAlign="Center" />
                   <HeaderStyle Width="25px" />
               </asp:TemplateColumn>
               <asp:BoundColumn DataField="ORDER_DETAIL_ID" HeaderText="订单明细号" Visible="False">
                   <HeaderStyle Width="20px" />
               </asp:BoundColumn>
               <asp:BoundColumn DataField="BRAND_CODE" HeaderText="商品编码">
                   <HeaderStyle Width="90px" />
                   <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                       Font-Underline="False" HorizontalAlign="Center" />
               </asp:BoundColumn>
               <asp:BoundColumn DataField="BRAND_NAME" HeaderText="商品名称">
                   <HeaderStyle Width="150px" />
               </asp:BoundColumn>
                <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                   <HeaderStyle Width="60px" />
                   <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                       Font-Underline="False" HorizontalAlign="Right" />
               </asp:BoundColumn>
               <asp:BoundColumn DataField="BRAND_UNIT_NAME" HeaderText="单位">
                   <HeaderStyle Width="55px" />
                   <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                       Font-Underline="False" HorizontalAlign="Center" />
               </asp:BoundColumn>
               <asp:BoundColumn DataField="PRICE" HeaderText="单价">
                   <HeaderStyle Width="60px" />
                   <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                       Font-Underline="False" HorizontalAlign="Right" />
               </asp:BoundColumn>
           </Columns>
           <ItemStyle BackColor="AliceBlue" Height="26px" />
           <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
           <AlternatingItemStyle BackColor="White" />
       </asp:DataGrid>
       <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px"></asp:Label>
   </div> 
   <div class="edit_detail_pager"><!--明细分页-->
       <NetPager:AspNetPager ID="pager2" Width="450px" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
   </div>
      
  <div style="font-size:0px; position:absolute; bottom:0px; right:0px;">
           <asp:Button ID="btnReload" runat="server" Text="" CssClass="HiddenControl" OnClick="btnReload_Click" />
           <asp:HiddenField ID="hdnOpFlag" runat="server"  Value="2"/>
           <asp:HiddenField ID="hdnRowIndex" runat="server"  Value="0"/>
           <asp:HiddenField ID="hdnScrollTop" runat="server"  Value="0"/>
           <asp:HiddenField ID="hdnDetailRowIndex" runat="server"  Value="0"/>
  </div>

<script >
 
function selectRow(objGridName,index)
{

  if(document.getElementById('hdnOpFlag').value=="0")
  {
     return;
  }
  var table=document.getElementById(objGridName);
  for(var i=1;i<table.rows.length;i++)       //table.rows[1].cells.length
  {
     table.rows[i].cells[0].innerHTML="";
  }
  table.rows[index+1].cells[0].innerHTML="<img src=../../images/arrow01.gif />";
  if(objGridName=="gvMain")
  {
      document.getElementById('hdnRowIndex').value=index;
      document.getElementById('hdnScrollTop').value=document.getElementById('gvMain').parentElement.parentElement.scrollTop;
      
      document.getElementById('btnReload').click();
  }
}

//删除确认
function DelConfirm(btnID)
{
     var table=document.getElementById('gvMain');
     var hasChecked=false;
     for(var i=1;i<table.rows.length;i++)
     {
        var cell=table.rows[i].cells[1];
        var chk=cell.getElementsByTagName("INPUT");
        if(chk[0].checked)
        {
            hasChecked=true;
            break;
        }
     }
     
     if(!hasChecked)
     {
        alert('请选择要删除的入库单！');
        return false;
     }
      if(confirm('确定要删除选择的入库单？','删除提示'))
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

function Exit()
{
   window.open("../../MainPage.aspx","_self");
}
</script> 

               </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
