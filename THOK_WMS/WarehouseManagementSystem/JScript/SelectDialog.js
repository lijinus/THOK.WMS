// JScript 文件
/*模式窗体*/
function SelectDialog(target,TableName,ReturnField,filterTarget,filterField)
{
   var date=new Date();
   var time=date.getMilliseconds();
  if(filterTarget!=null && filterField!=null)
  {
     var strFilterValue=document.getElementById(filterTarget).value;
     var returnvalue=window.showModalDialog("../../Common/SelectDialog.aspx?TableName="+TableName+"&ReturnField="+ReturnField+"&filterField="+filterField+"&filterValue="+strFilterValue+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=680px;dialogHeight=450px");
  }
  else
  {
     var returnvalue=window.showModalDialog("../../Common/SelectDialog.aspx?TableName="+TableName+"&ReturnField="+ReturnField+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=680px;dialogHeight=450px");
  }
   
   if(returnvalue==null)
   {
     return;
   }
   else if(returnvalue!='')
   {
        if(typeof(target)=="object")
        {
             target.value=returnvalue;
        }
        else
        {
           document.getElementById(target).value =returnvalue;
        }
   }                                     
}


function SelectDialog2(strTarget,strTableName,strReturnField,strFilterField,strFilterValue)
{
   var date=new Date();
   var time=date.getMilliseconds();
   var aryTarget=strTarget.split(',');
   var aryField=strReturnField.split(',');
   if(aryTarget.length!=aryField.length)
   {
      alert('参数有错！');
      return false;
   }
   
   var localPath = location.pathname;
   if(localPath.substring(0,1)!='/')
   {
      localPath='/'+localPath;
   }
  // alert(localPath);
   var path=localPath.split('/'); 
   var num = path.length;
   rootPath = "";
   if(num>2)
   {
       num = num-1;  //本地测试减1，发布成网站不减
       for(var i=2;i<num;i++)
       {
         rootPath=rootPath+"../";      
       }
   }
  // alert(rootPath);
   if (window.document.all)//IE判断window.showModalDialog!=null
   {
      var returnvalue;
       if(strFilterField!=null && strFilterField.length>0)
       {
         returnvalue=window.showModalDialog(rootPath+"Common/SelectDialog2.aspx?TableName="+strTableName+"&ReturnField="+strReturnField+"&filterField="+strFilterField+"&filterValue="+strFilterValue+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=700px;dialogHeight=460px");
       }
       else
       {
        returnvalue=window.showModalDialog(rootPath+"Common/SelectDialog2.aspx?TableName="+strTableName+"&ReturnField="+strReturnField+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=700px;dialogHeight=460px");
       }
       if(returnvalue==null)
       {
          return false;
       }
       else if(returnvalue!='')
       {
           var aryTarget=strTarget.split(',');
           var aryValue=new Array();
           aryValue=returnvalue.split('|');
           for(var i=0;i<aryTarget.length;i++)
           {
              var e=document.getElementById(aryTarget[i]);
              if(e!=null)
              {
                e.value=aryValue[i];
              }
           }  
           return false; 
       } 
   }
   else
   {
        //参数
        var strPara = "height=450px;width=500px;help=off;resizable=off;scroll=no;status=off;modal=yes;dialog=yes";
        //打开窗口
        var url=rootPath+"Common/SelectDialog2.aspx?TableName="+strTableName+"&ReturnField="+strReturnField+"&time="+time+"&targetControls="+strTarget;
        var DialogWin = window.open(url,"myOpen",strPara,true);
   } 
                                   
}
//限定数量条件，实现分配的仓库为空
function SelectDialog3(strTarget,strTableName,strReturnField,strFilterField,strFilterValue,sqlFilter)
{
   var date=new Date();
   var time=date.getMilliseconds();
   var aryTarget=strTarget.split(',');
   var aryField=strReturnField.split(',');
   if(aryTarget.length!=aryField.length)
   {
      alert('参数有错！');
      return false;
   }
   
   var localPath = location.pathname;
   if(localPath.substring(0,1)!='/')
   {
      localPath='/'+localPath;
   }
  // alert(localPath);
   var path=localPath.split('/'); 
   var num = path.length;
   rootPath = "";
   if(num>2)
   {
        num = num-1;  //本地测试减1，发布成网站不减
       for(var i=2;i<num;i++)
       {
         rootPath=rootPath+"../";      
       }
   }
    if(sqlFilter == null)
   {
        sqlFilter = "";
   }
  // alert(rootPath);
   if (window.document.all)//IE判断window.showModalDialog!=null
   {
      var returnvalue;
       if(strFilterField!=null && strFilterField.length>0)
       {
         returnvalue=window.showModalDialog(rootPath+"Common/SelectDialog2.aspx?TableName="+strTableName+"&ReturnField="+strReturnField+"&filterField="+strFilterField+"&filterValue="+strFilterValue+"&sqlFilter="+sqlFilter+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=700px;dialogHeight=460px");
       }
       else
       {
        returnvalue=window.showModalDialog(rootPath+"Common/SelectDialog2.aspx?TableName="+strTableName+"&ReturnField="+strReturnField+"&sqlFilter="+sqlFilter+"&time="+time,"",
                                         "top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no;dialogWidth=700px;dialogHeight=460px");
       }
       if(returnvalue==null)
       {
          return false;
       }
       else if(returnvalue!='')
       {
           var aryTarget=strTarget.split(',');
           var aryValue=new Array();
           aryValue=returnvalue.split('|');
           for(var i=0;i<aryTarget.length;i++)
           {
              var e=document.getElementById(aryTarget[i]);
              if(e!=null)
              {
                e.value=aryValue[i];
              }
           }  
           return false; 
       } 
   }
   else
   {
        //参数
        var strPara = "height=450px;width=500px;help=off;resizable=off;scroll=no;status=off;modal=yes;dialog=yes";
        //打开窗口
        var url=rootPath+"Common/SelectDialog2.aspx?TableName="+strTableName+"&ReturnField="+strReturnField+"&time="+time+"&targetControls="+strTarget;
        var DialogWin = window.open(url,"myOpen",strPara,true);
   } 
  }

