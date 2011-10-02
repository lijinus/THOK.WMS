<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfoParameterSetup.aspx.cs" Inherits="Code_SysInfomation_UserInfoParameterSetup_UserInfoParameterSetup" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <link rel="stylesheet" href="../../../css/css.css" type="text/css" />
 <style type="text/css">
    A
    {
	    color:black;
	    text-decoration:none;
    }

    A:hover
    {
	    color:#FE6103;
	    text-decoration:none; /*underline;*/
    }
    A:Active
    {
	    text-decoration:none;
    }    
    td { 
      font-size: 12px;
      color: #000000;
      line-height: 150%;
      }
    .sec1 { 
      background-color: #EEEEEE;
      cursor: hand;
      color: #000000;
      border-left: 1px solid #FFFFFF;
      border-top: 1px solid #FFFFFF;
      border-right: 1px solid gray;
      border-bottom: 1px solid #FFFFFF
      }
    .sec2 { 
      background-color: #D4D0C8;
      cursor: hand;
      color: #000000;
      border-left: 1px solid #FFFFFF; 
      border-top: 1px solid #FFFFFF; 
      border-right: 1px solid gray; 
      font-weight: bold; 
      }
    .main_tab {
      background-color: #D4D0C8;
      color: #000000;
      border-left:1px solid #FFFFFF;
      border-right: 1px solid gray;
      border-bottom: 1px solid gray; 
      }
    </style>

