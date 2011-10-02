// JScript 文件
var reDate=/^(19|20)\d{2}-(0?\d|1[012])-(0?\d|[12]\d|3[01])$/; //判断日期

//输入字数限制
function textCount(field, maxlimit)
{
 if (field.value.length > maxlimit)
 {
    field.value = field.value.substring(0, maxlimit);
 }
}

//不可编辑
function CannotEdit(obj)
{
   
     obj.blur();
   
}
function EncodeURI(TypeName)
{
    return encodeURIComponent(TypeName);                           
} 
//必须为数字
function IsNumber(obj,strFieldName)
{
   var num=obj.value;
   if(isNaN(num))
   {
      alert(strFieldName+'请填入数字！');
      return false;
   }
   return true;
}
function IsNumber1(obj,strFieldName)
{
   var num=obj.value;
   if(isNaN(num))
   {
      alert(strFieldName+'请填入数字！');
      return false;
   }
   else 
    if(num>999999.99 || num< 0.000)
   {
      alert(strFieldName+'请填入0.000 - 999999.99范围数字！');
      return false;
   }
   return true;
}


//字符串字数
function CheckLength(obj,iLen)
{
    var v=obj.value;
    if(v.length>iLen)
    {
      alert('填写的内容超过最大长度！');
      obj.focus();
      return;
    }
}


//是否空字符串		
function checkEmpty(oObj)
{
   str=oObj.value.Trim();
   if(str.length==0)
   {
     oObj.focus();
	alert("不能为空");	
	return false;
   }   
 }		
 
// 是否为数字或小数点后只能一位

function checkFloat1(oObj)
{   
    str=oObj.value;
	if (str=='')
	{
	oObj.value="";
	
	return false;
	
	}
	else
	{
	  if(!str.IsFloat1())
	  {
	    alert("不是数字或小数点后只能1位");
	    oObj.focus();
    	return false;    	
	  }
	return true;
	}

} 

function checkFloat2(oObj)
{   
    str=oObj.value;
	if (str=='')
	{
	oObj.value="";
	
	return false;
	
	}
	else
	{
	  if(!str.IsFloat2())
	  {
	    alert("不是数字或小数点后只能2位");
	    oObj.focus();
    	return false;    	
	  }
	return true;
	}

} 

//是否为整数
function checkFloat0(oObj)
{   
    str=oObj.value;
	if (str=='')
	{
	oObj.value="";
	
	return false;
	
	}
	else
	{
	  if(!str.IsFloat0())
	  {
	    alert("不是整数");
	    oObj.focus();
    	return false;    	
	  }
	return true;
	}

} 
 function CheckDelete(obj)
{
    //czAlert('请您选择数据,再进行删除操作！');
    //return false;
    
    if(!obj.disabled)
    {
        var table=document.getElementById('gvMain');
        if(!table)
        {
        //alert('无记录可删除');
        alert('无记录可删除！');
        return false;
        }
        for(var i=1;i<table.rows.length;i++)
        {
                    
            var cell=table.rows[i].cells[0]; 
            var checkbox=cell.getElementsByTagName("input");  
            if (checkbox[0].checked)
            {
                if(confirm('确定要删除选择的数据？'))
                return true;
                else
                return false;
            }
        }
        alert('请选择要删除的数据！')
        //alert('请选择记录!')
    }
    return false;
}
//删除确认
function DeleteConfirm(btnID)
{
      if(confirm('确定要删除选择的数据？','删除提示'))
      {
         var btn=document.getElementById(btnID);
         btn.click();
         window.location.reload();
      }
      else
      {
         return false;
      }
}
function DeleteConfirm()
{
      if(confirm('确定要删除？','删除提示'))
      {
         return true;
      }
      else
      {
         return false;
      }
}

//清空
function Clear(strTableName)
{
   var table =document.getElementById(strTableName);
   for(var i=2;i<table.rows.length;i++)
   {
     var cell=table.rows[i].cells[1];
     var box=cell.getElementsByTagName("INPUT");
     if(box[0]!=null)
     {
        box[0].value="";
     }
   }
   return false;
}

//全选
function checkboxChange(objCheckBox,strGridName)
{
    var table=document.getElementById(strGridName);  
    if(table.rows[1].cells.length==1)
    {
       return;
    }
    if(objCheckBox.checked==true)
    {
         for(var i=1;i<table.rows.length;i++)
         {
            var cell=table.rows[i].cells[0];
            var chk=cell.getElementsByTagName("INPUT");
            if(chk[0].disabled==false)
            {
              chk[0].checked=true;
            }
         }
    }
    else
    {
         for(var i=1;i<table.rows.length;i++)
         {
            var cell=table.rows[i].cells[0];
            var chk=cell.getElementsByTagName("INPUT");
            chk[0].checked=false;
         }            
    }
}
//清空控件内容
function Clear(strControlID)
{
  document.getElementById(strControlID).value="";
}

