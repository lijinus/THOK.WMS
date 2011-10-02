<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveBillEditPage.aspx.cs" Inherits="Code_StorageManagement_MoveBillEditPage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>移位单编辑</title>
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
          <table class="OperationBar" style="width:100%;">
             <tr>
               <td style="width:500px;"></td>
               <td style=" white-space:nowrap; text-align:right;">
                 <asp:Button ID="btnCreate" runat="server" Text=" 新增" CssClass="ButtonCreate" OnClick="btnCreate_Click"  />&nbsp;
                 <asp:Button ID="btnSave" runat="server" Text=" 保存汇总" CssClass="ButtonSave" OnClick="btnSave_Click"  />
                 <asp:Button ID="btnValidate" runat="server" Text=" 审核" CssClass="ButtonAudit" Enabled="False" OnClick="btnValidate_Click"  />
                 <asp:Button ID="btnReverseValidate" runat="server" Text=" 反审" CssClass="ButtonAudit2" Enabled="False" OnClick="btnReverseValidate_Click"  />
                 <asp:Button ID="btnBack" runat="server" Text=" 返回" CssClass="ButtonBack" Visible="true" OnClick="btnBack_Click" OnClientClick="Back();" />
                 <asp:Button ID="Button3" runat="server" Text=" 图表" CssClass="ButtonChart" Visible="true"    OnClientClick="Chart();" OnClick="Button3_Click" />
               </td>
             </tr>
          </table>
          
          
       <table style="width:100%; " cellpadding="0" cellspacing="1" >
                    <tr style="height:6px;"><td colspan="6">&nbsp;</td></tr>   
                    <tr>
                      <td class="tdTitle">
                          移位单号</td>
                      <td ><asp:TextBox ID="txtBillNo" runat="server" CssClass="myinput"  onfocus="CannotEdit(this)"></asp:TextBox></td>

                      <td class="tdTitle">
                          制单日期</td>
                      <td><asp:TextBox ID="txtBillDate" runat="server" CssClass="myinput" ></asp:TextBox></td>
                      <td class="tdTitle">单据类型</td>
                      <td >
                            <table  cellpadding="0" cellspacing="0">
                              <tr><td><asp:TextBox ID="txtBillTypeName" runat="server" CssClass="myinput" TabIndex="1" ></asp:TextBox></td>
                                  <td><asp:TextBox ID="txtBillTypeCode" runat="server" Width="0" Height="0" BorderWidth="0"></asp:TextBox></td>
                                  <td><input id="Button2" class="ButtonBrowse2" type="button" value="" onclick="SelectDialog2('txtBillTypeCode,txtBillTypeName','WMS_BILLTYPE','TYPECODE,TYPENAME','BUSINESS','3');" /></td>
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

                        <td class="tdTitle">移位仓库</td>
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
                      <td class="tdTitle">总数量</td>
                      <td><asp:TextBox ID="txtTotalQty" runat="server" CssClass="myinput_right" Enabled="False"></asp:TextBox></td>                                            
                    </tr> 
                    <tr>
                      <td class="tdTitle">备注</td><td><asp:TextBox ID="txtMemo" runat="server" CssClass="myinput" TabIndex="3"  ></asp:TextBox></td>
                      <td class="tdTitle"></td><td></td>
                      <td class="tdTitle"></td>
                      <td><asp:TextBox ID="txtTotalAmount" runat="server" CssClass="myinput_right" Enabled="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr style="height:6px;"><td colspan="6">&nbsp;</td></tr>                    
                  </table>
                       
       <div  class="edit_detail_header">
         <table cellpadding="0"  cellspacing="0">
            <tr>
               <td colspan="3" style="padding-left:1em; padding-top:5px; height:30px;">
                   <asp:Button ID="btnCreateDetail" runat="server" Text="新增明细" CssClass="ButtonCreate"  OnClick="btnCreateDetail_Click"/>
                   <asp:Button ID="btnSaveDetail" runat="server" CssClass="ButtonSave" Text="保存明细" OnClick="btnSaveDetail_Click"  />
                   <asp:Button ID="btnDeleteDetail" runat="server" Text="删除明细" CssClass="ButtonDel" OnClick="btnDeleteDetail_Click"  OnClientClick="return DelConfirm('btnDeleteDetail')" />
               </td>
            </tr>
         </table>
       </div>   
       
       <div  class="edit_detail_body">
                  <asp:DataGrid ID="dgDetail" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgDetail_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify" OnPreRender="dgDetail_PreRender">
                       <Columns>
                           <asp:TemplateColumn HeaderText="序号">
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                               <HeaderStyle Width="25px" />
                           </asp:TemplateColumn>
                           <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                               <HeaderStyle Width="20px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="OUT_CELLCODE" HeaderText="货位编码(出)">
                               <HeaderStyle Width="100px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="OUT_CELLNAME" HeaderText="货位名称(出)">
                               <HeaderStyle Width="70px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="IN_CELLCODE" HeaderText="货位编码(入)">
                               <HeaderStyle Width="100px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="IN_CELLNAME" HeaderText="货位名称(入)">
                               <HeaderStyle Width="70px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                               <HeaderStyle Width="80px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                               <HeaderStyle Width="60px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Center" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                               <HeaderStyle Width="60px" />
                           </asp:BoundColumn>
                           <asp:BoundColumn DataField="QUANTITY" HeaderText="数量">
                               <HeaderStyle Width="60px" />
                               <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                   Font-Underline="False" HorizontalAlign="Right" />
                           </asp:BoundColumn>
                       </Columns>
                       <ItemStyle BackColor="AliceBlue" Height="26px" />
                       <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                       <AlternatingItemStyle BackColor="White" />
                   </asp:DataGrid>
           <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>无记录" style="width:100%; text-align:center;" Visible="False" Height="100px" ></asp:Label>
       </div>
          <!--分页导航-->
      <div Style=" text-align:right;" class="edit_detail_pager">
               <NetPager:AspNetPager ID="pager" Width="450" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true" SubmitButtonStyle="margin-top:0px;border:solid 0px gray; background-color:transparent; cursor:hand;"></NetPager:AspNetPager>
       </div>
       
       
       <div>
           <asp:HiddenField ID="hdnOpFlag" runat="server" Value="0" />
       </div>
    </form>