</head>
<body style="margin:10 20 10 20;">
    <form id="form1" runat="server">
    <div><table  width="100%" style="  position: relative; top: 0px; margin-bottom: 0px;" cellpadding="2" cellspacing="0">
            <tr style="">
                <td ><asp:Label ID="Label1" runat="server" Text="参数名称"></asp:Label></td>
                <td colspan="3" >
                    <asp:TextBox ID="txtParameterName" runat="server" Width="190px"></asp:TextBox>
                    <asp:DropDownList ID="ddlParameterName" runat="server" Width="114px">
                    </asp:DropDownList></td>
            </tr>
            <tr style="">
                <td><asp:Label ID="Label2" runat="server" Text="参 数 值"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtParameterValue" runat="server" Width="190px"></asp:TextBox></td>
            </tr>
            
        <tr style="">
            <td><asp:Label ID="Label5" runat="server" Text="参数显示文本"></asp:Label></td>
            <td colspan="3"><asp:TextBox ID="txtParameterText" runat="server" Width="190px"></asp:TextBox></td>
        </tr>
        <tr style="">
            <td><asp:Label ID="Label4" runat="server" Text="参数可用状态"></asp:Label></td>
            <td colspan="3">
                    <asp:DropDownList ID="ddlState" runat="server">
                        <asp:ListItem Selected="True" Value="1">可用</asp:ListItem>
                        <asp:ListItem Value="0">不可用</asp:ListItem>
                    </asp:DropDownList></td>
        </tr>
            <tr style="">
                <td style="height: 66px" >
                    <asp:Label ID="Label3" runat="server" Text="参数描述"></asp:Label></td>
                <td colspan="3" style="height: 66px" >
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="580px" Height="60px"></asp:TextBox>
                    </td>
            </tr>
            <tr style="">
                <td colspan="4">
                    
                    <table>
                        <tr>
                            <td><asp:Button ID="btnSearch" runat="server" CssClass="ButtonCss" OnClick="btnSearch_Click" Text="查找" /></td>
                            <td><asp:Button ID="btnInsert" runat="server" CssClass="ButtonCss" OnClick="btnInsert_Click"  Text="新增" /></td>
                            <td><asp:Button ID="btnUpdate" runat="server" CssClass="ButtonCss" OnClick="btnUpdate_Click"  Text="修改" /></td>
                            <td style="width: 48px"><asp:Button ID="Button1" runat="server" CssClass="ButtonCss" OnClientClick="return CancelEdit();"  Text="取消" /></td>
                            <td style="width: 48px"><asp:Button ID="btndewqh" runat="server" CssClass="ButtonCss" OnClientClick="return DeleteSelected();"  Text="删除" OnClick="btnDelete_Click" /></td>
                            <td style="width: 48px"><asp:Button ID="btnClear" runat="server" Text="退出" CssClass="ButtonCss" OnClientClick="return Exit();" OnClick="btnClear_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="4" >
                    <asp:GridView ID="gvSystemParameter" runat="server" AllowPaging="True" OnPageIndexChanging="gvSystemParameter_PageIndexChanging"
                        OnRowDataBound="gvSystemParameter_RowDataBound" OnRowEditing="gvSystemParameter_RowEditing"
                        Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField></asp:TemplateField>
                            <asp:BoundField DataField="ParameterName" HeaderText="参数名称" />
                            <asp:BoundField DataField="ParameterValue" HeaderText="参数值" />
                            <asp:BoundField DataField="ParameterText" HeaderText="显示文本" />
                            <asp:BoundField DataField="Description" HeaderText="参数描述" />
                            <asp:BoundField DataField="State" HeaderText="状态" />
                        </Columns>
                        <HeaderStyle CssClass="GridHeader" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:TextBox   ID="lbID" runat="server"  Height="0px" Width="0px"  BorderStyle="None"></asp:TextBox>
    
    <script type="text/javascript" language="javascript">
        
        
        function DDLParameterNameChanged()
        {
          if(!document.getElementById("ddlParameterName").selectedIndex==0)
          {
             var index=document.getElementById("ddlParameterName").options.selectedIndex;
             document.getElementById("txtParameterName").value=document.getElementById("ddlParameterName").options(index).text;
             document.getElementById("txtDescription").value=document.getElementById("ddlParameterName").value;
          }
          else
          {
             document.getElementById("txtParameterName").value="";
             document.getElementById("txtDescription").value="";
          }
        }
        
        function GotoEdit(rowIndex)
        {  
//           alert(rowIndex);
//           secBoard(2);
           document.getElementById("btnUpdate").style.display="block";  
           document.getElementById("btnInsert").style.display="none"; 
           var table=document.getElementById("gvSystemParameter");
           var row=table.rows[rowIndex];
           document.getElementById("lbID").innerText=row.cells[0].innerText;
           document.getElementById("txtParameterName").value=row.cells[1].innerText;
           document.getElementById("txtParameterValue").value=row.cells[2].innerText;
           document.getElementById("txtParameterText").value=row.cells[3].innerText;
           document.getElementById("txtDescription").value=row.cells[4].innerText;
           if(row.cells[5].innerText=="可用")
           {
              document.getElementById("ddlState").options.selectedIndex=0;
           }
           else
           {
              document.getElementById("ddlState").options.selectedIndex=1;
           }
           document.getElementById("btnUpdate").style.display="block";
           return false;
        }
        
        function GotoDelete(index)
        {
            var table=document.getElementById("gvSystemParameter");
            var row=table.rows[index]; 
            var recid=row.cells[0].innerText;
            document.getElementById("lbID").value=recid;
            //document.getElementById("btnDelete").click();
            return confirm('确定要删除选择的数据？','删除提示');
        }
        
        function DeleteSelected()
        {
            var table=document.getElementById("gvSystemParameter");    
            var ID="0,";  
             for(var i=1;i<table.rows.length-1;i++)
             {
                var cell=table.rows[i].cells[0];
                var chk=cell.getElementsByTagName("INPUT");
                if(chk[0].checked==true)
                {
                   ID+=cell.innerText+",";
                }
             }
             document.getElementById("lbID").value=ID;
             if(ID=="0,")
             {
                alert('请选择要删除的数据！','删除提示');
                return false;
             }   
           return confirm('确定删除选中的记录？','删除提示');
         
        }
        
        function checkboxChange(obj)
        {
            var table=document.getElementById("gvSystemParameter");  
            var chktop=table.rows[0].getElementsByTagName("INPUT")[0];
            if(chktop.checked==true)
            {
                 for(var i=1;i<table.rows.length-1;i++)
                 {
                    var cell=table.rows[i].cells[0];
                    var chk=cell.getElementsByTagName("INPUT");
                    chk[0].checked=true;
                 }
            }
            else
            {
                 for(var i=1;i<table.rows.length-1;i++)
                 {
                    var cell=table.rows[i].cells[0];
                    var chk=cell.getElementsByTagName("INPUT");
                    chk[0].checked=false;
                 }            
            }
        }
        
        function CancelEdit()
        {
           document.getElementById("lbID").innerText="0";
           document.getElementById("txtParameterName").value="";
           document.getElementById("txtParameterValue").value="";
           document.getElementById("txtParameterText").value="";
           document.getElementById("txtDescription").value=""; 
           document.getElementById("btnUpdate").style.display="none";  
           document.getElementById("btnInsert").style.display="block"; 
           document.getElementById("ddlState").options.selectedIndex=0;    
           return false;   
        }
        
          function Exit()
          {
             window.parent.location="../../../MainPage.aspx";
             return false;
          }
       
    </script>  
    </form>
</body>
</html>
