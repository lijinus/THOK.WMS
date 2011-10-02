<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckBillByProduct.aspx.cs" Inherits="Code_StorageManagement_CheckBillByProduct" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>产品盘点</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <link href="../../css/css.css?t=98" rel="Stylesheet" type="text/css" />
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" />
</head>
<body style="margin:0 0 0 15;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
           

<table style="width:100%">
  <tr>
    <td align="center">

    <div id="div01" style="width:615px; border:solid 0px black; display:<%=div01display%>">
      <div style="height:30px; line-height:16pt; text-align:left;" class="edit_detail_header">
          <asp:RadioButton ID="rbCode" runat="server" Text="产品代码"  GroupName="ord" AutoPostBack="True" Checked="True" OnCheckedChanged="rbName_CheckedChanged"/>
          <asp:RadioButton ID="rbName" runat="server" Text="产品名称" GroupName="ord" AutoPostBack="True" OnCheckedChanged="rbName_CheckedChanged"/>
          &nbsp; <span style="color: #cc3300"> 请将要盘点的产品移到右栏，取消则移到左栏</span></div>
      <table style="width:100%;" cellpadding="1" cellspacing="2" class="edit_detail_body">
        <tr>
          <td valign="top" align="center" width="150px">
              <div style="width:250px; height:420px; overflow-x:auto; overflow-y:scroll; background-color:White; border:solid 1px WhiteSmoke;">
                  <asp:DataGrid ID="dgProduct" runat="server" AutoGenerateColumns="False" style="" Font-Size="10pt" OnItemDataBound="dgProduct_ItemDataBound">
                      <Columns>
                          <asp:TemplateColumn>
                              <HeaderStyle Width="10px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                              <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Center" BackColor="Gainsboro" />
                          </asp:TemplateColumn>
                          <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                              <HeaderStyle Width="70px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                              <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Center" />
                          </asp:BoundColumn>
                          <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                              <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Left" />
                              <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Center" Width="150px" />
                          </asp:BoundColumn>
                      </Columns>
                      <HeaderStyle BackColor="GradientInactiveCaption" Font-Size="10pt" ForeColor="White" Height="22px" />
                      <ItemStyle BackColor="WhiteSmoke" />
                      <AlternatingItemStyle BackColor="White" />
                  </asp:DataGrid>
              </div>
          </td>
          
          <td style="width: 80px" align="center">
              <asp:Button ID="btnAllToRight" runat="server" Text="全添加 >>|" OnClick="btnAllToRight_Click" Width="90px" /><br />
              <asp:Button ID="btnAllToLeft" runat="server" Text="|<< 全删除 " OnClick="btnAllToLeft_Click" Width="90px" /><br /><br />
              
              <asp:Button ID="btnToRight" runat="server" Text="添加 >>" Width="90px" OnClick="btnToRight_Click" /><br />
              <asp:Button ID="btnToLeft" runat="server" Text="<< 删除" Width="90px" OnClick="btnToLeft_Click" /><br />
              <br />
         </td>
            
          <td valign="top" align="center">
              <div style="width:250px; height:420px; overflow-x:auto; overflow-y:scroll; background-color:White;border:solid 1px WhiteSmoke;">
                  
                  <asp:DataGrid ID="dgSelected" runat="server" AutoGenerateColumns="False" style="" Font-Size="10pt" OnItemDataBound="dgSelected_ItemDataBound">
                      <Columns>
                          <asp:TemplateColumn>
                              <HeaderStyle Width="10px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                              <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Center" BackColor="Gainsboro" />
                          </asp:TemplateColumn>
                          <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码">
                              <HeaderStyle Width="70px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                              <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Center" />
                          </asp:BoundColumn>
                          <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称">
                              <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Left" />
                              <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                  Font-Underline="False" HorizontalAlign="Center" Width="150px" />
                          </asp:BoundColumn>
                      </Columns>
                      <HeaderStyle BackColor="GradientInactiveCaption" Font-Size="10pt" ForeColor="White" Height="22px" />
                      <ItemStyle BackColor="WhiteSmoke" />
                      <AlternatingItemStyle BackColor="White" />
                  </asp:DataGrid>
              </div>
          </td>
        </tr>
      </table> 
      <table style="width:100%" class="edit_detail_pager">
         <tr>
           <td  style="height:38px; line-height:16pt;">
              <asp:Button ID="btnPrevious1" runat="server" Text="上一步" Enabled="False" />
              <asp:Button ID="btnNext1" runat="server" Text="下一步" OnClick="btnNext1_Click" />
           </td>
         </tr>
      </table>
    </div>
    
    <div id="div02" style="width:615px; border:solid 0px black; display:<%=div02display%>">
         <div style="height:30px; line-height:16pt; text-align:left;" class="edit_detail_header"> &nbsp; &nbsp; 已选择盘点的产品所在货位</div>
         <div style="width:100%; height:420px; overflow-x:auto; overflow-y:auto;" class="edit_detail_body">
           
            <asp:DataGrid ID="dgSelectedCell" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="GridStyle" CellPadding="3" CellSpacing="1" GridLines="None"  OnItemDataBound="dgCell_ItemDataBound" EnableViewState="False" HorizontalAlign="Justify">
                    <Columns>
                        <asp:BoundColumn DataField="CELLCODE" HeaderText="货位编码">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="90px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CELLNAME" HeaderText="货位名称">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="90px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CURRENTPRODUCT" HeaderText="产品代码">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="90px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="产品名称" DataField="C_PRODUCTNAME"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <HeaderStyle Width="60px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称">
                            <HeaderStyle Width="60px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="QUANTITY" HeaderText="库存量">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Width="60px" />
                        </asp:BoundColumn>
                    </Columns>
                   <ItemStyle BackColor="AliceBlue" Height="26px" />
                   <HeaderStyle CssClass="GridHeader2" HorizontalAlign="Center" VerticalAlign="Middle" Height="28px" />
                   <AlternatingItemStyle BackColor="White" />
               </asp:DataGrid> 
         </div>
       <div style="height:38px; line-height:16pt;" class="edit_detail_pager">
         <asp:Button ID="btnPrevious2" runat="server" Text="上一步" />
         <asp:Button ID="btnNext2" runat="server" Text="生成盘点单" OnClick="btnNext2_Click" />
      </div>
    </div>
    
    
    </td>
  </tr>
</table> 
        <asp:HiddenField ID="hdnLeftRowIndex" runat="server" Value="0" />
        <asp:HiddenField ID="hdnRightRowIndex" runat="server"  Value="0"/>
        
        
           </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
<script>
   function selectRow(objGridName,index)
   {
      var table=document.getElementById(objGridName);
      for(var i=1;i<table.rows.length;i++)       //table.rows[1].cells.length
      {
         table.rows[i].cells[0].innerHTML="";
      }
      table.rows[index+1].cells[0].innerHTML="<img src=../../images/arrow01.gif />";
      if(objGridName=='dgProduct')
      {
         document.getElementById('hdnLeftRowIndex').value=index;
      }
      else
      {
         document.getElementById('hdnRightRowIndex').value=index;
      }
   }
</script>