//全选
function checkboxChange(objCheckBox,strGridName,iCheckBoxCol)//
{
    var table=document.getElementById(strGridName);  
    if(table.rows[1]==null)
    {
       return;
    }
    else if(table.rows[1].cells.length==1)
    {
       return;
    }
    if(objCheckBox.checked==true)
    {
         for(var i=1;i<table.rows.length;i++)
         {
            var cell=table.rows[i].cells[iCheckBoxCol];
            var chk=cell.getElementsByTagName("INPUT");
            if(chk[0].disabled==false)
            {
              chk[0].checked=true;
            }
         }
    }
    else
    {
         for(var i=1;i<table.rows.length;i++)
         {
            var cell=table.rows[i].cells[iCheckBoxCol];
            var chk=cell.getElementsByTagName("INPUT");
            chk[0].checked=false;
         }            
    }
}

function ShowEdit2(flag)
    {
        funHideDdlField();
        if(flag=='1')
            {
            document.getElementById('div_Edit').style.display='block';
            }
            else
            {
            document.getElementById('div_Edit').style.display='none';
            funShowDdlField();
            }
            
       return false;
    }
       function funShowAllSelect()
    {
        var obj;
        obj = document.getElementsByTagName("Select");
        var i;
        for(i = 0; i < obj.length; i++)
        {
        obj[i].style.display = "block"; 
        }
    }
    function funHideAllSelect()
    {
        var obj;
        obj = document.getElementsByTagName("Select");
        var i;
        for(i = 0; i < obj.length; i++)
        {
        obj[i].style.display = "none"; 
        }
    }
        function GetBatchNo()
    {
        var reDate=/^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$/; //判断日期格式
        var orderDate=Trim(document.getElementById('txtOrderDate').value);
        //document.getElementById('txtOrderDate').value="000";
        if(orderDate.length<10)
        {
            return;
        }
         if(!reDate.test(orderDate))
         {
            return false;
         }
         else
         {
            document.getElementById('lnkBtnGetBatchNo').click();
         }
    } 
    function CheckCondition()
    {
        var reDate=/^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$/; //判断日期格式
        var orderDate=Trim(document.getElementById('txtOrderDate').value);

         if(!reDate.test(orderDate))
         {
            alert('日期格式不正确！');
            return false;
         }
         
    }


    
    function funShowDdlField()
    {
        var obj;
        obj=document.getElementById('ddl_Field');
        if(obj)
        obj.style.display = "block";
        
        var scz;
        scz=document.getElementById('ddlSTATUS3');
        if(scz)
        scz.style.display = "block";
    }
    
    function funHideDdlField()
    {
        var obj;
        obj=document.getElementById('ddl_Field');
        if(obj)
        obj.style.display = "none";
        
         var scz;
        scz=document.getElementById('ddlSTATUS3');
        if(scz)
        scz.style.display = "none";
    }
    
    
String.prototype.Trim=function()
{
	return this.replace(/^\s*/g,"").replace(/\s*$/g,"");
}
String.prototype.LTrim=function()
{
	return this.replace(/^\s*/g,"");
}
String.prototype.RTrim=function()
{
	return this.replace(/\s*$/g,"");
}
String.prototype.Left=function(count)
{
	return this.substr(0,count);
}
String.prototype.Right=function(count)
{
	return this.substr(this.length-count,count);
}
String.prototype.RemoveBlank=function()
{
	return this.replace(/\s*/g,"");
}
String.prototype.IsEnglish=function()
{
	return /^[a-zA-Z]+$/.test(this);
}
String.prototype.IsNumber=function(){
	return /^\d+$/.test(this);
}
String.prototype.IsFloat0=function(){
	return /^\d+\.\d{0}$|^\d+$/.test(this);

}
String.prototype.IsFloat1=function(){
	return /^\d+\.\d{1}$|^\d+$/.test(this);

}
String.prototype.IsFloat2=function(){
	return /^\d+\.\d{2}$|^\d+$/.test(this);

}
String.prototype.IsFloat3=function(){
	return /^\d+\.\d{3}$|^\d+$/.test(this);

}
String.prototype.IsFloat4=function(){
	return /^\d+\.\d{4}$|^\d+$/.test(this);

}
String.prototype.IsFloat5=function(){
	return /^\d+\.\d{5}$|^\d+$/.test(this);

}
String.prototype.IsFloat6=function(){
	return /^\d+\.\d{6}$|^\d+$/.test(this);

}
String.prototype.IsFloat7=function(){
	return /^\d+\.\d{7}$|^\d+$/.test(this);

}
String.prototype.IsFloat8=function(){
	return /^\d+\.\d{8}$|^\d+$/.test(this);

}
String.prototype.IsFloat9=function(){
	return /^\d+\.\d{9}$|^\d+$/.test(this);

}

String.prototype.Trim = function(){ return Trim(this);} 
String.prototype.LTrim = function(){return LTrim(this);} 
String.prototype.RTrim = function(){return RTrim(this);} 
function LTrim(str) 
{ 
var i; 
for(i=0;i<str.length;i++) 
{ 
if(str.charAt(i)!=" "&&str.charAt(i)!=" ")break; 
} 
str=str.substring(i,str.length); 
return str; 
} 
function RTrim(str) 
{ 
var i; 
for(i=str.length-1;i>=0;i--) 
{ 
if(str.charAt(i)!=" "&&str.charAt(i)!=" ")break; 
} 
str=str.substring(0,i+1); 
return str; 
} 
function Trim(str) 
{ 
return LTrim(RTrim(str)); 
} 