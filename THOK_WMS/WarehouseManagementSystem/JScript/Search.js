// JScript 文件


//搜索提示2011.4.25
var searchReq = createReq();
      /**//* 创建XMLHttpRequest的第一种方法
     try{
             searchReq = new ActiveXObject('Msxml2.XMLHTTP');
         }
         catch(e){
             try{
                 searchReq = new ActiveXObject('Microsoft.XMLHTTP');
             }
             catch(e){
                 try{
                     searchReq = new XMLHttpRequest();
                 }
                 catch(e)
                 {}
             }
         }
         */
     //创建XMLHttpRequest的第二种方法
     function createReq(){
         var httpReq;
         
         if(window.XMLHttpRequest){
             httpReq = new XMLHttpRequest();
             if(httpReq.overrideMimeType){
                 httpReq.overrideMimeType('text/xml');
             }
         }
         else if(window.ActiveXObject){
             try{
                  httpReq = new ActiveXObject('Msxml2.XMLHTTP');
             }
             catch(e){
                 try{
                         httpReq = new ActiveXObject('Microsoft.XMLHTTP');
                 }
                 catch(e){
                 }
              }  
         }
         return httpReq;
      }
    //发送HTTP请求，当输入框的内容变化时，会调用该函数
     function searchSuggest(){
            var str = escape(document.getElementById("txtProductCode").value);
            searchReq.open("get","Server.aspx?searchText="+str,true);
            searchReq.onreadystatechange = handleSearchSuggest;
            searchReq.send(null);
      }
     
     //当 onreadystatechange 值变化时，会调用该函数
     //注意searchSuggest()中的这一句searchReq.onreadystatechange = handleSearchSuggest;
     function handleSearchSuggest(){
        if(searchReq.readyState == 4){
            if(searchReq.status == 200){
                var suggestText = document.getElementById("search_suggest");
                var sourceText = searchReq.responseText.split("\n");
                if(sourceText.length>1){
                   suggestText.style.display="";
                   suggestText.innerHTML = "";
                    for(var i=0;i<sourceText.length-1;i++) {
                        var s='<div onmouseover="javascript:suggestOver(this);"';
                        s+=' onmouseout="javascript:suggestOut(this);" ';
                        s+=' onclick="javascript:setSearch(this.innerHTML);" ';
                        s+=' class="suggest_link">' +sourceText[i]+'</div>';
                        suggestText.innerHTML += s;
                   }
                }
                else{
                   suggestText.style.display="none";
                }
           }
      }
     }
     
     function suggestOver(div_value){
        div_value.className = "suggest_link_over";
     }
     
     function suggestOut(div_value){
        div_value.className = "suggest_link";
     }
     
     function setSearch(obj){
        document.getElementById("txtProductCode").value = obj;
        var div = document.getElementById("search_suggest");
        div.innerHTML = "";
        div.style.display="none"; 
    }
    
    function tbblur(){
        var div = document.getElementById("search_suggest");
        //div.innerHTML = "";
        div.style.display="none"; 
    }
    //-->
