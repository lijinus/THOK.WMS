<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyBalancePage.aspx.cs" Inherits="Code_StorageManagement_DailyBalancePage" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>产品日结</title>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
    <script src="../../JScript/Calendar.js"></script>
    <script language="JavaScript" type="text/javascript" src="../../JScript/ajax.js"></script>
</head>
  <script language="JavaScript" type="text/javascript">
        var ajaxOBJSubmit = new CallBackObject(OnComplete);
        window.setInterval("stateRequest()", 500);
        var iStep = 7;
        var post = false;
                
        function initImg() {
            for (var i = 1; i <= iStep; i++)
            {
                var div = document.getElementById("img" + i);
                div.style.width = 0;
            }
        }

        function completeImg(step)
        {
            for (var i = 1; i < step; i++)
            {
                var div = document.getElementById("img1");
                if (div.style.width != 290)
                    div.style.width = 290;
            }
        }
                                
        function OnComplete(text, xml)
        {
            var status = xml.getElementsByTagName("status")[0].text;
            if (status == "PROCESSING")
            {
                var step = xml.getElementsByTagName("step")[0].text;
                var div = document.getElementById("img" + step);
                var completeCount = xml.getElementsByTagName("completecount")[0].text;
                var totalCount = xml.getElementsByTagName("totalcount")[0].text;
                
                completeImg(step);
                div.style.width = 290 / totalCount * completeCount;
            }
            else if (status == "ERROR")
            {
                post = false;
                var message = xml.getElementsByTagName("message")[0].text;
                alert(message);
            }
            else if (status == "CONTINUE")
            {
                post = false;
                completeImg(iStep + 1);                
              
                alert("数据上报完成！");
                window.location.href='DailyBalancePage.aspx'; 
            }
            else if (status == "SwitchView")
            {
                post = false;
                document.getElementById("lbtnSwitch").click();
            }
        }
        
        function stateRequest() {
            if (post)
                ajaxOBJSubmit.PostData("OptimizeState.aspx", "Operator=State");
        }
        
    </script>
