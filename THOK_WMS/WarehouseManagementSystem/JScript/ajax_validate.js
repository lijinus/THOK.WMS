// JScript 文件
function UniqueValidate(tableName,fieldName,value,filter)
{
    Validate.UniqueValidate(tableName,fieldName,value,filter,onSuccess,onFailed);
}

function onSuccess(result)
{
   if(result!='0')
   {
       document.getElementById('validate_msg').style.display='block';
   }
   else
   {
       document.getElementById('validate_msg').style.display='none';
   }
   return false;
}

function onFailed(result)
{ 
   alert(result.get_message());
   //alert('服务器验证失败');
}

function IsExist(tableName,fieldName,value,objCode,objName)
{
    if(value=="")
    {
       return;
    }
    Validate.IsExist(tableName,fieldName,value,onSuccess2,onFailed);
}

function onSuccess2(result)
{
   if(result!='0')
   {
       btnClear.click();
   }
   return false;
}