<script>

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
        alert('请选择要删除的移位明细！');
        return false;
     }
      if(confirm('确定要删除选择的移位明细？','删除提示'))
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
   window.open("MoveBillPage.aspx?t="+time,"_self");
}

function Chart()
{
    var date=new Date();
    var time=date.getMilliseconds();
   window.location="MoveBillChartPage.aspx";
}
function SelectCell(strTarget)
{
   var date=new Date();
   var time=date.getMilliseconds();

   if (window.document.all)//IE判断window.showModalDialog!=null
   {
      var returnvalue=window.showModalDialog("SelectCellDialog.aspx?time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=750px;dialogHeight=450px");
       
       if(returnvalue==null)
       {
          return false;
       }
       else if(returnvalue!='')
       {
           var aryValue=new Array();
           aryValue=returnvalue.split('|');
           var aryTarget=strTarget.split(',');
           for(var i=0;i<aryTarget.length;i++)
           {
              var e=document.getElementById(aryTarget[i]);
              if(e!=null)
              {
                e.value=aryValue[i];
              }
           }  
           return false; 
       } 
   }
   else
   {
        //参数
        var strPara = "height=450px;width=500px;help=off;resizable=off;scroll=no;status=off;modal=yes;dialog=yes";
        //打开窗口
        var url="SelectCellDialog.aspx?time="+time+"&targetControls="+strTarget;
        var DialogWin = window.open(url,"myOpen",strPara,true);
   } 
                                   
}
function SelectCell2(strTarget,entryFliter,outFliter)
{
   var date=new Date();
   var time=date.getMilliseconds();
   if(entryFliter==null)
   {
        entryFliter="";
   }
   if(outFliter==null)
   {
        outFliter="";
   }

   if (window.document.all)//IE判断window.showModalDialog!=null
   {
      var returnvalue=window.showModalDialog("SelectCellDialog.aspx?entryFliter="+entryFliter+"&outFliter="+outFliter+"&time="+time,"",      
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=750px;dialogHeight=450px");
       
       if(returnvalue==null)
       {
          return false;
       }
       else if(returnvalue!='')
       {
           var aryValue=new Array();
           aryValue=returnvalue.split('|');
           var aryTarget=strTarget.split(',');
           for(var i=0;i<aryTarget.length;i++)
           {
              var e=document.getElementById(aryTarget[i]);
              if(e!=null)
              {
                e.value=aryValue[i];
              }
           }  
           return false; 
       } 
   }
   else
   {
        //参数
        var strPara = "height=450px;width=500px;help=off;resizable=off;scroll=no;status=off;modal=yes;dialog=yes";
        //打开窗口
        var url="SelectCellDialog.aspx?time="+time+"&targetControls="+strTarget;
        var DialogWin = window.open(url,"myOpen",strPara,true);
   } 
                                   
}
</script> 
</body>
</html>