<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveBillChartPage.aspx.cs" Inherits="Code_StorageManagement_MoveBillChartPage" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="AspNetPager" TagPrefix="NetPager" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
      .moveCell{position:relative; cursor:move;}

#menu{

} 

#menu ul{
	background-color:white;
	position: absolute;
	width: 73px;
	overflow:hidden;
}  
#menu ul li{
	border: 1px solid #BBB;
	text-align:center;
	width: 71px;height:26px; 
	cursor:hand;

}

#menu ul li a:hover
{ padding-top:5px;
    background:#88C366;
	color:red;
	width:70px;
	height:25px;
}

#menu ul li a
{ padding-top:5px;
    background:white;
	width:70px;
	height:25px;
}
    </style>
<script   language="javascript">   
 
 var objMoveOut=null;
 var objMoveIn=null;
 var objTemp=null;

  var   beginMoving=false;   
  var   sourceObj=null;   
  var   objectObj=null;   
  function   MouseDownToMove(obj){ 
    
      if(event.button==2)   
      { 
        return false;
      }
  
      obj.style.zIndex=1;   
      obj.mouseDownY=event.clientY;   
      obj.mouseDownX=event.clientX;   
      beginMoving=true;   
      obj.setCapture();   
      sourceObj=obj;   
      objectObj=null;   
  }   
    
  function   MouseMoveToMove(obj){   
      if(!beginMoving)   return   false;   
      obj.style.top   =   (event.clientY-obj.mouseDownY);   
      obj.style.left   =   (event.clientX-obj.mouseDownX);   
  }   
  function   MouseUpToMove(obj){   
  
      showMenu(obj);
  
      if(!beginMoving)   return   false;   
      obj.releaseCapture();   
      obj.style.top=0;   
      obj.style.left=0;   
      obj.style.zIndex=0;   
      beginMoving=false;   
      window.setTimeout("swapFun()",10);   
  }   
    
  function   MouseOverFun(obj)   
  {   
     objectObj=obj;   
  }  
    
  function   swapFun()   
  {   
      if(sourceObj!=null&&   objectObj!=null) 
      {  
         var cellcode_out=sourceObj.cellcode;  //移出货位
         var cellname_out=sourceObj.cellname;
         var cellquantity_out=sourceObj.quantity;
         var cellproductcode_out=sourceObj.productcode;
         var cellproductname_out=sourceObj.productname;
         
         var cellcode_in=objectObj.cellcode;  //移入货位
         var cellname_in=objectObj.cellname;
         var cellquantity_in=objectObj.quantity;
         var cellproductcode_in=objectObj.productcode;
         var cellproductname_in=objectObj.productname;
         
         document.getElementById('cellcode_out').value=cellcode_out;
         document.getElementById('cellname_out').value=cellname_out;
         document.getElementById('cellquantity_out').value=cellquantity_out;
         document.getElementById('productcode_out').value=cellproductcode_out;
         document.getElementById('productname_out').value=cellproductname_out;
         document.getElementById('unitcode').value=sourceObj.unitcode;
         document.getElementById('unitname').value=sourceObj.unitname; 
         
         document.getElementById('cellcode_in').value=cellcode_in;
         document.getElementById('cellname_in').value=cellname_in;
         document.getElementById('cellquantity_in').value=cellquantity_in;
         document.getElementById('productcode_in').value=cellproductcode_in;
         document.getElementById('productname_in').value=cellproductname_in;
         
         if(cellcode_out==cellcode_in)
         {
           return;
         }
         
         if(cellproductcode_in!='' && cellproductcode_out!=cellproductcode_in && cellquantity_in>0.00)
         {
            alert('产品不同，不能移入');
            return;
         }
         var mergeQty =parseFloat(cellquantity_out)+parseFloat(cellquantity_in);
         //alert(mergeQty);
         if(mergeQty>30.00)
         {
             alert('移位合并后，数量超标');
             return;
         }
         
         document.getElementById('btnCreate').click();
         
////          var row=document.getElementById("moveList").insertRow( document.getElementById("moveList").rows.length);
////　        row.style.backgroundColor="#FFFFFF"
////　        var cell0=row.insertCell(0);
////          cell0.innerHTML=cellcode_out;
////          var cell1=row.insertCell(1);
////          cell1.innerHTML=cellname_out;
////          var cell2=row.insertCell(2);
////          cell2.innerHTML=cellproductcode_out;
////          var cell3=row.insertCell(3);
////          cell3.innerHTML=cellproductname_out;
////          
////          var cell4=row.insertCell(4);
////          cell4.innerHTML=cellcode_in;
////          var cell5=row.insertCell(5);
////          cell5.innerHTML=cellname_in;
////          var cell6=row.insertCell(6);
////          cell6.innerHTML=cellquantity_out;
////          var cell7=row.insertCell(7);
////          cell7.innerHTML="<span onclick='removeRow(this.parentElement.parentElement.rowIndex);'>删除</span>";
      }
      sourceObj=null;   
      objectObj=null;   
      
    if(document.getElementById('moveList').rows.length>1)
    {
       document.getElementById('divList').style.display='block';
    }
    else
    {
       document.getElementById('divList').style.display='none';
    }
  } 
  
