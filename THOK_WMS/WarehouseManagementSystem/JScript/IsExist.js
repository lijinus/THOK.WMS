// JScript 文件
var xmlHttp;
var obj;
function createxmlhttp()
{
    if (window.ActiveXObject)
    {
    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    if (window.XMLHttpRequest)
    {
    xmlHttp = new XMLHttpRequest();
    }
}
function StartAuthentication(obj2,TableName,FieldName)
{
//alert(obj.value);
obj=obj2;
var FieldContent=Trim(obj2.value);
obj2.value=FieldContent;
createxmlhttp();
 var url;
 var number = Math.random(); 
  url='../../../Common/IsExist.ashx?time='+number+'&TableName='+TableName+'&FieldName='+FieldName+'&FieldContent='+EncodeURI(FieldContent);
  xmlHttp.open("GET",url,true);
  xmlHttp.onreadystatechange=ShowResult;
  xmlHttp.send(null);
}

function ShowResult()
{

  if (xmlHttp.readyState==4)
      {
          if(xmlHttp.status==200)
              {
                  if(xmlHttp.responseText=="00")//在数据库中不存在，则通过
                  {
                        //obj.style.backgroundColor='#ffffff';
                        obj.className='CorrectTextBox';
                       //obj.parentNode.parentNode.cells[2].innerHTML='可用';
                       //alert('no exist!');
                  }
                  else if(xmlHttp.responseText=="11")
                  {
                        //obj.className='TextBox';
                        obj.className='ErrorTextBox ';
                       //obj.style.backgroundColor='red';
                       //obj.parentNode.parentNode.cells[2].innerHTML='该编号已在数据库中存在,请尝试别的编号';
                       
                       //alert('该编号已在数据库中存在,请尝试别的编号');
                       //alert('exist!');
                  }
                  else
                  {
                       //alert('未知的错误!');
                  }
               }
      }
}
