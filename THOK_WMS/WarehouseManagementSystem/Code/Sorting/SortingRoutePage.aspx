<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SortingRoutePage.aspx.cs" Inherits="Code_StockDelivery_DeliveryBillEditPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>出库编辑</title>
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
<script type="text/javascript">
function OpenNew()
{
    if(document.getElementById("hdnOpFlag").value=="0")
    {
//       alert("请先保存出库单汇总信息");
//       return false;
    }
    else
    {
        var date=new Date();
        var time=date.getMilliseconds();
        var BillNo=document.getElementById('txtBillNo').value;
        var unitcode=document.getElementById('lblUnitCode').value;
        var unitname=document.getElementById('lblUnitName').value;
        // alert(unitname);
        window.showModalDialog("DeliveryBillDetailEditPage.aspx?BILLNO="+BillNo+"&UNITNAME="+unitname+"&UNITCODE="+unitcode+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=310px;dialogHeight=390px");
        var billno=document.getElementById('txtBillNo').value;
        window.location.href='DeliveryBillEditPage.aspx?BILLNO='+billno;
    }
}

function OpenEdit(id)
{
    var date=new Date();
    var time=date.getMilliseconds();
    //var BillNo=document.getElementById('txtBillNo').value;
    //alert(id);
    window.showModalDialog("DeliveryBillDetailEditPage.aspx?ID="+id+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=310px;dialogHeight=390px");
}

//删除确认
function DelConfirm(btnID)
{
     var table=document.getElementById('dgDetail');
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
        alert('请选择要删除的出库明细！');
        return false;
     }
      if(confirm('确定要删除选择的出库明细？','删除提示'))
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
function Add(chkex)
{
     var table=document.getElementById('dgDetail');
     var hasChecked=false;
     var quantity=0;
     for(var i=0;i<table.rows.length;i++)
     {
        var cell=table.rows[i].cells[0];
        var chk=cell.getElementsByTagName("INPUT");
        if(chk[0].checked)
        {
            var quan =table.rows[i].cells[6].Text;
            quatity=quantity+quan;
        }
     }
}

function Exit()
{
   window.open("../../MainPage.aspx","_self");
}
function Back()
{
    var date=new Date();
    var time=date.getMilliseconds();
   window.open("DeliveryBillPage.aspx?t="+time,"_self");
   return false;
}


</script> 
</head>
<body>
    <form id="form1" runat="server" style="height:100%;">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
          <table class="OperationBar" style="width:100%;">
             <tr>
             </tr>
          </table>          
       <table style="width:100%;" cellpadding="0" cellspacing="1" >
                    <tr>
                         <td style=" width:64px;">
                      <asp:DropDownList ID="ddl_Field" runat="server">
                               <asp:ListItem Selected="True" Value="DELIVER_LINE_NAME">线路名称</asp:ListItem>
                               <asp:ListItem Value="DIST_STA_NAME">区域名称</asp:ListItem>
                               <asp:ListItem Value="ORDER_DATE">订单时间</asp:ListItem>
                           </asp:DropDownList>
                           
                     </td>
                     <td style="width: 150px"> <asp:TextBox ID="txtKeyWords" runat="server" CssClass="TextBox" Width="151px"></asp:TextBox></td>
                     <td><input id="Button1" type="button" value="" onclick="setday(txtKeyWords)" class="ButtonDate" /></td>
                     <td style="width: 45px"><asp:Button ID="btnQuery" runat="server" Text="查询"  CssClass="ButtonQuery" OnClick="btnQuery_Click"/>&nbsp;
                     </td>
                     <td class="tdTitle" style="width: 80px; height: 29px">
                          分拣线编码</td>
                       <td style="height: 29px; width: 100px;" >
                            <table  cellpadding="0" cellspacing="0">
                              <tr><td style="height: 24px"><asp:TextBox ID="txtSortingName" runat="server" CssClass="myinput" TabIndex="1" ></asp:TextBox></td>
                                  <td style="height: 24px"><asp:TextBox ID="txtSortingCode" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                                  <td style="height: 24px"><input id="Button2" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtSortingCode,txtSortingName','DWV_DPS_SORTING','SORTING_CODE,SORTING_NAME','ISACTIVE','1');" /></td>
                              </tr>
                            </table>
                       </td> 
                     <td align="left">
                     <asp:Button ID="btnCreate" runat="server" Text="确认分配" CssClass="ButtonAudit" OnClick="btnCreate_Click"  />
                     <asp:Button ID="BtnClose" runat="server" Text="取消分配" CssClass="ButtonAudit2" OnClick="btnClose_Click"  />
                     </td>
                     <td><asp:Button ID="btnCollect" runat="server" Text="分拣线汇总"  OnClick="btnCollect_Click"  /></td>
                      <td align="right"><asp:Button ID="btnDown" runat="server" CssClass="ButtonDown" Text="线路下载" OnClick="btnDown_Click" />
                      <asp:Button ID="btnExit" runat="server" Text="退出"  OnClientClick="Exit();" CssClass="ButtonExit" OnClick="btnExit_Click" />&nbsp;</td>
                    </tr>
                  </table>
       <div style=" height:80%;" class="edit_detail_body">
           <asp:DataGrid ID="dgDetail" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  EnableViewState="False" OnItemDataBound="dgDetail_ItemDataBound">
               <Columns>
                    <asp:TemplateColumn > 
                        <ItemTemplate>
                         <asp:CheckBox ID="chkExport" Runat="server" OnCheckedChanged="chkExport_CheckedChanged" AutoPostBack="True" />                       
                       </ItemTemplate>
                      
                   </asp:TemplateColumn>
                   <asp:BoundColumn DataField="DELIVER_LINE_CODE" HeaderText="送货线路编码">
                       <HeaderStyle Width="70px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="DELIVER_LINE_NAME" HeaderText="送货线路名称">
                       <HeaderStyle Width="120px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="DIST_STA_NAME" HeaderText="送货区域名称">
                    <HeaderStyle Width="120px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                        <asp:BoundColumn DataField="ORDER_DATE" HeaderText="订单日期">
                       <HeaderStyle Width="60px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="ORDERQUANTITY" HeaderText="订单明细数">
                       <HeaderStyle Width="100px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                    <asp:BoundColumn DataField="QUANTITY" HeaderText="总数量">
                       <HeaderStyle Width="100px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="AMOUNT" HeaderText="总金额">
                    <HeaderStyle Width="100px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="ISALLOTS" HeaderText="是否分配">
                    <HeaderStyle Width="100px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                    <asp:BoundColumn DataField="SORTING_NAME" HeaderText="分拣线名称">
                       <HeaderStyle Width="120px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
               </Columns>
               <ItemStyle BackColor="White" Height="22px" />
               <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
           </asp:DataGrid>
           <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px"></asp:Label>&nbsp;
       </div>
          <%--
              <asp:TemplateColumn > 
                        <ItemTemplate>
                           <input type="checkbox" id="chkExport" runat="server" onclick="return Add('chkExport')"/>                  
                       </ItemTemplate>
                   </asp:TemplateColumn>
           <asp:Button ID="btnWei" runat="server" CssClass="ButtonQuery" Text="未分配线路" OnClick="btnWei_Click" />OnCheckedChanged="chkExport_CheckedChanged"
          <asp:Button ID="ButYi" runat="server" Text="已分配线路" CssClass="ButtonQuery"  OnClick="btnYi_Click" />
       <div Style=" text-align:right; height: 1px;" class="edit_detail_pager"><!--分页导航--><asp:CheckBox ID="chkExport" Runat="server"  OnCheckedChanged="return Add('chkExport')" AutoPostBack="True" />    
          <NetPager:AspNetPager ID="pager" Width="450" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
       </div><asp:Button ID="BtnDelete" runat="server" Text="删除分配" CssClass="ButtonDel" Visible="false" OnClick="btnDelete_Click"  />
       <asp:Label ID="lblQuantitySum" runat="server" Text=""  Visible="TRUE" > </asp:Label>--%>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>  
        <table style="width:100%;" cellpadding="0" cellspacing="1">
              <tr>
             <td style="width:55%;"></td>
             <td class="tdTitle" style="width: 66px; height: 29px; text-align:right" align="right">
              选中数量：</td><td><asp:Label ID="lblQuantitySum" runat="server" Text=""  Visible="TRUE" ></asp:Label></td>
             <td class="tdTitle" style="width: 70px; height: 29px; text-align:right" align="right">
              总数量：</td><td><asp:Label ID="LblQuantity" runat="server" Text="总数量"  Visible="TRUE" ></asp:Label></td>
              <td class="tdTitle" style="width: 66px; height: 29px; text-align:right" align="right">
              总金额：</td><td><asp:Label ID="LBlAmount" runat="server" Text="总金额"  Visible="TRUE" ></asp:Label>
              &nbsp;&nbsp;&nbsp;</td>
             </tr>
          </table>   
       </ContentTemplate>
       </asp:UpdatePanel> 
       <div>
           <asp:HiddenField ID="hdnOpFlag" runat="server" Value="0" />
       </div>
       
    </form>
       
</body>
</html>