function MoveIn(objIn)
{
   showMenu(objIn);
}

function showMenu(obj)
{
      if(event.button==2)   
      {  
        objTemp=obj;
        var menu=document.getElementById('menu');
        menu.style.display='block';
        menu.style.posLeft=document.body.scrollLeft+event.clientX-20;//event.clientX-20 ;
        menu.style.posTop=document.body.scrollTop+event.clientY;//event.clientY-15; 
        return   false;   
      }   
      else
      {
        document.getElementById('menu').style.display='none';
      }
}

function setObjMoveOut()
{
    objMoveOut=objTemp;
    document.getElementById('menu').style.display='none';
    return false;
}
function setObjMoveIn()
{
         if(objMoveOut==null)
         {
            alert('请先选择移出货位');
            return false;
         }
         objMoveIn=objTemp;
         document.getElementById('menu').style.display='none';
         
         document.getElementById('cellcode_out').value=objMoveOut.cellcode;
         document.getElementById('cellname_out').value=objMoveOut.cellname;
         document.getElementById('cellquantity_out').value=objMoveOut.quantity;
         document.getElementById('productcode_out').value=objMoveOut.productcode;
         document.getElementById('productname_out').value=objMoveOut.productname;
         document.getElementById('unitcode').value=objMoveOut.unitcode;
         document.getElementById('unitname').value=objMoveOut.unitname;         
         
         document.getElementById('cellcode_in').value=objMoveIn.cellcode;
         document.getElementById('cellname_in').value=objMoveIn.cellname;
         document.getElementById('cellquantity_in').value=objMoveIn.quantity;
         document.getElementById('productcode_in').value=objMoveIn.productcode;
         document.getElementById('productname_in').value=objMoveIn.productname;
         
         if(objMoveOut.cellcode==objMoveIn.cellcode)
         {
           return;
         }
         
         if(objMoveIn.productcode!='' && objMoveOut.productcode!=objMoveIn.productcode && parseFloat(objMoveIn.quantity)>0.00)
         {
            alert('产品不同，不能移入');
            return;
         }
         var mergeQty =parseFloat(objMoveOut.quantity)+parseFloat(objMoveIn.quantity);
         //alert(mergeQty);
         if(mergeQty>30.00)
         {
             alert('移位合并后，数量超标');
             return;
         }
         
         document.getElementById('btnCreate').click();
//////         var row=document.getElementById("moveList").insertRow( document.getElementById("moveList").rows.length);
//////　        //var rowCount =document.getElementById("moveList").rows.length;
//////　        row.style.backgroundColor="#FFFFFF"
//////　        var cell0=row.insertCell(0);
//////          cell0.innerHTML=objMoveOut.cellcode;//cellcode_out;
//////          var cell1=row.insertCell(1);
//////          cell1.innerHTML=objMoveOut.cellname;//cellname_out;
//////          var cell2=row.insertCell(2);
//////          cell2.innerHTML=objMoveOut.productcode;//cellproductcode_out;
//////          var cell3=row.insertCell(3);
//////          cell3.innerHTML=objMoveOut.productname;//cellproductname_out;
//////          
//////          var cell4=row.insertCell(4);
//////          cell4.innerHTML=objMoveIn.cellcode;//cellcode_in;
//////          var cell5=row.insertCell(5);
//////          cell5.innerHTML=objMoveIn.cellname;//cellname_in;
//////          var cell6=row.insertCell(6);
//////          cell6.innerHTML=objMoveOut.quantity;//cellquantity_out;
//////          var cell7=row.insertCell(7);
//////          cell7.innerHTML="<span style='cursor:hand;' onclick='removeRow(this.parentElement.parentElement.rowIndex);'>删除</span>";
          objMoveOut=null;
          objMoveIn=null;
    if(document.getElementById('moveList').rows.length>1)
    {
       document.getElementById('divList').style.display='block';
    }
    else
    {
       document.getElementById('divList').style.display='none';
    }
          return false;
}

/*删除行，采用deleteRow(row Index)*/
function removeRow(index){
　　document.getElementById("moveList").deleteRow(index); 
} 

//function   document.onmouseup()   
//{       

//} 
  
function Exit()
{
  window.open("../../MainPage.aspx","_self");return false;
}

function Back()
{
    window.open("MoveBillPage.aspx","_self");return false;
}  
  </script> 
