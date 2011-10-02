<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchAssignedProduct.aspx.cs" Inherits="Code_BasicInfo_BatchAssignedProduct" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>为指定卷烟批量分配货位</title>
    <base target="_self" />
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <script type="text/javascript" src="../../JScript/SelectDialog.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
    .SideBar
    {
       
       padding-top:5px;
       vertical-align:top; 
       width:184px; 
       padding-right:4px;
    }
    .topic
    {
       padding-top:10px;
    }
    .topic2
    {
       text-align:center; 
       padding-top:3px;
       height:25px; 
       width:72px; 
       background-image:url(../../images/topic.gif);
       background-repeat:no-repeat;
    }
    </style>
<script type="text/javascript">
function ReturnValue(fieldValue){
   if (window.document.all)//IE判断window.showModalDialog!=null
   {
     window.returnValue=fieldValue;
     window.opener=null;
     window.close();
   }
   else
   {
       var aryValue=new Array();
       aryValue=fieldValue.split('|');
       var aryTarget=new Array();
       aryTarget=document.getElementById('hideTargetControls').value.split(',');
       for(var i=0;i<aryTarget.length;i++)
       {
          var e=parent.opener.document.getElementById(aryTarget[i]);
          if(e!=null)
          {
            e.value=aryValue[i];
          }
       }  
       window.close();      
   }
}
function window_onunload() {
//对于Firefox需要进行返回值的额外逻辑处理
    if(!window.document.all)//FireFox
    {
       window.opener.myAction.returnAction(window.returnValue)
    }
}
function back()
{
        location.href="WarehouseCellEditPage.aspx";
}
</script>  
</head>
<body style="background-image:url(../);">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
               </ContentTemplate>
        </asp:UpdatePanel>  
  
    <table style="width:100%; background-color:WHITE;" cellpadding="0" cellspacing="0">
       <tr>
          <td style=" background-color:#e4e4e4; width: 220px;" class="SideBar"><!--仓库架构-->
            <div style="overflow-x:auto; overflow-y:auto; width:250px; height:400px;">
              <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged" AllowCascadeCheckbox="True" ShowCheckBoxes="All" Width="123px">
                  <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
              </yyc:smarttreeview>
            </div>
          </td>
          
          <td style="vertical-align:top; padding-left:10px;"> <!--编辑区-->
            <div style="width:100%; height:380px; overflow-x:auto; overflow-y:auto;">
                &nbsp;<fieldset style="width: 250px">&nbsp;&nbsp;&nbsp;
                    <legend> 货位 </legend>
                    <table>
                        <tr>
                            <td colspan="4" style="height: 7px">
                                <asp:TextBox ID="txtCELLID" runat="server" CssClass="HiddenControl"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="tdTitle" style="height: 48px">
                                <asp:TextBox ID="txtAssignedProductCode" runat="server" CssClass="HiddenControl"
                                    Height="0"></asp:TextBox>
                                指定卷烟</td>
                            <td style="width: 207px; height: 48px;">
                                <asp:TextBox ID="txtAssignedProductName" runat="server" CssClass="TextBox" Width="112px"></asp:TextBox>
                                <input id="Button1" class="ButtonBrowse" onclick="SelectDialog2('txtAssignedProductCode,txtAssignedProductName','WMS_PRODUCT','PRODUCTCODE,PRODUCTNAME');"
                                    type="button" value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdTitle" style="height: 31px">
                                <font color="red">*</font>货位单位</td>
                            <td style="width: 207px; height: 31px;">
                                <asp:TextBox ID="txtUnitCode" runat="server" CssClass="TextBox" Width="66px"></asp:TextBox>
                                &nbsp; <asp:TextBox
                                    ID="txtUnitName" runat="server" CssClass="TextBox" onfocus="CannotEdit(this)"
                                    Width="29px"></asp:TextBox>
                                <input id="Button2" class="ButtonBrowse" onclick="SelectDialog2('txtUnitCode,txtUnitName','WMS_UNIT','UNITCODE,UNITNAME');"
                                    type="button" value="..." />
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                              <td class="tdTitle" style="height: 48px">
                                是否启用</td>
                              <td style="height: 48px">
                                <asp:DropDownList ID="ddlActive" runat="server">
                                    <asp:ListItem Selected="True" Value="1">启用</asp:ListItem>
                                    <asp:ListItem Value="0">未启用</asp:ListItem>
                                </asp:DropDownList></td>  
                        </tr>
                        <tr>
                                <td class="tdTitle" style="height: 31px">
                                虚拟货位</td>
                            <td style="height: 31px">
                                <asp:DropDownList ID="ddlVirtual" runat="server">
                                    <asp:ListItem Selected="True" Value="0">不是</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 35px; text-align: center">
                                &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                                    OnClientClick="return CheckBeforeSubmit()" Text="保存" />
                                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                <asp:Button ID="btnCancel"
                                            runat="server" CssClass="button" OnClick="btnCancel_Click" Text="关闭" Visible="true" /></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
                <script type="text/javascript">
function CheckBeforeSubmit()
{
    //var cellname=document.getElementById('txtCellName').value;//document.getElementById('txtTitle').value.trim();
    var unitcode=document.getElementById('txtUnitCode').value;
     var productname=document.getElementById('txtAssignedProductName').value;
     if(productname=="")
     {
        document.getElementById('txtAssignedProductCode').value="";
     }
    if(unitcode=="")
    {
       alert('货位单位不能为空！');
       return false;
    }
}

function clear(id)
{
   alert(id)
   document.getElementById(id).value="";
}
                </script>
          </td>
       </tr>
    </table>
    

 <div>
     <asp:HiddenField ID="hideScrollTop" runat="server" />
     <asp:HiddenField ID="hdnWarehouseCode" runat="server" />
     <asp:HiddenField ID="hdnAreaCode" runat="server" />
     <asp:HiddenField ID="hdnShelfCode" runat="server" />
     <asp:HiddenField ID="hdnDepth" runat="server" Value="-1" />
     <asp:HiddenField ID="hdnRemovePath" runat="server" Value="/" />
 </div>  

    </form>
<script type="text/javascript">

function KeepSilent()
{
  return;
}
</script>    
</body>
</html>
