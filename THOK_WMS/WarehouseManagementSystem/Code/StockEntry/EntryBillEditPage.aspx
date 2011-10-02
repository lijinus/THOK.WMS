<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryBillEditPage.aspx.cs" Inherits="Code_StockEntry_EntryBillEdit" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>入库编辑</title>
    <link href="../../css/css.css?t=9" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    
<script>
function OpenNew()
{
    if(document.getElementById("hdnOpFlag").value=="0")
    {
//       alert("请先保存入库单汇总信息");
    }
    else
    {
        var date=new Date();
        var time=date.getMilliseconds();
        var BillNo=document.getElementById('txtBillNo').value;
        var unitcode=document.getElementById('lblUnitCode').value;
        var unitname=document.getElementById('lblUnitName').value;
        // alert(unitname);
        window.showModalDialog("EntryBillDetailEditPage.aspx?BILLNO="+BillNo+"&UNITNAME="+unitname+"&UNITCODE="+unitcode+"&time="+time,"",
                                             "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=310px;dialogHeight=450px");
                                             
        var billno=document.getElementById('txtBillNo').value;
        window.location.href='EntryBillEditPage.aspx?BILLNO='+billno;                                             
       
   }
}

function OpenEdit(id)
{
    var date=new Date();
    var time=date.getMilliseconds();
    //var BillNo=document.getElementById('txtBillNo').value;
    //alert(id);
    window.showModalDialog("EntryBillDetailEditPage.aspx?ID="+id+"&time="+time,"",
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
        alert('请选择要删除的入库明细！');
        return false;
     }
      if(confirm('确定要删除选择的入库明细？','删除提示'))
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

function Back()
{
    var date=new Date();
    var time=date.getMilliseconds();
   window.open("EntryBillPage.aspx?t="+time,"_self");
}
</script> 
</head>
<body>
    <form id="form1" runat="server">
          <table class="OperationBar" style="width:100%;">
             <tr>
               <td style="width:500px;"></td>
               <td style=" white-space:nowrap; text-align:right;">
                 
                 <asp:Button ID="btnSave" runat="server" Text=" 保存汇总" CssClass="ButtonSave" OnClick="btnSave_Click"  />
                 <%--<asp:Button ID="btnValidate" runat="server" Text=" 审核" CssClass="ButtonAudit" Enabled="False" OnClick="btnValidate_Click"  />
                 <asp:Button ID="btnReverseValidate" runat="server" Text=" 反审" CssClass="ButtonAudit2" Enabled="False" OnClick="btnReverseValidate_Click"  />
                 <asp:Button ID="btnCreate" runat="server" Text=" 新增" CssClass="ButtonCreate" OnClick="btnCreate_Click"  />&nbsp;
                 --%>
                 <asp:Button ID="btnBack" runat="server" Text=" 返回" CssClass="ButtonBack" Visible="true" OnClick="btnBack_Click" OnClientClick="Back();" />
               </td>
             </tr>
          </table>
          
          
       <table style="width:100%; " cellpadding="0" cellspacing="1" >
                    <tr style="height:6px;"><td colspan="6">&nbsp;</td></tr>   
                    <tr>
                      <td class="tdTitle">
                          入库单号</td>
                      <td ><asp:TextBox ID="txtBillNo" runat="server" CssClass="myinput"  onfocus="CannotEdit(this)"></asp:TextBox></td>

                      <td class="tdTitle">
                          制单日期</td>
                      <td><asp:TextBox ID="txtBillDate" runat="server" CssClass="myinput" ></asp:TextBox></td>
                      <td class="tdTitle">单据类型</td>
                      <td >
                            <table  cellpadding="0" cellspacing="0">
                              <tr><td style="height: 24px"><asp:TextBox ID="txtBillTypeName" runat="server" CssClass="myinput" TabIndex="1" ></asp:TextBox></td>
                                  <td style="height: 24px"><asp:TextBox ID="txtBillTypeCode" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                                  <td style="height: 24px"><input id="Button2" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtBillTypeCode,txtBillTypeName','WMS_BILLTYPE','TYPECODE,TYPENAME','BUSINESS','1');" /></td>
                              </tr>
                            </table>
                       </td>                      
                    </tr>
                    <tr>
 
                    <tr>
                      <td class="tdTitle">操作员</td>
                      <td ><asp:TextBox ID="txtEmployeeName" runat="server" CssClass="myinput" onfocus="this.blur()"></asp:TextBox>
                            <asp:TextBox ID="txtOperatePerson" runat="server" CssClass="HiddenControl"></asp:TextBox></td> 
                      <td class="tdTitle">
                          审 核 人</td>
                      <td><asp:TextBox ID="txtValidatePerson" runat="server" CssClass="myinput" Enabled="False" ></asp:TextBox></td>

                        <td class="tdTitle">入库仓库</td>
                        <td>
                           <table cellpadding="0" cellspacing="0">
                              <tr>
                                <td><asp:TextBox ID="txtWHNAME" runat="server" CssClass="myinput" TabIndex="2"></asp:TextBox></td>
                                <td><input id="Button1" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtWHCODE,txtWHNAME,lblUnitCode,lblUnitName','WMS_WAREHOUSE','WH_CODE,WH_NAME,DEFAULTUNIT,UNITNAME');" /></td>
                                <td><asp:TextBox ID="txtWHCODE" runat="server" CssClass="HiddenControl"></asp:TextBox></td>
                                <td><asp:TextBox ID="lblUnitCode" runat="server" Text="" CssClass="HiddenControl"></asp:TextBox>
                                    <asp:TextBox ID="lblUnitName" runat="server" Text="" CssClass="HiddenControl"></asp:TextBox>
                                </td>
                              </tr>
                           </table>
                        </td>
                    </tr>
                    <tr>
                      <td class="tdTitle">处理状态</td>
                      <td >
                          <asp:DropDownList ID="ddlBillState" runat="server" Enabled="False">
                              <asp:ListItem Selected="True" Value="0">未录入</asp:ListItem>
                              <asp:ListItem Value="1">未审核</asp:ListItem>
                              <asp:ListItem Value="2">已审核</asp:ListItem>
                          </asp:DropDownList></td>

                      <td class="tdTitle">
                          审核日期</td>
                      <td><asp:TextBox ID="txtValidateDate" runat="server" CssClass="myinput" Enabled="False" ></asp:TextBox>
                          </td>
                      <td class="tdTitle">总金额</td>
                      <td><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="myinput_right" Enabled="False"></asp:TextBox></td>                                            
                    </tr> 
                    <tr>
                      <td class="tdTitle">备注</td><td><asp:TextBox ID="txtMemo" runat="server" CssClass="myinput" TabIndex="3"  ></asp:TextBox></td>
                      <td class="tdTitle"></td><td></td>
                      <td class="tdTitle">总数量</td>
                      <td><asp:TextBox ID="txtTotalQty" runat="server" CssClass="myinput_right" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr style="height:6px;"><td colspan="6">&nbsp;</td></tr>                    
                  </table>
        
<div style="margin:0 3px 0 2px;">  
                    
       <div  class="edit_detail_header">
         <table cellpadding="0"  cellspacing="0">
            <tr>
               <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                   <asp:Button ID="btnCreateDetail" runat="server" Text="新增明细" CssClass="ButtonCreate"  OnClientClick="return OpenNew()" OnClick="btnCreateDetail_Click"/>
                   <asp:Button ID="btnSaveDetail" runat="server" CssClass="ButtonSave" Text="保存明细" OnClick="btnSaveDetail_Click" Visible="False"  />
                   <asp:Button ID="btnDeleteDetail" runat="server" Text="删除明细" CssClass="ButtonDel" OnClick="btnDeleteDetail_Click"  OnClientClick="return DelConfirm('btnDeleteDetail')" />
               </td>
            </tr>
         </table>
       </div>   
       
       <div class="edit_detail_body">
           <asp:DataGrid ID="dgDetail" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  EnableViewState="False" OnItemDataBound="dgDetail_ItemDataBound">
               <Columns>
                   <asp:TemplateColumn>
                       <HeaderStyle Width="40px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:TemplateColumn>
                   <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                       <HeaderStyle Width="20px" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                       <HeaderStyle Width="70px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                       <HeaderStyle Width="150px" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                       <HeaderStyle Width="55px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Center" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                       <HeaderStyle Width="55px" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                       <HeaderStyle Width="55px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Right" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="INPUTQUANTITY" HeaderText="实际入库量">
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Right" />
                       <HeaderStyle Width="80px" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="PRICE" HeaderText="单价">
                       <HeaderStyle Width="60px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Right" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="TOTALAMOUNT" HeaderText="总金额">
                       <HeaderStyle Width="60px" />
                       <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                           Font-Underline="False" HorizontalAlign="Right" />
                   </asp:BoundColumn>
                   <asp:BoundColumn DataField="MEMO" HeaderText="备注"></asp:BoundColumn>
               </Columns>
               <ItemStyle BackColor="White" Height="22px" />
               <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
           </asp:DataGrid>
           <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px"></asp:Label>
       </div>
          
      <div class="edit_detail_pager"><!--分页导航-->
        <NetPager:AspNetPager  Width="450px"  ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
      </div>
</div>   
    
       <div>
           <asp:HiddenField ID="hdnOpFlag" runat="server" Value="0" />
       </div>
    </form>

</body>
</html>