</head>
<body oncontextmenu="return false;"> 
    <form id="form1" runat="server">
     <div id="menu" style="position:absolute; display:none;">
         <ul>
           <li><a onclick="return setObjMoveOut()" href="#">移出</a></li>
           <li><a onclick="return setObjMoveIn()"  href="#">移入</a></li>
         </ul>
      </div>
      <table style="width:100%">
         <tr>
            <td style=" width:213px;display:none;" valign="top">
                <div id="div_tree" onscroll="setDivPosition()" onmouseup="loadEventCookie()" onmouseover="loadEventCookie()" 
                style="overflow-x:hidden; overflow-y:auto; width:213px; height:520px;">
                  <yyc:smarttreeview ID="tvWarehouse" runat="server" ShowLines="True" ExpandDepth="3" OnSelectedNodeChanged="tvWarehouse_SelectedNodeChanged" >
                      <SelectedNodeStyle BackColor="SkyBlue" BorderColor="Black" BorderWidth="1px" />
                  </yyc:smarttreeview>
                </div>
            </td>
            <td style="vertical-align:top;">
               <div style="display:block; position:relative;" id="divList">
                  <table id="moveList2" style="background-color:Gainsboro; display:none;" cellpadding="5" cellspacing="1" runat="server">
                     <tr style="background-color:#f9f9f9; text-align:center;">
                        <td>移出货位编码</td>
                        <td>移出货位名称</td>
                        <td>产品编码</td>
                        <td>产品名称</td>
                        <td>移入货位编码</td>
                        <td>移入货位名称</td>
                        <td>数量</td>
                        <td>操作</td>
                     </tr>
                     
                  </table>
                   
                   <asp:DataGrid ID="moveList" runat="server" AutoGenerateColumns="False" CellPadding="5" CellSpacing="1" BackColor="Gainsboro" ForeColor="#333333" GridLines="None" OnItemCommand="moveList_ItemCommand" OnItemDataBound="moveList_ItemDataBound">
                       <HeaderStyle HorizontalAlign="Center" Font-Bold="True" ForeColor="DimGray" CssClass="GridHeader2" />
                       <Columns>
                           <asp:BoundColumn DataField="OUT_CELLCODE" HeaderText="移出货位编码"></asp:BoundColumn>
                           <asp:BoundColumn DataField="OUT_CELLNAME" HeaderText="移出货位名称"></asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTCODE" HeaderText="产品代码"></asp:BoundColumn>
                           <asp:BoundColumn DataField="PRODUCTNAME" HeaderText="产品名称"></asp:BoundColumn>
                           <asp:BoundColumn DataField="IN_CELLCODE" HeaderText="移入货位编码"></asp:BoundColumn>
                           <asp:BoundColumn DataField="IN_CELLNAME" HeaderText="移入货位名称"></asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITCODE" HeaderText="单位编码"></asp:BoundColumn>
                           <asp:BoundColumn DataField="UNITNAME" HeaderText="单位名称"></asp:BoundColumn>
                           <asp:BoundColumn DataField="QUANTITY" HeaderText="数量"></asp:BoundColumn>
                           <asp:TemplateColumn HeaderText="操作"></asp:TemplateColumn>
                       </Columns>
                       <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                       <EditItemStyle BackColor="#999999" />
                       <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                       <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                       <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                   </asp:DataGrid>
                   
                  <table>
                    <tr>
                      <td> <asp:Button ID="btnCreateBill" runat="server" Text="生成移位单" OnClick="btnCreateBill_Click" />
                          &nbsp; &nbsp; <input type="button" value="放弃" onclick="Back();" /> 
                      
                      </td>
                    </tr>
                  </table>
               </div>
              <asp:Panel ID="pnlCell" runat="server"  Width="3900px"  style=" overflow:auto; padding:10 10 10 5;"></asp:Panel>
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
    
    if(document.getElementById('moveList').rows.length>1)
    {
       document.getElementById('divList').style.display='block';
    }
    else
    {
       document.getElementById('divList').style.display='none';
    }
</script> 


<div>
    <asp:HiddenField ID="billNo" runat="server" Value="" />

    <asp:HiddenField ID="cellcode_out" runat="server" />
    <asp:HiddenField ID="cellname_out" runat="server" />
    <asp:HiddenField ID="cellquantity_out" runat="server" />
    <asp:HiddenField ID="productcode_out" runat="server" />
    <asp:HiddenField ID="productname_out" runat="server" />   
    <asp:HiddenField ID="unitcode" runat="server" />
    <asp:HiddenField ID="unitname" runat="server" /> 
    
    <asp:HiddenField ID="cellcode_in" runat="server" />
    <asp:HiddenField ID="cellname_in" runat="server" />
    <asp:HiddenField ID="cellquantity_in" runat="server" />
    <asp:HiddenField ID="productcode_in" runat="server" />
    <asp:HiddenField ID="productname_in" runat="server" />
    
    <asp:Button ID="btnCreate" runat="server" Text="Button" CssClass="HiddenControl" OnClick="btnCreate_Click" />
</div>
    </form>
</body>
</html>