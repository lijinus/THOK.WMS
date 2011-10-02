<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckBillAutoCreatePage.aspx.cs" Inherits="Code_StorageManagement_CheckBillAutoCreatePage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>盘点单自动生成</title>
    <base target="_self" />
    <link href="../../css/css.css" type="text/css" rel="stylesheet" />
    <link href="../../css/op.css" type="text/css" rel="stylesheet" />
    <script src="../../JScript/Calendar.js"></script>
    <script src="../../JScript/SelectDialog.js?time=098"></script>
    <script src="../../JScript/Check.js?time=2008" type="text/javascript"></script>
    <style>
      .tab_selected
      {
        text-align:center; 
        height:27px; width:102px; 
        background-image:url(../../images/option_selected.gif); 
        background-repeat:no-repeat; 
        cursor:hand;
      }
      .tab{
         text-align:center; 
         height:27px; width:102px; 
         background-image:url(../../images/option_no.gif); 
         background-repeat:no-repeat;
         cursor:hand;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; height:5px; background-image:url(../../images/option_bg01.gif);overflow: hidden ;"></div>
    <table cellpadding="1" cellspacing="1" style="width:100%; background-image:url(../../images/option_bg01.gif);">
      <tr style="height:27px; z-index:2;">
       <td id="td00" class="tab_selected" onclick="switchTab(0)">货位盘点</td>
       <td id="td01" class="tab" onclick="switchTab(1)">产品盘点</td>
       <td id="td02" class="tab" onclick="switchTab(2)">异动盘点</td>
       <td style="border-bottom:solid 1px gray;"></td>
      </tr>
    </table>
    <div>
      <iframe id="frame" src="CheckBillByCell.aspx" style=" height:530px; width:100%;" bordercolor="white" frameborder="0">
      
      </iframe>
    </div>
    </form>
<script>
  function switchTab(tabIndex)
  {
     if(tabIndex==0)
     {
         document.getElementById('frame').src='CheckBillByCell.aspx';
         document.getElementById('td00').className='tab_selected';
         document.getElementById('td01').className='tab';
     }
     else if(tabIndex==1)
     {
         document.getElementById('frame').src='CheckBillByProduct.aspx';
         document.getElementById('td00').className='tab';
         document.getElementById('td01').className='tab_selected';
     }
     else if(tabIndex==2)
     {
         document.getElementById('frame').src='CheckBillByChanged.aspx';
         document.getElementById('td00').className='tab';
         document.getElementById('td01').className='tab';
         document.getElementById('td02').className='tab_selected';
     }
  }
</script>    
</body>
</html>
