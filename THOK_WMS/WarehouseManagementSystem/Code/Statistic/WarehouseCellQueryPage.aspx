<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseCellQueryPage.aspx.cs" Inherits="Code_Statistic_WarehouseCellQueryPage" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>货位查询</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" /> 
    <style>
      .cell
      {
        height:90px; width:87px; background-color:#fcfcfc;border:1px solid #595a5c;
        text-align:center; vertical-align:middle;  word-break:keep-all; word-wrap : 
      }
      .cell2
      {
        height:150px; width:87px; background-color:#fcfcfc;border:1px solid #595a5c;
        text-align:center; vertical-align:middle;  word-break:keep-all; word-wrap : 
      }
      .panel
      {
        height:100px; width:90px;
        over-float:hidden; float:left; 
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <table style="width:100%">
         <tr>
            <td style=" width:213px;" valign="top">
                <div id="div_tree" onscroll="setDivPosition()" onmouseup="loadEventCookie()" onmouseover="loadEventCookie()" 
                style="overflow-x:hidden; overflow-y:auto; width:213px; height:1000px;">
                  <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged" >
                      <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
                  </yyc:smarttreeview>
                </div>
            </td>
            <td>
              <asp:Panel ID="pnlCell" runat="server"  Width="100%"  style=" overflow:auto; padding:10 10 10 5;" ></asp:Panel>
            </td>
         </tr>
      </table>
      
<script language="javaScript"> 

    function loadEventCookie()
    {
        var strCook = document.cookie;
        document.title=strCook;
        if(strCook.indexOf("!~")!=0){
            var intS = strCook.indexOf("!~");
            var intE = strCook.indexOf("~!");
            var strPos = strCook.substring(intS+2,intE);
            document.getElementById("div_tree").scrollTop = strPos;
        }
    }
    function setDivPosition()
    {
        var intY = document.getElementById("div_tree").scrollTop;
        document.title = intY;
        document.cookie = "yPos=!~" + intY + "~!";
    }
</script> 



    </form>
</body>
</html>
