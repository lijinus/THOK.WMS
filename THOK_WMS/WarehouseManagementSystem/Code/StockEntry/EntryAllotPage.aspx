<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntryAllotPage.aspx.cs" Inherits="Code_StockEntry_EntryAllot" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>入库分配</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <style>
     .content_panel
     {
        width:100%;
        padding-left:1px;
        overflow:auto;
     }
     
     .step
     {
        width:100%;
        height:35px;
        padding-top:5px;
        padding-bottom:5px;
        padding-left:18px;
     }
         
    fieldset {
        padding:10px;
        margin-top:5px;
        border:1px solid #91abd6;
        background:#fff;
    }

    fieldset legend {
        color:#0d3373;
        font-weight:normal;
        padding:5px 20px 5px 20px;
        border:1px solid #91abd6;    
        background:#fff;
    }

    fieldset label {
        float:left;
        width:120px;
        text-align:right;
        padding:4px;
        margin:1px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
          <ContentTemplate>
          
      <table class="OperationBar2" style="width:100%;" cellpadding="0" cellspacing="0">
         <tr>
           <td style="width:460px;">
                   <table >
                       <tr>
                         <td class="tdTitle">
                             &nbsp; 制单日期</td>
                         <td><asp:TextBox ID="txtStartDate" runat="server" Width="80px"></asp:TextBox><input id="Button1" type="button" value="" onclick="setday(txtStartDate)" class="ButtonDate" />
                          至 <asp:TextBox ID="txtEndDate" runat="server" Width="80px"></asp:TextBox><input id="Button2" type="button" value="" onclick="setday(txtEndDate)" class="ButtonDate"/></td>
                         <td class="tdTitle">
                             &nbsp; 入库单号</td>
                         <td><asp:TextBox ID="txtBillNo" runat="server" Width="86px"></asp:TextBox></td>
                       </tr>
                       <tr>
                         <td class="tdTitle">
                             备注信息</td>
                         <td>
                             <asp:TextBox ID="txtMemo" runat="server" Width="225px"></asp:TextBox></td>
                         
                         <td class="tdTitle">入库类别</td>
                         <td>
                             <asp:DropDownList ID="ddlBillType" runat="server">
                                 <asp:ListItem Selected="True" Value="1">所有入库单</asp:ListItem>
                             </asp:DropDownList></td>
                       </tr>    
                   </table>
           </td>
           <td style=" white-space:nowrap;">
               &nbsp;<asp:Button ID="btnQuery" runat="server" Text=" 查询" CssClass="ButtonQuery" Visible="true" OnClick="btnQuery_Click" />
             <asp:Button ID="btnBack" runat="server" Text=" 退出" CssClass="ButtonExit" Visible="true" OnClick="btnBack_Click" />
             <asp:Button ID="btnLoadDetail" runat="server" Text="Button" OnClick="btnLoadDetail_Click" CssClass="HiddenControl" />
           </td>
           <td></td>
         </tr>
      </table>  
    
    <div id="div01" style="display:<%=div01display%>; width:100%;">
        <div class="step">
            <asp:Button ID="btnPrevious" runat="server" Text="  上一步" Enabled="false" CssClass="ButtonPreviousDisable" />
            <asp:Button ID="btnNext" runat="server" Text="  下一步" OnClick="btnNext_Click" CssClass="ButtonNext" />
        </div>
        <asp:Panel ID="pnlOne" runat="server"  CssClass="content_panel"  Width="100%" >
           
                 <asp:DataGrid ID="dgMaster" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None"  OnItemDataBound="dgMaster_ItemDataBound" Width="100%">
                     <Columns>
                         <asp:TemplateColumn>
                             <HeaderStyle Width="15px" />
                         </asp:TemplateColumn>
                         <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                             <HeaderStyle Width="20px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BILLDATE" HeaderText="单据日期" DataFormatString="{0:yyyy-MM-dd}">
                             <HeaderStyle Width="70px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BILLNO" HeaderText="入库单号">
                             <HeaderStyle Width="80px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="VALIDATEPERSON" HeaderText="审核人">
                             <HeaderStyle Width="70px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="VALIDATEDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审核日期">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                             <HeaderStyle Width="70px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="TOTALINPUTQUANTITY" HeaderText="实际总数量">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Right" />
                             <HeaderStyle Width="70px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BILLTYPE" HeaderText="入库类别">
                             <HeaderStyle Width="80px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="WH_NAME" HeaderText="仓库名称"></asp:BoundColumn>
                     </Columns>
                     <ItemStyle BackColor="White" Height="26px" />
                     <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" />
                 </asp:DataGrid>
                 <asp:Label ID="lblMsg" runat="server" Text="<br><br><br>没有要分配的入库单" style="width:100%; text-align:center;" Visible="False" Height="100px" BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        </asp:Panel>

    </div>
    <div id="div02" style="display:<%=div02display%>;width:100%;">
      <div class="step">
            <asp:Button ID="btnPrevious2" runat="server" Text="  上一步" OnClick="btnPrevious2_Click" CssClass="ButtonPrevious"/>
            <asp:Button ID="btnNext2" runat="server" Text="  下一步" OnClick="btnNext2_Click" CssClass="ButtonNext" />
      </div>
    <asp:Panel ID="pnlTwo" runat="server"  Width="100%" Height="300px"  style="overflow-y:auto;">
      <%-- <asp:Panel ID="pnlTwo" runat="server" CssClass="content_panel" Width="100%">--%>
                 <asp:DataGrid ID="dgDetail" runat="server" AutoGenerateColumns="False" CssClass="GridStyle"
                     CellPadding="3" CellSpacing="1" EnableViewState="False" GridLines="None"
                     OnItemDataBound="dgDetail_ItemDataBound" Width="100%">
                     <Columns>
                         <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                             <HeaderStyle Width="20px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="BILLNO" HeaderText="入库单号">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                             <HeaderStyle Width="100px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                             <HeaderStyle Width="70px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                             <HeaderStyle Width="150px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="INPUTQUANTITY" HeaderText="实际数量">
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                             <HeaderStyle Width="80px" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                             <HeaderStyle Width="60px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                         <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                             <HeaderStyle Width="60px" />
                             <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                 Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                         </asp:BoundColumn>
                     </Columns>
                     <ItemStyle BackColor="White" Height="26px" />
                     <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                 </asp:DataGrid>
        </asp:Panel>
    </div>
    <div>
      <asp:HiddenField  runat="server" ID="hdnRowIndex" Value="0"/>
    </div>
    
          </ContentTemplate>
        </asp:UpdatePanel>
    </form>
<script>
function selectRow(objGridName,index)
{
//////////  var table=document.getElementById(objGridName);
//////////  for(var i=1;i<table.rows.length;i++)       //table.rows[1].cells.length
//////////  {
//////////     table.rows[i].cells[0].innerHTML="";
//////////  }
//////////  table.rows[index+1].cells[0].innerHTML="<img src=../../images/arrow01.gif />";
//////////  document.getElementById('hdnRowIndex').value=index;
//////////  document.getElementById('btnLoadDetail').click();
  

//      document.getElementById('hdnRowIndex').value=index;
//      document.getElementById('hdnScrollTop').value=document.getElementById('gvMain').parentElement.parentElement.scrollTop;


}
</script>    
</body>
</html>