<body>
    <form id="form1" runat="server">    
    <asp:Panel ID="pnlList" runat="server" Height="520" Width="100%" Visible="true">
    <table class="OperationBar" cellpadding="0" cellspacing="0">
       <tr>
         <td>
            <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                    &nbsp; 仓库
                    <asp:DropDownList ID="ddlWarehouse" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp; &nbsp;
                    日结日期<asp:TextBox ID="txtDate" runat="server" Width="75px"></asp:TextBox>
                    <input id="Button1" type="button"  class="ButtonDate" onclick="setday(txtDate)" />
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" Visible="False" />
                </td>
                <td>
                    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnExecBalance" runat="server" Text="日结" OnClick="btnExecBalance_Click" />
                     <asp:Button ID="BtnZhongyan" runat="server" Text="上报" OnClick="btnZhangYan_Click" />
                     <asp:Label ID="labShow" runat="server" ForeColor="Red"></asp:Label>
                </td>
              </tr>
            </table>
         </td>
       </tr>
    </table>
    <div id="divbill" style="overflow-x:auto;overflow-y:auto; height:480px; width:100%">
     <asp:GridView ID="gvList" runat="server" Style="position: relative;table-layout:fixed;width:100%;"
              CssClass="GridStyle" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False"  OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" OnRowDeleting="gvList_RowDeleting">
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:CommandField EditText="日结明细" SelectText="日结明细" ShowCancelButton="False" ShowEditButton="True" CancelText="" DeleteText="" HeaderText="操作">
                      <HeaderStyle Width="120px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:CommandField>
                  <asp:CommandField DeleteText="重新日结" ShowCancelButton="False" ShowDeleteButton="True">
                      <HeaderStyle Width="60px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:CommandField>
                  <asp:BoundField DataField="SETTLEDATE" DataFormatString="{0:yyyy-MM-dd}" HeaderText="日结日期"
                      HtmlEncode="False">
                      <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle Width="90px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BEGINNING" HeaderText="期初量" DataFormatString="{0:N2}" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENTRYAMOUNT" HeaderText="入库总量" DataFormatString="{0:N2}" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="DELIVERYAMOUNT" HeaderText="出库总量" DataFormatString="{0:N2}" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PROFITAMOUNT" HeaderText="报损总量" DataFormatString="{0:N2}" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LOSSAMOUNT" HeaderText="报益总量" DataFormatString="{0:N2}" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENDING" HeaderText="日结量" DataFormatString="{0:N2}" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
              </Columns>
          </asp:GridView>
  
    </div>
    <table id="paging" cellpadding="0" cellspacing="0" style="width:500px;">
       <tr>
         <td>
           <NetPager:AspNetPager ID="pager" runat="server" OnPageChanging="pager_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
         </td>
       </tr>
      </table>         
    </asp:Panel>
      <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Visible="False">
         <table cellpadding="1" cellspacing="5" style="border-top: lightgrey 1px solid; width: 460px; height: 40px; border-right: lightgrey 1px solid; border-left: lightgrey 1px solid;"  align="center">
                <tr style="display: block; width: 100%">
                    <td style="width: 480px; height: 46px;">
                        <table cellspacing="3" style="width: 100%;">
                           <tr style="display: block; width: 100%">
                                <td colspan="4" style="padding-left: 3em; height: 18px">数据上报</td>
                            </tr>
                            <tr style="color: #333300">
                                <td style="width: 60px; height: 33px;"></td>
                                <td style="width: 290px; height: 33px;">
                                    <div id="div5" style="border-right: lavender 1px solid; border-top: lavender 1px solid; left: 332px;
                                                border-left: lavender 1px solid; width: 289px; border-bottom: lavender 1px solid; top: 97px; height: 5px">
                                        <img id="img1" style="background-image: url(../../images/process/process_bar.gif); width: 1px; height: 14px; left: 1px;"/></div>
                                </td>
                                <td style="width: 60px; height: 33px;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
             <table cellpadding="1" cellspacing="5" style="border-top: lightgrey 1px solid; width: 460px;"  align="center">
                <tr style="display: block; width: 100%">
                    <td style="width: 480px; height: 46px;">
                        <table cellspacing="3" style="width: 100%;">
                           <tr style="display: block; width: 100%">
                                <td colspan="4" style="padding-left: 3em; height: 18px"></td>
                            </tr>
                            <tr style="color: #333300">
                                <td style="width: 60px; height: 33px;"></td>
                                <td style="width: 290px; height: 33px;">
                                    <div id="div1" style=" width: 289px;">
                                        </div>
                                </td>
                                <td style="width: 60px; height: 33px;"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
  </asp:Panel>
 <asp:Panel ID="pnlBalance" runat="server" Height="520px" Width="100%" Visible="false">
    <table class="OperationBar" cellpadding="0" cellspacing="0">
       <tr>
         <td>&nbsp;&nbsp;仓库：<asp:Label ID="lblHouse" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;
              日结日期：<asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
             &nbsp; &nbsp;&nbsp;
              <asp:Button ID="btnBack" runat="server" CssClass="ButtonBack" Text="返回" OnClick="btnBack_Click" />
         </td>
       </tr>
    </table>
    <div style="overflow-x:auto; overflow-y:auto; height:480px; width:100%">
     <asp:GridView ID="gvBalance" runat="server" Style="position: relative;table-layout:fixed;width:100%;"
              CssClass="GridStyle" CellPadding="3" CellSpacing="1" BorderWidth="0px" AutoGenerateColumns="False" OnRowDataBound="gvBalance_RowDataBound">
              <RowStyle BackColor="AliceBlue" Height="28px" />
              <HeaderStyle CssClass="GridHeader2" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="PRODUCTCODE" HeaderText="产品代码" >
                      <HeaderStyle Width="70px" />
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PRODUCTNAME" HeaderText="产品名称" >
                      <HeaderStyle Width="130px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="BEGINNING" HeaderText="期初量" DataFormatString="{0:N2}" HtmlEncode="False" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENTRYAMOUNT" HeaderText="入库总量" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="DELIVERYAMOUNT" HeaderText="出库总量" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="PROFITAMOUNT" HeaderText="报损总量" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="LOSSAMOUNT" HeaderText="报益总量" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ENDING" HeaderText="日结量" DataFormatString="{0:N2}" >
                      <ItemStyle HorizontalAlign="Right" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITCODE" HeaderText="单位编码" >
                      <ItemStyle HorizontalAlign="Center" />
                  </asp:BoundField>
                  <asp:BoundField DataField="UNITNAME" HeaderText="单位名称" />
              </Columns>
          </asp:GridView>
    </div>
    <table id="Table1" cellpadding="0" cellspacing="0" style="width:500px;">
       <tr>
         <td>
           <NetPager:AspNetPager ID="pager2" runat="server" OnPageChanging="pager2_PageChanging" ShowPageIndex="false" ShowInputBox="Always" AlwaysShow="true"></NetPager:AspNetPager>
         </td>
       </tr>
      </table> 
    
</asp:Panel>

    </form>
</body>
</html>
